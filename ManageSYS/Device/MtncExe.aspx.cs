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
            bindGrid();
        }
    }
    protected void bindGrid()
    {
        string query = "select t.mech_area as 区域,t1.eq_name as 设备名称,t.reason as 维保原因,t.content as 维保内容,t.exp_finish_time as 期望完成时间,t.STATUS as 状态,t.remark as 备注 ,t.ID  from ht_eq_mt_plan_detail t left join Ht_Eq_Eqp_Tbl t1 on t1.idkey = t.equipment_id  where  t.is_del = '0' and t.exp_finish_time between '" + txtStart.Text + "' and '" + txtStop.Text + "'  and t.Status  >= '1'";

        if (ckDone.Checked)
            query += " and t.STATUS <'2' ";
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
                if ("2" == mydrv["状态"].ToString())
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
        string ID = GridView1.DataKeys[rowIndex].Value.ToString();
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
            if (ckFault.Checked)
            {
                string ftid = row["FAULT_ID"].ToString();
                data = opt.CreateDataSetOra("select * from ht_eq_fault_db where ID = '" + ftid + "'");
                if (data != null && data.Tables[0].Rows.Count > 0)
                {
                    row = data.Tables[0].Rows[0];
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
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "GridClick();", true);

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string ftID = "";
        if (ckFault.Checked)
        {
            string[] seg1 = { "ERROR_NAME", "EQP_TYPE", "SPECIFIC_LOCATION", "SECTION_CODE", "FAULT_TYPE1", "FAULT_TYPE2", "FAULT_TYPE3", "FAULT_TYPE4", "FAULT_TYPE5", "FAULT_TYPE6", "SCEAN", "ERROR_DESCRIPTION", "FAILURE_CAUSE", "SOLUTION" };
            string[] value1 = { txtName.Text, listEqType.SelectedValue, txtLocation.Text, listSection.SelectedValue, listStyle1.SelectedValue, listStyle2.SelectedValue, listStyle3.SelectedValue, listStyle4.SelectedValue, listStyle5.SelectedValue, listStyle6.SelectedValue, txtScean.Text, txtDescpt.Text, txtReason.Text, txtSolution.Text };
            opt.InsertData(seg1, value1, "HT_EQ_FAULT_DB");
            ftID = opt.GetSegValue("select max(ID) as ID  from HT_EQ_FAULT_DB", "ID");

        }

        string[] seg = { "EQUIPMENT_ID", "EXE_TIME", "RESPONER", "MECH_AREA", "IS_FAULT", "RECORD", "RESULTS", "CONDITION", "FAULT_ID", "EXE_SEGTIME","STATUS" };
        string[] value = { listEq.SelectedValue, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), listOptor.SelectedValue, listArea.SelectedValue, Convert.ToInt16(ckFault.Checked).ToString(), txtRecord.Text, txtResults.Text, txtCondition.Text, ftID, txtSegcount.Text,"2" };
        opt.UpDateData(seg, value, "HT_EQ_MT_PLAN_DETAIL", "where id = '" + txtCode.Text + "'");
        bindGrid();
    }
}