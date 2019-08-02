----------------------------------------------------
-- Export file for user ZS18                      --
-- Created by Administrator on 2019/8/1, 13:44:39 --
----------------------------------------------------

spool ZS180801.log

prompt
prompt Creating view HT_QLT_PROCESS_AVG_SUB
prompt ====================================
prompt

prompt
prompt Creating view HV_CRAFT_OFFLINE
prompt ==============================
prompt
create or replace view zs18.hv_craft_offline as
select distinct t.ID,t.prod_code,t.team_id,'��Ʒ���'  as inspect_type ,r.inspect_code, h.name as insgroup,r.inspect_name,nvl(t.inspect_value,0) as value,s.lower_value||'~'||s.upper_value as range,r.unit,nvl(j.status,0) as status  ,nvl(s.minus_score,0) as minus_score,t.record_time  from ht_qlt_inspect_record t left join ht_qlt_inspect_stdd s on s.inspect_code = t.inspect_code left join  ht_qlt_inspect_proj r  on t.inspect_code = r.inspect_code left join ht_inner_inspect_group h on h.id = r.inspect_group left join ht_qlt_inspect_event j on j.record_id = t.id where r.inspect_type = '1' and not( t.inspect_value >s.lower_value and t.inspect_value <s.upper_value)
 union select t.ID, t.prod_code,t.team_id,'���̼���'  as inspect_type,r.inspect_code,h.section_name as insgroup,r.inspect_name,nvl(t.inspect_value,0) as value,s.lower_value||'~'||s.upper_value as range,r.unit,nvl(j.status,0) as status ,nvl(s.minus_score,0) as minus_score,t.record_time   from ht_qlt_inspect_record t  left join ht_qlt_inspect_stdd s on s.inspect_code = t.inspect_code left join ht_qlt_inspect_proj r  on r.inspect_code = t.inspect_code left join ht_pub_tech_section h on h.section_code = r.inspect_group left join ht_qlt_inspect_event j on j.record_id = t.id where r.inspect_type = '0' and not( t.inspect_value >s.lower_value and t.inspect_value <s.upper_value) order by inspect_type,insgroup;



prompt
prompt Creating view HV_QLT_INSPECT_FACTORY
prompt ====================================
prompt
create or replace view zs18.hv_qlt_inspect_factory as
select id, product_code,  factory_time as ����ʱ��, factory_address as ��ַ, PRODUCT_NAME as ��Ʒ
    from HT_QLT_INSPECT_FACTORY;




ect t.prod_code ,t.team_id,t.Record_time,round(Avg(decode(t.inspect_code,'703100001009',t.inspect_value))) "������(0~10)%1",round(Avg(decode(t.inspect_code,'703100002001',t.inspect_value))) "ˮ��(0~5)%1",round(Avg(decode(t.inspect_code,'703100002003',t.inspect_value))) "���ֵ(10~20)%2",round(Avg(decode(t.inspect_code,'703100002004',t.inspect_value))) "��˿��(0~10)%2",round(Avg(decode(t.inspect_code,'703100002005',t.inspect_value))) "��˿��(0~20)%3",round(Avg(decode(t.inspect_code,'703100003001',t.inspect_value))) "ˮ������(0~10)%1",round(Avg(decode(t.inspect_code,'703100003002',t.inspect_value))) "��ֲ���(8~17)%2",round(Avg(decode(t.inspect_code,'703100003003',t.inspect_value))) "�ܵ�(4~20)%3", 100-sum(case  when t.inspect_value >= nvl(r.lower_value,0) and t.inspect_value <=nvl(r.upper_value,0) then 0 else  nvl(r.minus_score,0) end ) as �÷�  from ht_qlt_inspect_record t left join ht_qlt_inspect_stdd r on r.inspect_code = t.inspect_code left join ht_qlt_inspect_proj s on s.inspect_code = t.inspect_code where s.inspect_group in ('1','2','3') group by t.prod_code,t.team_id,t.Record_time;

