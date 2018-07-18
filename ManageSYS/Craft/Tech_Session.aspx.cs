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
            try
            {
                string session_code = Request["session_code"].ToString();
                if (session_code != "")
                {
                    bindData(session_code);
                }
            }
            catch
            { }
        }
    }
    protected void bindData(string session_code)
    {
        string query = "select * from HT_PUB_TECH_SECTION where  SECTION_CODE = '" + session_code + "'";
       DataBaseOperator opt =new DataBaseOperator();
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
        string[] seg = { "SECTION_CODE", "SECTION_NAME", "REMARK", "IS_VALID" };
        string[] value = {txtCode.Text,txtName.Text,txtDscrp.Text,Convert.ToInt16(rdValid.Checked).ToString()};
       DataBaseOperator opt =new DataBaseOperator();
        opt.InsertData(seg, value, "HT_PUB_TECH_SECTION");
        
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        string[] seg = {  "SECTION_NAME", "REMARK", "IS_VALID" };
        string[] value = { txtName.Text, txtDscrp.Text, Convert.ToInt16(rdValid.Checked).ToString() };
        string condition = " where SECTION_CODE = '" + txtCode.Text + "'";
       DataBaseOperator opt =new DataBaseOperator();
        opt.UpDateData(seg, value, "HT_PUB_TECH_SECTION", condition);
        
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        DataBaseOperator opt =new DataBaseOperator();
        string query = "update HT_PUB_TECH_SECTION set IS_DEL = '1' where SECTION_CODE = '" + txtCode.Text + "'";  
        //opt.UpDateOra(query);
        string log_message = (opt.UpDateOra(query) == "Success") ? "删除成功" : "删除失败";
        opt.InsertTlog(Session["userName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
        query = "update ht_pub_inspect_process set IS_DEL = '1' where substr(PROCESS_CODE,1,5) = '" + txtCode.Text + "'";
        //opt.UpDateOra(query);
        log_message = (opt.UpDateOra(query) == "Success") ? "删除成功" : "删除失败";
        opt.InsertTlog(Session["userName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
        query = "update HT_PUB_TECH_PARA set IS_DEL = '1' where substr(PARA_CODE,1,5) =  '" + txtCode.Text + "'";
        //opt.UpDateOra(query);
        log_message = (opt.UpDateOra(query) == "Success") ? "删除成功" : "删除失败";
        opt.InsertTlog(Session["userName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
        
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        bindData(hdcode.Value);
        
    }
}