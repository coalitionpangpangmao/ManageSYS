using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Craft_Tech_Process : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           DataBaseOperator opt =new DataBaseOperator();
            opt.bindDropDownList(list2Section, "select section_code,section_name from ht_pub_tech_section where is_del = '0' and is_valid = '1' order by section_code", "section_name", "section_code");
        }
    }
    protected void bindData(string processcode)
    {
        string query = "select * from HT_PUB_INSPECT_PROCESS where  PROCESS_CODE = '" + processcode + "'";
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            txtCode.Text = processcode;
            txtName.Text = data.Tables[0].Rows[0]["PROCESS_NAME"].ToString();
            txtDscrp.Text = data.Tables[0].Rows[0]["REMARK"].ToString();
            rdValid.Checked = Convert.ToBoolean(Convert.ToDecimal(data.Tables[0].Rows[0]["IS_VALID"].ToString()));
            list2Section.SelectedValue = processcode.Substring(0, 5);
            txt2pcode.Text = processcode.Substring(0, 5);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string[] seg = { "PROCESS_CODE", "PROCESS_NAME", "REMARK", "IS_VALID" };
        string[] value = { txtCode.Text, txtName.Text, txtDscrp.Text, Convert.ToInt16(rdValid.Checked).ToString()};
       DataBaseOperator opt =new DataBaseOperator();
        opt.InsertData(seg, value, "HT_PUB_INSPECT_PROCESS");
        
    }
  
    protected void btnModify_Click(object sender, EventArgs e)
    {
        string[] seg = {  "PROCESS_NAME", "REMARK", "IS_VALID" };
        string[] value = {  txtName.Text, txtDscrp.Text, Convert.ToInt16(rdValid.Checked).ToString() };
        string condition = " where PROCESS_CODE = '" + txtCode.Text + "'";
       DataBaseOperator opt =new DataBaseOperator();
        opt.UpDateData(seg, value, "HT_PUB_INSPECT_PROCESS", condition);
        
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
       DataBaseOperator opt =new DataBaseOperator();       
       string query = "update ht_pub_inspect_process set IS_DEL = '1' where PROCESS_CODE = '" + txtCode.Text + "'";
        opt.UpDateOra(query);
        query = "update HT_PUB_TECH_PARA set IS_DEL = '1' where substr(PARA_CODE,1,7) =  '" + txtCode.Text + "'";
        opt.UpDateOra(query);
    }
 
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        bindData(hdcode.Value);

    }
    protected void list2Section_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt2pcode.Text = list2Section.SelectedValue;
    }
    protected void hdcode_ValueChanged(object sender, EventArgs e)
    {

    }
}