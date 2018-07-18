using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Craft_RecipeAux : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
          
        }
    }
    protected void bindData()
    {
        string query = "select FORMULA_CODE  as 配方编号,FORMULA_NAME  as 配方名称,PROD_CODE  as 产品编码,STANDARD_VOL  as 标准版本号,B_DATE  as 执行日期,E_DATE  as 结束日期,CONTROL_STATUS  as 受控状态,CREATOR  as 编制人,CREATE_DATE  as 编制日期,CREATE_DEPT  as 编制部门,REMARK  as 备注,is_valid from ht_qa_aux_formula where is_del = '0' and FORMULA_CODE = '" + hdcode.Value + "'";
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if(data!= null && data.Tables[0].Rows.Count > 0)
        {
            txtCode.Text = hdcode.Value;
            txtName.Text = data.Tables[0].Rows[0]["配方名称"].ToString();
            txtPro.Text = data.Tables[0].Rows[0]["产品编码"].ToString();
            txtVersion.Text = data.Tables[0].Rows[0]["标准版本号"].ToString();
            txtExeDate.Text = data.Tables[0].Rows[0]["执行日期"].ToString();
            txtEndDate.Text = data.Tables[0].Rows[0]["结束日期"].ToString();
            listStatus.SelectedValue = data.Tables[0].Rows[0]["受控状态"].ToString();
            txtCreator.Text = data.Tables[0].Rows[0]["编制人"].ToString();
            txtCrtDate.Text = data.Tables[0].Rows[0]["编制日期"].ToString();
            listCrtApt.SelectedValue = data.Tables[0].Rows[0]["编制部门"].ToString();
            txtDscpt.Text = data.Tables[0].Rows[0]["备注"].ToString();
            ckValid.Checked = ("1" == data.Tables[0].Rows[0]["is_valid"].ToString());            
        }
        bindGrid();
    }
    protected void bindGrid()
  {
      string query = "select r.mater_type as 类别, r.MATER_CODE   as 物料编码,s.material_name as 物料名称,r.aux_scale  as 比例,r.aux_percent   as 占原料比例 from ht_qa_aux_formula_detail r left join ht_pub_materiel s on s.material_code = r.mater_code  where r.is_del = '0'  and FORMULA_CODE = '" + hdcode.Value + "'";
    DataBaseOperator opt =new DataBaseOperator();
     DataSet data = opt.CreateDataSetOra(query);
      GridView1.DataSource = data;
      GridView1.DataBind();
      if (data != null && data.Tables[0].Rows.Count > 0)
      {
          for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
          {
              DataRowView mydrv = data.Tables[0].DefaultView[i];
              ((TextBox)GridView1.Rows[i].FindControl("txtCodeM")).Text = mydrv["物料编码"].ToString();
              ((DropDownList)GridView1.Rows[i].FindControl("listGridName")).SelectedValue = mydrv["物料编码"].ToString();
              ((TextBox)GridView1.Rows[i].FindControl("txtScale")).Text = mydrv["比例"].ToString();
              ((TextBox)GridView1.Rows[i].FindControl("txtPercent")).Text = mydrv["占原料比例"].ToString();
              ((DropDownList)GridView1.Rows[i].FindControl("listGridType")).SelectedValue = mydrv["类别"].ToString();
            
          }

      }
  }

    protected DataSet gridTypebind()
    {
       DataBaseOperator opt =new DataBaseOperator();
        return opt.CreateDataSetOra("select material_code,material_name from ht_pub_materiel  where is_valid = '1'  and is_del = '0' and TYPE_FLAG = 'FL'");      
    }
    protected void listGirdName_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList list = (DropDownList)sender;
        int rowIndex = ((GridViewRow)list.NamingContainer).RowIndex;
        ((TextBox)GridView1.Rows[rowIndex].FindControl("txtCodeM")).Text = list.SelectedValue;      
    }
    
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string query = "select r.mater_type as 类别, r.MATER_CODE   as 物料编码,s.material_name as 物料名称,r.aux_scale  as 比例,r.aux_percent   as 占原料比例 from ht_qa_aux_formula_detail r left join ht_pub_materiel s on s.material_code = r.mater_code  where r.is_del = '0'  and FORMULA_CODE = '" + hdcode.Value + "'";
       DataBaseOperator opt =new DataBaseOperator();
        DataSet set = opt.CreateDataSetOra(query);
        DataTable data = set.Tables[0];
        if (data == null)
        {
            data = new DataTable();
            data.Columns.Add("类别");
            data.Columns.Add("物料编码");
            data.Columns.Add("物料名称");
            data.Columns.Add("比例");
            data.Columns.Add("占原料比例");
        }
            object[] value = { "", "", "", 0, 0 };
            data.Rows.Add(value);        
        GridView1.DataSource = data;
        GridView1.DataBind();
        if (data != null && data.Rows.Count > 0)
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                DataRowView mydrv = data.DefaultView[i];
                ((TextBox)GridView1.Rows[i].FindControl("txtCodeM")).Text = mydrv["物料编码"].ToString();
                ((DropDownList)GridView1.Rows[i].FindControl("listGridName")).SelectedValue = mydrv["物料编码"].ToString();
                ((TextBox)GridView1.Rows[i].FindControl("txtScale")).Text = mydrv["比例"].ToString();
                ((TextBox)GridView1.Rows[i].FindControl("txtPercent")).Text = mydrv["占原料比例"].ToString();
                ((DropDownList)GridView1.Rows[i].FindControl("listGridType")).SelectedValue = mydrv["类别"].ToString();
              /*  if (i < GridView1.Rows.Count - 1)
                {
                    ((TextBox)GridView1.Rows[i].FindControl("txtCodeM")).Enabled = false;
                    ((TextBox)GridView1.Rows[i].FindControl("txtNameM")).Enabled = false;
                    ((TextBox)GridView1.Rows[i].FindControl("txtAmountM")).Enabled = false;
                    ((TextBox)GridView1.Rows[i].FindControl("txtGroupM")).Enabled = false;
                    ((TextBox)GridView1.Rows[i].FindControl("txtSortM")).Enabled = false;
                }
                else
                {
                    ((TextBox)GridView1.Rows[i].FindControl("txtCodeM")).Enabled = true;
                    ((TextBox)GridView1.Rows[i].FindControl("txtNameM")).Enabled = true;
                    ((TextBox)GridView1.Rows[i].FindControl("txtAmountM")).Enabled = true;
                    ((TextBox)GridView1.Rows[i].FindControl("txtGroupM")).Enabled = true;
                    ((TextBox)GridView1.Rows[i].FindControl("txtSortM")).Enabled = true;
                }*/
            }

        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        bindData();
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
       DataBaseOperator opt =new DataBaseOperator();
        string query = "select * from ht_qa_aux_formula where FORMULA_CODE = '" + txtCode.Text + "'";
        DataSet data = opt.CreateDataSetOra(query);
        hdcode.Value = txtCode.Text;
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string[] seg = { "FORMULA_NAME", "PROD_CODE", "STANDARD_VOL", "B_DATE", "E_DATE", "CONTROL_STATUS", "CREATOR", "CREATE_DATE", "CREATE_DEPT", "REMARK" };
            string[] value = {  txtName.Text, txtPro.Text, txtVersion.Text, txtExeDate.Text, txtEndDate.Text, listStatus.SelectedValue, txtCreator.Text, txtCrtDate.Text, listCrtApt.SelectedValue, txtDscpt.Text, };
            string condition = " where FORMULA_CODE = '" + txtCode.Text + "'";
            string log_message = opt.UpDateData(seg, value, "ht_qa_aux_formula", condition)=="Success"? "辅料配方保存成功":"辅料配方保存失败";
            log_message = ",辅料配方保存信息：" + string.Join(" ", value);
            opt.InsertTlog(Session["UserNmae"].ToString(), Page.Request.UserHostName.ToString(), log_message);
        }
        else
        {

            string[] seg = { "FORMULA_CODE", "FORMULA_NAME", "PROD_CODE", "STANDARD_VOL", "B_DATE", "E_DATE", "CONTROL_STATUS", "CREATOR", "CREATE_DATE", "CREATE_DEPT", "REMARK" };
            string[] value = { txtCode.Text, txtName.Text, txtPro.Text, txtVersion.Text, txtExeDate.Text, txtEndDate.Text, listStatus.SelectedValue, txtCreator.Text, txtCrtDate.Text, listCrtApt.SelectedValue, txtDscpt.Text, };
            string log_message = opt.InsertData(seg, value, "ht_qa_aux_formula")=="Success"? "辅料配方保存成功":"辅料配方保存失败";
            log_message += ",辅料配方保存信息：" + string.Join(" ", value);
            opt.InsertTlog(Session["UserNmae"].ToString(), Page.Request.UserHostName.ToString(), log_message);
        }
        bindGrid();
        
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号  
            string mtr_code = ((TextBox)GridView1.Rows[Rowindex].FindControl("txtCodeM")).Text;
            string query = "update ht_qa_aux_formula_DETAIL set IS_DEL = '1'  where FORMULA_CODE = '" + txtCode.Text + "' and MATER_CODE = '" + mtr_code + "'";
           DataBaseOperator opt =new DataBaseOperator();
            string log_message = opt.UpDateOra(query)=="Success" ? "物料删除成功" : "物料删除失败";
            log_message+=",物料编号："+txtCode.Text;
            opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
            bindGrid();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
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
                    string mtr_code = ((TextBox)GridView1.Rows[i].FindControl("txtCodeM")).Text;
                    string query = "update ht_qa_aux_formula_DETAIL set IS_DEL = '1'  where FORMULA_CODE = '" + txtCode.Text + "' and MATER_CODE = '" + mtr_code + "'";
                   DataBaseOperator opt =new DataBaseOperator();
                    string log_message = opt.UpDateOra(query)=="Success" ? " 物料删除成功":"物料删除失败";
                    log_message += ",物料编号:"+txtCode.Text;
                    opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
                }
            }
            bindGrid();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号  
            string mtr_code = ((TextBox)GridView1.Rows[Rowindex].FindControl("txtCodeM")).Text;
            if (Rowindex >= 0)
            {               
               DataBaseOperator opt =new DataBaseOperator();                
                    opt.UpDateOra("delete from ht_qa_aux_formula_detail  where FORMULA_CODE = '" + txtCode.Text + "' and MATER_CODE = '" + mtr_code + "'");               
                    string[] seg = { "FORMULA_CODE", "MATER_CODE",  "aux_scale", "aux_percent", "mater_type" };
                    string[] value = { txtCode.Text, mtr_code, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtNameM")).Text, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtScale")).Text, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtPercent")).Text, ((DropDownList)GridView1.Rows[Rowindex].FindControl("listGridType")).SelectedValue };
                string log_message = opt.InsertData(seg, value, "ht_qa_aux_formula_detail")=="Success" ? "物料保存成功":"物料保存失败";
                log_message += ",物料信息：" + txtCode.Text;
                opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
                bindGrid();
            }
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
 

    }
   
}