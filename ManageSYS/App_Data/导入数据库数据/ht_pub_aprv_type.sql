insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('10', '�����䷽����', 'HT_QA_AUX_FORMULA', 'FLOW_STATUS', 'FORMULA_CODE', 'select r.material_name as ��������,s.mater_code as ���ϱ���,s.aux_scale as Ͷ�ϱ��� from    ht_qa_Aux_formula_detail s left join ht_pub_materiel r  on r.material_code = s.mater_code where s.formula_code = ''@BUZ_ID'' and s.is_del = ''0''            ', 'AAATVwAAHAABWwbAAA');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('11', 'Ϳ��Һ���䷽����', 'HT_QA_COAT_FORMULA', 'FLOW_STATUS', 'FORMULA_CODE', 'select r.material_name as ��������,s.mater_code as ���ϱ���,s.class_name as ��������,s.coat_scale as Ͷ�ϱ���,s.need_size as ����������,s.coat_flag as �䷽���� from    ht_qa_coat_formula_detail s left join ht_pub_materiel r  on r.material_code = s.mater_code where s.formula_code = ''@BUZ_ID'' and s.is_del = ''0''                                                 ', 'AAATVwAAHAABWwbAAB');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('09', '���ϳ��������', 'ht_strg_Aux', 'AUDIT_MARK', 'ORDER_SN', 'select STORAGE as  �ֿ�,unit_code as  ������λ,mater_code as   ���ϱ���,mater_name as  ��������,NUM as   ������,ID,  main_code from ht_strg_aux_sub where main_code =''@BUZ_ID'' and is_del = ''0''                    ', 'AAATVwAAHAABWwcAAA');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('12', '���������ƻ�����', 'HT_PROD_SEASON_PLAN', 'FLOW_STATUS', 'ID', 'select distinct r.plan_year as ���, r.quarter as ����, s.prod_name as ��Ʒ,t.plan_output_1 as �ƻ�����1, t.plan_output_2 as �ƻ�����2,t.plan_output_3 as �ƻ�����3 from ht_prod_SEASON_PLAN_detail t  left join ht_pub_prod_design s on s.prod_code = t.prod_code left join ht_prod_season_plan r on r.id = t.quarter_plan_id where t.QUARTER_PLAN_ID = ''@BUZ_ID'' and t.is_del = ''0''                                                     ', 'AAATVwAAHAABWwcAAB');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('15', 'ά�޼ƻ�����', 'HT_EQ_RP_PLAN', 'FLOW_STATUS', 'PZ_CODE', 'select t.mech_area as ����,t.equipment_id as �豸����,t.reason as ά��ԭ��,t.content as ά������,t.exp_finish_time as �������ʱ��,t.STATUS as ״̬,t.remark as ��ע ,t.ID  from HT_EQ_RP_PLAN_detail t   where t.main_id = ''@BUZ_ID'' and t.is_del = ''0''                                           ', 'AAATVwAAHAABWwcAAC');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('16', '�󻬼ƻ�����', 'HT_EQ_LB_PLAN', 'FLOW_STATUS', 'PZ_CODE', 'select section as  ����,equipment_id as  �豸����,STATUS as״̬,EXP_FINISH_TIME as �������ʱ��,  remark as  ��ע,ID from ht_eq_lb_plan_detail  where main_id = ''@BUZ_ID'' and is_del = ''0''                ', 'AAATVwAAHAABWwcAAD');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('08', 'ԭ�ϳ��������', 'ht_strg_materia', 'AUDIT_MARK', 'ORDER_SN', 'select STORAGE as  �ֿ�,mater_flag as   ���� ,unit_code as  ������λ,mater_code as   ԭ�ϱ���,mater_name as  ԭ������,original_demand as   ������,ID,  main_code from ht_strg_mater_sub where main_code =''@BUZ_ID'' and is_del = ''0''                   ', 'AAATVwAAHAABWweAAA');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('14', 'ά���ƻ�����', 'HT_EQ_MT_PLAN', 'FLOW_STATUS', 'PZ_CODE', 'select t.mech_area as ����,t.equipment_id as �豸����,t.reason as ά�ޱ���ԭ��,t.content as ά�ޱ�������,t.exp_finish_time as �������ʱ��,t.STATUS as ״̬,t.remark as ��ע ,t.ID  from ht_eq_mt_plan_detail t   where t.main_id = ''@BUZ_ID'' and t.is_del = ''0''                            ', 'AAATVwAAHAABWweAAB');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('02', '��Ʒ����', 'HT_PUB_PROD_DESIGN', 'B_FLOW_STATUS', 'PROD_CODE', 'select g1.prod_name as ��Ʒ��,g2.formula_name as ԭ���䷽,g3.formula_name as �����䷽,g4.formula_name  as Ϳ��Һ�䷽,g5.tech_name  as ���ձ�׼,g6.qlt_name as �������˱�׼ from Ht_pub_prod_design g1 left join ht_qa_mater_formula g2 on g1.mater_formula_code = g2.formula_code left join ht_qa_aux_formula g3 on g1.aux_formula_code = g3.formula_code left join ht_qa_coat_formula g4 on g1.coat_formula_code = g4.formula_code left join ht_tech_stdd_code g5 on g1.tech_stdd_code = g5.tech_code left join ht_qlt_stdd_code g6 on g1.qlt_code = g6.qlt_code where g1.prod_code = ''@BUZ_ID''                                           ', 'AAATVwAAHAABWwfAAB');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('03', 'ԭ���䷽����', 'HT_QA_MATER_FORMULA', 'FLOW_STATUS', 'FORMULA_CODE', 'select r.material_name as ��������,s.mater_code as ���ϱ���,s.batch_size as ��Ͷ����,s.mater_sort as ���Ϸ��� from    ht_qa_mater_formula_detail s left join ht_pub_materiel r  on r.material_code = s.mater_code where s.formula_code = ''@BUZ_ID''  and r.is_del = ''0''                ', 'AAATVwAAHAABWwfAAC');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('04', '���ձ�׼����', 'HT_TECH_STDD_CODE', 'FLOW_STATUS', 'TECH_CODE', 'select tech_code as ���ձ�׼,t.section_name as ���ն�,  para_name as ������,VALUE as ��׼ֵ,UPPER_LIMIT as ����,LOWER_LIMIT as ����,EER_DEV as �ʲ�,UNIT as ��λ  from ht_tech_stdd_code_detail r left join ht_pub_tech_para s on s.para_code = r.para_code left join ht_pub_tech_section t on t.section_code = substr(r.para_code,1,5) where r.IS_DEL = ''0'' and    r.tech_code = ''@BUZ_ID'' and s.is_del = ''0'' order by t.section_name                                     ', 'AAATVwAAHAABWwfAAD');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('05', '����·������', 'HT_PUB_PATH_SECTION', 'FLOW_STATUS', 'ID', 'select s.orders as ���,s.nodename as �ڵ���,substr(r.pathcode,s.orders,1) as �Ƿ�ѡ�� from HT_PUB_PATH_SECTION r left join ht_pub_path_node s on s.section_code = r.section_code and s.is_del = ''0''  where r.ID = ''@BUZ_ID'' order by s.orders          ', 'AAATVwAAHAABWwfAAE');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('06', '�����ƻ�����', 'HT_PROD_MONTH_PLAN', 'B_FLOW_STATUS', 'ID', 'select t.plan_Sort as ˳���, t.plan_no as �ƻ���, s.prod_name as ��Ʒ���,t.plan_output as �ƻ����� from ht_prod_month_plan_detail t  left join ht_pub_prod_design s on s.prod_code = t.prod_code where t.month_plan_id =  ''@BUZ_ID''  and t.is_del = ''0''              ', 'AAATVwAAHAABWwfAAF');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('07', '�������˱�׼����', 'HT_QLT_STDD_CODE', 'FLOW_STATUS', 'QLT_CODE', 'select r.qlt_code as ���ձ�׼,t.section_name as ���ն�,  s.para_name as ������,q.name as ����,r.value as ��׼ֵ,r.upper as ����,r.lower as ����,r.minus_score as �۷�,s.para_unit as ��λ  from ht_qlt_stdd_code_detail r left join ht_pub_tech_para s on s.para_code = r.para_code left join ht_pub_tech_section t on t.section_code = substr(r.para_code,1,5) left join ht_inner_qlt_type q on q.id = r.qlt_type where r.IS_DEL = ''0'' and    r.qlt_code = ''@BUZ_ID'' and s.is_del = ''0'' order by t.section_name                       ', 'AAATVwAAHAABWwfAAG');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('13', '������������', 'HT_EQ_STG_PICKUP', 'FLOW_STATUS', 'PZ_CODE', 'select t.storage as �ֿ�,t.Sp_Code as ����,t.sp_name as ����,t.sp_standard as ���,t.sp_model as �ͺ�,t.sp_unit as ��λ,t.pickup_num as ����,t.own_section as ���ն�,t.remark as ��ע,t.ID from ht_eq_stg_pickup_detail t  where MAIN_CODE = ''@BUZ_ID'' and IS_DEL = ''0''                   ', 'AAATVwAAHAABWwfAAH');

insert into ht_pub_aprv_type (PZ_TYPE, PZ_TYPE_NAME, APRV_TABLE, APRV_TABSEG, BUZ_ID, PLSQL, ROWID)
values ('17', 'У׼�ƻ�����', 'HT_EQ_MCLBR_PLAN', 'FLOW_STATUS', 'PZ_CODE', 'select section as  ����,equipment_id as  �豸����,STATUS as״̬,EXP_FINISH_TIME as �������ʱ��,  remark as  ��ע,ID from ht_eq_mclbr_plan_detail  where main_id = ''@BUZ_ID'' and is_del = ''0''                ', 'AAATVwAAHAABWwfAAI');

