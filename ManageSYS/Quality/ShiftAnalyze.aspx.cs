using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
public partial class Quality_ShiftAnalyze : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            txtStartTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
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
        DataSet data = opt.CreateDataSetOra("select * from ht_qlt_weight t where is_del = '0' order by ID ");
        if (data != null && data.Tables[0].Rows.Count == 4)
        {
            DataRowCollection Rows = data.Tables[0].Rows;
            StringBuilder str = new StringBuilder();
            str.Append("select r.产品,r.班组,r.时间,r.总分 as 在线考核分,s.得分 as 过程检测得分,t.得分 as 理化检测得分,p.得分 as 感观评测得分,r.总分 *");
            str.Append(Rows[0]["Weight"].ToString());
            str.Append("+ s.得分 * ");
            str.Append(Rows[1]["Weight"].ToString());
            str.Append("+ t.得分 * ");
            str.Append(Rows[2]["Weight"].ToString());
            str.Append("+ p.得分 * ");
            str.Append(Rows[2]["Weight"].ToString());
            str.Append(" as 总得分 from hv_online_daily_report r left join hv_process_daily_report s on s.产品 = r.产品 and s.班组 = r.班组 and s.检测时间 = r.时间 left join hv_phychem_daily_report t on t.产品 = r.产品 and t.班组 = r.班组 and t.检测时间 = r.时间 left join hv_sensor_daily_report p on p.产品 = r.产品 and p.班组 = r.班组 and p.检测时间 = r.时间 where r.时间 = '");
            str.Append(txtStartTime.Text);
            str.Append("'");
            if (listTeam.SelectedValue != "")
            {
                str.Append(" r.班组 ='");
                str.Append(listTeam.SelectedItem.Text);
                str.Append("'");
            }

            data = opt.CreateDataSetOra(str.ToString());
            GridAll.DataSource = data;
            GridAll.DataBind();
            GridView1.DataSource = opt.CreateDataSetOra("select * from hv_online_daily_report t where 时间 = '" + txtStartTime.Text + "'");
            GridView1.DataBind();
            GridView2.DataSource = opt.CreateDataSetOra("select * from hv_process_daily_report t where 检测时间 = '" + txtStartTime.Text + "'");
            GridView2.DataBind();
            GridView3.DataSource = opt.CreateDataSetOra("select * from hv_phychem_daily_report t where 检测时间 = '" + txtStartTime.Text + "'");
            GridView3.DataBind();
            GridView4.DataSource = opt.CreateDataSetOra("select * from hv_sensor_daily_report t where 检测时间 = '" + txtStartTime.Text + "'");
            GridView4.DataBind();
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindStatistic();
    }
}