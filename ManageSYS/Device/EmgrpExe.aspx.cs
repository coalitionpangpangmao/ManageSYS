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
            opt.bindDropDownList(listOptor, "select s.name,s.id from ht_svr_sys_role t left join ht_svr_sys_menu r on substr(t.f_right,r.f_id,1) = '1' left join ht_svr_user s on s.role = t.f_id where r.f_id = '" + this.RightId + "' union select q.name,q.id from ht_svr_sys_role t left join ht_svr_sys_menu r on substr(t.f_right,r.f_id,1) = '1' left join ht_svr_org_group  s on s.f_role = t.f_id  left join ht_svr_user q on q.levelgroupid = s.f_code  where r.f_id = '" + this.RightId + "'  order by id desc", "name", "ID");
            opt.bindDropDownList(listArea, "select r.section_code,r.section_name from ht_pub_tech_section r  where r.is_del = '0' and r.is_valid = '1'  union select '' as section_code,'' as section_name from dual order by section_code", "section_name", "section_code");
            opt.bindDropDownList(listSection, "select r.section_code,r.section_name from ht_pub_tech_section r  where r.is_del = '0' and r.is_valid = '1'  union select '' as section_code,'' as section_name from dual order by section_code", "section_name", "section_code");
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
            string log_message = opt.InsertData(seg, value, "HT_EQ_RP_PLAN_DETAIL") == "Success" ? "应急维修上报成功" : "应急维修上报失败";
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
            string ftID = "";
            List<String> commandlist = new List<String>();
             string query = "select * from HT_EQ_FAULT_DB where Error_name = '" + txtName.Text + "' and eqp_TYpe = '" + listEqType.SelectedValue + "' and SPECIFIC_LOCATION = '" + txtLocation.Text + "' and SECTION_CODE = '" + listSection.SelectedValue + "' and FAULT_TYPE1 = '" + listStyle1.SelectedValue + "' and FAULT_TYPE2 = '" + listStyle2.SelectedValue + "' and FAULT_TYPE3 = '" + listStyle3.SelectedValue + "' and FAULT_TYPE4 = '" + listStyle4.SelectedValue + "' and FAULT_TYPE5 = '" + listStyle5.SelectedValue + "' and FAULT_TYPE6 = '" + listStyle6.SelectedValue + "' and SCEAN = '" + txtScean.Text + "' and ERROR_DESCRIPTION = '" + txtDescpt.Text + "' and FAILURE_CAUSE = '" + txtReason.Text + "' and SOLUTION = '" + txtSolution.Text + "' and EQUIP_CODE = '" + listEq.SelectedValue + "'";
            query = query.Replace("= ''", "is null");
         
            DataSet data = opt.CreateDataSetOra(query);
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                ftID = data.Tables[0].Rows[0]["ID"].ToString();
            }
            else
            {
                ftID = opt.GetSegValue("select fault_id_seq.nextval from dual", "nextval");

                string[] seg1 = { "ID", "ERROR_NAME", "EQP_TYPE", "SPECIFIC_LOCATION", "SECTION_CODE", "FAULT_TYPE1", "FAULT_TYPE2", "FAULT_TYPE3", "FAULT_TYPE4", "FAULT_TYPE5", "FAULT_TYPE6", "SCEAN", "ERROR_DESCRIPTION", "FAILURE_CAUSE", "SOLUTION", "CREATE_TIME", "EQUIP_CODE","EDITOR" };
                string[] value1 = { ftID, txtName.Text, listEqType.SelectedValue, txtLocation.Text, listSection.SelectedValue, listStyle1.SelectedValue, listStyle2.SelectedValue, listStyle3.SelectedValue, listStyle4.SelectedValue, listStyle5.SelectedValue, listStyle6.SelectedValue, txtScean.Text, txtDescpt.Text, txtReason.Text, txtSolution.Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), listEq.SelectedValue,((MSYS.Data.SysUser)Session["User"]).text};
                commandlist.Add(opt.InsertDatastr(seg1, value1, "HT_EQ_FAULT_DB"));
            }
         
          
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
    protected void listArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listArea.SelectedValue != "")
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listEq, "select IDKEY,EQ_NAME from ht_eq_eqp_tbl where is_del = '0' and is_valid = '1' and SECTION_CODE = '" + listArea.SelectedValue + "'", "EQ_NAME", "IDKEY");
            listSection.SelectedValue = listArea.SelectedValue;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        fillFalutData(txtFtID.Text);
    }
    private void fillFalutData(string id)
    {
        if (id == "")
            SetBlank();
        else
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataSet data = opt.CreateDataSetOra("select * from ht_eq_fault_db where ID = '" + id + "'");
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                DataRow row = data.Tables[0].Rows[0];
                txtFtID.Text = id;
                txtName.Text = row["ERROR_NAME"].ToString();
                listEqType.SelectedValue = row["EQP_TYPE"].ToString();
                txtLocation.Text = row["SPECIFIC_LOCATION"].ToString();
                listSection.SelectedValue = row["SECTION_CODE"].ToString();
                listStyle1.SelectedValue = row["FAULT_TYPE1"].ToString();
                listStyle2.SelectedValue = row["FAULT_TYPE2"].ToString();
                listStyle3.SelectedValue = row["FAULT_TYPE3"].ToString();
                listStyle4.SelectedValue = row["FAULT_TYPE4"].ToString();
                listStyle5.SelectedValue = row["FAULT_TYPE5"].ToString();
                listStyle6.SelectedValue = row["FAULT_TYPE6"].ToString();
                txtScean.Text = row["SCEAN"].ToString();
                txtDescpt.Text = row["ERROR_DESCRIPTION"].ToString();
                txtReason.Text = row["FAILURE_CAUSE"].ToString();
                txtSolution.Text = row["SOLUTION"].ToString();
            }
        }
    }
    private void SetBlank()
    {
        txtFtID.Text = "";
        txtName.Text = "";
        listEqType.SelectedValue = "";
        txtLocation.Text = "";
        listSection.SelectedValue = "";
        listStyle1.SelectedValue = "";
        listStyle2.SelectedValue = "";
        listStyle3.SelectedValue = "";
        listStyle4.SelectedValue = "";
        listStyle5.SelectedValue = "";
        listStyle6.SelectedValue = "";
        txtScean.Text = "";
        txtDescpt.Text = "";
        txtReason.Text = "";
        txtSolution.Text = "";
    }
}