using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
public partial class Quality_MonthlyAnlz : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            txtStartTime.Text = System.DateTime.Now.ToString("yyyy-MM");
            initView();
        }
    }
    protected void initView()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listTeam, "select team_code,team_name from ht_sys_team where is_del = '0' order by team_code", "team_name", "team_code");
        bindStatistic();

    }
    protected void bindStatistic()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query;
       if(listTeam.SelectedValue == "")
           query = "select a5.prod_name as 产品, a1.score as 在线考核分,a2.score as 理化检测得分,a3.score as 感观评测得分,a4.score as 过程检测得分,  (select weight from ht_qlt_weight where id='1')*nvl(a1.score,100) +  (select weight from ht_qlt_weight where id='2')*nvl(a2.score,100)+ (select weight from ht_qlt_weight where id='3')*nvl(a3.score,100) + (select weight from ht_qlt_weight where id='4')*nvl(a4.score,100) as 总得分 from (select distinct prod_code from ht_qlt_inspect_record r where substr(r.record_time,1,7) = '" + txtStartTime.Text + "' union select distinct prod_code from ht_qlt_data_record where substr(b_time,1,7) = '" + txtStartTime.Text + "') a left join ht_pub_prod_design a5 on  a5.prod_code = a.prod_code left join (select sum(r.score * s.weight) as score,r.prod_code  from  (select a.section,sum(a.quarate * c.weight*100) as score,a.prod_code from (select r.prod_code, substr(r.para_code,1,5) as section,r.para_code,sum(r.quarate*r.count)/sum(r.count) as quarate from ht_qlt_data_record r where substr(r.b_time,1,7) = '" + txtStartTime.Text + "' group by r.para_code,r.prod_code) a   left join ht_pub_tech_para b on b.para_code  = a.para_code and b.para_type  like '______1%'  left join ht_qlt_weight c on c.id = a.para_code  group by a.section,a.prod_code )r  left join ht_pub_tech_section  s on s.section_code = r.section group by r.prod_code )a1 on a5.prod_name = a.prod_code left join (select round(avg(得分),2) as score,产品 from hv_phychem_daily_report  r left join ht_pub_prod_design t on r.产品 = t.prod_name  where substr(r.检测时间,1,7) = '" + txtStartTime.Text + "' group by 产品)a2 on a5.prod_name = a2.产品 left join (select round(avg(得分),2) as score,产品名称 from hv_qlt_sensor_realrec  r where r.sensor_month = '" + txtStartTime.Text + "' group by 产品)a3 on a5.prod_name = a3.产品 left join (select round(avg(得分),2) as score,产品 from hv_process_daily_report  r left join ht_pub_prod_design t on r.产品 = t.prod_name  where substr(r.检测时间,1,7) = '" + txtStartTime.Text + "' group by 产品)a4 on a5.prod_name = a4.产品 order by a5.prod_name";
       else
           query = " select a5.prod_name as 产品,a6.team_name as 班组, a1.score as 在线考核分,a2.score as 理化检测得分,a3.score as 感观评测得分,a4.score as 过程检测得分,  (select weight from ht_qlt_weight where id='1')*nvl(a1.score,100) +  (select weight from ht_qlt_weight where id='2')*nvl(a2.score,100)+ (select weight from ht_qlt_weight where id='3')*nvl(a3.score,100) + (select weight from ht_qlt_weight where id='4')*nvl(a4.score,100) as 总得分   from  (select distinct team_id as team, prod_code from ht_qlt_inspect_record  where substr(record_time,1,7) =  '" + txtStartTime.Text + "' union select distinct team, prod_code from ht_qlt_data_record  where substr(b_time,1,7) =  '" + txtStartTime.Text + "') a  left join ht_pub_prod_design a5 on  a5.prod_code = a.prod_code  left join ht_sys_team a6 on a6.team_code = a.team  left join (select sum(r.score * s.weight) as score,r.prod_code,r.team  from  (select a.section,sum(a.quarate * c.weight*100) as score,a.prod_code,a.team from (select r.prod_code,r.team, substr(r.para_code,1,5) as section,r.para_code,sum(r.quarate*r.count)/sum(r.count) as quarate from ht_qlt_data_record r where substr(r.b_time,1,7) =  '" + txtStartTime.Text + "' group by r.para_code,r.prod_code,r.team) a   left join ht_pub_tech_para b on b.para_code  = a.para_code and b.para_type  like '______1%'  left join ht_qlt_weight c on c.id = a.para_code  group by a.section,a.prod_code ,a.team)r  left join ht_pub_tech_section  s on s.section_code = r.section group by r.prod_code,r.team  )a1 on a.prod_code = a1.prod_code and a6.team_code = a1.team  left join (select round(avg(得分),2) as score,产品,班组 from hv_phychem_daily_report  r left join ht_pub_prod_design t on r.产品 = t.prod_name  where substr(r.检测时间,1,7) =  '" + txtStartTime.Text + "' group by 产品,班组)a2 on a5.prod_name = a2.产品 and a6.team_name = a2.班组  left join (select round(avg(得分),2) as score,产品,班组 from hv_sensor_daily_report  r left join ht_pub_prod_design t on r.产品 = t.prod_name  where substr(r.检测时间,1,7) =  '" + txtStartTime.Text + "' group by 产品,班组)a3 on a5.prod_name = a3.产品 and a6.team_name = a3.班组  left join (select round(avg(得分),2) as score,产品,班组 from hv_process_daily_report  r left join ht_pub_prod_design t on r.产品 = t.prod_name  where substr(r.检测时间,1,7) =  '" + txtStartTime.Text + "' group by 产品,班组)a4 on a5.prod_name = a4.产品 and a6.team_name = a4.班组 where a6.team_code = '" + listTeam.SelectedValue + "'";
          
            GridAll.DataSource = opt.CreateDataSetOra(query);
            GridAll.DataBind();
             query= "select * from hv_online_daily_report t where substr(时间,1,7) = '" + txtStartTime.Text + "'";
            if (listTeam.SelectedValue != "")
                query += " and t.班组 = '" + listTeam.SelectedItem.Text + "'";
            query += " order by t.产品,t.班组";
            GridView1.DataSource = opt.CreateDataSetOra(query);
            GridView1.DataBind();
            query = "select * from hv_process_daily_report t where substr(检测时间,1,7) = '" + txtStartTime.Text + "'";
            if (listTeam.SelectedValue != "")
                query += " and t.班组 = '" + listTeam.SelectedItem.Text + "'";
            query += " order by t.产品,t.班组";
            GridView2.DataSource = opt.CreateDataSetOra(query);
            GridView2.DataBind();
            query = "select * from hv_phychem_daily_report t where substr(检测时间,1,7) = '" + txtStartTime.Text + "'";
            if (listTeam.SelectedValue != "")
                query += " and t.班组 = '" + listTeam.SelectedItem.Text + "'";
            query += " order by t.产品,t.班组";
            GridView3.DataSource = opt.CreateDataSetOra(query);
            GridView3.DataBind();
            query = "select * from hv_sensor_daily_report t where substr(检测时间,1,7) = '" + txtStartTime.Text + "'";
            if (listTeam.SelectedValue != "")
                query += " and t.班组 = '" + listTeam.SelectedItem.Text + "'";
            query += " order by t.产品,t.班组";
            GridView4.DataSource = opt.CreateDataSetOra(query);
            GridView4.DataBind();
   
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindStatistic();
    }
}