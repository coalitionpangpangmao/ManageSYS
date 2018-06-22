using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Drawing;
public partial class Product_ShiftChange : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            txtStartDate.Text = System.DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            txtStopDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
           DataBaseOperator opt =new DataBaseOperator();
            opt.bindDropDownList(listShift, "select t.shift_code,t.shift_name  from ht_sys_shift t where t.is_valid = '1' and t.is_del = '0' order by t.shift_code", "shift_name", "shift_code");
            opt.bindDropDownList(listTeam, "select t.team_code,t.team_name  from ht_sys_team t where t.is_valid = '1' and t.is_del = '0' order by t.team_code", "team_name", "team_code");
            opt.bindDropDownList(listProd, "select t.prod_code,t.prod_name  from ht_pub_prod_design t where t.is_valid = '1' and t.is_del = '0' order by t.prod_code", "prod_name", "prod_code");
            bindGrid1();
         
           
        }
 
    }
    protected void bindGrid1()
    {
        string query = "select g1.work_date as 日期,g2.team_name as 班组,g3.shift_name as 班时,g1.date_begin as 开始时间,g1.date_end as 结束时间,g1.Id,g4.shift_status from ht_prod_schedule g1 left join Ht_Sys_Team g2 on g2.team_code = g1.team_code left join ht_sys_shift g3 on g3.shift_code = g1.shift_code left join ht_prod_shiftchg g4 on g1.id = g4.shift_main_id where g1.work_date between '" + txtStartDate.Text + "' and '" + txtStopDate.Text + "' and g1.is_del = '0' and g1.is_valid = '1' order by g1.work_date,g1.id";
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            foreach (DataRow row in data.Tables[0].Rows)
            {
                Button btn = (Button)GridView1.Rows[i++].FindControl("btnGrid1Edit");
                if ("1" == row["shift_status"].ToString())
                {
                    btn.Text = "查看";
                    btn.CssClass = "btnred";
                }
                else
                {
                    btn.Text = "填写";
                    btn.CssClass = "btn1 auth";
                }
                
            }
        }

    }
    protected void bindGrid2()
    {
        try
        {
            string query = "select t.mater_code as 物料名称,t.mater_vl as 数量,t.bz_unit as 单位,t.remark as 备注 from ht_prod_shiftchg_detail t where t.shift_main_id = '" + hdID.Value + "'";
           DataBaseOperator opt =new DataBaseOperator();
            DataSet data = opt.CreateDataSetOra(query);
           
            
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
                {
                    DataRowView mydrv = data.Tables[0].DefaultView[i];
                    ((TextBox)GridView2.Rows[i].FindControl("txtUnit")).Text = mydrv["单位"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtAmount")).Text = mydrv["数量"].ToString();
                    ((DropDownList)GridView2.Rows[i].FindControl("listMater")).SelectedValue = mydrv["物料名称"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtDescpt")).Text = mydrv["备注"].ToString();

                }
            }
        }
        catch (Exception ee)
        {
            string str = ee.Message;
        }
    }
 
  
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid1();
    }
    protected void btnGrid1Edit_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string id = GridView1.DataKeys[rowIndex].Value.ToString();
        hdID.Value = id;
        string query = "select g.work_date as 日期,g.shift_code as 班时,g.team_code as 班组, g3.prod_code as 牌号,g3.planno as 计划号,g4.output_vl,g4.shift_id,g4.succ_id,g4.remark,g4.create_id,g4.devicestatus,g4.qlt_status,g4.scean_status from Ht_Prod_Schedule g   left join ht_prod_report g3 on (g.date_end between g3.starttime and g3.endtime)  or (g.date_end >g3.starttime and g3.endtime is null) left join ht_prod_shiftchg g4 on g4.shift_main_id = g.id   where g.id = '" + id + "'";
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            DataRow row = data.Tables[0].Rows[0];
            txtDate.Text = row["日期"].ToString();
            listShift.SelectedValue = row["班时"].ToString();
            listTeam.SelectedValue = row["班组"].ToString();
            listProd.SelectedValue = row["牌号"].ToString();
            txtPlanNo.Text = row["计划号"].ToString();
            txtEditor.Text = row["create_id"].ToString();
            txtOutput.Text = row["output_vl"].ToString();
            txtOlder.Text = row["shift_id"].ToString();
            txtNewer.Text = row["succ_id"].ToString();
            txtDevice.Text = row["devicestatus"].ToString();
            txtQlt.Text = row["qlt_status"].ToString();
            txtScean.Text = row["scean_status"].ToString();
            txtRemark.Text = row["remark"].ToString(); ;

        }
        bindGrid2();
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "GridClick();", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
       DataBaseOperator opt =new DataBaseOperator();
        hdID.Value = hdID.Value.Substring(0, hdID.Value.IndexOf(','));
        string[] seg = { "SHIFT_MAIN_ID", "INSPECT_DATE", "SHIFT_CODE", "TEAM_CODE", "PROD_CODE", "PLAN_NO", "OUTPUT_VL", "CREATE_ID", "SHIFT_ID", "SUCC_ID", "DEVICESTATUS", "QLT_STATUS", "SCEAN_STATUS", "REMARK", "OUTPLUS" };
        string[] value = {hdID.Value,txtDate.Text,listShift.SelectedValue,listTeam.SelectedValue,listProd.SelectedValue,txtPlanNo.Text,txtOutput.Text,"cookieID",txtOlder.Text,txtNewer.Text,txtDevice.Text,txtQlt.Text,txtScean.Text,txtRemark.Text,txtOutPlus.Text };
        opt.InsertData(seg, value, "HT_PROD_SHIFTCHG");

    }
    protected DataSet gridTypebind()
    {
       DataBaseOperator opt =new DataBaseOperator();
        return opt.CreateDataSetOra("select material_code,material_name from ht_pub_materiel  where is_valid = '1'  and is_del = '0' and TYPE_FLAG = 'YL'");
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "select t.mater_code as 物料名称,t.mater_vl as 数量,t.bz_unit as 单位,t.remark as 备注 from ht_prod_shiftchg_detail t where t.shift_main_id = '" + hdID.Value + "'" ;
           DataBaseOperator opt =new DataBaseOperator();
            DataSet set = opt.CreateDataSetOra(query);
            DataTable data = new DataTable();
            if (set == null)
            {
                data.Columns.Add("物料名称");
                data.Columns.Add("数量");
                data.Columns.Add("单位");
                data.Columns.Add("备注");
            }
            else
                data = set.Tables[0];
            object[] value = { "", 0, "", "" };
            data.Rows.Add(value);
            GridView2.DataSource = data;
            GridView2.DataBind();
            if (data != null && data.Rows.Count > 0)
            {
                for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
                {
                    DataRowView mydrv = data.DefaultView[i];
                    ((TextBox)GridView2.Rows[i].FindControl("txtUnit")).Text = mydrv["单位"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtAmount")).Text = mydrv["数量"].ToString();
                    ((DropDownList)GridView2.Rows[i].FindControl("listMater")).SelectedValue = mydrv["物料名称"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtDescpt")).Text = mydrv["备注"].ToString();
                  
                }
            }
        }
        catch (Exception ee)
        {
            string str = ee.Message;
        }
    }
 

    protected void btnCkAll_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                ((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked = true;
            }
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnDelSel_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked)
                {
                    string mtr_code = ((DropDownList)GridView2.Rows[i].FindControl("listMater")).SelectedValue;
                    string query = "update ht_prod_shiftchg_detail set IS_DEL = '1'  where mater_code = '" + mtr_code + "' and SHIFT_MAIN_ID = '" + hdID.Value + "'";
                   DataBaseOperator opt =new DataBaseOperator();
                    opt.UpDateOra(query);
                }
            }
            bindGrid2();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void btnGrid2Save_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;        
       DataBaseOperator opt =new DataBaseOperator();
        opt.UpDateOra("delete from HT_PROD_SHIFTCHG_DETAIL where SHIFT_MAIN_ID = '" + hdID.Value + "' and mater_code = '" + ((DropDownList)GridView2.Rows[rowIndex].FindControl("listMater")).SelectedValue + "'");
        string[] seg = { "SHIFT_MAIN_ID", "mater_code", "mater_vl", "bz_unit", "remark" };
        string[] value = { hdID.Value, ((DropDownList)GridView2.Rows[rowIndex].FindControl("listMater")).SelectedValue, ((TextBox)GridView2.Rows[rowIndex].FindControl("txtAmount")).Text, ((TextBox)GridView2.Rows[rowIndex].FindControl("txtUnit")).Text, ((TextBox)GridView2.Rows[rowIndex].FindControl("txtDescpt")).Text };
        opt.InsertData(seg, value, "HT_PROD_SHIFTCHG_DETAIL");
    }
       
 
  
}