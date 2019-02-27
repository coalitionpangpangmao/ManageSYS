using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
public partial class Device_MtncFeedback : MSYS.Web.BasePage
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
        string query = "select distinct t.mt_name as 维保计划,t.pz_code as 计划号,t.expired_date as 过期时间,t1.name as 申请人,t.remark as 备注,t.pz_code from ht_eq_mt_plan t left join ht_svr_user t1 on t1.id = t.create_id  left join ht_eq_mt_plan_detail t2 on t2.main_id = t.pz_code where t2.status >= '3' and  t.expired_date between '" + txtStart.Text + "' and '" + txtStop.Text + "'  and t.IS_DEL = '0' ";
        if (ckDone.Checked)
            query += " and t.TASK_STATUS > '3'";
        else
            query += " and t.TASK_STATUS = '3' ";
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
           DataSet data = opt.CreateDataSetOra(query);
           GridView1.DataSource = data;
           GridView1.DataBind();
          
    }//绑定gridview1数据源

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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
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
        bindGrid2(txtCode.Value);
    }
     protected void btnGridview_Click(object sender, EventArgs e)//查看明细
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        txtCode.Value = GridView1.DataKeys[rowIndex].Value.ToString();
        bindGrid2(txtCode.Value);
        
    }
  
/// <summary>
/// /tab2
/// </summary>

     protected void bindGrid2(string code )
     {

         string query = "select t1.section_name as 区域,t2.eq_name as 设备名称,t.reason as 维保原因,t.content as 维保内容,t.exp_finish_time as 期望完成时间,t.STATUS as 状态,t.remark as 备注 ,t.ID  from ht_eq_mt_plan_detail t left join ht_pub_tech_section t1 on t1.section_code = t.mech_area left join ht_eq_eqp_tbl t2 on t2.idkey = t.equipment_id  where t.main_id = '" + code + "' and t.is_del = '0'";
       //  query += " and t.RESPONER = '" + ((MSYS.Data.SysUser)Session["User"]).id + "'";
        MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
         DataSet data = opt.CreateDataSetOra(query);
         GridView2.DataSource = data;
         GridView2.DataBind();
         if (data != null && data.Tables[0].Rows.Count > 0)
         {
             for (int i = GridView2.PageSize * GridView2.PageIndex; i < GridView2.PageSize * (GridView2.PageIndex + 1) && i < data.Tables[0].Rows.Count; i++)
             {
                 DataRowView mydrv = data.Tables[0].DefaultView[i];
                 GridViewRow row = GridView2.Rows[i - GridView2.PageSize * GridView2.PageIndex];            
                 ((DropDownList)row.FindControl("listGrid2Status")).SelectedValue = mydrv["状态"].ToString();
                 ((TextBox)row.FindControl("txtGridremark")).Text = mydrv["备注"].ToString();

             }

         }

     }//绑定GridView2数据源
    protected DataSet eqbind()
    {
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select IDKEY,EQ_NAME from ht_eq_eqp_tbl where is_del = '0' and is_valid = '1'");
    }
    protected DataSet statusbind()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select ID, Name from ht_inner_eqexe_status order by ID ");
    }
    protected void btngrid2Deal_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        txtID.Text = GridView2.DataKeys[row.RowIndex].Value.ToString();
        txtCodeZ.Text = txtCode.Value;
        string status = ((DropDownList)row.FindControl("listGrid2Status")).SelectedValue;
        if (status == "5")
            btnModify.Visible = false;
        else
            btnModify.Visible = true;
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select * from ht_eq_mt_plan_detail where ID = '" + txtID.Text + "'");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            DataRow drow = data.Tables[0].Rows[0];
            txtScean.Text = drow["FEEDBACK"].ToString();

            txtPlus.Text = drow["REMARKPLUS"].ToString();
        }
        else
        {
            txtScean.Text = "";
            txtPlus.Text = "";
        }

        ScriptManager.RegisterStartupScript(updtpanel1, this.Page.GetType(), "", " $('.shade').fadeIn(200);", true);

    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        string[] seg = { "ID", "STATUS", "FEEDBACK", "REMARKPLUS" };
        string[] value = { txtID.Text, "4", txtScean.Text, txtPlus.Text };
        
        string log_message = opt.MergeInto(seg, value, 1, "ht_eq_mt_plan_detail") == "Success" ? "维保反馈成功" : "维保反馈失败";
        log_message += "--详情:" + string.Join(",", value);
        InsertTlog(log_message);
        bindGrid2(txtCode.Value);
        string alter = opt.GetSegValue("select case  when total = done then 1 else 0 end as status from (select  count(distinct t.id) as total,count( distinct t1.id) as done from ht_eq_mt_plan_detail t left join ht_eq_mt_plan_detail t1 on t1.id = t.id and t1.status >= '4' and t1.is_del = '0' where t.main_id = '" + txtCode.Value + "'  and t.is_del = '0')", "status");
        if (alter == "1")
        {
            opt.UpDateOra("update ht_eq_mt_plan set TASK_STATUS = '4' where PZ_CODE = '" + txtCode.Value + "'  and TASK_STATUS = '3'");
            bindGrid1();
        }
        ScriptManager.RegisterStartupScript(updtpanel1, this.Page.GetType(), "", " $('.shade').fadeOut(200);", true);
    }

}