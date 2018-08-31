using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MSYS.Data;
public partial class Authority_UserInfo : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listApt, "select F_CODE,F_NAME from HT_SVR_ORG_GROUP", "F_NAME", "F_CODE");
            opt.bindDropDownList(listRole, "select * from ht_svr_sys_role t", "F_ROLE", "F_ID");
            if (Session["User"] != null)
            {
                SysUser user = (SysUser)Session["User"];
                txtID.Text = user.id;
                txtName.Text = user.text;
                 listApt.SelectedValue = user.OwningBusinessUnitId;
                 listRole.SelectedValue = user.UserRoleID; 
                DataSet dt = opt.CreateDataSetOra("select * from ht_svr_user where ID = '" + txtID.Text + "'");
                if(dt != null && dt.Tables[0].Rows.Count > 0)
                {
                    DataRow row = dt.Tables[0].Rows[0];
                txtPhone.Text = row["MOBILE"].ToString();
                txtCallNO.Text= row["PHONE"].ToString();
                txtFax.Text =row["RTXID"].ToString();
                setGender(row["GENDER"].ToString());
                txtUser.Text= row["LOGINNAME"].ToString();
                txtEmail.Text= row["EMAIL"].ToString();               
                txtDscp.Text =row["DESCRIPTION"].ToString();
                }
            }
    }
    }


    protected string getGender()
    {
        if (rdMale.Checked)
            return "男";
        else
            return "女";
    }
    protected void setGender(string gender)
    {
        if (gender == "男")
            rdMale.Checked = true;
        else
            rdFemale.Checked = true;
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string[] seg = { "ID", "NAME", "MOBILE", "PHONE", "RTXID", "GENDER", "LOGINNAME", "EMAIL", "LEVELGROUPID", "DESCRIPTION", "ROLE" };
        string[] value = { txtID.Text, txtName.Text, txtPhone.Text, txtCallNO.Text, txtFax.Text, getGender(), txtUser.Text, txtEmail.Text, listApt.SelectedValue, txtDscp.Text, listRole.SelectedValue };

        string log_message = opt.UpDateData(seg, value, "ht_svr_user", " where ID = '" + txtID.Text + "'") == "Success" ? "修改用户成功" : "修改用户失败";
        log_message += "数据信息:" + string.Join(" ", value);
        InsertTlog(log_message);      

    }

}