prompt
prompt Creating view HV_QLT_PROCESS_AVG
prompt ================================
prompt
create or replace view zs18.hv_qlt_process_avg as
select s.record_time, t.inspect_code, t.inspect_name || '('||r.lower_value ||'~'|| r.upper_value||')' as range,g2.avrg,g2.quarate,g2.std ,g1.avrg as avrgs ,g1.quarate as quarates,h.team_name, s.inspect_value from ht_qlt_inspect_proj t left join ht_qlt_inspect_stdd r on r.inspect_code = t.inspect_code left join ht_qlt_inspect_record s on s.inspect_code = t.inspect_code  left join ht_sys_team h on h.team_code = s.team_id left join ( select t.inspect_code,round(avg(t.inspect_value),2) as avrg,count(case when t.inspect_value >= r.lower_value and t.inspect_value <= r.upper_value then 1 else  0 end )/ count(*) as quarate from ht_qlt_inspect_record t left join ht_qlt_inspect_stdd r on r.inspect_code = t.inspect_code where  substr(t.inspect_code,4,1) = '0' group by t.inspect_code) g1 on g1.inspect_code = t.inspect_code  left join (select t.inspect_code,round(avg(t.inspect_value),2) as avrg,count(case when t.inspect_value >= r.lower_value and t.inspect_value <= r.upper_value then 1 else  0 end )/ count(*) as quarate,round(stddev(t.inspect_value),2) as std from ht_qlt_inspect_record t left join ht_qlt_inspect_stdd r on r.inspect_code = t.inspect_code where  substr(t.inspect_code,4,1) = '0'  group by t.inspect_code)g2 on g2.inspect_code = t.inspect_code where t.inspect_type = '0' and t.is_del ='0' order by  t.inspect_code;

prompt
prompt Creating view HV_QLT_PROCESS_AVG_SUB
prompt ====================================
prompt
create or replace view zs18.hv_qlt_process_avg_sub as
select s.record_time, t.inspect_code,  substr(t.inspect_name,0,4) as range,g2.avrg,g2.quarate,g2.std ,g1.avrg as avrgs ,g1.quarate as quarates,h.team_name, s.inspect_value from ht_qlt_inspect_proj t left join ht_qlt_inspect_stdd r on r.inspect_code = t.inspect_code left join ht_qlt_inspect_record s on s.inspect_code = t.inspect_code  left join ht_sys_team h on h.team_code = s.team_id left join ( select t.inspect_code,round(avg(t.inspect_value),2) as avrg,count(case when t.inspect_value >= r.lower_value and t.inspect_value <= r.upper_value then 1 else  0 end )/ count(*) as quarate from ht_qlt_inspect_record t left join ht_qlt_inspect_stdd r on r.inspect_code = t.inspect_code where  substr(t.inspect_code,4,1) = '0' group by t.inspect_code) g1 on g1.inspect_code = t.inspect_code  left join (select t.inspect_code,round(avg(t.inspect_value),2) as avrg,count(case when t.inspect_value >= r.lower_value and t.inspect_value <= r.upper_value then 1 else  0 end )/ count(*) as quarate,round(stddev(t.inspect_value),2) as std from ht_qlt_inspect_record t left join ht_qlt_inspect_stdd r on r.inspect_code = t.inspect_code where  substr(t.inspect_code,4,1) = '0'  group by t.inspect_code)g2 on g2.inspect_code = t.inspect_code where t.inspect_type = '0' and t.is_del ='0' order by  t.inspect_code;

