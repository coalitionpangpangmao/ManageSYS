using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MSYS.Data;
using MSYS.Web;
using System.Data;
public partial class Authority_ChgPsd : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);

    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if (Session["User"] != null)
        {
            string userID = ((SysUser)Session["User"]).id;
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataTable userTable = opt.CreateDataSetOra("select * from ht_svr_user t where t.id = '" + userID + "'").Tables[0];

            if (userTable != null && userTable.Rows.Count > 0)
            {
                string oldPwd = userTable.Rows[0]["PASSWORD"].ToString();
                if (oldPwd == MSYS.Security.Encrypt.GetMD5String(txtold.Text))
                {
                    if (txtPwd1.Text == txtpwd2.Text)
                    {
                        string userPwd = MSYS.Security.Encrypt.GetMD5String(txtPwd1.Text);

                        string log_message = opt.UpDateOra("update ht_svr_user set PASSWORD = '" + userPwd + "' where ID = '" + userID + "'") == "Success" ? "修改密码成功" : "修改密码失败";
                        log_message += "用户ID:" + userID;
                        InsertTlog(log_message);
                        string returnUrl = Request["ReturnUrl"];
                        if (string.IsNullOrEmpty(returnUrl))
                            returnUrl = "default.aspx";
                        Response.Redirect(returnUrl);
                    }
                }
                else
                    Response.Write("<script language=\"javascript\" type=\"text/javascript\">alert('原始密码输入有误，请再次输入密码！！'); </script>");
            }
            else
            {
                Response.Write("<script language=\"javascript\" type=\"text/javascript\">alert('密码输入有误，请再次输入密码！！'); </script>");
            }

        }
    }
}