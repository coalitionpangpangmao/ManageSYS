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
  is '��Ч���˹���÷�';

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
  is 'Ψһ��ʶ';
comment on column ZS18.HT_EQ_EQP_CLS.NODE_NAME
  is '�ڵ�����';
comment on column ZS18.HT_EQ_EQP_CLS.NODE_VALUE
  is '�ڵ�ֵ';
comment on column ZS18.HT_EQ_EQP_CLS.PARENT_ID
  is '���ڵ��ʶ';
comment on column ZS18.HT_EQ_EQP_CLS.TYPE
  is '����';
comment on column ZS18.HT_EQ_EQP_CLS.PATH
  is '·��';
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
  is 'Ψһ��ʶ һ������2λ+��������2λ+��������2λ+�ļ�����2λ+˳���3λ';
comment on column ZS18.HT_EQ_EQP_TBL.CLS_CODE
  is '������';
comment on column ZS18.HT_EQ_EQP_TBL.EQ_NAME
  is '���豸����';
comment on column ZS18.HT_EQ_EQP_TBL.SGS_CODE
  is 'ʡ��˾�̶��ʲ�����';
comment on column ZS18.HT_EQ_EQP_TBL.NC_CODE
  is 'NC����';
comment on column ZS18.HT_EQ_EQP_TBL.FINANCE_EQ_NAME
  is '����̶��ʲ�����';
comment on column ZS18.HT_EQ_EQP_TBL.EQ_TYPE
  is '��ҵ�豸����';
comment on column ZS18.HT_EQ_EQP_TBL.EQ_STATUS
  is '�豸״̬';
comment on column ZS18.HT_EQ_EQP_TBL.ZG_DATE
  is 'ת������';
comment on column ZS18.HT_EQ_EQP_TBL.EQ_MODEL
  is '�豸�ͺ�';
comment on column ZS18.HT_EQ_EQP_TBL.ORI_WORTH
  is '�豸�ʲ�ԭֵ';
comment on column ZS18.HT_EQ_EQP_TBL.NET_WORTH
  is '�豸�ʲ���ֵ';
comment on column ZS18.HT_EQ_EQP_TBL.USED_DATE
  is 'Ͷ��ʹ������';
comment on column ZS18.HT_EQ_EQP_TBL.RATED_POWER
  is '���������';
comment on column ZS18.HT_EQ_EQP_TBL.REAL_POWER
  is 'ʵ����������';
comment on column ZS18.HT_EQ_EQP_TBL.POWER_UNIT
  is '������λ';
comment on column ZS18.HT_EQ_EQP_TBL.OWNER_NAME
  is '������ҵ����';
comment on column ZS18.HT_EQ_EQP_TBL.EQP_FROM
  is '�豸��Դ';
comment on column ZS18.HT_EQ_EQP_TBL.ORI_OWNER_NAME
  is 'ԭ������ҵ����';
comment on column ZS18.HT_EQ_EQP_TBL.MANUFACTURER
  is '������';
comment on column ZS18.HT_EQ_EQP_TBL.SERIAL_NUMBER
  is '�������';
comment on column ZS18.HT_EQ_EQP_TBL.SUPPLIER
  is '��Ӧ��';
comment on column ZS18.HT_EQ_EQP_TBL.IS_SPEC_EQP
  is '�Ƿ������豸';
comment on column ZS18.HT_EQ_EQP_TBL.IS_MADEINCHINA
  is '�Ƿ����';
comment on column ZS18.HT_EQ_EQP_TBL.MGT_DEPT_CODE
  is '�����ű���';
comment on column ZS18.HT_EQ_EQP_TBL.USE_DEPT_CODE
  is 'ʹ�ò��ű���';
comment on column ZS18.HT_EQ_EQP_TBL.DUTY_NAME
  is '������';
comment on column ZS18.HT_EQ_EQP_TBL.EQP_IP
  is 'IP��ַ';
comment on column ZS18.HT_EQ_EQP_TBL.EQP_MAC
  is 'MAC��ַ';
comment on column ZS18.HT_EQ_EQP_TBL.EQP_SN
  is '�豸SN';
comment on column ZS18.HT_EQ_EQP_TBL.EQP_SYS
  is '����ϵͳ';
comment on column ZS18.HT_EQ_EQP_TBL.REMARK
  is '��ע';
comment on column ZS18.HT_EQ_EQP_TBL.IS_DEL
  is 'ɾ����ʶ';
comment on column ZS18.HT_EQ_EQP_TBL.CREATOR
  is '������';
comment on column ZS18.HT_EQ_EQP_TBL.CREATE_TIME
  is '����ʱ��';
comment on column ZS18.HT_EQ_EQP_TBL.FLAG
  is '˳��Ψһ��ʶ';
comment on column ZS18.HT_EQ_EQP_TBL.SECTION_CODE
  is '�������ձ���';
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
  is '����ID';
comment on column ZS18.HT_EQ_FAULT_DB.EQP_TYPE
  is '�������� 0 ����1 ��е';
comment on column ZS18.HT_EQ_FAULT_DB.SPECIFIC_LOCATION
  is '���Ͼ���λ��';
comment on column ZS18.HT_EQ_FAULT_DB.SECTION_CODE
  is '�豸��������';
comment on column ZS18.HT_EQ_FAULT_DB.ERROR_DESCRIPTION
  is '�豸��������';
comment on column ZS18.HT_EQ_FAULT_DB.FAILURE_CAUSE
  is '�豸����ԭ��';
comment on column ZS18.HT_EQ_FAULT_DB.SOLUTION
  is '�豸���Ͻ������';
comment on column ZS18.HT_EQ_FAULT_DB.FAULT_TYPE1
  is '1�����Թ���2ͻ���Թ���';
comment on column ZS18.HT_EQ_FAULT_DB.FAULT_TYPE2
  is '1����Թ���2�����Թ���';
comment on column ZS18.HT_EQ_FAULT_DB.FAULT_TYPE3
  is '1��ȫ�Թ���2�ֲ��Թ���';
comment on column ZS18.HT_EQ_FAULT_DB.FAULT_TYPE4
  is '1ĥ���Թ���2�����Թ���3���еı����Թ���';
comment on column ZS18.HT_EQ_FAULT_DB.FAULT_TYPE5
  is '1Σ���Թ���2��ȫ�Թ���';
comment on column ZS18.HT_EQ_FAULT_DB.FAULT_TYPE6
  is '1�������2�й��ɹ���';
comment on column ZS18.HT_EQ_FAULT_DB.IS_DEL
  is 'ɾ����ʶ';
comment on column ZS18.HT_EQ_FAULT_DB.EDITOR_ID
  is '������';
comment on column ZS18.HT_EQ_FAULT_DB.ERROR_NAME
  is '������';
comment on column ZS18.HT_EQ_FAULT_DB.SCEAN
  is '�����ֳ�';
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
  is '������';
comment on column ZS18.HT_EQ_LB_PLAN.FLOW_STATUS
  is '����״̬ -1 δ�ύ���� 0������ ����ͨ�� ��δͨ��';
comment on column ZS18.HT_EQ_LB_PLAN.CREATE_ID
  is '������id';
comment on column ZS18.HT_EQ_LB_PLAN.CREATE_DEPT_ID
  is '���벿��id';
comment on column ZS18.HT_EQ_LB_PLAN.CREATE_TIME
  is '����ʱ��';
comment on column ZS18.HT_EQ_LB_PLAN.REMARK
  is '��ע';
comment on column ZS18.HT_EQ_LB_PLAN.IS_DEL
  is 'ɾ����ʶ';
comment on column ZS18.HT_EQ_LB_PLAN.PZ_CODE
  is 'ƾ֤�� LB+ YYYYMMDD + ��λ��ˮ��';
comment on column ZS18.HT_EQ_LB_PLAN.GOWHERE
  is '���̾���';
comment on column ZS18.HT_EQ_LB_PLAN.EXPIRED_DATE
  is '����ʱ��';
comment on column ZS18.HT_EQ_LB_PLAN.TASK_STATUS
  is 'ִ��״̬ 0 δִ��  1 ִ����  2 ����� 3 ������';
comment on column ZS18.HT_EQ_LB_PLAN.IS_MODEL
  is '�Ƿ�����ģ��';
comment on column ZS18.HT_EQ_LB_PLAN.MT_NAME
  is 'ά���ƻ���';
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
  is 'ͣ��ά�޼ƻ������ӱ�';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.ID
  is '������ʶ';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.MAIN_ID
  is '����id';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.EQUIPMENT_ID
  is '�豸ID';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.NTRODUCER
  is '�����';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.STATUS
  is '״̬ 0δ�ɹ�1���ɹ� 2�����';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.REMARK
  is '��ע';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.CREATE_ID
  is '������id';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.CREATE_TIME
  is '����ʱ��';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.IS_DEL
  is '�Ƿ�ɾ��';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.SECTION
  is '��������';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.RESPONER
  is '������';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.EXP_FINISH_TIME
  is '�������ʱ��';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.PICKUP_DESC
  is '�������';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.YEAR_MONTH
  is '��������';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.POSITION
  is '�󻬲�λ';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.POINTNUM
  is '�󻬵���';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.LUBOIL
  is '����';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.EXE_TIME
  is '����ʱ��';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.PERIODIC
  is '������';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.STYLE
  is '�󻬷�ʽ';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.AMOUNT
  is '������';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.EXE_SEGTIME
  is '����ʱ��';
comment on column ZS18.HT_EQ_LB_PLAN_DETAIL.VERIFIOR
  is '��֤��';
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
  is '�˹�У׼�ƻ������ӱ�����';
comment on column ZS18.HT_EQ_MCLBR_CONTENT.MAIN_ID
  is '����id';
comment on column ZS18.HT_EQ_MCLBR_CONTENT.BTIME
  is 'ȡ����ʼʱ��';
comment on column ZS18.HT_EQ_MCLBR_CONTENT.ETIME
  is 'ȡ������ʱ��';
comment on column ZS18.HT_EQ_MCLBR_CONTENT.AVG
  is '��ֵ';
comment on column ZS18.HT_EQ_MCLBR_CONTENT.STD
  is '��׼��';
comment on column ZS18.HT_EQ_MCLBR_CONTENT.ERR_AVG
  is '����ֵ';
comment on column ZS18.HT_EQ_MCLBR_CONTENT.IS_DEL
  is '�Ƿ�ɾ��';
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
  is '�˹�У׼����';
comment on column ZS18.HT_EQ_MCLBR_PLAN.FLOW_STATUS
  is '����״̬ -1 δ�ύ���� 0������ ����ͨ�� ��δͨ��';
comment on column ZS18.HT_EQ_MCLBR_PLAN.CREATE_ID
  is '������id';
comment on column ZS18.HT_EQ_MCLBR_PLAN.CREATE_DEPT_ID
  is '���벿��id';
comment on column ZS18.HT_EQ_MCLBR_PLAN.CREATE_TIME
  is '����ʱ��';
comment on column ZS18.HT_EQ_MCLBR_PLAN.REMARK
  is '��ע';
comment on column ZS18.HT_EQ_MCLBR_PLAN.IS_DEL
  is 'ɾ����ʶ';
comment on column ZS18.HT_EQ_MCLBR_PLAN.PZ_CODE
  is 'ƾ֤�� LB+ YYYYMMDD + ��λ��ˮ��';
comment on column ZS18.HT_EQ_MCLBR_PLAN.GOWHERE
  is '���̾���';
comment on column ZS18.HT_EQ_MCLBR_PLAN.EXPIRED_DATE
  is '����ʱ��';
comment on column ZS18.HT_EQ_MCLBR_PLAN.TASK_STATUS
  is 'ִ��״̬ 0 δִ��  1 ִ����  2 ����� 3 ������';
comment on column ZS18.HT_EQ_MCLBR_PLAN.IS_MODEL
  is '�Ƿ���ģ��';
comment on column ZS18.HT_EQ_MCLBR_PLAN.MT_NAME
  is '�ƻ���';
