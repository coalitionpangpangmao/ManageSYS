--------------------------------------------
-- Export file for user ZS18              --
-- Created by wicky on 2018/6/30, 9:16:18 --
--------------------------------------------

spool db.log

prompt
prompt Creating table G_SECTION_RATE
prompt =============================
prompt
create table ZS18.G_SECTION_RATE
(
  F_BATCH          VARCHAR2(20),
  F_SECTION        VARCHAR2(20),
  F_SECTION_WEIGHT FLOAT,
  F_SECTION_SCORE  FLOAT,
  F_SYNCHRO_TIME   VARCHAR2(19),
  F_IS_GAP         VARCHAR2(6),
  F_SHIFT          VARCHAR2(4),
  F_DONE           VARCHAR2(1) default 0,
  F_GAP_TIME       FLOAT default 0
)
tablespace ZS_DATA
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 320K
    next 8K
    minextents 1
  );
comment on table ZS18.G_SECTION_RATE
  is '绩效考核工序得分';

prompt
prompt Creating table HT_EQ_EQP_CLS
prompt ============================
prompt
create table ZS18.HT_EQ_EQP_CLS
(
  ID_KEY     VARCHAR2(64) not null,
  NODE_NAME  VARCHAR2(128),
  NODE_VALUE VARCHAR2(32),
  PARENT_ID  VARCHAR2(64),
  TYPE       VARCHAR2(32),
  PATH       VARCHAR2(128),
  IS_DEL     VARCHAR2(1) default 0
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
comment on column ZS18.HT_EQ_EQP_CLS.ID_KEY
  is '唯一标识';
comment on column ZS18.HT_EQ_EQP_CLS.NODE_NAME
  is '节点名称';
comment on column ZS18.HT_EQ_EQP_CLS.NODE_VALUE
  is '节点值';
comment on column ZS18.HT_EQ_EQP_CLS.PARENT_ID
  is '父节点标识';
comment on column ZS18.HT_EQ_EQP_CLS.TYPE
  is '类型';
comment on column ZS18.HT_EQ_EQP_CLS.PATH
  is '路径';
alter table ZS18.HT_EQ_EQP_CLS
  add constraint ID_KEY primary key (ID_KEY)
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
prompt Creating table HT_EQ_EQP_TBL
prompt ============================
prompt
create table ZS18.HT_EQ_EQP_TBL
(
  IDKEY           VARCHAR2(64) not null,
  CLS_CODE        VARCHAR2(128),
  EQ_NAME         VARCHAR2(128),
  SGS_CODE        VARCHAR2(32),
  NC_CODE         VARCHAR2(32),
  FINANCE_EQ_NAME VARCHAR2(128),
  EQ_TYPE         VARCHAR2(32),
  EQ_STATUS       VARCHAR2(32),
  ZG_DATE         DATE,
  EQ_MODEL        VARCHAR2(256),
  ORI_WORTH       VARCHAR2(50),
  NET_WORTH       VARCHAR2(50),
  USED_DATE       DATE,
  RATED_POWER     VARCHAR2(50),
  REAL_POWER      VARCHAR2(50),
  POWER_UNIT      VARCHAR2(32),
  OWNER_NAME      VARCHAR2(256),
  EQP_FROM        VARCHAR2(128),
  ORI_OWNER_NAME  VARCHAR2(256),
  MANUFACTURER    VARCHAR2(256) default 0,
  SERIAL_NUMBER   VARCHAR2(256),
  SUPPLIER        VARCHAR2(256),
  IS_SPEC_EQP     VARCHAR2(1) default 0,
  IS_MADEINCHINA  VARCHAR2(1) default 0,
  MGT_DEPT_CODE   VARCHAR2(256),
  USE_DEPT_CODE   VARCHAR2(256),
  DUTY_NAME       VARCHAR2(256),
  EQP_IP          VARCHAR2(64),
  EQP_MAC         VARCHAR2(64),
  EQP_SN          VARCHAR2(64),
  EQP_SYS         VARCHAR2(128),
  REMARK          VARCHAR2(256),
  IS_DEL          VARCHAR2(1) default 0,
  CREATOR         VARCHAR2(32),
  CREATE_TIME     VARCHAR2(32),
  FLAG            INTEGER default 0 not null,
  SECTION_CODE    VARCHAR2(32),
  IS_VALID        VARCHAR2(1) default 1
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
comment on column ZS18.HT_EQ_EQP_TBL.IDKEY
  is '唯一标识 一级分类2位+二级分类2位+三级分类2位+四级分类2位+顺序号3位';
comment on column ZS18.HT_EQ_EQP_TBL.CLS_CODE
  is '分类编号';
comment on column ZS18.HT_EQ_EQP_TBL.EQ_NAME
  is '父设备名称';
comment on column ZS18.HT_EQ_EQP_TBL.SGS_CODE
  is '省公司固定资产编码';
comment on column ZS18.HT_EQ_EQP_TBL.NC_CODE
  is 'NC编码';
comment on column ZS18.HT_EQ_EQP_TBL.FINANCE_EQ_NAME
  is '财务固定资产名称';
comment on column ZS18.HT_EQ_EQP_TBL.EQ_TYPE
  is '企业设备分类';
comment on column ZS18.HT_EQ_EQP_TBL.EQ_STATUS
  is '设备状态';
comment on column ZS18.HT_EQ_EQP_TBL.ZG_DATE
  is '转固日期';
comment on column ZS18.HT_EQ_EQP_TBL.EQ_MODEL
  is '设备型号';
comment on column ZS18.HT_EQ_EQP_TBL.ORI_WORTH
  is '设备资产原值';
comment on column ZS18.HT_EQ_EQP_TBL.NET_WORTH
  is '设备资产净值';
comment on column ZS18.HT_EQ_EQP_TBL.USED_DATE
  is '投入使用日期';
comment on column ZS18.HT_EQ_EQP_TBL.RATED_POWER
  is '额定生产能力';
comment on column ZS18.HT_EQ_EQP_TBL.REAL_POWER
  is '实际生产能力';
comment on column ZS18.HT_EQ_EQP_TBL.POWER_UNIT
  is '能力单位';
comment on column ZS18.HT_EQ_EQP_TBL.OWNER_NAME
  is '所属企业名称';
comment on column ZS18.HT_EQ_EQP_TBL.EQP_FROM
  is '设备来源';
comment on column ZS18.HT_EQ_EQP_TBL.ORI_OWNER_NAME
  is '原所属企业名称';
comment on column ZS18.HT_EQ_EQP_TBL.MANUFACTURER
  is '制造商';
comment on column ZS18.HT_EQ_EQP_TBL.SERIAL_NUMBER
  is '出厂编号';
comment on column ZS18.HT_EQ_EQP_TBL.SUPPLIER
  is '供应商';
comment on column ZS18.HT_EQ_EQP_TBL.IS_SPEC_EQP
  is '是否特种设备';
comment on column ZS18.HT_EQ_EQP_TBL.IS_MADEINCHINA
  is '是否国产';
comment on column ZS18.HT_EQ_EQP_TBL.MGT_DEPT_CODE
  is '管理部门编码';
comment on column ZS18.HT_EQ_EQP_TBL.USE_DEPT_CODE
  is '使用部门编码';
comment on column ZS18.HT_EQ_EQP_TBL.DUTY_NAME
  is '责任人';
comment on column ZS18.HT_EQ_EQP_TBL.EQP_IP
  is 'IP地址';
comment on column ZS18.HT_EQ_EQP_TBL.EQP_MAC
  is 'MAC地址';
comment on column ZS18.HT_EQ_EQP_TBL.EQP_SN
  is '设备SN';
comment on column ZS18.HT_EQ_EQP_TBL.EQP_SYS
  is '操作系统';
comment on column ZS18.HT_EQ_EQP_TBL.REMARK
  is '备注';
comment on column ZS18.HT_EQ_EQP_TBL.IS_DEL
  is '删除标识';
comment on column ZS18.HT_EQ_EQP_TBL.CREATOR
  is '创建人';
comment on column ZS18.HT_EQ_EQP_TBL.CREATE_TIME
  is '创建时间';
comment on column ZS18.HT_EQ_EQP_TBL.FLAG
  is '顺序唯一标识';
comment on column ZS18.HT_EQ_EQP_TBL.SECTION_CODE
  is '所属工艺编码';
alter table ZS18.HT_EQ_EQP_TBL
  add constraint IDKEY primary key (IDKEY)
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
prompt Creating table HT_EQ_FAULT_DB
prompt =============================
prompt
create table ZS18.HT_EQ_FAULT_DB
(
  ID                INTEGER not null,
  EQP_TYPE          VARCHAR2(128),
  SPECIFIC_LOCATION VARCHAR2(128),
  SECTION_CODE      VARCHAR2(32),
  ERROR_DESCRIPTION VARCHAR2(512),
  FAILURE_CAUSE     VARCHAR2(512),
  SOLUTION          VARCHAR2(512),
  FAULT_TYPE1       VARCHAR2(2),
  FAULT_TYPE2       VARCHAR2(2),
  FAULT_TYPE3       VARCHAR2(2),
  FAULT_TYPE4       VARCHAR2(2),
  FAULT_TYPE5       VARCHAR2(2),
  FAULT_TYPE6       VARCHAR2(2),
  IS_DEL            VARCHAR2(1) default 0,
  EDITOR_ID         VARCHAR2(32),
  ERROR_NAME        VARCHAR2(100),
  SCEAN             VARCHAR2(512)
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
comment on column ZS18.HT_EQ_FAULT_DB.ID
  is '故障ID';
comment on column ZS18.HT_EQ_FAULT_DB.EQP_TYPE
  is '故障类型 0 电气1 机械';
comment on column ZS18.HT_EQ_FAULT_DB.SPECIFIC_LOCATION
  is '故障具体位置';
comment on column ZS18.HT_EQ_FAULT_DB.SECTION_CODE
  is '设备所属工段';
comment on column ZS18.HT_EQ_FAULT_DB.ERROR_DESCRIPTION
  is '设备故障描述';
comment on column ZS18.HT_EQ_FAULT_DB.FAILURE_CAUSE
  is '设备故障原因';
comment on column ZS18.HT_EQ_FAULT_DB.SOLUTION
  is '设备故障解决方案';
comment on column ZS18.HT_EQ_FAULT_DB.FAULT_TYPE1
  is '1渐发性故障2突发性故障';
comment on column ZS18.HT_EQ_FAULT_DB.FAULT_TYPE2
  is '1间断性故障2永久性故障';
comment on column ZS18.HT_EQ_FAULT_DB.FAULT_TYPE3
  is '1完全性故障2局部性故障';
comment on column ZS18.HT_EQ_FAULT_DB.FAULT_TYPE4
  is '1磨损性故障2错用性故障3固有的薄弱性故障';
comment on column ZS18.HT_EQ_FAULT_DB.FAULT_TYPE5
  is '1危险性故障2安全性故障';
comment on column ZS18.HT_EQ_FAULT_DB.FAULT_TYPE6
  is '1随机故障2有规律故障';
comment on column ZS18.HT_EQ_FAULT_DB.IS_DEL
  is '删除标识';
comment on column ZS18.HT_EQ_FAULT_DB.EDITOR_ID
  is '编制人';
comment on column ZS18.HT_EQ_FAULT_DB.ERROR_NAME
  is '故障名';
comment on column ZS18.HT_EQ_FAULT_DB.SCEAN
  is '故障现场';
alter table ZS18.HT_EQ_FAULT_DB
  add constraint PK_T_FAULTDB_ID primary key (ID)
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
prompt Creating table HT_EQ_LB_PLAN
prompt ============================
prompt
create table ZS18.HT_EQ_LB_PLAN
(
  FLOW_STATUS    VARCHAR2(2) default -1,
  CREATE_ID      VARCHAR2(32),
  CREATE_DEPT_ID VARCHAR2(32),
  CREATE_TIME    VARCHAR2(19),
  REMARK         VARCHAR2(512),
  IS_DEL         VARCHAR2(1) default 0,
  PZ_CODE        VARCHAR2(32) not null,
  GOWHERE        VARCHAR2(1),
  EXPIRED_DATE   VARCHAR2(10),
  TASK_STATUS    CHAR(10) default 0,
  IS_MODEL       VARCHAR2(1) default 0,
  MT_NAME        VARCHAR2(50)
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
comment on table ZS18.HT_EQ_LB_PLAN
  is '润滑主表';
comment on column ZS18.HT_EQ_LB_PLAN.FLOW_STATUS
  is '审批状态 -1 未提交审批 0办理中 ２己通过 １未通过';
comment on column ZS18.HT_EQ_LB_PLAN.CREATE_ID
  is '申请人id';
comment on column ZS18.HT_EQ_LB_PLAN.CREATE_DEPT_ID
  is '申请部门id';
comment on column ZS18.HT_EQ_LB_PLAN.CREATE_TIME
  is '申请时间';
comment on column ZS18.HT_EQ_LB_PLAN.REMARK
  is '备注';
comment on column ZS18.HT_EQ_LB_PLAN.IS_DEL
  is '删除标识';
comment on column ZS18.HT_EQ_LB_PLAN.PZ_CODE
  is '凭证号 LB+ YYYYMMDD + 三位流水号';
comment on column ZS18.HT_EQ_LB_PLAN.GOWHERE
  is '流程决策';
comment on column ZS18.HT_EQ_LB_PLAN.EXPIRED_DATE
  is '过期时间';
comment on column ZS18.HT_EQ_LB_PLAN.TASK_STATUS
  is '执行状态 0 未执行  1 执行中  2 己完成 3 己过期';
comment on column ZS18.HT_EQ_LB_PLAN.IS_MODEL
  is '是否是润滑模版';
comment on column ZS18.HT_EQ_LB_PLAN.MT_NAME
  is '维保计划名';
alter table ZS18.HT_EQ_LB_PLAN
  add constraint PK_HT_LB_PLAN primary key (PZ_CODE)
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
prompt Creating table HT_EQ_LB_PLAN_DETAIL
prompt ===================================
prompt
create table ZS18.HT_EQ_LB_PLAN_DETAIL
(
  ID              INTEGER not null,
  MAIN_ID         VARCHAR2(32),
  EQUIPMENT_ID    VARCHAR2(512),
  NTRODUCER       VARCHAR2(128),
  STATUS          VARCHAR2(16) default 0,
  REMARK          VARCHAR2(512),
  CREATE_ID       VARCHAR2(32),
  CREATE_TIME     VARCHAR2(32),
  IS_DEL          VARCHAR2(1) default 0,
  SECTION         VARCHAR2(32),
  RESPONER        VARCHAR2(20),
  EXP_FINISH_TIME VARCHAR2(19),
  PICKUP_DESC     VARCHAR2(512),
  YEAR_MONTH      VARCHAR2(20),
  POSITION        VARCHAR2(128),
  POINTNUM        INTEGER,
  LUBOIL          VARCHAR2(1) default 0,
  EXE_TIME        VARCHAR2(19),
  PERIODIC        VARCHAR2(50),
  STYLE           VARCHAR2(128),
  AMOUNT          FLOAT,
  EXE_SEGTIME     INTEGER,
  VERIFIOR        VARCHAR2(20)
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
comment on table ZS18.HT_EQ_LB_PLAN_DETAIL
  is '停产维修计划审批从表';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.ID
  is '主键标识';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.MAIN_ID
  is '主表id';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.EQUIPMENT_ID
  is '设备ID';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.NTRODUCER
  is '提出人';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.STATUS
  is '状态 0未派工1己派工 2己完成';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.REMARK
  is '备注';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.CREATE_ID
  is '创建人id';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.CREATE_TIME
  is '创建时间';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.IS_DEL
  is '是否删除';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.SECTION
  is '工段名称';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.RESPONER
  is '责任人';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.EXP_FINISH_TIME
  is '期望完成时间';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.PICKUP_DESC
  is '备件情况';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.YEAR_MONTH
  is '所属年月';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.POSITION
  is '润滑部位';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.POINTNUM
  is '润滑点数';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.LUBOIL
  is '润滑油';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.EXE_TIME
  is '操作时间';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.PERIODIC
  is '润滑周期';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.STYLE
  is '润滑方式';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.AMOUNT
  is '滑润量';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.EXE_SEGTIME
  is '操作时长';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.VERIFIOR
  is '验证人';
alter table ZS18.HT_EQ_LB_PLAN_DETAIL
  add constraint PK_HT_EQ_LB_PLAN_DETAIL primary key (ID)
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
prompt Creating table HT_EQ_MCLBR_CONTENT
prompt ==================================
prompt
create table ZS18.HT_EQ_MCLBR_CONTENT
(
  MAIN_ID VARCHAR2(32) not null,
  BTIME   VARCHAR2(128),
  ETIME   VARCHAR2(16) default 0,
  AVG     VARCHAR2(512),
  STD     VARCHAR2(32),
  ERR_AVG VARCHAR2(32),
  IS_DEL  VARCHAR2(1) default 0
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
comment on table ZS18.HT_EQ_MCLBR_CONTENT
  is '人工校准计划审批从表内容';
comment on column ZS18.HT_EQ_MCLBR_CONTENT.MAIN_ID
  is '主表id';
comment on column ZS18.HT_EQ_MCLBR_CONTENT.BTIME
  is '取样开始时间';
comment on column ZS18.HT_EQ_MCLBR_CONTENT.ETIME
  is '取样结束时间';
comment on column ZS18.HT_EQ_MCLBR_CONTENT.AVG
  is '均值';
comment on column ZS18.HT_EQ_MCLBR_CONTENT.STD
  is '标准差';
comment on column ZS18.HT_EQ_MCLBR_CONTENT.ERR_AVG
  is '误差均值';
comment on column ZS18.HT_EQ_MCLBR_CONTENT.IS_DEL
  is '是否删除';
alter table ZS18.HT_EQ_MCLBR_CONTENT
  add constraint PK_HT_EQ_CONTENT primary key (MAIN_ID)
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
prompt Creating table HT_EQ_MCLBR_PLAN
prompt ===============================
prompt
create table ZS18.HT_EQ_MCLBR_PLAN
(
  FLOW_STATUS    VARCHAR2(2) default -1,
  CREATE_ID      VARCHAR2(32),
  CREATE_DEPT_ID VARCHAR2(32),
  CREATE_TIME    VARCHAR2(19),
  REMARK         VARCHAR2(512),
  IS_DEL         VARCHAR2(1) default 0,
  PZ_CODE        VARCHAR2(32) not null,
  GOWHERE        VARCHAR2(1),
  EXPIRED_DATE   VARCHAR2(10),
  TASK_STATUS    CHAR(10) default 0,
  IS_MODEL       VARCHAR2(1) default 0,
  MT_NAME        VARCHAR2(50),
  CLBRT_TYPE     VARCHAR2(2)
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
comment on table ZS18.HT_EQ_MCLBR_PLAN
  is '人工校准主表';
comment on column ZS18.HT_EQ_MCLBR_PLAN.FLOW_STATUS
  is '审批状态 -1 未提交审批 0办理中 ２己通过 １未通过';
comment on column ZS18.HT_EQ_MCLBR_PLAN.CREATE_ID
  is '申请人id';
comment on column ZS18.HT_EQ_MCLBR_PLAN.CREATE_DEPT_ID
  is '申请部门id';
comment on column ZS18.HT_EQ_MCLBR_PLAN.CREATE_TIME
  is '申请时间';
comment on column ZS18.HT_EQ_MCLBR_PLAN.REMARK
  is '备注';
comment on column ZS18.HT_EQ_MCLBR_PLAN.IS_DEL
  is '删除标识';
comment on column ZS18.HT_EQ_MCLBR_PLAN.PZ_CODE
  is '凭证号 LB+ YYYYMMDD + 三位流水号';
comment on column ZS18.HT_EQ_MCLBR_PLAN.GOWHERE
  is '流程决策';
comment on column ZS18.HT_EQ_MCLBR_PLAN.EXPIRED_DATE
  is '过期时间';
comment on column ZS18.HT_EQ_MCLBR_PLAN.TASK_STATUS
  is '执行状态 0 未执行  1 执行中  2 己完成 3 己过期';
comment on column ZS18.HT_EQ_MCLBR_PLAN.IS_MODEL
  is '是否是模版';
comment on column ZS18.HT_EQ_MCLBR_PLAN.MT_NAME
  is '计划名';
comment on column ZS18.HT_EQ_MCLBR_PLAN.CLBRT_TYPE
  is '校准方式 0 人工1自动';
alter table ZS18.HT_EQ_MCLBR_PLAN
  add constraint PK_HT_MCLBRT_PLAN primary key (PZ_CODE)
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
prompt Creating table HT_EQ_MCLBR_PLAN_DETAIL
prompt ======================================
prompt
create table ZS18.HT_EQ_MCLBR_PLAN_DETAIL
(
  ID              INTEGER not null,
  MAIN_ID         VARCHAR2(32),
  EQUIPMENT_ID    VARCHAR2(512),
  NTRODUCER       VARCHAR2(128),
  STATUS          VARCHAR2(16) default 0,
  REMARK          VARCHAR2(512),
  CREATE_ID       VARCHAR2(32),
  CREATE_TIME     VARCHAR2(32),
  IS_DEL          VARCHAR2(1) default 0,
  SECTION         VARCHAR2(32),
  RESPONER        VARCHAR2(20),
  EXP_FINISH_TIME VARCHAR2(19),
  POINT           VARCHAR2(32),
  SHIFT           VARCHAR2(4),
  TEAM            VARCHAR2(4),
  SAMPLE_TIME     VARCHAR2(19),
  VERIFIOR        VARCHAR2(20),
  POINTVALUE      FLOAT
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
comment on table ZS18.HT_EQ_MCLBR_PLAN_DETAIL
  is '人工校准计划审批从表';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.ID
  is '主键标识';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.MAIN_ID
  is '主表id';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.EQUIPMENT_ID
  is '设备ID';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.NTRODUCER
  is '提出人';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.STATUS
  is '状态 0未派工1己派工 2己完成';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.REMARK
  is '备注';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.CREATE_ID
  is '创建人id';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.CREATE_TIME
  is '创建时间';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.IS_DEL
  is '是否删除';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.SECTION
  is '工段名称';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.RESPONER
  is '责任人';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.EXP_FINISH_TIME
  is '期望完成时间';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.POINT
  is '取样点';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.SHIFT
  is '取样班次';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.TEAM
  is '取样班组';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.SAMPLE_TIME
  is '取样时间';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.VERIFIOR
  is '验证人';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.POINTVALUE
  is '取样值';
alter table ZS18.HT_EQ_MCLBR_PLAN_DETAIL
  add constraint PK_HT_EQ_MCLBRT_PLAN_DETAIL primary key (ID)
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
prompt Creating table HT_EQ_MT_PLAN
prompt ============================
prompt
create table ZS18.HT_EQ_MT_PLAN
(
  FLOW_STATUS    VARCHAR2(2) default -1,
  CREATE_ID      VARCHAR2(32),
  CREATE_DEPT_ID VARCHAR2(32),
  CREATE_TIME    VARCHAR2(19),
  REMARK         VARCHAR2(512),
  IS_DEL         VARCHAR2(1) default 0,
  PZ_CODE        VARCHAR2(32) not null,
  GOWHERE        VARCHAR2(1),
  EXPIRED_DATE   VARCHAR2(10),
  TASK_STATUS    CHAR(10) default 0,
  IS_MODEL       VARCHAR2(1) default 0,
  MT_NAME        VARCHAR2(50)
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
comment on table ZS18.HT_EQ_MT_PLAN
  is '维保计划审批主表';
comment on column ZS18.HT_EQ_MT_PLAN.FLOW_STATUS
  is '审批状态 -1 未提交审批 0办理中 ２己通过 １未通过';
comment on column ZS18.HT_EQ_MT_PLAN.CREATE_ID
  is '申请人id';
comment on column ZS18.HT_EQ_MT_PLAN.CREATE_DEPT_ID
  is '申请部门id';
comment on column ZS18.HT_EQ_MT_PLAN.CREATE_TIME
  is '申请时间';
comment on column ZS18.HT_EQ_MT_PLAN.REMARK
  is '备注';
comment on column ZS18.HT_EQ_MT_PLAN.IS_DEL
  is '删除标识';
comment on column ZS18.HT_EQ_MT_PLAN.PZ_CODE
  is '凭证号 MT+ YYYYMMDD + 三位流水号';
comment on column ZS18.HT_EQ_MT_PLAN.GOWHERE
  is '流程决策';
comment on column ZS18.HT_EQ_MT_PLAN.EXPIRED_DATE
  is '过期时间';
comment on column ZS18.HT_EQ_MT_PLAN.TASK_STATUS
  is '执行状态 0 未执行  1 执行中  2 己完成 3 己过期';
comment on column ZS18.HT_EQ_MT_PLAN.IS_MODEL
  is '是否是维保模版';
comment on column ZS18.HT_EQ_MT_PLAN.MT_NAME
  is '维保计划名';
alter table ZS18.HT_EQ_MT_PLAN
  add constraint PK_HT_MT_PLAN primary key (PZ_CODE)
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
prompt Creating table HT_EQ_MT_PLAN_DETAIL
prompt ===================================
prompt
create table ZS18.HT_EQ_MT_PLAN_DETAIL
(
  ID              INTEGER not null,
  MAIN_ID         VARCHAR2(32),
  EQUIPMENT_ID    VARCHAR2(512),
  NTRODUCER       VARCHAR2(128),
  STATUS          VARCHAR2(16) default 0,
  REMARK          VARCHAR2(512),
  CREATE_ID       VARCHAR2(32),
  CREATE_TIME     VARCHAR2(32),
  IS_DEL          VARCHAR2(1) default 0,
  MECH_AREA       VARCHAR2(32),
  RESPONER        VARCHAR2(20),
  EXP_FINISH_TIME VARCHAR2(19),
  IS_OUT          VARCHAR2(10),
  PICKUP_DESC     VARCHAR2(512),
  YEAR_MONTH      VARCHAR2(20),
  FAULT_ID        INTEGER,
  REASON          VARCHAR2(128),
  CONTENT         VARCHAR2(128),
  IS_FAULT        VARCHAR2(1) default 0,
  EXE_TIME        VARCHAR2(19),
  RECORD          VARCHAR2(512),
  RESULTS         VARCHAR2(512),
  CONDITION       VARCHAR2(512),
  EXE_SEGTIME     INTEGER,
  VERIFIOR        VARCHAR2(20)
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
comment on table ZS18.HT_EQ_MT_PLAN_DETAIL
  is '停产维修计划审批从表';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.ID
  is '主键标识';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.MAIN_ID
  is '主表id';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.EQUIPMENT_ID
  is '维保设备ID';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.NTRODUCER
  is '提出人';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.STATUS
  is '状态 0未派工1己派工 2己完成';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.REMARK
  is '备注';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.CREATE_ID
  is '创建人id';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.CREATE_TIME
  is '创建时间';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.IS_DEL
  is '是否删除';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.MECH_AREA
  is '区域';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.RESPONER
  is '责任人';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.EXP_FINISH_TIME
  is '期望完成时间';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.IS_OUT
  is '是否委外';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.PICKUP_DESC
  is '备件情况';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.YEAR_MONTH
  is '所属年月';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.FAULT_ID
  is '故障ID';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.REASON
  is '维保原因';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.CONTENT
  is '维保内容';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.IS_FAULT
  is '是否有故障';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.EXE_TIME
  is '操作时间';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.RECORD
  is '操作记录';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.RESULTS
  is '操作结果';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.CONDITION
  is '维保情况';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.EXE_SEGTIME
  is '操作时长';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.VERIFIOR
  is '验证人';
alter table ZS18.HT_EQ_MT_PLAN_DETAIL
  add constraint PK_HT_EQ_MT_PLAN_DETAIL primary key (ID)
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
prompt Creating table HT_EQ_MT_SHIFT
prompt =============================
prompt
create table ZS18.HT_EQ_MT_SHIFT
(
  ID               INTEGER not null,
  WORKSHOP_CODE    VARCHAR2(32),
  SHIFT_CODE       VARCHAR2(32),
  TEAM_CODE        VARCHAR2(32),
  HANDOVER_DATE    VARCHAR2(10),
  B_TIME           VARCHAR2(19),
  E_TIME           VARCHAR2(19),
  CREATE_ID        VARCHAR2(32),
  MODIFY_ID        VARCHAR2(32),
  RECORD_TIME      VARCHAR2(19),
  REMARK           VARCHAR2(256),
  IS_VALID         VARCHAR2(1) default 1,
  IS_DEL           VARCHAR2(1) default 0,
  SHIFT_STATUS     VARCHAR2(16) default 1,
  MAINTENANCE_TYPE VARCHAR2(4) not null
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
comment on column ZS18.HT_EQ_MT_SHIFT.ID
  is '机械维修班交班记录ID';
comment on column ZS18.HT_EQ_MT_SHIFT.WORKSHOP_CODE
  is '车间名称';
comment on column ZS18.HT_EQ_MT_SHIFT.SHIFT_CODE
  is '班时编码';
comment on column ZS18.HT_EQ_MT_SHIFT.TEAM_CODE
  is '班组编码';
comment on column ZS18.HT_EQ_MT_SHIFT.HANDOVER_DATE
  is '交班日期';
comment on column ZS18.HT_EQ_MT_SHIFT.B_TIME
  is '开始时间';
comment on column ZS18.HT_EQ_MT_SHIFT.E_TIME
  is '结束时间';
comment on column ZS18.HT_EQ_MT_SHIFT.CREATE_ID
  is '交班人ID';
comment on column ZS18.HT_EQ_MT_SHIFT.MODIFY_ID
  is '接班人ID';
comment on column ZS18.HT_EQ_MT_SHIFT.RECORD_TIME
  is '记录时间';
comment on column ZS18.HT_EQ_MT_SHIFT.REMARK
  is '备注';
comment on column ZS18.HT_EQ_MT_SHIFT.IS_VALID
  is '是否有效';
comment on column ZS18.HT_EQ_MT_SHIFT.IS_DEL
  is '删除标识';
comment on column ZS18.HT_EQ_MT_SHIFT.SHIFT_STATUS
  is '交接班状态';
comment on column ZS18.HT_EQ_MT_SHIFT.MAINTENANCE_TYPE
  is '维修类型';
alter table ZS18.HT_EQ_MT_SHIFT
  add constraint PK_HT_EQ_ELECTRIC_SHIFT primary key (ID, MAINTENANCE_TYPE)
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
prompt Creating table HT_EQ_MT_SHIFT_DETAIL
prompt ====================================
prompt
create table ZS18.HT_EQ_MT_SHIFT_DETAIL
(
  ID               INTEGER not null,
  SHIFT_MAIN_ID    INTEGER,
  MAINTENANCE_TYPE VARCHAR2(4),
  MANAGE_STATUS    VARCHAR2(32),
  IS_DEL           VARCHAR2(1) default 0,
  BUZ_ID           INTEGER
)
tablespace ZS_DATA
  pctfree 10
  initrans 1
  maxtrans 255;
comment on column ZS18.HT_EQ_MT_SHIFT_DETAIL.ID
  is '维修班交班子表ID';
comment on column ZS18.HT_EQ_MT_SHIFT_DETAIL.SHIFT_MAIN_ID
  is '维修班交班主表ID';
comment on column ZS18.HT_EQ_MT_SHIFT_DETAIL.MAINTENANCE_TYPE
  is '维修类型　0电气1机械';
comment on column ZS18.HT_EQ_MT_SHIFT_DETAIL.MANAGE_STATUS
  is '处理状态';
comment on column ZS18.HT_EQ_MT_SHIFT_DETAIL.IS_DEL
  is '删除标识';
comment on column ZS18.HT_EQ_MT_SHIFT_DETAIL.BUZ_ID
  is '业务ID';
alter table ZS18.HT_EQ_MT_SHIFT_DETAIL
  add constraint PK_T_SCENE_ELECTRIC_SHIFT_DETA primary key (ID)
  using index 
  tablespace ZS_DATA
  pctfree 10
  initrans 2
  maxtrans 255;

prompt
prompt Creating table HT_EQ_RP_PLAN
prompt ============================
prompt
create table ZS18.HT_EQ_RP_PLAN
(
  FLOW_STATUS    VARCHAR2(2) default -1,
  CREATE_ID      VARCHAR2(32),
  CREATE_DEPT_ID VARCHAR2(32),
  CREATE_TIME    VARCHAR2(19),
  REMARK         VARCHAR2(512),
  IS_DEL         VARCHAR2(1) default 0,
  PZ_CODE        VARCHAR2(32) not null,
  GOWHERE        VARCHAR2(1),
  EXPIRED_DATE   VARCHAR2(10),
  TASK_STATUS    CHAR(10) default 0,
  MT_NAME        VARCHAR2(50)
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
comment on table ZS18.HT_EQ_RP_PLAN
  is '停产维修计划审批主表';
comment on column ZS18.HT_EQ_RP_PLAN.FLOW_STATUS
  is '审批状态 -1 未提交审批 0办理中 ２己通过 １未通过';
comment on column ZS18.HT_EQ_RP_PLAN.CREATE_ID
  is '申请人id';
comment on column ZS18.HT_EQ_RP_PLAN.CREATE_DEPT_ID
  is '申请部门id';
comment on column ZS18.HT_EQ_RP_PLAN.CREATE_TIME
  is '申请时间';
comment on column ZS18.HT_EQ_RP_PLAN.REMARK
  is '备注';
comment on column ZS18.HT_EQ_RP_PLAN.IS_DEL
  is '删除标识';
comment on column ZS18.HT_EQ_RP_PLAN.PZ_CODE
  is '凭证号 RP+ YYYYMMDD + 三位流水号';
comment on column ZS18.HT_EQ_RP_PLAN.GOWHERE
  is '流程决策';
comment on column ZS18.HT_EQ_RP_PLAN.EXPIRED_DATE
  is '过期时间';
comment on column ZS18.HT_EQ_RP_PLAN.TASK_STATUS
  is '执行状态 0 未执行  1 执行中  2 己完成 3 己过期';
comment on column ZS18.HT_EQ_RP_PLAN.MT_NAME
  is '维修计划名';
alter table ZS18.HT_EQ_RP_PLAN
  add constraint PK_HT_RP_PLAN primary key (PZ_CODE)
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
prompt Creating table HT_EQ_RP_PLAN_DETAIL
prompt ===================================
prompt
create table ZS18.HT_EQ_RP_PLAN_DETAIL
(
  ID              INTEGER not null,
  MAIN_ID         VARCHAR2(32),
  EQUIPMENT_ID    VARCHAR2(512),
  NTRODUCER       VARCHAR2(128),
  STATUS          VARCHAR2(16) default 0,
  REMARK          VARCHAR2(512),
  CREATE_ID       VARCHAR2(32),
  CREATE_TIME     VARCHAR2(32),
  IS_DEL          VARCHAR2(1) default 0,
  MECH_AREA       VARCHAR2(32),
  RESPONER        VARCHAR2(20),
  EXP_FINISH_TIME VARCHAR2(19),
  IS_OUT          VARCHAR2(10),
  PICKUP_DESC     VARCHAR2(512),
  YEAR_MONTH      VARCHAR2(20),
  FAULT_ID        INTEGER,
  EXE_TIME        VARCHAR2(19),
  REASON          VARCHAR2(512),
  CONTENT         VARCHAR2(512),
  IS_EMG          VARCHAR2(1) default 0,
  EXE_SEGTIME     INTEGER,
  VERIFIOR        VARCHAR2(20)
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
comment on table ZS18.HT_EQ_RP_PLAN_DETAIL
  is '停产维修计划从表';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.ID
  is '主键标识';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.MAIN_ID
  is '主表id';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.EQUIPMENT_ID
  is '维修设备ID';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.NTRODUCER
  is '提出人';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.STATUS
  is '状态 -1上报未处理0未派工1己派工 2己完成';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.REMARK
  is '备注';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.CREATE_ID
  is '创建人id';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.CREATE_TIME
  is '创建时间';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.IS_DEL
  is '是否删除';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.MECH_AREA
  is '区域';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.RESPONER
  is '责任人';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.EXP_FINISH_TIME
  is '期望完成时间';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.IS_OUT
  is '是否委外';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.PICKUP_DESC
  is '备件情况';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.YEAR_MONTH
  is '所属年月';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.FAULT_ID
  is '故障ID';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.EXE_TIME
  is '操作时间';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.REASON
  is '维修原因';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.CONTENT
  is '维修内容';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.IS_EMG
  is '是否是应急维修';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.EXE_SEGTIME
  is '操作时长';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.VERIFIOR
  is '验证人';
alter table ZS18.HT_EQ_RP_PLAN_DETAIL
  add constraint PK_HT_EQ_RP_PLAN_DETAIL primary key (ID)
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
prompt Creating table HT_EQ_STG_PICKUP
prompt ===============================
prompt
create table ZS18.HT_EQ_STG_PICKUP
(
  PICKUP_DATE    VARCHAR2(32),
  FLOW_STATUS    VARCHAR2(10),
  CREATE_ID      VARCHAR2(32),
  CREATE_DEPT_ID VARCHAR2(32),
  CREATE_TIME    VARCHAR2(19),
  REMARK         VARCHAR2(512),
  IS_DEL         VARCHAR2(1),
  PZ_CODE        VARCHAR2(32) not null,
  GOWHERE        VARCHAR2(1),
  IS_PICKUP      VARCHAR2(1) default 0
)
tablespace ZS_DATA
  pctfree 10
  initrans 1
  maxtrans 255;
comment on table ZS18.HT_EQ_STG_PICKUP
  is '备件领用审批';
comment on column ZS18.HT_EQ_STG_PICKUP.PICKUP_DATE
  is '领用日期';
comment on column ZS18.HT_EQ_STG_PICKUP.FLOW_STATUS
  is '审批状态 -1 未提交审批 0办理中 ２己通过 １未通过';
comment on column ZS18.HT_EQ_STG_PICKUP.CREATE_ID
  is '申请人id';
comment on column ZS18.HT_EQ_STG_PICKUP.CREATE_DEPT_ID
  is '申请部门id';
comment on column ZS18.HT_EQ_STG_PICKUP.CREATE_TIME
  is '申请时间';
comment on column ZS18.HT_EQ_STG_PICKUP.REMARK
  is '备注';
comment on column ZS18.HT_EQ_STG_PICKUP.IS_DEL
  is '删除标识';
comment on column ZS18.HT_EQ_STG_PICKUP.PZ_CODE
  is '凭证号';
comment on column ZS18.HT_EQ_STG_PICKUP.GOWHERE
  is '流程决策';
comment on column ZS18.HT_EQ_STG_PICKUP.IS_PICKUP
  is '是否领用';
alter table ZS18.HT_EQ_STG_PICKUP
  add constraint PK_HT_EQ_STG_PICKUP primary key (PZ_CODE)
  using index 
  tablespace ZS_DATA
  pctfree 10
  initrans 2
  maxtrans 255;

prompt
prompt Creating table HT_EQ_STG_PICKUP_DETAIL
prompt ======================================
prompt
create table ZS18.HT_EQ_STG_PICKUP_DETAIL
(
  ID          INTEGER not null,
  MAIN_CODE   VARCHAR2(32),
  SP_CODE     VARCHAR2(32),
  SP_NAME     VARCHAR2(256),
  SP_STANDARD VARCHAR2(64),
  SP_MODEL    VARCHAR2(64),
  PICKUP_NUM  VARCHAR2(32),
  SP_UNIT     VARCHAR2(32),
  OWN_SECTION VARCHAR2(512),
  REMARK      VARCHAR2(512),
  STATUS      VARCHAR2(16),
  OWN_EQUI    VARCHAR2(128),
  CREATE_ID   VARCHAR2(32),
  CREATE_TIME VARCHAR2(32),
  IS_DEL      VARCHAR2(1),
  STORAGE     VARCHAR2(32)
)
tablespace ZS_DATA
  pctfree 10
  initrans 1
  maxtrans 255;
comment on table ZS18.HT_EQ_STG_PICKUP_DETAIL
  is '备件领用明细表';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.ID
  is '主键标识';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.MAIN_CODE
  is '领用审批主表id';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.SP_CODE
  is '备件编码';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.SP_NAME
  is '备件名称';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.SP_STANDARD
  is '规格';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.SP_MODEL
  is '型号';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.PICKUP_NUM
  is '采购数量';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.SP_UNIT
  is '备件单位';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.OWN_SECTION
  is '使用部位（工艺段）';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.REMARK
  is '备注';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.STATUS
  is '状态';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.OWN_EQUI
  is '所属设备';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.CREATE_ID
  is '创建人id';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.CREATE_TIME
  is '创建时间';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.IS_DEL
  is '是否删除';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.STORAGE
  is '仓库ID';
alter table ZS18.HT_EQ_STG_PICKUP_DETAIL
  add constraint PK_HT_EQ_STG_PICKUP_DETAIL primary key (ID)
  using index 
  tablespace ZS_DATA
  pctfree 10
  initrans 2
  maxtrans 255;

prompt
prompt Creating table HT_INNER_APRV_STATUS
prompt ===================================
prompt
create table ZS18.HT_INNER_APRV_STATUS
(
  ID   VARCHAR2(2) not null,
  NAME VARCHAR2(50)
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
comment on table ZS18.HT_INNER_APRV_STATUS
  is '审批状态';
comment on column ZS18.HT_INNER_APRV_STATUS.ID
  is 'ID值';
comment on column ZS18.HT_INNER_APRV_STATUS.NAME
  is '描述';
alter table ZS18.HT_INNER_APRV_STATUS
  add constraint PK_APRV_STATUS_ID primary key (ID)
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
prompt Creating table HT_INNER_BOOL_DISPLAY
prompt ====================================
prompt
create table ZS18.HT_INNER_BOOL_DISPLAY
(
  ID           VARCHAR2(2) not null,
  CTRL_NAME    VARCHAR2(50),
  STRG_NAME    VARCHAR2(50),
  ISSUE_NAME   VARCHAR2(50),
  INSPECT_TYPE VARCHAR2(50)
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
comment on table ZS18.HT_INNER_BOOL_DISPLAY
  is '受控状态';
comment on column ZS18.HT_INNER_BOOL_DISPLAY.ID
  is 'ID值';
comment on column ZS18.HT_INNER_BOOL_DISPLAY.CTRL_NAME
  is '受控状态';
comment on column ZS18.HT_INNER_BOOL_DISPLAY.STRG_NAME
  is '出入库状态';
comment on column ZS18.HT_INNER_BOOL_DISPLAY.ISSUE_NAME
  is '下发状态';
comment on column ZS18.HT_INNER_BOOL_DISPLAY.INSPECT_TYPE
  is '工艺检查类型';

prompt
prompt Creating table HT_INNER_INSPECT_GROUP
prompt =====================================
prompt
create table ZS18.HT_INNER_INSPECT_GROUP
(
  ID   VARCHAR2(2) not null,
  NAME VARCHAR2(50)
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
comment on table ZS18.HT_INNER_INSPECT_GROUP
  is '工艺检查成品检查分组';
comment on column ZS18.HT_INNER_INSPECT_GROUP.ID
  is 'ID值';
comment on column ZS18.HT_INNER_INSPECT_GROUP.NAME
  is '描述';
alter table ZS18.HT_INNER_INSPECT_GROUP
  add constraint PK_INSPECT_GROUP primary key (ID)
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
prompt Creating table HT_INNER_MAP
prompt ===========================
prompt
create table ZS18.HT_INNER_MAP
(
  URL    VARCHAR2(255) not null,
  REMARK VARCHAR2(255),
  MAPID  VARCHAR2(5) not null,
  IS_DEL VARCHAR2(1) default 0
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
comment on table ZS18.HT_INNER_MAP
  is '页面映射表';
comment on column ZS18.HT_INNER_MAP.URL
  is 'URL';
comment on column ZS18.HT_INNER_MAP.REMARK
  is '描述';
alter table ZS18.HT_INNER_MAP
  add constraint PK_MAPID primary key (MAPID)
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
prompt Creating table HT_INNER_MAT_DEPOT
prompt =================================
prompt
create table ZS18.HT_INNER_MAT_DEPOT
(
  ID   VARCHAR2(2) not null,
  NAME VARCHAR2(50)
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
comment on table ZS18.HT_INNER_MAT_DEPOT
  is '原料仓库';
comment on column ZS18.HT_INNER_MAT_DEPOT.ID
  is 'ID值';
comment on column ZS18.HT_INNER_MAT_DEPOT.NAME
  is '描述';
alter table ZS18.HT_INNER_MAT_DEPOT
  add constraint PK_DEPOT_ID primary key (ID)
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
prompt Creating table HT_PROD_MANUAL_RECODE
prompt ====================================
prompt
create table ZS18.HT_PROD_MANUAL_RECODE
(
  ID          INTEGER not null,
  PARA_NAME   VARCHAR2(30),
  VALUE       FLOAT,
  B_TIME      VARCHAR2(19),
  E_TIME      VARCHAR2(10),
  SHIFT       VARCHAR2(20),
  CREATOR     VARCHAR2(50),
  CREATE_TIME VARCHAR2(50),
  IS_DEL      VARCHAR2(19),
  PARA_CODE   VARCHAR2(30)
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
  );
comment on column ZS18.HT_PROD_MANUAL_RECODE.ID
  is '记录ID';
comment on column ZS18.HT_PROD_MANUAL_RECODE.PARA_NAME
  is '参数名';
comment on column ZS18.HT_PROD_MANUAL_RECODE.VALUE
  is '参数值';
comment on column ZS18.HT_PROD_MANUAL_RECODE.B_TIME
  is '开始时间';
comment on column ZS18.HT_PROD_MANUAL_RECODE.E_TIME
  is '结束时间';
comment on column ZS18.HT_PROD_MANUAL_RECODE.SHIFT
  is '班组';
comment on column ZS18.HT_PROD_MANUAL_RECODE.CREATOR
  is '记录人员';
comment on column ZS18.HT_PROD_MANUAL_RECODE.CREATE_TIME
  is '记录时间';
comment on column ZS18.HT_PROD_MANUAL_RECODE.PARA_CODE
  is '参数编码';

prompt
prompt Creating table HT_PROD_MONTH_PLAN
prompt =================================
prompt
create table ZS18.HT_PROD_MONTH_PLAN
(
  ID            INTEGER not null,
  PLAN_NAME     VARCHAR2(128) not null,
  B_FLOW_STATUS VARCHAR2(2) default -1,
  E_FLOW_STATUS VARCHAR2(10),
  ISSUED_STATUS VARCHAR2(1) default 0,
  CREATE_ID     VARCHAR2(32),
  CREATE_TIME   VARCHAR2(19),
  MODIFY_ID     VARCHAR2(32),
  MODIFY_TIME   VARCHAR2(19),
  IS_DEL        VARCHAR2(1) default 0,
  ADJUST_STATUS VARCHAR2(1),
  PLAN_TIME     VARCHAR2(8),
  IS_VALID      VARCHAR2(1) default 0,
  REMARK        VARCHAR2(512)
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
comment on table ZS18.HT_PROD_MONTH_PLAN
  is '依据季度到月生产计划拆分成月度生产任务';
comment on column ZS18.HT_PROD_MONTH_PLAN.ID
  is '月度生产任务唯一标识';
comment on column ZS18.HT_PROD_MONTH_PLAN.PLAN_NAME
  is '计划名称';
comment on column ZS18.HT_PROD_MONTH_PLAN.B_FLOW_STATUS
  is '计划生产审批状态 -1 未提交审批 0办理中 ２己通过 １未通过';
comment on column ZS18.HT_PROD_MONTH_PLAN.E_FLOW_STATUS
  is '计划结束审批状态';
comment on column ZS18.HT_PROD_MONTH_PLAN.ISSUED_STATUS
  is '下发状态 0 未下发 1 己下发';
comment on column ZS18.HT_PROD_MONTH_PLAN.CREATE_ID
  is '创建人编码';
comment on column ZS18.HT_PROD_MONTH_PLAN.CREATE_TIME
  is '创建时间';
comment on column ZS18.HT_PROD_MONTH_PLAN.MODIFY_ID
  is '修改人编码';
comment on column ZS18.HT_PROD_MONTH_PLAN.MODIFY_TIME
  is '修改时间';
comment on column ZS18.HT_PROD_MONTH_PLAN.IS_DEL
  is '删除标识';
comment on column ZS18.HT_PROD_MONTH_PLAN.ADJUST_STATUS
  is '计划明细是否有调整 0 未调整 1 已调整';
comment on column ZS18.HT_PROD_MONTH_PLAN.PLAN_TIME
  is '后加字段-计划时间';
comment on column ZS18.HT_PROD_MONTH_PLAN.IS_VALID
  is '是否审批';
alter table ZS18.HT_PROD_MONTH_PLAN
  add constraint PK_HT_PROD_MONTH_PLAN primary key (ID, PLAN_NAME)
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
prompt Creating table HT_PROD_MONTH_PLAN_DETAIL
prompt ========================================
prompt
create table ZS18.HT_PROD_MONTH_PLAN_DETAIL
(
  ID            INTEGER,
  MONTH_PLAN_ID INTEGER not null,
  PLAN_NO       VARCHAR2(32) not null,
  PROD_CODE     VARCHAR2(32),
  PLAN_YEAR     VARCHAR2(10),
  PROD_MONTH    VARCHAR2(10),
  PLAN_TYPE     VARCHAR2(10),
  PLAN_OUTPUT   NUMBER(19,2),
  ADJUST_OUTPUT NUMBER(19,2),
  FINISH_OUTPUT NUMBER(19,3),
  TECH_VERSION  VARCHAR2(64),
  CREATE_ID     VARCHAR2(32),
  CREATOR       VARCHAR2(64),
  CREATE_TIME   DATE,
  MODIFY_ID     VARCHAR2(32),
  MODIFY_NAME   VARCHAR2(32),
  MODIFY_TIME   DATE,
  EXE_STATUS    VARCHAR2(10) default 0,
  IS_DEL        VARCHAR2(1) default 0,
  PLAN_SORT     VARCHAR2(16) default 0,
  PLAN_PATH     VARCHAR2(60) default 0,
  IS_VALID      VARCHAR2(1) default 1,
  PATH_DONE     VARCHAR2(1) default 0
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
comment on table ZS18.HT_PROD_MONTH_PLAN_DETAIL
  is '月度生产任务计划明细表';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.ID
  is '月度生产任务唯一标识';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.MONTH_PLAN_ID
  is '月度生产计划标识';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.PLAN_NO
  is '计划号';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.PROD_CODE
  is '生产牌号';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.PLAN_YEAR
  is '计划年份';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.PROD_MONTH
  is '生产月份';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.PLAN_TYPE
  is '生产计划类型(0:日常生产,1:临时增加,2:工艺测试)';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.PLAN_OUTPUT
  is '本月计划生产量';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.ADJUST_OUTPUT
  is '调整后产量';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.FINISH_OUTPUT
  is '完成产量';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.TECH_VERSION
  is '工艺版本号';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.CREATE_ID
  is '创建人编码';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.CREATOR
  is '创建人';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.CREATE_TIME
  is '创建时间';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.MODIFY_ID
  is '修改人编码';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.MODIFY_NAME
  is '修改人名称';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.MODIFY_TIME
  is '修改时间';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.EXE_STATUS
  is '执行状态 0未下发 1 己下发 2 生产中 3 暂停4完成5撤消';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.IS_DEL
  is '删除标识';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.PLAN_SORT
  is '排序';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.IS_VALID
  is '是否被审批后有效';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.PATH_DONE
  is '是否指定了生产路径';
alter table ZS18.HT_PROD_MONTH_PLAN_DETAIL
  add constraint PK_HT_PROD_MONTH_PLAN_DETAIL primary key (PLAN_NO, MONTH_PLAN_ID)
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
prompt Creating table HT_PROD_REPORT
prompt =============================
prompt
create table ZS18.HT_PROD_REPORT
(
  SECTION_CODE VARCHAR2(50),
  PLANNO       VARCHAR2(50),
  STARTTIME    VARCHAR2(19),
  ENDTIME      VARCHAR2(19) default '2200-01-01 00:00:00',
  PROD_CODE    VARCHAR2(20),
  TECH_PARA1   FLOAT,
  TECH_PARA2   FLOAT,
  TECH_PARA3   FLOAT,
  TECH_PARA4   FLOAT,
  TECH_PARA5   FLOAT,
  TECH_PARA6   FLOAT,
  TECH_PARA7   FLOAT,
  TECH_PARA8   FLOAT,
  TECH_PARA9   FLOAT,
  TECH_PARA10  FLOAT,
  TECH_PARA11  FLOAT
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
comment on column ZS18.HT_PROD_REPORT.SECTION_CODE
  is '工艺段/工序段编码';
comment on column ZS18.HT_PROD_REPORT.PLANNO
  is '计划号';
comment on column ZS18.HT_PROD_REPORT.STARTTIME
  is '开始时间';
comment on column ZS18.HT_PROD_REPORT.ENDTIME
  is '结束时间';
comment on column ZS18.HT_PROD_REPORT.PROD_CODE
  is '产品编码';
comment on column ZS18.HT_PROD_REPORT.TECH_PARA1
  is '参数1';
comment on column ZS18.HT_PROD_REPORT.TECH_PARA2
  is '参数2';

prompt
prompt Creating table HT_SYS_SHIFT
prompt ===========================
prompt
create table ZS18.HT_SYS_SHIFT
(
  SHIFT_CODE  VARCHAR2(2) not null,
  SHIFT_NAME  VARCHAR2(64),
  WORKSHOP_ID VARCHAR2(32),
  BEGIN_TIME  VARCHAR2(32),
  END_TIME    VARCHAR2(32),
  CREATE_ID   VARCHAR2(32),
  CREATE_TIME VARCHAR2(19),
  MODIFY_ID   VARCHAR2(32),
  MODIFY_TIME VARCHAR2(19),
  IS_VALID    VARCHAR2(1) default 1,
  IS_DEL      VARCHAR2(1) default 0,
  INTER_DAY   VARCHAR2(1)
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
comment on table ZS18.HT_SYS_SHIFT
  is '班时基础信息表';
comment on column ZS18.HT_SYS_SHIFT.SHIFT_CODE
  is '班时编码';
comment on column ZS18.HT_SYS_SHIFT.SHIFT_NAME
  is '班时名称';
comment on column ZS18.HT_SYS_SHIFT.WORKSHOP_ID
  is '所属职能部门';
comment on column ZS18.HT_SYS_SHIFT.BEGIN_TIME
  is '班时开始时间';
comment on column ZS18.HT_SYS_SHIFT.END_TIME
  is '班时结束时间';
comment on column ZS18.HT_SYS_SHIFT.CREATE_ID
  is '创建人编码';
comment on column ZS18.HT_SYS_SHIFT.CREATE_TIME
  is '创建时间';
comment on column ZS18.HT_SYS_SHIFT.MODIFY_ID
  is '修改人编码';
comment on column ZS18.HT_SYS_SHIFT.MODIFY_TIME
  is '修改时间';
comment on column ZS18.HT_SYS_SHIFT.IS_VALID
  is '是否启用';
comment on column ZS18.HT_SYS_SHIFT.IS_DEL
  is '删除标识';
comment on column ZS18.HT_SYS_SHIFT.INTER_DAY
  is '是否跨天';
alter table ZS18.HT_SYS_SHIFT
  add constraint PK_HT_SYS_SHIFT primary key (SHIFT_CODE)
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
prompt Creating table HT_SYS_TEAM
prompt ==========================
prompt
create table ZS18.HT_SYS_TEAM
(
  TEAM_CODE   VARCHAR2(2) not null,
  TEAM_NAME   VARCHAR2(64),
  TEAM_TYPE   VARCHAR2(32),
  WORKSHOP_ID VARCHAR2(32),
  CREATE_ID   VARCHAR2(32),
  CREATE_TIME VARCHAR2(19),
  MODIFY_ID   VARCHAR2(32),
  MODIFY_TIME VARCHAR2(19),
  IS_VALID    VARCHAR2(1) default 1,
  IS_DEL      VARCHAR2(1) default 0
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
comment on table ZS18.HT_SYS_TEAM
  is '班组基础信息表';
comment on column ZS18.HT_SYS_TEAM.TEAM_CODE
  is '班组编码';
comment on column ZS18.HT_SYS_TEAM.TEAM_NAME
  is '班组名称';
comment on column ZS18.HT_SYS_TEAM.TEAM_TYPE
  is '班组类型（数据字典维护）';
comment on column ZS18.HT_SYS_TEAM.WORKSHOP_ID
  is '所属职能部门';
comment on column ZS18.HT_SYS_TEAM.CREATE_ID
  is '创建人编码';
comment on column ZS18.HT_SYS_TEAM.CREATE_TIME
  is '创建时间';
comment on column ZS18.HT_SYS_TEAM.MODIFY_ID
  is '修改人编码';
comment on column ZS18.HT_SYS_TEAM.MODIFY_TIME
  is '修改时间';
comment on column ZS18.HT_SYS_TEAM.IS_VALID
  is '是否启用';
comment on column ZS18.HT_SYS_TEAM.IS_DEL
  is '软删除标识';
alter table ZS18.HT_SYS_TEAM
  add constraint PK_HT_SYS_TEAM primary key (TEAM_CODE)
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
prompt Creating table HT_PROD_SCHEDULE
prompt ===============================
prompt
create table ZS18.HT_PROD_SCHEDULE
(
  ID             INTEGER not null,
  WORK_DATE      VARCHAR2(10),
  WORK_SHOP_CODE VARCHAR2(32),
  SHIFT_CODE     VARCHAR2(32),
  TEAM_CODE      VARCHAR2(32),
  WORK_STAUS     VARCHAR2(1),
  REMARK         VARCHAR2(256),
  TEAM_TYPE      VARCHAR2(1),
  CREATE_ID      VARCHAR2(32),
  CREATOR        VARCHAR2(64),
  CREATE_TIME    VARCHAR2(19),
  MODIFY_ID      VARCHAR2(32),
  MODIFY_TIME    VARCHAR2(19),
  IS_VALID       VARCHAR2(1) default 1,
  IS_DEL         VARCHAR2(1) default 0,
  DATE_BEGIN     VARCHAR2(19),
  DATE_END       VARCHAR2(19)
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
comment on table ZS18.HT_PROD_SCHEDULE
  is '生产和电修班排班一致，分甲乙丙班，机修班除甲乙丙外还有常白班';
comment on column ZS18.HT_PROD_SCHEDULE.ID
  is '排班表唯一标识';
comment on column ZS18.HT_PROD_SCHEDULE.WORK_DATE
  is '生产排班日';
comment on column ZS18.HT_PROD_SCHEDULE.WORK_SHOP_CODE
  is '车间编码';
comment on column ZS18.HT_PROD_SCHEDULE.SHIFT_CODE
  is '班时编码';
comment on column ZS18.HT_PROD_SCHEDULE.TEAM_CODE
  is '班组编码';
comment on column ZS18.HT_PROD_SCHEDULE.WORK_STAUS
  is '0:休息,1:工作';
comment on column ZS18.HT_PROD_SCHEDULE.REMARK
  is '备注';
comment on column ZS18.HT_PROD_SCHEDULE.TEAM_TYPE
  is '排班类型,0:生产(电修)排班;1:机修排班';
comment on column ZS18.HT_PROD_SCHEDULE.CREATE_ID
  is '创建人编码';
comment on column ZS18.HT_PROD_SCHEDULE.CREATOR
  is '创建人名称';
comment on column ZS18.HT_PROD_SCHEDULE.CREATE_TIME
  is '创建时间';
comment on column ZS18.HT_PROD_SCHEDULE.MODIFY_ID
  is '修改人编码';
comment on column ZS18.HT_PROD_SCHEDULE.MODIFY_TIME
  is '修改时间';
comment on column ZS18.HT_PROD_SCHEDULE.IS_VALID
  is '有效标识';
comment on column ZS18.HT_PROD_SCHEDULE.IS_DEL
  is '软删除标识';
comment on column ZS18.HT_PROD_SCHEDULE.DATE_BEGIN
  is '进班时间';
comment on column ZS18.HT_PROD_SCHEDULE.DATE_END
  is '结班时间';
alter table ZS18.HT_PROD_SCHEDULE
  add constraint PK_HT_PROD_SCHEDULE primary key (ID)
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
alter table ZS18.HT_PROD_SCHEDULE
  add constraint FK_T_AM_WOR_班时_排班_T_AM_SHI foreign key (SHIFT_CODE)
  references ZS18.HT_SYS_SHIFT (SHIFT_CODE);
alter table ZS18.HT_PROD_SCHEDULE
  add constraint FK_T_AM_WOR_班组_排班_T_AM_TEA foreign key (TEAM_CODE)
  references ZS18.HT_SYS_TEAM (TEAM_CODE);

prompt
prompt Creating table HT_PROD_SEASON_PLAN
prompt ==================================
prompt
create table ZS18.HT_PROD_SEASON_PLAN
(
  ID            VARCHAR2(64) not null,
  PLAN_NAME     VARCHAR2(128),
  FLOW_STATUS   VARCHAR2(2) default -1,
  ISSUED_STATUS VARCHAR2(1) default 0,
  CREATE_ID     VARCHAR2(32),
  CREATE_TIME   VARCHAR2(19),
  MODIFY_ID     VARCHAR2(32),
  MODIFY        VARCHAR2(32),
  MODIFY_TIME   VARCHAR2(19),
  DEPT_ID       VARCHAR2(32),
  PLAN_YEAR     VARCHAR2(16),
  QUARTER       VARCHAR2(32),
  UNIT          VARCHAR2(32),
  REMARK        VARCHAR2(256),
  IS_DEL        VARCHAR2(1) default 0,
  TOTAL_OUTPUT  NUMBER(19,2),
  PZ_CODE       VARCHAR2(64)
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
comment on table ZS18.HT_PROD_SEASON_PLAN
  is '季度到月生产计划';
comment on column ZS18.HT_PROD_SEASON_PLAN.ID
  is '季度到月生产计划唯一标识';
comment on column ZS18.HT_PROD_SEASON_PLAN.PLAN_NAME
  is '季度到月生产计划名称';
comment on column ZS18.HT_PROD_SEASON_PLAN.FLOW_STATUS
  is '审批状态 -1 未提交审批 0办理中 ２己通过 １未通过';
comment on column ZS18.HT_PROD_SEASON_PLAN.ISSUED_STATUS
  is '下发状态 0 未下发 1 己下发';
comment on column ZS18.HT_PROD_SEASON_PLAN.CREATE_ID
  is '创建人标识';
comment on column ZS18.HT_PROD_SEASON_PLAN.CREATE_TIME
  is '创建时间';
comment on column ZS18.HT_PROD_SEASON_PLAN.MODIFY_ID
  is '修改人标识（编码）';
comment on column ZS18.HT_PROD_SEASON_PLAN.MODIFY
  is '修改人';
comment on column ZS18.HT_PROD_SEASON_PLAN.MODIFY_TIME
  is '修改时间';
comment on column ZS18.HT_PROD_SEASON_PLAN.DEPT_ID
  is '部门标识';
comment on column ZS18.HT_PROD_SEASON_PLAN.PLAN_YEAR
  is '年份';
comment on column ZS18.HT_PROD_SEASON_PLAN.QUARTER
  is '季度标识';
comment on column ZS18.HT_PROD_SEASON_PLAN.UNIT
  is '计划产量单位标识';
comment on column ZS18.HT_PROD_SEASON_PLAN.REMARK
  is '季度到月生产计划备注';
comment on column ZS18.HT_PROD_SEASON_PLAN.IS_DEL
  is '删除标识';
comment on column ZS18.HT_PROD_SEASON_PLAN.TOTAL_OUTPUT
  is '本季度计划总产量';
comment on column ZS18.HT_PROD_SEASON_PLAN.PZ_CODE
  is '凭证号';
alter table ZS18.HT_PROD_SEASON_PLAN
  add constraint PK_HT_PROD_SEASON_PLAN primary key (ID)
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
prompt Creating table HT_PROD_SEASON_PLAN_DETAIL
prompt =========================================
prompt
create table ZS18.HT_PROD_SEASON_PLAN_DETAIL
(
  ID              INTEGER not null,
  QUARTER_PLAN_ID VARCHAR2(64),
  PROD_CODE       VARCHAR2(32),
  PLAN_OUTPUT_1   NUMBER(19,2),
  PLAN_OUTPUT_2   NUMBER(19,2),
  PLAN_OUTPUT_3   NUMBER(19,2),
  TOTAL_OUTPUT    NUMBER(19,2),
  IS_DEL          VARCHAR2(1) default 0,
  OUTPUT_1_ADJUST VARCHAR2(1),
  OUTPUT_2_ADJUST VARCHAR2(1),
  OUTPUT_3_ADJUST VARCHAR2(1)
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
comment on table ZS18.HT_PROD_SEASON_PLAN_DETAIL
  is '季度到月计划明细情况';
comment on column ZS18.HT_PROD_SEASON_PLAN_DETAIL.ID
  is '季度到月计划唯一表';
comment on column ZS18.HT_PROD_SEASON_PLAN_DETAIL.QUARTER_PLAN_ID
  is '父表ID';
comment on column ZS18.HT_PROD_SEASON_PLAN_DETAIL.PROD_CODE
  is '产品牌号（产品等级或产品规格）';
comment on column ZS18.HT_PROD_SEASON_PLAN_DETAIL.PLAN_OUTPUT_1
  is '计划生产数量';
comment on column ZS18.HT_PROD_SEASON_PLAN_DETAIL.PLAN_OUTPUT_2
  is '计划生产数量';
comment on column ZS18.HT_PROD_SEASON_PLAN_DETAIL.PLAN_OUTPUT_3
  is '计划生产数量';
comment on column ZS18.HT_PROD_SEASON_PLAN_DETAIL.TOTAL_OUTPUT
  is '计划生产总量';
comment on column ZS18.HT_PROD_SEASON_PLAN_DETAIL.IS_DEL
  is '删除标识';
comment on column ZS18.HT_PROD_SEASON_PLAN_DETAIL.OUTPUT_1_ADJUST
  is '计划数量1是否有调整';
comment on column ZS18.HT_PROD_SEASON_PLAN_DETAIL.OUTPUT_2_ADJUST
  is '计划数量2是否有调整';
comment on column ZS18.HT_PROD_SEASON_PLAN_DETAIL.OUTPUT_3_ADJUST
  is '计划数量3是否有调整';
alter table ZS18.HT_PROD_SEASON_PLAN_DETAIL
  add constraint PK_SEASON_DETAIL_ID primary key (ID)
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
prompt Creating table HT_PROD_SHIFTCHG
prompt ===============================
prompt
create table ZS18.HT_PROD_SHIFTCHG
(
  SHIFT_MAIN_ID INTEGER,
  INSPECT_DATE  VARCHAR2(10),
  WORKSHOP_CODE VARCHAR2(32),
  SHIFT_CODE    VARCHAR2(32),
  TEAM_CODE     VARCHAR2(32),
  PLAN_NO       VARCHAR2(32),
  PROD_CODE     VARCHAR2(32),
  OUTPUT_VL     NUMBER(10,2),
  SHIFT_STATUS  VARCHAR2(15) default 1,
  B_TIME        DATE,
  E_TIME        DATE,
  SHIFT_ID      VARCHAR2(32),
  SUCC_ID       VARCHAR2(32),
  REMARK        VARCHAR2(1000),
  CREATE_ID     VARCHAR2(32),
  CREATE_TIME   DATE,
  MODIFY_ID     VARCHAR2(32),
  MODIFY_TIME   DATE,
  DEVICESTATUS  VARCHAR2(1000),
  QLT_STATUS    VARCHAR2(1000),
  SCEAN_STATUS  VARCHAR2(1000),
  OUTPLUS       NUMBER(10,2)
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
comment on column ZS18.HT_PROD_SHIFTCHG.SHIFT_MAIN_ID
  is '主标识ID';
comment on column ZS18.HT_PROD_SHIFTCHG.INSPECT_DATE
  is '日期';
comment on column ZS18.HT_PROD_SHIFTCHG.WORKSHOP_CODE
  is '车间';
comment on column ZS18.HT_PROD_SHIFTCHG.SHIFT_CODE
  is '班时';
comment on column ZS18.HT_PROD_SHIFTCHG.TEAM_CODE
  is '班组';
comment on column ZS18.HT_PROD_SHIFTCHG.PLAN_NO
  is '计划号';
comment on column ZS18.HT_PROD_SHIFTCHG.PROD_CODE
  is '产品编码';
comment on column ZS18.HT_PROD_SHIFTCHG.OUTPUT_VL
  is '当班产量';
comment on column ZS18.HT_PROD_SHIFTCHG.SHIFT_STATUS
  is '状态 ';
comment on column ZS18.HT_PROD_SHIFTCHG.B_TIME
  is '生产开始时间';
comment on column ZS18.HT_PROD_SHIFTCHG.E_TIME
  is '生产时间时间';
comment on column ZS18.HT_PROD_SHIFTCHG.SHIFT_ID
  is '交班人';
comment on column ZS18.HT_PROD_SHIFTCHG.SUCC_ID
  is '接班人';
comment on column ZS18.HT_PROD_SHIFTCHG.REMARK
  is '备注';
comment on column ZS18.HT_PROD_SHIFTCHG.DEVICESTATUS
  is '设备运行情况';
comment on column ZS18.HT_PROD_SHIFTCHG.QLT_STATUS
  is '工艺质量情况';
comment on column ZS18.HT_PROD_SHIFTCHG.SCEAN_STATUS
  is '现场情况';
comment on column ZS18.HT_PROD_SHIFTCHG.OUTPLUS
  is '产量零头';

prompt
prompt Creating table HT_PROD_SHIFTCHG_DETAIL
prompt ======================================
prompt
create table ZS18.HT_PROD_SHIFTCHG_DETAIL
(
  OUTPUT_ID     INTEGER,
  SHIFT_MAIN_ID INTEGER,
  MATER_CODE    VARCHAR2(32),
  MATER_VL      NUMBER(10,2),
  BZ_UNIT       VARCHAR2(32),
  REMARK        VARCHAR2(500)
)
tablespace ZS_DATA
  pctfree 10
  initrans 1
  maxtrans 255;
comment on column ZS18.HT_PROD_SHIFTCHG_DETAIL.OUTPUT_ID
  is '从表ID';
comment on column ZS18.HT_PROD_SHIFTCHG_DETAIL.SHIFT_MAIN_ID
  is '主ID';
comment on column ZS18.HT_PROD_SHIFTCHG_DETAIL.MATER_CODE
  is '物料编码';
comment on column ZS18.HT_PROD_SHIFTCHG_DETAIL.MATER_VL
  is '物料值';
comment on column ZS18.HT_PROD_SHIFTCHG_DETAIL.BZ_UNIT
  is '单位';
comment on column ZS18.HT_PROD_SHIFTCHG_DETAIL.REMARK
  is '备注';

prompt
prompt Creating table HT_PUB_APRV_FLOWINFO
prompt ===================================
prompt
create table ZS18.HT_PUB_APRV_FLOWINFO
(
  ID         INTEGER not null,
  TBR_ID     VARCHAR2(20),
  TBR_NAME   VARCHAR2(20),
  TB_BM_ID   VARCHAR2(32),
  TB_BM_NAME VARCHAR2(100),
  TB_DATE    VARCHAR2(20),
  MODULENAME VARCHAR2(32),
  URL        VARCHAR2(500),
  BUSIN_ID   VARCHAR2(64),
  STATE      VARCHAR2(10) default 0,
  TB_ZT      VARCHAR2(200),
  REMARK     VARCHAR2(256)
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
comment on table ZS18.HT_PUB_APRV_FLOWINFO
  is '审批流程基本信息
';
comment on column ZS18.HT_PUB_APRV_FLOWINFO.ID
  is '唯一主键ID';
comment on column ZS18.HT_PUB_APRV_FLOWINFO.TBR_ID
  is '填报人id';
comment on column ZS18.HT_PUB_APRV_FLOWINFO.TBR_NAME
  is '填报人name';
comment on column ZS18.HT_PUB_APRV_FLOWINFO.TB_BM_ID
  is '填报部门id';
comment on column ZS18.HT_PUB_APRV_FLOWINFO.TB_BM_NAME
  is '填报部门name';
comment on column ZS18.HT_PUB_APRV_FLOWINFO.TB_DATE
  is '申请时间，创建日期';
comment on column ZS18.HT_PUB_APRV_FLOWINFO.MODULENAME
  is '审批类型编码';
comment on column ZS18.HT_PUB_APRV_FLOWINFO.URL
  is '单独登录url';
comment on column ZS18.HT_PUB_APRV_FLOWINFO.BUSIN_ID
  is '业务数据id';
comment on column ZS18.HT_PUB_APRV_FLOWINFO.STATE
  is '流程状态0办理中 ２己办结 １被驳回';
comment on column ZS18.HT_PUB_APRV_FLOWINFO.TB_ZT
  is '标题';
comment on column ZS18.HT_PUB_APRV_FLOWINFO.REMARK
  is '备注';
alter table ZS18.HT_PUB_APRV_FLOWINFO
  add constraint PK_HT_PUB_APRV_INFO primary key (ID)
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
prompt Creating table HT_PUB_APRV_MODEL
prompt ================================
prompt
create table ZS18.HT_PUB_APRV_MODEL
(
  PZ_TYPE   VARCHAR2(2) not null,
  INDEX_NO  INTEGER not null,
  ROLE      VARCHAR2(50),
  FLOW_NAME VARCHAR2(50)
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
comment on column ZS18.HT_PUB_APRV_MODEL.PZ_TYPE
  is '审批类型编码';
comment on column ZS18.HT_PUB_APRV_MODEL.INDEX_NO
  is '顺序号';
comment on column ZS18.HT_PUB_APRV_MODEL.ROLE
  is '审批角色';
comment on column ZS18.HT_PUB_APRV_MODEL.FLOW_NAME
  is '发送环节';
alter table ZS18.HT_PUB_APRV_MODEL
  add constraint PK_APRV primary key (PZ_TYPE, INDEX_NO)
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
prompt Creating table HT_PUB_APRV_OPINION
prompt ==================================
prompt
create table ZS18.HT_PUB_APRV_OPINION
(
  ID          INTEGER not null,
  GONGWEN_ID  VARCHAR2(64),
  USERID      VARCHAR2(32),
  USERNAME    VARCHAR2(20),
  COMMENTS    VARCHAR2(500),
  ROLENAME    VARCHAR2(50),
  OPINIONTIME VARCHAR2(19),
  STATUS      VARCHAR2(1) default 0,
  POS         VARCHAR2(2),
  WORKITEMID  VARCHAR2(50),
  ISENABLE    VARCHAR2(1) default 0
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
comment on table ZS18.HT_PUB_APRV_OPINION
  is '审批流程意见表';
comment on column ZS18.HT_PUB_APRV_OPINION.ID
  is '唯一标示';
comment on column ZS18.HT_PUB_APRV_OPINION.GONGWEN_ID
  is '业务流程主表id';
comment on column ZS18.HT_PUB_APRV_OPINION.USERID
  is '用户id';
comment on column ZS18.HT_PUB_APRV_OPINION.USERNAME
  is '用户名';
comment on column ZS18.HT_PUB_APRV_OPINION.COMMENTS
  is '意见内容';
comment on column ZS18.HT_PUB_APRV_OPINION.ROLENAME
  is '角色';
comment on column ZS18.HT_PUB_APRV_OPINION.OPINIONTIME
  is '意见填写日期';
comment on column ZS18.HT_PUB_APRV_OPINION.STATUS
  is '状态 0未审批1未通过2通过';
comment on column ZS18.HT_PUB_APRV_OPINION.POS
  is '排序';
comment on column ZS18.HT_PUB_APRV_OPINION.WORKITEMID
  is '发送环节';
comment on column ZS18.HT_PUB_APRV_OPINION.ISENABLE
  is '当前环节能否被处理，被角色看到，在上一环节完成后置为1';
alter table ZS18.HT_PUB_APRV_OPINION
  add constraint PK_HT_PUB_APRV_OPINION primary key (ID)
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
alter table ZS18.HT_PUB_APRV_OPINION
  add constraint PK_APRV_UNIQ unique (GONGWEN_ID, POS)
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
prompt Creating table HT_PUB_APRV_TYPE
prompt ===============================
prompt
create table ZS18.HT_PUB_APRV_TYPE
(
  PZ_TYPE      VARCHAR2(2),
  PZ_TYPE_NAME VARCHAR2(50),
  APRV_TABLE   VARCHAR2(60),
  APRV_TABSEG  VARCHAR2(60),
  BUZ_ID       VARCHAR2(60),
  PLSQL        CLOB
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
comment on table ZS18.HT_PUB_APRV_TYPE
  is '单据编号顺序表';
comment on column ZS18.HT_PUB_APRV_TYPE.PZ_TYPE
  is '审批类型编码';
comment on column ZS18.HT_PUB_APRV_TYPE.PZ_TYPE_NAME
  is '审批类型名称';
comment on column ZS18.HT_PUB_APRV_TYPE.APRV_TABLE
  is '业务对应数据表名';
comment on column ZS18.HT_PUB_APRV_TYPE.APRV_TABSEG
  is '业务对应数据表审批字段名';
comment on column ZS18.HT_PUB_APRV_TYPE.BUZ_ID
  is '业务主ID字段名';
comment on column ZS18.HT_PUB_APRV_TYPE.PLSQL
  is '业务明细查询SQL语言';

prompt
prompt Creating table HT_PUB_INSPECT_PROCESS
prompt =====================================
prompt
create table ZS18.HT_PUB_INSPECT_PROCESS
(
  ID           INTEGER,
  PROCESS_CODE VARCHAR2(32) not null,
  PROCESS_NAME VARCHAR2(256),
  IS_VALID     VARCHAR2(1) default 0,
  IS_DEL       VARCHAR2(1) default 0,
  REMARK       VARCHAR2(256),
  CREATE_ID    VARCHAR2(32),
  CREATE_TIME  DATE,
  MODIFY_ID    VARCHAR2(32),
  MODIFY_TIME  DATE
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
comment on column ZS18.HT_PUB_INSPECT_PROCESS.ID
  is '唯一标识';
comment on column ZS18.HT_PUB_INSPECT_PROCESS.PROCESS_CODE
  is '工序编码组织机构1位+生产线2位+工艺段2位+2位流水号';
comment on column ZS18.HT_PUB_INSPECT_PROCESS.PROCESS_NAME
  is '名称';
comment on column ZS18.HT_PUB_INSPECT_PROCESS.REMARK
  is '备注';
comment on column ZS18.HT_PUB_INSPECT_PROCESS.CREATE_ID
  is '创建人标识';
comment on column ZS18.HT_PUB_INSPECT_PROCESS.CREATE_TIME
  is '创建时间';
comment on column ZS18.HT_PUB_INSPECT_PROCESS.MODIFY_ID
  is '修改人标识';
comment on column ZS18.HT_PUB_INSPECT_PROCESS.MODIFY_TIME
  is '修改时间';
alter table ZS18.HT_PUB_INSPECT_PROCESS
  add constraint PROCESS_CODE primary key (PROCESS_CODE)
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
prompt Creating table HT_PUB_MATERIEL
prompt ==============================
prompt
create table ZS18.HT_PUB_MATERIEL
(
  ID               INTEGER,
  MATERIAL_CODE    VARCHAR2(32) not null,
  MATERIAL_NAME    VARCHAR2(256),
  TYPE_CODE        VARCHAR2(32),
  SPEC_VAL         VARCHAR2(256),
  MODEL_VAL        VARCHAR2(256),
  IS_VALID         VARCHAR2(1) default 1,
  IS_DEL           VARCHAR2(1) default 0,
  TYPE_FLAG        VARCHAR2(32),
  UNIT_CODE        VARCHAR2(30),
  DATA_ORIGIN_FLAG VARCHAR2(4) default 0,
  PK_MATERIAL      VARCHAR2(20),
  FACTORY          VARCHAR2(32),
  MAT_YEAR         VARCHAR2(32),
  MAT_CATEGORY     VARCHAR2(512),
  MAT_TYPE         VARCHAR2(512),
  MAT_LEVEL        VARCHAR2(512),
  MAT_VARIETY      VARCHAR2(512),
  MAT_PACK         VARCHAR2(512),
  MAT_PLACE        VARCHAR2(512),
  REMARK           VARCHAR2(512),
  MAT_TYPE2        VARCHAR2(512),
  MAT_PLACE_NAME   VARCHAR2(512),
  MAT_PROVINCE     VARCHAR2(512),
  MAT_CITY         VARCHAR2(512),
  PK_MARBASCLASS   VARCHAR2(200),
  LAST_UPDATE_TIME VARCHAR2(24),
  COSTPRICE        NUMBER(20,8),
  XY_MATERIAL_CODE VARCHAR2(32),
  PK_MATTAXES      VARCHAR2(32),
  NORIGTAXPRICE    NUMBER(20,8),
  PIECE_WEIGHT     VARCHAR2(10)
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
comment on table ZS18.HT_PUB_MATERIEL
  is '物料分类及信息表';
comment on column ZS18.HT_PUB_MATERIEL.ID
  is '物料分类id';
comment on column ZS18.HT_PUB_MATERIEL.MATERIAL_CODE
  is '物料分类编码';
comment on column ZS18.HT_PUB_MATERIEL.MATERIAL_NAME
  is '物料分类名称';
comment on column ZS18.HT_PUB_MATERIEL.TYPE_CODE
  is '父类编码';
comment on column ZS18.HT_PUB_MATERIEL.SPEC_VAL
  is '规格';
comment on column ZS18.HT_PUB_MATERIEL.MODEL_VAL
  is '型号';
comment on column ZS18.HT_PUB_MATERIEL.IS_DEL
  is '是否删除';
comment on column ZS18.HT_PUB_MATERIEL.TYPE_FLAG
  is '物料分类标识(YL-原料  FL- 辅料  BJ-备品备件  XL-香精香料)';
comment on column ZS18.HT_PUB_MATERIEL.UNIT_CODE
  is '计量单位';
comment on column ZS18.HT_PUB_MATERIEL.DATA_ORIGIN_FLAG
  is '数据来源标识0未选1库存商品库2烟厂原料库3鑫源原料库4烟厂免费原料库5鑫源免费原料库';
comment on column ZS18.HT_PUB_MATERIEL.PK_MATERIAL
  is '外部系统主键标识';
comment on column ZS18.HT_PUB_MATERIEL.FACTORY
  is '工厂';
comment on column ZS18.HT_PUB_MATERIEL.MAT_YEAR
  is '年份';
comment on column ZS18.HT_PUB_MATERIEL.MAT_CATEGORY
  is '类别';
comment on column ZS18.HT_PUB_MATERIEL.MAT_TYPE
  is '类型';
comment on column ZS18.HT_PUB_MATERIEL.MAT_LEVEL
  is '等级';
comment on column ZS18.HT_PUB_MATERIEL.MAT_VARIETY
  is '品种';
comment on column ZS18.HT_PUB_MATERIEL.MAT_PACK
  is '单重';
comment on column ZS18.HT_PUB_MATERIEL.MAT_PLACE
  is '产地';
comment on column ZS18.HT_PUB_MATERIEL.REMARK
  is '备注';
comment on column ZS18.HT_PUB_MATERIEL.MAT_TYPE2
  is '类别2(烤烟、晒黄烟)';
comment on column ZS18.HT_PUB_MATERIEL.MAT_PLACE_NAME
  is '产地名称';
comment on column ZS18.HT_PUB_MATERIEL.MAT_PROVINCE
  is '省份';
comment on column ZS18.HT_PUB_MATERIEL.MAT_CITY
  is '城市';
comment on column ZS18.HT_PUB_MATERIEL.PK_MARBASCLASS
  is '基本分类编码';
comment on column ZS18.HT_PUB_MATERIEL.COSTPRICE
  is '成本价';
comment on column ZS18.HT_PUB_MATERIEL.PK_MATTAXES
  is '税';
comment on column ZS18.HT_PUB_MATERIEL.NORIGTAXPRICE
  is '原值';
alter table ZS18.HT_PUB_MATERIEL
  add constraint MATERIAL_CODE primary key (MATERIAL_CODE)
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
prompt Creating table HT_PUB_MATTREE
prompt =============================
prompt
create table ZS18.HT_PUB_MATTREE
(
  ID               INTEGER,
  MATTREE_CODE     VARCHAR2(9) not null,
  MATTREE_NAME     VARCHAR2(256) not null,
  IS_VALID         VARCHAR2(1) default 1,
  IS_DEL           VARCHAR2(1) default 0,
  PK_CLASS         VARCHAR2(256),
  PK_PARENT_CLASS  VARCHAR2(256),
  DATA_ORIGIN_FLAG VARCHAR2(4) default 1,
  PARENT_CODE      VARCHAR2(9)
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
comment on table ZS18.HT_PUB_MATTREE
  is '物料分类信息表';
comment on column ZS18.HT_PUB_MATTREE.ID
  is '物料分类id';
comment on column ZS18.HT_PUB_MATTREE.MATTREE_CODE
  is '物料分类编码';
comment on column ZS18.HT_PUB_MATTREE.MATTREE_NAME
  is '物料分类名称';
comment on column ZS18.HT_PUB_MATTREE.IS_VALID
  is '是否有效';
comment on column ZS18.HT_PUB_MATTREE.IS_DEL
  is '是否删除';
comment on column ZS18.HT_PUB_MATTREE.PK_CLASS
  is '分类标识';
comment on column ZS18.HT_PUB_MATTREE.PK_PARENT_CLASS
  is '父级分类标识';
comment on column ZS18.HT_PUB_MATTREE.PARENT_CODE
  is '父级分类编码';
alter table ZS18.HT_PUB_MATTREE
  add constraint MATTREE_CODE primary key (MATTREE_CODE)
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
prompt Creating table HT_PUB_PATH_NODE
prompt ===============================
prompt
create table ZS18.HT_PUB_PATH_NODE
(
  ID           VARCHAR2(3) not null,
  SECTION_CODE VARCHAR2(10),
  NODENAME     VARCHAR2(30),
  ORDERS       VARCHAR2(2),
  DESCRIPT     VARCHAR2(255),
  IS_DEL       VARCHAR2(1) default 0,
  CREATE_TIME  VARCHAR2(19),
  TAG          VARCHAR2(60)
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
comment on column ZS18.HT_PUB_PATH_NODE.ID
  is '节点ID';
comment on column ZS18.HT_PUB_PATH_NODE.SECTION_CODE
  is '所属工艺段编码';
comment on column ZS18.HT_PUB_PATH_NODE.NODENAME
  is '节点名';
comment on column ZS18.HT_PUB_PATH_NODE.ORDERS
  is '顺序号';
comment on column ZS18.HT_PUB_PATH_NODE.DESCRIPT
  is '描述';
comment on column ZS18.HT_PUB_PATH_NODE.CREATE_TIME
  is '创建时间';
comment on column ZS18.HT_PUB_PATH_NODE.TAG
  is '节点对应控制标签';

prompt
prompt Creating table HT_PUB_PATH_PLAN
prompt ===============================
prompt
create table ZS18.HT_PUB_PATH_PLAN
(
  SECTION_CODE VARCHAR2(10) not null,
  PATHCODE     VARCHAR2(30) not null,
  PATHNAME     VARCHAR2(60),
  DESCRIPT     VARCHAR2(255),
  IS_DEL       VARCHAR2(1) default 0,
  CREATE_TIME  VARCHAR2(19),
  PROD_PLAN    VARCHAR2(30) not null
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
comment on column ZS18.HT_PUB_PATH_PLAN.SECTION_CODE
  is '工艺段';
comment on column ZS18.HT_PUB_PATH_PLAN.PATHCODE
  is '工艺段路径码';
comment on column ZS18.HT_PUB_PATH_PLAN.PATHNAME
  is '路径名';
comment on column ZS18.HT_PUB_PATH_PLAN.DESCRIPT
  is '描述';
comment on column ZS18.HT_PUB_PATH_PLAN.CREATE_TIME
  is '创建时间';
comment on column ZS18.HT_PUB_PATH_PLAN.PROD_PLAN
  is '计划号';
alter table ZS18.HT_PUB_PATH_PLAN
  add constraint PK_PLANPATHCODE primary key (SECTION_CODE, PROD_PLAN)
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
prompt Creating table HT_PUB_PATH_SECTION
prompt ==================================
prompt
create table ZS18.HT_PUB_PATH_SECTION
(
  SECTION_CODE VARCHAR2(10) not null,
  PATHCODE     VARCHAR2(30) not null,
  PATHNAME     VARCHAR2(60),
  DESCRIPT     VARCHAR2(255),
  IS_DEL       VARCHAR2(1) default 0,
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
comment on column ZS18.HT_PUB_PATH_SECTION.SECTION_CODE
  is '工艺段';
comment on column ZS18.HT_PUB_PATH_SECTION.PATHCODE
  is '工艺段路径码';
comment on column ZS18.HT_PUB_PATH_SECTION.PATHNAME
  is '路径名';
comment on column ZS18.HT_PUB_PATH_SECTION.DESCRIPT
  is '描述';
comment on column ZS18.HT_PUB_PATH_SECTION.CREATE_TIME
  is '创建时间';
alter table ZS18.HT_PUB_PATH_SECTION
  add constraint PK_PATHCODE primary key (PATHCODE, SECTION_CODE)
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
  CREATE_TIME        DATE,
  MODIFY_ID          VARCHAR2(32),
  MODIFY_TIME        DATE,
  STANDARD_VALUE     VARCHAR2(32),
  XY_PROD_CODE       VARCHAR2(32),
  QLT_CODE           VARCHAR2(500),
  B_FLOW_STATUS      VARCHAR2(2) default -1
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
alter table ZS18.HT_PUB_PROD_DESIGN
  add constraint PROD_CODE primary key (PROD_CODE)
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
prompt Creating table HT_PUB_TECH_PARA
prompt ===============================
prompt
create table ZS18.HT_PUB_TECH_PARA
(
  INSPECT_ITEM_CODE VARCHAR2(32),
  PARA_CODE         VARCHAR2(32) not null,
  PARA_NAME         VARCHAR2(256),
  PARA_UNIT         VARCHAR2(32),
  IS_VALID          VARCHAR2(1) default 1,
  IS_DEL            VARCHAR2(1) default 0,
  REMARK            VARCHAR2(256),
  CREATE_ID         VARCHAR2(32),
  CREATE_TIME       VARCHAR2(19),
  MODIFY_ID         VARCHAR2(32),
  MODIFY_TIME       VARCHAR2(19),
  PARA_TYPE         VARCHAR2(10),
  EQUIP_CODE        VARCHAR2(32),
  SET_TAG           VARCHAR2(100),
  VALUE_TAG         VARCHAR2(100),
  IS_KEY            VARCHAR2(1) default 0
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
comment on column ZS18.HT_PUB_TECH_PARA.PARA_CODE
  is '工艺参数编码a)	编码规则：组织机构1位+生产线2位+工艺段2位+工序2位+3位流水号';
comment on column ZS18.HT_PUB_TECH_PARA.PARA_NAME
  is '工艺参数名称';
comment on column ZS18.HT_PUB_TECH_PARA.PARA_UNIT
  is '单位';
comment on column ZS18.HT_PUB_TECH_PARA.REMARK
  is '备注';
comment on column ZS18.HT_PUB_TECH_PARA.CREATE_ID
  is '创建人标识';
comment on column ZS18.HT_PUB_TECH_PARA.CREATE_TIME
  is '创建时间';
comment on column ZS18.HT_PUB_TECH_PARA.MODIFY_ID
  is '修改人标识';
comment on column ZS18.HT_PUB_TECH_PARA.MODIFY_TIME
  is '修改时间';
comment on column ZS18.HT_PUB_TECH_PARA.PARA_TYPE
  is '类型';
comment on column ZS18.HT_PUB_TECH_PARA.EQUIP_CODE
  is '所属设备';
comment on column ZS18.HT_PUB_TECH_PARA.SET_TAG
  is '设定标签';
comment on column ZS18.HT_PUB_TECH_PARA.VALUE_TAG
  is '反馈标签';
comment on column ZS18.HT_PUB_TECH_PARA.IS_KEY
  is '在设备参数中标识是否为主要参数';
alter table ZS18.HT_PUB_TECH_PARA
  add constraint PARA_CODE primary key (PARA_CODE)
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
prompt Creating table HT_PUB_TECH_SECTION
prompt ==================================
prompt
create table ZS18.HT_PUB_TECH_SECTION
(
  ID           INTEGER,
  SECTION_CODE VARCHAR2(10) not null,
  SECTION_NAME VARCHAR2(128),
  IS_VALID     VARCHAR2(1) default 0,
  IS_DEL       VARCHAR2(1) default 0,
  REMARK       VARCHAR2(256),
  CREATE_ID    VARCHAR2(12),
  CREATE_TIME  DATE,
  MODIFY_ID    VARCHAR2(12),
  MODIFY_TIME  DATE
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
comment on column ZS18.HT_PUB_TECH_SECTION.ID
  is '唯一标识';
comment on column ZS18.HT_PUB_TECH_SECTION.SECTION_CODE
  is '工艺段编码';
comment on column ZS18.HT_PUB_TECH_SECTION.SECTION_NAME
  is '名称';
comment on column ZS18.HT_PUB_TECH_SECTION.IS_VALID
  is '是否有效';
comment on column ZS18.HT_PUB_TECH_SECTION.IS_DEL
  is '删除';
comment on column ZS18.HT_PUB_TECH_SECTION.REMARK
  is '备注';
comment on column ZS18.HT_PUB_TECH_SECTION.CREATE_ID
  is '创建人标识';
comment on column ZS18.HT_PUB_TECH_SECTION.CREATE_TIME
  is '创建时间';
comment on column ZS18.HT_PUB_TECH_SECTION.MODIFY_ID
  is '修改人标识';
comment on column ZS18.HT_PUB_TECH_SECTION.MODIFY_TIME
  is '修改时间';
alter table ZS18.HT_PUB_TECH_SECTION
  add constraint SECTION_CODE primary key (SECTION_CODE)
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
prompt Creating table HT_QA_AUX_FORMULA
prompt ================================
prompt
create table ZS18.HT_QA_AUX_FORMULA
(
  ID             INTEGER,
  FORMULA_NAME   VARCHAR2(256),
  FORMULA_CODE   VARCHAR2(32),
  PROD_CODE      VARCHAR2(32),
  STANDARD_VOL   VARCHAR2(32),
  B_DATE         VARCHAR2(10),
  E_DATE         VARCHAR2(10),
  CONTROL_STATUS VARCHAR2(16),
  CREATE_ID      VARCHAR2(32),
  CREATE_DATE    VARCHAR2(10),
  CREATE_DEPT_ID VARCHAR2(32),
  MODIFY_ID      VARCHAR2(32),
  MODIFY_TIME    VARCHAR2(19),
  IS_VALID       VARCHAR2(1),
  IS_DEL         VARCHAR2(1) default 0,
  REMARK         VARCHAR2(256),
  FLOW_STATUS    VARCHAR2(2) default -1
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
comment on table ZS18.HT_QA_AUX_FORMULA
  is '辅料配方信息主表';
comment on column ZS18.HT_QA_AUX_FORMULA.ID
  is '辅料配方信息表id';
comment on column ZS18.HT_QA_AUX_FORMULA.FORMULA_NAME
  is '配方名称';
comment on column ZS18.HT_QA_AUX_FORMULA.FORMULA_CODE
  is '配方编号';
comment on column ZS18.HT_QA_AUX_FORMULA.PROD_CODE
  is '产品编号';
comment on column ZS18.HT_QA_AUX_FORMULA.STANDARD_VOL
  is '标准版本号';
comment on column ZS18.HT_QA_AUX_FORMULA.B_DATE
  is '执行日期';
comment on column ZS18.HT_QA_AUX_FORMULA.E_DATE
  is '结束日期';
comment on column ZS18.HT_QA_AUX_FORMULA.CONTROL_STATUS
  is '受控状态';
comment on column ZS18.HT_QA_AUX_FORMULA.CREATE_ID
  is '编制人id';
comment on column ZS18.HT_QA_AUX_FORMULA.CREATE_DATE
  is '编制日期';
comment on column ZS18.HT_QA_AUX_FORMULA.CREATE_DEPT_ID
  is '编制部门';
comment on column ZS18.HT_QA_AUX_FORMULA.MODIFY_ID
  is '修改人id';
comment on column ZS18.HT_QA_AUX_FORMULA.MODIFY_TIME
  is '修改时间';
comment on column ZS18.HT_QA_AUX_FORMULA.IS_VALID
  is '是否有效';
comment on column ZS18.HT_QA_AUX_FORMULA.IS_DEL
  is '是否删除';
comment on column ZS18.HT_QA_AUX_FORMULA.REMARK
  is '备注';
comment on column ZS18.HT_QA_AUX_FORMULA.FLOW_STATUS
  is '流程状态';

prompt
prompt Creating table HT_QA_AUX_FORMULA_DETAIL
prompt =======================================
prompt
create table ZS18.HT_QA_AUX_FORMULA_DETAIL
(
  ID           INTEGER,
  FORMULA_CODE VARCHAR2(32),
  MATER_CODE   VARCHAR2(32),
  MATER_TYPE   VARCHAR2(32),
  AUX_SORT     NUMBER(10,2),
  IS_DEL       VARCHAR2(1) default 0,
  AUX_SCALE    NUMBER(10,2),
  REMARK       VARCHAR2(1024),
  AUX_PERCENT  NUMBER(10,2)
)
tablespace ZS_DATA
  pctfree 10
  initrans 1
  maxtrans 255;
comment on table ZS18.HT_QA_AUX_FORMULA_DETAIL
  is '辅料配方信息子表';
comment on column ZS18.HT_QA_AUX_FORMULA_DETAIL.ID
  is '辅料配方信息子表id';
comment on column ZS18.HT_QA_AUX_FORMULA_DETAIL.FORMULA_CODE
  is '辅料配方主表id';
comment on column ZS18.HT_QA_AUX_FORMULA_DETAIL.MATER_CODE
  is '物料编号';
comment on column ZS18.HT_QA_AUX_FORMULA_DETAIL.MATER_TYPE
  is '物料类型编码';
comment on column ZS18.HT_QA_AUX_FORMULA_DETAIL.AUX_SORT
  is '序号';
comment on column ZS18.HT_QA_AUX_FORMULA_DETAIL.IS_DEL
  is '是否删除';
comment on column ZS18.HT_QA_AUX_FORMULA_DETAIL.AUX_SCALE
  is '比例';
comment on column ZS18.HT_QA_AUX_FORMULA_DETAIL.REMARK
  is '备注';

prompt
prompt Creating table HT_QA_COAT_FORMULA
prompt =================================
prompt
create table ZS18.HT_QA_COAT_FORMULA
(
  ID             INTEGER,
  FORMULA_NAME   VARCHAR2(256),
  FORMULA_CODE   VARCHAR2(32) not null,
  PROD_CODE      VARCHAR2(32),
  STANDARD_VOL   VARCHAR2(32),
  B_DATE         VARCHAR2(10),
  E_DATE         VARCHAR2(10),
  CONTROL_STATUS VARCHAR2(16),
  CREATE_ID      VARCHAR2(32),
  CREATE_DATE    VARCHAR2(19),
  MODIFY_ID      VARCHAR2(32),
  MODIFY_TIME    VARCHAR2(19),
  CREATE_DEPT_ID VARCHAR2(32),
  IS_VALID       VARCHAR2(1) default 1,
  IS_DEL         VARCHAR2(1) default 0,
  REMARK         VARCHAR2(256),
  FORMULA_XJ     NUMBER(10,2),
  W_TOTAL        NUMBER(10,2),
  FORMULA_TPY    NUMBER(10,2),
  FLOW_STATUS    VARCHAR2(2) default -1
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
comment on table ZS18.HT_QA_COAT_FORMULA
  is '涂布液配方信息主表';
comment on column ZS18.HT_QA_COAT_FORMULA.ID
  is '涂布液配方信息表id';
comment on column ZS18.HT_QA_COAT_FORMULA.FORMULA_NAME
  is '配方名称';
comment on column ZS18.HT_QA_COAT_FORMULA.FORMULA_CODE
  is '配方编号';
comment on column ZS18.HT_QA_COAT_FORMULA.PROD_CODE
  is '产品编号';
comment on column ZS18.HT_QA_COAT_FORMULA.STANDARD_VOL
  is '标准版本号';
comment on column ZS18.HT_QA_COAT_FORMULA.B_DATE
  is '执行日期';
comment on column ZS18.HT_QA_COAT_FORMULA.E_DATE
  is '结束日期';
comment on column ZS18.HT_QA_COAT_FORMULA.CONTROL_STATUS
  is '受控状态';
comment on column ZS18.HT_QA_COAT_FORMULA.CREATE_ID
  is '编制人id';
comment on column ZS18.HT_QA_COAT_FORMULA.CREATE_DATE
  is '编制日期';
comment on column ZS18.HT_QA_COAT_FORMULA.MODIFY_ID
  is '修改人id';
comment on column ZS18.HT_QA_COAT_FORMULA.MODIFY_TIME
  is '修改时间';
comment on column ZS18.HT_QA_COAT_FORMULA.CREATE_DEPT_ID
  is '编制部门';
comment on column ZS18.HT_QA_COAT_FORMULA.IS_VALID
  is '是否有效';
comment on column ZS18.HT_QA_COAT_FORMULA.IS_DEL
  is '是否删除';
comment on column ZS18.HT_QA_COAT_FORMULA.REMARK
  is '备注';
comment on column ZS18.HT_QA_COAT_FORMULA.FORMULA_XJ
  is '香精比例';
comment on column ZS18.HT_QA_COAT_FORMULA.W_TOTAL
  is '香精总重';
comment on column ZS18.HT_QA_COAT_FORMULA.FORMULA_TPY
  is '涂布液比例';
comment on column ZS18.HT_QA_COAT_FORMULA.FLOW_STATUS
  is '审批状态';
alter table ZS18.HT_QA_COAT_FORMULA
  add constraint PK_HT_QA_COAT_FORMULA primary key (FORMULA_CODE)
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
prompt Creating table HT_QA_COAT_FORMULA_DETAIL
prompt ========================================
prompt
create table ZS18.HT_QA_COAT_FORMULA_DETAIL
(
  ID           INTEGER,
  FORMULA_CODE VARCHAR2(10),
  CLASS_NAME   VARCHAR2(256),
  COAT_SCALE   VARCHAR2(16),
  NEED_SIZE    NUMBER(10,2),
  COAT_SORT    NUMBER(10,2),
  IS_VALID     VARCHAR2(1) default 1,
  IS_DEL       VARCHAR2(1) default 0,
  REMARK       VARCHAR2(1024),
  COAT_FLAG    VARCHAR2(16),
  MATER_CODE   VARCHAR2(32)
)
tablespace ZS_DATA
  pctfree 10
  initrans 1
  maxtrans 255;
comment on table ZS18.HT_QA_COAT_FORMULA_DETAIL
  is '涂布液配方信息子表';
comment on column ZS18.HT_QA_COAT_FORMULA_DETAIL.ID
  is '涂布液配方信息子表id';
comment on column ZS18.HT_QA_COAT_FORMULA_DETAIL.FORMULA_CODE
  is '涂布液配方主表id';
comment on column ZS18.HT_QA_COAT_FORMULA_DETAIL.CLASS_NAME
  is '分组名称（种类）';
comment on column ZS18.HT_QA_COAT_FORMULA_DETAIL.COAT_SCALE
  is '比例';
comment on column ZS18.HT_QA_COAT_FORMULA_DETAIL.NEED_SIZE
  is '调配所需量';
comment on column ZS18.HT_QA_COAT_FORMULA_DETAIL.COAT_SORT
  is '序号';
comment on column ZS18.HT_QA_COAT_FORMULA_DETAIL.IS_VALID
  is '是否有效';
comment on column ZS18.HT_QA_COAT_FORMULA_DETAIL.IS_DEL
  is '是否删除';
comment on column ZS18.HT_QA_COAT_FORMULA_DETAIL.REMARK
  is '备注';
comment on column ZS18.HT_QA_COAT_FORMULA_DETAIL.COAT_FLAG
  is '配方分类(XJ--香精香料； TPY--涂布液配方)';
comment on column ZS18.HT_QA_COAT_FORMULA_DETAIL.MATER_CODE
  is '物料编号';

prompt
prompt Creating table HT_QA_MATER_FORMULA
prompt ==================================
prompt
create table ZS18.HT_QA_MATER_FORMULA
(
  ID             INTEGER,
  FORMULA_NAME   VARCHAR2(256),
  FORMULA_CODE   VARCHAR2(32) not null,
  PROD_CODE      VARCHAR2(32),
  STANDARD_VOL   VARCHAR2(32),
  B_DATE         VARCHAR2(10),
  E_DATE         VARCHAR2(10),
  CONTROL_STATUS VARCHAR2(1),
  CABO_SUM       NUMBER(10,2),
  PIECES_SUM     NUMBER(10,2),
  CREATE_ID      VARCHAR2(32),
  CREATE_DATE    VARCHAR2(10),
  CREATE_DEPT_ID VARCHAR2(32),
  MODIFY_ID      VARCHAR2(32),
  MODIFY_TIME    VARCHAR2(19),
  IS_VALID       VARCHAR2(1),
  IS_DEL         VARCHAR2(1) default 0,
  REMARK         VARCHAR2(1024),
  STEM_NUM       NUMBER(10,2),
  PIECE_NUM      NUMBER(10,2),
  SMALLS_NUM     NUMBER(10,2),
  STICKS_NUM     NUMBER(10,2),
  ADJUST         VARCHAR2(32),
  FLOW_STATUS    VARCHAR2(2) default -1,
  PZ_CODE        VARCHAR2(64)
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
comment on table ZS18.HT_QA_MATER_FORMULA
  is '原料配方信息主表';
comment on column ZS18.HT_QA_MATER_FORMULA.ID
  is '原料配方信息表id';
comment on column ZS18.HT_QA_MATER_FORMULA.FORMULA_NAME
  is '配方名称';
comment on column ZS18.HT_QA_MATER_FORMULA.FORMULA_CODE
  is '配方编号';
comment on column ZS18.HT_QA_MATER_FORMULA.PROD_CODE
  is '产品编号';
comment on column ZS18.HT_QA_MATER_FORMULA.STANDARD_VOL
  is '标准版本号';
comment on column ZS18.HT_QA_MATER_FORMULA.B_DATE
  is '执行日期';
comment on column ZS18.HT_QA_MATER_FORMULA.E_DATE
  is '结束日期';
comment on column ZS18.HT_QA_MATER_FORMULA.CONTROL_STATUS
  is '受控状态';
comment on column ZS18.HT_QA_MATER_FORMULA.CABO_SUM
  is '烟梗总量';
comment on column ZS18.HT_QA_MATER_FORMULA.PIECES_SUM
  is '水提碎片总量';
comment on column ZS18.HT_QA_MATER_FORMULA.CREATE_ID
  is '创建人id';
comment on column ZS18.HT_QA_MATER_FORMULA.CREATE_DATE
  is '编制日期';
comment on column ZS18.HT_QA_MATER_FORMULA.CREATE_DEPT_ID
  is '编制部门';
comment on column ZS18.HT_QA_MATER_FORMULA.MODIFY_ID
  is '修改人id';
comment on column ZS18.HT_QA_MATER_FORMULA.MODIFY_TIME
  is '修改时间';
comment on column ZS18.HT_QA_MATER_FORMULA.IS_VALID
  is '是否有效';
comment on column ZS18.HT_QA_MATER_FORMULA.IS_DEL
  is '是否删除';
comment on column ZS18.HT_QA_MATER_FORMULA.REMARK
  is '备注';
comment on column ZS18.HT_QA_MATER_FORMULA.STEM_NUM
  is '烟梗总计';
comment on column ZS18.HT_QA_MATER_FORMULA.PIECE_NUM
  is '碎片总计';
comment on column ZS18.HT_QA_MATER_FORMULA.SMALLS_NUM
  is '烟末总计';
comment on column ZS18.HT_QA_MATER_FORMULA.STICKS_NUM
  is '烟棒总计';
comment on column ZS18.HT_QA_MATER_FORMULA.ADJUST
  is '是否是调整配方单';
comment on column ZS18.HT_QA_MATER_FORMULA.FLOW_STATUS
  is '流程状态';
comment on column ZS18.HT_QA_MATER_FORMULA.PZ_CODE
  is '单据凭证';
alter table ZS18.HT_QA_MATER_FORMULA
  add constraint PK_T_QA_MATER_FORMULA primary key (FORMULA_CODE)
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
prompt Creating table HT_QA_MATER_FORMULA_DETAIL
prompt =========================================
prompt
create table ZS18.HT_QA_MATER_FORMULA_DETAIL
(
  ID           INTEGER,
  FORMULA_CODE INTEGER not null,
  MATER_CODE   VARCHAR2(32) not null,
  BATCH_SIZE   NUMBER(10,2),
  FRONT_GROUP  VARCHAR2(16),
  MATER_SORT   INTEGER,
  IS_DEL       VARCHAR2(1) default 0,
  REMARK       VARCHAR2(1024),
  MATER_FLAG   VARCHAR2(4)
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
comment on table ZS18.HT_QA_MATER_FORMULA_DETAIL
  is '原料配方信息子表';
comment on column ZS18.HT_QA_MATER_FORMULA_DETAIL.ID
  is '原料配方信息子表id';
comment on column ZS18.HT_QA_MATER_FORMULA_DETAIL.FORMULA_CODE
  is '原料配方主表编码';
comment on column ZS18.HT_QA_MATER_FORMULA_DETAIL.MATER_CODE
  is '物料编号';
comment on column ZS18.HT_QA_MATER_FORMULA_DETAIL.BATCH_SIZE
  is '批投料量';
comment on column ZS18.HT_QA_MATER_FORMULA_DETAIL.FRONT_GROUP
  is '优先组';
comment on column ZS18.HT_QA_MATER_FORMULA_DETAIL.MATER_SORT
  is '顺序号';
comment on column ZS18.HT_QA_MATER_FORMULA_DETAIL.IS_DEL
  is '是否删除';
comment on column ZS18.HT_QA_MATER_FORMULA_DETAIL.REMARK
  is '备注';
comment on column ZS18.HT_QA_MATER_FORMULA_DETAIL.MATER_FLAG
  is '配方物料分类（烟梗类YG、碎片类SP）';
alter table ZS18.HT_QA_MATER_FORMULA_DETAIL
  add constraint PK_MATER_CODE primary key (FORMULA_CODE, MATER_CODE)
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
prompt Creating table HT_QLT_AUTO_EVENT
prompt ================================
prompt
create table ZS18.HT_QLT_AUTO_EVENT
(
  PROD_CODE   VARCHAR2(20),
  PARA_CODE   VARCHAR2(100),
  SORT        VARCHAR2(12),
  DEAL        VARCHAR2(255),
  SCENE       VARCHAR2(255),
  TEAM        VARCHAR2(4),
  OTHERS      VARCHAR2(255),
  DONE        VARCHAR2(1) default 0,
  SCORE       FLOAT default 0,
  EVENT_TIME  VARCHAR2(19),
  ALTERS      VARCHAR2(255),
  CREATOR     VARCHAR2(19),
  CREATE_TIME VARCHAR2(19),
  VALUE       FLOAT
)
tablespace ZS_DATA
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 128K
    next 8K
    minextents 1
  );
comment on column ZS18.HT_QLT_AUTO_EVENT.PROD_CODE
  is '产品';
comment on column ZS18.HT_QLT_AUTO_EVENT.PARA_CODE
  is '工艺点编码';
comment on column ZS18.HT_QLT_AUTO_EVENT.SORT
  is '类型（均值、合格率。。。）';
comment on column ZS18.HT_QLT_AUTO_EVENT.DEAL
  is '处理方式';
comment on column ZS18.HT_QLT_AUTO_EVENT.SCENE
  is '原因';
comment on column ZS18.HT_QLT_AUTO_EVENT.TEAM
  is '班组';
comment on column ZS18.HT_QLT_AUTO_EVENT.OTHERS
  is '其它补充说明';
comment on column ZS18.HT_QLT_AUTO_EVENT.DONE
  is '是否己处理完成';
comment on column ZS18.HT_QLT_AUTO_EVENT.SCORE
  is '扣分';
comment on column ZS18.HT_QLT_AUTO_EVENT.EVENT_TIME
  is '事件时间';
comment on column ZS18.HT_QLT_AUTO_EVENT.CREATOR
  is '记录人员';
comment on column ZS18.HT_QLT_AUTO_EVENT.CREATE_TIME
  is '记录时间';
comment on column ZS18.HT_QLT_AUTO_EVENT.VALUE
  is '值';

prompt
prompt Creating table HT_QLT_COLLECTION
prompt ================================
prompt
create table ZS18.HT_QLT_COLLECTION
(
  PARA_CODE           VARCHAR2(11) not null,
  IS_VALID            VARCHAR2(1),
  SYNCHRO_TIME        CHAR(19),
  UNIT                VARCHAR2(70),
  PERIODIC            VARCHAR2(3),
  DESCRIPT            VARCHAR2(100),
  VARMONITOR_TAG      VARCHAR2(100),
  HEAD_DELAY          FLOAT default 0,
  TAIL_DELAY          FLOAT default 0,
  BATCH_HEAD_DELAY    FLOAT default 0,
  BATCH_TAIL_DELAY    FLOAT default 0,
  WEIGHT              FLOAT,
  GAP_DELAY           FLOAT,
  IS_DEL              VARCHAR2(1),
  CUTOFF_POINT_TAG    VARCHAR2(100),
  CUTOFF_RST          VARCHAR2(10),
  CUTOFF_RST_VALUE    FLOAT default 0,
  TAILLOGIC_TAG       VARCHAR2(100),
  TAILLOGIC_RST       VARCHAR2(10),
  TAILLOGIC_RST_VALUE FLOAT default 0,
  CUTOFF_TIMEGAP      INTEGER,
  PARA_TYPE           VARCHAR2(10)
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
comment on column ZS18.HT_QLT_COLLECTION.PARA_CODE
  is '参数编码';
comment on column ZS18.HT_QLT_COLLECTION.PERIODIC
  is '采集周期';
comment on column ZS18.HT_QLT_COLLECTION.DESCRIPT
  is '描述';
comment on column ZS18.HT_QLT_COLLECTION.VARMONITOR_TAG
  is '监控变量标签';
comment on column ZS18.HT_QLT_COLLECTION.HEAD_DELAY
  is '料头延时';
comment on column ZS18.HT_QLT_COLLECTION.TAIL_DELAY
  is '料尾延时';
comment on column ZS18.HT_QLT_COLLECTION.BATCH_HEAD_DELAY
  is '批头延时';
comment on column ZS18.HT_QLT_COLLECTION.BATCH_TAIL_DELAY
  is '批尾延时';
comment on column ZS18.HT_QLT_COLLECTION.WEIGHT
  is '权重';
comment on column ZS18.HT_QLT_COLLECTION.GAP_DELAY
  is '断流偏移';
comment on column ZS18.HT_QLT_COLLECTION.CUTOFF_POINT_TAG
  is '断流点标签';
comment on column ZS18.HT_QLT_COLLECTION.CUTOFF_RST
  is '断流判定浮号';
comment on column ZS18.HT_QLT_COLLECTION.CUTOFF_RST_VALUE
  is '断流判定值';
comment on column ZS18.HT_QLT_COLLECTION.TAILLOGIC_TAG
  is '批头批尾判定标签';
comment on column ZS18.HT_QLT_COLLECTION.TAILLOGIC_RST
  is '批头批尾判定浮号';
comment on column ZS18.HT_QLT_COLLECTION.TAILLOGIC_RST_VALUE
  is '判定值';
comment on column ZS18.HT_QLT_COLLECTION.CUTOFF_TIMEGAP
  is '断流判定时长';
comment on column ZS18.HT_QLT_COLLECTION.PARA_TYPE
  is '数点采集点类型，用以对比分析';

prompt
prompt Creating table HT_QLT_CUTOFF_RECORD
prompt ===================================
prompt
create table ZS18.HT_QLT_CUTOFF_RECORD
(
  PROD_CODE    VARCHAR2(20),
  SECTION_CODE VARCHAR2(100),
  DEAL         VARCHAR2(255),
  SCENE        VARCHAR2(255),
  TEAM         VARCHAR2(4),
  OTHERS       VARCHAR2(255),
  DONE         VARCHAR2(1) default 0,
  SCORE        FLOAT default 0,
  EVENT_TIME   VARCHAR2(19),
  ALTERS       VARCHAR2(255),
  CREATOR      VARCHAR2(19),
  CREATE_TIME  VARCHAR2(19),
  CUTOFF_TIME  INTEGER
)
tablespace ZS_DATA
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 128K
    next 8K
    minextents 1
  );
comment on column ZS18.HT_QLT_CUTOFF_RECORD.PROD_CODE
  is '产品';
comment on column ZS18.HT_QLT_CUTOFF_RECORD.SECTION_CODE
  is '工艺段编码';
comment on column ZS18.HT_QLT_CUTOFF_RECORD.DEAL
  is '处理方式';
comment on column ZS18.HT_QLT_CUTOFF_RECORD.SCENE
  is '原因';
comment on column ZS18.HT_QLT_CUTOFF_RECORD.TEAM
  is '班组';
comment on column ZS18.HT_QLT_CUTOFF_RECORD.OTHERS
  is '其它补充说明';
comment on column ZS18.HT_QLT_CUTOFF_RECORD.DONE
  is '是否己处理完成';
comment on column ZS18.HT_QLT_CUTOFF_RECORD.SCORE
  is '扣分';
comment on column ZS18.HT_QLT_CUTOFF_RECORD.EVENT_TIME
  is '事件时间';
comment on column ZS18.HT_QLT_CUTOFF_RECORD.CREATOR
  is '记录人员';
comment on column ZS18.HT_QLT_CUTOFF_RECORD.CREATE_TIME
  is '记录时间';
comment on column ZS18.HT_QLT_CUTOFF_RECORD.CUTOFF_TIME
  is '断流时间';

prompt
prompt Creating table HT_QLT_DATA_RECORD
prompt =================================
prompt
create table ZS18.HT_QLT_DATA_RECORD
(
  PLAN_ID   VARCHAR2(20),
  PARA_CODE VARCHAR2(5),
  AVG       FLOAT default 0,
  COUNT     FLOAT default 0,
  MIN       FLOAT default 0,
  MAX       FLOAT default 0,
  QUANUM    FLOAT default 0,
  QUARATE   FLOAT default 0,
  UPCOUNT   FLOAT default 0,
  UPRATE    FLOAT default 0,
  DOWNCOUNT FLOAT default 0,
  DOWNRATE  FLOAT default 0,
  STDDEV    FLOAT default 0,
  ABSDEV    FLOAT default 0,
  IS_GAP    VARCHAR2(4) default 0,
  CPK       FLOAT default 0,
  VAR       FLOAT default 0,
  RATE      FLOAT default 0,
  CA        FLOAT default 0,
  CP        FLOAT default 0,
  RANGE     FLOAT default 0,
  B_TIME    VARCHAR2(19),
  SHIFT     VARCHAR2(4),
  GAP_TIME  FLOAT default 0,
  E_TIME    VARCHAR2(19)
)
tablespace ZS_DATA
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 2M
    next 8K
    minextents 1
  );
comment on column ZS18.HT_QLT_DATA_RECORD.PLAN_ID
  is '生产任务ID';
comment on column ZS18.HT_QLT_DATA_RECORD.PARA_CODE
  is '工艺点编码';
comment on column ZS18.HT_QLT_DATA_RECORD.AVG
  is '均值';
comment on column ZS18.HT_QLT_DATA_RECORD.COUNT
  is '总点数';
comment on column ZS18.HT_QLT_DATA_RECORD.MIN
  is '最大值';
comment on column ZS18.HT_QLT_DATA_RECORD.MAX
  is '最小值';
comment on column ZS18.HT_QLT_DATA_RECORD.QUANUM
  is '合格点数';
comment on column ZS18.HT_QLT_DATA_RECORD.QUARATE
  is '合格率';
comment on column ZS18.HT_QLT_DATA_RECORD.UPCOUNT
  is '超上限数';
comment on column ZS18.HT_QLT_DATA_RECORD.UPRATE
  is '超上限率';
comment on column ZS18.HT_QLT_DATA_RECORD.DOWNCOUNT
  is '超下限数';
comment on column ZS18.HT_QLT_DATA_RECORD.DOWNRATE
  is '超下限率';
comment on column ZS18.HT_QLT_DATA_RECORD.STDDEV
  is '标准差';
comment on column ZS18.HT_QLT_DATA_RECORD.ABSDEV
  is '绝对差';
comment on column ZS18.HT_QLT_DATA_RECORD.IS_GAP
  is '是否断料';
comment on column ZS18.HT_QLT_DATA_RECORD.CPK
  is 'CPK';
comment on column ZS18.HT_QLT_DATA_RECORD.B_TIME
  is '开始时间';
comment on column ZS18.HT_QLT_DATA_RECORD.SHIFT
  is '班组';
comment on column ZS18.HT_QLT_DATA_RECORD.GAP_TIME
  is '断料时间';
comment on column ZS18.HT_QLT_DATA_RECORD.E_TIME
  is '结束时间';

prompt
prompt Creating table HT_QLT_INSPECT_EVENT
prompt ===================================
prompt
create table ZS18.HT_QLT_INSPECT_EVENT
(
  RECORD_ID    INTEGER not null,
  INSPECT_CODE VARCHAR2(100),
  PROD_CODE    VARCHAR2(20),
  TEAM         VARCHAR2(4),
  STATUS       VARCHAR2(5),
  REASON       VARCHAR2(255),
  SCENE        VARCHAR2(255),
  DEAL         VARCHAR2(255),
  REMARK       VARCHAR2(255),
  ISDONE       VARCHAR2(1) default 0,
  SCORE        FLOAT default 0,
  EVENT_TIME   VARCHAR2(19),
  CREATOR      VARCHAR2(19),
  CREATE_TIME  VARCHAR2(19),
  IS_DEL       VARCHAR2(1) default 0
)
tablespace ZS_DATA
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 128K
    next 8K
    minextents 1
  );
comment on column ZS18.HT_QLT_INSPECT_EVENT.RECORD_ID
  is '工艺检查记录ID';
comment on column ZS18.HT_QLT_INSPECT_EVENT.INSPECT_CODE
  is '工艺检查项目编码';
comment on column ZS18.HT_QLT_INSPECT_EVENT.PROD_CODE
  is '产品号';
comment on column ZS18.HT_QLT_INSPECT_EVENT.TEAM
  is '班组';
comment on column ZS18.HT_QLT_INSPECT_EVENT.STATUS
  is '状态';
comment on column ZS18.HT_QLT_INSPECT_EVENT.REASON
  is '原因分析';
comment on column ZS18.HT_QLT_INSPECT_EVENT.SCENE
  is '现场记录';
comment on column ZS18.HT_QLT_INSPECT_EVENT.DEAL
  is '处理方式';
comment on column ZS18.HT_QLT_INSPECT_EVENT.REMARK
  is '其它补充说明';
comment on column ZS18.HT_QLT_INSPECT_EVENT.ISDONE
  is '是否己处理完成';
comment on column ZS18.HT_QLT_INSPECT_EVENT.SCORE
  is '扣分';
comment on column ZS18.HT_QLT_INSPECT_EVENT.EVENT_TIME
  is '事件时间';
comment on column ZS18.HT_QLT_INSPECT_EVENT.CREATOR
  is '记录人员';
comment on column ZS18.HT_QLT_INSPECT_EVENT.CREATE_TIME
  is '记录时间';
alter table ZS18.HT_QLT_INSPECT_EVENT
  add constraint PK_EVENT_ID primary key (RECORD_ID)
  using index 
  tablespace ZS_DATA
  pctfree 10
  initrans 2
  maxtrans 255;

prompt
prompt Creating table HT_QLT_INSPECT_PROJ
prompt ==================================
prompt
create table ZS18.HT_QLT_INSPECT_PROJ
(
  INSPECT_CODE  VARCHAR2(32) not null,
  INSPECT_NAME  VARCHAR2(256),
  INSPECT_TYPE  VARCHAR2(1),
  INSPECT_GROUP VARCHAR2(5),
  UNIT          VARCHAR2(10),
  IS_VALID      VARCHAR2(1) default 1,
  IS_DEL        VARCHAR2(1) default 0,
  REMARK        VARCHAR2(256),
  CREATE_ID     VARCHAR2(32),
  CREATE_TIME   VARCHAR2(19),
  MODIFY_ID     VARCHAR2(32),
  MODIFY_TIME   VARCHAR2(19)
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
comment on table ZS18.HT_QLT_INSPECT_PROJ
  is '工艺检查项目表';
comment on column ZS18.HT_QLT_INSPECT_PROJ.INSPECT_CODE
  is '检验项目编码';
comment on column ZS18.HT_QLT_INSPECT_PROJ.INSPECT_NAME
  is '检验项目名称';
comment on column ZS18.HT_QLT_INSPECT_PROJ.INSPECT_TYPE
  is '检查类型';
comment on column ZS18.HT_QLT_INSPECT_PROJ.INSPECT_GROUP
  is '次级类型 当为过程检验时为工段名 为成品检验时为理化感官';
comment on column ZS18.HT_QLT_INSPECT_PROJ.UNIT
  is '单位';
comment on column ZS18.HT_QLT_INSPECT_PROJ.IS_VALID
  is '是否有效';
comment on column ZS18.HT_QLT_INSPECT_PROJ.IS_DEL
  is '是否删除';
comment on column ZS18.HT_QLT_INSPECT_PROJ.REMARK
  is '备注';
comment on column ZS18.HT_QLT_INSPECT_PROJ.CREATE_ID
  is '创建人id';
comment on column ZS18.HT_QLT_INSPECT_PROJ.CREATE_TIME
  is '创建时间';
comment on column ZS18.HT_QLT_INSPECT_PROJ.MODIFY_ID
  is '修改人id';
comment on column ZS18.HT_QLT_INSPECT_PROJ.MODIFY_TIME
  is '修改时间';
alter table ZS18.HT_QLT_INSPECT_PROJ
  add constraint PK_PROJ_CODE primary key (INSPECT_CODE)
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
prompt Creating table HT_QLT_INSPECT_RECORD
prompt ====================================
prompt
create table ZS18.HT_QLT_INSPECT_RECORD
(
  INSPECT_CODE  VARCHAR2(100),
  PROD_CODE     VARCHAR2(19),
  SHIFT_ID      VARCHAR2(255),
  TEAM_ID       VARCHAR2(4),
  INSPECT_VALUE FLOAT,
  RECORD_TIME   VARCHAR2(255),
  CREAT_ID      VARCHAR2(19),
  CREATE_TIME   VARCHAR2(19),
  REMARK        VARCHAR2(255),
  IS_DEL        VARCHAR2(1) default 0
)
tablespace ZS_DATA
  pctfree 10
  initrans 1
  maxtrans 255;
comment on column ZS18.HT_QLT_INSPECT_RECORD.INSPECT_CODE
  is '工艺检查编码';
comment on column ZS18.HT_QLT_INSPECT_RECORD.PROD_CODE
  is '产品';
comment on column ZS18.HT_QLT_INSPECT_RECORD.SHIFT_ID
  is '班时';
comment on column ZS18.HT_QLT_INSPECT_RECORD.TEAM_ID
  is '班组';
comment on column ZS18.HT_QLT_INSPECT_RECORD.INSPECT_VALUE
  is '检查值';
comment on column ZS18.HT_QLT_INSPECT_RECORD.RECORD_TIME
  is '评测时间';
comment on column ZS18.HT_QLT_INSPECT_RECORD.CREAT_ID
  is '记录人';
comment on column ZS18.HT_QLT_INSPECT_RECORD.CREATE_TIME
  is '记录时间';
comment on column ZS18.HT_QLT_INSPECT_RECORD.REMARK
  is '说明';

prompt
prompt Creating table HT_QLT_INSPECT_STDD
prompt ==================================
prompt
create table ZS18.HT_QLT_INSPECT_STDD
(
  INSPECT_CODE VARCHAR2(32) not null,
  UPPER_VALUE  FLOAT,
  LOWER_VALUE  FLOAT,
  MINUS_SCORE  INTEGER default 1,
  IS_DEL       VARCHAR2(1) default 0,
  REMARK       VARCHAR2(256),
  CREATE_ID    VARCHAR2(32),
  CREATE_TIME  VARCHAR2(19),
  PROD_CODE    VARCHAR2(12)
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
  is '工艺检查项目标准';
comment on column ZS18.HT_QLT_INSPECT_STDD.INSPECT_CODE
  is '检验项目编码';
comment on column ZS18.HT_QLT_INSPECT_STDD.UPPER_VALUE
  is '上限';
comment on column ZS18.HT_QLT_INSPECT_STDD.LOWER_VALUE
  is '下限';
comment on column ZS18.HT_QLT_INSPECT_STDD.MINUS_SCORE
  is '不符合怀况时单次扣分';
comment on column ZS18.HT_QLT_INSPECT_STDD.IS_DEL
  is '是否删除';
comment on column ZS18.HT_QLT_INSPECT_STDD.REMARK
  is '备注';
comment on column ZS18.HT_QLT_INSPECT_STDD.CREATE_ID
  is '创建人id';
comment on column ZS18.HT_QLT_INSPECT_STDD.CREATE_TIME
  is '创建时间';
comment on column ZS18.HT_QLT_INSPECT_STDD.PROD_CODE
  is '产品编码 确认是否需要每个产品单独设定';
alter table ZS18.HT_QLT_INSPECT_STDD
  add constraint PK_INSPECT_STD primary key (INSPECT_CODE)
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
prompt Creating table HT_QLT_STDD_CODE
prompt ===============================
prompt
create table ZS18.HT_QLT_STDD_CODE
(
  ID           INTEGER,
  QLT_CODE     VARCHAR2(32) not null,
  STANDARD_VOL VARCHAR2(32),
  B_DATE       DATE,
  E_DATE       DATE,
  CREATE_ID    VARCHAR2(32),
  CREATOR      VARCHAR2(32),
  CREATE_DATE  DATE,
  CREATE_DEPT  VARCHAR2(32),
  MODIFY_ID    VARCHAR2(32),
  MODIFY_TIME  DATE,
  IS_VALID     VARCHAR2(1) default 1,
  IS_DEL       VARCHAR2(1) default 0,
  REMARK       VARCHAR2(2048),
  QLT_NAME     VARCHAR2(256)
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
comment on table ZS18.HT_QLT_STDD_CODE
  is '质量考标准主表';
comment on column ZS18.HT_QLT_STDD_CODE.ID
  is '唯一标识';
comment on column ZS18.HT_QLT_STDD_CODE.QLT_CODE
  is '质量标准编码';
comment on column ZS18.HT_QLT_STDD_CODE.STANDARD_VOL
  is '标准版本号';
comment on column ZS18.HT_QLT_STDD_CODE.B_DATE
  is '执行日期';
comment on column ZS18.HT_QLT_STDD_CODE.E_DATE
  is '结束日期';
comment on column ZS18.HT_QLT_STDD_CODE.CREATE_ID
  is '创建人标识';
comment on column ZS18.HT_QLT_STDD_CODE.CREATOR
  is '编制人';
comment on column ZS18.HT_QLT_STDD_CODE.CREATE_DATE
  is '编制日期';
comment on column ZS18.HT_QLT_STDD_CODE.CREATE_DEPT
  is '编制部门';
comment on column ZS18.HT_QLT_STDD_CODE.MODIFY_ID
  is '修改人标识';
comment on column ZS18.HT_QLT_STDD_CODE.MODIFY_TIME
  is '修改时间';
comment on column ZS18.HT_QLT_STDD_CODE.IS_DEL
  is '删除标识';
comment on column ZS18.HT_QLT_STDD_CODE.REMARK
  is '备注';
comment on column ZS18.HT_QLT_STDD_CODE.QLT_NAME
  is '质量标准名';
alter table ZS18.HT_QLT_STDD_CODE
  add constraint PK_QLT_CODE primary key (QLT_CODE)
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
prompt Creating table HT_QLT_STDD_CODE_DETAIL
prompt ======================================
prompt
create table ZS18.HT_QLT_STDD_CODE_DETAIL
(
  ID        INTEGER,
  QLT_CODE  VARCHAR2(32) not null,
  PARA_CODE VARCHAR2(32) not null,
  QLT_TYPE  VARCHAR2(32) not null,
  IS_DEL    VARCHAR2(1) default 0,
  REMARK    VARCHAR2(1024),
  PARA_NAME VARCHAR2(50),
  VALUE     FLOAT,
  EER_DEV   VARCHAR2(50)
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
comment on table ZS18.HT_QLT_STDD_CODE_DETAIL
  is '质量标准从表';
comment on column ZS18.HT_QLT_STDD_CODE_DETAIL.ID
  is '唯一标识';
comment on column ZS18.HT_QLT_STDD_CODE_DETAIL.QLT_CODE
  is '质量标准主表ID';
comment on column ZS18.HT_QLT_STDD_CODE_DETAIL.PARA_CODE
  is '参数码';
comment on column ZS18.HT_QLT_STDD_CODE_DETAIL.QLT_TYPE
  is '考核类型';
comment on column ZS18.HT_QLT_STDD_CODE_DETAIL.IS_DEL
  is '删除标识';
comment on column ZS18.HT_QLT_STDD_CODE_DETAIL.REMARK
  is '备注';
comment on column ZS18.HT_QLT_STDD_CODE_DETAIL.PARA_NAME
  is '参数名';
comment on column ZS18.HT_QLT_STDD_CODE_DETAIL.VALUE
  is '标准值';
comment on column ZS18.HT_QLT_STDD_CODE_DETAIL.EER_DEV
  is '判断条件';
alter table ZS18.HT_QLT_STDD_CODE_DETAIL
  add constraint PARA_QLT primary key (PARA_CODE, QLT_CODE, QLT_TYPE)
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
prompt Creating table HT_STRG_AUX
prompt ==========================
prompt
create table ZS18.HT_STRG_AUX
(
  ID            VARCHAR2(64),
  OUT_DATE      VARCHAR2(10),
  ORDER_SN      VARCHAR2(64) not null,
  MONTHPLANNO   VARCHAR2(64),
  EXPIRED_DATE  VARCHAR2(10),
  MODIFY_TIME   VARCHAR2(64),
  AUDIT_MARK    VARCHAR2(2) default -1,
  WARE_HOUSE_ID VARCHAR2(64),
  DEPT_ID       VARCHAR2(128),
  CREATOR_ID    VARCHAR2(50),
  REMARK        VARCHAR2(300),
  STRG_TYPE     VARCHAR2(2),
  ISSUER_ID     VARCHAR2(50),
  ISSUE_STATUS  VARCHAR2(1) default 0,
  IS_DEL        VARCHAR2(1) default 0
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
comment on table ZS18.HT_STRG_AUX
  is '成品出库表，本表数据库管员确认后直接与用友系统同步';
comment on column ZS18.HT_STRG_AUX.ID
  is '主键  ';
comment on column ZS18.HT_STRG_AUX.OUT_DATE
  is '出入库日期';
comment on column ZS18.HT_STRG_AUX.ORDER_SN
  is '单据号  SM + 年月日 + 顺序号（3位）';
comment on column ZS18.HT_STRG_AUX.MONTHPLANNO
  is '关联生产计划ID';
comment on column ZS18.HT_STRG_AUX.EXPIRED_DATE
  is '失效日期';
comment on column ZS18.HT_STRG_AUX.MODIFY_TIME
  is '修改时间';
comment on column ZS18.HT_STRG_AUX.AUDIT_MARK
  is '审批状态';
comment on column ZS18.HT_STRG_AUX.WARE_HOUSE_ID
  is '仓库ID';
comment on column ZS18.HT_STRG_AUX.DEPT_ID
  is '部门ID';
comment on column ZS18.HT_STRG_AUX.CREATOR_ID
  is '创建人ID';
comment on column ZS18.HT_STRG_AUX.REMARK
  is '备注';
comment on column ZS18.HT_STRG_AUX.STRG_TYPE
  is '出库库类型 0 出库 1 入库';
comment on column ZS18.HT_STRG_AUX.ISSUER_ID
  is '下发人员ID';
comment on column ZS18.HT_STRG_AUX.ISSUE_STATUS
  is '下发状态 0未下达 1 己下达';
alter table ZS18.HT_STRG_AUX
  add constraint PK_T_STRG_AUX primary key (ORDER_SN)
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
prompt Creating table HT_STRG_AUX_SUB
prompt ==============================
prompt
create table ZS18.HT_STRG_AUX_SUB
(
  ID         INTEGER,
  MATER_CODE VARCHAR2(32),
  MATER_NAME VARCHAR2(512),
  STORAGE    VARCHAR2(32),
  REMARK     VARCHAR2(512),
  UNIT_CODE  VARCHAR2(32),
  IS_DEL     VARCHAR2(1) default 0,
  MAIN_CODE  VARCHAR2(32),
  NUM        INTEGER
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
comment on table ZS18.HT_STRG_AUX_SUB
  is '辅料备货计划明细表';
comment on column ZS18.HT_STRG_AUX_SUB.ID
  is '唯一标识';
comment on column ZS18.HT_STRG_AUX_SUB.MATER_CODE
  is '物料编码，按规则自动生成';
comment on column ZS18.HT_STRG_AUX_SUB.MATER_NAME
  is '物料名称';
comment on column ZS18.HT_STRG_AUX_SUB.STORAGE
  is '仓库ID';
comment on column ZS18.HT_STRG_AUX_SUB.REMARK
  is '备注';
comment on column ZS18.HT_STRG_AUX_SUB.UNIT_CODE
  is '单位';
comment on column ZS18.HT_STRG_AUX_SUB.IS_DEL
  is '删除标识';
comment on column ZS18.HT_STRG_AUX_SUB.MAIN_CODE
  is '父表ID';
comment on column ZS18.HT_STRG_AUX_SUB.NUM
  is '领用件数';

prompt
prompt Creating table HT_STRG_MATERIA
prompt ==============================
prompt
create table ZS18.HT_STRG_MATERIA
(
  ID            VARCHAR2(64),
  OUT_DATE      VARCHAR2(10),
  ORDER_SN      VARCHAR2(64) not null,
  MONTHPLANNO   VARCHAR2(32),
  EXPIRED_DATE  VARCHAR2(10),
  MODIFY_TIME   VARCHAR2(64),
  AUDIT_MARK    VARCHAR2(2) default -1,
  WARE_HOUSE_ID VARCHAR2(64),
  DEPT_ID       VARCHAR2(128),
  CREATOR_ID    VARCHAR2(50),
  REMARK        VARCHAR2(300),
  BATCHNUM      INTEGER,
  STRG_TYPE     VARCHAR2(2) default 0 not null,
  ISSUER_ID     VARCHAR2(50),
  ISSUE_STATUS  VARCHAR2(1) default 0,
  IS_DEL        VARCHAR2(1) default 0
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
comment on table ZS18.HT_STRG_MATERIA
  is '成品出库表，本表数据库管员确认后直接与用友系统同步';
comment on column ZS18.HT_STRG_MATERIA.ID
  is '主键  ';
comment on column ZS18.HT_STRG_MATERIA.OUT_DATE
  is '出入库日期';
comment on column ZS18.HT_STRG_MATERIA.ORDER_SN
  is '单据号  SM + 年月日 + 顺序号（3位）';
comment on column ZS18.HT_STRG_MATERIA.MONTHPLANNO
  is '关联生产计划ID';
comment on column ZS18.HT_STRG_MATERIA.EXPIRED_DATE
  is '失效日期';
comment on column ZS18.HT_STRG_MATERIA.MODIFY_TIME
  is '修改时间';
comment on column ZS18.HT_STRG_MATERIA.AUDIT_MARK
  is '审批状态';
comment on column ZS18.HT_STRG_MATERIA.WARE_HOUSE_ID
  is '仓库ID';
comment on column ZS18.HT_STRG_MATERIA.DEPT_ID
  is '部门ID';
comment on column ZS18.HT_STRG_MATERIA.CREATOR_ID
  is '创建人ID';
comment on column ZS18.HT_STRG_MATERIA.REMARK
  is '备注';
comment on column ZS18.HT_STRG_MATERIA.BATCHNUM
  is '投料批次';
comment on column ZS18.HT_STRG_MATERIA.STRG_TYPE
  is '出库库类型 0 出库 1 入库';
comment on column ZS18.HT_STRG_MATERIA.ISSUER_ID
  is '下发人员ID';
comment on column ZS18.HT_STRG_MATERIA.ISSUE_STATUS
  is '下发状态 0未下达 1 己下达';
alter table ZS18.HT_STRG_MATERIA
  add constraint PK_T_WA_PROD_STOCK_OUT primary key (ORDER_SN)
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
prompt Creating table HT_STRG_MATER_SUB
prompt ================================
prompt
create table ZS18.HT_STRG_MATER_SUB
(
  ID              INTEGER,
  MATER_CODE      VARCHAR2(32),
  MATER_NAME      VARCHAR2(512),
  STORAGE         VARCHAR2(32),
  ORIGINAL_DEMAND NUMBER(19,2),
  REAL_DEMAND     NUMBER(19,2),
  REMARK          VARCHAR2(512),
  UNIT_CODE       VARCHAR2(32),
  IS_DEL          VARCHAR2(1) default 0,
  MATER_FLAG      VARCHAR2(2),
  MAIN_CODE       VARCHAR2(32)
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
comment on table ZS18.HT_STRG_MATER_SUB
  is '原料备货计划明细表';
comment on column ZS18.HT_STRG_MATER_SUB.ID
  is '唯一标识';
comment on column ZS18.HT_STRG_MATER_SUB.MATER_CODE
  is '物料编码，按规则自动生成';
comment on column ZS18.HT_STRG_MATER_SUB.MATER_NAME
  is '物料名称';
comment on column ZS18.HT_STRG_MATER_SUB.STORAGE
  is '仓库ID';
comment on column ZS18.HT_STRG_MATER_SUB.ORIGINAL_DEMAND
  is '理论需求量';
comment on column ZS18.HT_STRG_MATER_SUB.REAL_DEMAND
  is '实际需求量';
comment on column ZS18.HT_STRG_MATER_SUB.REMARK
  is '备注';
comment on column ZS18.HT_STRG_MATER_SUB.UNIT_CODE
  is '单位';
comment on column ZS18.HT_STRG_MATER_SUB.IS_DEL
  is '删除标识';
comment on column ZS18.HT_STRG_MATER_SUB.MATER_FLAG
  is '类型配方物料分类（烟梗类YG、碎片类SP）';
comment on column ZS18.HT_STRG_MATER_SUB.MAIN_CODE
  is '父表ID';

prompt
prompt Creating table HT_STRG_PRODFLOW
prompt ===============================
prompt
create table ZS18.HT_STRG_PRODFLOW
(
  CGENERALHID  VARCHAR2(100) not null,
  PK_CUSTOMER  VARCHAR2(20),
  DBILLDATE    VARCHAR2(19),
  CCUSTOMERID  VARCHAR2(20),
  NAME         VARCHAR2(300),
  COTHERWHID   VARCHAR2(20),
  CMATERIALVID VARCHAR2(20),
  matername    VARCHAR2(300),
  MATERIALSPEC VARCHAR2(50),
  CUNITID      VARCHAR2(20),
  unitname     VARCHAR2(300),
  CASSCUSTID   VARCHAR2(20),
  rackname     VARCHAR2(300),
  CMATERIALOID VARCHAR2(20),
  CLOCATIONID  VARCHAR2(20),
  NASSISTNUM   NUMBER(28,8),
  NCOUNTNUM    NUMBER(28,8),
  NNUM         NUMBER(28,8),
  CGENERALBID  VARCHAR2(20)
)
tablespace ZS_DATA
  pctfree 10
  initrans 1
  maxtrans 255;
comment on column ZS18.HT_STRG_PRODFLOW.CGENERALHID
  is '出库单表头主键';
comment on column ZS18.HT_STRG_PRODFLOW.PK_CUSTOMER
  is '主键';
comment on column ZS18.HT_STRG_PRODFLOW.DBILLDATE
  is '单据日期';
comment on column ZS18.HT_STRG_PRODFLOW.CCUSTOMERID
  is '订单客户';
comment on column ZS18.HT_STRG_PRODFLOW.NAME
  is '客户名称';
comment on column ZS18.HT_STRG_PRODFLOW.COTHERWHID
  is '入库仓库';
comment on column ZS18.HT_STRG_PRODFLOW.CMATERIALVID
  is '成品编码';
comment on column ZS18.HT_STRG_PRODFLOW.matername
  is '成品名称';
comment on column ZS18.HT_STRG_PRODFLOW.MATERIALSPEC
  is '成品规格';
comment on column ZS18.HT_STRG_PRODFLOW.CUNITID
  is '单位编码';
comment on column ZS18.HT_STRG_PRODFLOW.unitname
  is '单位名称';
comment on column ZS18.HT_STRG_PRODFLOW.CASSCUSTID
  is '客户编号';
comment on column ZS18.HT_STRG_PRODFLOW.rackname
  is '货位名称';
comment on column ZS18.HT_STRG_PRODFLOW.CMATERIALOID
  is '成品编号';
comment on column ZS18.HT_STRG_PRODFLOW.CLOCATIONID
  is '货位编号';
comment on column ZS18.HT_STRG_PRODFLOW.NASSISTNUM
  is '实发数量';
comment on column ZS18.HT_STRG_PRODFLOW.NCOUNTNUM
  is '箱数';
comment on column ZS18.HT_STRG_PRODFLOW.NNUM
  is '实发主数量';
comment on column ZS18.HT_STRG_PRODFLOW.CGENERALBID
  is '唯一主键';
alter table ZS18.HT_STRG_PRODFLOW
  add constraint PK_HT_STRG_PRODFLOW primary key (CGENERALHID)
  using index 
  tablespace ZS_DATA
  pctfree 10
  initrans 2
  maxtrans 255;

prompt
prompt Creating table HT_SVR_LOGIN_RECORD
prompt ==================================
prompt
create table ZS18.HT_SVR_LOGIN_RECORD
(
  F_USER     VARCHAR2(10),
  F_COMPUTER VARCHAR2(20),
  F_TIME     VARCHAR2(19),
  F_DESCRIPT VARCHAR2(255),
  F_VALUE    FLOAT default 0
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

prompt
prompt Creating table HT_SVR_ORG_GROUP
prompt ===============================
prompt
create table ZS18.HT_SVR_ORG_GROUP
(
  F_CODE     VARCHAR2(8) not null,
  F_NAME     VARCHAR2(50) not null,
  F_PRITYPE  VARCHAR2(10),
  F_PATH     VARCHAR2(32),
  F_WEIGHT   NUMBER,
  F_PARENTID VARCHAR2(32),
  F_ROLE     VARCHAR2(20),
  F_KEY      VARCHAR2(32)
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
comment on column ZS18.HT_SVR_ORG_GROUP.F_CODE
  is '组织机构代码公司编码3位（007）+ 一级部门（3位流水）+ 子部门（2位，如无子部门默认为00）';
comment on column ZS18.HT_SVR_ORG_GROUP.F_NAME
  is '组织机构名称';
comment on column ZS18.HT_SVR_ORG_GROUP.F_PRITYPE
  is '类型';
comment on column ZS18.HT_SVR_ORG_GROUP.F_PATH
  is '路径';
comment on column ZS18.HT_SVR_ORG_GROUP.F_WEIGHT
  is '权重';
comment on column ZS18.HT_SVR_ORG_GROUP.F_PARENTID
  is '父级标识';
comment on column ZS18.HT_SVR_ORG_GROUP.F_ROLE
  is '部门基本角色';
comment on column ZS18.HT_SVR_ORG_GROUP.F_KEY
  is '主数据平台ID';
alter table ZS18.HT_SVR_ORG_GROUP
  add constraint F_CODE primary key (F_CODE)
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
prompt Creating table HT_SVR_PRT_MENU
prompt ==============================
prompt
create table ZS18.HT_SVR_PRT_MENU
(
  ID        VARCHAR2(2) not null,
  NAME      VARCHAR2(50),
  IS_DEL    VARCHAR2(1) default 0,
  PID       VARCHAR2(2),
  MENULEVEL VARCHAR2(1) default 1
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
comment on table ZS18.HT_SVR_PRT_MENU
  is '父菜单项';
comment on column ZS18.HT_SVR_PRT_MENU.ID
  is 'ID';
comment on column ZS18.HT_SVR_PRT_MENU.NAME
  is '名字';
comment on column ZS18.HT_SVR_PRT_MENU.PID
  is '父菜单ID';
comment on column ZS18.HT_SVR_PRT_MENU.MENULEVEL
  is '标识几级菜单';
alter table ZS18.HT_SVR_PRT_MENU
  add constraint PK_PRTMENUID primary key (ID)
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
prompt Creating table HT_SVR_SYS_MENU
prompt ==============================
prompt
create table ZS18.HT_SVR_SYS_MENU
(
  F_MENU     VARCHAR2(32) not null,
  F_MAPID    VARCHAR2(255) not null,
  F_DESCRIPT VARCHAR2(255),
  F_ID       VARCHAR2(5) not null,
  F_PID      VARCHAR2(5),
  F_TYPE     VARCHAR2(2) default 0 not null,
  IS_DEL     VARCHAR2(1) default 0
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
comment on column ZS18.HT_SVR_SYS_MENU.F_MENU
  is '菜单';
comment on column ZS18.HT_SVR_SYS_MENU.F_MAPID
  is 'URL映射ID';
comment on column ZS18.HT_SVR_SYS_MENU.F_DESCRIPT
  is '描述';
comment on column ZS18.HT_SVR_SYS_MENU.F_PID
  is '父菜单ID';
comment on column ZS18.HT_SVR_SYS_MENU.F_TYPE
  is '权限类型： 0菜单  1 操作   2     ';
alter table ZS18.HT_SVR_SYS_MENU
  add constraint MENU_ID primary key (F_ID)
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
prompt Creating table HT_SVR_SYS_ROLE
prompt ==============================
prompt
create table ZS18.HT_SVR_SYS_ROLE
(
  F_ROLE     VARCHAR2(19) not null,
  F_RIGHT    VARCHAR2(1024) not null,
  F_TIME     VARCHAR2(19),
  F_DESCRIPT VARCHAR2(255),
  F_ID       VARCHAR2(3)
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

prompt
prompt Creating table HT_SVR_USER
prompt ==========================
prompt
create table ZS18.HT_SVR_USER
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
  ROLE         VARCHAR2(5)
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
comment on column ZS18.HT_SVR_USER.ID
  is '唯一标示由公司编码（2位）+ 员工序号（5位）';
comment on column ZS18.HT_SVR_USER.NAME
  is '人员名称';
comment on column ZS18.HT_SVR_USER.WEIGHT
  is '权重';
comment on column ZS18.HT_SVR_USER.PARENTID
  is '父节点id';
comment on column ZS18.HT_SVR_USER.MOBILE
  is '手机';
comment on column ZS18.HT_SVR_USER.PHONE
  is '电话';
comment on column ZS18.HT_SVR_USER.RTXID
  is '传真';
comment on column ZS18.HT_SVR_USER.GENDER
  is '性别';
comment on column ZS18.HT_SVR_USER.LOGINNAME
  is '主数据标识';
comment on column ZS18.HT_SVR_USER.PASSWORD
  is '密码';
comment on column ZS18.HT_SVR_USER.EMAIL
  is '电子邮件';
comment on column ZS18.HT_SVR_USER.LEVELGROUPID
  is '组织机构id';
comment on column ZS18.HT_SVR_USER.IS_LOCAL
  is '是否本地';
comment on column ZS18.HT_SVR_USER.IS_DEL
  is '删除标识';
comment on column ZS18.HT_SVR_USER.IS_SYNC
  is '是否同步';
comment on column ZS18.HT_SVR_USER.C_STATE
  is '是否在用';
comment on column ZS18.HT_SVR_USER.DESCRIPTION
  is '描述';
comment on column ZS18.HT_SVR_USER.ROLE
  is '角色ID';
alter table ZS18.HT_SVR_USER
  add constraint PK_USER_ID primary key (ID)
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
prompt Creating table HT_SYS_EXCEL_BOOK
prompt ================================
prompt
create table ZS18.HT_SYS_EXCEL_BOOK
(
  F_ID           INTEGER not null,
  F_NAME         VARCHAR2(50) not null,
  F_PARA         VARCHAR2(5),
  F_SYNCHRO_TIME VARCHAR2(19),
  F_SOURCE       VARCHAR2(255)
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
comment on column ZS18.HT_SYS_EXCEL_BOOK.F_NAME
  is '报表名';
comment on column ZS18.HT_SYS_EXCEL_BOOK.F_PARA
  is '报表参数';
comment on column ZS18.HT_SYS_EXCEL_BOOK.F_SYNCHRO_TIME
  is '创建时间';
comment on column ZS18.HT_SYS_EXCEL_BOOK.F_SOURCE
  is '模版地址';
alter table ZS18.HT_SYS_EXCEL_BOOK
  add constraint PK_BOOK primary key (F_ID, F_NAME)
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
prompt Creating table HT_SYS_EXCEL_SEG
prompt ===============================
prompt
create table ZS18.HT_SYS_EXCEL_SEG
(
  F_ID           INTEGER not null,
  F_BOOK_ID      INTEGER not null,
  F_SHEET        VARCHAR2(128),
  F_SQL          VARCHAR2(255),
  F_DES          VARCHAR2(8) not null,
  F_DESX         VARCHAR2(3),
  F_DESY         VARCHAR2(3),
  F_SHEETINDEX   INTEGER not null,
  F_SYNCHRO_TIME VARCHAR2(19),
  IS_DEL         VARCHAR2(1) default 0
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
comment on column ZS18.HT_SYS_EXCEL_SEG.F_ID
  is 'ID标识';
comment on column ZS18.HT_SYS_EXCEL_SEG.F_BOOK_ID
  is '主模版ID标识';
comment on column ZS18.HT_SYS_EXCEL_SEG.F_SHEET
  is '工作表名';
comment on column ZS18.HT_SYS_EXCEL_SEG.F_SQL
  is '查询SQL脚本';
comment on column ZS18.HT_SYS_EXCEL_SEG.F_DES
  is '目标位置';
comment on column ZS18.HT_SYS_EXCEL_SEG.F_DESX
  is '目标行';
comment on column ZS18.HT_SYS_EXCEL_SEG.F_DESY
  is '目标列';
comment on column ZS18.HT_SYS_EXCEL_SEG.F_SHEETINDEX
  is '工作表索引';
comment on column ZS18.HT_SYS_EXCEL_SEG.F_SYNCHRO_TIME
  is '时间';
alter table ZS18.HT_SYS_EXCEL_SEG
  add constraint PK_EXCEL_SEG primary key (F_BOOK_ID, F_DES, F_SHEETINDEX)
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
prompt Creating table HT_TECH_STDD_CODE
prompt ================================
prompt
create table ZS18.HT_TECH_STDD_CODE
(
  ID             INTEGER,
  TECH_CODE      VARCHAR2(12) not null,
  PROD_CODE      VARCHAR2(10) not null,
  STANDARD_VOL   VARCHAR2(32),
  B_DATE         VARCHAR2(10),
  E_DATE         VARCHAR2(10),
  CONTROL_STATUS VARCHAR2(16),
  CREATE_ID      VARCHAR2(32),
  CREATE_DATE    VARCHAR2(10),
  CREATE_DEPT_ID VARCHAR2(32),
  MODIFY_ID      VARCHAR2(32),
  MODIFY_TIME    VARCHAR2(19),
  IS_VALID       VARCHAR2(1) default 1,
  IS_DEL         VARCHAR2(1) default 0,
  REMARK         VARCHAR2(2048),
  TECH_NAME      VARCHAR2(128),
  FLOW_STATUS    VARCHAR2(2) default -1
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
comment on column ZS18.HT_TECH_STDD_CODE.ID
  is '唯一标识';
comment on column ZS18.HT_TECH_STDD_CODE.TECH_CODE
  is '工艺标准编码';
comment on column ZS18.HT_TECH_STDD_CODE.PROD_CODE
  is '产品编码';
comment on column ZS18.HT_TECH_STDD_CODE.STANDARD_VOL
  is '标准版本号';
comment on column ZS18.HT_TECH_STDD_CODE.B_DATE
  is '执行日期';
comment on column ZS18.HT_TECH_STDD_CODE.E_DATE
  is '结束日期';
comment on column ZS18.HT_TECH_STDD_CODE.CONTROL_STATUS
  is '受控状态';
comment on column ZS18.HT_TECH_STDD_CODE.CREATE_ID
  is '创建人标识';
comment on column ZS18.HT_TECH_STDD_CODE.CREATE_DATE
  is '编制日期';
comment on column ZS18.HT_TECH_STDD_CODE.CREATE_DEPT_ID
  is '编制部门ID';
comment on column ZS18.HT_TECH_STDD_CODE.MODIFY_ID
  is '修改人标识';
comment on column ZS18.HT_TECH_STDD_CODE.MODIFY_TIME
  is '修改时间';
comment on column ZS18.HT_TECH_STDD_CODE.IS_DEL
  is '删除标识';
comment on column ZS18.HT_TECH_STDD_CODE.REMARK
  is '备注';
comment on column ZS18.HT_TECH_STDD_CODE.TECH_NAME
  is '工艺标准名';
comment on column ZS18.HT_TECH_STDD_CODE.FLOW_STATUS
  is '审批状态';
alter table ZS18.HT_TECH_STDD_CODE
  add constraint TECH_CODE primary key (TECH_CODE)
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
prompt Creating table HT_TECH_STDD_CODE_DETAIL
prompt =======================================
prompt
create table ZS18.HT_TECH_STDD_CODE_DETAIL
(
  ID          INTEGER,
  TECH_CODE   VARCHAR2(32) not null,
  PARA_CODE   VARCHAR2(32) not null,
  PARA_TYPE   VARCHAR2(32),
  TECH_SORT   NUMBER(10,2),
  IS_DEL      VARCHAR2(1) default 0,
  REMARK      VARCHAR2(1024),
  VALUE       FLOAT,
  UPPER_LIMIT FLOAT,
  LOWER_LIMIT FLOAT,
  EER_DEV     FLOAT,
  UNIT        VARCHAR2(32)
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
comment on column ZS18.HT_TECH_STDD_CODE_DETAIL.ID
  is '唯一标识';
comment on column ZS18.HT_TECH_STDD_CODE_DETAIL.TECH_CODE
  is '工艺标准码';
comment on column ZS18.HT_TECH_STDD_CODE_DETAIL.PARA_CODE
  is '参数码';
comment on column ZS18.HT_TECH_STDD_CODE_DETAIL.PARA_TYPE
  is '参数类型';
comment on column ZS18.HT_TECH_STDD_CODE_DETAIL.IS_DEL
  is '删除标识';
comment on column ZS18.HT_TECH_STDD_CODE_DETAIL.REMARK
  is '备注';
comment on column ZS18.HT_TECH_STDD_CODE_DETAIL.VALUE
  is '标准值';
comment on column ZS18.HT_TECH_STDD_CODE_DETAIL.UPPER_LIMIT
  is '上限';
comment on column ZS18.HT_TECH_STDD_CODE_DETAIL.LOWER_LIMIT
  is '下限';
comment on column ZS18.HT_TECH_STDD_CODE_DETAIL.EER_DEV
  is '允差';
comment on column ZS18.HT_TECH_STDD_CODE_DETAIL.UNIT
  is '单位';
alter table ZS18.HT_TECH_STDD_CODE_DETAIL
  add constraint PARA_TECH primary key (PARA_CODE, TECH_CODE)
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
prompt Creating sequence APRVFLOW_ID_SEQ
prompt =================================
prompt
create sequence ZS18.APRVFLOW_ID_SEQ
minvalue 1
maxvalue 9999999999999999999999999999
start with 45
increment by 1
nocache;

prompt
prompt Creating sequence APRVOPINION_ID_SEQ
prompt ====================================
prompt
create sequence ZS18.APRVOPINION_ID_SEQ
minvalue 1
maxvalue 9999999999999999999999999999
start with 75
increment by 1
nocache;

prompt
prompt Creating sequence EXCELBOOK_ID_SEQ
prompt ==================================
prompt
create sequence ZS18.EXCELBOOK_ID_SEQ
minvalue 1
maxvalue 9999999999999999999999999999
start with 7
increment by 1
nocache;

prompt
prompt Creating sequence EXCELSEG_ID_SEQ
prompt =================================
prompt
create sequence ZS18.EXCELSEG_ID_SEQ
minvalue 1
maxvalue 9999999999999999999999999999
start with 16
increment by 1
nocache;

prompt
prompt Creating sequence FAULT_ID_SEQ
prompt ==============================
prompt
create sequence ZS18.FAULT_ID_SEQ
minvalue 1
maxvalue 9999999999999999999999999999
start with 13
increment by 1
nocache;

prompt
prompt Creating sequence LBDETAIL_ID_SEQ
prompt =================================
prompt
create sequence ZS18.LBDETAIL_ID_SEQ
minvalue 1
maxvalue 9999999999999999999999999999
start with 5
increment by 1
nocache;

prompt
prompt Creating sequence MONTHPLAN_ID_SEQ
prompt ==================================
prompt
create sequence ZS18.MONTHPLAN_ID_SEQ
minvalue 1
maxvalue 9999999999999999999999999999
start with 12
increment by 1
nocache;

prompt
prompt Creating sequence MTDETAIL_ID_SEQ
prompt =================================
prompt
create sequence ZS18.MTDETAIL_ID_SEQ
minvalue 1
maxvalue 9999999999999999999999999999
start with 3
increment by 1
nocache;

prompt
prompt Creating sequence MTSHIFT_ID_SEQ
prompt ================================
prompt
create sequence ZS18.MTSHIFT_ID_SEQ
minvalue 1
maxvalue 9999999999999999999999999999
start with 1
increment by 1
nocache;

prompt
prompt Creating sequence PATHNODE_ID_SEQ
prompt =================================
prompt
create sequence ZS18.PATHNODE_ID_SEQ
minvalue 1
maxvalue 9999999999999999999999999999
start with 25
increment by 1
nocache;

prompt
prompt Creating sequence ROLE_ID_SEQ
prompt =============================
prompt
create sequence ZS18.ROLE_ID_SEQ
minvalue 1
maxvalue 9999999999999999999999999999
start with 1
increment by 1
nocache;

prompt
prompt Creating sequence RPDETAIL_ID_SEQ
prompt =================================
prompt
create sequence ZS18.RPDETAIL_ID_SEQ
minvalue 1
maxvalue 9999999999999999999999999999
start with 13
increment by 1
nocache;

prompt
prompt Creating sequence SCHEDULE_ID_SEQ
prompt =================================
prompt
create sequence ZS18.SCHEDULE_ID_SEQ
minvalue 1
maxvalue 9999999999999999999999999999
start with 179
increment by 1
nocache;

prompt
prompt Creating sequence SEASONDT_ID_SEQ
prompt =================================
prompt
create sequence ZS18.SEASONDT_ID_SEQ
minvalue 1
maxvalue 9999999999999999999999999999
start with 11
increment by 1
nocache;

prompt
prompt Creating sequence SEASONPLAN_ID_SEQ
prompt ===================================
prompt
create sequence ZS18.SEASONPLAN_ID_SEQ
minvalue 1
maxvalue 9999999999999999999999999999
start with 4
increment by 1
nocache;

prompt
prompt Creating sequence STRGAUX_ID_SEQ
prompt ================================
prompt
create sequence ZS18.STRGAUX_ID_SEQ
minvalue 1
maxvalue 9999999999999999999999999999
start with 1
increment by 1
nocache;

prompt
prompt Creating sequence STRGMATER_ID_SEQ
prompt ==================================
prompt
create sequence ZS18.STRGMATER_ID_SEQ
minvalue 1
maxvalue 9999999999999999999999999999
start with 4
increment by 1
nocache;

prompt
prompt Creating sequence SUB_PICKUP_ID_SEQ
prompt ===================================
prompt
create sequence ZS18.SUB_PICKUP_ID_SEQ
minvalue 1
maxvalue 9999999999999999999999999999
start with 1
increment by 1
nocache;

prompt
prompt Creating trigger APRVFLOW_INS_TRG
prompt =================================
prompt
CREATE OR REPLACE TRIGGER ZS18.APRVFLOW_INS_TRG BEFORE INSERT ON HT_PUB_APRV_FLOWINFO FOR EACH ROW
when (NEW.ID IS NULL)
BEGIN
SELECT APRVFLOW_ID_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

prompt
prompt Creating trigger APRVOPINION_INS_TRG
prompt ====================================
prompt
CREATE OR REPLACE TRIGGER ZS18.APRVOPINION_INS_TRG BEFORE INSERT ON HT_PUB_APRV_OPINION FOR EACH ROW
when (NEW.ID IS NULL)
BEGIN
SELECT APRVOPINION_ID_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

prompt
prompt Creating trigger EXCELBOOK_INS_TRG
prompt ==================================
prompt
CREATE OR REPLACE TRIGGER ZS18.ExcelBook_INS_TRG BEFORE INSERT ON HT_SYS_EXCEL_BOOK FOR EACH ROW
when (NEW.F_ID IS NULL)
BEGIN
SELECT ExcelBook_ID_SEQ.NEXTVAL INTO :NEW.F_ID FROM DUAL;
END;
/

prompt
prompt Creating trigger EXCELSEG_INS_TRG
prompt =================================
prompt
CREATE OR REPLACE TRIGGER ZS18.ExcelSeg_INS_TRG BEFORE INSERT ON HT_SYS_EXCEL_SEG FOR EACH ROW
when (NEW.F_ID IS NULL)
BEGIN
SELECT ExcelSeg_ID_SEQ.NEXTVAL INTO :NEW.F_ID FROM DUAL;
END;
/

prompt
prompt Creating trigger FAULT_INS_TRG
prompt ==============================
prompt
CREATE OR REPLACE TRIGGER ZS18.fault_INS_TRG BEFORE INSERT ON HT_EQ_FAULT_DB FOR EACH ROW
when (NEW.ID IS NULL)
BEGIN
SELECT fault_ID_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

prompt
prompt Creating trigger LBDETAIL_INS_TRG
prompt =================================
prompt
CREATE OR REPLACE TRIGGER ZS18.lbdetail_INS_TRG BEFORE INSERT ON HT_EQ_LB_PLAN_DETAIL FOR EACH ROW
when (NEW.ID IS NULL)
BEGIN
SELECT lbdetail_ID_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

prompt
prompt Creating trigger MONTHPLAN_INS_TRG
prompt ==================================
prompt
CREATE OR REPLACE TRIGGER ZS18.MonthPlan_INS_TRG BEFORE INSERT ON ht_prod_month_plan FOR EACH ROW
when (NEW.ID IS NULL)
BEGIN
SELECT Monthplan_ID_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

prompt
prompt Creating trigger MTDETAIL_INS_TRG
prompt =================================
prompt
CREATE OR REPLACE TRIGGER ZS18.MTDetail_INS_TRG BEFORE INSERT ON HT_EQ_MT_PLAN_DETAIL FOR EACH ROW
when (NEW.ID IS NULL)
BEGIN
SELECT MTDetail_ID_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

prompt
prompt Creating trigger MTSHIFT_INS_TRG
prompt ================================
prompt
CREATE OR REPLACE TRIGGER ZS18.mtshift_INS_TRG BEFORE INSERT ON HT_EQ_MT_SHIFT_DETAIL FOR EACH ROW
when (NEW.ID IS NULL)
BEGIN
SELECT mtshift_ID_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

prompt
prompt Creating trigger PATHNODE_INS_TRG
prompt =================================
prompt
CREATE OR REPLACE TRIGGER ZS18.PathNode_INS_TRG BEFORE INSERT ON HT_PUB_PATH_NODE FOR EACH ROW
when (NEW.ID IS NULL)
BEGIN
SELECT PathNode_ID_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

prompt
prompt Creating trigger ROLE_INS_TRG
prompt =============================
prompt
CREATE OR REPLACE TRIGGER ZS18.Role_INS_TRG BEFORE INSERT ON HT_SVR_SYS_ROLE FOR EACH ROW
when (NEW.F_ID IS NULL)
BEGIN
SELECT Role_ID_SEQ.NEXTVAL INTO :NEW.F_ID FROM DUAL;
END;
/

prompt
prompt Creating trigger RPDETAIL_INS_TRG
prompt =================================
prompt
CREATE OR REPLACE TRIGGER ZS18.RPDETAIL_INS_TRG BEFORE INSERT ON HT_EQ_RP_PLAN_DETAIL FOR EACH ROW
when (NEW.ID IS NULL)
BEGIN
SELECT RPDETAIL_ID_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

prompt
prompt Creating trigger SCHEDULE_INS_TRG
prompt =================================
prompt
CREATE OR REPLACE TRIGGER ZS18.SCHEDULE_INS_TRG BEFORE INSERT ON HT_PROD_SCHEDULE FOR EACH ROW
when (NEW.ID IS NULL)
BEGIN
SELECT SCHEDULE_ID_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

prompt
prompt Creating trigger SEASONDT_INS_TRG
prompt =================================
prompt
CREATE OR REPLACE TRIGGER ZS18.seasonDT_INS_TRG BEFORE INSERT ON HT_PROD_SEASON_PLAN_DETAIL FOR EACH ROW
when (NEW.ID IS NULL)
BEGIN
SELECT seasonDT_ID_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

prompt
prompt Creating trigger SEASONPLAN_INS_TRG
prompt ===================================
prompt
CREATE OR REPLACE TRIGGER ZS18.SeasonPlan_INS_TRG BEFORE INSERT ON HT_PROD_SEASON_PLAN FOR EACH ROW
when (NEW.ID IS NULL)
BEGIN
SELECT SeasonPlan_ID_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

prompt
prompt Creating trigger STRGAUX_INS_TRG
prompt ================================
prompt
CREATE OR REPLACE TRIGGER ZS18.STRGAUX_INS_TRG BEFORE INSERT ON HT_STRG_AUX_SUB FOR EACH ROW
when (NEW.ID IS NULL)
BEGIN
SELECT STRGAUX_ID_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

prompt
prompt Creating trigger STRGMATER_INS_TRG
prompt ==================================
prompt
CREATE OR REPLACE TRIGGER ZS18.STRGMater_INS_TRG BEFORE INSERT ON HT_STRG_MATER_SUB FOR EACH ROW
when (NEW.ID IS NULL)
BEGIN
SELECT STRGMater_ID_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

prompt
prompt Creating trigger SUB_PICKUP_INS_TRG
prompt ===================================
prompt
CREATE OR REPLACE TRIGGER ZS18.SUB_PICKUP_INS_TRG BEFORE INSERT ON HT_EQ_STG_PICKUP_DETAIL FOR EACH ROW
when (NEW.ID IS NULL)
BEGIN
SELECT SUB_PICKUP_ID_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/


spool off
