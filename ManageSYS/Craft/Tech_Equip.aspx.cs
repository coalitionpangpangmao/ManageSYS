using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Device_EquipmentInfo : MSYS.Web.BasePage
{
   
    protected string tvHtml2;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listSection, "select section_code,section_name from ht_pub_tech_section where is_del = '0' and is_valid = '1' order by section_code", "section_name", "section_code");
           
        }
    }

  
   
   

    protected void btnUpdate_Click(object sender, EventArgs e)
    {        
            bindData(hdcode.Value);
    }   
  
    protected void bindData(string eqcode)
    {
        string query = "select * from HT_EQ_EQP_TBL where  IDKEY = '" + eqcode + "'";
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            DataRow row = data.Tables[0].Rows[0];                 
            txtDscpt.Text = row["REMARK"].ToString();
            listSection.SelectedValue = row["SECTION_CODE"].ToString();
            txtCode.Text = eqcode;
            txtName.Text = row["EQ_NAME"].ToString();
            ckCtrl.Checked = ("1" == row["IS_CTRL"].ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        if (listSection.SelectedValue == "")
            ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "message", "alert('请选择设备所属工段及分类')", true);
        else
        {
            txtCode.Text = listSection.SelectedValue + opt.GetSegValue("select nvl(max(substr(idkey,6,3)),0)+1 as code from ht_eq_eqp_tbl where SECTION_CODE = '" + listSection.SelectedValue + "'", "CODE").PadLeft(3, '0'); 
            txtName.Text = "";   
           
        }
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        if(txtCode.Text.Length == 8 && txtCode.Text.Substring(0,5) == listSection.SelectedValue)
        {
            string[] seg = { "IDKEY", "CLS_CODE", "EQ_NAME", "SECTION_CODE", "REMARK", "CREATOR", "CREATE_TIME" ,"IS_CTRL"};
            string[] value = { txtCode.Text,listSort.SelectedValue, txtName.Text,listSection.SelectedValue, txtDscpt.Text, ((MSYS.Data.SysUser)Session["user"]).id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),Convert.ToInt16(ckCtrl.Checked).ToString() };
            opt.MergeInto(seg, value, 1, "HT_EQ_EQP_TBL");
            ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "updatetree", "  window.parent.update()", true);
        }
        else
            ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "message", "alert('请确认设备所属工艺段是否正确')", true);
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        List<String> commandlist = new List<String>();
        commandlist.Add( "update HT_EQ_EQP_TBL set IS_DEL = '1' where IDKEY = '" + txtCode.Text + "'");
        commandlist.Add("update HT_PUB_TECH_PARA set IS_DEL = '1' where EQUIP_CODE =  '" + txtCode.Text + "'");
        opt.TransactionCommand(commandlist);
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "updatetree", "  window.parent.update()", true);
    }

  
}