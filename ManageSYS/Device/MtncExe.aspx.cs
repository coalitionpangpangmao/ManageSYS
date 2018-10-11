using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
public partial class Device_MtncExe : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            txtStart.Text = System.DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd");
            txtStop.Text = System.DateTime.Now.AddDays(45).ToString("yyyy-MM-dd");
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listEq, "select IDKEY,EQ_NAME from ht_eq_eqp_tbl where is_del = '0' and is_valid = '1'", "EQ_NAME", "IDKEY");
            opt.bindDropDownList(listOptor, "select ID,name  from ht_svr_user t ", "name", "ID");
            opt.bindDropDownList(listArea, "select r.section_code,r.section_name from ht_pub_tech_section r  where r.is_del = '0' and r.is_valid = '1'  union select '' as section_code,'' as section_name from dual order by section_code", "section_name", "section_code");
            opt.bindDropDownList(listSection, "select r.section_code,r.section_name from ht_pub_tech_section r  where r.is_del = '0' and r.is_valid = '1'  union select '' as section_code,'' as section_name from dual order by section_code", "section_name", "section_code");
            bindGrid();
        }
    }

    protected DataSet sectionbind()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select r.section_code,r.section_name from ht_pub_tech_section r  where r.is_del = '0' and r.is_valid = '1'  union select '' as section_code,'' as section_name from dual order by section_code");
    }
    protected void bindGrid()
    {
        string query = "select t.mech_area as 区域,t1.eq_name as 设备名称,t.reason as 维保原因,t.content as 维保内容,t.exp_finish_time as 期望完成时间,t.STATUS as 状态,t.remark as 备注 ,t.ID,t.MAIN_ID  from ht_eq_mt_plan_detail t left join Ht_Eq_Eqp_Tbl t1 on t1.idkey = t.equipment_id left join ht_eq_mt_plan t2 on t.main_id = t2.pz_code  where  t.is_del = '0' and t.exp_finish_time between '" + txtStart.Text + "' and '" + txtStop.Text + "'   and t2.FLOW_STATUS = '2' ";
    //    query += " and t.RESPONER = '" + ((MSYS.Data.SysUser)Session["User"]).id + "'";

        if (ckDone.Checked)
            query += " and t.STATUS  > '1' ";
        else
            query += " and t.Status  = '1' ";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                Button btn = (Button)GridView1.Rows[i].FindControl("btnGrid1View");
                if (Convert.ToInt16( mydrv["状态"].ToString()) >= 2)
                {
                    btn.Text = "查看";
                    btn.CssClass = "btnred";
                }
                else
                {
                    btn.Text = "编辑";
                    btn.CssClass = "btn1 auth";
                }
                ((DropDownList)GridView1.Rows[i].FindControl("listGridarea")).SelectedValue = mydrv["区域"].ToString();
                ((DropDownList)GridView1.Rows[i].FindControl("listGrid2Status")).SelectedValue = mydrv["状态"].ToString();

            }

        }

    }//绑定GridView2数据源
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid();
    }
    protected DataSet statusbind()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select ID, Name from ht_inner_eqexe_status order by ID ");
    }

    protected void btnGrid1View_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string ID = GridView1.DataKeys[rowIndex].Values[0].ToString();
        hideMainid.Value = GridView1.DataKeys[rowIndex].Values[1].ToString();
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select * from HT_EQ_MT_PLAN_DETAIL where id = '" + ID + "'");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            DataRow row = data.Tables[0].Rows[0];
            txtCode.Text = ID;
            listEq.SelectedValue = row["EQUIPMENT_ID"].ToString();
            txtOpttime.Text = row["EXE_TIME"].ToString();
            listOptor.SelectedValue = row["RESPONER"].ToString();
            listArea.SelectedValue = row["MECH_AREA"].ToString();
            ckFault.Checked = ("1" == row["IS_FAULT"].ToString());
            txtRecord.Text = row["RECORD"].ToString();
            txtResults.Text = row["RESULTS"].ToString();
            txtCondition.Text = row["CONDITION"].ToString();           
                string ftid = row["FAULT_ID"].ToString();
                fillFalutData(ftid);
                if (btn.Text == "查看")
                {
                    btnSumit.Visible = false;
                    btnSave.Visible = false;
                }
                else
                {
                    btnSumit.Visible = true;
                    btnSave.Visible = true;
                }
          
        }
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", "GridClick();", true);

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
    protected void btnSumit_Click(object sender, EventArgs e)
    {      
        
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            List<string> commandlist = new List<string>();
            commandlist.Add("update HT_EQ_MT_PLAN_DETAIL set STATUS = '2' where id = '" + txtCode.Text + "'");
         
            string[] seg = { "EQUIPMENT_ID", "EXE_TIME", "RESPONER", "MECH_AREA", "IS_EMG", "REASON", "CONTENT", "STATUS" };
            string[] value = { listEq.SelectedValue, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), listOptor.SelectedValue, listArea.SelectedValue, Convert.ToInt16(ckFault.Checked).ToString(), txtResults.Text, txtRecord.Text, "-1" };
            commandlist.Add(opt.InsertDatastr(seg, value, "HT_EQ_RP_PLAN_DETAIL"));
            string log_message = opt.TransactionCommand(commandlist) == "Success" ? "维保上报故障成功" : "维保上报故障失败";
            log_message += "--详情:" + string.Join(",", value);
            InsertTlog(log_message);
            bindGrid();

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        
        string ftID = "";
        List<String> commandlist = new List<string>();
        string log_message;
        if (ckFault.Checked)
        {
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
                string[] seg1 = { "ID", "ERROR_NAME", "EQP_TYPE", "SPECIFIC_LOCATION", "SECTION_CODE", "FAULT_TYPE1", "FAULT_TYPE2", "FAULT_TYPE3", "FAULT_TYPE4", "FAULT_TYPE5", "FAULT_TYPE6", "SCEAN", "ERROR_DESCRIPTION", "FAILURE_CAUSE", "SOLUTION", "CREATE_TIME", "EQUIP_CODE", "EDITOR" };
                string[] value1 = { ftID, txtName.Text, listEqType.SelectedValue, txtLocation.Text, listSection.SelectedValue, listStyle1.SelectedValue, listStyle2.SelectedValue, listStyle3.SelectedValue, listStyle4.SelectedValue, listStyle5.SelectedValue, listStyle6.SelectedValue, txtScean.Text, txtDescpt.Text, txtReason.Text, txtSolution.Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), listEq.SelectedValue, ((MSYS.Data.SysUser)Session["User"]).text };

                log_message = opt.InsertData(seg1, value1, "HT_EQ_FAULT_DB") == "Success" ? "故障信息入库成功" : "故障信息入库失败";
                log_message += ",故障信息ID：" + ftID;
                InsertTlog(log_message);
            }

        }

        string[] seg = { "EQUIPMENT_ID", "EXE_TIME", "RESPONER", "MECH_AREA", "IS_FAULT", "RECORD", "RESULTS", "CONDITION", "FAULT_ID", "EXE_SEGTIME","STATUS" };
        string[] value = { listEq.SelectedValue, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), listOptor.SelectedValue, listArea.SelectedValue, Convert.ToInt16(ckFault.Checked).ToString(), txtRecord.Text, txtResults.Text, txtCondition.Text, ftID, txtSegcount.Text,"2" };
        log_message = opt.UpDateData(seg, value, "HT_EQ_MT_PLAN_DETAIL", "where id = '" + txtCode.Text + "'") == "Success" ? "维修执行成功" : "维修执行失败";
        log_message += ",维修明细ID：" + txtCode.Text;
        InsertTlog(log_message);      
        bindGrid();

        string alter = opt.GetSegValue("select case  when total = done then 1 else 0 end as status from (select  count(distinct t.id) as total,count( distinct t1.id) as done from HT_EQ_MT_PLAN_DETAIL t left join HT_EQ_MT_PLAN_DETAIL t1 on t1.id = t.id and t1.status = '2' and t1.is_del = '0' where t.main_id = '" + hideMainid.Value + "'  and t.is_del = '0')", "status");
        if (alter == "1")
        {
            opt.UpDateOra("update HT_EQ_MT_PLAN set TASK_STATUS = '2' where PZ_CODE = '" + hideMainid.Value + "'");
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
}