comment on column ZS18.HT_EQ_MCLBR_PLAN.CLBRT_TYPE
  is 'У׼��ʽ 0 �˹�1�Զ�';
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
  is '�˹�У׼�ƻ������ӱ�';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.ID
  is '������ʶ';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.MAIN_ID
  is '����id';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.EQUIPMENT_ID
  is '�豸ID';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.NTRODUCER
  is '�����';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.STATUS
  is '״̬ 0δ�ɹ�1���ɹ� 2�����';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.REMARK
  is '��ע';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.CREATE_ID
  is '������id';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.CREATE_TIME
  is '����ʱ��';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.IS_DEL
  is '�Ƿ�ɾ��';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.SECTION
  is '��������';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.RESPONER
  is '������';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.EXP_FINISH_TIME
  is '�������ʱ��';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.POINT
  is 'ȡ����';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.SHIFT
  is 'ȡ�����';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.TEAM
  is 'ȡ������';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.SAMPLE_TIME
  is 'ȡ��ʱ��';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.VERIFIOR
  is '��֤��';
comment on column ZS18.HT_EQ_MCLBR_PLAN_DETAIL.POINTVALUE
  is 'ȡ��ֵ';
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
  is 'ά���ƻ���������';
comment on column ZS18.HT_EQ_MT_PLAN.FLOW_STATUS
  is '����״̬ -1 δ�ύ���� 0������ ����ͨ�� ��δͨ��';
comment on column ZS18.HT_EQ_MT_PLAN.CREATE_ID
  is '������id';
comment on column ZS18.HT_EQ_MT_PLAN.CREATE_DEPT_ID
  is '���벿��id';
comment on column ZS18.HT_EQ_MT_PLAN.CREATE_TIME
  is '����ʱ��';
comment on column ZS18.HT_EQ_MT_PLAN.REMARK
  is '��ע';
comment on column ZS18.HT_EQ_MT_PLAN.IS_DEL
  is 'ɾ����ʶ';
comment on column ZS18.HT_EQ_MT_PLAN.PZ_CODE
  is 'ƾ֤�� MT+ YYYYMMDD + ��λ��ˮ��';
comment on column ZS18.HT_EQ_MT_PLAN.GOWHERE
  is '���̾���';
comment on column ZS18.HT_EQ_MT_PLAN.EXPIRED_DATE
  is '����ʱ��';
comment on column ZS18.HT_EQ_MT_PLAN.TASK_STATUS
  is 'ִ��״̬ 0 δִ��  1 ִ����  2 ����� 3 ������';
comment on column ZS18.HT_EQ_MT_PLAN.IS_MODEL
  is '�Ƿ���ά��ģ��';
comment on column ZS18.HT_EQ_MT_PLAN.MT_NAME
  is 'ά���ƻ���';
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
  is 'ͣ��ά�޼ƻ������ӱ�';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.ID
  is '������ʶ';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.MAIN_ID
  is '����id';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.EQUIPMENT_ID
  is 'ά���豸ID';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.NTRODUCER
  is '�����';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.STATUS
  is '״̬ 0δ�ɹ�1���ɹ� 2�����';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.REMARK
  is '��ע';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.CREATE_ID
  is '������id';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.CREATE_TIME
  is '����ʱ��';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.IS_DEL
  is '�Ƿ�ɾ��';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.MECH_AREA
  is '����';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.RESPONER
  is '������';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.EXP_FINISH_TIME
  is '�������ʱ��';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.IS_OUT
  is '�Ƿ�ί��';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.PICKUP_DESC
  is '�������';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.YEAR_MONTH
  is '��������';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.FAULT_ID
  is '����ID';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.REASON
  is 'ά��ԭ��';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.CONTENT
  is 'ά������';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.IS_FAULT
  is '�Ƿ��й���';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.EXE_TIME
  is '����ʱ��';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.RECORD
  is '������¼';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.RESULTS
  is '�������';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.CONDITION
  is 'ά�����';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.EXE_SEGTIME
  is '����ʱ��';
comment on column ZS18.HT_EQ_MT_PLAN_DETAIL.VERIFIOR
  is '��֤��';
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
  is '��еά�ްཻ���¼ID';
comment on column ZS18.HT_EQ_MT_SHIFT.WORKSHOP_CODE
  is '��������';
comment on column ZS18.HT_EQ_MT_SHIFT.SHIFT_CODE
  is '��ʱ����';
comment on column ZS18.HT_EQ_MT_SHIFT.TEAM_CODE
  is '�������';
comment on column ZS18.HT_EQ_MT_SHIFT.HANDOVER_DATE
  is '��������';
comment on column ZS18.HT_EQ_MT_SHIFT.B_TIME
  is '��ʼʱ��';
comment on column ZS18.HT_EQ_MT_SHIFT.E_TIME
  is '����ʱ��';
comment on column ZS18.HT_EQ_MT_SHIFT.CREATE_ID
  is '������ID';
comment on column ZS18.HT_EQ_MT_SHIFT.MODIFY_ID
  is '�Ӱ���ID';
comment on column ZS18.HT_EQ_MT_SHIFT.RECORD_TIME
  is '��¼ʱ��';
comment on column ZS18.HT_EQ_MT_SHIFT.REMARK
  is '��ע';
comment on column ZS18.HT_EQ_MT_SHIFT.IS_VALID
  is '�Ƿ���Ч';
comment on column ZS18.HT_EQ_MT_SHIFT.IS_DEL
  is 'ɾ����ʶ';
comment on column ZS18.HT_EQ_MT_SHIFT.SHIFT_STATUS
  is '���Ӱ�״̬';
comment on column ZS18.HT_EQ_MT_SHIFT.MAINTENANCE_TYPE
  is 'ά������';
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
  is 'ά�ްཻ���ӱ�ID';
comment on column ZS18.HT_EQ_MT_SHIFT_DETAIL.SHIFT_MAIN_ID
  is 'ά�ްཻ������ID';
comment on column ZS18.HT_EQ_MT_SHIFT_DETAIL.MAINTENANCE_TYPE
  is 'ά�����͡�0����1��е';
comment on column ZS18.HT_EQ_MT_SHIFT_DETAIL.MANAGE_STATUS
  is '����״̬';
comment on column ZS18.HT_EQ_MT_SHIFT_DETAIL.IS_DEL
  is 'ɾ����ʶ';
comment on column ZS18.HT_EQ_MT_SHIFT_DETAIL.BUZ_ID
  is 'ҵ��ID';
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
  is 'ͣ��ά�޼ƻ���������';
comment on column ZS18.HT_EQ_RP_PLAN.FLOW_STATUS
  is '����״̬ -1 δ�ύ���� 0������ ����ͨ�� ��δͨ��';
comment on column ZS18.HT_EQ_RP_PLAN.CREATE_ID
  is '������id';
comment on column ZS18.HT_EQ_RP_PLAN.CREATE_DEPT_ID
  is '���벿��id';
comment on column ZS18.HT_EQ_RP_PLAN.CREATE_TIME
  is '����ʱ��';
comment on column ZS18.HT_EQ_RP_PLAN.REMARK
  is '��ע';
comment on column ZS18.HT_EQ_RP_PLAN.IS_DEL
  is 'ɾ����ʶ';
comment on column ZS18.HT_EQ_RP_PLAN.PZ_CODE
  is 'ƾ֤�� RP+ YYYYMMDD + ��λ��ˮ��';
comment on column ZS18.HT_EQ_RP_PLAN.GOWHERE
  is '���̾���';
comment on column ZS18.HT_EQ_RP_PLAN.EXPIRED_DATE
  is '����ʱ��';
comment on column ZS18.HT_EQ_RP_PLAN.TASK_STATUS
  is 'ִ��״̬ 0 δִ��  1 ִ����  2 ����� 3 ������';
comment on column ZS18.HT_EQ_RP_PLAN.MT_NAME
  is 'ά�޼ƻ���';
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
  is 'ͣ��ά�޼ƻ��ӱ�';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.ID
  is '������ʶ';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.MAIN_ID
  is '����id';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.EQUIPMENT_ID
  is 'ά���豸ID';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.NTRODUCER
  is '�����';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.STATUS
  is '״̬ -1�ϱ�δ����0δ�ɹ�1���ɹ� 2�����';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.REMARK
  is '��ע';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.CREATE_ID
  is '������id';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.CREATE_TIME
  is '����ʱ��';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.IS_DEL
  is '�Ƿ�ɾ��';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.MECH_AREA
  is '����';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.RESPONER
  is '������';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.EXP_FINISH_TIME
  is '�������ʱ��';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.IS_OUT
  is '�Ƿ�ί��';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.PICKUP_DESC
  is '�������';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.YEAR_MONTH
  is '��������';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.FAULT_ID
  is '����ID';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.EXE_TIME
  is '����ʱ��';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.REASON
  is 'ά��ԭ��';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.CONTENT
  is 'ά������';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.IS_EMG
  is '�Ƿ���Ӧ��ά��';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.EXE_SEGTIME
  is '����ʱ��';
comment on column ZS18.HT_EQ_RP_PLAN_DETAIL.VERIFIOR
  is '��֤��';
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
  is '������������';
comment on column ZS18.HT_EQ_STG_PICKUP.PICKUP_DATE
  is '��������';
comment on column ZS18.HT_EQ_STG_PICKUP.FLOW_STATUS
  is '����״̬ -1 δ�ύ���� 0������ ����ͨ�� ��δͨ��';
comment on column ZS18.HT_EQ_STG_PICKUP.CREATE_ID
  is '������id';
comment on column ZS18.HT_EQ_STG_PICKUP.CREATE_DEPT_ID
  is '���벿��id';
comment on column ZS18.HT_EQ_STG_PICKUP.CREATE_TIME
  is '����ʱ��';
comment on column ZS18.HT_EQ_STG_PICKUP.REMARK
  is '��ע';
comment on column ZS18.HT_EQ_STG_PICKUP.IS_DEL
  is 'ɾ����ʶ';
comment on column ZS18.HT_EQ_STG_PICKUP.PZ_CODE
  is 'ƾ֤��';
comment on column ZS18.HT_EQ_STG_PICKUP.GOWHERE
  is '���̾���';
comment on column ZS18.HT_EQ_STG_PICKUP.IS_PICKUP
  is '�Ƿ�����';
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
  is '����������ϸ��';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.ID
  is '������ʶ';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.MAIN_CODE
  is '������������id';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.SP_CODE
  is '��������';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.SP_NAME
  is '��������';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.SP_STANDARD
  is '���';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.SP_MODEL
  is '�ͺ�';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.PICKUP_NUM
  is '�ɹ�����';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.SP_UNIT
  is '������λ';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.OWN_SECTION
  is 'ʹ�ò�λ�����նΣ�';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.REMARK
  is '��ע';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.STATUS
  is '״̬';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.OWN_EQUI
  is '�����豸';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.CREATE_ID
  is '������id';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.CREATE_TIME
  is '����ʱ��';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.IS_DEL
  is '�Ƿ�ɾ��';
comment on column ZS18.HT_EQ_STG_PICKUP_DETAIL.STORAGE
  is '�ֿ�ID';
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
  is '����״̬';
comment on column ZS18.HT_INNER_APRV_STATUS.ID
  is 'IDֵ';
comment on column ZS18.HT_INNER_APRV_STATUS.NAME
  is '����';
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
  is '�ܿ�״̬';
comment on column ZS18.HT_INNER_BOOL_DISPLAY.ID
  is 'IDֵ';
comment on column ZS18.HT_INNER_BOOL_DISPLAY.CTRL_NAME
  is '�ܿ�״̬';
comment on column ZS18.HT_INNER_BOOL_DISPLAY.STRG_NAME
  is '�����״̬';
comment on column ZS18.HT_INNER_BOOL_DISPLAY.ISSUE_NAME
  is '�·�״̬';
comment on column ZS18.HT_INNER_BOOL_DISPLAY.INSPECT_TYPE
  is '���ռ������';

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
  is '���ռ���Ʒ������';
comment on column ZS18.HT_INNER_INSPECT_GROUP.ID
  is 'IDֵ';
