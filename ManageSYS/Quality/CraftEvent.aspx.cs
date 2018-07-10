using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Quality_CraftEvent : MSYS.Web.BasePage
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
        string query = "select t.para_name,s.prod_name,r.type,r.value,r.range,r.b_time,r.e_time,h.team_name,nvl(j.status,0) as status,nvl(r.minus_score,0) as minus_score,r.id from hv_qlt_data_event r left join ht_pub_prod_design s on s.prod_code = r.prod_code left join ht_pub_tech_para t on t.para_code = r.para_code left join ht_sys_team h on h.team_code = r.team left join ht_qlt_auto_event j on j.record_id = r.id and j.sort = r.type where r.b_time>'" + txtBtime.Text + "' and r.e_time <'" + txtEtime.Text + "'";

        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                ((Label)row.FindControl("labStatus")).Text = opt.GetSegValue("select * from ht_inner_inspect_status where id = '" + data.Tables[0].Rows[row.RowIndex]["status"].ToString() + "'", "NAME");
            }
        }
    }

    protected void bindgrid2()
    {
        DataBaseOperator opt = new DataBaseOperator();
        string query = "select t.ID,'成品检测'  as inspect_type ,r.inspect_code, h.name as insgroup,r.inspect_name,nvl(t.inspect_value,0) as value,s.lower_value||'~'||s.upper_value as range,r.unit,nvl(j.status,0) as status  ,nvl(s.minus_score,0) as minus_score  from ht_qlt_inspect_record t left join ht_qlt_inspect_stdd s on s.inspect_code = t.inspect_code left join  ht_qlt_inspect_proj r  on t.inspect_code = r.inspect_code left join ht_inner_inspect_group h on h.id = r.inspect_group left join ht_qlt_inspect_event j on j.record_id = t.id where r.inspect_type = '1' and not( t.inspect_value >s.lower_value and t.inspect_value <s.upper_value) and t.RECORD_TIME between '" + txtBtime.Text + "' and '" + txtEtime.Text + "' union select t.ID, '过程检验'  as inspect_type,r.inspect_code,h.section_name as insgroup,r.inspect_name,nvl(t.inspect_value,0) as value,s.lower_value||'~'||s.upper_value as range,r.unit,nvl(j.status,0) as status ,nvl(s.minus_score,0) as minus_score   from ht_qlt_inspect_record t  left join ht_qlt_inspect_stdd s on s.inspect_code = t.inspect_code left join ht_qlt_inspect_proj r  on r.inspect_code = t.inspect_code left join ht_pub_tech_section h on h.section_code = r.inspect_group left join ht_qlt_inspect_event j on j.record_id = t.id where r.inspect_type = '0' and not( t.inspect_value >s.lower_value and t.inspect_value <s.upper_value) and t.RECORD_TIME between '" + txtBtime.Text + "' and '" + txtEtime.Text + "'  order by inspect_type,insgroup ";
        DataSet data = opt.CreateDataSetOra(query);
        GridView2.DataSource = data;
        GridView2.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            foreach (GridViewRow row in GridView2.Rows)
            {
                ((Label)row.FindControl("labStatus")).Text = opt.GetSegValue("select * from ht_inner_inspect_status where id = '" + data.Tables[0].Rows[row.RowIndex]["status"].ToString() + "'", "NAME");
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindgrid1();
        bindgrid2();
    }
    /// <summary>
    /// 在线工艺事件
    /// </summary>

    protected void btnSelAll1_Click(object sender, EventArgs e)
    {
        bool check = true;
        if ("全选" == btnSelAll.Text)
        {
            check = true;
            btnSelAll.Text = "取消";
        }
        else
        {
            check = false;
            btnSelAll.Text = "全选";
        }
        foreach (GridViewRow row in GridView1.Rows)
        {
            ((CheckBox)row.FindControl("ck")).Checked = check;
        }
    }
    protected void btnIgnore1_Click(object sender, EventArgs e)
    {
        MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
        foreach (GridViewRow row in GridView1.Rows)
        {
             
            if (((CheckBox)row.FindControl("ck")).Checked)
            {
                int index = row.RowIndex;
                string[] seg = { "RECORD_ID", "SORT", "SCORE", "STATUS", "CREAT_ID", "CREATE_TIME" };
                string[] value = { GridView1.DataKeys[index].Values[0].ToString(), GridView1.DataKeys[index].Values[1].ToString(), row.Cells[10].Text, "1", user.Id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
                DataBaseOperator opt = new DataBaseOperator();
                opt.MergeInto(seg, value, 2, "HT_QLT_AUTO_EVENT");
               
            }
        }
        bindgrid1();
    }
    protected void btnConfirm1_Click(object sender, EventArgs e)
    {
        MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (((CheckBox)row.FindControl("ck")).Checked)
            {
                int index = row.RowIndex;
                string[] seg = { "RECORD_ID", "SORT", "SCORE", "STATUS", "CREAT_ID", "CREATE_TIME" };
                string[] value = { GridView1.DataKeys[index].Values[0].ToString(), GridView1.DataKeys[index].Values[1].ToString(), row.Cells[10].Text, "2", user.Id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
                DataBaseOperator opt = new DataBaseOperator();
                opt.MergeInto(seg, value, 2, "HT_QLT_AUTO_EVENT");
            }
        }
        bindgrid2();
    }
    protected void btngrid1Ignore_Click(object sender, EventArgs e)
    {
        MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int index = row.RowIndex;
        string[] seg = { "RECORD_ID", "SORT", "SCORE", "STATUS", "CREAT_ID", "CREATE_TIME" };
        string[] value = { GridView1.DataKeys[index].Values[0].ToString(), GridView1.DataKeys[index].Values[1].ToString(), row.Cells[10].Text, "1", user.Id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        DataBaseOperator opt = new DataBaseOperator();
        opt.MergeInto(seg, value, 2, "HT_QLT_AUTO_EVENT");
        bindgrid1();
    }
    protected void btngrid1Sure_Click(object sender, EventArgs e)
    {
        MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int index = row.RowIndex;
        string[] seg = { "RECORD_ID", "SORT", "SCORE", "STATUS", "CREAT_ID", "CREATE_TIME" };
        string[] value = { GridView1.DataKeys[index].Values[0].ToString(), GridView1.DataKeys[index].Values[1].ToString(), row.Cells[10].Text, "2", user.Id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        DataBaseOperator opt = new DataBaseOperator();
        opt.MergeInto(seg, value, 2, "HT_QLT_AUTO_EVENT");
        bindgrid1();
    }
    /// <summary>
    /// 离线工艺事件
    /// </summary>
    protected void btnSelAll_Click(object sender, EventArgs e)
    {
        bool check = true;
        if ("全选" == btnSelAll.Text)
        {
            check = true;
            btnSelAll.Text = "取消";
        }
        else
        {
            check = false;
            btnSelAll.Text = "全选";
        }
        foreach (GridViewRow row in GridView2.Rows)
        {
            ((CheckBox)row.FindControl("ck")).Checked = check;
        }
    }
    protected void btnIgnore_Click(object sender, EventArgs e)
    {
        MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
        foreach (GridViewRow row in GridView2.Rows)
        {
            if (((CheckBox)row.FindControl("ck")).Checked)
            {
                int index = row.RowIndex;
                string[] seg = { "RECORD_ID", "INSPECT_CODE", "STATUS","SCORE", "CREAT_ID", "CREATE_TIME" };
                string[] value = { GridView2.DataKeys[index].Values[0].ToString(), GridView2.DataKeys[index].Values[1].ToString(), "1",row.Cells[8].Text, user.Id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
                DataBaseOperator opt = new DataBaseOperator();
                opt.MergeInto(seg, value, 1, "HT_QLT_INSPECT_EVENT");
            }
        }
        bindgrid2();
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
        foreach (GridViewRow row in GridView2.Rows)
        {
            if (((CheckBox)row.FindControl("ck")).Checked)
            {
                int index = row.RowIndex;
                string[] seg = { "RECORD_ID", "INSPECT_CODE", "STATUS", "SCORE", "CREAT_ID", "CREATE_TIME" };
                string[] value = { GridView2.DataKeys[index].Values[0].ToString(), GridView2.DataKeys[index].Values[1].ToString(), "2", row.Cells[8].Text, user.Id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
                DataBaseOperator opt = new DataBaseOperator();
                opt.MergeInto(seg, value, 1, "HT_QLT_INSPECT_EVENT");
            }
        }
        bindgrid2();
    }
    protected void btngrid2Ignore_Click(object sender, EventArgs e)
    {
        MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int index = row.RowIndex;
        string[] seg = { "RECORD_ID", "INSPECT_CODE", "STATUS", "SCORE", "CREAT_ID", "CREATE_TIME" };
        string[] value = { GridView2.DataKeys[index].Values[0].ToString(), GridView2.DataKeys[index].Values[1].ToString(), "1", row.Cells[8].Text, user.Id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        DataBaseOperator opt = new DataBaseOperator();
        opt.MergeInto(seg, value, 1, "HT_QLT_INSPECT_EVENT");
        bindgrid2();
    }
    protected void btngrid2Sure_Click(object sender, EventArgs e)
    {
        MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int index = row.RowIndex;
        string[] seg = { "RECORD_ID", "INSPECT_CODE", "STATUS", "SCORE", "CREAT_ID", "CREATE_TIME" };
        string[] value = { GridView2.DataKeys[index].Values[0].ToString(), GridView2.DataKeys[index].Values[1].ToString(), "2", row.Cells[8].Text, user.Id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        DataBaseOperator opt = new DataBaseOperator();
        opt.MergeInto(seg, value, 1, "HT_QLT_INSPECT_EVENT");
        bindgrid2();
    }


}