prompt
prompt Creating view HV_QLT_PROCESS_DAILY_DATA
prompt =======================================
prompt
create or replace view zs18.hv_qlt_process_daily_data as
select t.record_time, p.prod_code, zz.team_name, ss.shift_name,round(Avg(decode(t.inspect_code,'703070302002',t.inspect_value)),2) "��ˮ��1(28.5~31.5)%0",round(Avg(decode(t.inspect_code,'703070303001',t.inspect_value)),2) "��˿ˮ��1(31.5~34.5)%0",round(Avg(decode(t.inspect_code,'703070303002',t.inspect_value)),2) "��˿ˮ��2(31.5~34.5)%0",round(Avg(decode(t.inspect_code,'703070303003',t.inspect_value)),2) "�����������ˮ��1(73~77)%0",round(Avg(decode(t.inspect_code,'703070303005',t.inspect_value)),2) "һ����������ˮ��1(49~53)%0",round(Avg(decode(t.inspect_code,'703070303006',t.inspect_value)),2) "һ����������ˮ��2(49~53)%0",round(Avg(decode(t.inspect_code,'703070303007',t.inspect_value)),2) "������������ˮ��1(25.5~28.5)%0",round(Avg(decode(t.inspect_code,'703070303008',t.inspect_value)),2) "������������ˮ��2(25.5~28.5)%0",round(Avg(decode(t.inspect_code,'703070303009',t.inspect_value)),2) "������������ˮ��3(25.5~28.5)%0",round(Avg(decode(t.inspect_code,'703070304001',t.inspect_value)),2) "������1(10~20)%1",round(Avg(decode(t.inspect_code,'703070304002',t.inspect_value)),2) "������2(0~0)%0",round(Avg(decode(t.inspect_code,'703070304003',t.inspect_value)),2) "����������ˮ��1(31.5~34.5)%0",round(Avg(decode(t.inspect_code,'703070304004',t.inspect_value)),2) "����������ˮ��2(31.5~34.5)%0",round(Avg(decode(t.inspect_code,'703070304005',t.inspect_value)),2) "�����ͳ���ˮ��1(13.5~16.5)%0",round(Avg(decode(t.inspect_code,'703070304006',t.inspect_value)),2) "�����ͳ���ˮ��2(13.5~16.5)%0",round(Avg(decode(t.inspect_code,'703070305001',t.inspect_value)),2) "��Ʒˮ��1(12.5~14.5)%25",round(Avg(decode(t.inspect_code,'703070305002',t.inspect_value)),2) "��Ʒˮ��2(12.5~14.5)%25",round(Avg(decode(t.inspect_code,'703070305003',t.inspect_value)),2) "��Ʒˮ��3(12.5~14.5)%25",round(Avg(decode(t.inspect_code,'703070305004',t.inspect_value)),2) "��Ʒˮ��4(12.5~14.5)%25",round(Avg(decode(t.inspect_code,'703070305005',t.inspect_value)),2) "��Ʒˮ��5(12.5~14.5)%0",round(Avg(decode(t.inspect_code,'703070305006',t.inspect_value)),2) "��Ʒˮ��6(12.5~14.5)%0",round(Avg(decode(t.inspect_code,'703070305007',t.inspect_value)),2) "��Ʒˮ��7(12.5~14.5)%0",round(Avg(decode(t.inspect_code,'703070305008',t.inspect_value)),2) "��Ʒˮ��8(12.5~14.5)%0",round(Avg(decode(t.inspect_code,'703070305009',t.inspect_value)),2) "��Ʒˮ��9(12.5~14.5)%0", 100-sum(case  when t.inspect_value >= nvl(r.lower_value,0) and t.inspect_value <=nvl(r.upper_value,0) then 0 else  nvl(r.minus_score,0) end ) as �÷�  from ht_qlt_inspect_record t left join ht_qlt_inspect_stdd r on r.inspect_code = t.inspect_code left join ht_qlt_inspect_proj s on s.inspect_code = t.inspect_code left join ht_pub_prod_design p on p.prod_code = t.prod_code left join ht_sys_team zz on zz.team_code = t.team_id left join ht_sys_shift ss on ss.shift_code = t.shift_id  where s.INSPECT_TYPE = '0' and s.is_Del = '0' group by t.prod_code,t.team_id,t.shift_id, t.Record_time, p.prod_code, zz.team_name, ss.shift_name order by t.record_time desc;