comment on column ZS18.HT_INNER_INSPECT_GROUP.NAME
  is '����';
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
  is 'ҳ��ӳ���';
comment on column ZS18.HT_INNER_MAP.URL
  is 'URL';
comment on column ZS18.HT_INNER_MAP.REMARK
  is '����';
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
  is 'ԭ�ϲֿ�';
comment on column ZS18.HT_INNER_MAT_DEPOT.ID
  is 'IDֵ';
comment on column ZS18.HT_INNER_MAT_DEPOT.NAME
  is '����';
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
  is '��¼ID';
comment on column ZS18.HT_PROD_MANUAL_RECODE.PARA_NAME
  is '������';
comment on column ZS18.HT_PROD_MANUAL_RECODE.VALUE
  is '����ֵ';
comment on column ZS18.HT_PROD_MANUAL_RECODE.B_TIME
  is '��ʼʱ��';
comment on column ZS18.HT_PROD_MANUAL_RECODE.E_TIME
  is '����ʱ��';
comment on column ZS18.HT_PROD_MANUAL_RECODE.SHIFT
  is '����';
comment on column ZS18.HT_PROD_MANUAL_RECODE.CREATOR
  is '��¼��Ա';
comment on column ZS18.HT_PROD_MANUAL_RECODE.CREATE_TIME
  is '��¼ʱ��';
comment on column ZS18.HT_PROD_MANUAL_RECODE.PARA_CODE
  is '��������';

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
  is '���ݼ��ȵ��������ƻ���ֳ��¶���������';
comment on column ZS18.HT_PROD_MONTH_PLAN.ID
  is '�¶���������Ψһ��ʶ';
comment on column ZS18.HT_PROD_MONTH_PLAN.PLAN_NAME
  is '�ƻ�����';
comment on column ZS18.HT_PROD_MONTH_PLAN.B_FLOW_STATUS
  is '�ƻ���������״̬ -1 δ�ύ���� 0������ ����ͨ�� ��δͨ��';
comment on column ZS18.HT_PROD_MONTH_PLAN.E_FLOW_STATUS
  is '�ƻ���������״̬';
comment on column ZS18.HT_PROD_MONTH_PLAN.ISSUED_STATUS
  is '�·�״̬ 0 δ�·� 1 ���·�';
comment on column ZS18.HT_PROD_MONTH_PLAN.CREATE_ID
  is '�����˱���';
comment on column ZS18.HT_PROD_MONTH_PLAN.CREATE_TIME
  is '����ʱ��';
comment on column ZS18.HT_PROD_MONTH_PLAN.MODIFY_ID
  is '�޸��˱���';
comment on column ZS18.HT_PROD_MONTH_PLAN.MODIFY_TIME
  is '�޸�ʱ��';
comment on column ZS18.HT_PROD_MONTH_PLAN.IS_DEL
  is 'ɾ����ʶ';
comment on column ZS18.HT_PROD_MONTH_PLAN.ADJUST_STATUS
  is '�ƻ���ϸ�Ƿ��е��� 0 δ���� 1 �ѵ���';
comment on column ZS18.HT_PROD_MONTH_PLAN.PLAN_TIME
  is '����ֶ�-�ƻ�ʱ��';
comment on column ZS18.HT_PROD_MONTH_PLAN.IS_VALID
  is '�Ƿ�����';
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
  is '�¶���������ƻ���ϸ��';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.ID
  is '�¶���������Ψһ��ʶ';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.MONTH_PLAN_ID
  is '�¶������ƻ���ʶ';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.PLAN_NO
  is '�ƻ���';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.PROD_CODE
  is '�����ƺ�';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.PLAN_YEAR
  is '�ƻ����';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.PROD_MONTH
  is '�����·�';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.PLAN_TYPE
  is '�����ƻ�����(0:�ճ�����,1:��ʱ����,2:���ղ���)';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.PLAN_OUTPUT
  is '���¼ƻ�������';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.ADJUST_OUTPUT
  is '���������';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.FINISH_OUTPUT
  is '��ɲ���';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.TECH_VERSION
  is '���հ汾��';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.CREATE_ID
  is '�����˱���';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.CREATOR
  is '������';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.CREATE_TIME
  is '����ʱ��';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.MODIFY_ID
  is '�޸��˱���';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.MODIFY_NAME
  is '�޸�������';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.MODIFY_TIME
  is '�޸�ʱ��';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.EXE_STATUS
  is 'ִ��״̬ 0δ�·� 1 ���·� 2 ������ 3 ��ͣ4���5����';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.IS_DEL
  is 'ɾ����ʶ';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.PLAN_SORT
  is '����';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.IS_VALID
  is '�Ƿ���������Ч';
comment on column ZS18.HT_PROD_MONTH_PLAN_DETAIL.PATH_DONE
  is '�Ƿ�ָ��������·��';
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
  is '���ն�/����α���';
comment on column ZS18.HT_PROD_REPORT.PLANNO
  is '�ƻ���';
comment on column ZS18.HT_PROD_REPORT.STARTTIME
  is '��ʼʱ��';
comment on column ZS18.HT_PROD_REPORT.ENDTIME
  is '����ʱ��';
comment on column ZS18.HT_PROD_REPORT.PROD_CODE
  is '��Ʒ����';
comment on column ZS18.HT_PROD_REPORT.TECH_PARA1
  is '����1';
comment on column ZS18.HT_PROD_REPORT.TECH_PARA2
  is '����2';

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
  is '��ʱ������Ϣ��';
comment on column ZS18.HT_SYS_SHIFT.SHIFT_CODE
  is '��ʱ����';
comment on column ZS18.HT_SYS_SHIFT.SHIFT_NAME
  is '��ʱ����';
comment on column ZS18.HT_SYS_SHIFT.WORKSHOP_ID
  is '����ְ�ܲ���';
comment on column ZS18.HT_SYS_SHIFT.BEGIN_TIME
  is '��ʱ��ʼʱ��';
comment on column ZS18.HT_SYS_SHIFT.END_TIME
  is '��ʱ����ʱ��';
comment on column ZS18.HT_SYS_SHIFT.CREATE_ID
  is '�����˱���';
comment on column ZS18.HT_SYS_SHIFT.CREATE_TIME
  is '����ʱ��';
comment on column ZS18.HT_SYS_SHIFT.MODIFY_ID
  is '�޸��˱���';
comment on column ZS18.HT_SYS_SHIFT.MODIFY_TIME
  is '�޸�ʱ��';
comment on column ZS18.HT_SYS_SHIFT.IS_VALID
  is '�Ƿ�����';
comment on column ZS18.HT_SYS_SHIFT.IS_DEL
  is 'ɾ����ʶ';
comment on column ZS18.HT_SYS_SHIFT.INTER_DAY
  is '�Ƿ����';
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
  is '���������Ϣ��';
comment on column ZS18.HT_SYS_TEAM.TEAM_CODE
  is '�������';
comment on column ZS18.HT_SYS_TEAM.TEAM_NAME
  is '��������';
comment on column ZS18.HT_SYS_TEAM.TEAM_TYPE
  is '�������ͣ������ֵ�ά����';
comment on column ZS18.HT_SYS_TEAM.WORKSHOP_ID
  is '����ְ�ܲ���';
comment on column ZS18.HT_SYS_TEAM.CREATE_ID
  is '�����˱���';
comment on column ZS18.HT_SYS_TEAM.CREATE_TIME
  is '����ʱ��';
comment on column ZS18.HT_SYS_TEAM.MODIFY_ID
  is '�޸��˱���';
comment on column ZS18.HT_SYS_TEAM.MODIFY_TIME
  is '�޸�ʱ��';
comment on column ZS18.HT_SYS_TEAM.IS_VALID
  is '�Ƿ�����';
comment on column ZS18.HT_SYS_TEAM.IS_DEL
  is '��ɾ����ʶ';
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
  is '�����͵��ް��Ű�һ�£��ּ��ұ��࣬���ް�����ұ��⻹�г��װ�';
comment on column ZS18.HT_PROD_SCHEDULE.ID
  is '�Ű��Ψһ��ʶ';
comment on column ZS18.HT_PROD_SCHEDULE.WORK_DATE
  is '�����Ű���';
comment on column ZS18.HT_PROD_SCHEDULE.WORK_SHOP_CODE
  is '�������';
comment on column ZS18.HT_PROD_SCHEDULE.SHIFT_CODE
  is '��ʱ����';
comment on column ZS18.HT_PROD_SCHEDULE.TEAM_CODE
  is '�������';
comment on column ZS18.HT_PROD_SCHEDULE.WORK_STAUS
  is '0:��Ϣ,1:����';
comment on column ZS18.HT_PROD_SCHEDULE.REMARK
  is '��ע';
comment on column ZS18.HT_PROD_SCHEDULE.TEAM_TYPE
  is '�Ű�����,0:����(����)�Ű�;1:�����Ű�';
comment on column ZS18.HT_PROD_SCHEDULE.CREATE_ID
  is '�����˱���';
comment on column ZS18.HT_PROD_SCHEDULE.CREATOR
  is '����������';
comment on column ZS18.HT_PROD_SCHEDULE.CREATE_TIME
  is '����ʱ��';
comment on column ZS18.HT_PROD_SCHEDULE.MODIFY_ID
  is '�޸��˱���';
comment on column ZS18.HT_PROD_SCHEDULE.MODIFY_TIME
  is '�޸�ʱ��';
comment on column ZS18.HT_PROD_SCHEDULE.IS_VALID
  is '��Ч��ʶ';
comment on column ZS18.HT_PROD_SCHEDULE.IS_DEL
  is '��ɾ����ʶ';
comment on column ZS18.HT_PROD_SCHEDULE.DATE_BEGIN
  is '����ʱ��';
comment on column ZS18.HT_PROD_SCHEDULE.DATE_END
  is '���ʱ��';
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
  add constraint FK_T_AM_WOR_��ʱ_�Ű�_T_AM_SHI foreign key (SHIFT_CODE)
  references ZS18.HT_SYS_SHIFT (SHIFT_CODE);
alter table ZS18.HT_PROD_SCHEDULE
  add constraint FK_T_AM_WOR_����_�Ű�_T_AM_TEA foreign key (TEAM_CODE)
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
  is '���ȵ��������ƻ�';
comment on column ZS18.HT_PROD_SEASON_PLAN.ID
  is '���ȵ��������ƻ�Ψһ��ʶ';
comment on column ZS18.HT_PROD_SEASON_PLAN.PLAN_NAME
  is '���ȵ��������ƻ�����';
comment on column ZS18.HT_PROD_SEASON_PLAN.FLOW_STATUS
  is '����״̬ -1 δ�ύ���� 0������ ����ͨ�� ��δͨ��';
comment on column ZS18.HT_PROD_SEASON_PLAN.ISSUED_STATUS
  is '�·�״̬ 0 δ�·� 1 ���·�';
comment on column ZS18.HT_PROD_SEASON_PLAN.CREATE_ID
  is '�����˱�ʶ';
comment on column ZS18.HT_PROD_SEASON_PLAN.CREATE_TIME
  is '����ʱ��';
comment on column ZS18.HT_PROD_SEASON_PLAN.MODIFY_ID
  is '�޸��˱�ʶ�����룩';
comment on column ZS18.HT_PROD_SEASON_PLAN.MODIFY
  is '�޸���';
comment on column ZS18.HT_PROD_SEASON_PLAN.MODIFY_TIME
  is '�޸�ʱ��';
comment on column ZS18.HT_PROD_SEASON_PLAN.DEPT_ID
  is '���ű�ʶ';
comment on column ZS18.HT_PROD_SEASON_PLAN.PLAN_YEAR
  is '���';
comment on column ZS18.HT_PROD_SEASON_PLAN.QUARTER
  is '���ȱ�ʶ';
comment on column ZS18.HT_PROD_SEASON_PLAN.UNIT
  is '�ƻ�������λ��ʶ';
comment on column ZS18.HT_PROD_SEASON_PLAN.REMARK
  is '���ȵ��������ƻ���ע';
