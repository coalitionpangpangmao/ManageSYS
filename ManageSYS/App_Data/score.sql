select r.��Ʒ,r.����,r.prodday as ʱ��,r.�ܷ� as ���߿��˷�,s.�÷� as ���̼��÷�,t.�÷� as �����÷�,p.�÷� as �й�����÷�,r.�ܷ� * 0.2 + s.�÷� * 0.3 + t.�÷� * 0.4 + p.�÷� * 0.1 as �ܵ÷� from hv_online_daily_report r 
left join hv_process_daily_report s on s.��Ʒ = r.��Ʒ and s.���� = r.���� and s.���ʱ�� = r.prodday
left join hv_phychem_daily_report t on t.��Ʒ = r.��Ʒ and t.���� = r.���� and t.���ʱ�� = r.prodday
left join hv_sensor_daily_report p on p.��Ʒ = r.��Ʒ and p.���� = r.���� and p.���ʱ�� = r.prodday
