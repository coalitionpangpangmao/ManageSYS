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
        opt.bindDropDownList(listTeam, "select team_code,team_name from ht_sys_team where is_del = '0'", "team_name", "team_code");
        bindStatistic();

    }
    protected void bindStatistic()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        //DataSet data = opt.CreateDataSetOra("select * from ht_qlt_weight t where is_del = '0' order by ID ");
        //if (data != null && data.Tables[0].Rows.Count == 4)
        //{
        //    DataRowCollection Rows = data.Tables[0].Rows;
        //    StringBuilder str = new StringBuilder();
        //    str.Append("select s.prod_name as 产品,r.team_name as 班组,substr(t.时间,0,7) as 时间,avg(nvl(t.在线考核分,100)) as 在线考核分,avg(nvl(t.过程检测得分,100)) as 过程检测得分,avg(nvl(t.理化检测得分,100)) as 理化检测得分,avg(nvl(t.感观评测得分,100)) as 感观评测得分 , avg(nvl(t.在线考核分,100))*");
        //    str.Append(Rows[0]["Weight"].ToString());
        //    str.Append("+ avg(nvl(t.理化检测得分,100)) * ");
        //    str.Append(Rows[1]["Weight"].ToString());
        //    str.Append("+ avg(nvl(t.感观评测得分,100)) * ");
        //    str.Append(Rows[2]["Weight"].ToString());
        //    str.Append("+ avg(nvl(t.过程检测得分,100)) * ");
        //    str.Append(Rows[3]["Weight"].ToString());
        //    str.Append(" as 总得分 from hv_qlt_daily_report   t left join ht_sys_team r on r.team_code = t.班组 left join ht_pub_prod_design s on s.prod_code = t.产品  where substr(t.时间,1,7) = '");
        //    str.Append(txtStartTime.Text);
        //    str.Append("'");
        //    if (listTeam.SelectedValue != "")
        //    {
        //        str.Append(" t.班组 ='");
        //        str.Append(listTeam.SelectedValue);
        //        str.Append("'");
        //    }
        ////    str.Append(" group by s.prod_name,r.team_name,substr(t.时间,0,7) order by s.prod_name,r.team_name");
        string query = "select t.prod_name as 产品,s.team_name as 班组,r.score1 as 在线考核分,r.score2 as 过程检测得分,r.score3 as 理化检测得分,r.score4 as 感观评测得分,r.score as 总得分   from (select t.产品,t.班组,avg(nvl(t.在线考核分,100)) as score1,avg(nvl(t.过程检测得分,100)) as score2,avg(nvl(t.理化检测得分,100)) as score3,avg(nvl(t.感观评测得分,100)) as score4,(select weight from ht_qlt_weight where id = '1')* avg(nvl(t.在线考核分,100))+(select weight from ht_qlt_weight where id = '2')* avg(nvl(t.理化检测得分,100))+(select weight from ht_qlt_weight where id = '3')* avg(nvl(t.感观评测得分,100))+(select weight from ht_qlt_weight where id = '4')* avg(nvl(t.过程检测得分,100)) as score  from hv_qlt_daily_report t  where substr(时间,0,7) = '" + txtStartTime.Text + "' group by 产品,班组,substr(时间,0,7)) r left join ht_pub_prod_design t on t.prod_code = r.产品 left join ht_sys_team s on s.team_code = r.班组 ";
           if (listTeam.SelectedValue != "")
                query += " and team_name = '" + listTeam.SelectedItem.Text + "'";
            query += " order by prod_name,team_name";
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