comment on column ZS18.HT_PROD_SEASON_PLAN.IS_DEL
  is 'ɾ����ʶ';
comment on column ZS18.HT_PROD_SEASON_PLAN.TOTAL_OUTPUT
  is '�����ȼƻ��ܲ���';
comment on column ZS18.HT_PROD_SEASON_PLAN.PZ_CODE
  is 'ƾ֤��';
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
  is '���ȵ��¼ƻ���ϸ���';
comment on column ZS18.HT_PROD_SEASON_PLAN_DETAIL.ID
  is '���ȵ��¼ƻ�Ψһ��';
comment on column ZS18.HT_PROD_SEASON_PLAN_DETAIL.QUARTER_PLAN_ID
  is '����ID';
comment on column ZS18.HT_PROD_SEASON_PLAN_DETAIL.PROD_CODE
  is '��Ʒ�ƺţ���Ʒ�ȼ����Ʒ���';
comment on column ZS18.HT_PROD_SEASON_PLAN_DETAIL.PLAN_OUTPUT_1
  is '�ƻ���������';
comment on column ZS18.HT_PROD_SEASON_PLAN_DETAIL.PLAN_OUTPUT_2
  is '�ƻ���������';
comment on column ZS18.HT_PROD_SEASON_PLAN_DETAIL.PLAN_OUTPUT_3
  is '�ƻ���������';
comment on column ZS18.HT_PROD_SEASON_PLAN_DETAIL.TOTAL_OUTPUT
  is '�ƻ���������';
comment on column ZS18.HT_PROD_SEASON_PLAN_DETAIL.IS_DEL
  is 'ɾ����ʶ';
comment on column ZS18.HT_PROD_SEASON_PLAN_DETAIL.OUTPUT_1_ADJUST
  is '�ƻ�����1�Ƿ��е���';
comment on column ZS18.HT_PROD_SEASON_PLAN_DETAIL.OUTPUT_2_ADJUST
  is '�ƻ�����2�Ƿ��е���';
comment on column ZS18.HT_PROD_SEASON_PLAN_DETAIL.OUTPUT_3_ADJUST
  is '�ƻ�����3�Ƿ��е���';
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
  is '����ʶID';
comment on column ZS18.HT_PROD_SHIFTCHG.INSPECT_DATE
  is '����';
comment on column ZS18.HT_PROD_SHIFTCHG.WORKSHOP_CODE
  is '����';
comment on column ZS18.HT_PROD_SHIFTCHG.SHIFT_CODE
  is '��ʱ';
comment on column ZS18.HT_PROD_SHIFTCHG.TEAM_CODE
  is '����';
comment on column ZS18.HT_PROD_SHIFTCHG.PLAN_NO
  is '�ƻ���';
comment on column ZS18.HT_PROD_SHIFTCHG.PROD_CODE
  is '��Ʒ����';
comment on column ZS18.HT_PROD_SHIFTCHG.OUTPUT_VL
  is '�������';
comment on column ZS18.HT_PROD_SHIFTCHG.SHIFT_STATUS
  is '״̬ ';
comment on column ZS18.HT_PROD_SHIFTCHG.B_TIME
  is '������ʼʱ��';
comment on column ZS18.HT_PROD_SHIFTCHG.E_TIME
  is '����ʱ��ʱ��';
comment on column ZS18.HT_PROD_SHIFTCHG.SHIFT_ID
  is '������';
comment on column ZS18.HT_PROD_SHIFTCHG.SUCC_ID
  is '�Ӱ���';
comment on column ZS18.HT_PROD_SHIFTCHG.REMARK
  is '��ע';
comment on column ZS18.HT_PROD_SHIFTCHG.DEVICESTATUS
  is '�豸�������';
comment on column ZS18.HT_PROD_SHIFTCHG.QLT_STATUS
  is '�����������';
comment on column ZS18.HT_PROD_SHIFTCHG.SCEAN_STATUS
  is '�ֳ����';
comment on column ZS18.HT_PROD_SHIFTCHG.OUTPLUS
  is '������ͷ';

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
  is '�ӱ�ID';
comment on column ZS18.HT_PROD_SHIFTCHG_DETAIL.SHIFT_MAIN_ID
  is '��ID';
comment on column ZS18.HT_PROD_SHIFTCHG_DETAIL.MATER_CODE
  is '���ϱ���';
comment on column ZS18.HT_PROD_SHIFTCHG_DETAIL.MATER_VL
  is '����ֵ';
comment on column ZS18.HT_PROD_SHIFTCHG_DETAIL.BZ_UNIT
  is '��λ';
comment on column ZS18.HT_PROD_SHIFTCHG_DETAIL.REMARK
  is '��ע';

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
  is '�������̻�����Ϣ
';
comment on column ZS18.HT_PUB_APRV_FLOWINFO.ID
  is 'Ψһ����ID';
comment on column ZS18.HT_PUB_APRV_FLOWINFO.TBR_ID
  is '���id';
comment on column ZS18.HT_PUB_APRV_FLOWINFO.TBR_NAME
  is '���name';
comment on column ZS18.HT_PUB_APRV_FLOWINFO.TB_BM_ID
  is '�����id';
comment on column ZS18.HT_PUB_APRV_FLOWINFO.TB_BM_NAME
  is '�����name';
comment on column ZS18.HT_PUB_APRV_FLOWINFO.TB_DATE
  is '����ʱ�䣬��������';
comment on column ZS18.HT_PUB_APRV_FLOWINFO.MODULENAME
  is '�������ͱ���';
comment on column ZS18.HT_PUB_APRV_FLOWINFO.URL
  is '������¼url';
comment on column ZS18.HT_PUB_APRV_FLOWINFO.BUSIN_ID
  is 'ҵ������id';
comment on column ZS18.HT_PUB_APRV_FLOWINFO.STATE
  is '����״̬0������ ������� ��������';
comment on column ZS18.HT_PUB_APRV_FLOWINFO.TB_ZT
  is '����';
comment on column ZS18.HT_PUB_APRV_FLOWINFO.REMARK
  is '��ע';
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
  is '�������ͱ���';
comment on column ZS18.HT_PUB_APRV_MODEL.INDEX_NO
  is '˳���';
comment on column ZS18.HT_PUB_APRV_MODEL.ROLE
  is '������ɫ';
comment on column ZS18.HT_PUB_APRV_MODEL.FLOW_NAME
  is '���ͻ���';
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
  is '�������������';
comment on column ZS18.HT_PUB_APRV_OPINION.ID
  is 'Ψһ��ʾ';
comment on column ZS18.HT_PUB_APRV_OPINION.GONGWEN_ID
  is 'ҵ����������id';
comment on column ZS18.HT_PUB_APRV_OPINION.USERID
  is '�û�id';
comment on column ZS18.HT_PUB_APRV_OPINION.USERNAME
  is '�û���';
comment on column ZS18.HT_PUB_APRV_OPINION.COMMENTS
  is '�������';
comment on column ZS18.HT_PUB_APRV_OPINION.ROLENAME
  is '��ɫ';
comment on column ZS18.HT_PUB_APRV_OPINION.OPINIONTIME
  is '�����д����';
comment on column ZS18.HT_PUB_APRV_OPINION.STATUS
  is '״̬ 0δ����1δͨ��2ͨ��';
comment on column ZS18.HT_PUB_APRV_OPINION.POS
  is '����';
comment on column ZS18.HT_PUB_APRV_OPINION.WORKITEMID
  is '���ͻ���';
comment on column ZS18.HT_PUB_APRV_OPINION.ISENABLE
  is '��ǰ�����ܷ񱻴�������ɫ����������һ������ɺ���Ϊ1';
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
  is '���ݱ��˳���';
comment on column ZS18.HT_PUB_APRV_TYPE.PZ_TYPE
  is '�������ͱ���';
comment on column ZS18.HT_PUB_APRV_TYPE.PZ_TYPE_NAME
  is '������������';
comment on column ZS18.HT_PUB_APRV_TYPE.APRV_TABLE
  is 'ҵ���Ӧ���ݱ���';
comment on column ZS18.HT_PUB_APRV_TYPE.APRV_TABSEG
  is 'ҵ���Ӧ���ݱ������ֶ���';
comment on column ZS18.HT_PUB_APRV_TYPE.BUZ_ID
  is 'ҵ����ID�ֶ���';
comment on column ZS18.HT_PUB_APRV_TYPE.PLSQL
  is 'ҵ����ϸ��ѯSQL����';

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
  is 'Ψһ��ʶ';
comment on column ZS18.HT_PUB_INSPECT_PROCESS.PROCESS_CODE
  is '���������֯����1λ+������2λ+���ն�2λ+2λ��ˮ��';
comment on column ZS18.HT_PUB_INSPECT_PROCESS.PROCESS_NAME
  is '����';
comment on column ZS18.HT_PUB_INSPECT_PROCESS.REMARK
  is '��ע';
comment on column ZS18.HT_PUB_INSPECT_PROCESS.CREATE_ID
  is '�����˱�ʶ';
comment on column ZS18.HT_PUB_INSPECT_PROCESS.CREATE_TIME
  is '����ʱ��';
comment on column ZS18.HT_PUB_INSPECT_PROCESS.MODIFY_ID
  is '�޸��˱�ʶ';
comment on column ZS18.HT_PUB_INSPECT_PROCESS.MODIFY_TIME
  is '�޸�ʱ��';
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
  is '���Ϸ��༰��Ϣ��';
comment on column ZS18.HT_PUB_MATERIEL.ID
  is '���Ϸ���id';
comment on column ZS18.HT_PUB_MATERIEL.MATERIAL_CODE
  is '���Ϸ������';
comment on column ZS18.HT_PUB_MATERIEL.MATERIAL_NAME
  is '���Ϸ�������';
comment on column ZS18.HT_PUB_MATERIEL.TYPE_CODE
  is '�������';
comment on column ZS18.HT_PUB_MATERIEL.SPEC_VAL
  is '���';
comment on column ZS18.HT_PUB_MATERIEL.MODEL_VAL
  is '�ͺ�';
comment on column ZS18.HT_PUB_MATERIEL.IS_DEL
  is '�Ƿ�ɾ��';
comment on column ZS18.HT_PUB_MATERIEL.TYPE_FLAG
  is '���Ϸ����ʶ(YL-ԭ��  FL- ����  BJ-��Ʒ����  XL-�㾫����)';
comment on column ZS18.HT_PUB_MATERIEL.UNIT_CODE
  is '������λ';
comment on column ZS18.HT_PUB_MATERIEL.DATA_ORIGIN_FLAG
  is '������Դ��ʶ0δѡ1�����Ʒ��2�̳�ԭ�Ͽ�3��Դԭ�Ͽ�4�̳����ԭ�Ͽ�5��Դ���ԭ�Ͽ�';
comment on column ZS18.HT_PUB_MATERIEL.PK_MATERIAL
  is '�ⲿϵͳ������ʶ';
comment on column ZS18.HT_PUB_MATERIEL.FACTORY
  is '����';
comment on column ZS18.HT_PUB_MATERIEL.MAT_YEAR
  is '���';
comment on column ZS18.HT_PUB_MATERIEL.MAT_CATEGORY
  is '���';
comment on column ZS18.HT_PUB_MATERIEL.MAT_TYPE
  is '����';
comment on column ZS18.HT_PUB_MATERIEL.MAT_LEVEL
  is '�ȼ�';
comment on column ZS18.HT_PUB_MATERIEL.MAT_VARIETY
  is 'Ʒ��';
comment on column ZS18.HT_PUB_MATERIEL.MAT_PACK
  is '����';
comment on column ZS18.HT_PUB_MATERIEL.MAT_PLACE
  is '����';
comment on column ZS18.HT_PUB_MATERIEL.REMARK
  is '��ע';
comment on column ZS18.HT_PUB_MATERIEL.MAT_TYPE2
  is '���2(���̡�ɹ����)';
comment on column ZS18.HT_PUB_MATERIEL.MAT_PLACE_NAME
  is '��������';
