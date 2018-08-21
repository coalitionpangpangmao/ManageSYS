select r.产品,r.班组,r.prodday as 时间,r.总分 as 在线考核分,s.得分 as 过程检测得分,t.得分 as 理化检测得分,p.得分 as 感观评测得分,r.总分 * 0.2 + s.得分 * 0.3 + t.得分 * 0.4 + p.得分 * 0.1 as 总得分 from hv_online_daily_report r 
left join hv_process_daily_report s on s.产品 = r.产品 and s.班组 = r.班组 and s.检测时间 = r.prodday
left join hv_phychem_daily_report t on t.产品 = r.产品 and t.班组 = r.班组 and t.检测时间 = r.prodday
left join hv_sensor_daily_report p on p.产品 = r.产品 and p.班组 = r.班组 and p.检测时间 = r.prodday
