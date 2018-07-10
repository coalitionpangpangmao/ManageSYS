using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Quality_EventDeal : MSYS.Web.BasePage
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
        bindgrid1();
        bindgrid2();
    }

    protected void bindgrid1()
    {
        DataBaseOperator opt = new DataBaseOperator();
        string query = "select t.para_name,s.prod_name,r.type,r.value,r.range,r.b_time,r.e_time,h.team_name,nvl(j.status,0) as status,r.id from hv_qlt_data_event r left join ht_pub_prod_design s on s.prod_code = r.prod_code left join ht_pub_tech_para t on t.para_code = r.para_code left join ht_sys_team h on h.team_code = r.team left join ht_qlt_auto_event j on j.record_id = r.id and j.sort = r.type where r.b_time>'" + txtBtime.Text + "' and r.e_time <'" + txtEtime.Text + "' ";

        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if ("处理中" == (((Label)row.FindControl("labStatus")).Text = opt.GetSegValue("select * from ht_inner_inspect_status where id = '" + data.Tables[0].Rows[row.RowIndex]["status"].ToString() + "'", "NAME")))
                    ((Button)row.FindControl("btngrid1Deal")).Text = "处理";
                else if ("己忽略" == ((Label)row.FindControl("labStatus")).Text)
                    ((Button)row.FindControl("btngrid1Deal")).Visible = false;
                else
                    ((Button)row.FindControl("btngrid1Deal")).Text = "查看";
            }
        }
    }

    protected void bindgrid2()
    {
        DataBaseOperator opt = new DataBaseOperator();
        string query = "select t.ID,'成品检测'  as inspect_type ,r.inspect_code, h.name as insgroup,r.inspect_name,nvl(t.inspect_value,0) as value,s.lower_value||'~'||s.upper_value as range,r.unit,nvl(j.status,0) as status  ,s.minus_score  from ht_qlt_inspect_record t left join ht_qlt_inspect_stdd s on s.inspect_code = t.inspect_code left join  ht_qlt_inspect_proj r  on t.inspect_code = r.inspect_code left join ht_inner_inspect_group h on h.id = r.inspect_group left join ht_qlt_inspect_event j on j.record_id = t.id where r.inspect_type = '1' and not( t.inspect_value >s.lower_value and t.inspect_value <s.upper_value) and t.RECORD_TIME between '" + txtBtime.Text + "' and '" + txtEtime.Text + "'  union select t.ID, '过程检验'  as inspect_type,r.inspect_code,h.section_name as insgroup,r.inspect_name,nvl(t.inspect_value,0) as value,s.lower_value||'~'||s.upper_value as range,r.unit,nvl(j.status,0) as status ,s.minus_score  from ht_qlt_inspect_record t  left join ht_qlt_inspect_stdd s on s.inspect_code = t.inspect_code left join ht_qlt_inspect_proj r  on r.inspect_code = t.inspect_code left join ht_pub_tech_section h on h.section_code = r.inspect_group left join ht_qlt_inspect_event j on j.record_id = t.id where r.inspect_type = '0' and not( t.inspect_value >s.lower_value and t.inspect_value <s.upper_value) and t.RECORD_TIME between '" + txtBtime.Text + "' and '" + txtEtime.Text + "'   order by inspect_type,insgroup ";
        DataSet data = opt.CreateDataSetOra(query);
        GridView2.DataSource = data;
        GridView2.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            foreach (GridViewRow row in GridView2.Rows)
            {
                if ("处理中" == (((Label)row.FindControl("labStatus")).Text = opt.GetSegValue("select * from ht_inner_inspect_status where id = '" + data.Tables[0].Rows[row.RowIndex]["status"].ToString() + "'", "NAME")))
                    ((Button)row.FindControl("btngrid2Deal")).Text = "处理";
                else if ("己忽略" == ((Label)row.FindControl("labStatus")).Text)
                    ((Button)row.FindControl("btngrid2Deal")).Visible = false;
                else
                    ((Button)row.FindControl("btngrid2Deal")).Text = "查看";
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
        txtStyle.Text = GridView2.DataKeys[row.RowIndex].Values[1].ToString();
        if (((Button)row.FindControl("btngrid2Deal")).Text == "查看")
        {
            DataBaseOperator opt = new DataBaseOperator();
            DataSet data = opt.CreateDataSetOra("select * from HT_QLT_INSPECT_EVENT where RECORD_ID = '" + txtEventID.Text + "' and INSPECT_CODE = '" + txtStyle.Text + "'");
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                DataRow drow = data.Tables[0].Rows[0];
                txtScean.Text = drow["SCENE"].ToString();
                txtReason.Text = drow["REASON"].ToString();
                txtDeal.Text = drow["DEAL"].ToString();
                txtPlus.Text = drow["REMARK"].ToString();
            }
        }
        else
        {
            txtScean.Text = "";
            txtReason.Text = "";
            txtDeal.Text = "";
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
        txtStyle.Text = GridView1.DataKeys[row.RowIndex].Values[1].ToString();
        if (((Button)row.FindControl("btngrid1Deal")).Text == "查看")
        {
            DataBaseOperator opt = new DataBaseOperator();
            DataSet data = opt.CreateDataSetOra("select * from HT_QLT_AUTO_EVENT where RECORD_ID = '" + txtEventID.Text + "' and SORT = '" + txtStyle.Text + "'");
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                DataRow drow = data.Tables[0].Rows[0];
                txtScean.Text = drow["SCENE"].ToString();
                txtReason.Text = drow["REASON"].ToString();
                txtDeal.Text = drow["DEAL"].ToString();
                txtPlus.Text = drow["REMARK"].ToString();
            }
        }
        else
        {
            txtScean.Text = "";
            txtReason.Text = "";
            txtDeal.Text = "";
            txtPlus.Text = "";
        }
        ScriptManager.RegisterStartupScript(updtpanel1, this.Page.GetType(), "", " $('.shade').fadeIn(200);", true);

    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        DataBaseOperator opt = new DataBaseOperator();
        if (hdType.Value == "1")
        {
            string[] seg = {"RECORD_ID","SORT","STATUS","REASON","SCENE","DEAL","REMARK" };
            string[] value = {txtEventID.Text,txtStyle.Text,"3",txtReason.Text,txtScean.Text,txtDeal.Text,txtPlus.Text};
            opt.MergeInto(seg, value, 2, "HT_QLT_AUTO_EVENT");
            bindgrid1();
        }
        else
        {            
            string[] seg = { "RECORD_ID", "INSPECT_CODE", "STATUS", "REASON", "SCENE", "DEAL", "REMARK" };
            string[] value = { txtEventID.Text, txtStyle.Text, "3", txtReason.Text, txtScean.Text, txtDeal.Text, txtPlus.Text };
            opt.MergeInto(seg, value, 2, "HT_QLT_INSPECT_EVENT");
            bindgrid2();
        }
        ScriptManager.RegisterStartupScript(updtpanel1, this.Page.GetType(), "", " $('.shade').fadeOut(200);", true);
    }
 

}