comment on column ZS18.HT_PUB_MATERIEL.MAT_PROVINCE
  is 'ʡ��';
comment on column ZS18.HT_PUB_MATERIEL.MAT_CITY
  is '����';
comment on column ZS18.HT_PUB_MATERIEL.PK_MARBASCLASS
  is '�����������';
comment on column ZS18.HT_PUB_MATERIEL.COSTPRICE
  is '�ɱ���';
comment on column ZS18.HT_PUB_MATERIEL.PK_MATTAXES
  is '˰';
comment on column ZS18.HT_PUB_MATERIEL.NORIGTAXPRICE
  is 'ԭֵ';
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
  is '���Ϸ�����Ϣ��';
comment on column ZS18.HT_PUB_MATTREE.ID
  is '���Ϸ���id';
comment on column ZS18.HT_PUB_MATTREE.MATTREE_CODE
  is '���Ϸ������';
comment on column ZS18.HT_PUB_MATTREE.MATTREE_NAME
  is '���Ϸ�������';
comment on column ZS18.HT_PUB_MATTREE.IS_VALID
  is '�Ƿ���Ч';
comment on column ZS18.HT_PUB_MATTREE.IS_DEL
  is '�Ƿ�ɾ��';
comment on column ZS18.HT_PUB_MATTREE.PK_CLASS
  is '�����ʶ';
comment on column ZS18.HT_PUB_MATTREE.PK_PARENT_CLASS
  is '���������ʶ';
comment on column ZS18.HT_PUB_MATTREE.PARENT_CODE
  is '�����������';
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
  is '�ڵ�ID';
comment on column ZS18.HT_PUB_PATH_NODE.SECTION_CODE
  is '�������նα���';
comment on column ZS18.HT_PUB_PATH_NODE.NODENAME
  is '�ڵ���';
comment on column ZS18.HT_PUB_PATH_NODE.ORDERS
  is '˳���';
comment on column ZS18.HT_PUB_PATH_NODE.DESCRIPT
  is '����';
comment on column ZS18.HT_PUB_PATH_NODE.CREATE_TIME
  is '����ʱ��';
comment on column ZS18.HT_PUB_PATH_NODE.TAG
  is '�ڵ��Ӧ���Ʊ�ǩ';

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
  is '���ն�';
comment on column ZS18.HT_PUB_PATH_PLAN.PATHCODE
  is '���ն�·����';
comment on column ZS18.HT_PUB_PATH_PLAN.PATHNAME
  is '·����';
comment on column ZS18.HT_PUB_PATH_PLAN.DESCRIPT
  is '����';
comment on column ZS18.HT_PUB_PATH_PLAN.CREATE_TIME
  is '����ʱ��';
comment on column ZS18.HT_PUB_PATH_PLAN.PROD_PLAN
  is '�ƻ���';
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
  is '���ն�';
comment on column ZS18.HT_PUB_PATH_SECTION.PATHCODE
  is '���ն�·����';
comment on column ZS18.HT_PUB_PATH_SECTION.PATHNAME
  is '·����';
comment on column ZS18.HT_PUB_PATH_SECTION.DESCRIPT
  is '����';
comment on column ZS18.HT_PUB_PATH_SECTION.CREATE_TIME
  is '����ʱ��';
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
  is '��Ʒ��ʶ';
comment on column ZS18.HT_PUB_PROD_DESIGN.PROD_CODE
  is '��Ʒ����';
comment on column ZS18.HT_PUB_PROD_DESIGN.PROD_NAME
  is '��Ʒ����';
comment on column ZS18.HT_PUB_PROD_DESIGN.PACK_NAME
  is '��װ���';
comment on column ZS18.HT_PUB_PROD_DESIGN.HAND_MODE
  is '�ӹ���ʽ';
comment on column ZS18.HT_PUB_PROD_DESIGN.TECH_STDD_CODE
  is '������׼����';
comment on column ZS18.HT_PUB_PROD_DESIGN.MATER_FORMULA_CODE
  is 'ԭ���䷽����';
comment on column ZS18.HT_PUB_PROD_DESIGN.AUX_FORMULA_CODE
  is '�����䷽����';
comment on column ZS18.HT_PUB_PROD_DESIGN.COAT_FORMULA_CODE
  is 'Ϳ���㾫�����䷽����';
comment on column ZS18.HT_PUB_PROD_DESIGN.CREATEOR_ID
  is '������ID';
comment on column ZS18.HT_PUB_PROD_DESIGN.CREATE_TIME
  is '����ʱ��';
comment on column ZS18.HT_PUB_PROD_DESIGN.MODIFY_ID
  is '�޸��˱�ʶ';
comment on column ZS18.HT_PUB_PROD_DESIGN.MODIFY_TIME
  is '�޸�ʱ��';
comment on column ZS18.HT_PUB_PROD_DESIGN.STANDARD_VALUE
  is '��׼ֵ';
comment on column ZS18.HT_PUB_PROD_DESIGN.QLT_CODE
  is '������׼����';
comment on column ZS18.HT_PUB_PROD_DESIGN.B_FLOW_STATUS
  is '����״̬ -1 δ�ύ���� 0������ ����ͨ�� ��δͨ��';
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
  is '���ղ�������a)	���������֯����1λ+������2λ+���ն�2λ+����2λ+3λ��ˮ��';
comment on column ZS18.HT_PUB_TECH_PARA.PARA_NAME
  is '���ղ�������';
comment on column ZS18.HT_PUB_TECH_PARA.PARA_UNIT
  is '��λ';
comment on column ZS18.HT_PUB_TECH_PARA.REMARK
  is '��ע';
comment on column ZS18.HT_PUB_TECH_PARA.CREATE_ID
  is '�����˱�ʶ';
comment on column ZS18.HT_PUB_TECH_PARA.CREATE_TIME
  is '����ʱ��';
comment on column ZS18.HT_PUB_TECH_PARA.MODIFY_ID
  is '�޸��˱�ʶ';
comment on column ZS18.HT_PUB_TECH_PARA.MODIFY_TIME
  is '�޸�ʱ��';
comment on column ZS18.HT_PUB_TECH_PARA.PARA_TYPE
  is '����';
comment on column ZS18.HT_PUB_TECH_PARA.EQUIP_CODE
  is '�����豸';
comment on column ZS18.HT_PUB_TECH_PARA.SET_TAG
  is '�趨��ǩ';
comment on column ZS18.HT_PUB_TECH_PARA.VALUE_TAG
  is '������ǩ';
comment on column ZS18.HT_PUB_TECH_PARA.IS_KEY
  is '���豸�����б�ʶ�Ƿ�Ϊ��Ҫ����';
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
  is 'Ψһ��ʶ';
comment on column ZS18.HT_PUB_TECH_SECTION.SECTION_CODE
  is '���նα���';
comment on column ZS18.HT_PUB_TECH_SECTION.SECTION_NAME
  is '����';
comment on column ZS18.HT_PUB_TECH_SECTION.IS_VALID
  is '�Ƿ���Ч';
comment on column ZS18.HT_PUB_TECH_SECTION.IS_DEL
  is 'ɾ��';
comment on column ZS18.HT_PUB_TECH_SECTION.REMARK
  is '��ע';
comment on column ZS18.HT_PUB_TECH_SECTION.CREATE_ID
  is '�����˱�ʶ';
comment on column ZS18.HT_PUB_TECH_SECTION.CREATE_TIME
  is '����ʱ��';
comment on column ZS18.HT_PUB_TECH_SECTION.MODIFY_ID
  is '�޸��˱�ʶ';
comment on column ZS18.HT_PUB_TECH_SECTION.MODIFY_TIME
  is '�޸�ʱ��';
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
  is '�����䷽��Ϣ����';
comment on column ZS18.HT_QA_AUX_FORMULA.ID
  is '�����䷽��Ϣ��id';
comment on column ZS18.HT_QA_AUX_FORMULA.FORMULA_NAME
  is '�䷽����';
comment on column ZS18.HT_QA_AUX_FORMULA.FORMULA_CODE
  is '�䷽���';
comment on column ZS18.HT_QA_AUX_FORMULA.PROD_CODE
  is '��Ʒ���';
comment on column ZS18.HT_QA_AUX_FORMULA.STANDARD_VOL
  is '��׼�汾��';
comment on column ZS18.HT_QA_AUX_FORMULA.B_DATE
  is 'ִ������';
comment on column ZS18.HT_QA_AUX_FORMULA.E_DATE
  is '��������';
comment on column ZS18.HT_QA_AUX_FORMULA.CONTROL_STATUS
  is '�ܿ�״̬';
comment on column ZS18.HT_QA_AUX_FORMULA.CREATE_ID
  is '������id';
comment on column ZS18.HT_QA_AUX_FORMULA.CREATE_DATE
  is '��������';
comment on column ZS18.HT_QA_AUX_FORMULA.CREATE_DEPT_ID
  is '���Ʋ���';
comment on column ZS18.HT_QA_AUX_FORMULA.MODIFY_ID
  is '�޸���id';
comment on column ZS18.HT_QA_AUX_FORMULA.MODIFY_TIME
  is '�޸�ʱ��';
comment on column ZS18.HT_QA_AUX_FORMULA.IS_VALID
  is '�Ƿ���Ч';
comment on column ZS18.HT_QA_AUX_FORMULA.IS_DEL
  is '�Ƿ�ɾ��';
comment on column ZS18.HT_QA_AUX_FORMULA.REMARK
  is '��ע';
comment on column ZS18.HT_QA_AUX_FORMULA.FLOW_STATUS
  is '����״̬';

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
  is '�����䷽��Ϣ�ӱ�';
comment on column ZS18.HT_QA_AUX_FORMULA_DETAIL.ID
  is '�����䷽��Ϣ�ӱ�id';
comment on column ZS18.HT_QA_AUX_FORMULA_DETAIL.FORMULA_CODE
  is '�����䷽����id';
comment on column ZS18.HT_QA_AUX_FORMULA_DETAIL.MATER_CODE
  is '���ϱ��';
comment on column ZS18.HT_QA_AUX_FORMULA_DETAIL.MATER_TYPE
  is '�������ͱ���';
comment on column ZS18.HT_QA_AUX_FORMULA_DETAIL.AUX_SORT
  is '���';
comment on column ZS18.HT_QA_AUX_FORMULA_DETAIL.IS_DEL
  is '�Ƿ�ɾ��';
comment on column ZS18.HT_QA_AUX_FORMULA_DETAIL.AUX_SCALE
  is '����';
comment on column ZS18.HT_QA_AUX_FORMULA_DETAIL.REMARK
  is '��ע';

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
  is 'Ϳ��Һ�䷽��Ϣ����';
comment on column ZS18.HT_QA_COAT_FORMULA.ID
  is 'Ϳ��Һ�䷽��Ϣ��id';
comment on column ZS18.HT_QA_COAT_FORMULA.FORMULA_NAME
  is '�䷽����';
comment on column ZS18.HT_QA_COAT_FORMULA.FORMULA_CODE
  is '�䷽���';
comment on column ZS18.HT_QA_COAT_FORMULA.PROD_CODE
  is '��Ʒ���';
comment on column ZS18.HT_QA_COAT_FORMULA.STANDARD_VOL
  is '��׼�汾��';
comment on column ZS18.HT_QA_COAT_FORMULA.B_DATE
  is 'ִ������';
comment on column ZS18.HT_QA_COAT_FORMULA.E_DATE
  is '��������';
comment on column ZS18.HT_QA_COAT_FORMULA.CONTROL_STATUS
  is '�ܿ�״̬';
comment on column ZS18.HT_QA_COAT_FORMULA.CREATE_ID
  is '������id';
comment on column ZS18.HT_QA_COAT_FORMULA.CREATE_DATE
  is '��������';
comment on column ZS18.HT_QA_COAT_FORMULA.MODIFY_ID
  is '�޸���id';
comment on column ZS18.HT_QA_COAT_FORMULA.MODIFY_TIME
  is '�޸�ʱ��';
comment on column ZS18.HT_QA_COAT_FORMULA.CREATE_DEPT_ID
  is '���Ʋ���';
