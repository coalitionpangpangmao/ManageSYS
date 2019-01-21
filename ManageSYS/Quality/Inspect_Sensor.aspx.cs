using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
public partial class Quality_Inspect_Sensor : MSYS.Web.BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listEditor, "select ID,Name from ht_SVR_USER where is_del = '0' order by id", "NAME", "ID");
            opt.bindDropDownList(listEditor2, "select ID,Name from ht_SVR_USER where is_del = '0' order by id", "NAME", "ID");
            txtMonth.Text = System.DateTime.Now.ToString("yyyy-MM");
            bindGrid1();

        }
        bindGrid2(hideID.Value);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid1();
    }
    protected void bindGrid1()
    {
        string query = "select t.sensor_month,s.name,t.sensor_time,t.record_time,t.ID ,t.creat_id from ht_qlt_sensor_record t left join ht_svr_user s on s.id = t.creat_id where t.sensor_month = '" + txtMonth.Text + "'";
        if (listEditor.SelectedValue != "")
            query += " and t.creat_id = '" + listEditor.SelectedValue + "'";
        query += "  order by t.ID";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();
    }

    protected void btnAddPlan_Click(object sender, EventArgs e)//新增计划
    {
        listEditor2.SelectedValue = ((MSYS.Data.SysUser)Session["User"]).id;
        txtRecordTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
     
        hideID.Value = opt.GetSegValue("select sensor_record_id_seq.nextval as id from dual", "ID");
      
    }
    protected void btnDel_Click(object sender, EventArgs e)//计划删除
    {
        List<String> commandlist = new List<String>();
        foreach (GridViewRow row in GridView1.Rows)
        {
            int index = row.RowIndex;
            if (((CheckBox)row.FindControl("chk")).Checked)
            {
                string id = GridView1.DataKeys[index].Value.ToString();
                commandlist.Add("delete from  HT_QLT_SENSOR_RECORD  where ID = '" + id + "'");
            }
        }
        if (commandlist.Count > 0)
        {
            commandlist.Add("delete from HT_QLT_SENSOR_RECORD_SUB where is_valid = '0'");
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string log_message = opt.TransactionCommand(commandlist) == "Success" ? "删除感观评测原始记录成功" : "删除感观评测原始记录失败";
            InsertTlog(log_message);
            bindGrid1();
        }

    }
    protected void btnGridEdit_Click(object sender, EventArgs e)//编制计划
    {

        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        string id = GridView1.DataKeys[row.RowIndex].Values[0].ToString();
        txtMonth2.Text = row.Cells[1].Text;
        txtRecordTime.Text = row.Cells[4].Text;
        txtSensorTime.Text = row.Cells[2].Text;
        listEditor2.SelectedValue = GridView1.DataKeys[row.RowIndex].Values[1].ToString();
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.UpDateOra("delete from HT_QLT_SENSOR_RECORD_SUB where is_valid = '0'");
        bindGrid2(id);
      

    }


    protected void bindGrid2(string ID)
    {       
        hideID.Value = ID;
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select * from hv_qlt_sensor_report where main_id = '" + ID + "' order by record_time";
        DataSet set = opt.CreateDataSetOra(query);
        if (set != null)
        {
            DataTable data = set.Tables[0];
            attachData(data);
        }

    }

    protected void createGridView(DataTable data)
    {
        //按表结构生成显示表     
        GridView2.Columns.Clear();
        TemplateField customField;
        customField = new TemplateField();
        customField.ShowHeader = true;
        customField.HeaderTemplate = new MSYS.GridViewTemplate(DataControlRowType.Header, "选择", "");
        customField.ItemTemplate = new MSYS.GridViewTemplate(DataControlRowType.DataRow, "sel", "CheckBox");
        ViewState["ck_sel"] = true;
        GridView2.Columns.Add(customField);
        customField = new TemplateField();
        customField.ShowHeader = true;
        customField.HeaderTemplate = new MSYS.GridViewTemplate(DataControlRowType.Header, "产品", "");
        customField.ItemTemplate = new MSYS.GridViewTemplate(DataControlRowType.DataRow, "prod", "DropDownList");
        ViewState["list_prod"] = true;
        GridView2.Columns.Add(customField);
        for (int j = 3; j < data.Columns.Count; j++)
        {
            customField = new TemplateField();
            customField.ShowHeader = true;
            customField.HeaderTemplate = new MSYS.GridViewTemplate(DataControlRowType.Header, data.Columns[j].ColumnName, "");
            customField.ItemTemplate = new MSYS.GridViewTemplate(DataControlRowType.DataRow, j.ToString(), "TextBox");
            ViewState["txt_" + j.ToString()] = true;
            GridView2.Columns.Add(customField);
        }
    }

    protected void attachData(DataTable data)
    {
        try
        {
            createGridView(data);
            GridView2.DataSource = data;
            GridView2.DataBind();
            if (data != null && data.Rows.Count > 0)
            {
                MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                for (int k = 0; k < GridView2.Rows.Count; k++)
                {
                    DataRowView mydrv = data.DefaultView[k];
                    DropDownList list = (DropDownList)GridView2.Rows[k].FindControl("list_prod");
                    opt.bindDropDownList(list, "select prod_name ,prod_code from ht_pub_prod_design where is_valid = '1' and is_del  = '0' order by prod_code DESC", "prod_name", "prod_code");
                    list.SelectedValue = mydrv[data.Columns[0].ColumnName].ToString();
                    for (int j = 3; j < data.Columns.Count; j++)
                    {
                        ((TextBox)GridView2.Rows[k].FindControl("txt_" + j.ToString())).Width = 100;
                        ((TextBox)GridView2.Rows[k].FindControl("txt_" + j.ToString())).Text = mydrv[data.Columns[j].ColumnName].ToString();
                    }
                }
            }
        }
        catch (Exception ee)
        {
        }
      
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string[] seg = { "ID", "CREAT_ID", "RECORD_TIME", "SENSOR_TIME", "SENSOR_MONTH" };
        string[] value = { hideID.Value, listEditor2.SelectedValue, txtRecordTime.Text, txtSensorTime.Text, txtMonth2.Text };

        string log_message = opt.MergeInto(seg, value, 1, "HT_QLT_SENSOR_RECORD") == "Success" ? "新增感观评测原始记录成功" : "新增感观评测原始记录失败";
        log_message += "--详情:" + string.Join(",", value);
        InsertTlog(log_message);
        opt.UpDateOra("delete from HT_QLT_SENSOR_RECORD_SUB where is_valid = '0'");
        bindGrid1();
        bindGrid2(hideID.Value);
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "alert", "alert('" + log_message + "')", true);

    } 

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        List<string> commandlist = new List<string>();
        DataSet data = opt.CreateDataSetOra("select distinct t.inspect_code from ht_qlt_inspect_proj t  where t.inspect_group = '4'  and t.is_del = '0' order by inspect_code");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string[] seg = { "MAIN_ID", "PROD_CODE", "record_time", "INSPECT_CODE", "INSPECT_SCORE", "IS_VALID" };

            string rnd = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            foreach (DataRow row in data.Tables[0].Rows)
            {
                string[] value = { hideID.Value, "",rnd,row[0].ToString(),  "", "0"};
                commandlist.Add(opt.getMergeStr(seg, value,4, "HT_QLT_SENSOR_RECORD_SUB"));
            }
            if (commandlist.Count > 0)
                opt.TransactionCommand(commandlist);
        }
        bindGrid2(hideID.Value);
    }
    
    protected void btnDelSel_Click(object sender, EventArgs e)
    {
        try
        {
            
            List<string> commandlist = new List<string>();
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
          
            for (int i = 0; i < GridView2.Rows.Count ; i++)
            {
                if (((CheckBox)GridView2.Rows[i].FindControl("ck_sel")).Checked)
                {
                    string mtr_code = GridView2.DataKeys[i].Value.ToString();
                    mtr_code = (mtr_code == "") ? " is null" :"='" +  mtr_code + "'";
                    string prod_code = ((DropDownList) GridView2.Rows[i].FindControl("list_prod")).SelectedValue;
                    commandlist.Add("delete from HT_QLT_SENSOR_RECORD_SUB where main_id = '" + hideID.Value + "' and PROD_CODE = '" + prod_code + "' and record_time  "+ mtr_code );                   
                }
            }
            string log_message = opt.TransactionCommand(commandlist) == "Success" ? "删除感观评测原始记录详情成功" : "删除感观评测原始记录详情失败";           
            InsertTlog(log_message);
            bindGrid2(hideID.Value);
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
  
    protected void btnGrid2Modify_Click(object sender, EventArgs e)
    {
        try
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            List<string> commandlist = new List<string>();
            DataSet data = opt.CreateDataSetOra("select distinct t.inspect_code from ht_qlt_inspect_proj t  where t.inspect_group = '4'  and t.is_del = '0' order by inspect_code");
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                string[] seg = { "MAIN_ID", "PROD_CODE",  "record_time","INSPECT_CODE", "INSPECT_SCORE", "IS_VALID" };
                DataRowCollection Rows = data.Tables[0].Rows;
                foreach (GridViewRow gridrow in GridView2.Rows)
                {
                    string prod = ((DropDownList)gridrow.FindControl("list_prod")).SelectedValue;
                    string rnd = GridView2.DataKeys[gridrow.RowIndex].Value.ToString();
                    for (int i = 0; i < Rows.Count; i++)
                    {
                        string[] value = { hideID.Value, prod,rnd , Rows[i][0].ToString(), ((TextBox)gridrow.FindControl("txt_" + (i + 3).ToString())).Text, "1" };
                        commandlist.Add(opt.getMergeStr(seg, value,4, "HT_QLT_SENSOR_RECORD_SUB"));
                    }                    
                }
                if (commandlist.Count > 0)
                {
                    string log_message = opt.TransactionCommand(commandlist) == "Success" ? "保存感观评测原始记录详情成功" : "保存删除感观评测原始记录详情失败";
                    InsertTlog(log_message);
                    ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "alert", "alert('" + log_message + "')", true);
                }
            }
            opt.UpDateOra("delete from HT_QLT_SENSOR_RECORD_SUB where is_valid = '0'");
            bindGrid2(hideID.Value);

        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
   




}