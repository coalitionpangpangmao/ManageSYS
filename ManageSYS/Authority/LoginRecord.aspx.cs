using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Authority_LoginRecord : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            StartTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            EndTime.Text = System.DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

            bindGrid();           
        }
    }
    protected void bindGrid()
    {
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        string query = "select F_USER as 用户,F_COMPUTER as 操作站,F_TIME as 时间,F_DESCRIPT as 描述  from HT_SVR_LOGIN_RECORD  where F_TIME between '" + StartTime.Text + "' and  '" + EndTime.Text + "'";
     
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid();       
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bindGrid();       
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        string query = "delete from HT_SVR_LOGIN_RECORD where  F_TIME between '" + StartTime.Text + "' and  '" + EndTime.Text + "'";
        if(opt.UpDateOra(query)=="Success")
            opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "删除日志成功，开始时间：" + StartTime.Text + " 结束时间: " + EndTime.Text);
        else
            opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "删除日志失败，开始时间：" + StartTime.Text + " 结束时间: " + EndTime.Text);
        bindGrid();       
    }
}