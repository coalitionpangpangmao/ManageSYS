using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Craft_Tech_Para : MSYS.Web.BasePage
{
   protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            DataBaseOperator opt = new DataBaseOperator();
            opt.bindDropDownList(listSection, "select section_code,section_name from ht_pub_tech_section where is_valid = '1' and is_del = '0' order by section_code", "section_name", "section_code");
        }
       
    }
    protected void bindData(string paracode)
    {
        try
        {
            string query = "select * from HT_PUB_TECH_PARA where  PARA_CODE = '" + paracode + "'";
            DataBaseOperator opt = new DataBaseOperator();
            DataSet data = opt.CreateDataSetOra(query);
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                DataRow row = data.Tables[0].Rows[0];
                txtCode.Text = paracode;
                txtName.Text = row["PARA_NAME"].ToString();
                txtUnit.Text = row["PARA_UNIT"].ToString();
                txtSetTag.Text = row["SET_TAG"].ToString();
                txtValueTag.Text = row["VALUE_TAG"].ToString();
                setType(row["PARA_TYPE"].ToString());
                txtDscrp.Text = row["REMARK"].ToString();
                rdValid.Checked = ("1" == row["IS_VALID"].ToString());
                opt.bindDropDownList(listEquip, "select EQ_NAME,IDKEY from ht_eq_eqp_tbl t where t.section_code = '" + txtCode.Text.Substring(0, 5) + "'", "EQ_NAME", "IDKEY");
                listEquip.SelectedValue = row["EQUIP_CODE"].ToString();
                listSection.SelectedValue = paracode.Substring(0, 5);
                
            }
        }
        catch (Exception error)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "error", "<Script>alert('" + error.Message + "')</Script>", false);
        }
    }
 
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        DataBaseOperator opt = new DataBaseOperator();
        string str = opt.GetSegValue("select max(para_code) as CODE from ht_pub_tech_para where substr(para_code,0,5)= '" + listSection.SelectedValue + "'", "CODE");
        if (str == "")
            str = "000000000";
        txtCode.Text = listSection.SelectedValue + (Convert.ToInt16(str.Substring(5)) + 1).ToString().PadLeft(5, '0');
      
    }
    protected string getType()
    {
        string type = "";
        if (ckCenterCtrl.Checked)
            type += "1";
        else
            type += "0";
        if (ckRecipePara.Checked)
            type += "1";
        else
            type += "0";
        if (ckSetPara.Checked)
            type += "1";
        else
            type += "0";
        if (ckQuality.Checked)
            type += "1";
        else
            type += "0";
        return type;
    }
    protected void setType(string Type)
    {
        if (Type.Length >= 4)
        {
            ckCenterCtrl.Checked = Convert.ToBoolean(Convert.ToInt16(Type.Substring(0, 1)));
            ckRecipePara.Checked = Convert.ToBoolean(Convert.ToInt16(Type.Substring(1, 1)));
            ckSetPara.Checked = Convert.ToBoolean(Convert.ToInt16(Type.Substring(2, 1)));
            ckQuality.Checked = Convert.ToBoolean(Convert.ToInt16(Type.Substring(3, 1)));
        }
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        DataBaseOperator opt = new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra("select *  from HT_PUB_TECH_PARA where PARA_CODE = '" + txtCode.Text + "'");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string[] seg = { "PARA_NAME", "PARA_UNIT", "PARA_TYPE", "REMARK", "IS_VALID", "MODIFY_ID", "MODIFY_TIME", "EQUIP_CODE", "SET_TAG", "VALUE_TAG" };
            string[] value = { txtName.Text, txtUnit.Text, getType(), txtDscrp.Text, Convert.ToInt16(rdValid.Checked).ToString(), ((MSYS.Data.SysUser)Session["user"]).Id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), listEquip.SelectedValue, txtSetTag.Text,txtValueTag.Text };
            string condition = " where PARA_CODE = '" + txtCode.Text + "'";
            opt.UpDateData(seg, value, "HT_PUB_TECH_PARA", condition);
        }
        else
        {
            string[] seg = { "PARA_CODE", "PARA_NAME", "PARA_UNIT", "PARA_TYPE", "REMARK", "IS_VALID", "CREATE_ID", "CREATE_TIME", "EQUIP_CODE", "SET_TAG", "VALUE_TAG" };
            string[] value = { txtCode.Text, txtName.Text, txtUnit.Text, getType(), txtDscrp.Text, Convert.ToInt16(rdValid.Checked).ToString(), ((MSYS.Data.SysUser)Session["user"]).Id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), listEquip.SelectedValue, txtSetTag.Text, txtValueTag.Text };
            opt.InsertData(seg, value, "HT_PUB_TECH_PARA");
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "error", "updateModel();", true);
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
       DataBaseOperator opt =new DataBaseOperator();      
       string query = "update HT_PUB_TECH_PARA set IS_DEL = '1' where PARA_CODE =  '" + txtCode.Text + "'";
        opt.UpDateOra(query);
        
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        bindData(hdcode.Value);

    }


    protected void listSection_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        DataBaseOperator opt = new DataBaseOperator();
        opt.bindDropDownList(listEquip, "select EQ_NAME,IDKEY from ht_eq_eqp_tbl t where t.Section_code = '" + listSection.SelectedValue + "'", "EQ_NAME", "IDKEY");
        txtCode.Text = "";
    }
}