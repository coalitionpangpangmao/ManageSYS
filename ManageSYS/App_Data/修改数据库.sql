alter table HT_QLT_DATA_RECORD
  add constraint UK_AUTO_QLT_DATA unique (PLAN_ID, PARA_CODE, B_TIME, E_TIME, TEAM)
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


-- Create table
create table HT_QLT_COLLECTION
(
  PARA_CODE        VARCHAR2(11) not null,
  PARA_TYPE        VARCHAR2(10),
  WEIGHT           FLOAT,
  PERIODIC         VARCHAR2(3),
  RST_VALUE        FLOAT,
  HEAD_DELAY       FLOAT default 0,
  TAIL_DELAY       FLOAT default 0,
  BATCH_HEAD_DELAY FLOAT default 0,
  BATCH_TAIL_DELAY FLOAT default 0,
  IS_DEL           VARCHAR2(1) default 0,
  IS_VALID         VARCHAR2(1) default 1,
  DESCRIPT         VARCHAR2(128),
  IS_GAP_JUDGE     VARCHAR2(1) default 0,
  SYNCHRO_TIME     CHAR(19),
  GAP_HDELAY       INTEGER default 0,
  GAP_TDELAY       INTEGER default 0,
  CTRL_POINT       VARCHAR2(11),
  GAP_TIME         INTEGER default 30
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
comment on column HT_QLT_COLLECTION.PARA_CODE
  is '��������';
comment on column HT_QLT_COLLECTION.PARA_TYPE
  is '����ɼ������ͣ����ԶԱȷ���';
comment on column HT_QLT_COLLECTION.WEIGHT
  is 'Ȩ��';
comment on column HT_QLT_COLLECTION.PERIODIC
  is '�ɼ�����';
comment on column HT_QLT_COLLECTION.RST_VALUE
  is '�ж�ֵ';
comment on column HT_QLT_COLLECTION.HEAD_DELAY
  is '��ͷ��ʱ';
comment on column HT_QLT_COLLECTION.TAIL_DELAY
  is '��β��ʱ';
comment on column HT_QLT_COLLECTION.BATCH_HEAD_DELAY
  is '��ͷ��ʱ';
comment on column HT_QLT_COLLECTION.BATCH_TAIL_DELAY
  is '��β��ʱ';
comment on column HT_QLT_COLLECTION.DESCRIPT
  is '����';
comment on column HT_QLT_COLLECTION.IS_GAP_JUDGE
  is '�Ƿ��Ƕ����ж���';
comment on column HT_QLT_COLLECTION.SYNCHRO_TIME
  is 'ga';
comment on column HT_QLT_COLLECTION.GAP_HDELAY
  is '����ǰƫ��';
comment on column HT_QLT_COLLECTION.GAP_TDELAY
  is '������ƫ��';
comment on column HT_QLT_COLLECTION.CTRL_POINT
  is '�����ж���';
comment on column HT_QLT_COLLECTION.GAP_TIME
  is '�����ж�ʱ��';
-- Create/Recreate primary, unique and foreign key constraints 
alter table HT_QLT_COLLECTION
  add constraint FK_QLT_COL_ID foreign key (PARA_CODE)
  references HT_PUB_TECH_PARA (PARA_CODE) on delete cascade;
  
  
  -- Create table
create table HT_QLT_GAP_COLLECTION
(
  PLANNO       VARCHAR2(32) not null,
  STARTTIME    VARCHAR2(19),
  ENDTIME      VARCHAR2(19),
  SECTION_CODE VARCHAR2(5),
  GAPTIME      INTEGER,
  GAP_BTIME    VARCHAR2(19),
  GAP_ETIME    VARCHAR2(19),
  ORDERNO      INTEGER,
  TYPE         VARCHAR2(1) default 0,
  BATCH_BTIME  VARCHAR2(19),
  BATCH_ETIME  VARCHAR2(19)
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
comment on table HT_QLT_GAP_COLLECTION
  is '�����ж�����';
-- Add comments to the columns 
comment on column HT_QLT_GAP_COLLECTION.PLANNO
  is '�ƻ���';
comment on column HT_QLT_GAP_COLLECTION.STARTTIME
  is 'ͳ�ƿ�ʼʱ��';
comment on column HT_QLT_GAP_COLLECTION.ENDTIME
  is 'ͳ�ƽ���ʱ��';
comment on column HT_QLT_GAP_COLLECTION.SECTION_CODE
  is '���նα���';
comment on column HT_QLT_GAP_COLLECTION.GAPTIME
  is '����ʱ��';
comment on column HT_QLT_GAP_COLLECTION.GAP_BTIME
  is '������ʼʱ��';
comment on column HT_QLT_GAP_COLLECTION.GAP_ETIME
  is '��������ʱ��';
comment on column HT_QLT_GAP_COLLECTION.TYPE
  is '0 ��¼���ն���ͷ��β����ͷ��β��Ϣ  1 ��¼������Ϣ';
-- Create/Recreate primary, unique and foreign key constraints 
alter table HT_QLT_GAP_COLLECTION
  add constraint FK_QLT_GAP_ID foreign key (PLANNO)
  references HT_PUB_TECH_PARA (PARA_CODE) on delete cascade;
