--------------------------------------------
-- Export file for user ZS18              --
-- Created by wicky on 2019/8/2, 15:55:56 --
--------------------------------------------

spool zs180802.log

prompt
prompt Creating table HT_PUB_PROD_DESIGN
prompt =================================
prompt
create table ZS18.HT_PUB_PROD_DESIGN
(
  ID                 INTEGER,
  PROD_CODE          VARCHAR2(32) not null,
  PROD_NAME          VARCHAR2(100),
  PACK_NAME          VARCHAR2(32),
  HAND_MODE          VARCHAR2(32),
  TECH_STDD_CODE     VARCHAR2(30),
  MATER_FORMULA_CODE VARCHAR2(30),
  AUX_FORMULA_CODE   VARCHAR2(30),
  COAT_FORMULA_CODE  VARCHAR2(30),
  IS_VALID           VARCHAR2(1) default 1,
  IS_DEL             VARCHAR2(1) default 0,
  REMARK             VARCHAR2(256),
  CREATEOR_ID        VARCHAR2(32),
  CREATE_TIME        VARCHAR2(19),
  MODIFY_ID          VARCHAR2(32),
  MODIFY_TIME        VARCHAR2(19),
  STANDARD_VALUE     VARCHAR2(32),
  XY_PROD_CODE       VARCHAR2(32),
  QLT_CODE           VARCHAR2(500),
  B_FLOW_STATUS      VARCHAR2(2) default -1,
  PATH_CODE          VARCHAR2(100),
  FLA_FORMULA_CODE   VARCHAR2(30),
  SENSOR_SCORE       NUMBER(5,2),
  INSPECT_STDD       VARCHAR2(32)
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
comment on column ZS18.HT_PUB_PROD_DESIGN.ID
  is '产品标识';
comment on column ZS18.HT_PUB_PROD_DESIGN.PROD_CODE
  is '产品编码';
comment on column ZS18.HT_PUB_PROD_DESIGN.PROD_NAME
  is '产品名称';
comment on column ZS18.HT_PUB_PROD_DESIGN.PACK_NAME
  is '包装规格';
comment on column ZS18.HT_PUB_PROD_DESIGN.HAND_MODE
  is '加工方式';
comment on column ZS18.HT_PUB_PROD_DESIGN.TECH_STDD_CODE
  is '技术标准编码';
comment on column ZS18.HT_PUB_PROD_DESIGN.MATER_FORMULA_CODE
  is '原料配方编码';
comment on column ZS18.HT_PUB_PROD_DESIGN.AUX_FORMULA_CODE
  is '辅料配方编码';
comment on column ZS18.HT_PUB_PROD_DESIGN.COAT_FORMULA_CODE
  is '涂布香精香料配方编码';
comment on column ZS18.HT_PUB_PROD_DESIGN.CREATEOR_ID
  is '创建人ID';
comment on column ZS18.HT_PUB_PROD_DESIGN.CREATE_TIME
  is '创建时间';
comment on column ZS18.HT_PUB_PROD_DESIGN.MODIFY_ID
  is '修改人标识';
comment on column ZS18.HT_PUB_PROD_DESIGN.MODIFY_TIME
  is '修改时间';
comment on column ZS18.HT_PUB_PROD_DESIGN.STANDARD_VALUE
  is '标准值';
comment on column ZS18.HT_PUB_PROD_DESIGN.QLT_CODE
  is '质量标准编码';
comment on column ZS18.HT_PUB_PROD_DESIGN.B_FLOW_STATUS
  is '审批状态 -1 未提交审批 0办理中 ２己通过 １未通过';
comment on column ZS18.HT_PUB_PROD_DESIGN.FLA_FORMULA_CODE
  is '香精香料';
comment on column ZS18.HT_PUB_PROD_DESIGN.SENSOR_SCORE
  is '产品感观评测基础分';
comment on column ZS18.HT_PUB_PROD_DESIGN.INSPECT_STDD
  is ' 离线 工艺检测标准';
alter table ZS18.HT_PUB_PROD_DESIGN
  add constraint PK_PROD_CODE primary key (PROD_CODE)
  using index 
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
alter table ZS18.HT_PUB_PROD_DESIGN
  add constraint FK_AUX_FORMULA_CODE foreign key (AUX_FORMULA_CODE)
  references ZS18.HT_QA_AUX_FORMULA (FORMULA_CODE) on delete set null
  disable;
alter table ZS18.HT_PUB_PROD_DESIGN
  add constraint FK_COAT_FORMULA_CODE foreign key (COAT_FORMULA_CODE)
  references ZS18.HT_QA_COAT_FORMULA (FORMULA_CODE) on delete set null
  disable;
alter table ZS18.HT_PUB_PROD_DESIGN
  add constraint FK_MATER_FORMULA_CODE foreign key (MATER_FORMULA_CODE)
  references ZS18.HT_QA_MATER_FORMULA (FORMULA_CODE) on delete set null
  disable;
alter table ZS18.HT_PUB_PROD_DESIGN
  add constraint FK_QLT_CODE foreign key (QLT_CODE)
  references ZS18.HT_QLT_STDD_CODE (QLT_CODE) on delete set null;
alter table ZS18.HT_PUB_PROD_DESIGN
  add constraint FK_TECH_STDD_CODE foreign key (TECH_STDD_CODE)
  references ZS18.HT_TECH_STDD_CODE (TECH_CODE) on delete set null;

prompt
prompt Creating table HT_QLT_INSPECT_STDD
prompt ==================================
prompt
create table ZS18.HT_QLT_INSPECT_STDD
(
  INSPECT_STDD_CODE VARCHAR2(32) not null,
  STANDARD_VOL      VARCHAR2(32),
  CREATE_ID         VARCHAR2(32),
  CREATE_DATE       VARCHAR2(19),
  MODIFY_ID         VARCHAR2(32),
  MODIFY_TIME       VARCHAR2(19),
  IS_VALID          VARCHAR2(1) default 1,
  IS_DEL            VARCHAR2(1) default 0,
  REMARK            VARCHAR2(512),
  INSPECT_STDD_NAME VARCHAR2(128),
  FLOW_STATUS       VARCHAR2(2) default -1
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
comment on table ZS18.HT_QLT_INSPECT_STDD
  is '工艺检查项目标准主表';
comment on column ZS18.HT_QLT_INSPECT_STDD.INSPECT_STDD_CODE
  is '标准编码  ISP + 三位流水号';
comment on column ZS18.HT_QLT_INSPECT_STDD.STANDARD_VOL
  is '标准版本号';
comment on column ZS18.HT_QLT_INSPECT_STDD.CREATE_ID
  is '创建人标识';
comment on column ZS18.HT_QLT_INSPECT_STDD.CREATE_DATE
  is '编制日期';
comment on column ZS18.HT_QLT_INSPECT_STDD.MODIFY_ID
  is '修改人标识';
comment on column ZS18.HT_QLT_INSPECT_STDD.MODIFY_TIME
  is '修改时间';
comment on column ZS18.HT_QLT_INSPECT_STDD.IS_DEL
  is '删除标识';
comment on column ZS18.HT_QLT_INSPECT_STDD.REMARK
  is '备注';
comment on column ZS18.HT_QLT_INSPECT_STDD.INSPECT_STDD_NAME
  is '标准名';
comment on column ZS18.HT_QLT_INSPECT_STDD.FLOW_STATUS
  is '审批状态';
alter table ZS18.HT_QLT_INSPECT_STDD
  add constraint PK_INSPECTC_STDD primary key (INSPECT_STDD_CODE)
  using index 
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

prompt
prompt Creating table HT_QLT_INSPECT_STDD_SUB
prompt ======================================
prompt
create table ZS18.HT_QLT_INSPECT_STDD_SUB
(
  INSPECT_CODE VARCHAR2(32) not null,
  UPPER_VALUE  FLOAT,
  LOWER_VALUE  FLOAT,
  MINUS_SCORE  INTEGER default 1,
  IS_DEL       VARCHAR2(1) default 0,
  REMARK       VARCHAR2(256),
  CREATE_ID    VARCHAR2(32),
  CREATE_TIME  VARCHAR2(19),
  STDD_CODE    VARCHAR2(12) not null
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
comment on table ZS18.HT_QLT_INSPECT_STDD_SUB
  is '工艺检查项目标准';
comment on column ZS18.HT_QLT_INSPECT_STDD_SUB.INSPECT_CODE
  is '检验项目编码';
comment on column ZS18.HT_QLT_INSPECT_STDD_SUB.UPPER_VALUE
  is '上限';
comment on column ZS18.HT_QLT_INSPECT_STDD_SUB.LOWER_VALUE
  is '下限';
comment on column ZS18.HT_QLT_INSPECT_STDD_SUB.MINUS_SCORE
  is '不符合怀况时单次扣分';
comment on column ZS18.HT_QLT_INSPECT_STDD_SUB.IS_DEL
  is '是否删除';
comment on column ZS18.HT_QLT_INSPECT_STDD_SUB.REMARK
  is '备注';
comment on column ZS18.HT_QLT_INSPECT_STDD_SUB.CREATE_ID
  is '创建人id';
comment on column ZS18.HT_QLT_INSPECT_STDD_SUB.CREATE_TIME
  is '创建时间';
comment on column ZS18.HT_QLT_INSPECT_STDD_SUB.STDD_CODE
  is ' 标准码';
alter table ZS18.HT_QLT_INSPECT_STDD_SUB
  add constraint PK_INSPECT_STD primary key (INSPECT_CODE, STDD_CODE)
  using index 
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

prompt
prompt Creating table HT_QLT_SENSOR_STDD
prompt =================================
prompt
create table ZS18.HT_QLT_SENSOR_STDD
(
  INSPECT_CODE VARCHAR2(32) not null,
  MINUS_SCORE  INTEGER default 1,
  IS_DEL       VARCHAR2(1) default 0,
  REMARK       VARCHAR2(256),
  CREATE_ID    VARCHAR2(32),
  CREATE_TIME  VARCHAR2(19)
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
comment on table ZS18.HT_QLT_SENSOR_STDD
  is '感观评测标准';
comment on column ZS18.HT_QLT_SENSOR_STDD.INSPECT_CODE
  is '检验项目编码';
comment on column ZS18.HT_QLT_SENSOR_STDD.MINUS_SCORE
  is '不符合怀况时单次扣分';
comment on column ZS18.HT_QLT_SENSOR_STDD.IS_DEL
  is '是否删除';
comment on column ZS18.HT_QLT_SENSOR_STDD.REMARK
  is '备注';
comment on column ZS18.HT_QLT_SENSOR_STDD.CREATE_ID
  is '创建人id';
comment on column ZS18.HT_QLT_SENSOR_STDD.CREATE_TIME
  is '创建时间';
alter table ZS18.HT_QLT_SENSOR_STDD
  add constraint PK_SENSOR_STD primary key (INSPECT_CODE)
  using index 
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

prompt
prompt Creating view HV_QLT_PHYCHEM_MONTH_DETAIL
prompt =========================================
prompt
create or replace view zs18.hv_qlt_phychem_month_detail as
select g.prod_code,g.inspect_code,g.month,g.insvalue,case when g.insvalue between k.lower_value and k.upper_value then 0  when g.insvalue is null then 0 when k.minus_score is null then 0 else k.minus_score end  as score,mintime,maxtime from
(select r.prod_code,r.inspect_code,substr(r.record_time,1,7) as month ,avg(r.inspect_value)as insvalue,min(r.record_time) as mintime,max(r.record_time) as maxtime from ht_qlt_inspect_record r
 group by r.prod_code,r.inspect_code,substr(r.record_time,1,7))  g
 left join ht_pub_prod_design j on j.prod_code = g.prod_code
left join ht_qlt_inspect_stdd_sub k on k.stdd_code = j.inspect_stdd;

prompt
prompt Creating view HV_QLT_PHYCHEM_MONTH_DETAILT
prompt ==========================================
prompt
create or replace view zs18.hv_qlt_phychem_month_detailt as
select g.prod_code,g.inspect_code,g.team_id,g.month,g.insvalue,case when g.insvalue between k.lower_value and k.upper_value then 0  when g.insvalue is null then 0 when k.minus_score is null then 0 else k.minus_score end  as score,mintime,maxtime  from
(select r.prod_code,r.inspect_code,r.team_id,substr(r.record_time,1,7) as month ,avg(r.inspect_value)as insvalue,min(r.record_time) as mintime,max(r.record_time) as maxtime from ht_qlt_inspect_record r
 group by r.prod_code,r.inspect_code,r.team_id ,substr(r.record_time,1,7))  g
 left join ht_pub_prod_design j on j.prod_code = g.prod_code
left join ht_qlt_inspect_stdd_sub k on k.stdd_code = j.inspect_stdd;

prompt
prompt Creating procedure CREATE_PHYCHEM_REPORT
prompt ========================================
prompt
create or replace procedure zs18.Create_phychem_Report
  Authid Current_User
 is
  SQL_COMMOND VARCHAR2(8000);
  SQL_SCORE VARCHAR2(5000);
  --定义游标
  CURSOR CUR IS
select distinct t.inspect_code,t.inspect_name from ht_qlt_inspect_proj t  where t.inspect_group in('1','2','3')  and t.is_del = '0' order by inspect_code;
BEGIN
  --定义查询开头
  SQL_COMMOND := 'select t.month, t.prod_code ,min(mintime)||''~''||max(maxtime) as 生产时间';
 SQL_SCORE := ',100';
 FOR I IN CUR LOOP
    --将结果相连接
    SQL_COMMOND := SQL_COMMOND || ',max(round(decode(t.inspect_code,''' || I.inspect_code ||
                   ''',t.insvalue),2)) "'||I.inspect_name||'"' ;
    SQL_SCORE := SQL_SCORE||'-nvl(max(round(decode(t.inspect_code,''' || I.inspect_code ||''',t.score),2)) ,0) ';
  END LOOP;
  SQL_COMMOND := SQL_COMMOND || SQL_SCORE ||' as 得分 ';
  SQL_COMMOND := SQL_COMMOND || ' from hv_qlt_phychem_month_detail t  left join ht_qlt_inspect_proj s on s.inspect_code = t.inspect_code where s.inspect_group in (''1'',''2'',''3'')  group by t.prod_code,t.month';
  SQL_COMMOND := 'create or replace view hv_qlt_phychem_month_report as '|| SQL_COMMOND;
  EXECUTE IMMEDIATE SQL_COMMOND;
  ----------------------------------------------------------------------------
   SQL_COMMOND := 'select t.month, t.prod_code ,t.team_id,min(mintime)||''~''||max(maxtime) as 生产时间';
 SQL_SCORE := ',100';
 FOR I IN CUR LOOP
    --将结果相连接
    SQL_COMMOND := SQL_COMMOND || ',max(round(decode(t.inspect_code,''' || I.inspect_code ||
                   ''',t.insvalue),2)) "'||I.inspect_name||'"' ;
    SQL_SCORE := SQL_SCORE||'- nvl(max(round(decode(t.inspect_code,''' || I.inspect_code ||''',t.score),2)),0)';
  END LOOP;
  SQL_COMMOND := SQL_COMMOND || SQL_SCORE ||' as 得分 ';
  SQL_COMMOND := SQL_COMMOND || ' from hv_qlt_phychem_month_detailT t   left join ht_qlt_inspect_proj s on s.inspect_code = t.inspect_code where s.inspect_group in (''1'',''2'',''3'')  group by t.prod_code,t.month,t.team_id';
  SQL_COMMOND := 'create or replace view hv_qlt_phychem_month_Trep as '|| SQL_COMMOND;
  EXECUTE IMMEDIATE SQL_COMMOND;

  ---------------------------------------------------------------------------------------------------------
  SQL_COMMOND := 'select t.prod_code ,t.team_id,t.Record_time';
  FOR I IN CUR LOOP
    --将结果相连接
    SQL_COMMOND := SQL_COMMOND || ',round(Avg(decode(t.inspect_code,''' || I.inspect_code ||
                   ''',t.inspect_value))) "'||I.inspect_name||'"' ;
  END LOOP;
  SQL_COMMOND := SQL_COMMOND ||', 100-sum(case  when t.inspect_value >= nvl(r.lower_value,0) and t.inspect_value <=nvl(r.upper_value,0) then 0 else  nvl(r.minus_score,0) end ) as 得分 ';
  SQL_COMMOND := SQL_COMMOND || ' from ht_qlt_inspect_record t left join  ht_pub_prod_design j on j.prod_code = t.prod_code left join ht_qlt_inspect_stdd_sub r on r.inspect_code = t.inspect_code and r.stdd_code = j.inspect_stdd  left join ht_qlt_inspect_proj s on s.inspect_code = t.inspect_code where s.inspect_group in (''1'',''2'',''3'') group by t.prod_code,t.team_id,t.Record_time';
  SQL_COMMOND := 'create or replace view hv_qlt_phychem_daily_report as '|| SQL_COMMOND;
  DBMS_OUTPUT.PUT_LINE(SQL_COMMOND);
  EXECUTE IMMEDIATE SQL_COMMOND;
END;
/

prompt
prompt Creating procedure CREATE_PROCESS_REPORT
prompt ========================================
prompt
create or replace procedure zs18.Create_process_Report
  Authid Current_User
 is
  SQL_COMMOND VARCHAR2(8000);
  --定义游标
  CURSOR CUR IS
select distinct t.inspect_code,t.inspect_name from ht_qlt_inspect_proj t where t.is_valid = '1' and t.is_del = '0' and t.inspect_type = '0';
BEGIN
 -----------------------------------------------------
  SQL_COMMOND := 'select t.prod_code ,t.team_id,t.Record_time';
  FOR I IN CUR LOOP
    --将结果相连接
    SQL_COMMOND := SQL_COMMOND || ',round(Avg(decode(t.inspect_code,''' || I.inspect_code ||
                   ''',t.inspect_value))) "'||I.inspect_name||'"' ;
  END LOOP;
  SQL_COMMOND := SQL_COMMOND ||', 100-sum(case  when t.inspect_value >= nvl(r.lower_value,0) and t.inspect_value <=nvl(r.upper_value,0) then 0 else  nvl(r.minus_score,0) end ) as 得分 ';
  SQL_COMMOND := SQL_COMMOND || ' from ht_qlt_inspect_record t left join ht_pub_prod_design j on j.prod_code = t.prod_code left join ht_qlt_inspect_stdd_sub r on r.inspect_code = t.inspect_code and r.stdd_code = j.inspect_stdd left join ht_qlt_inspect_proj s on s.inspect_code = t.inspect_code where s.INSPECT_TYPE = ''0'' and s.is_Del = ''0'' group by t.prod_code,t.team_id,t.Record_time';
  SQL_COMMOND := 'create or replace view hv_qlt_process_daily_report as '|| SQL_COMMOND;
  DBMS_OUTPUT.PUT_LINE(SQL_COMMOND);
  EXECUTE IMMEDIATE SQL_COMMOND;
  -----------------------------------------------
  SQL_COMMOND := 'select substr(t.record_time,1,7) as month, t.prod_code ,min(t.Record_time)||''~''||max(t.record_time) as 生产时间';
  FOR I IN CUR LOOP
    --将结果相连接
    SQL_COMMOND := SQL_COMMOND || ',round(Avg(decode(t.inspect_code,''' || I.inspect_code ||
                   ''',t.inspect_value)),2) "'||I.inspect_name||'"' ;
  END LOOP;
  SQL_COMMOND := SQL_COMMOND ||', round(avg(r.得分),2) as 得分';
  SQL_COMMOND := SQL_COMMOND || ' from ht_qlt_inspect_record t  left join ht_qlt_inspect_proj s on s.inspect_code = t.inspect_code ';
  SQL_COMMOND := SQL_COMMOND || ' left join  hv_qlt_process_daily_report r on r.prod_code = t.prod_code and substr(r.Record_time ,1,10) = substr(t.record_time,1,10) ';
  SQL_COMMOND := SQL_COMMOND || '  where s.INSPECT_TYPE = ''0'' and s.is_Del = ''0'' group by t.prod_code,substr(t.record_time,1,7)';
  SQL_COMMOND := 'create or replace view hv_qlt_process_month_report as '|| SQL_COMMOND;
  EXECUTE IMMEDIATE SQL_COMMOND;
  --------------------------------------------------------
   SQL_COMMOND := 'select substr(t.record_time,1,7) as month, t.prod_code ,t.team_id';
 FOR I IN CUR LOOP
    --将结果相连接
    SQL_COMMOND := SQL_COMMOND || ',round(Avg(decode(t.inspect_code,''' || I.inspect_code ||
                   ''',t.inspect_value)),2) "'||I.inspect_name||'"' ;
  END LOOP;
  SQL_COMMOND := SQL_COMMOND ||', round(avg(r.得分),2) as 得分';
  SQL_COMMOND := SQL_COMMOND || ' from ht_qlt_inspect_record t  left join ht_qlt_inspect_proj s on s.inspect_code = t.inspect_code ';
  SQL_COMMOND := SQL_COMMOND || ' left join  hv_qlt_process_daily_report r on r.prod_code = t.prod_code and substr(r.Record_time ,1,10) = substr(t.record_time,1,10) and r.team_id = t.team_id ';
  SQL_COMMOND := SQL_COMMOND || '  where s.INSPECT_TYPE = ''0'' and s.is_Del = ''0'' group by t.prod_code,substr(t.record_time,1,7),t.team_id';
   SQL_COMMOND := 'create or replace view hv_qlt_process_month_TRep as '|| SQL_COMMOND;
  EXECUTE IMMEDIATE SQL_COMMOND;
END;
/

prompt
prompt Creating procedure CREATE_SENSOR_MONREPORT
prompt ==========================================
prompt
create or replace procedure zs18.Create_Sensor_monReport
(month in ht_qlt_sensor_record.sensor_month%TYPE)
  Authid Current_User
 is
  SQL_COMMOND VARCHAR2(3000);
  --定义游标
  CURSOR CUR IS
select distinct t.inspect_code,t.inspect_name||'('||r.minus_score||')' as inspect_name from ht_qlt_inspect_proj t left join ht_qlt_sensor_stdd r on r.inspect_code = t.inspect_code left join ht_qlt_sensor_record_sub s on s.inspect_code = t.inspect_code where t.inspect_group = '4'  and t.is_del = '0' order by inspect_code;
BEGIN
  --定义查询开头
  SQL_COMMOND := 'select t.产品名称||''(''||r.平均分||'')'' as 产品得分,t.产品基础分';
  FOR I IN CUR LOOP
    --将结果相连接
    SQL_COMMOND := SQL_COMMOND || ',"'||I.inspect_name||'"' ;
  END LOOP;
  SQL_COMMOND := SQL_COMMOND || ',t.总得分,t.实际得分 from hv_qlt_sensor_RealRec t left join (select g.产品名称,g.sensor_month,round(avg(g.实际得分),2) as 平均分 from hv_qlt_sensor_realRec g group by g.产品名称,g.sensor_month） r on t.产品名称 = r.产品名称 and t.sensor_month = r.sensor_month  where t.sensor_month = '''|| month||''' order by t.产品 ';
  SQL_COMMOND := 'create or replace view hv_qlt_sensor_MonRep as '|| SQL_COMMOND;
  DBMS_OUTPUT.PUT_LINE(SQL_COMMOND);
  
  EXECUTE IMMEDIATE SQL_COMMOND;
END;
/

prompt
prompt Creating procedure CREATE_SENSOR_PRODREPORT
prompt ===========================================
prompt
create or replace procedure zs18.Create_Sensor_ProdReport
(month in ht_qlt_sensor_record.sensor_month%TYPE,
prod_code in ht_pub_prod_design.prod_code%TYPE)
  Authid Current_User
 is
  SQL_COMMOND VARCHAR2(3000);
  --定义游标
  CURSOR CUR IS
select distinct t.inspect_code,t.inspect_name||'('||r.minus_score||')' as inspect_name from ht_qlt_inspect_proj t left join ht_qlt_sensor_stdd r on r.inspect_code = t.inspect_code left join ht_qlt_sensor_record_sub s on s.inspect_code = t.inspect_code where t.inspect_group = '4'  and t.is_del = '0' order by inspect_code;
BEGIN
  --定义查询开头
  SQL_COMMOND := 'select ';
  FOR I IN CUR LOOP
    --将结果相连接
    SQL_COMMOND := SQL_COMMOND || '"'||I.inspect_name||'",' ;
  END LOOP;
  SQL_COMMOND := SQL_COMMOND || 't.总得分,t.实际得分 from hv_qlt_sensor_RealRec t ';
  SQL_COMMOND := SQL_COMMOND ||'  where t.sensor_month = '''|| month||''' and t.产品 ='''|| prod_code ||'''';
  SQL_COMMOND := 'create or replace view hv_qlt_sensor_ProdRep as '|| SQL_COMMOND;
  EXECUTE IMMEDIATE SQL_COMMOND;
END;
/

prompt
prompt Creating procedure CREATE_SENSOR_REPORT
prompt =======================================
prompt
create or replace procedure zs18.Create_Sensor_Report
  Authid Current_User
 is
  SQL_COMMOND VARCHAR2(3000);
  --定义游标
  CURSOR CUR IS
select distinct t.inspect_code,t.inspect_name||'('||r.minus_score||')' as inspect_name from ht_qlt_inspect_proj t left join ht_qlt_sensor_stdd r on r.inspect_code = t.inspect_code left join ht_qlt_sensor_record_sub s on s.inspect_code = t.inspect_code where t.inspect_group = '4'  and t.is_del = '0' order by inspect_code;
BEGIN
  --定义查询开头
  SQL_COMMOND := 'select t.prod_code as 产品,t.main_id,t.Record_time';
  FOR I IN CUR LOOP
    --将结果相连接
    SQL_COMMOND := SQL_COMMOND || ',Max(decode(t.inspect_code,''' || I.inspect_code ||
                   ''',t.inspect_score)) "'||I.inspect_name||'"' ;
  END LOOP;
  SQL_COMMOND := SQL_COMMOND || ',SUM(nvl(t.inspect_score,0)) as 总得分 ';
  SQL_COMMOND := SQL_COMMOND || ' from ht_qlt_sensor_record_sub t  group by t.prod_code,t.main_id,t.Record_time';
  SQL_COMMOND := 'create or replace view hv_qlt_sensor_report as '|| SQL_COMMOND;
  DBMS_OUTPUT.PUT_LINE(SQL_COMMOND);
  EXECUTE IMMEDIATE SQL_COMMOND;

  SQL_COMMOND := 'create or replace view hv_qlt_sensor_realrec as select r.sensor_month,s.prod_name as 产品名称 ,s.sensor_score as 产品基础分,t.*,round(100-abs(t.总得分 - s.sensor_score)/s.sensor_score * 100,2) as 实际得分 from hv_qlt_sensor_report t left join ht_qlt_sensor_record r on r.id = t.main_id left join ht_pub_prod_design s on s.prod_code = t.产品  order by t.产品,r.sensor_month';
  EXECUTE IMMEDIATE SQL_COMMOND;

END;
/


spool off
