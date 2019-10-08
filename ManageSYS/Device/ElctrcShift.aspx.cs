using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Drawing;
public partial class Device_ElctrcShift : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            txtStartDate.Text = System.DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            txtStopDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listShift, "select t.shift_code,t.shift_name  from ht_sys_shift t where t.is_valid = '1' and t.is_del = '0' order by t.shift_code", "shift_name", "shift_code");
            opt.bindDropDownList(listTeam, "select t.team_code,t.team_name  from ht_sys_team t where t.is_valid = '1' and t.is_del = '0' order by t.team_code", "team_name", "team_code");
            opt.bindDropDownList(listolder, "select s.name,s.id from ht_svr_sys_role t left join ht_svr_sys_menu r on substr(t.f_right,r.f_id,1) = '1' left join ht_svr_user s on s.role = t.f_id where r.f_id = '" + this.RightId + "' union select q.name,q.id from ht_svr_sys_role t left join ht_svr_sys_menu r on substr(t.f_right,r.f_id,1) = '1' left join ht_svr_org_group  s on s.f_role = t.f_id  left join ht_svr_user q on q.levelgroupid = s.f_code  where r.f_id = '" + this.RightId + "'  order by id desc", "name", "ID");
            opt.bindDropDownList(listnewer, "select s.name,s.id from ht_svr_sys_role t left join ht_svr_sys_menu r on substr(t.f_right,r.f_id,1) = '1' left join ht_svr_user s on s.role = t.f_id where r.f_id = '" + this.RightId + "' union select q.name,q.id from ht_svr_sys_role t left join ht_svr_sys_menu r on substr(t.f_right,r.f_id,1) = '1' left join ht_svr_org_group  s on s.f_role = t.f_id  left join ht_svr_user q on q.levelgroupid = s.f_code  where r.f_id = '" + this.RightId + "'  order by id desc", "name", "ID");
            if (((MSYS.Data.SysUser)Session["user"]).UserRole == "生产处电气维修")
            {
                rdElec.Enabled = false;
                rdMchnc.Enabled = false;
                rdElec.Checked = true;
                rdMchnc.Checked = false;
            }
            else if (((MSYS.Data.SysUser)Session["user"]).UserRole == "生产处机械维修")
            {
                rdElec.Enabled = false;
                rdMchnc.Enabled = false;
                rdElec.Checked = false;
                rdMchnc.Checked = true;
            }
            else
            {
                rdElec.Enabled = true;
                rdMchnc.Enabled = true;
            }
            bindGrid1();
         
           
        }
 
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView theGrid = sender as GridView;
        int newPageIndex = 0;
        if (e.NewPageIndex == -3)
        {
            //点击跳转按钮
            TextBox txtNewPageIndex = null;

            //GridView较DataGrid提供了更多的API，获取分页块可以使用BottomPagerRow 或者TopPagerRow，当然还增加了HeaderRow和FooterRow
            GridViewRow pagerRow = theGrid.BottomPagerRow;

            if (pagerRow != null)
            {
                //得到text控件
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;
            }
            if (txtNewPageIndex != null)
            {
                //得到索引
                newPageIndex = int.Parse(txtNewPageIndex.Text) - 1;
            }
        }
        else
        {
            //点击了其他的按钮
            newPageIndex = e.NewPageIndex;
        }
        //防止新索引溢出
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;
        //得到新的值
        theGrid.PageIndex = newPageIndex;
        //重新绑定

        bindGrid1();
    }
    protected void bindGrid1()
    {
      
            string query = "select g1.work_date as 日期,g2.team_name as 班组,g3.shift_name as 班时,g1.date_begin as 开始时间,g1.date_end as 结束时间,g1.Id,g4.shift_status from ht_prod_schedule g1 left join Ht_Sys_Team g2 on g2.team_code = g1.team_code left join ht_sys_shift g3 on g3.shift_code = g1.shift_code left join HT_EQ_MT_SHIFT g4 on g1.id = g4.id";
            if (rdElec.Checked)
                query += " and g4.MAINTENANCE_TYPE = '0'";
            if (rdMchnc.Checked)
                query += " and g4.MAINTENANCE_TYPE = '1'";
        query += " where g1.work_date between '" + txtStartDate.Text + "' and '" + txtStopDate.Text + "' and g1.is_del = '0' and g1.is_valid = '1' order by g1.work_date DESC,g1.id ";
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            DataSet data = opt.CreateDataSetOra(query);
            GridView1.DataSource = data;
            GridView1.DataBind();
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                for (int i = GridView1.PageSize * GridView1.PageIndex; i < GridView1.PageSize * (GridView1.PageIndex + 1) && i < data.Tables[0].Rows.Count; i++)
                {
                    int j = i - GridView1.PageSize * GridView1.PageIndex;
                    DataRowView mydrv = data.Tables[0].DefaultView[i];
                    GridViewRow row = GridView1.Rows[j];
                    Button btn = (Button)row.FindControl("btnGrid1Edit");
                    if ("1" == mydrv["shift_status"].ToString())
                    {
                        btn.Text = "查看";
                        btn.CssClass = "btnred";
                    }
                    else
                    {
                        btn.Text = "填写";
                        btn.CssClass = "btn1 auth";
                    }

                }
            }
       
    }
    protected void bindGrid2()
    {
        try
        {
            string query = "select distinct t.exe_time as 维修开始时间 ,t.exe_segtime as 维修时长, t.NTRODUCER as 提出人,r.eqp_type as 故障类型,s.eq_name  as 设备名称,s.section_code as 工段 ,r.error_name as 故障,r.specific_location as 故障具体位置,r.error_description as 设备故障描述,r.failure_cause  as 设备故障原因 ,r.solution  as 故障解决方案,t.RESPONER  as  实施人,t.status  as 处理状态,t.verifior  as 验证人,t.id from Ht_Eq_Rp_Plan_Detail t left join ht_eq_fault_db r on r.id = t.fault_id left join ht_eq_eqp_tbl s on s.idkey = t.equipment_id where t.exe_time between '" + txtBtime.Text + "' and '" + txtEtime.Text + "' and  t.is_del = '0' and r.eqp_type = '" + listtype.SelectedValue + "' union select distinct t.exe_time as 维修开始时间 ,t.exe_segtime as 维修时长, t.NTRODUCER as 提出人,r.eqp_type as 故障类型,s.eq_name  as 设备名称,s.section_code as 工段 ,r.error_name as 故障,r.specific_location as 故障具体位置,r.error_description as 设备故障描述,r.failure_cause  as 设备故障原因 ,r.solution  as 故障解决方案,t.RESPONER  as  实施人,t.status  as 处理状态,t.verifior  as 验证人,t.id from ht_eq_mt_plan_detail t left join ht_eq_fault_db r on r.id = t.fault_id left join ht_eq_eqp_tbl s on s.idkey = t.equipment_id where t.exe_time between '" + txtBtime.Text + "' and '" + txtEtime.Text + "' and  t.is_del = '0' and r.eqp_type = '" + listtype.SelectedValue + "' and t.is_fault = '1'";
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            DataSet data = opt.CreateDataSetOra(query);
            GridView2.DataSource = data;
            GridView2.DataBind();
           
            
          
        }
        catch (Exception ee)
        {
            string str = ee.Message;
        }
    }
 
  
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid1();
    }
    protected void btnGrid1Edit_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string id = GridView1.DataKeys[rowIndex].Value.ToString();
        hdID.Value = id;
        string query = "select g.work_date as 日期,g.shift_code as 班时,g.team_code as 班组,g1.create_id as 交班人,g1.modify_id as 接班人,g1.remark as 备注,g.date_begin as 开始时间,g.date_end as 结束时间,MAINTENANCE_TYPE from Ht_Prod_Schedule g   left join HT_EQ_MT_SHIFT g1 on g1.ID = g.id   where g.id = '" + id + "'";
         if (rdElec.Checked)
                query += " and g1.MAINTENANCE_TYPE = '0'";
            if (rdMchnc.Checked)
                query += " and g1.MAINTENANCE_TYPE = '1'";
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            DataRow row = data.Tables[0].Rows[0];
            txtDate.Text = row["日期"].ToString();
            listShift.SelectedValue = row["班时"].ToString();
            listTeam.SelectedValue = row["班组"].ToString();

            listolder.SelectedValue = row["交班人"].ToString();
            listnewer.SelectedValue = row["接班人"].ToString();
            txtRemark.Text = row["备注"].ToString();

            txtBtime.Text = row["开始时间"].ToString();
            txtEtime.Text = row["结束时间"].ToString();
            listtype.SelectedValue = row["MAINTENANCE_TYPE"].ToString();

        }
        else
        {
            query = "select g.work_date as 日期,g.shift_code as 班时,g.team_code as 班组,g.date_begin as 开始时间,g.date_end as 结束时间 from Ht_Prod_Schedule g   where g.id = '" + id + "'";
            data = opt.CreateDataSetOra(query);
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                DataRow row = data.Tables[0].Rows[0];
                txtDate.Text = row["日期"].ToString();
                listShift.SelectedValue = row["班时"].ToString();
                listTeam.SelectedValue = row["班组"].ToString();           

                txtBtime.Text = row["开始时间"].ToString();
                txtEtime.Text = row["结束时间"].ToString();
                listtype.SelectedValue = (rdElec.Checked ? "0" : "1");
                listolder.SelectedValue = "";
                listnewer.SelectedValue = "";
                txtRemark.Text = "";
            }
        }
        bindGrid2();
     
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
       string[] seg = { "ID", "MAINTENANCE_TYPE", "WORKSHOP_CODE", " SHIFT_CODE", " TEAM_CODE", " HANDOVER_DATE", " B_TIME", " E_TIME", " CREATE_ID", " MODIFY_ID", " RECORD_TIME", " REMARK" };
       string[] value = { hdID.Value, listtype.SelectedValue, listApt.SelectedValue, listShift.SelectedValue, listTeam.SelectedValue, txtDate.Text, txtBtime.Text, txtEtime.Text, listolder.SelectedValue, listnewer.SelectedValue, System.DateTime.Now.ToString("yyyy-MM-dd"), txtRemark.Text };
        List<string> commandlist = new List<string>();
        commandlist.Add(opt.getMergeStr(seg, value,2, "HT_EQ_MT_SHIFT"));       
        if (GridView2.Rows.Count > 0)
        {
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                string key = GridView2.DataKeys[i].Value.ToString();

                string[] seg1 = {  "SHIFT_MAIN_ID","MAINTENANCE_TYPE","MANAGE_STATUS","BUZ_ID" };
                string[] value1 = { hdID.Value, listtype.SelectedValue, "1", key };
                commandlist.Add(opt.getMergeStr(seg1, value1,2, "HT_EQ_MT_SHIFT_DETAIL")); 
            }
        }
        string log_message = opt.TransactionCommand(commandlist) == "Success" ? "新增机电交接班记录成功" : "新增机电交接班记录失败";
        log_message += "--详情:" + string.Join(",", value);
        InsertTlog(log_message);
      
    }





   
}