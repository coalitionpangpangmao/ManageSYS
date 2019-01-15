using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
public partial class Quality_MonthEvaluat : MSYS.Web.BasePage
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

        bindStatistic();

    }
    protected void bindStatistic()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query;

        query = "select r.产品, t.output as 产量,t.output/s.output as 产量占比,r.* from hv_qlt_month_prod_report r left join (select t.PROD_CODE,substr(t.STARTTIME,1,7) as month, sum(decode(t.para_code,'7030500029',t.SEG_VALUE))  * sum(decode(t.para_code,'7030500030',t.SECTION_CODE)) as output from hv_prod_report t where t.SECTION_CODE = '70305' and t.starttime like '" + txtStartTime.Text + "%' group by substr(t.starttime,1,7),t.prod_code) t on t.PROD_CODE = r.prod_code and t.month = r.month left join (select substr(t.STARTTIME,1,7) as month, sum(decode(t.para_code,'7030500029',t.SEG_VALUE))  * sum(decode(t.para_code,'7030500030',t.SEG_VALUE)) as output from hv_prod_report t where t.SECTION_CODE = '70305' and t.starttime like '" + txtStartTime.Text + "%' group by substr(t.starttime,1,7)) s   on s.month = r.month where r.month = '" + txtStartTime.Text + "'";
        DataSet data = opt.CreateDataSetOra(query);
        GridAll.DataSource = data;
        GridAll.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            labout.Text = opt.GetSegValue("select substr(t.STARTTIME,1,7) as month, sum(decode(t.para_code,'7030500029',t.SEG_VALUE))  * sum(decode(t.para_code,'7030500030',t.SEG_VALUE)) as output from hv_prod_report t where t.SECTION_CODE = '70305' and t.starttime like '" + txtStartTime.Text + "%' group by substr(t.starttime,1,7)", "output");
            labout.Text = (labout.Text == "NoRecord"||labout.Text =="") ? "0" : labout.Text;
            if (labout.Text != "0")
            {
                double total = Convert.ToDouble(labout.Text);
                double score = 0;
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    score += Convert.ToDouble(row["产量"].ToString()) / total * Convert.ToDouble(row["产品得分"].ToString());
                }
                labScore.Text = score.ToString();
            }
        }



    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
    }
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
        e.Row.Cells[1].Visible = false;      
    }
    protected void GridView3_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
        e.Row.Cells[1].Visible = false;    
    }
    protected void GridView4_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
        e.Row.Cells[1].Visible = false;    
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindStatistic();
    }

    protected void btngridDetail_Click(object sender, EventArgs e)
    {
        
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        string prod_code = GridAll.DataKeys[row.RowIndex].Value.ToString();
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select * from hv_qlt_online_month_score t where t.month = '" + txtStartTime.Text + "' and t.prod_code = '" + prod_code + "'");
        GridView1.DataSource = data;
        GridView1.DataBind();

        data = opt.CreateDataSetOra("select * from hv_qlt_phychem_month_report t where t.month = '" +  txtStartTime.Text  + "' and t.prod_code = '"+ prod_code + "'");
        GridView2.DataSource = data;
        GridView2.DataBind();

        data = opt.CreateDataSetOra("select * from hv_qlt_process_month_report t where t.month = '" + txtStartTime.Text + "' and t.prod_code = '" + prod_code + "'");
        GridView3.DataSource = data;
        GridView3.DataBind();

        data = opt.CreateDataSetOra("select * from hv_qlt_sensor_report t where t.产品 = '" + prod_code + "' and t.Record_time like '" + txtStartTime.Text + "%'");
        GridView4.DataSource = data;
        GridView4.DataBind();

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportExcel("再造梗丝工艺质量总结", "", "", "", "", ".xls", txtStartTime.Text, DateTime.Now, true);
    }
}