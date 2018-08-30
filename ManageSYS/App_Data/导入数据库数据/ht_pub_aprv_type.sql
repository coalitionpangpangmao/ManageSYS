insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('10', '辅料配方审批', 'HT_QA_AUX_FORMULA', 'FLOW_STATUS', 'FORMULA_CODE', 'select r.material_name as 物料名称,s.mater_code as 物料编码,s.aux_scale as 投料比例 from    ht_qa_Aux_formula_detail s left join ht_pub_materiel r  on r.material_code = s.mater_code where s.formula_code = ''@BUZ_ID'' and s.is_del = ''0''            ', 'AAATVwAAHAABWwbAAA');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('11', '涂布液料配方审批', 'HT_QA_COAT_FORMULA', 'FLOW_STATUS', 'FORMULA_CODE', 'select r.material_name as 物料名称,s.mater_code as 物料编码,s.class_name as 分组名称,s.coat_scale as 投料比例,s.need_size as 调配所需量,s.coat_flag as 配方分类 from    ht_qa_coat_formula_detail s left join ht_pub_materiel r  on r.material_code = s.mater_code where s.formula_code = ''@BUZ_ID'' and s.is_del = ''0''                                                 ', 'AAATVwAAHAABWwbAAB');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('09', '辅料出入库审批', 'ht_strg_Aux', 'AUDIT_MARK', 'ORDER_SN', 'select STORAGE as  仓库,unit_code as  计量单位,mater_code as   辅料编码,mater_name as  辅料名称,NUM as   领料量,ID,  main_code from ht_strg_aux_sub where main_code =''@BUZ_ID'' and is_del = ''0''                    ', 'AAATVwAAHAABWwcAAA');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('12', '季度生产计划审批', 'HT_PROD_SEASON_PLAN', 'FLOW_STATUS', 'ID', 'select distinct r.plan_year as 年份, r.quarter as 季度, s.prod_name as 产品,t.plan_output_1 as 计划产量1, t.plan_output_2 as 计划产量2,t.plan_output_3 as 计划产量3 from ht_prod_SEASON_PLAN_detail t  left join ht_pub_prod_design s on s.prod_code = t.prod_code left join ht_prod_season_plan r on r.id = t.quarter_plan_id where t.QUARTER_PLAN_ID = ''@BUZ_ID'' and t.is_del = ''0''                                                     ', 'AAATVwAAHAABWwcAAB');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('15', '维修计划审批', 'HT_EQ_RP_PLAN', 'FLOW_STATUS', 'PZ_CODE', 'select t.mech_area as 区域,t.equipment_id as 设备名称,t.reason as 维修原因,t.content as 维修内容,t.exp_finish_time as 期望完成时间,t.STATUS as 状态,t.remark as 备注 ,t.ID  from HT_EQ_RP_PLAN_detail t   where t.main_id = ''@BUZ_ID'' and t.is_del = ''0''                                           ', 'AAATVwAAHAABWwcAAC');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('16', '润滑计划审批', 'HT_EQ_LB_PLAN', 'FLOW_STATUS', 'PZ_CODE', 'select section as  工段,equipment_id as  设备名称,STATUS as状态,EXP_FINISH_TIME as 期望完成时间,  remark as  备注,ID from ht_eq_lb_plan_detail  where main_id = ''@BUZ_ID'' and is_del = ''0''                ', 'AAATVwAAHAABWwcAAD');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('08', '原料出入库审批', 'ht_strg_materia', 'AUDIT_MARK', 'ORDER_SN', 'select STORAGE as  仓库,mater_flag as   类型 ,unit_code as  计量单位,mater_code as   原料编码,mater_name as  原料名称,original_demand as   领料量,ID,  main_code from ht_strg_mater_sub where main_code =''@BUZ_ID'' and is_del = ''0''                   ', 'AAATVwAAHAABWweAAA');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('14', '维保计划审批', 'HT_EQ_MT_PLAN', 'FLOW_STATUS', 'PZ_CODE', 'select t.mech_area as 区域,t.equipment_id as 设备名称,t.reason as 维修保养原因,t.content as 维修保养内容,t.exp_finish_time as 期望完成时间,t.STATUS as 状态,t.remark as 备注 ,t.ID  from ht_eq_mt_plan_detail t   where t.main_id = ''@BUZ_ID'' and t.is_del = ''0''                            ', 'AAATVwAAHAABWweAAB');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('02', '产品审批', 'HT_PUB_PROD_DESIGN', 'B_FLOW_STATUS', 'PROD_CODE', 'select g1.prod_name as 产品名,g2.formula_name as 原料配方,g3.formula_name as 辅料配方,g4.formula_name  as 涂布液配方,g5.tech_name  as 工艺标准,g6.qlt_name as 质量考核标准 from Ht_pub_prod_design g1 left join ht_qa_mater_formula g2 on g1.mater_formula_code = g2.formula_code left join ht_qa_aux_formula g3 on g1.aux_formula_code = g3.formula_code left join ht_qa_coat_formula g4 on g1.coat_formula_code = g4.formula_code left join ht_tech_stdd_code g5 on g1.tech_stdd_code = g5.tech_code left join ht_qlt_stdd_code g6 on g1.qlt_code = g6.qlt_code where g1.prod_code = ''@BUZ_ID''                                           ', 'AAATVwAAHAABWwfAAB');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('03', '原料配方审批', 'HT_QA_MATER_FORMULA', 'FLOW_STATUS', 'FORMULA_CODE', 'select r.material_name as 物料名称,s.mater_code as 物料编码,s.batch_size as 批投料量,s.mater_sort as 物料分类 from    ht_qa_mater_formula_detail s left join ht_pub_materiel r  on r.material_code = s.mater_code where s.formula_code = ''@BUZ_ID''  and r.is_del = ''0''                ', 'AAATVwAAHAABWwfAAC');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('04', '工艺标准审批', 'HT_TECH_STDD_CODE', 'FLOW_STATUS', 'TECH_CODE', 'select tech_code as 工艺标准,t.section_name as 工艺段,  para_name as 参数名,VALUE as 标准值,UPPER_LIMIT as 上限,LOWER_LIMIT as 下限,EER_DEV as 允差,UNIT as 单位  from ht_tech_stdd_code_detail r left join ht_pub_tech_para s on s.para_code = r.para_code left join ht_pub_tech_section t on t.section_code = substr(r.para_code,1,5) where r.IS_DEL = ''0'' and    r.tech_code = ''@BUZ_ID'' and s.is_del = ''0'' order by t.section_name                                     ', 'AAATVwAAHAABWwfAAD');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('05', '工艺路径审批', 'HT_PUB_PATH_SECTION', 'FLOW_STATUS', 'ID', 'select s.orders as 序号,s.nodename as 节点名,substr(r.pathcode,s.orders,1) as 是否选择 from HT_PUB_PATH_SECTION r left join ht_pub_path_node s on s.section_code = r.section_code and s.is_del = ''0''  where r.ID = ''@BUZ_ID'' order by s.orders          ', 'AAATVwAAHAABWwfAAE');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('06', '生产计划审批', 'HT_PROD_MONTH_PLAN', 'B_FLOW_STATUS', 'ID', 'select t.plan_Sort as 顺序号, t.plan_no as 计划号, s.prod_name as 产品规格,t.plan_output as 计划产量 from ht_prod_month_plan_detail t  left join ht_pub_prod_design s on s.prod_code = t.prod_code where t.month_plan_id =  ''@BUZ_ID''  and t.is_del = ''0''              ', 'AAATVwAAHAABWwfAAF');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('07', '质量考核标准审批', 'HT_QLT_STDD_CODE', 'FLOW_STATUS', 'QLT_CODE', 'select r.qlt_code as 工艺标准,t.section_name as 工艺段,  s.para_name as 参数名,q.name as 类型,r.value as 标准值,r.upper as 上限,r.lower as 下限,r.minus_score as 扣分,s.para_unit as 单位  from ht_qlt_stdd_code_detail r left join ht_pub_tech_para s on s.para_code = r.para_code left join ht_pub_tech_section t on t.section_code = substr(r.para_code,1,5) left join ht_inner_qlt_type q on q.id = r.qlt_type where r.IS_DEL = ''0'' and    r.qlt_code = ''@BUZ_ID'' and s.is_del = ''0'' order by t.section_name                       ', 'AAATVwAAHAABWwfAAG');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('13', '备件领用审批', 'HT_EQ_STG_PICKUP', 'FLOW_STATUS', 'PZ_CODE', 'select t.storage as 仓库,t.Sp_Code as 编码,t.sp_name as 名称,t.sp_standard as 规格,t.sp_model as 型号,t.sp_unit as 单位,t.pickup_num as 数量,t.own_section as 工艺段,t.remark as 备注,t.ID from ht_eq_stg_pickup_detail t  where MAIN_CODE = ''@BUZ_ID'' and IS_DEL = ''0''                   ', 'AAATVwAAHAABWwfAAH');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('17', '校准计划审批', 'HT_EQ_MCLBR_PLAN', 'FLOW_STATUS', 'PZ_CODE', 'select section as  工段,equipment_id as  设备名称,STATUS as状态,EXP_FINISH_TIME as 期望完成时间,  remark as  备注,ID from ht_eq_mclbr_plan_detail  where main_id = ''@BUZ_ID'' and is_del = ''0''                ', 'AAATVwAAHAABWwfAAI');

