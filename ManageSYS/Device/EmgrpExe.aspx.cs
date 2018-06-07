using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
public partial class Device_EmgrpExe : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           DataBaseOperator opt =new DataBaseOperator();
            opt.bindDropDownList(listEq, "select IDKEY,EQ_NAME from ht_eq_eqp_tbl where is_del = '0' and is_valid = '1'", "EQ_NAME", "IDKEY");
            opt.bindDropDownList(listOptor, "select ID,name  from ht_svr_user t ", "name", "ID");
            
          
        }
 
    }


    protected void btnSumit_Click(object sender, EventArgs e)
    {
        if (ckFault.Checked)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "alert('应急维修请直接处理，无需上报！！');", true);
        }
        else
        {
           DataBaseOperator opt =new DataBaseOperator();
            string[] seg = { "EQUIPMENT_ID", "EXE_TIME", "RESPONER", "MECH_AREA", "IS_EMG", "REASON", "CONTENT","STATUS" };
            string[] value = { listEq.SelectedValue, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), listOptor.SelectedValue, listArea.SelectedValue, Convert.ToInt16(ckFault.Checked).ToString(), txtReasons.Text, txtContent.Text,"-1" };
            opt.InsertData(seg, value, "HT_EQ_RP_PLAN_DETAIL");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
       DataBaseOperator opt =new DataBaseOperator();
        if (ckFault.Checked)
        {
            string[] seg1 = { "ERROR_NAME", "EQP_TYPE", "SPECIFIC_LOCATION", "SECTION_CODE", "FAULT_TYPE1", "FAULT_TYPE2", "FAULT_TYPE3", "FAULT_TYPE4", "FAULT_TYPE5", "FAULT_TYPE6", "SCEAN", "ERROR_DESCRIPTION", "FAILURE_CAUSE", "SOLUTION" };
            string[] value1 = { txtName.Text, listEqType.SelectedValue, txtLocation.Text, listSection.SelectedValue, listStyle1.SelectedValue, listStyle2.SelectedValue, listStyle3.SelectedValue, listStyle4.SelectedValue, listStyle5.SelectedValue, listStyle6.SelectedValue, txtScean.Text, txtDescpt.Text, txtReason.Text, txtSolution.Text };
            opt.InsertData(seg1, value1, "HT_EQ_FAULT_DB");
            string ftID = opt.GetSegValue("select max(ID) as ID  from HT_EQ_FAULT_DB", "ID");
            string status = "2";
            string[] seg = { "EQUIPMENT_ID", "EXE_TIME", "RESPONER", "MECH_AREA", "IS_EMG", "REASON", "CONTENT", "FAULT_ID", "STATUS" };
            string[] value = { listEq.SelectedValue, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), listOptor.SelectedValue, listArea.SelectedValue, Convert.ToInt16(ckFault.Checked).ToString(), txtReasons.Text, txtContent.Text, ftID, status };
            opt.InsertData(seg, value, "HT_EQ_RP_PLAN_DETAIL");
        }
        else
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "alert('非应急维修请上报！！');", true);
        }

       
    }
}