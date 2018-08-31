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
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listSection, "select section_code,section_name from ht_pub_tech_section where is_valid = '1' and is_del = '0' order by section_code", "section_name", "section_code");
        }
       
    }
    protected void bindData(string paracode)
    {
        try
        {
            string query = "select * from HT_PUB_TECH_PARA where  PARA_CODE = '" + paracode + "'";
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
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

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string str = opt.GetSegValue("select max(para_code) as CODE from ht_pub_tech_para where substr(para_code,0,5)= '" + listSection.SelectedValue + "'", "CODE");
        if (str == "")
            str = "000000000";
        txtCode.Text = listSection.SelectedValue + (Convert.ToInt16(str.Substring(5)) + 1).ToString().PadLeft(5, '0');
        txtName.Text = "";
        txtUnit.Text = "";
        txtSetTag.Text = "";
        txtValueTag.Text = "";
        setType("0000000");
        txtDscrp.Text = "";
       
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

        if (ckManul.Checked)
            type += "1";
        else
            type += "0";

        if (ckEqpara.Checked)
            type += "1";
        else
            type += "0";

        if (ckCalibrate.Checked)
            type += "1";
        else
            type += "0";
        return type;
    }
    protected void setType(string Type)
    {
        Type = Type.PadRight(7, '0');
        if (Type.Length >= 7)
        {
            ckCenterCtrl.Checked =("1"==Type.Substring(0, 1));
            ckRecipePara.Checked = ("1" == Type.Substring(1, 1));
            ckSetPara.Checked = ("1" == Type.Substring(2, 1));
            ckQuality.Checked = ("1" == Type.Substring(3, 1));
            ckManul.Checked = ("1" == Type.Substring(4, 1));
            ckEqpara.Checked = ("1" == Type.Substring(5, 1));
            ckCalibrate.Checked = ("1" == Type.Substring(6, 1));
        }
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
     if(txtCode.Text.Length == 10 && txtCode.Text.Substring(0,5) == listSection.SelectedValue)
        {
            string[] seg = { "PARA_CODE", "PARA_NAME", "PARA_UNIT", "PARA_TYPE", "REMARK", "IS_VALID", "CREATE_ID", "CREATE_TIME", "EQUIP_CODE", "SET_TAG", "VALUE_TAG" };
            string[] value = { txtCode.Text, txtName.Text, txtUnit.Text, getType(), txtDscrp.Text, Convert.ToInt16(rdValid.Checked).ToString(), ((MSYS.Data.SysUser)Session["user"]).id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), listEquip.SelectedValue, txtSetTag.Text, txtValueTag.Text };
            opt.MergeInto(seg, value,1, "HT_PUB_TECH_PARA");
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "updatetree", " window.parent.update();", true);
        }
     else
         ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "message", "alert('请确认工艺参数所属工艺段是否正确')", true);
     
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();      
       string query = "delete from HT_PUB_TECH_PARA  where PARA_CODE =  '" + txtCode.Text + "'";
       string log_message = opt.UpDateOra(query) == "Success" ? "删除工艺参数点成功" : "删除工艺参数点失败";
       log_message += "工艺参数点ID:" + txtCode.Text;
       InsertTlog(log_message);
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "updatetree", "  window.parent.update()", true);
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        bindData(hdcode.Value);

    }


    protected void listSection_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listEquip, "select EQ_NAME,IDKEY from ht_eq_eqp_tbl t where t.Section_code = '" + listSection.SelectedValue + "' and t.is_del = '0'", "EQ_NAME", "IDKEY");
        txtCode.Text = "";
    }
}