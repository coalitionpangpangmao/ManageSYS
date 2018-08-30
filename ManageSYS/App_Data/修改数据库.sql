-- Create table
create table HT_PUB_PATH_SECTION
(
  SECTION_CODE VARCHAR2(10) not null,
  PATHCODE     VARCHAR2(30) not null,
  PATHNAME     VARCHAR2(100),
  DESCRIPT     VARCHAR2(255),
  IS_DEL       VARCHAR2(1) default 0,
  CREATE_TIME  VARCHAR2(19),
  FLOW_STATUS  VARCHAR2(2),
  ID           INTEGER
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
comment on column HT_PUB_PATH_SECTION.SECTION_CODE
  is '工艺段';
comment on column HT_PUB_PATH_SECTION.PATHCODE
  is '工艺段路径码';
comment on column HT_PUB_PATH_SECTION.PATHNAME
  is '路径名';
comment on column HT_PUB_PATH_SECTION.DESCRIPT
  is '描述';
comment on column HT_PUB_PATH_SECTION.CREATE_TIME
  is '创建时间';
-- Create/Recreate primary, unique and foreign key constraints 
alter table HT_PUB_PATH_SECTION
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
alter table HT_PUB_PATH_SECTION
  add constraint FK_PATH_SECTION foreign key (SECTION_CODE)
  references HT_PUB_TECH_SECTION (SECTION_CODE) on delete cascade;
  
  
 create sequence PathSection_ID_SEQ
minvalue 1
maxvalue 9999999999999999999999999999
start with 1
increment by 1
nocache; 

  CREATE OR REPLACE TRIGGER PathSection_INS_TRG BEFORE INSERT ON HT_PUB_PATH_SECTION FOR EACH ROW
when (NEW.ID IS NULL)
BEGIN
SELECT PathSection_ID_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;

create sequence PRODSHIFT_ID_SEQ
minvalue 1
maxvalue 9999999999999999999999999999
start with 1
increment by 1
nocache;

CREATE OR REPLACE TRIGGER PRODSHIFT_INS_TRG BEFORE INSERT ON HT_PROD_SHIFTCHG_DETAIL FOR EACH ROW
when (NEW.OUTPUT_ID IS NULL)
BEGIN
SELECT PRODSHIFT_ID_SEQ.NEXTVAL INTO :NEW.OUTPUT_ID FROM DUAL;
END;

alter table HT_PROD_SHIFTCHG_DETAIL
  add constraint UK_PROD_SHIFT unique (SHIFT_MAIN_ID, MATER_CODE)
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
  
  
  create table HT_SYS_EXCEL_BOOK
(
  F_ID           INTEGER not null,
  F_NAME         VARCHAR2(50) not null,
  F_PARA         VARCHAR2(5),
  F_SYNCHRO_TIME VARCHAR2(19),
  F_SOURCE       VARCHAR2(255),
  F_TYPE         VARCHAR2(50)
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
comment on column HT_SYS_EXCEL_BOOK.F_NAME
  is '报表名';
comment on column HT_SYS_EXCEL_BOOK.F_PARA
  is '报表参数';
comment on column HT_SYS_EXCEL_BOOK.F_SYNCHRO_TIME
  is '创建时间';
comment on column HT_SYS_EXCEL_BOOK.F_SOURCE
  is '模版地址';
comment on column HT_SYS_EXCEL_BOOK.F_TYPE
  is '报表类型';
-- Create/Recreate primary, unique and foreign key constraints 
alter table HT_SYS_EXCEL_BOOK
  add constraint PK_BOOK primary key (F_ID)
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
alter table HT_SYS_EXCEL_BOOK
  add constraint UK_BOOK_NAME unique (F_NAME)
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

create table HT_PROD_MONTH_PLAN_DETAIL
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
  PATH_CODE     VARCHAR2(50)/*修改了该字段*/
)


-- Create table
create table HT_PROD_MANUAL_RECORD
(
  VALUE       FLOAT not null,
  TEAM        VARCHAR2(2),
  CREATOR     VARCHAR2(20) not null,
  CREATE_TIME VARCHAR2(10) not null,
  IS_DEL      VARCHAR2(1) default 0 not null,
  PARA_CODE   VARCHAR2(15) not null,
  PROD_CODE   VARCHAR2(7) not null,
  PLAN_NO     VARCHAR2(20)
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
-- Add comments to the columns 
comment on column HT_PROD_MANUAL_RECORD.VALUE
  is '参数值';
comment on column HT_PROD_MANUAL_RECORD.TEAM
  is '班组';
comment on column HT_PROD_MANUAL_RECORD.CREATOR
  is '记录人员';
comment on column HT_PROD_MANUAL_RECORD.CREATE_TIME
  is '记录时间';
comment on column HT_PROD_MANUAL_RECORD.PARA_CODE
  is '参数编码';
comment on column HT_PROD_MANUAL_RECORD.PROD_CODE
  is '产品';
comment on column HT_PROD_MANUAL_RECORD.PLAN_NO
  is '计划号';
-- Create/Recreate primary, unique and foreign key constraints 
alter table HT_PROD_MANUAL_RECORD
  add constraint FK_PLAN_NO foreign key (PLAN_NO)
  references HT_PROD_MONTH_PLAN_DETAIL (PLAN_NO);
alter table HT_PROD_MANUAL_RECORD
  add constraint FK_PROD_CODE foreign key (PROD_CODE)
  references HT_PUB_PROD_DESIGN (PROD_CODE) on delete cascade;
alter table HT_PROD_MANUAL_RECORD
  add constraint FK_TEAM_CODE foreign key (TEAM)
  references HT_SYS_TEAM (TEAM_CODE) on delete cascade;
