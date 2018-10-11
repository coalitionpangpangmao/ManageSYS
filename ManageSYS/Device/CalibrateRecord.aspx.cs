using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
public partial class Device_CalibrateRecord : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            txtStart.Text = System.DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd");
            txtStop.Text = System.DateTime.Now.AddDays(45).ToString("yyyy-MM-dd");
           
          
            bindGrid1();

        }

    }
    protected void bindGrid1()
    {

        string query = "select t.mt_name as 校准计划,t.pz_code as 计划号,t.expired_date as 过期时间,t1.name as 申请人,t2.name as 状态,t.remark as 备注,t.pz_code,t.task_status  from HT_EQ_MCLBR_PLAN t left join ht_svr_user t1 on t1.id = t.create_id left join ht_inner_eqexe_status t2 on t2.id = t.task_status   where t.expired_date between '" + txtStart.Text + "' and '" + txtStop.Text + "'  and t.IS_DEL = '0'   and t.FLOW_STATUS = '2' and t.task_status = '5'";
        if(rdAuto.Checked)
        query += "and t.CLBRT_TYPE = '1'";
        if(rdManual.Checked)
            query += "and t.CLBRT_TYPE = '0'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();        


    }//绑定gridview1数据源
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid1();
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

        bindGrid1();
    }


    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
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

        bindGrid2(hidecode.Value);
    }
    protected void btnGridview_Click(object sender, EventArgs e)//查看明细
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        hidecode.Value = GridView1.DataKeys[rowIndex].Value.ToString();
        bindGrid2(hidecode.Value);
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", "$('.shade').fadeIn(200);", true);
       
    }

    /// <summary>
    /// /tab2
    /// </summary>
    protected DataSet statusbind()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select ID, Name from ht_inner_eqexe_status order by ID ");
    }
    protected DataSet sectionbind()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select section_code,section_name from ht_pub_tech_section where is_del = '0' and is_valid = '1' union select '' as section_code,'' as section_name from dual order by section_code");
    }
    protected void bindGrid2(string code)
    {

        string query = "select t.section as 工段,t.equipment_id as 设备名称,t.point as 数据点,t.OLDVALUE as 原值,t.POINTVALUE as 校准值,t.SAMPLE_TIME as 校准时间,t.STATUS as 状态,t.remark as 备注 ,t.ID  from HT_EQ_MCLBR_PLAN_detail  t where t.main_id = '" + code + "' and t.is_del = '0'";

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView2.DataSource = data;
        GridView2.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            for (int i = GridView2.PageSize * GridView2.PageIndex; i < GridView2.PageSize * (GridView2.PageIndex + 1) && i < data.Tables[0].Rows.Count; i++)
            {
                int j = i - GridView2.PageSize * GridView2.PageIndex;
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                GridViewRow row = GridView2.Rows[j];
                DropDownList list1 = (DropDownList)row.FindControl("listGridsct");
                DropDownList list2 = (DropDownList)row.FindControl("listGridEq");
                DropDownList list3 = (DropDownList)row.FindControl("listGridPoint");
                list1.SelectedValue = mydrv["工段"].ToString();
                opt.bindDropDownList(list2, "select IDKEY,EQ_NAME  from ht_eq_eqp_tbl where section_code = '" + list1.SelectedValue + "'  order by idkey", "EQ_NAME", "IDKEY");
                list2.SelectedValue = mydrv["设备名称"].ToString();
                opt.bindDropDownList(list3, "select para_code,para_name from ht_pub_tech_para where equip_code = '" + list2.SelectedValue + "' order by para_code ", "para_name", "para_code");
                list3.SelectedValue = mydrv["数据点"].ToString();
                ((TextBox)row.FindControl("txtGridOldvalue")).Text = mydrv["原值"].ToString();
                ((TextBox)row.FindControl("txtGridNewvalue")).Text = mydrv["校准值"].ToString();
                ((TextBox)row.FindControl("txtGridClbrtime")).Text = mydrv["校准时间"].ToString();
                ((DropDownList)row.FindControl("listGrid2Status")).SelectedValue = mydrv["状态"].ToString();
                ((TextBox)row.FindControl("txtGridremark")).Text = mydrv["备注"].ToString();

            }
        }

    }//绑定GridView2数据源

}