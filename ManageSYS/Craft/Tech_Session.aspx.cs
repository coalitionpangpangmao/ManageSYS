using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Craft_Tech_Session : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
           
        }
    }
    protected void bindData(string session_code)
    {
        string query = "select * from HT_PUB_TECH_SECTION where  SECTION_CODE = '" + session_code + "'";
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            txtCode.Text = session_code;
            txtName.Text = data.Tables[0].Rows[0]["SECTION_NAME"].ToString();
            txtDscrp.Text = data.Tables[0].Rows[0]["REMARK"].ToString();
            rdValid.Checked = Convert.ToBoolean(Convert.ToDecimal( data.Tables[0].Rows[0]["IS_VALID"].ToString()));
        }
    }
    protected void updateNav()
    {
         Response.Write("<script>javascript:window.parent.frames['subleftFrame'].location='Tech_TreeNav.aspx';</script>");
       // Response.Redirect("Tech_TreeNav.aspx? target = 'subleftFrame'");
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string str = opt.GetSegValue("select Max(Section_code) as Code from ht_pub_tech_section t", "CODE");
        if (str == "")
            str = "00000";
        txtCode.Text = "703" + (Convert.ToInt16(str.Substring(3)) + 1).ToString().PadLeft(2, '0');
     
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
    
        {
            string[] seg = { "SECTION_CODE", "SECTION_NAME", "REMARK", "IS_VALID", "CREATE_ID", "CREATE_TIME" };
            string[] value = { txtCode.Text, txtName.Text, txtDscrp.Text, Convert.ToInt16(rdValid.Checked).ToString(), ((MSYS.Data.SysUser)Session["user"]).id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };

            opt.MergeInto(seg, value,1, "HT_PUB_TECH_SECTION");
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "updatetree", "  window.parent.update()", true);
        }
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        string query = "update HT_PUB_TECH_SECTION set IS_DEL = '1' where SECTION_CODE = '" + txtCode.Text + "'";  
        opt.UpDateOra(query);
        query = "update ht_pub_inspect_process set IS_DEL = '1' where substr(PROCESS_CODE,1,5) = '" + txtCode.Text + "'";
        opt.UpDateOra(query);
        query = "update HT_PUB_TECH_PARA set IS_DEL = '1' where substr(PARA_CODE,1,5) =  '" + txtCode.Text + "'";
        opt.UpDateOra(query);
        
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        bindData(hdcode.Value);
        
    }
}