/*create table HT_SVR_USER
(
  ID           VARCHAR2(10) not null,
  NAME         VARCHAR2(100) not null,
  WEIGHT       VARCHAR2(10),
  PARENTID     VARCHAR2(50),
  MOBILE       VARCHAR2(20),
  PHONE        VARCHAR2(20),
  RTXID        VARCHAR2(20),
  GENDER       VARCHAR2(10),
  LOGINNAME    VARCHAR2(50),
  PASSWORD     VARCHAR2(50),
  EMAIL        VARCHAR2(50),
  LEVELGROUPID VARCHAR2(50) not null,
  RELATION     VARCHAR2(100),
  IS_LOCAL     VARCHAR2(1) default 0,
  IS_DEL       VARCHAR2(1) default 0,
  IS_SYNC      VARCHAR2(1) default 0,
  C_STATE      VARCHAR2(1) default 0,
  DESCRIPTION  VARCHAR2(1024),
  ROLE         VARCHAR2(5),
  PSD          VARCHAR2(20),
  TEAM_CODE    VARCHAR2(2)
)*/
--在线工艺段得分 参数  班组 产品 时间  
select a.section,round(exp(sum(ln(power((case a.quarate when 0 then 0.1 else a.quarate end),nvl(c.weight,0))))),4)*100 as score from (select substr(r.para_code,1,5) as section,r.team,r.para_code,sum(r.quarate*r.count)/sum(r.count) as quarate from ht_qlt_data_record r left join ht_pub_tech_para s on s.para_code = r.para_code where s.para_type like '______1%' and r.team = '$team$' and  r.prod_code = '$brand$' and substr(r.b_time,1,10) = '$startDate$' group by r.para_code,r.team) a   left join ht_qlt_collection c on c.para_code = a.para_code   group by a.section 
--横表  参数  班组 产品 时间  
 select g2.section_name,g1.score from (  select a.section,round(exp(sum(ln(power((case a.quarate when 0 then 0.1 else a.quarate end),nvl(c.weight,0))))),4)*100 as score from (select substr(r.para_code,1,5) as section,r.team,r.para_code,sum(r.quarate*r.count)/sum(r.count) as quarate from ht_qlt_data_record r left join ht_pub_tech_para s on s.para_code = r.para_code where s.para_type like '______1%' and r.team = '$team$' and  r.prod_code = '$brand$' and substr(r.b_time,1,10) = '$startDate$' group by r.para_code,r.team) a   left join ht_qlt_collection c on c.para_code = a.para_code   group by a.section )g1 left join ht_pub_tech_section g2 on g1.section = g2.section_code order by g2.section_name;

--在线考核分 按班组、日期、产品
select  t.section_name||'('||r.score||')' as section,h.para_name,s.quarate,s.stddev,s.absdev,s.cpk from (  select a.section,round(exp(sum(ln(power((case a.quarate when 0 then 0.01 else a.quarate end),nvl(c.weight,0))))),4)*100 as score from (select substr(r.para_code,1,5) as section,r.team,r.para_code,sum(r.quarate*r.count)/sum(r.count) as quarate from ht_qlt_data_record r left join ht_pub_tech_para s on s.para_code = r.para_code where s.para_type like '______1%' and  r.team = '$team$' and  r.prod_code = '$brand$' and substr(r.b_time,1,10) = '$startDate$' group by r.para_code,r.team) a  left join ht_qlt_collection c on c.para_code = a.para_code  group by a.section )r left join (select r.para_code,r.team,round(sum(r.quarate*r.count)/sum(r.count),2) as quarate,round(sqrt(sum(r.stddev*r.stddev * (r.count -1))/(sum(r.count)-1)),2) as stddev,round(sum(r.absdev*(r.count-1))/(sum(r.count)-1),2) as absdev,round(avg(r.cpk),2) as cpk from ht_qlt_data_record r left join ht_pub_tech_para s on s.para_code = r.para_code where s.para_type like '______1%'  and r.team = '$team$' and r.prod_code = '$brand$' and substr(r.b_time,1,10) = '$startDate$' group by r.para_code,r.team ) s on substr(s.para_code,1,5) = r.section  left join ht_pub_tech_section t on t.section_code = r.section  left join ht_pub_tech_para h on h.para_code = s.para_code order by section;
--在线得分
select round(exp(sum(ln(power((case g1.score when 0 then 0.1 else g1.score end),nvl(g2.weight,0))))),4)*100 as score from (select a.section,round(exp(sum(ln(power((case a.quarate when 0 then 0.1 else a.quarate end),nvl(c.weight,0))))),4) as score from (select substr(r.para_code,1,5) as section,r.team,r.para_code,sum(r.quarate*r.count)/sum(r.count) as quarate from ht_qlt_data_record r left join ht_pub_tech_para s on s.para_code = r.para_code where s.para_type like '______1%' and r.team = '$team$' and  r.prod_code = '$brand$' and substr(r.b_time,1,10) = '$startDate$' group by r.para_code,r.team) a   left join ht_qlt_collection c on c.para_code = a.para_code   group by a.section )g1 left join ht_pub_tech_section g2 on g1.section = g2.section_code 

