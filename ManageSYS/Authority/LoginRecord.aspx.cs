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
        GridView theGrid = sender as GridView;
        int newPageIndex = 0;
        if (e.NewPageIndex == -3)
        {
            //点击跳转按钮
            TextBox txtNewPageIndex = null;

            //GridView较DataGrid提供了更多的API，获取分页块可以使用BottomPagerRow 或者TopPagerRow，当然还增加了HeaderRow和FooterRow
            GridViewRow pagerRow = theGrid.BottomPagerRow;
           
            if (pagerRow != null)
            {
                //得到text控件
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;
            }
            if (txtNewPageIndex != null)
            {
                //得到索引
                newPageIndex = int.Parse(txtNewPageIndex.Text) - 1;
            }
        }
        else
        {
            //点击了其他的按钮
            newPageIndex = e.NewPageIndex;
        }
        //防止新索引溢出
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;
        //得到新的值
        theGrid.PageIndex = newPageIndex;
        //重新绑定
     
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