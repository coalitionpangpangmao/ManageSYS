create or replace view hv_prod_inout_ratio as
select a.datetime ,a.prod_code,nvl(a.value,0) as inweight,nvl(b.value,0) as outweight,case b.value when 0 then 0 else nvl(round(a.value/b.value,4),0) end as rate from
(select t1.datetime,t1.prod_code,nvl(t2.min-t1.min,t1.max-t1.min) as value from
(select a.*,rownum as id from (select  substr (time,1,10) as datetime ,prod_code,min(value) as min,max(value) as max from ht_prod_report_detail where para_code = '7030100003' group by substr(time,1,10),prod_code order by datetime) a) t1
left join (select a.*,rownum as id from  (select substr (time,1,10) as datetime ,prod_code,min(value) as min,max(value) as max from ht_prod_report_detail where para_code = '7030100003' group by substr(time,1,10),prod_code order by datetime) a) t2 on t1.id = t2.id-1 order by t1.id
) a
left join (select t1.datetime,t1.prod_code,nvl(t2.min-t1.min,t1.max-t1.min) as value from
(select a.*,rownum as id from (select  substr (time,1,10) as datetime ,prod_code,min(value) as min,max(value) as max from ht_prod_report_detail where para_code = '7030500019' group by substr(time,1,10),prod_code order by datetime) a) t1
left join (select a.*,rownum as id from  (select substr (time,1,10) as datetime ,prod_code,min(value) as min,max(value) as max from ht_prod_report_detail where para_code = '7030500019' group by substr(time,1,10),prod_code order by datetime) a) t2 on t1.id = t2.id-1 order by t1.id
) b on a.datetime = b.datetime and a.prod_code = b.prod_code where a.value is not null and b.value is not null 