--在线工艺段得分 参数   产品 时间  
 select a.section,round(exp(sum(ln(power((case a.quarate when 0 then 0.01 else a.quarate end),nvl(c.weight,0))))),4)*100 as score from (select substr(r.para_code,1,5) as section,r.para_code,sum(r.quarate*r.count)/sum(r.count) as quarate from ht_qlt_data_record r left join ht_pub_tech_para s on s.para_code = r.para_code where s.para_type like '______1%' and  r.prod_code = '$brand$' and substr(r.b_time,1,7) = '$month$' group by r.para_code) a   left join ht_qlt_collection c on c.para_code = a.para_code   group by a.section 

--横表  参数   产品 时间  
 select g2.section_name,g1.score from (  select a.section,round(exp(sum(ln(power((case a.quarate when 0 then 0.1 else a.quarate end),nvl(c.weight,0))))),4)*100 as score from (select substr(r.para_code,1,5) as section,r.para_code,sum(r.quarate*r.count)/sum(r.count) as quarate from ht_qlt_data_record r where  r.prod_code = '$brand$' and substr(r.b_time,1,7) = '$month$' group by r.para_code) a  left join ht_pub_tech_para b on b.para_code  = a.para_code and b.para_type  like '______1%'  left join ht_qlt_collection c on c.para_code = a.para_code   group by a.section )g1 left join ht_pub_tech_section g2 on g1.section = g2.section_code order by g2.section_name;

--在线考核分 、日期、产品
select  t.section_name||'('||r.score||')' as section,h.para_name,s.quarate,s.stddev,s.absdev,s.cpk from (  select a.section,round(exp(sum(ln(power((case a.quarate when 0 then 0.01 else a.quarate end),nvl(c.weight,0))))),4)*100 as score from (select substr(r.para_code,1,5) as section,r.para_code,sum(r.quarate*r.count)/sum(r.count) as quarate from ht_qlt_data_record r left join ht_pub_tech_para s on s.para_code = r.para_code where s.para_type like '______1%' and  r.prod_code = '$brand$' and substr(r.b_time,1,7) = '$month$' group by r.para_code) a   left join ht_qlt_collection c on c.para_code = a.para_code   group by a.section )r left join (select r.para_code,round(sum(r.quarate*r.count)/sum(r.count),2) as quarate,round(sqrt(sum(r.stddev*r.stddev * (r.count -1))/(sum(r.count)-1)),2) as stddev,round(sum(r.absdev*(r.count-1))/(sum(r.count)-1),2) as absdev,round(avg(r.cpk),2) as cpk from ht_qlt_data_record r left join ht_pub_tech_para s on s.para_code = r.para_code where s.para_type like '______1%'  and  r.prod_code = '$brand$' and substr(r.b_time,1,7) = '$month$' group by r.para_code,r.team ) s on substr(s.para_code,1,5) = r.section  left join ht_pub_tech_section t on t.section_code = r.section  left join ht_pub_tech_para h on h.para_code = s.para_code order by section

--在线得分
select round(exp(sum(ln(power((case g1.score when 0 then 0.1 else g1.score end),nvl(g2.weight,0))))),4)*100 as score from  (select a.section,round(exp(sum(ln(power((case a.quarate when 0 then 0.01 else a.quarate end),nvl(c.weight,0))))),4)*100 as score from (select substr(r.para_code,1,5) as section,r.para_code,sum(r.quarate*r.count)/sum(r.count) as quarate from ht_qlt_data_record r left join ht_pub_tech_para s on s.para_code = r.para_code where s.para_type like '______1%' and  r.prod_code = '$brand$' and substr(r.b_time,1,7) = '$month$' group by r.para_code) a   left join ht_qlt_collection c on c.para_code = a.para_code   group by a.section )g1 left join ht_pub_tech_section g2 on g1.section = g2.section_code 


create or replace view hv_qlt_online_score as
select g.prodday, g.prod_code ,g.team,substr(g.para_code,1,5) as section,g.para_code, m.weight,g.avg ,g.quarate ,g.stddev ,g.absdev ,g.range,g.cpk as cpk,g.count,g.b_time ,g.e_time  from
(select substr(t.b_time,1,10) as prodday, t.prod_code,t.para_code,t.team,round(sum(t.avg*t.count)/sum(t.count),2) as avg,sum(t.count) as count,min(t.min) as min,max(t.max) as max,round(sum(t.quanum)/sum(t.count),2) as quarate,round(sum(t.upcount)/sum(t.count),2) as uprate,round(sum(t.downcount)/sum(t.count),2) as downrate,round(sqrt(sum(t.stddev*t.stddev*(t.count-1))/(sum(t.count)-1)),2) as stddev,round(sum(t.absdev *(t.count-1))/(sum(t.count)-1),2) as absdev,round((max(t.max) - min(t.min)),2) as range ,round(avg(t.cpk),3) as cpk,min(b_time) as b_time,max(e_time) as e_time  from ht_qlt_data_record t left join ht_pub_tech_para s on s.para_code = t.para_code where s.para_type like '______1%' group by (t.plan_id,t.para_code,t.prod_code,t.team ,substr(t.b_time,1,10))）g
 left join ht_qlt_collection m on m.para_code = g.para_code  order by g.count;
