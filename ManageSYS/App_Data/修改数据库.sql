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
