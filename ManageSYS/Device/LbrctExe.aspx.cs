using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
public partial class Device_LbrctExe : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            txtStart.Text = System.DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd");
            txtStop.Text = System.DateTime.Now.AddDays(45).ToString("yyyy-MM-dd");
            bindGrid1();
           // btnGrid2Save.CssClass = "btnhide";
        }
    }
    protected void bindGrid1()
    {
        string query = "select t.mt_name as 润滑计划,t1.f_name as 部门,t2.name as 审批状态,t3.name as 执行状态,t.remark as 备注,t.pz_code,t.task_status  from ht_eq_lb_plan t left join ht_svr_org_group t1 on t1.f_code = t.create_dept_id   left join ht_inner_aprv_status t2 on t2.id = t.flow_status left join ht_inner_eqexe_status t3 on t3.id = t.task_status   where t.expired_date between '" + txtStart.Text + "' and '" + txtStop.Text + "'  and t.IS_DEL = '0' and t.FLOW_STATUS = '2'";
        if (ckDone.Checked)
            query += " and t.task_status  > '1' ";
        else
            query += " and t.task_status  = '1' ";
      
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
        hdcode.Value = GridView1.DataKeys[rowIndex].Value.ToString();
        bindGrid2(hdcode.Value);
        if (btn.Text == "查看")
        {
            btnGrid2Save.Visible = false;
        }
        else
        {
            btnGrid2Save.Visible = true;
        }

    }

    protected void bindGrid2(string code)
    {
        string query = "select section as  工段,equipment_id as  设备名称,position as  润滑部位,pointnum as   润滑点数,luboil as   润滑油脂,periodic as   润滑周期 , style as 润滑方式 ,amount as  润滑量 ,EXP_FINISH_TIME as 过期时间, STATUS as 状态,ID from ht_eq_lb_plan_detail  where main_id = '" + code + "' and is_del = '0' and Status  >= '1'";
    //    query += " and RESPONER = '" + ((MSYS.Data.SysUser)Session["User"]).id + "'";
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
                ((DropDownList)row.FindControl("listGridsct")).SelectedValue = mydrv["工段"].ToString();
                ((DropDownList)row.FindControl("listGridEq")).SelectedValue = mydrv["设备名称"].ToString();
                ((DropDownList)row.FindControl("listGrid2Status")).SelectedValue = mydrv["状态"].ToString();
                ((TextBox)row.FindControl("txtGridpos")).Text = mydrv["润滑部位"].ToString();
                ((TextBox)row.FindControl("txtGridnum")).Text = mydrv["润滑点数"].ToString();
                ((TextBox)row.FindControl("txtGridoil")).Text = mydrv["润滑油脂"].ToString();
                ((TextBox)row.FindControl("txtGriPric")).Text = mydrv["润滑周期"].ToString();
                ((TextBox)row.FindControl("txtGridStyle")).Text = mydrv["润滑方式"].ToString();
                ((TextBox)row.FindControl("txtGridamount")).Text = mydrv["润滑量"].ToString();
                ((TextBox)row.FindControl("txtGridExptime")).Text = mydrv["过期时间"].ToString();
            }
        }
    }
    protected DataSet eqbind()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select IDKEY,EQ_NAME from ht_eq_eqp_tbl where is_del = '0' and is_valid = '1'");
    }
    protected DataSet sectionbind()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select section_code,section_name from ht_pub_tech_section where is_del = '0' and is_valid = '1'");
    }
    protected DataSet statusbind()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select ID, Name from ht_inner_eqexe_status order by ID ");
    }
    protected void btnGrid2Save_Click(object sender, EventArgs e)//
    {      

        List<string> commandlist = new List<string>();
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        foreach (GridViewRow row in GridView2.Rows)
        {
            int rowIndex = row.RowIndex;
            string id = GridView2.DataKeys[rowIndex].Value.ToString();
            string status = ((DropDownList)row.FindControl("listGrid2Status")).SelectedValue;
            if ((status == "1" || status == "2") && ((TextBox)row.FindControl("txtGridpos")).Text != "")
            {
                string[] seg = { "section", "equipment_id", "position", "pointnum", "luboil", "periodic", "style", "amount", "CREATE_TIME", "MAIN_ID", "STATUS" };
                string[] value = { ((DropDownList)row.FindControl("listGridsct")).SelectedValue, ((DropDownList)row.FindControl("listGridEq")).SelectedValue, ((TextBox)row.FindControl("txtGridpos")).Text, ((TextBox)row.FindControl("txtGridnum")).Text, ((TextBox)row.FindControl("txtGridoil")).Text, ((TextBox)row.FindControl("txtGriPric")).Text, ((TextBox)row.FindControl("txtGridStyle")).Text, ((TextBox)row.FindControl("txtGridamount")).Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), hdcode.Value, "2" };
                commandlist.Add(opt.UpdateStr(seg, value, "ht_eq_lb_plan_detail", " where ID = '" + id + "'"));
            }
        }
        if (commandlist.Count > 0)
        {
            string log_message = opt.TransactionCommand(commandlist) == "Success" ? "润滑记录录入成功" : "润滑记录录入失败";
            InsertTlog(log_message);
            bindGrid2(txtCode.Value);

            string alter = opt.GetSegValue("select case  when total = done then 1 else 0 end as status from (select  count(distinct t.id) as total,count( distinct t1.id) as done from ht_eq_lb_plan_detail t left join ht_eq_lb_plan_detail t1 on t1.id = t.id and t1.status >= '2' and t1.is_del = '0' where t.main_id = '" + hdcode.Value + "'  and t.is_del = '0')", "status");
            if (alter == "1")
            {
                opt.UpDateOra("update ht_eq_lb_plan set TASK_STATUS = '2' where PZ_CODE = '" + hdcode.Value + "'  and TASK_STATUS = '1'");
                bindGrid1();
            }
        }

       
    }

}