comment on column ZS18.HT_QA_COAT_FORMULA.IS_VALID
  is '�Ƿ���Ч';
comment on column ZS18.HT_QA_COAT_FORMULA.IS_DEL
  is '�Ƿ�ɾ��';
comment on column ZS18.HT_QA_COAT_FORMULA.REMARK
  is '��ע';
comment on column ZS18.HT_QA_COAT_FORMULA.FORMULA_XJ
  is '�㾫����';
comment on column ZS18.HT_QA_COAT_FORMULA.W_TOTAL
  is '�㾫����';
comment on column ZS18.HT_QA_COAT_FORMULA.FORMULA_TPY
  is 'Ϳ��Һ����';
comment on column ZS18.HT_QA_COAT_FORMULA.FLOW_STATUS
  is '����״̬';
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
  is 'Ϳ��Һ�䷽��Ϣ�ӱ�';
comment on column ZS18.HT_QA_COAT_FORMULA_DETAIL.ID
  is 'Ϳ��Һ�䷽��Ϣ�ӱ�id';
comment on column ZS18.HT_QA_COAT_FORMULA_DETAIL.FORMULA_CODE
  is 'Ϳ��Һ�䷽����id';
comment on column ZS18.HT_QA_COAT_FORMULA_DETAIL.CLASS_NAME
  is '�������ƣ����ࣩ';
comment on column ZS18.HT_QA_COAT_FORMULA_DETAIL.COAT_SCALE
  is '����';
comment on column ZS18.HT_QA_COAT_FORMULA_DETAIL.NEED_SIZE
  is '����������';
comment on column ZS18.HT_QA_COAT_FORMULA_DETAIL.COAT_SORT
  is '���';
comment on column ZS18.HT_QA_COAT_FORMULA_DETAIL.IS_VALID
  is '�Ƿ���Ч';
comment on column ZS18.HT_QA_COAT_FORMULA_DETAIL.IS_DEL
  is '�Ƿ�ɾ��';
comment on column ZS18.HT_QA_COAT_FORMULA_DETAIL.REMARK
  is '��ע';
comment on column ZS18.HT_QA_COAT_FORMULA_DETAIL.COAT_FLAG
  is '�䷽����(XJ--�㾫���ϣ� TPY--Ϳ��Һ�䷽)';
comment on column ZS18.HT_QA_COAT_FORMULA_DETAIL.MATER_CODE
  is '���ϱ��';

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
  is 'ԭ���䷽��Ϣ����';
comment on column ZS18.HT_QA_MATER_FORMULA.ID
  is 'ԭ���䷽��Ϣ��id';
comment on column ZS18.HT_QA_MATER_FORMULA.FORMULA_NAME
  is '�䷽����';
comment on column ZS18.HT_QA_MATER_FORMULA.FORMULA_CODE
  is '�䷽���';
comment on column ZS18.HT_QA_MATER_FORMULA.PROD_CODE
  is '��Ʒ���';
comment on column ZS18.HT_QA_MATER_FORMULA.STANDARD_VOL
  is '��׼�汾��';
comment on column ZS18.HT_QA_MATER_FORMULA.B_DATE
  is 'ִ������';
comment on column ZS18.HT_QA_MATER_FORMULA.E_DATE
  is '��������';
comment on column ZS18.HT_QA_MATER_FORMULA.CONTROL_STATUS
  is '�ܿ�״̬';
comment on column ZS18.HT_QA_MATER_FORMULA.CABO_SUM
  is '�̹�����';
comment on column ZS18.HT_QA_MATER_FORMULA.PIECES_SUM
  is 'ˮ����Ƭ����';
comment on column ZS18.HT_QA_MATER_FORMULA.CREATE_ID
  is '������id';
comment on column ZS18.HT_QA_MATER_FORMULA.CREATE_DATE
  is '��������';
comment on column ZS18.HT_QA_MATER_FORMULA.CREATE_DEPT_ID
  is '���Ʋ���';
comment on column ZS18.HT_QA_MATER_FORMULA.MODIFY_ID
  is '�޸���id';
comment on column ZS18.HT_QA_MATER_FORMULA.MODIFY_TIME
  is '�޸�ʱ��';
comment on column ZS18.HT_QA_MATER_FORMULA.IS_VALID
  is '�Ƿ���Ч';
comment on column ZS18.HT_QA_MATER_FORMULA.IS_DEL
  is '�Ƿ�ɾ��';
comment on column ZS18.HT_QA_MATER_FORMULA.REMARK
  is '��ע';
comment on column ZS18.HT_QA_MATER_FORMULA.STEM_NUM
  is '�̹��ܼ�';
comment on column ZS18.HT_QA_MATER_FORMULA.PIECE_NUM
  is '��Ƭ�ܼ�';
comment on column ZS18.HT_QA_MATER_FORMULA.SMALLS_NUM
  is '��ĩ�ܼ�';
comment on column ZS18.HT_QA_MATER_FORMULA.STICKS_NUM
  is '�̰��ܼ�';
comment on column ZS18.HT_QA_MATER_FORMULA.ADJUST
  is '�Ƿ��ǵ����䷽��';
comment on column ZS18.HT_QA_MATER_FORMULA.FLOW_STATUS
  is '����״̬';
comment on column ZS18.HT_QA_MATER_FORMULA.PZ_CODE
  is '����ƾ֤';
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
  is 'ԭ���䷽��Ϣ�ӱ�';
comment on column ZS18.HT_QA_MATER_FORMULA_DETAIL.ID
  is 'ԭ���䷽��Ϣ�ӱ�id';
comment on column ZS18.HT_QA_MATER_FORMULA_DETAIL.FORMULA_CODE
  is 'ԭ���䷽�������';
comment on column ZS18.HT_QA_MATER_FORMULA_DETAIL.MATER_CODE
  is '���ϱ��';
comment on column ZS18.HT_QA_MATER_FORMULA_DETAIL.BATCH_SIZE
  is '��Ͷ����';
comment on column ZS18.HT_QA_MATER_FORMULA_DETAIL.FRONT_GROUP
  is '������';
comment on column ZS18.HT_QA_MATER_FORMULA_DETAIL.MATER_SORT
  is '˳���';
comment on column ZS18.HT_QA_MATER_FORMULA_DETAIL.IS_DEL
  is '�Ƿ�ɾ��';
comment on column ZS18.HT_QA_MATER_FORMULA_DETAIL.REMARK
  is '��ע';
comment on column ZS18.HT_QA_MATER_FORMULA_DETAIL.MATER_FLAG
  is '�䷽���Ϸ��ࣨ�̹���YG����Ƭ��SP��';
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
  is '��Ʒ';
comment on column ZS18.HT_QLT_AUTO_EVENT.PARA_CODE
  is '���յ����';
comment on column ZS18.HT_QLT_AUTO_EVENT.SORT
  is '���ͣ���ֵ���ϸ��ʡ�������';
comment on column ZS18.HT_QLT_AUTO_EVENT.DEAL
  is '����ʽ';
comment on column ZS18.HT_QLT_AUTO_EVENT.SCENE
  is 'ԭ��';
comment on column ZS18.HT_QLT_AUTO_EVENT.TEAM
  is '����';
comment on column ZS18.HT_QLT_AUTO_EVENT.OTHERS
  is '��������˵��';
comment on column ZS18.HT_QLT_AUTO_EVENT.DONE
  is '�Ƿ񼺴������';
comment on column ZS18.HT_QLT_AUTO_EVENT.SCORE
  is '�۷�';
comment on column ZS18.HT_QLT_AUTO_EVENT.EVENT_TIME
  is '�¼�ʱ��';
comment on column ZS18.HT_QLT_AUTO_EVENT.CREATOR
  is '��¼��Ա';
comment on column ZS18.HT_QLT_AUTO_EVENT.CREATE_TIME
  is '��¼ʱ��';
comment on column ZS18.HT_QLT_AUTO_EVENT.VALUE
  is 'ֵ';

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
  is '��������';
comment on column ZS18.HT_QLT_COLLECTION.PERIODIC
  is '�ɼ�����';
comment on column ZS18.HT_QLT_COLLECTION.DESCRIPT
  is '����';
comment on column ZS18.HT_QLT_COLLECTION.VARMONITOR_TAG
  is '��ر�����ǩ';
comment on column ZS18.HT_QLT_COLLECTION.HEAD_DELAY
  is '��ͷ��ʱ';
comment on column ZS18.HT_QLT_COLLECTION.TAIL_DELAY
  is '��β��ʱ';
comment on column ZS18.HT_QLT_COLLECTION.BATCH_HEAD_DELAY
  is '��ͷ��ʱ';
comment on column ZS18.HT_QLT_COLLECTION.BATCH_TAIL_DELAY
  is '��β��ʱ';
comment on column ZS18.HT_QLT_COLLECTION.WEIGHT
  is 'Ȩ��';
comment on column ZS18.HT_QLT_COLLECTION.GAP_DELAY
  is '����ƫ��';
comment on column ZS18.HT_QLT_COLLECTION.CUTOFF_POINT_TAG
  is '�������ǩ';
comment on column ZS18.HT_QLT_COLLECTION.CUTOFF_RST
  is '�����ж�����';
comment on column ZS18.HT_QLT_COLLECTION.CUTOFF_RST_VALUE
  is '�����ж�ֵ';
comment on column ZS18.HT_QLT_COLLECTION.TAILLOGIC_TAG
  is '��ͷ��β�ж���ǩ';
comment on column ZS18.HT_QLT_COLLECTION.TAILLOGIC_RST
  is '��ͷ��β�ж�����';
comment on column ZS18.HT_QLT_COLLECTION.TAILLOGIC_RST_VALUE
  is '�ж�ֵ';
comment on column ZS18.HT_QLT_COLLECTION.CUTOFF_TIMEGAP
  is '�����ж�ʱ��';
comment on column ZS18.HT_QLT_COLLECTION.PARA_TYPE
  is '����ɼ������ͣ����ԶԱȷ���';

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
  is '��Ʒ';
comment on column ZS18.HT_QLT_CUTOFF_RECORD.SECTION_CODE
  is '���նα���';
comment on column ZS18.HT_QLT_CUTOFF_RECORD.DEAL
  is '����ʽ';
comment on column ZS18.HT_QLT_CUTOFF_RECORD.SCENE
  is 'ԭ��';
comment on column ZS18.HT_QLT_CUTOFF_RECORD.TEAM
  is '����';
comment on column ZS18.HT_QLT_CUTOFF_RECORD.OTHERS
  is '��������˵��';
comment on column ZS18.HT_QLT_CUTOFF_RECORD.DONE
  is '�Ƿ񼺴������';
comment on column ZS18.HT_QLT_CUTOFF_RECORD.SCORE
  is '�۷�';
comment on column ZS18.HT_QLT_CUTOFF_RECORD.EVENT_TIME
  is '�¼�ʱ��';
comment on column ZS18.HT_QLT_CUTOFF_RECORD.CREATOR
  is '��¼��Ա';
comment on column ZS18.HT_QLT_CUTOFF_RECORD.CREATE_TIME
  is '��¼ʱ��';
comment on column ZS18.HT_QLT_CUTOFF_RECORD.CUTOFF_TIME
  is '����ʱ��';

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
  is '��������ID';
comment on column ZS18.HT_QLT_DATA_RECORD.PARA_CODE
  is '���յ����';
comment on column ZS18.HT_QLT_DATA_RECORD.AVG
  is '��ֵ';
comment on column ZS18.HT_QLT_DATA_RECORD.COUNT
  is '�ܵ���';
comment on column ZS18.HT_QLT_DATA_RECORD.MIN
  is '���ֵ';
comment on column ZS18.HT_QLT_DATA_RECORD.MAX
  is '��Сֵ';
comment on column ZS18.HT_QLT_DATA_RECORD.QUANUM
  is '�ϸ����';
comment on column ZS18.HT_QLT_DATA_RECORD.QUARATE
  is '�ϸ���';
