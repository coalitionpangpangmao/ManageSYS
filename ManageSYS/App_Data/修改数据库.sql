create or replace view hv_prod_report as
select section_code,planno,starttime,endtime,prod_code,'TECH_PARA1' as seg,tech_para1 as seg_value from ht_prod_report where tech_para1 is not null
union select section_code,planno,starttime,endtime,prod_code,'TECH_PARA2' as seg,tech_para2 as seg_value from ht_prod_report where tech_para2 is not null
union select section_code,planno,starttime,endtime,prod_code,'TECH_PARA3' as seg,tech_para3 as seg_value from ht_prod_report where tech_para3 is not null
union select section_code,planno,starttime,endtime,prod_code,'TECH_PARA4' as seg,tech_para4  as seg_value from ht_prod_report where tech_para4 is not null
union select section_code,planno,starttime,endtime,prod_code,'TECH_PARA5' as seg,tech_para5 as seg_value from ht_prod_report where tech_para5 is not null
union select section_code,planno,starttime,endtime,prod_code,'TECH_PARA6' as seg,tech_para6 as seg_value from ht_prod_report where tech_para6 is not null
union select section_code,planno,starttime,endtime,prod_code,'TECH_PARA7' as seg,tech_para7 as seg_value from ht_prod_report where tech_para7 is not null
union select section_code,planno,starttime,endtime,prod_code,'TECH_PARA8' as seg,tech_para8 as seg_value from ht_prod_report where tech_para8 is not null
union select section_code,planno,starttime,endtime,prod_code,'TECH_PARA9' as seg,tech_para9 as seg_value from ht_prod_report where tech_para9 is not null
union select section_code,planno,starttime,endtime,prod_code,'TECH_PARA10' as seg,tech_para10 as seg_value from ht_prod_report where tech_para10 is not null
union select section_code,planno,starttime,endtime,prod_code,'TECH_PARA11' as seg,tech_para11 as seg_value from ht_prod_report where tech_para11 is not null;


-- Create table
create table HT_PROD_REPORT_DETAIL
(
  SECTION_CODE VARCHAR2(50) not null,
  PLANNO       VARCHAR2(50) not null,
  TIME         VARCHAR2(19) not null,
  PARA_CODE    VARCHAR2(20) not null,
  VALUE        FLOAT,
  TEAM         VARCHAR2(10) not null,
  PROD_CODE    VARCHAR2(10),
  CREATOR      VARCHAR2(20),
  IS_DEL       VARCHAR2(1) default 0
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
-- Add comments to the columns 
comment on column HT_PROD_REPORT_DETAIL.SECTION_CODE
  is '工艺段/工序段编码';
comment on column HT_PROD_REPORT_DETAIL.PLANNO
  is '计划号';
comment on column HT_PROD_REPORT_DETAIL.TIME
  is '开始时间';
comment on column HT_PROD_REPORT_DETAIL.PARA_CODE
  is '产品编码';
comment on column HT_PROD_REPORT_DETAIL.VALUE
  is '参数值';
comment on column HT_PROD_REPORT_DETAIL.TEAM
  is '班组';
comment on column HT_PROD_REPORT_DETAIL.PROD_CODE
  is '产品';
-- Create/Recreate primary, unique and foreign key constraints 
alter table HT_PROD_REPORT_DETAIL
  add constraint PK_REPORT_DETAIL primary key (SECTION_CODE, PLANNO, PARA_CODE, TIME, TEAM)
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
alter table HT_PROD_REPORT_DETAIL
  add constraint FK_REPORT_DETAIL foreign key (PLANNO)
  references HT_PROD_MONTH_PLAN_DETAIL (PLAN_NO) on delete cascade;
