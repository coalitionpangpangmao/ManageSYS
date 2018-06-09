select r.id as userid,s.f_menu,s.f_type,s.f_url,s.f_id as rightID  from HT_SVR_USER r 
left join HT_SVR_SYS_ROLE t on r.role = t.f_id
left join HT_SVR_SYS_MENU s on substr(t.f_right,s.f_id,1) = '1'
