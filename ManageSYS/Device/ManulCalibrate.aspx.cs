using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
public partial class Device_ManulCalibrate : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            txtStart.Text = System.DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd");
            txtStop.Text = System.DateTime.Now.AddDays(45).ToString("yyyy-MM-dd");
            bindGrid1();

        }

    }
    protected void bindGrid1()
    {

        string query = "select t.mt_name as 校准计划,t.pz_code as 计划号,t.expired_date as 过期时间,t1.name as 申请人,t2.name as 状态,t.remark as 备注,t.pz_code,t.task_status from HT_EQ_MCLBR_PLAN t left join ht_svr_user t1 on t1.id = t.create_id left join ht_inner_eqexe_status t2 on t2.id = t.task_status   where t.expired_date between '" + txtStart.Text + "' and '" + txtStop.Text + "'  and t.IS_DEL = '0' and t.CLBRT_TYPE = '0' and t.FLOW_STATUS = '2'";

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                Button btn = (Button)GridView1.Rows[i].FindControl("btnGridview");
                if (Convert.ToInt16(mydrv["task_status"].ToString()) >= 2)
                {
                    btn.Text = "查看";
                    btn.CssClass = "btnred";
                }
                else
                {
                    btn.Text = "编辑";
                    btn.CssClass = "btn1 auth";
                }

            }
        }



    }//绑定gridview1数据源
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid1();
    }



    protected void btnGridview_Click(object sender, EventArgs e)//查看明细
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        txtCode.Value = GridView1.DataKeys[rowIndex].Value.ToString();
        bindGrid2(txtCode.Value);
     
        if (btn.Text == "查看")
            btnGrid2Save.Visible = false;
        else
            btnGrid2Save.Visible = true;
    }

    /// <summary>
    /// /tab2
    /// </summary>
    protected DataSet sectionbind()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select section_code,section_name from ht_pub_tech_section where is_del = '0' and is_valid = '1' union select '' as section_code,'' as section_name from dual order by section_code");
    }
    protected DataSet statusbind()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select ID, Name from ht_inner_eqexe_status order by ID ");
    }
    protected void bindGrid2(string code)
    {

        string query = "select t.section as 工段,t.equipment_id as 设备名称,t.point as 数据点,t.OLDVALUE as 原值,t.POINTVALUE as 校准值,t.SAMPLE_TIME as 校准时间,t.STATUS as 状态,t.remark as 备注 ,t.ID  from HT_EQ_MCLBR_PLAN_detail  t where t.main_id = '" + code + "' and t.is_del = '0'";
     //   query += " and t.RESPONER = '" + ((MSYS.Data.SysUser)Session["User"]).id + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView2.DataSource = data;
        GridView2.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {

            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                GridViewRow row = GridView2.Rows[i];
                DropDownList list1 = (DropDownList)row.FindControl("listGridsct");
                DropDownList list2 = (DropDownList)row.FindControl("listGridEq");
                DropDownList list3 = (DropDownList)row.FindControl("listGridPoint");
                list1.SelectedValue = mydrv["工段"].ToString();
                opt.bindDropDownList(list2, "select IDKEY,EQ_NAME  from ht_eq_eqp_tbl where section_code = '" + list1.SelectedValue + "'  order by idkey", "EQ_NAME", "IDKEY");
                list2.SelectedValue = mydrv["设备名称"].ToString();
                opt.bindDropDownList(list3, "select para_code,para_name from ht_pub_tech_para where equip_code = '" + list2.SelectedValue + "' order by para_code ", "para_name", "para_code");
                list3.SelectedValue = mydrv["数据点"].ToString();
                ((TextBox)row.FindControl("txtGridOldvalue")).Text = mydrv["原值"].ToString();
                ((TextBox)row.FindControl("txtGridNewvalue")).Text = mydrv["校准值"].ToString();
                ((TextBox)row.FindControl("txtGridClbrtime")).Text = mydrv["校准时间"].ToString();
                ((DropDownList)row.FindControl("listGrid2Status")).SelectedValue = mydrv["状态"].ToString();
                ((TextBox)row.FindControl("txtGridremark")).Text = mydrv["备注"].ToString();
                if (Convert.ToInt16( mydrv["状态"].ToString()) >= 3)
                {
                    ((TextBox)row.FindControl("txtGridOldvalue")).Enabled = false;
                    ((TextBox)row.FindControl("txtGridNewvalue")).Enabled = false;
                    ((TextBox)row.FindControl("txtGridClbrtime")).Enabled = false;
                    ((TextBox)row.FindControl("txtGridremark")).Enabled = false;
                }

            }
        }

    }//绑定GridView2数据源




    protected void btnGrid2Save_Click(object sender, EventArgs e)//
    {
        List<string> commandlist = new List<string>();
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        foreach (GridViewRow row in GridView2.Rows)
        {           
            int rowIndex = row.RowIndex;
            string id = GridView2.DataKeys[rowIndex].Value.ToString();
            string status = ((DropDownList)row.FindControl("listGrid2Status")).SelectedValue;
            if ((status == "1" || status == "2") && ((TextBox)row.FindControl("txtGridNewvalue")).Text != "")
            {
                string[] seg = { "ID", "OLDVALUE", "POINTVALUE", "SAMPLE_TIME", "remark", "STATUS" };

                string[] value = { id, ((TextBox)row.FindControl("txtGridOldvalue")).Text, ((TextBox)row.FindControl("txtGridNewvalue")).Text, ((TextBox)row.FindControl("txtGridClbrtime")).Text, ((TextBox)row.FindControl("txtGridremark")).Text, "2" };
                commandlist.Add(opt.getMergeStr(seg, value, 1, "HT_EQ_MCLBR_PLAN_detail"));
            }            
        }
        if (commandlist.Count > 0)
        {
            string log_message = opt.TransactionCommand(commandlist) == "Success" ? "人工校准录入成功" : "人工校准录入失败";
            InsertTlog(log_message);
            bindGrid2(txtCode.Value);

            string alter = opt.GetSegValue("select case  when total = done then 1 else 0 end as status from (select  count(distinct t.id) as total,count( distinct t1.id) as done from HT_EQ_MCLBR_PLAN_detail t left join HT_EQ_MCLBR_PLAN_detail t1 on t1.id = t.id and t1.status >= '2' and t1.is_del = '0' where t.main_id = '" + txtCode.Value + "'  and t.is_del = '0')", "status");
            if (alter == "1")
            {
                opt.UpDateOra("update HT_EQ_MCLBR_PLAN set TASK_STATUS = '2' where PZ_CODE = '" + txtCode.Value + "'  and TASK_STATUS = '1'");
                bindGrid1();
            }
        }

    }


}