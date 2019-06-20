using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
public partial class Quality_ProdMonthEvaluat : MSYS.Web.BasePage
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
        opt.bindDropDownList(listProd, "select prod_code,prod_name from ht_pub_prod_design where is_del = '0' order by prod_code", "prod_name", "prod_code");
        bindStatistic();

    }
    protected void bindStatistic()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query;
        if (listProd.SelectedValue == "")
        {
            query = "select * from hv_qlt_month_prod_report t where t.month = '" + txtStartTime.Text + "' order by prod_code";
            GridAll.DataSource = opt.CreateDataSetOra(query);
            GridAll.DataBind();

            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView3.DataSource = null;
            GridView3.DataBind();
            GridView4.DataSource = null;
            GridView4.DataBind();
        }
        else
        {
            query = " select * from hv_qlt_month_prod_report t where t.month = '" + txtStartTime.Text + "' and t.prod_code = '" + listProd.SelectedValue + "'";
            GridAll.DataSource = opt.CreateDataSetOra(query);
            GridAll.DataBind();

            query = "select t.section_name as 工艺段,t.para_name as 工艺点,t.quarate as 合格率,t.stddev as 标准差,t.absdev as 绝对差,t.avg as 均值,t.cpk from hv_qlt_online_month_report t where t.month = '" + txtStartTime.Text + "' and t.prod_code = '" + listProd.SelectedValue + "'";
            GridView1.DataSource = opt.CreateDataSetOra(query);
            GridView1.DataBind();

            query = "select r.team_name as 班组,t.Record_time as 检测时间,  t.* from hv_qlt_phychem_daily_report t left join ht_sys_team r on r.team_code = t.team_id  where t.Record_time like '" + txtStartTime.Text + "%' and t.prod_code = '" + listProd.SelectedValue + "'";
            GridView2.DataSource = opt.CreateDataSetOra(query);
            GridView2.DataBind();

            //query = "select t.record_time as 检测时间，t.* from hv_qlt_sensor_realrec t where t.产品 = '" + listProd.SelectedValue + "' and t.sensor_month = '" + txtStartTime.Text + "'";
            query = "select t.record_time as 评测时间, t.产品名称,  t.* from hv_qlt_sensor_realrec2 t where t.产品 = '" + listProd.SelectedValue + "' and t.sensor_month = '" + txtStartTime.Text + "'";
            GridView3.DataSource = opt.CreateDataSetOra(query);
            GridView3.DataBind();

            query = "select r.team_name as 班组,t.Record_time as 检测时间,  t.* from hv_qlt_process_daily_report t left join ht_sys_team r on r.team_code = t.team_id  where t.Record_time like '" + txtStartTime.Text + "%' and t.prod_code = '" + listProd.SelectedValue + "'";
            GridView4.DataSource = opt.CreateDataSetOra(query);
            GridView4.DataBind();
        }
   
    }
   
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {       
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[3].Visible = false;
        e.Row.Cells[4].Visible = false;
    }
    protected void GridView3_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[3].Visible = false;
        e.Row.Cells[4].Visible = false;
        e.Row.Cells[5].Visible = false;
    }
    protected void GridView4_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[3].Visible = false;
        e.Row.Cells[4].Visible = false;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindStatistic();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportExcel("再造梗丝产品质量总评", listProd.SelectedValue, "","", "", ".xls", txtStartTime.Text, DateTime.Now, true);
    }
}