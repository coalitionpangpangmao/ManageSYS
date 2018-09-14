using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
public partial class Device_EmgrpExe : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listEq, "select IDKEY,EQ_NAME from ht_eq_eqp_tbl where is_del = '0' and is_valid = '1'", "EQ_NAME", "IDKEY");
            opt.bindDropDownList(listOptor, "select ID,name  from ht_svr_user t ", "name", "ID");
            MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
            txtOpttime.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            listOptor.SelectedValue = user.id;          
          
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
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            string[] seg = { "EQUIPMENT_ID", "EXE_TIME", "RESPONER", "MECH_AREA", "IS_EMG", "REASON", "CONTENT","STATUS" };
            string[] value = { listEq.SelectedValue, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), listOptor.SelectedValue, listArea.SelectedValue, Convert.ToInt16(ckFault.Checked).ToString(), txtReasons.Text, txtContent.Text,"-1" };
            string log_message = opt.InsertData(seg, value, "HT_EQ_RP_PLAN_DETAIL") == "Success" ? "新建维修计划明细成功" : "新建维修计划明细失败";
            log_message += "--详情:" + string.Join(",", value);
            InsertTlog(log_message);
        }
    }
    protected DataSet statusbind()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select ID, Name from ht_inner_eqexe_status order by ID ");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        if (ckFault.Checked)
        {
            List<String> commandlist = new List<String>();
            string ftID = opt.GetSegValue("select fault_id_seq.nextval from dual", "nextval");
            string[] seg1 = { "ID","ERROR_NAME", "EQP_TYPE", "SPECIFIC_LOCATION", "SECTION_CODE", "FAULT_TYPE1", "FAULT_TYPE2", "FAULT_TYPE3", "FAULT_TYPE4", "FAULT_TYPE5", "FAULT_TYPE6", "SCEAN", "ERROR_DESCRIPTION", "FAILURE_CAUSE", "SOLUTION" };
            string[] value1 = {ftID, txtName.Text, listEqType.SelectedValue, txtLocation.Text, listSection.SelectedValue, listStyle1.SelectedValue, listStyle2.SelectedValue, listStyle3.SelectedValue, listStyle4.SelectedValue, listStyle5.SelectedValue, listStyle6.SelectedValue, txtScean.Text, txtDescpt.Text, txtReason.Text, txtSolution.Text };
            commandlist.Add(opt.InsertDatastr(seg1, value1, "HT_EQ_FAULT_DB"));
         
          
            string status = "2";
            string[] seg = { "EQUIPMENT_ID", "EXE_TIME", "RESPONER", "MECH_AREA", "IS_EMG", "REASON", "CONTENT", "FAULT_ID", "STATUS" };
            string[] value = { listEq.SelectedValue, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), listOptor.SelectedValue, listArea.SelectedValue, Convert.ToInt16(ckFault.Checked).ToString(), txtReasons.Text, txtContent.Text, ftID, status };
            commandlist.Add(opt.InsertDatastr(seg, value, "HT_EQ_RP_PLAN_DETAIL"));

            string log_message = opt.TransactionCommand(commandlist) == "Success" ? "应急维修记录成功" : "应急维修记录失败";
            log_message += "--详情:" + string.Join(",", value);
            InsertTlog(log_message);
       
        }
        else
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "alert('非应急维修请上报！！');", true);
        }

       
    }
}