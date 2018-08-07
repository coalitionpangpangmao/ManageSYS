using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SysConfig_AptConfig : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listRole, "select * from ht_svr_sys_role t", "F_ROLE", "F_ID");
            opt.bindDropDownList(listParent, "select f_code,F_name from ht_svr_org_group where substr(F_CODE,7,2) = '00'", "F_NAME", "F_CODE");
            bindData();
        }
    }



    protected void ck_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ck = (CheckBox)sender;
        for (int i = 0; i < GridView1.Rows.Count; i++)

        {
            ((CheckBox)GridView1.Rows[i].FindControl("ck")).Checked = ck.Checked;

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
            ScriptManager.RegisterStartupScript(updtpanel1, this.Page.GetType(), "", "alert('请选择需查看机构!!!');", true);
        else if (num == 1)
        {            
            txtCode.Text = id;
            string query = "select * from HT_SVR_ORG_GROUP where F_CODE = '" + txtCode.Text + "'";
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            DataSet data = opt.CreateDataSetOra(query);
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                txtCode.Text = data.Tables[0].Rows[0]["F_CODE"].ToString();
                txtName.Text = data.Tables[0].Rows[0]["F_NAME"].ToString();
                listParent.SelectedValue = data.Tables[0].Rows[0]["F_PARENTID"].ToString();
                txtType.Text = data.Tables[0].Rows[0]["F_PRITYPE"].ToString();                
                txtWeight.Text = data.Tables[0].Rows[0]["F_WEIGHT"].ToString();
                txtSapID.Text = data.Tables[0].Rows[0]["F_KEY"].ToString();
              
                listRole.SelectedValue = data.Tables[0].Rows[0]["F_ROLE"].ToString();
            }
            ScriptManager.RegisterStartupScript(updtpanel1, this.Page.GetType(), "", " $('.shade').fadeIn(200);", true);
        }

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        SetBlank();
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        string id = (Convert.ToInt16(opt.GetSegValue("select  nvl(max(substr(F_CODE,4,3)),0) as F_CODE from HT_SVR_ORG_GROUP ", "F_CODE")) + 1).ToString().PadLeft(3, '0');
        txtCode.Text = "007" + id + "00";
        listParent.SelectedValue = "00700000";
        ScriptManager.RegisterStartupScript(updtpanel1, this.Page.GetType(), "", " $('.shade').fadeIn(200);", true);
    }



    protected void btnModify_Click(object sender, EventArgs e)
    {       
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();        
    
        string[] seg = { "F_CODE", "F_NAME", "F_PARENTID", "F_PRITYPE","F_WEIGHT", "F_KEY", "F_ROLE" };
        string[] value = { txtCode.Text, txtName.Text, listParent.SelectedValue, txtType.Text,txtWeight.Text, txtSapID.Text,  listRole.SelectedValue };      
        opt.MergeInto(seg, value,1, "HT_SVR_ORG_GROUP");
        bindData();
        ScriptManager.RegisterStartupScript(updtpanel1, this.Page.GetType(), "", " $('.shade').fadeOut(200);", true);
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (((CheckBox)GridView1.Rows[i].FindControl("ck")).Checked)
            {
                string query = "delete from HT_SVR_ORG_GROUP where F_CODE = '" + GridView1.DataKeys[i].Value.ToString() + "'";
                opt.UpDateOra(query);
            }
        }
        bindData();
    }

    protected void SetBlank()
    {
        txtCode.Text = "";
        txtName.Text = "";
        listParent.SelectedValue = "";
        txtType.Text = "";     
        txtWeight.Text = "";
        txtSapID.Text = "";
        
    } 
  
    protected void bindData()
    {
        string query = "select  r.F_CODE   as 组织机构代码,r.F_NAME  as 组织机构名称,s.f_name  as 父级,t.f_role as 默认角色,r.F_PRITYPE  as 类型,r.F_WEIGHT  as 权重 from HT_SVR_ORG_GROUP r left join ht_svr_org_group s  on r.f_parentid = s.f_code left join ht_svr_sys_role t on t.f_id = r.f_role order by r.F_CODE";
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        GridView1.DataSource = opt.CreateDataSetOra(query);
        GridView1.DataBind();
    }
}