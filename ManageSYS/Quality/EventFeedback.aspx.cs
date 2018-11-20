using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Quality_EventFeedback : MSYS.Web.BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            initView();
        }
    }
    protected void initView()
    {
        txtBtime.Text = System.DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
        txtEtime.Text = System.DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listStyle, "select ID,Name from ht_inner_qlt_type union select inspect_code as ID,inspect_name as name from ht_qlt_inspect_proj where is_del = '0'", "Name", "ID");
        bindgrid1();
        bindgrid2();
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

        bindgrid1();
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

        bindgrid2();
    }
    protected void bindgrid1()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select distinct t.para_name,s.prod_name,k.name,r.value,r.range,r.b_time,r.e_time,h.team_name,nvl(j.status,0) as status,r.id ,r.type from hv_qlt_data_event r left join ht_pub_prod_design s on s.prod_code = r.prod_code left join ht_pub_tech_para t on t.para_code = r.para_code left join ht_sys_team h on h.team_code = r.team left join ht_qlt_auto_event j on j.record_id = r.id and j.sort = r.type left join ht_inner_qlt_type k on k.id = r.type where r.b_time>'" + txtBtime.Text + "' and r.e_time <'" + txtEtime.Text + "' and j.status = '4'";

        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string status = "";
            for (int i = GridView1.PageSize * GridView1.PageIndex; i < GridView1.PageSize * (GridView1.PageIndex + 1) && i < data.Tables[0].Rows.Count; i++)
            {
                int j = i - GridView1.PageSize * GridView1.PageIndex;
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                GridViewRow row = GridView1.Rows[j];
                status = mydrv["status"].ToString();
              
                ((Label)row.FindControl("labStatus")).Text = opt.GetSegValue("select * from ht_inner_inspect_status where id = '" + status + "'", "NAME");
            }
        }
      
    }

    protected void bindgrid2()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select t.id,r.prod_name,s.team_name,t.inspect_type,t.inspect_code,t.insgroup,t.inspect_name,t.value,t.range,t.unit,t.status,t.minus_score,t.record_time from hv_craft_offline t left join ht_pub_prod_design r on r.prod_code = t.prod_code left join ht_sys_team s on s.team_code = t.team_id where t.RECORD_TIME between '" + txtBtime.Text + "' and '" + txtEtime.Text + "' and t.status >= '4'  order by  RECORD_TIME,prod_name,inspect_code ";
        DataSet data = opt.CreateDataSetOra(query);
        GridView2.DataSource = data;
        GridView2.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string status = "";
            for (int i = GridView2.PageSize * GridView2.PageIndex; i < GridView2.PageSize * (GridView2.PageIndex + 1) && i < data.Tables[0].Rows.Count; i++)
            {
                int j = i - GridView2.PageSize * GridView2.PageIndex;
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                GridViewRow row = GridView2.Rows[j];
                status = mydrv["status"].ToString();
                ((Label)row.FindControl("labStatus")).Text = opt.GetSegValue("select * from ht_inner_inspect_status where id = '" + status + "'", "NAME");
            }
        }
        
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindgrid1();
        bindgrid2();
    }

    protected void btngrid2Deal_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdType.Value = "2";
        txtEventID.Text = GridView2.DataKeys[row.RowIndex].Values[0].ToString();
       listStyle.SelectedValue= GridView2.DataKeys[row.RowIndex].Values[1].ToString();
        if (((Button)row.FindControl("btngrid2Deal")).Text == "查看")
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataSet data = opt.CreateDataSetOra("select * from HT_QLT_INSPECT_EVENT where RECORD_ID = '" + txtEventID.Text + "' and INSPECT_CODE = '" + listStyle.SelectedValue + "'");
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                DataRow drow = data.Tables[0].Rows[0];
                txtScean.Text = drow["FEEDBACK"].ToString();

                txtPlus.Text = drow["REMARKPLUS"].ToString();
            }
        }
        else
        {
            txtScean.Text = "";
           
            txtPlus.Text = "";
        }
        ScriptManager.RegisterStartupScript(updtpanel1, this.Page.GetType(), "", " $('.shade').fadeIn(200);", true);

    }

    protected void btngrid1Deal_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdType.Value = "1";
        txtEventID.Text = GridView1.DataKeys[row.RowIndex].Values[0].ToString();
        listStyle.SelectedValue = GridView1.DataKeys[row.RowIndex].Values[1].ToString();
        if (((Button)row.FindControl("btngrid1Deal")).Text == "查看")
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataSet data = opt.CreateDataSetOra("select * from HT_QLT_AUTO_EVENT where RECORD_ID = '" + txtEventID.Text + "' and SORT = '" + listStyle.SelectedValue + "'");
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                DataRow drow = data.Tables[0].Rows[0];
                txtScean.Text = drow["FEEDBACK"].ToString();

                txtPlus.Text = drow["REMARKPLUS"].ToString();
            }
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
        if (hdType.Value == "1")
        {
            string[] seg = { "RECORD_ID", "SORT", "STATUS", "FEEDBACK", "REMARKPLUS" };
            string[] value = { txtEventID.Text, listStyle.SelectedValue, "5", txtScean.Text,  txtPlus.Text };
            
            string log_message = opt.MergeInto(seg, value, 2, "HT_QLT_AUTO_EVENT") == "Success" ? "反馈工艺质量事件成功" : "反馈工艺质量事件失败";
            log_message += "--详情:" + string.Join(",", value);
            InsertTlog(log_message);
            bindgrid1();
        }
        else
        {
            string[] seg = { "RECORD_ID", "INSPECT_CODE", "STATUS", "FEEDBACK", "REMARKPLUS" };
            string[] value = { txtEventID.Text, listStyle.SelectedValue, "5",txtScean.Text,  txtPlus.Text };
            string log_message = opt.MergeInto(seg, value, 1, "HT_QLT_INSPECT_EVENT") == "Success" ? "反馈工艺质量事件成功" : "反馈工艺质量事件失败";
            log_message += "--详情:" + string.Join(",", value);
            InsertTlog(log_message);
            bindgrid2();
        }
        ScriptManager.RegisterStartupScript(updtpanel1, this.Page.GetType(), "", " $('.shade').fadeOut(200);", true);
    }
 

}