comment on column ZS18.HT_QLT_DATA_RECORD.UPCOUNT
  is '��������';
comment on column ZS18.HT_QLT_DATA_RECORD.UPRATE
  is '��������';
comment on column ZS18.HT_QLT_DATA_RECORD.DOWNCOUNT
  is '��������';
comment on column ZS18.HT_QLT_DATA_RECORD.DOWNRATE
  is '��������';
comment on column ZS18.HT_QLT_DATA_RECORD.STDDEV
  is '��׼��';
comment on column ZS18.HT_QLT_DATA_RECORD.ABSDEV
  is '���Բ�';
comment on column ZS18.HT_QLT_DATA_RECORD.IS_GAP
  is '�Ƿ����';
comment on column ZS18.HT_QLT_DATA_RECORD.CPK
  is 'CPK';
comment on column ZS18.HT_QLT_DATA_RECORD.B_TIME
  is '��ʼʱ��';
comment on column ZS18.HT_QLT_DATA_RECORD.SHIFT
  is '����';
comment on column ZS18.HT_QLT_DATA_RECORD.GAP_TIME
  is '����ʱ��';
comment on column ZS18.HT_QLT_DATA_RECORD.E_TIME
  is '����ʱ��';

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
  is '���ռ���¼ID';
comment on column ZS18.HT_QLT_INSPECT_EVENT.INSPECT_CODE
  is '���ռ����Ŀ����';
comment on column ZS18.HT_QLT_INSPECT_EVENT.PROD_CODE
  is '��Ʒ��';
comment on column ZS18.HT_QLT_INSPECT_EVENT.TEAM
  is '����';
comment on column ZS18.HT_QLT_INSPECT_EVENT.STATUS
  is '״̬';
comment on column ZS18.HT_QLT_INSPECT_EVENT.REASON
  is 'ԭ�����';
comment on column ZS18.HT_QLT_INSPECT_EVENT.SCENE
  is '�ֳ���¼';
comment on column ZS18.HT_QLT_INSPECT_EVENT.DEAL
  is '����ʽ';
comment on column ZS18.HT_QLT_INSPECT_EVENT.REMARK
  is '��������˵��';
comment on column ZS18.HT_QLT_INSPECT_EVENT.ISDONE
  is '�Ƿ񼺴������';
comment on column ZS18.HT_QLT_INSPECT_EVENT.SCORE
  is '�۷�';
comment on column ZS18.HT_QLT_INSPECT_EVENT.EVENT_TIME
  is '�¼�ʱ��';
comment on column ZS18.HT_QLT_INSPECT_EVENT.CREATOR
  is '��¼��Ա';
comment on column ZS18.HT_QLT_INSPECT_EVENT.CREATE_TIME
  is '��¼ʱ��';
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
  is '���ռ����Ŀ��';
comment on column ZS18.HT_QLT_INSPECT_PROJ.INSPECT_CODE
  is '������Ŀ����';
comment on column ZS18.HT_QLT_INSPECT_PROJ.INSPECT_NAME
  is '������Ŀ����';
comment on column ZS18.HT_QLT_INSPECT_PROJ.INSPECT_TYPE
  is '�������';
comment on column ZS18.HT_QLT_INSPECT_PROJ.INSPECT_GROUP
  is '�μ����� ��Ϊ���̼���ʱΪ������ Ϊ��Ʒ����ʱΪ���й�';
comment on column ZS18.HT_QLT_INSPECT_PROJ.UNIT
  is '��λ';
comment on column ZS18.HT_QLT_INSPECT_PROJ.IS_VALID
  is '�Ƿ���Ч';
comment on column ZS18.HT_QLT_INSPECT_PROJ.IS_DEL
  is '�Ƿ�ɾ��';
comment on column ZS18.HT_QLT_INSPECT_PROJ.REMARK
  is '��ע';
comment on column ZS18.HT_QLT_INSPECT_PROJ.CREATE_ID
  is '������id';
comment on column ZS18.HT_QLT_INSPECT_PROJ.CREATE_TIME
  is '����ʱ��';
comment on column ZS18.HT_QLT_INSPECT_PROJ.MODIFY_ID
  is '�޸���id';
comment on column ZS18.HT_QLT_INSPECT_PROJ.MODIFY_TIME
  is '�޸�ʱ��';
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
  is '���ռ�����';
comment on column ZS18.HT_QLT_INSPECT_RECORD.PROD_CODE
  is '��Ʒ';
comment on column ZS18.HT_QLT_INSPECT_RECORD.SHIFT_ID
  is '��ʱ';
comment on column ZS18.HT_QLT_INSPECT_RECORD.TEAM_ID
  is '����';
comment on column ZS18.HT_QLT_INSPECT_RECORD.INSPECT_VALUE
  is '���ֵ';
comment on column ZS18.HT_QLT_INSPECT_RECORD.RECORD_TIME
  is '����ʱ��';
comment on column ZS18.HT_QLT_INSPECT_RECORD.CREAT_ID
  is '��¼��';
comment on column ZS18.HT_QLT_INSPECT_RECORD.CREATE_TIME
  is '��¼ʱ��';
comment on column ZS18.HT_QLT_INSPECT_RECORD.REMARK
  is '˵��';

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
  is '���ռ����Ŀ��׼';
comment on column ZS18.HT_QLT_INSPECT_STDD.INSPECT_CODE
  is '������Ŀ����';
comment on column ZS18.HT_QLT_INSPECT_STDD.UPPER_VALUE
  is '����';
comment on column ZS18.HT_QLT_INSPECT_STDD.LOWER_VALUE
  is '����';
comment on column ZS18.HT_QLT_INSPECT_STDD.MINUS_SCORE
  is '�����ϻ���ʱ���ο۷�';
comment on column ZS18.HT_QLT_INSPECT_STDD.IS_DEL
  is '�Ƿ�ɾ��';
comment on column ZS18.HT_QLT_INSPECT_STDD.REMARK
  is '��ע';
comment on column ZS18.HT_QLT_INSPECT_STDD.CREATE_ID
  is '������id';
comment on column ZS18.HT_QLT_INSPECT_STDD.CREATE_TIME
  is '����ʱ��';
comment on column ZS18.HT_QLT_INSPECT_STDD.PROD_CODE
  is '��Ʒ���� ȷ���Ƿ���Ҫÿ����Ʒ�����趨';
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
  is '��������׼����';
comment on column ZS18.HT_QLT_STDD_CODE.ID
  is 'Ψһ��ʶ';
comment on column ZS18.HT_QLT_STDD_CODE.QLT_CODE
  is '������׼����';
comment on column ZS18.HT_QLT_STDD_CODE.STANDARD_VOL
  is '��׼�汾��';
comment on column ZS18.HT_QLT_STDD_CODE.B_DATE
  is 'ִ������';
comment on column ZS18.HT_QLT_STDD_CODE.E_DATE
  is '��������';
comment on column ZS18.HT_QLT_STDD_CODE.CREATE_ID
  is '�����˱�ʶ';
comment on column ZS18.HT_QLT_STDD_CODE.CREATOR
  is '������';
comment on column ZS18.HT_QLT_STDD_CODE.CREATE_DATE
  is '��������';
comment on column ZS18.HT_QLT_STDD_CODE.CREATE_DEPT
  is '���Ʋ���';
comment on column ZS18.HT_QLT_STDD_CODE.MODIFY_ID
  is '�޸��˱�ʶ';
comment on column ZS18.HT_QLT_STDD_CODE.MODIFY_TIME
  is '�޸�ʱ��';
comment on column ZS18.HT_QLT_STDD_CODE.IS_DEL
  is 'ɾ����ʶ';
comment on column ZS18.HT_QLT_STDD_CODE.REMARK
  is '��ע';
comment on column ZS18.HT_QLT_STDD_CODE.QLT_NAME
  is '������׼��';
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
  is '������׼�ӱ�';
comment on column ZS18.HT_QLT_STDD_CODE_DETAIL.ID
  is 'Ψһ��ʶ';
comment on column ZS18.HT_QLT_STDD_CODE_DETAIL.QLT_CODE
  is '������׼����ID';
comment on column ZS18.HT_QLT_STDD_CODE_DETAIL.PARA_CODE
  is '������';
comment on column ZS18.HT_QLT_STDD_CODE_DETAIL.QLT_TYPE
  is '��������';
comment on column ZS18.HT_QLT_STDD_CODE_DETAIL.IS_DEL
  is 'ɾ����ʶ';
comment on column ZS18.HT_QLT_STDD_CODE_DETAIL.REMARK
  is '��ע';
comment on column ZS18.HT_QLT_STDD_CODE_DETAIL.PARA_NAME
  is '������';
comment on column ZS18.HT_QLT_STDD_CODE_DETAIL.VALUE
  is '��׼ֵ';
comment on column ZS18.HT_QLT_STDD_CODE_DETAIL.EER_DEV
  is '�ж�����';
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
  is '��Ʒ������������ݿ��Աȷ�Ϻ�ֱ��������ϵͳͬ��';
comment on column ZS18.HT_STRG_AUX.ID
  is '����  ';
comment on column ZS18.HT_STRG_AUX.OUT_DATE
  is '���������';
comment on column ZS18.HT_STRG_AUX.ORDER_SN
  is '���ݺ�  SM + ������ + ˳��ţ�3λ��';
comment on column ZS18.HT_STRG_AUX.MONTHPLANNO
  is '���������ƻ�ID';
comment on column ZS18.HT_STRG_AUX.EXPIRED_DATE
  is 'ʧЧ����';
comment on column ZS18.HT_STRG_AUX.MODIFY_TIME
  is '�޸�ʱ��';
comment on column ZS18.HT_STRG_AUX.AUDIT_MARK
  is '����״̬';
comment on column ZS18.HT_STRG_AUX.WARE_HOUSE_ID
  is '�ֿ�ID';
comment on column ZS18.HT_STRG_AUX.DEPT_ID
  is '����ID';
comment on column ZS18.HT_STRG_AUX.CREATOR_ID
  is '������ID';
comment on column ZS18.HT_STRG_AUX.REMARK
  is '��ע';
comment on column ZS18.HT_STRG_AUX.STRG_TYPE
  is '��������� 0 ���� 1 ���';
comment on column ZS18.HT_STRG_AUX.ISSUER_ID
  is '�·���ԱID';
comment on column ZS18.HT_STRG_AUX.ISSUE_STATUS
  is '�·�״̬ 0δ�´� 1 ���´�';
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
  is '���ϱ����ƻ���ϸ��';
comment on column ZS18.HT_STRG_AUX_SUB.ID
  is 'Ψһ��ʶ';
comment on column ZS18.HT_STRG_AUX_SUB.MATER_CODE
  is '���ϱ��룬�������Զ�����';
comment on column ZS18.HT_STRG_AUX_SUB.MATER_NAME
  is '��������';
comment on column ZS18.HT_STRG_AUX_SUB.STORAGE
  is '�ֿ�ID';
comment on column ZS18.HT_STRG_AUX_SUB.REMARK
  is '��ע';
comment on column ZS18.HT_STRG_AUX_SUB.UNIT_CODE
  is '��λ';
comment on column ZS18.HT_STRG_AUX_SUB.IS_DEL
  is 'ɾ����ʶ';
comment on column ZS18.HT_STRG_AUX_SUB.MAIN_CODE
  is '����ID';
comment on column ZS18.HT_STRG_AUX_SUB.NUM
  is '���ü���';

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
  is '��Ʒ������������ݿ��Աȷ�Ϻ�ֱ��������ϵͳͬ��';
comment on column ZS18.HT_STRG_MATERIA.ID
  is '����  ';
comment on column ZS18.HT_STRG_MATERIA.OUT_DATE
  is '���������';
comment on column ZS18.HT_STRG_MATERIA.ORDER_SN
  is '���ݺ�  SM + ������ + ˳��ţ�3λ��';
comment on column ZS18.HT_STRG_MATERIA.MONTHPLANNO
  is '���������ƻ�ID';
comment on column ZS18.HT_STRG_MATERIA.EXPIRED_DATE
  is 'ʧЧ����';
