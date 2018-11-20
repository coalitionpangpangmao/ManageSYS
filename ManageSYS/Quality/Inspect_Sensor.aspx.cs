using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Quality_Inspect_Sensor : MSYS.Web.BasePage
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
       MSYS.Data.SysUser user =  (MSYS.Data.SysUser)Session["User"];
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listProd, "select prod_code,prod_name from ht_pub_prod_design where is_del= '0'", "prod_name", "prod_code");
        opt.bindDropDownList(listTeam, "select team_code,team_name from ht_sys_team where is_del = '0' order by team_code", "team_name", "team_code");
        opt.bindDropDownList(listShift, "select shift_code,shift_name from ht_sys_shift where is_del = '0'", "shift_name", "shift_code");
        listTeam.SelectedValue = opt.GetSegValue(" select team_code from ht_prod_schedule where date_begin < '" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' and date_end > '" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "team_code");
        listShift.SelectedValue = opt.GetSegValue(" select Shift_code from ht_prod_schedule where date_begin < '" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' and date_end > '" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "Shift_code");
        listEditor.Items.Clear();
        ListItem item = new ListItem(user.text, user.id);
        listEditor.Items.Add(item);
        listEditor.SelectedValue = user.id;
        txtProdTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd");

        string query = "select r.inspect_code,s.name as insgroup,r.inspect_name,0 as value,t.lower_value,t.upper_value,r.unit,'' as status,t.minus_score from ht_qlt_inspect_proj r left join ht_inner_inspect_group s on s.id = r.inspect_group left join ht_qlt_inspect_stdd t on t.inspect_code = r.inspect_code and t.is_del = '0' where r.inspect_group = '4' and r.is_del = '0'";
        
        DataSet data = opt.CreateDataSetOra(query);
        bindGrid1(data);
        
    }
    protected void bindGrid1(DataSet data)
    {
            GridView1.DataSource = data;
            GridView1.DataBind();
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                {
                    DataRowView mydrv = data.Tables[0].DefaultView[i];

                    ((TextBox)GridView1.Rows[i].FindControl("txtPara")).Text = mydrv["value"].ToString();
                    ((TextBox)GridView1.Rows[i].FindControl("txtValue")).Text = mydrv["lower_value"].ToString() + "~" + mydrv["upper_value"].ToString();
                    ((Label)GridView1.Rows[i].FindControl("labStatus")).Text = mydrv["status"].ToString();
                    if (mydrv["status"].ToString() == "超限")
                    {
                        ((TextBox)GridView1.Rows[i].FindControl("txtScore")).Text = mydrv["minus_score"].ToString();
                        ((Label)GridView1.Rows[i].FindControl("labStatus")).CssClass = "labstatu";
                    }
                    else
                    {
                        ((TextBox)GridView1.Rows[i].FindControl("txtScore")).Text = "0";
                         ((Label)GridView1.Rows[i].FindControl("labStatus")).CssClass = "labstatuGreen";
                    }
                }
            }
   
    }
   
    protected void listProd_SelectedIndexChanged(object sender,EventArgs e)
    {
        bindData();
      
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {

            string[] seg = { "INSPECT_CODE", "PROD_CODE","TEAM_ID", "RECORD_TIME",  "SHIFT_ID", "INSPECT_VALUE", "CREAT_ID", "CREATE_TIME" };
            string[] value = {GridView1.DataKeys[row.RowIndex].Value.ToString(), listProd.SelectedValue,  listTeam.SelectedValue, txtProdTime.Text,listShift.SelectedValue, ((TextBox)row.FindControl("txtPara")).Text, listEditor.SelectedValue, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
           
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string log_message = opt.MergeInto(seg, value, 4, "HT_QLT_INSPECT_RECORD") == "Success" ? "保存感观评测结果成功" : "保存感观评测结果失败";
            log_message += "--详情:" + string.Join(",", value);
            InsertTlog(log_message);
            bindData();
           
        }
    }

    protected void bindData()
    {
        string query = "select r.inspect_code, h.name as insgroup,r.inspect_name,nvl(t.inspect_value,0) as value,s.lower_value,s.upper_value,r.unit,case when t.inspect_value is null then '' when t.inspect_value > s.lower_value and t.inspect_value <s.upper_value then '合格' else '超限' end as status,s.minus_score  from ht_qlt_inspect_proj r left join ht_qlt_inspect_stdd s on s.inspect_code = r.inspect_code and s.is_del = '0' left join ht_qlt_inspect_record t on t.inspect_code = r.inspect_code left join ht_inner_inspect_group h on h.id = r.inspect_group where r.inspect_type = '1' and  r.inspect_group = '4' and r.is_del = '0' and t.prod_code = '" + listProd.SelectedValue + "' and t.team_id = '" + listTeam.SelectedValue + "' and t.record_time = '" + txtProdTime.Text + "' union select r.inspect_code,s.name as insgroup,r.inspect_name,0 as value,t.lower_value,t.upper_value,r.unit,'' as status,t.minus_score from ht_qlt_inspect_proj r left join ht_inner_inspect_group s on s.id = r.inspect_group left join ht_qlt_inspect_stdd t on t.inspect_code = r.inspect_code and t.is_del = '0' where  r.inspect_group = '4'  and r.is_del = '0' and r.inspect_code in (select inspect_code from ht_qlt_inspect_proj where  inspect_group = '4' and is_del = '0' minus select inspect_code from ht_qlt_inspect_record where Prod_code = '" + listProd.SelectedValue + "' and team_id = '" + listTeam.SelectedValue + "' and record_time = '" + txtProdTime.Text + "')";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
            bindGrid1(data);
    }


    protected void txtProdTime_TextChanged(object sender, EventArgs e)
    {
        bindData();
    }
    protected void listTeam_SelectedIndexChanged(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string temp = opt.GetSegValue("select shift_code from ht_prod_schedule where work_date = '" + txtProdTime.Text + "' and team_code = '" + listTeam.SelectedValue + "'", "shift_code");
        if (temp != "NoRecord")
            listShift.SelectedValue = temp;
        bindData();
    }


 
}