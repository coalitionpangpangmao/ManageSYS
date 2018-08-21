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
  is '参数编码';
comment on column HT_QLT_COLLECTION.PARA_TYPE
  is '数点采集点类型，用以对比分析';
comment on column HT_QLT_COLLECTION.WEIGHT
  is '权重';
comment on column HT_QLT_COLLECTION.PERIODIC
  is '采集周期';
comment on column HT_QLT_COLLECTION.RST_VALUE
  is '判定值';
comment on column HT_QLT_COLLECTION.HEAD_DELAY
  is '料头延时';
comment on column HT_QLT_COLLECTION.TAIL_DELAY
  is '料尾延时';
comment on column HT_QLT_COLLECTION.BATCH_HEAD_DELAY
  is '批头延时';
comment on column HT_QLT_COLLECTION.BATCH_TAIL_DELAY
  is '批尾延时';
comment on column HT_QLT_COLLECTION.DESCRIPT
  is '描述';
comment on column HT_QLT_COLLECTION.IS_GAP_JUDGE
  is '是否是断流判定点';
comment on column HT_QLT_COLLECTION.SYNCHRO_TIME
  is 'ga';
comment on column HT_QLT_COLLECTION.GAP_HDELAY
  is '断流前偏移';
comment on column HT_QLT_COLLECTION.GAP_TDELAY
  is '断流后偏移';
comment on column HT_QLT_COLLECTION.CTRL_POINT
  is '断流判定点';
comment on column HT_QLT_COLLECTION.GAP_TIME
  is '断流判定时长';
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
  is '断流判定条件';
-- Add comments to the columns 
comment on column HT_QLT_GAP_COLLECTION.PLANNO
  is '计划号';
comment on column HT_QLT_GAP_COLLECTION.STARTTIME
  is '统计开始时间';
comment on column HT_QLT_GAP_COLLECTION.ENDTIME
  is '统计结束时间';
comment on column HT_QLT_GAP_COLLECTION.SECTION_CODE
  is '工艺段编码';
comment on column HT_QLT_GAP_COLLECTION.GAPTIME
  is '断流时长';
comment on column HT_QLT_GAP_COLLECTION.GAP_BTIME
  is '断流开始时间';
comment on column HT_QLT_GAP_COLLECTION.GAP_ETIME
  is '断流结束时间';
comment on column HT_QLT_GAP_COLLECTION.TYPE
  is '0 记录工艺段批头批尾、料头料尾信息  1 记录断流信息';
-- Create/Recreate primary, unique and foreign key constraints 
alter table HT_QLT_GAP_COLLECTION
  add constraint FK_QLT_GAP_ID foreign key (PLANNO)
  references HT_PUB_TECH_PARA (PARA_CODE) on delete cascade;
