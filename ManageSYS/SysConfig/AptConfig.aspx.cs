using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SysConfig_AptConfig : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           DataBaseOperator opt =new DataBaseOperator();
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
           DataBaseOperator opt =new DataBaseOperator();
            DataSet data = opt.CreateDataSetOra(query);
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                txtCode.Text = data.Tables[0].Rows[0]["F_CODE"].ToString();
                txtName.Text = data.Tables[0].Rows[0]["F_NAME"].ToString();
                listParent.SelectedValue = data.Tables[0].Rows[0]["F_PARENTID"].ToString();
                txtType.Text = data.Tables[0].Rows[0]["F_PRITYPE"].ToString();
                txtpath.Text = data.Tables[0].Rows[0]["F_PATH"].ToString();
                txtWeight.Text = data.Tables[0].Rows[0]["F_WEIGHT"].ToString();
                txtSapID.Text = data.Tables[0].Rows[0]["F_SAPID"].ToString();
                txtSAPrecodeID.Text = data.Tables[0].Rows[0]["F_SAP_RECORD_ID"].ToString();
                listRole.SelectedValue = data.Tables[0].Rows[0]["F_ROLE"].ToString();
            }
            ScriptManager.RegisterStartupScript(updtpanel1, this.Page.GetType(), "", " $('.shade').fadeIn(200);", true);
        }

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        SetBlank();
       DataBaseOperator opt =new DataBaseOperator();
        string id = (Convert.ToInt16(opt.GetSegValue("select  nvl(max(substr(F_CODE,4,3)),0) as F_CODE from HT_SVR_ORG_GROUP ", "F_CODE")) + 1).ToString().PadLeft(3, '0');
        txtCode.Text = "007" + id + "00";
        listParent.SelectedValue = "00700000";
        ScriptManager.RegisterStartupScript(updtpanel1, this.Page.GetType(), "", " $('.shade').fadeIn(200);", true);
    }



    protected void btnModify_Click(object sender, EventArgs e)
    {       
       DataBaseOperator opt =new DataBaseOperator();        
        opt.UpDateOra("delete from HT_SVR_ORG_GROUP  where F_CODE = '" + txtCode.Text + "'");
        string[] seg = { "F_CODE", "F_NAME", "F_PARENTID", "F_PRITYPE", "F_PATH", "F_WEIGHT", "F_SAPID", "F_SAP_RECORD_ID", "F_ROLE" };
        string[] value = { txtCode.Text, txtName.Text, listParent.SelectedValue, txtType.Text, txtpath.Text, txtWeight.Text, txtSapID.Text, txtSAPrecodeID.Text, listRole.SelectedValue };      
        opt.InsertData(seg, value, "HT_SVR_ORG_GROUP");
        bindData();
        ScriptManager.RegisterStartupScript(updtpanel1, this.Page.GetType(), "", " $('.shade').fadeOut(200);", true);
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
       DataBaseOperator opt =new DataBaseOperator();
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
        txtpath.Text = "";
        txtWeight.Text = "";
        txtSapID.Text = "";
        txtSAPrecodeID.Text = "";      
    } 
  
    protected void bindData()
    {
        string query = "select  F_CODE   as 组织机构代码,F_NAME  as 组织机构名称,F_PRITYPE  as 类型,F_PATH  as 路径,F_WEIGHT  as 权重,F_PARENTID  as 父级标识,F_SAPID  as SAP标识,F_SAP_RECORD_ID  as SAP记录标识,F_ROLE as 默认角色 from HT_SVR_ORG_GROUP order by F_CODE";
       DataBaseOperator opt =new DataBaseOperator();
        GridView1.DataSource = opt.CreateDataSetOra(query);
        GridView1.DataBind();
    }
}