prompt
prompt Creating view HV_QLT_PROCESS_INSPECT_CODE
prompt =========================================
prompt
create or replace view zs18.hv_qlt_process_inspect_code as
select r.inspect_code,s.section_name ,r.inspect_name,0 as value,t.lower_value,t.upper_value,r.unit,'' as status,t.minus_score from ht_qlt_inspect_proj r left join ht_pub_tech_section s on s.section_code = r.inspect_group left join ht_qlt_inspect_stdd t on t.inspect_code = r.inspect_code and t.is_del = '0' where r.inspect_type = '0' and r.is_del = '0' order by r.inspect_code;


prompt
prompt Creating view HV_QLT_SENSOR_REALREC2
prompt ====================================
prompt
create or replace view zs18.hv_qlt_sensor_realrec2 as
select r.sensor_month,t.Record_time, t.��Ʒ,  s.prod_name as ��Ʒ���� ,t."����(15)",t."��ζ(25)",t."����(15)",t."���ո�(15)",t."�����(15)",t."����(15)",t."�ܵ÷�",round(100-abs(t.�ܵ÷� - s.sensor_score)/s.sensor_score * 100,2) as ʵ�ʵ÷� from hv_qlt_sensor_report t left join ht_qlt_sensor_record r on r.id = t.main_id left join ht_pub_prod_design s on s.prod_code = t.��Ʒ  order by t.��Ʒ,r.sensor_month;





prompt
prompt Creating procedure CREATE_PROCESS_DATA
prompt ======================================
prompt
create or replace procedure zs18.CREATE_PROCESS_DATA Authid Current_User is
 SQL_COMMOND VARCHAR2(8000);
  --�����α�
  CURSOR CUR IS
select distinct t.inspect_code,t.inspect_name||'('||nvl(r.lower_value,0)||'~'||nvl(r.upper_value,0)||')%'||nvl(r.minus_score,0) as inspect_name,r.lower_value,r.upper_value,r.minus_score from ht_qlt_inspect_proj t left join ht_qlt_inspect_stdd r on r.inspect_code = t.inspect_code left join ht_qlt_sensor_record_sub s on s.inspect_code = t.inspect_code where t.INSPECT_TYPE = '0'  and t.is_del = '0' order by inspect_code;
BEGIN
 -----------------------------------------------------
  SQL_COMMOND := 'select t.record_time, p.prod_code, zz.team_name, ss.shift_name';
  FOR I IN CUR LOOP
    --�����������
    SQL_COMMOND := SQL_COMMOND || ',round(Avg(decode(t.inspect_code,''' || I.inspect_code ||
                   ''',t.inspect_value)),2) "'||I.inspect_name||'"' ;
  END LOOP;
  SQL_COMMOND := SQL_COMMOND ||', 100-sum(case  when t.inspect_value >= nvl(r.lower_value,0) and t.inspect_value <=nvl(r.upper_value,0) then 0 else  nvl(r.minus_score,0) end ) as �÷� ';
  SQL_COMMOND := SQL_COMMOND || ' from ht_qlt_inspect_record t left join ht_qlt_inspect_stdd r on r.inspect_code = t.inspect_code left join ht_qlt_inspect_proj s on s.inspect_code = t.inspect_code left join ht_pub_prod_design p on p.prod_code = t.prod_code left join ht_sys_team zz on zz.team_code = t.team_id left join ht_sys_shift ss on ss.shift_code = t.shift_id  where s.INSPECT_TYPE = ''0'' and s.is_Del = ''0'' group by t.prod_code,t.team_id,t.shift_id, t.Record_time, p.prod_code, zz.team_name, ss.shift_name order by t.record_time desc';
  SQL_COMMOND := 'create or replace view hv_qlt_process_daily_data as '|| SQL_COMMOND;
  DBMS_OUTPUT.PUT_LINE(SQL_COMMOND);
  EXECUTE IMMEDIATE SQL_COMMOND;


end CREATE_PROCESS_DATA;








