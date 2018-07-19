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
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            opt.bindDropDownList(list2Section, "select section_code,section_name from ht_pub_tech_section where is_del = '0' and is_valid = '1' order by section_code", "section_name", "section_code");
        }
    }
    protected void bindData(string processcode)
    {
        string query = "select * from HT_PUB_INSPECT_PROCESS where  PROCESS_CODE = '" + processcode + "'";
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
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

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string str = opt.GetSegValue("select max(Process_code) as code  from ht_pub_inspect_process where substr(process_code,0,5) = '" + list2Section.SelectedValue + "'", "CODE");
        if (str == "")
            str = "0000000";
        txtCode.Text = list2Section.SelectedValue + (Convert.ToInt16(str.Substring(5)) + 1).ToString().PadLeft(2, '0');
       
    }
  
    protected void btnModify_Click(object sender, EventArgs e)
    {
         MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
          DataSet data = opt.CreateDataSetOra("select *  from HT_PUB_INSPECT_PROCESS where PROCESS_CODE = '" + txtCode.Text + "'");
          if (data != null && data.Tables[0].Rows.Count > 0)
          {
              string[] seg = { "PROCESS_NAME", "REMARK", "IS_VALID", "MODIFY_ID", "MODIFY_TIME" };
              string[] value = { txtName.Text, txtDscrp.Text, Convert.ToInt16(rdValid.Checked).ToString(), ((MSYS.Data.SysUser)Session["user"]).id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
              string condition = " where PROCESS_CODE = '" + txtCode.Text + "'";
              opt.UpDateData(seg, value, "HT_PUB_INSPECT_PROCESS", condition);
          }
          else
          {
              string[] seg = { "PROCESS_CODE", "PROCESS_NAME", "REMARK", "IS_VALID", "CREATE_ID", "CREATE_TIME" };
              string[] value = { txtCode.Text, txtName.Text, txtDscrp.Text, Convert.ToInt16(rdValid.Checked).ToString(), ((MSYS.Data.SysUser)Session["user"]).id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
              opt.InsertData(seg, value, "HT_PUB_INSPECT_PROCESS");
          }
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();       
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
        txtCode.Text = "";
    }
}