using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

public partial class Device_AlarmSearch : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.bindDropDownList(tag, "select DISTINCT ALM_TAGNAME from fixalarms", "ALM_TAGNAME", "ALM_TAGNAME");
            opt.bindDropDownList(area, "select DISTINCT ALM_ALMAREA from fixalarms", "ALM_ALMAREA", "ALM_ALMAREA");

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e) {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string tagName = tag.Text.Trim()=="" ?"is null ":("='"+tag.Text.Trim()+"'");
        string areaName = area.Text.Trim() == "" ? "is null ": ("='"+area.Text.Trim()+"'");
        string sstime = stime.Text;
        /*if (sstime != "") {
            sstime = sstime.Remove(5, 1); 
        }*/
        string time = sstime+ "%";
        time = time.Replace("-", "/");
        System.Diagnostics.Debug.WriteLine(time);
        string query = "select * from fixalarms where ALM_TAGDESC is not null and  ALM_TAGNAME " + tagName + " and ALM_ALMAREA " + areaName + "";
        query += " and to_char(ALM_NATIVETIMEIN, 'yyyy/MM/dd') like '" + time + "'";
        System.Diagnostics.Debug.WriteLine(query);
        DataSet ds = opt.CreateDataSetOra(query);

        GridView1.DataSource = ds;
        GridView1.DataBind();
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

        //bindGrid();
    }
}