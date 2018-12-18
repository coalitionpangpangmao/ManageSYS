using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;
public partial class Product_ShiftChange : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            txtStartDate.Text = System.DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd");
            txtStopDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listShift, "select t.shift_code,t.shift_name  from ht_sys_shift t where t.is_valid = '1' and t.is_del = '0' order by t.shift_code", "shift_name", "shift_code");
            opt.bindDropDownList(listTeam, "select t.team_code,t.team_name  from ht_sys_team t where t.is_valid = '1' and t.is_del = '0' order by t.team_code", "team_name", "team_code");
            opt.bindDropDownList(listProd, "select t.prod_code,t.prod_name  from ht_pub_prod_design t where t.is_valid = '1' and t.is_del = '0' order by t.prod_code", "prod_name", "prod_code");
            opt.bindDropDownList(listOlder, "select s.name,s.id from ht_svr_sys_role t left join ht_svr_sys_menu r on substr(t.f_right,r.f_id,1) = '1' left join ht_svr_user s on s.role = t.f_id where r.f_id = '" + this.RightId + "' union select q.name,q.id from ht_svr_sys_role t left join ht_svr_sys_menu r on substr(t.f_right,r.f_id,1) = '1' left join ht_svr_org_group  s on s.f_role = t.f_id  left join ht_svr_user q on q.levelgroupid = s.f_code  where r.f_id = '" + this.RightId + "'  order by id desc", "NAME", "ID");
            opt.bindDropDownList(listNewer, "select s.name,s.id from ht_svr_sys_role t left join ht_svr_sys_menu r on substr(t.f_right,r.f_id,1) = '1' left join ht_svr_user s on s.role = t.f_id where r.f_id = '" + this.RightId + "' union select q.name,q.id from ht_svr_sys_role t left join ht_svr_sys_menu r on substr(t.f_right,r.f_id,1) = '1' left join ht_svr_org_group  s on s.f_role = t.f_id  left join ht_svr_user q on q.levelgroupid = s.f_code  where r.f_id = '" + this.RightId + "'  order by id desc", "NAME", "ID");
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
        string query = "select g1.work_date as 日期,g2.team_name as 班组,g3.shift_name as 班时,g1.date_begin as 开始时间,g1.date_end as 结束时间,g1.Id,g4.shift_status from ht_prod_schedule g1 left join Ht_Sys_Team g2 on g2.team_code = g1.team_code left join ht_sys_shift g3 on g3.shift_code = g1.shift_code left join ht_prod_shiftchg g4 on g1.id = g4.shift_main_id where g1.work_date between '" + txtStartDate.Text + "' and '" + txtStopDate.Text + "' and g1.is_del = '0' and g1.is_valid = '1' order by g1.date_begin,g1.id";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
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
            string query = "select t.mater_code as 物料名称,t.mater_vl as 数量,t.bz_unit as 单位,t.remark as 备注 from ht_prod_shiftchg_detail t where t.shift_main_id = '" + hdID.Value + "'";
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataSet data = opt.CreateDataSetOra(query);
            GridView2.DataSource = data;
            GridView2.DataBind();

            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
                {
                    DataRowView mydrv = data.Tables[0].DefaultView[i];
                    ((TextBox)GridView2.Rows[i].FindControl("txtUnit")).Text = mydrv["单位"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtAmount")).Text = mydrv["数量"].ToString();
                    ((DropDownList)GridView2.Rows[i].FindControl("listMater")).SelectedValue = mydrv["物料名称"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtDescpt")).Text = mydrv["备注"].ToString();

                }
            }
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
        string query = "select g.work_date as 日期,g.shift_code as 班时,g.team_code as 班组, g3.prod_code as 牌号,g3.planno as 计划号,g4.output_vl,g4.shift_id,g4.succ_id,g4.remark,g4.create_id,g4.devicestatus,g4.qlt_status,g4.scean_status,g.date_end from Ht_Prod_Schedule g   left join ht_prod_report g3 on (g.date_end between g3.starttime and g3.endtime)  or (g.date_end >g3.starttime and g3.endtime is null) left join ht_prod_shiftchg g4 on g4.shift_main_id = g.id   where g.id = '" + id + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            DataRow row = data.Tables[0].Rows[0];
            txtDate.Text = row["日期"].ToString();
            listShift.SelectedValue = row["班时"].ToString();
            listTeam.SelectedValue = row["班组"].ToString();
            listProd.SelectedValue = row["牌号"].ToString();
            txtEditor.Text = row["create_id"].ToString();
            if (txtEditor.Text == "")
                txtEditor.Text = ((MSYS.Data.SysUser)Session["User"]).text;
            txtOutput.Text = row["output_vl"].ToString();
            listOlder.SelectedValue = row["shift_id"].ToString();
            listNewer.SelectedValue = row["succ_id"].ToString();
            txtDevice.Text = row["devicestatus"].ToString();
            txtQlt.Text = row["qlt_status"].ToString();
            txtScean.Text = row["scean_status"].ToString();
            txtRemark.Text = row["remark"].ToString();

            string endtime = row["date_end"].ToString();
            opt.bindDropDownList(listPlanno, "select planno from  ht_prod_report  where  ('" + endtime + "' between starttime and endtime)  or ('" + endtime + "' >starttime and endtime is null) ", "planno", "planno");
            listPlanno.SelectedValue = row["计划号"].ToString();

        }
        bindGrid2();
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "GridClick();", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        // hdID.Value = hdID.Value.Substring(0, hdID.Value.IndexOf(','));
        string[] seg = { "SHIFT_MAIN_ID", "INSPECT_DATE", "SHIFT_CODE", "TEAM_CODE", "PROD_CODE", "PLAN_NO", "OUTPUT_VL", "CREATE_ID", "SHIFT_ID", "SUCC_ID", "DEVICESTATUS", "QLT_STATUS", "SCEAN_STATUS", "REMARK", "OUTPLUS" };
        string[] value = { hdID.Value, txtDate.Text, listShift.SelectedValue, listTeam.SelectedValue, listProd.SelectedValue, listPlanno.SelectedValue, txtOutput.Text, ((MSYS.Data.SysUser)Session["User"]).id, listOlder.SelectedValue, listNewer.SelectedValue, txtDevice.Text, txtQlt.Text, txtScean.Text, txtRemark.Text, txtOutPlus.Text };

        string log_message = opt.InsertData(seg, value, "HT_PROD_SHIFTCHG") == "Success" ? "生产交接班记录成功" : "新生产交接班记录失败";
        log_message += ",交接班ID：" + hdID.Value;
        InsertTlog(log_message);

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportExcel("再造梗丝车间交接班记录", "", "2018-08-21", "", "02", ".xls", "", DateTime.Now, false);

    }
    public DataSet bindpara()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select para_code,para_name  from ht_pub_tech_para where is_del = '0' and is_valid = '1' and para_type like '____1%' order by para_code");
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "select t.mater_code as 物料名称,t.mater_vl as 数量,t.bz_unit as 单位,t.remark as 备注 from ht_prod_shiftchg_detail t where t.shift_main_id = '" + hdID.Value + "'";
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataSet set = opt.CreateDataSetOra(query);
            DataTable data = new DataTable();
            if (set == null)
            {
                data.Columns.Add("物料名称");
                data.Columns.Add("数量");
                data.Columns.Add("单位");
                data.Columns.Add("备注");
            }
            else
                data = set.Tables[0];
            object[] value = { "", 0, "", "" };
            data.Rows.Add(value);
            GridView2.DataSource = data;
            GridView2.DataBind();
            if (data != null && data.Rows.Count > 0)
            {
                for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
                {
                    DataRowView mydrv = data.DefaultView[i];
                    ((TextBox)GridView2.Rows[i].FindControl("txtUnit")).Text = mydrv["单位"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtAmount")).Text = mydrv["数量"].ToString();
                    ((DropDownList)GridView2.Rows[i].FindControl("listMater")).SelectedValue = mydrv["物料名称"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtDescpt")).Text = mydrv["备注"].ToString();

                }
            }
        }
        catch (Exception ee)
        {
            string str = ee.Message;
        }
    }


    protected void btnCkAll_Click(object sender, EventArgs e)
    {
        int ckno = 0;
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            if (((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked)
                ckno++;
        }
        bool check = (ckno < GridView2.Rows.Count);
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            ((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked = check;

        }
    }
    protected void btnDelSel_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked)
                {
                    string mtr_code = ((DropDownList)GridView2.Rows[i].FindControl("listMater")).SelectedValue;
                    string query = "update ht_prod_shiftchg_detail set IS_DEL = '1'  where mater_code = '" + mtr_code + "' and SHIFT_MAIN_ID = '" + hdID.Value + "'";
                    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                    string log_message = opt.UpDateOra(query) == "Success" ? "删除生产交接班明细成功" : "删除生产交接班明细失败";
                    log_message += "--标识:" + ID;
                    InsertTlog(log_message);
                }
            }
            bindGrid2();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void btnGrid2Save_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        string[] seg = { "SHIFT_MAIN_ID", "mater_code", "mater_vl", "bz_unit", "remark" };
        string[] value = { hdID.Value, ((DropDownList)row.FindControl("listMater")).SelectedValue, ((TextBox)row.FindControl("txtAmount")).Text, ((TextBox)row.FindControl("txtUnit")).Text, ((TextBox)row.FindControl("txtDescpt")).Text };

        string log_message = opt.MergeInto(seg, value, 2, "HT_PROD_SHIFTCHG_DETAIL") == "Success" ? "保存生产交接班信息成功" : "保存生产交接班信息失败";
        log_message += "--详情:" + string.Join(",", value);
        InsertTlog(log_message);



        string[] seg1 = { "PLANNO", "PROD_CODE", "SECTION_CODE", "TEAM", "TIME", "PARA_CODE", "CREATOR", "VALUE", };

        string paravalue = ((DropDownList)row.FindControl("listMater")).SelectedValue;
        string paracode = ((DropDownList)row.FindControl("listMater")).SelectedValue;
        if (((TextBox)row.FindControl("txtParavalue")).Text != "")
        {
            string[] value1 = { listPlanno.SelectedValue, listPlanno.SelectedValue.Substring(8, 7), paracode.Substring(0, 5), listTeam.SelectedValue, txtDate.Text, paracode, txtEditor.Text, paravalue };
            opt.MergeInto(seg1, value1, 6, "HT_PROD_REPORT_DETAIL");
        }
    }

}