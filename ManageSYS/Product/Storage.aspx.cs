using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
public partial class Product_StorageOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // bindGrid1();
           
        }
 
    }
 /*   protected void bindGrid1()
    {
        string query = "select id, plan_name as 计划名,adjust_status 是否有调整,e_flow_status as 审批状态,ISSUED_STATUS as 下发状态 ,creator as 编制人  from ht_prd_month_plan where is_del = '0'";
        if (txtStart.Text != "" && txtStart.Text != "")
            query += " and PLAN_TIME between '" + txtStart.Text + "' and  '" + txtStop.Text;
       DataBaseOperator opt =new DataBaseOperator();
        GridView1.DataSource = opt.CreateDataSetOra(query); ;
        GridView1.DataBind();

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid1();
    }
    protected void bindGrid2(string planID)
    {
        if (Regex.IsMatch(planID, @"^[+-]?/d*$"))
            hidePlanID.Value = planID;
        else hidePlanID.Value = planID.Substring(planID.LastIndexOf(',') + 1);
        string query = " select plan_order as 顺序号, plan_no as 计划号, prod_code as 产品规格,plan_output as 计划产量 from HT_PROD_MONTH_PLAN_DETAIL where is_del = '0' and  MONTH_PLAN_ID = " + planID;
       
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra(query);       
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            GridView2.DataSource = data; 
            GridView2.DataBind();
            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                ((TextBox)GridView2.Rows[i].FindControl("txtOrder")).Text = mydrv["顺序号"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtPlanNo")).Text = mydrv["计划号"].ToString();
                ((DropDownList)GridView2.Rows[i].FindControl("listProd")).SelectedValue = mydrv["产品规格"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtOutput")).Text = mydrv["计划产量"].ToString();
               
            }

        }

    }
    public DataSet ddlbind()
    {
        string sqlstr = "select prod_name as 产品规格,prod_code from ht_pub_prod_design where is_valid = '1' and is_del  = '0'";
       DataBaseOperator opt =new DataBaseOperator();
        return opt.CreateDataSetOra(sqlstr);
    }
    protected void btnIssued_Click(object sender, EventArgs e)//下发计划
    {
        try
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked)
                {
                    string id = GridView1.DataKeys[i].Value.ToString();
                    string query = "update ht_prd_month_plan set ISSUED_STATUS = '己下发'  where ID = '" + id + "'";
                   DataBaseOperator opt =new DataBaseOperator();
                    opt.UpDateOra(query);
                }
            }
            bindGrid1();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnGridDel_Click(object sender, EventArgs e)//计划删除
    {
        try
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked)
                {
                    string id = GridView1.DataKeys[i].Value.ToString();
                    string query = "update ht_prd_month_plan set IS_DEL = '1'  where ID = '" + id + "'";
                   DataBaseOperator opt =new DataBaseOperator();
                    opt.UpDateOra(query);
                }
            }
            bindGrid1();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnGridEdit_Click(object sender, EventArgs e)//编制计划
    {
        try
        {
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号  
            string id = GridView1.DataKeys[Rowindex].Value.ToString();
            txtYear.Text = GridView1.Rows[Rowindex].Cells[1].Text.Substring(0, 4);
            listMonth.SelectedValue = GridView1.Rows[Rowindex].Cells[1].Text.Substring(5, 2);
            bindGrid2(id);
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "GridClick();", true);
         //   string query = "update HT_QA_MATER_FORMULA_DETAIL set IS_DEL = '1'  where FORMULA_CODE = '" + txtCode.Text + "' and MATER_CODE = '" + mtr_code + "'";
         //  DataBaseOperator opt =new DataBaseOperator();
         //   opt.UpDateOra(query);
         //   bindGrid(txtCode.Text);
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnGridIssue_Click(object sender, EventArgs e)//审批进度
    {
        try
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked)
                {
                    string id = GridView1.DataKeys[i].Value.ToString();
               //     string query = "update ht_prd_month_plan set IS_DEL = '1'  where ID = '" + id + "'";
               //    DataBaseOperator opt =new DataBaseOperator();
               //     opt.UpDateOra(query);
                }
            }
            
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (!Regex.IsMatch(hidePlanID.Value, @"^[+-]?/d*$"))
                hidePlanID.Value = hidePlanID.Value.Substring(hidePlanID.Value.LastIndexOf(',') + 1);
            string query = "select plan_order as 顺序号, plan_no as 计划号, prod_code as 产品规格,plan_output as 计划产量 from HT_PROD_MONTH_PLAN_DETAIL where is_del = '0' and  MONTH_PLAN_ID = " + hidePlanID.Value;
           DataBaseOperator opt =new DataBaseOperator();
            DataSet set = opt.CreateDataSetOra(query);
            DataTable data = new DataTable();
            if (set == null)
            {
                data.Columns.Add("顺序号");
                data.Columns.Add("计划号");
                data.Columns.Add("产品规格");
                data.Columns.Add("计划产量");
            }
            else
                data = set.Tables[0];
            object[] value = { "", "", "", 0 };
            data.Rows.Add(value);
            GridView2.DataSource = data;
            GridView2.DataBind();
            if (data != null && data.Rows.Count > 0)
            {
                for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
                {
                    DataRowView mydrv = data.DefaultView[i];
                    ((TextBox)GridView2.Rows[i].FindControl("txtOrder")).Text = mydrv["顺序号"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtPlanNo")).Text = mydrv["计划号"].ToString();
                    ((DropDownList)GridView2.Rows[i].FindControl("listProd")).SelectedValue = mydrv["产品规格"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtOutput")).Text = mydrv["计划产量"].ToString();
                    /*     if (i < GridView1.Rows.Count - 1)
                         {
                             ((TextBox)GridView2.Rows[i].FindControl("txtOrder")).Enabled = false;
                             ((TextBox)GridView2.Rows[i].FindControl("txtPlanNo")).Enabled = false;
                             ((DropDownList)GridView2.Rows[i].FindControl("listProd")).Enabled = false;
                             ((TextBox)GridView2.Rows[i].FindControl("txtOutput")).Enabled = false;
                         }
                         else
                         {
                             ((TextBox)GridView2.Rows[i].FindControl("txtOrder")).Enabled = true;
                             ((TextBox)GridView2.Rows[i].FindControl("txtPlanNo")).Enabled = true;
                             ((DropDownList)GridView2.Rows[i].FindControl("listProd")).Enabled = true;
                             ((TextBox)GridView2.Rows[i].FindControl("txtOutput")).Enabled = true;
                         }
                }
            }
        }
        catch (Exception ee)
        {
            string str = ee.Message;
        }
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
       DataBaseOperator opt =new DataBaseOperator();
        string planname = txtYear.Text + "-" + listMonth.SelectedValue;
        string query = "select * from ht_prd_month_plan where plan_name = '" + planname + "生产月计划' and  is_del = '0'";
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            return;
        }
        else
        {
            string[] seg = { "PLAN_YEAR", "PROD_MONTH", "PLAN_NAME","PLAN_TIME"};
            string[] value = { txtYear.Text,listMonth.SelectedValue,planname + "生产月计划",planname };
            opt.InsertData(seg, value, "ht_prd_month_plan");
            hidePlanID.Value = opt.GetSegValue(query, "ID");
        }
        bindGrid1();

    }

    protected void btnCkAll_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                ((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked = true;
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
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked)
                {
                    string mtr_code = ((TextBox)GridView1.Rows[i].FindControl("txtPlanNo")).Text;
                    string query = "update HT_PROD_MONTH_PLAN_DETAIL set IS_DEL = '1'  where PLAN_NO = '" + mtr_code +  "'";
                   DataBaseOperator opt =new DataBaseOperator();
                    opt.UpDateOra(query);
                }
            }
            bindGrid2(hidePlanID.Value);
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }    
    protected void btnGrid2Del_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号             
            string mtr_code = ((TextBox)GridView1.Rows[Rowindex].FindControl("txtPlanNo")).Text;
            string query = "update HT_PROD_MONTH_PLAN_DETAIL set IS_DEL = '1'  where PLAN_NO = '" + mtr_code + "'";
           DataBaseOperator opt =new DataBaseOperator();
            opt.UpDateOra(query);
            bindGrid2(hidePlanID.Value);
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void btnGrid2Save_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号             
            string mtr_code = ((TextBox)GridView2.Rows[Rowindex].FindControl("txtPlanNo")).Text;
            if (!Regex.IsMatch(hidePlanID.Value, @"^[+-]?/d*$"))
                hidePlanID.Value = hidePlanID.Value.Substring(hidePlanID.Value.LastIndexOf(',') + 1);
            string query = "select * from HT_PROD_MONTH_PLAN_DETAIL where MONTH_PLAN_ID = " + hidePlanID.Value + " and plan_no = '" + mtr_code + "' and is_del = '0'";
           DataBaseOperator opt =new DataBaseOperator();
            DataSet data = opt.CreateDataSetOra(query);
            if(data!= null && data.Tables[0].Rows.Count > 0)
            {
                 string[] seg = { "plan_order",  "prod_code","plan_output"};
            string[] value = {  ((TextBox)GridView2.Rows[Rowindex].FindControl("txtOrder")).Text,((DropDownList)GridView2.Rows[Rowindex].FindControl("listProd")).SelectedValue , ((TextBox)GridView2.Rows[Rowindex].FindControl("txtOutput")).Text};
            opt.UpDateData(seg, value, "HT_PROD_MONTH_PLAN_DETAIL", " where MONTH_PLAN_ID = " + hidePlanID.Value + " and plan_no = '" + mtr_code + "' and is_del = '0'");
            }         
          
            else
            {
          string[] seg = { "plan_order", "plan_no", "prod_code","plan_output","MONTH_PLAN_ID"};
            string[] value = {  ((TextBox)GridView2.Rows[Rowindex].FindControl("txtOrder")).Text, mtr_code,((DropDownList)GridView2.Rows[Rowindex].FindControl("listProd")).SelectedValue , ((TextBox)GridView2.Rows[Rowindex].FindControl("txtOutput")).Text,hidePlanID.Value};
            opt.InsertData(seg, value, "HT_PROD_MONTH_PLAN_DETAIL");
            }
            bindGrid2(hidePlanID.Value);
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }*/ 
  
}