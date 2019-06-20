-- Create table
create table HT_PROD_INOUT_REPORT
(
  PROD_CODE   VARCHAR2(10),
  PROD_DATE   VARCHAR2(10),
  TEAM        VARCHAR2(2),
  PARA_CODE   VARCHAR2(20),
  PARA_VALUE  FLOAT,
  RECORD_TIME VARCHAR2(19),
  EDITOR      VARCHAR2(10)
)
tablespace ZS_DATA
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
-- Add comments to the table 
comment on table HT_PROD_INOUT_REPORT
  is '投入产出人工录入表';
-- Add comments to the columns 
comment on column HT_PROD_INOUT_REPORT.PROD_CODE
  is '产品';
comment on column HT_PROD_INOUT_REPORT.PROD_DATE
  is '生产日期';
comment on column HT_PROD_INOUT_REPORT.TEAM
  is '班组';
comment on column HT_PROD_INOUT_REPORT.PARA_CODE
  is '参数';
comment on column HT_PROD_INOUT_REPORT.PARA_VALUE
  is '参数值';
comment on column HT_PROD_INOUT_REPORT.RECORD_TIME
  is '记录时间';
comment on column HT_PROD_INOUT_REPORT.EDITOR
  is '记录人';
-- Create/Recreate indexes 
create index P_PROD_INOUT on HT_PROD_INOUT_REPORT (PROD_DATE)
  tablespace ZS_DATA
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );


--报表---


create or replace procedure create_Prod_daily_output_M
  ( month in varchar)

  Authid Current_User
 is
 SQL_prod varchar2(20000);
  RES_SQL VARCHAR2(3000);
  COL_SQL  VARCHAR2(3000);
  LV_SQL VARCHAR2(30000);
  --存放连接的SQL
  SQL_COMMOND VARCHAR2(8000);
  CURSOR CUR IS
SELECT distinct t.prod_date as time FROM ht_prod_inout_report t  where  substr(t.prod_date,1,7) = month  order by time;
 BEGIN
  --定义查询开头

  SQL_COMMOND := 'SELECT para_code ';
  SQL_prod := 'select prod_code ';
  COL_SQL := '';
  FOR I IN CUR LOOP    --将结果相连接    to_char(sysdate,'yyyy-mm-dd
    SQL_COMMOND := SQL_COMMOND ||', round(case para_code when ''7030500029'' then avg(DECODE(prod_date,''' ||I.time ||
                   ''',para_value,null))  else sum(DECODE(prod_date,''' ||I.time ||
                   ''',para_value,null)) end ,0) "'||I.time||'"';
    SQL_prod := SQL_prod ||', round(nvl(max(DECODE(t.prod_date||t.para_code,''' ||I.time ||
                   '7030500029'',para_value,null)),0),0) *round(nvl(sum(DECODE(t.prod_date||t.para_code,''' ||I.Time ||
                   '7030500030'',para_value,null)),0),0) "'||I.time||'"';

    COL_SQL:= COL_SQL || ',t."'||I.time||'"';
  END LOOP;
  RES_SQL := 'select   t2.para_name as 项目名称'||COL_SQL ||'   from ht_pub_tech_para t2 left join (';
  SQL_COMMOND := SQL_COMMOND || ' from ht_prod_inout_report  where prod_date like ''' || month ||'%'' group by para_code order by para_code';
   LV_SQL      := ' create or replace view hv_prod_daily_output_subM as ' ||RES_SQL || SQL_COMMOND || ') t on t2.para_code = t.para_code  ';
   LV_SQL := LV_SQL||'  where t2.para_type like ''________1%'' order by  t2.para_code';
   EXECUTE IMMEDIATE LV_SQL;

   SQL_prod := SQL_prod ||' from ht_prod_inout_report  t where t.prod_date like ''' || month ||'%'' group by t.prod_code ';
   RES_SQL :='select g2.prod_name||''(''||t.prod_code||'')'' as prod_name' || COL_SQL || ' from (';
   lv_sql := 'create or replace view hv_prod_daily_outputM as '|| RES_SQL||SQL_prod||') t left join ht_pub_prod_design g2 on t.prod_code = g2.prod_code  ';
   EXECUTE IMMEDIATE LV_SQL;

END;

create or replace procedure Create_Prod_Daily_Tout_M
 ( datetime in varchar)
  Authid Current_User
 is
  SQL_COMMOND VARCHAR2(8000);
  LV_COMMOND VARCHAR2(8000);
  SUB_COMMOND VARCHAR2(8000);

  --定义游标
CURSOR CUR IS
   select distinct t.team as team_code from ht_prod_inout_report t where t.prod_date = datetime order by t.team;

  --定义查询开头
 Begin
    LV_COMMOND := 'select p.prod_code,p.para_name,p.para_code';
    SUB_COMMOND := '';
  FOR I IN CUR LOOP
    LV_COMMOND := LV_COMMOND || ',round(g'||I.team_code||'.para_value,2) as value'||I.team_code;
    --将结果相连接
    SUB_COMMOND := SUB_COMMOND || ' left join (select t.prod_code,t.para_code,t.team,t.para_value ' ;
    SUB_COMMOND := SUB_COMMOND || ' from ht_pub_tech_para r ';
    SUB_COMMOND := SUB_COMMOND || ' left join ht_prod_inout_report t  on r.para_code = t.para_code where r.para_type like ''________1%'' ';
    SUB_COMMOND := SUB_COMMOND || ' and   t.prod_date = '''||datetime ||''' and t.team = '''|| I.team_code ||'''  )';
    SUB_COMMOND := SUB_COMMOND || ' g'||I.team_code||' on g'||I.team_code||'.prod_code = p.prod_code and g'||I.team_code||'.para_code = p.para_code  ' ;
  END LOOP;
  SQL_COMMOND :=  LV_COMMOND ||' from ';
  SQL_COMMOND := SQL_COMMOND || ' (select distinct  t.prod_code,t.para_code,r.para_name from   ht_pub_tech_para r  left join ht_prod_inout_report t on r.para_code = t.para_code where t.prod_date =  '''|| datetime ||''' and r.para_type like ''________1%'' ) p ';
  SQL_COMMOND := SQL_COMMOND ||SUB_COMMOND;
  SQL_COMMOND := SQL_COMMOND || ' order by p.para_code ';
  SQL_COMMOND := 'create or replace view   hv_prod_daily_ToutputM as '|| SQL_COMMOND;
  EXECUTE IMMEDIATE SQL_COMMOND;

END;

