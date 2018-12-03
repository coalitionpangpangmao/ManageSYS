using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Authority_UserConfig : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listApt, "select F_CODE,F_NAME from HT_SVR_ORG_GROUP", "F_NAME", "F_CODE");
            opt.bindDropDownList(listRole, "select * from ht_svr_sys_role t", "F_ROLE", "F_ID");
            opt.bindDropDownList(listTeam, "select team_code,team_name from ht_sys_team t where t.is_del = '0' order by team_code", "team_name", "team_code");
            bindData();
        }
    }

    protected void bindData()
    {
        string query = "select  t.ID  as 人员ID, t.NAME  as 人员名称, r.f_name  as 组织机构名称,s.f_role as 角色,a.team_name as 班组,t.MOBILE  as 手机, t.PHONE  as 座机, t.RTXID  as 传真, t.GENDER  as 性别,  t.EMAIL  as 电子邮件, t.DESCRIPTION  as 描述 from ht_svr_user t left join ht_sys_team a on a.team_code = t.team_code left join ht_svr_org_group r on r.f_code = t.levelgroupid left join ht_svr_sys_role s on s.f_id = t.role where t.IS_DEL = '0' order by t.ID";
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        GridView1.DataSource = opt.CreateDataSetOra(query);
        GridView1.DataBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView theGrid = sender as GridView;
        int newPageIndex = 0;
        if (e.NewPageIndex == -3)
        {
            //点击跳转按钮
            TextBox txtNewPageIndex = null;

            //GridView较DataGrid提供了更多的API，获取分页块可以使用BottomPagerRow 或者TopPagerRow，当然还增加了HeaderRow和FooterRow
            GridViewRow pagerRow = theGrid.BottomPagerRow;

            if (pagerRow != null)
            {
                //得到text控件
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;
            }
            if (txtNewPageIndex != null)
            {
                //得到索引
                newPageIndex = int.Parse(txtNewPageIndex.Text) - 1;
            }
        }
        else
        {
            //点击了其他的按钮
            newPageIndex = e.NewPageIndex;
        }
        //防止新索引溢出
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;
        //得到新的值
        theGrid.PageIndex = newPageIndex;
        //重新绑定

        bindData();
    }
    protected void ck_CheckedChanged(object sender, EventArgs e)
    {
        int ckno = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (((CheckBox)GridView1.Rows[i].FindControl("ck")).Checked)
                ckno++;
        }
        bool check = (ckno < GridView1.Rows.Count);
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            ((CheckBox)GridView1.Rows[i].FindControl("ck")).Checked = check;

        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {

        int num = 0;
        string id = "";
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (((CheckBox)GridView1.Rows[i].FindControl("ck")).Checked)
            {
                num++;
                id = GridView1.DataKeys[i].Value.ToString();
            }
        }
        if (num == 0 || num > 1)
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "alert('请选择需查看用户!!!');", true);
        else if (num == 1)
        {
            txtID.Text = id;
            string query = "select * from ht_svr_user where ID = '" + txtID.Text + "'";
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            DataSet data = opt.CreateDataSetOra(query);
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                txtName.Text = data.Tables[0].Rows[0]["NAME"].ToString();
                txtWeight.Text = data.Tables[0].Rows[0]["WEIGHT"].ToString();
                txtPrt.Text = data.Tables[0].Rows[0]["PARENTID"].ToString();
                txtPhone.Text = data.Tables[0].Rows[0]["MOBILE"].ToString();
                txtCallNO.Text = data.Tables[0].Rows[0]["PHONE"].ToString();
                txtFax.Text = data.Tables[0].Rows[0]["RTXID"].ToString();
                setGender(data.Tables[0].Rows[0]["GENDER"].ToString());
                txtUser.Text = data.Tables[0].Rows[0]["LOGINNAME"].ToString();
                txtPswd.Text = data.Tables[0].Rows[0]["PASSWORD"].ToString();
                txtEmail.Text = data.Tables[0].Rows[0]["EMAIL"].ToString();
                listApt.SelectedValue = data.Tables[0].Rows[0]["LEVELGROUPID"].ToString();
                rdLocal.Checked = (data.Tables[0].Rows[0]["IS_LOCAL"].ToString() == "1");
                rdAsyn.Checked = (data.Tables[0].Rows[0]["IS_SYNC"].ToString() == "1");
                listTeam.SelectedValue = data.Tables[0].Rows[0]["Team_code"].ToString();
                txtDscp.Text = data.Tables[0].Rows[0]["DESCRIPTION"].ToString();
                listRole.SelectedValue = data.Tables[0].Rows[0]["ROLE"].ToString();
            }
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", " $('.shade').fadeIn(200);", true);
        }

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        SetBlank();
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
       string id = opt.GetSegValue("select nvl(max(substr(id,3,5)),0)+1 as id from ht_svr_user where substr(id,1,2) = '07'", "ID").PadLeft(5,'0');
        txtID.Text = "07" + id;
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", " $('.shade').fadeIn(200);", true);

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
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
       string userPwd;
    if (txtPswd.Text == "")
    {
        userPwd = opt.GetSegValue("select password from ht_svr_user where id= '" + txtID.Text + "'", "password");
        txtPswd.Text = opt.GetSegValue("select psd from ht_svr_user where id= '" + txtID.Text + "'", "psd");
    }
    else
    {
        userPwd = MSYS.Security.Encrypt.GetMD5String(txtPswd.Text);
    }
        string[] seg = { "ID", "NAME", "WEIGHT", "PARENTID", "MOBILE", "PHONE", "RTXID", "GENDER", "LOGINNAME", "PASSWORD", "EMAIL", "LEVELGROUPID",  "IS_LOCAL", "IS_SYNC", "IS_DEL", "DESCRIPTION", "ROLE","PSD" ,"TEAM_CODE"};
        string[] value = { txtID.Text, txtName.Text, txtWeight.Text, txtPrt.Text, txtPhone.Text, txtCallNO.Text, txtFax.Text, getGender(), txtUser.Text, userPwd, txtEmail.Text, listApt.SelectedValue, Convert.ToInt16(rdLocal.Checked).ToString(), Convert.ToInt16(rdAsyn.Checked).ToString(), "0", txtDscp.Text, listRole.SelectedValue,txtPswd.Text,listTeam.SelectedValue };        

        string log_message = opt.MergeInto(seg, value, 1, "ht_svr_user") == "Success" ? "修改用户成功" : "修改用户失败";
        value[9] = "";
        log_message += "--详情：" + string.Join(",", value);
        InsertTlog(log_message);  

        bindData();
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", " $('.shade').fadeOut(200);", true);
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
  	for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (((CheckBox)GridView1.Rows[i].FindControl("ck")).Checked)
            {
                string log_message = opt.UpDateOra("delete from ht_svr_user where ID = '" + GridView1.DataKeys[i].Value.ToString() + "'") == "Success" ? "删除用户成功" : "删除用户失败";
                log_message += "--标识:" + GridView1.DataKeys[i].Value.ToString();
                InsertTlog(log_message);                
            }     	    
        }
        bindData();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        MSYS.Web.UpdateUserInfo info = new MSYS.Web.UpdateUserInfo();
        //  string str = info.GetXmlStr();
        string result = info.InsertLocalFromMaster();
        if ("Success" == result)
        {
            bindData();
            ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "update", "alert('更新完毕！！！')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "update", "alert('" + result + "')", true);
        }
    }

    protected void SetBlank()
    {
        txtID.Text = "";
        txtName.Text = "";
        txtWeight.Text = "";
        txtPrt.Text = "";
        txtPhone.Text = "";
        txtCallNO.Text = "";
        txtFax.Text = "";
        txtUser.Text = "";
        txtPswd.Text = "";
        txtEmail.Text = "";
        listApt.SelectedValue = "";
        listRole.SelectedValue = "";
        rdLocal.Checked = false;
        rdAsyn.Checked = false;
        listTeam.SelectedValue = "";
        txtDscp.Text = "";


    }


}