comment on column ZS18.HT_STRG_MATERIA.MODIFY_TIME
  is '�޸�ʱ��';
comment on column ZS18.HT_STRG_MATERIA.AUDIT_MARK
  is '����״̬';
comment on column ZS18.HT_STRG_MATERIA.WARE_HOUSE_ID
  is '�ֿ�ID';
comment on column ZS18.HT_STRG_MATERIA.DEPT_ID
  is '����ID';
comment on column ZS18.HT_STRG_MATERIA.CREATOR_ID
  is '������ID';
comment on column ZS18.HT_STRG_MATERIA.REMARK
  is '��ע';
comment on column ZS18.HT_STRG_MATERIA.BATCHNUM
  is 'Ͷ������';
comment on column ZS18.HT_STRG_MATERIA.STRG_TYPE
  is '��������� 0 ���� 1 ���';
comment on column ZS18.HT_STRG_MATERIA.ISSUER_ID
  is '�·���ԱID';
comment on column ZS18.HT_STRG_MATERIA.ISSUE_STATUS
  is '�·�״̬ 0δ�´� 1 ���´�';
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
  is 'ԭ�ϱ����ƻ���ϸ��';
comment on column ZS18.HT_STRG_MATER_SUB.ID
  is 'Ψһ��ʶ';
comment on column ZS18.HT_STRG_MATER_SUB.MATER_CODE
  is '���ϱ��룬�������Զ�����';
comment on column ZS18.HT_STRG_MATER_SUB.MATER_NAME
  is '��������';
comment on column ZS18.HT_STRG_MATER_SUB.STORAGE
  is '�ֿ�ID';
comment on column ZS18.HT_STRG_MATER_SUB.ORIGINAL_DEMAND
  is '����������';
comment on column ZS18.HT_STRG_MATER_SUB.REAL_DEMAND
  is 'ʵ��������';
comment on column ZS18.HT_STRG_MATER_SUB.REMARK
  is '��ע';
comment on column ZS18.HT_STRG_MATER_SUB.UNIT_CODE
  is '��λ';
comment on column ZS18.HT_STRG_MATER_SUB.IS_DEL
  is 'ɾ����ʶ';
comment on column ZS18.HT_STRG_MATER_SUB.MATER_FLAG
  is '�����䷽���Ϸ��ࣨ�̹���YG����Ƭ��SP��';
comment on column ZS18.HT_STRG_MATER_SUB.MAIN_CODE
  is '����ID';

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
  is '���ⵥ��ͷ����';
comment on column ZS18.HT_STRG_PRODFLOW.PK_CUSTOMER
  is '����';
comment on column ZS18.HT_STRG_PRODFLOW.DBILLDATE
  is '��������';
comment on column ZS18.HT_STRG_PRODFLOW.CCUSTOMERID
  is '�����ͻ�';
comment on column ZS18.HT_STRG_PRODFLOW.NAME
  is '�ͻ�����';
comment on column ZS18.HT_STRG_PRODFLOW.COTHERWHID
  is '���ֿ�';
comment on column ZS18.HT_STRG_PRODFLOW.CMATERIALVID
  is '��Ʒ����';
comment on column ZS18.HT_STRG_PRODFLOW.matername
  is '��Ʒ����';
comment on column ZS18.HT_STRG_PRODFLOW.MATERIALSPEC
  is '��Ʒ���';
comment on column ZS18.HT_STRG_PRODFLOW.CUNITID
  is '��λ����';
comment on column ZS18.HT_STRG_PRODFLOW.unitname
  is '��λ����';
comment on column ZS18.HT_STRG_PRODFLOW.CASSCUSTID
  is '�ͻ����';
comment on column ZS18.HT_STRG_PRODFLOW.rackname
  is '��λ����';
comment on column ZS18.HT_STRG_PRODFLOW.CMATERIALOID
  is '��Ʒ���';
comment on column ZS18.HT_STRG_PRODFLOW.CLOCATIONID
  is '��λ���';
comment on column ZS18.HT_STRG_PRODFLOW.NASSISTNUM
  is 'ʵ������';
comment on column ZS18.HT_STRG_PRODFLOW.NCOUNTNUM
  is '����';
comment on column ZS18.HT_STRG_PRODFLOW.NNUM
  is 'ʵ��������';
comment on column ZS18.HT_STRG_PRODFLOW.CGENERALBID
  is 'Ψһ����';
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
  is '��֯�������빫˾����3λ��007��+ һ�����ţ�3λ��ˮ��+ �Ӳ��ţ�2λ�������Ӳ���Ĭ��Ϊ00��';
comment on column ZS18.HT_SVR_ORG_GROUP.F_NAME
  is '��֯��������';
comment on column ZS18.HT_SVR_ORG_GROUP.F_PRITYPE
  is '����';
comment on column ZS18.HT_SVR_ORG_GROUP.F_PATH
  is '·��';
comment on column ZS18.HT_SVR_ORG_GROUP.F_WEIGHT
  is 'Ȩ��';
comment on column ZS18.HT_SVR_ORG_GROUP.F_PARENTID
  is '������ʶ';
comment on column ZS18.HT_SVR_ORG_GROUP.F_ROLE
  is '���Ż�����ɫ';
comment on column ZS18.HT_SVR_ORG_GROUP.F_KEY
  is '������ƽ̨ID';
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
  is '���˵���';
comment on column ZS18.HT_SVR_PRT_MENU.ID
  is 'ID';
comment on column ZS18.HT_SVR_PRT_MENU.NAME
  is '����';
comment on column ZS18.HT_SVR_PRT_MENU.PID
  is '���˵�ID';
comment on column ZS18.HT_SVR_PRT_MENU.MENULEVEL
  is '��ʶ�����˵�';
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
  is '�˵�';
comment on column ZS18.HT_SVR_SYS_MENU.F_MAPID
  is 'URLӳ��ID';
comment on column ZS18.HT_SVR_SYS_MENU.F_DESCRIPT
  is '����';
comment on column ZS18.HT_SVR_SYS_MENU.F_PID
  is '���˵�ID';
comment on column ZS18.HT_SVR_SYS_MENU.F_TYPE
  is 'Ȩ�����ͣ� 0�˵�  1 ����   2     ';
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
  is 'Ψһ��ʾ�ɹ�˾���루2λ��+ Ա����ţ�5λ��';
comment on column ZS18.HT_SVR_USER.NAME
  is '��Ա����';
comment on column ZS18.HT_SVR_USER.WEIGHT
  is 'Ȩ��';
comment on column ZS18.HT_SVR_USER.PARENTID
  is '���ڵ�id';
comment on column ZS18.HT_SVR_USER.MOBILE
  is '�ֻ�';
comment on column ZS18.HT_SVR_USER.PHONE
  is '�绰';
comment on column ZS18.HT_SVR_USER.RTXID
  is '����';
comment on column ZS18.HT_SVR_USER.GENDER
  is '�Ա�';
comment on column ZS18.HT_SVR_USER.LOGINNAME
  is '�����ݱ�ʶ';
comment on column ZS18.HT_SVR_USER.PASSWORD
  is '����';
comment on column ZS18.HT_SVR_USER.EMAIL
  is '�����ʼ�';
comment on column ZS18.HT_SVR_USER.LEVELGROUPID
  is '��֯����id';
comment on column ZS18.HT_SVR_USER.IS_LOCAL
  is '�Ƿ񱾵�';
comment on column ZS18.HT_SVR_USER.IS_DEL
  is 'ɾ����ʶ';
comment on column ZS18.HT_SVR_USER.IS_SYNC
  is '�Ƿ�ͬ��';
comment on column ZS18.HT_SVR_USER.C_STATE
  is '�Ƿ�����';
comment on column ZS18.HT_SVR_USER.DESCRIPTION
  is '����';
comment on column ZS18.HT_SVR_USER.ROLE
  is '��ɫID';
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
  is '������';
comment on column ZS18.HT_SYS_EXCEL_BOOK.F_PARA
  is '�������';
comment on column ZS18.HT_SYS_EXCEL_BOOK.F_SYNCHRO_TIME
  is '����ʱ��';
comment on column ZS18.HT_SYS_EXCEL_BOOK.F_SOURCE
  is 'ģ���ַ';
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
  is 'ID��ʶ';
comment on column ZS18.HT_SYS_EXCEL_SEG.F_BOOK_ID
  is '��ģ��ID��ʶ';
comment on column ZS18.HT_SYS_EXCEL_SEG.F_SHEET
  is '��������';
comment on column ZS18.HT_SYS_EXCEL_SEG.F_SQL
  is '��ѯSQL�ű�';
comment on column ZS18.HT_SYS_EXCEL_SEG.F_DES
  is 'Ŀ��λ��';
comment on column ZS18.HT_SYS_EXCEL_SEG.F_DESX
  is 'Ŀ����';
comment on column ZS18.HT_SYS_EXCEL_SEG.F_DESY
  is 'Ŀ����';
comment on column ZS18.HT_SYS_EXCEL_SEG.F_SHEETINDEX
  is '����������';
comment on column ZS18.HT_SYS_EXCEL_SEG.F_SYNCHRO_TIME
  is 'ʱ��';
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
  is 'Ψһ��ʶ';
comment on column ZS18.HT_TECH_STDD_CODE.TECH_CODE
  is '���ձ�׼����';
comment on column ZS18.HT_TECH_STDD_CODE.PROD_CODE
  is '��Ʒ����';
comment on column ZS18.HT_TECH_STDD_CODE.STANDARD_VOL
  is '��׼�汾��';
comment on column ZS18.HT_TECH_STDD_CODE.B_DATE
  is 'ִ������';
comment on column ZS18.HT_TECH_STDD_CODE.E_DATE
  is '��������';
comment on column ZS18.HT_TECH_STDD_CODE.CONTROL_STATUS
  is '�ܿ�״̬';
comment on column ZS18.HT_TECH_STDD_CODE.CREATE_ID
  is '�����˱�ʶ';
comment on column ZS18.HT_TECH_STDD_CODE.CREATE_DATE
  is '��������';
comment on column ZS18.HT_TECH_STDD_CODE.CREATE_DEPT_ID
  is '���Ʋ���ID';
comment on column ZS18.HT_TECH_STDD_CODE.MODIFY_ID
  is '�޸��˱�ʶ';
comment on column ZS18.HT_TECH_STDD_CODE.MODIFY_TIME
  is '�޸�ʱ��';
comment on column ZS18.HT_TECH_STDD_CODE.IS_DEL
  is 'ɾ����ʶ';
comment on column ZS18.HT_TECH_STDD_CODE.REMARK
  is '��ע';
comment on column ZS18.HT_TECH_STDD_CODE.TECH_NAME
  is '���ձ�׼��';
comment on column ZS18.HT_TECH_STDD_CODE.FLOW_STATUS
  is '����״̬';
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
  is 'Ψһ��ʶ';
comment on column ZS18.HT_TECH_STDD_CODE_DETAIL.TECH_CODE
  is '���ձ�׼��';
comment on column ZS18.HT_TECH_STDD_CODE_DETAIL.PARA_CODE
  is '������';
comment on column ZS18.HT_TECH_STDD_CODE_DETAIL.PARA_TYPE
  is '��������';
comment on column ZS18.HT_TECH_STDD_CODE_DETAIL.IS_DEL
  is 'ɾ����ʶ';
comment on column ZS18.HT_TECH_STDD_CODE_DETAIL.REMARK
  is '��ע';
comment on column ZS18.HT_TECH_STDD_CODE_DETAIL.VALUE
  is '��׼ֵ';
comment on column ZS18.HT_TECH_STDD_CODE_DETAIL.UPPER_LIMIT
  is '����';
comment on column ZS18.HT_TECH_STDD_CODE_DETAIL.LOWER_LIMIT
  is '����';
comment on column ZS18.HT_TECH_STDD_CODE_DETAIL.EER_DEV
  is '�ʲ�';
comment on column ZS18.HT_TECH_STDD_CODE_DETAIL.UNIT
  is '��λ';
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
