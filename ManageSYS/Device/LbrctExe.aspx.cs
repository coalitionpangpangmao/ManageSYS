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
            txtStop.Text = System.DateTime.Now.AddDays(15).ToString("yyyy-MM-dd");         
            bindGrid1();

        }

    }
    protected void bindGrid1()
    {
        try
        {
            string query = "select t.mt_name as 润滑计划,t1.f_name as 部门,(case t.flow_status when '-1' then '未提交' when '0' then '办理中' when '1' then '未通过' else '己通过' end) as 审批状态,(case t.TASK_STATUS when '0' then '未执行' when '1' then '执行中' when '2' then '己完成' else '己过期' end) as 执行状态,t.remark as 备注,t.pz_code from ht_eq_lb_plan t left join ht_svr_org_group t1 on t1.f_code = t.create_dept_id   where t.expired_date between '" + txtStart.Text + "' and '" + txtStop.Text + "'  and t.IS_DEL = '0'";

           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            GridView1.DataSource = opt.CreateDataSetOra(query); ;
            GridView1.DataBind();
        }
        catch (Exception ee)
        {

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
        bindGrid2( hdcode.Value);
       
    }

    protected void bindGrid2(string code)
    {
        try
        {
            string query = "select section as  工段,equipment_id as  设备名称,position as  润滑部位,pointnum as   润滑点数,luboil as   润滑油脂,periodic as   润滑周期 , style as 润滑方式 ,amount as  润滑量 ,EXP_FINISH_TIME as 过期时间, STATUS as 状态,ID from ht_eq_lb_plan_detail  where main_id = '" + code + "' and is_del = '0' and Status  >= '1'";

           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
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
        catch (Exception ee)
        {

        }

    }//绑定GridView2数据源
    protected DataSet eqbind()
    {
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select IDKEY,EQ_NAME from ht_eq_eqp_tbl where is_del = '0' and is_valid = '1'");
    }
    protected DataSet sectionbind()
    {
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select section_code,section_name from ht_pub_tech_section where is_del = '0' and is_valid = '1'");
    }
   
   


    protected void btnGrid2Save_Click(object sender, EventArgs e)//
    {
        try
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int rowIndex = row.RowIndex;
            string id = GridView2.DataKeys[rowIndex].Value.ToString();

           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();            
           
            string[] seg = { "section", "equipment_id", "position","pointnum","luboil","periodic","style","amount", "CREATE_TIME", "MAIN_ID","STATUS" };
            string[] value = { ((DropDownList)row.FindControl("listGridsct")).SelectedValue, ((DropDownList)row.FindControl("listGridEq")).SelectedValue, ((TextBox)row.FindControl("txtGridpos")).Text,((TextBox)row.FindControl("txtGridnum")).Text, ((TextBox)row.FindControl("txtGridoil")).Text, ((TextBox)row.FindControl("txtGriPric")).Text, ((TextBox)row.FindControl("txtGridStyle")).Text, ((TextBox)row.FindControl("txtGridamount")).Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), hdcode.Value,"2" };
            opt.UpDateData(seg, value, "ht_eq_lb_plan_detail"," where ID = '" + id + "'");
            bindGrid2(hdcode.Value);

        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    
}