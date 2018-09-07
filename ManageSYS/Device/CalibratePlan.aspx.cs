using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
public partial class Device_CalibratePlan : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            txtStart.Text = System.DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd");
            txtStop.Text = System.DateTime.Now.AddDays(45).ToString("yyyy-MM-dd");
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listEditor, "select ID,name  from ht_svr_user t where is_del ='0'", "name", "ID");
            opt.bindDropDownList(listApt, "select f_code,f_name  from ht_svr_org_group ", "f_name", "f_code");
            opt.bindDropDownList(listModel, "select pz_code,mt_name from HT_EQ_MCLBR_PLAN where is_model = '1' and is_del = '0'", "mt_name", "pz_code");
            opt.bindDropDownList(listdspcth, "select ID,name  from ht_svr_user ", "name", "ID");
            bindGrid1();

        }

    }
    protected void bindGrid1()
    {

        string query = "select t.mt_name as 校准计划, t2.name as 审批状态,t3.name as 执行状态,t.remark as 备注,t.pz_code from HT_EQ_MCLBR_PLAN t left join ht_svr_org_group t1 on t1.f_code = t.create_dept_id  left join ht_inner_aprv_status t2 on t2.id = t.flow_status left join ht_inner_eqexe_status t3 on t3.id = t.task_status where t.expired_date between '" + txtStart.Text + "' and '" + txtStop.Text + "'  and t.IS_DEL = '0'";

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            foreach (DataRow row in data.Tables[0].Rows)
            {
                ((Label)GridView1.Rows[i].FindControl("labAprv")).Text = row["审批状态"].ToString();

                ((Label)GridView1.Rows[i++].FindControl("labexe")).Text = row["执行状态"].ToString();
            }
        }


    }//绑定gridview1数据源
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid1();
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
    protected void btnGridDel_Click(object sender, EventArgs e)//删除选中记录
    {
        try
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked)
                {
                    string order_sn = GridView1.DataKeys[i].Value.ToString();
                    string query = "update HT_EQ_MCLBR_PLAN set IS_DEL = '1'  where PZ_CODE = '" + order_sn + "'";
                    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                    string log_message = opt.UpDateOra(query) == "Success" ? "删除校准计划成功" : "删除校准计划失败";
                    log_message += "标识:" + order_sn;
                    InsertTlog(log_message);
                }
            }
            bindGrid1();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnGridNew_Click(object sender, EventArgs e)
    {
        setBlank();
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        txtCode.Text = "CL" + System.DateTime.Now.ToString("yyyyMMdd") + (Convert.ToInt16(opt.GetSegValue("select nvl( max(substr(pz_code,11,3)),0) as ordernum from HT_EQ_MCLBR_PLAN where substr(pz_code,1,10) ='CL" + System.DateTime.Now.ToString("yyyyMMdd") + "'", "ordernum")) + 1).ToString().PadLeft(3, '0');
        MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
        listApt.SelectedValue = user.OwningBusinessUnitId;
        listEditor.SelectedValue = user.id;
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "GridClick();", true);
        // this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "", "<script>GridClick();</script>", true);
    }

    protected void btnGridIssue_Click(object sender, EventArgs e)//查看审批流程
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string ID = GridView1.DataKeys[rowIndex].Value.ToString();
        string query = "select pos as 顺序号, workitemid as 审批环节,username as 负责人,comments as 意见,opiniontime 审批时间,(case status when '0' then '未审批'  when '1' then '未通过' else '己通过' end)  as 审批状态  from ht_pub_aprv_opinion r left join ht_pub_aprv_flowinfo s on r.gongwen_id = s.id where s.busin_id  = '" + ID + "' order by pos";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        GridView3.DataSource = opt.CreateDataSetOra(query);
        GridView3.DataBind();
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "Aprvlist();", true);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)//提交审批
    {
        try
        {
            Button btn = (Button)sender;
            int index = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号                 
            string id = GridView1.DataKeys[index].Value.ToString();
            /*启动审批TB_ZT标题,TBR_ID填报人id,TBR_NAME填报人name,TB_BM_ID填报部门id,TB_BM_NAME填报部门name,TB_DATE申请时间创建日期,MODULENAME审批类型编码,URL 单独登录url,BUSIN_ID业务数据id*/
            string[] subvalue = { GridView1.Rows[index].Cells[1].Text, "14", id, Page.Request.UserHostName.ToString() };
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string log_message = MSYS.Common.AprvFlow.createApproval(subvalue) ? "提交审批成功," : "提交审批失败，";
            log_message += "业务数据ID：" + id;
            InsertTlog(log_message);


        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void btnGridview_Click(object sender, EventArgs e)//查看明细
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        txtCode.Text = GridView1.DataKeys[rowIndex].Value.ToString();

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select * from HT_EQ_MCLBR_PLAN  where PZ_CODE =  '" + txtCode.Text + "'");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            DataRow row = data.Tables[0].Rows[0];
            txtName.Text = row["MT_NAME"].ToString();
            listEditor.SelectedValue = row["CREATE_ID"].ToString();
            listApt.SelectedValue = row["CREATE_DEPT_ID"].ToString();
            txtExptime.Text = row["EXPIRED_DATE"].ToString();
            txtdscrpt.Text = row["REMARK"].ToString();
            ckModel.Checked = ("1" == row["IS_MODEL"].ToString());
            setType(row["CLBRT_TYPE"].ToString());
            bindGrid2(txtCode.Text);
        }
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", "GridClick();", true);
    }

    /// <summary>
    /// /tab2
    /// </summary>
    protected void setBlank()
    {
        txtName.Text = "";
        txtCode.Text = "";
        listEditor.SelectedValue = "";
        listApt.SelectedValue = "";
        txtExptime.Text = "";
        txtdscrpt.Text = "";
        ckModel.Checked = false;
    }
    protected DataSet statusbind()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select ID, Name from ht_inner_eqexe_status order by ID ");
    }
    protected DataSet sectionbind()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select section_code,section_name from ht_pub_tech_section where is_del = '0' and is_valid = '1' union select '' as section_code,'' as section_name from dual order by section_code");
    }

    protected void listGridEq_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList list = (DropDownList)sender;
        GridViewRow row = (GridViewRow)list.NamingContainer;
        int rowindex = row.RowIndex;
        DropDownList list1 = (DropDownList)row.FindControl("listGridPoint");
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(list1, "select para_code,para_name from ht_pub_tech_para where equip_code = '" + list.SelectedValue + "' order by para_code ", "para_name", "para_code");
    }
    protected void listGridsct_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList list = (DropDownList)sender;
        GridViewRow row = (GridViewRow)list.NamingContainer;
        int rowindex = row.RowIndex;
        DropDownList list1 = (DropDownList)row.FindControl("listGridEq");
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(list1, "select IDKEY,EQ_NAME  from ht_eq_eqp_tbl where section_code = '" + list.SelectedValue + "'  order by idkey", "EQ_NAME", "IDKEY");
    }

    protected DataSet eqbind()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select IDKEY,EQ_NAME from ht_eq_eqp_tbl where is_del = '0' and is_valid = '1' union select '' as idkey,'' as eq_name from dual order by idkey");
    }

    protected void btnCkAll_Click(object sender, EventArgs e)//全选
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
    protected void btnDelSel_Click(object sender, EventArgs e)//删除选中记录
    {
        try
        {
            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked)
                {
                    string ID = GridView2.DataKeys[i].Value.ToString();
                    string query = "update HT_EQ_MCLBR_PLAN_detail set IS_DEL = '1'  where ID = '" + ID + "'";
                    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                    string log_message = opt.UpDateOra(query) == "Success" ? "删除校准计划明细成功" : "删除校准计划明细失败";
                    log_message += "标识:" + ID;
                    InsertTlog(log_message);
                }
            }
            bindGrid2(txtCode.Text);
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void bindGrid2Ctrl(DataTable data)
    {
        GridView2.DataSource = data;
        GridView2.DataBind();
        if (data != null && data.Rows.Count > 0)
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                DataRowView mydrv = data.DefaultView[i];
                GridViewRow row = GridView2.Rows[i];
                DropDownList list1 = (DropDownList)row.FindControl("listGridsct");
                DropDownList list2 = (DropDownList)row.FindControl("listGridEq");
                DropDownList list3 = (DropDownList)row.FindControl("listGridPoint");
                list1.SelectedValue = mydrv["工段"].ToString();
                opt.bindDropDownList(list2, "select IDKEY,EQ_NAME  from ht_eq_eqp_tbl where section_code = '" + list1.SelectedValue + "'  order by idkey", "EQ_NAME", "IDKEY");
                list2.SelectedValue = mydrv["设备名称"].ToString();
                opt.bindDropDownList(list3, "select para_code,para_name from ht_pub_tech_para where equip_code = '" + list2.SelectedValue + "' order by para_code ", "para_name", "para_code");
                list3.SelectedValue = mydrv["数据点"].ToString();

                ((TextBox)row.FindControl("txtGridExptime")).Text = mydrv["期望完成时间"].ToString();
                ((DropDownList)row.FindControl("listGrid2Status")).SelectedValue = mydrv["状态"].ToString();
                ((TextBox)row.FindControl("txtGridremark")).Text = mydrv["备注"].ToString();

            }
        }

    }
    protected void bindGrid2(string code)
    {

        string query = "select t.section as 工段,t.equipment_id as 设备名称,t.point as 数据点,t.exp_finish_time as 期望完成时间,t.STATUS as 状态,t.remark as 备注 ,t.ID  from HT_EQ_MCLBR_PLAN_detail  t where t.main_id = '" + code + "' and t.is_del = '0'";

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataTable data = opt.CreateDataSetOra(query).Tables[0];
        bindGrid2Ctrl(data);

    }//绑定GridView2数据源
    protected void btnAdd_Click(object sender, EventArgs e)//
    {

        string query = "select t.section as 工段,t.equipment_id as 设备名称,t.point as 数据点,t.exp_finish_time as 期望完成时间,t.STATUS as 状态,t.remark as 备注 ,t.ID  from HT_EQ_MCLBR_PLAN_detail t  where t.main_id = '" + txtCode.Text + "' and t.is_del = '0'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet set = opt.CreateDataSetOra(query);
        DataTable data = new DataTable();
        if (set == null)
        {
            data.Columns.Add("工段");
            data.Columns.Add("设备名称");
            data.Columns.Add("数据点");
            data.Columns.Add("期望完成时间");
            data.Columns.Add("状态");
            data.Columns.Add("备注");
            data.Columns.Add("ID");
        }
        else
            data = set.Tables[0];
        object[] value = { "", "", "", "", "", "", 0 };
        data.Rows.Add(value);
        bindGrid2Ctrl(data);
    }

    protected void btnDone_Click(object sender, EventArgs e)//
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        //if (opt.GetSegValue("select flow_status from ht_eq_mclbr_plan where pz_code = '" + txtCode.Text + "'", "flow_status") != "2")
        //    return;
        List<string> commandlist = new List<string>();
        for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
        {
            if (((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked)
            {
                string ID = GridView2.DataKeys[i].Value.ToString();
                commandlist.Add("update HT_EQ_MCLBR_PLAN_detail set STATUS = '5'  where ID = '" + ID + "' and status = '4'");               
            }
        }
        string log_message = opt.TransactionCommand(commandlist) == "Success" ? "确认校准反馈成功" : "确认校准反馈失败";       
        InsertTlog(log_message);
        string alter = opt.GetSegValue("select case  when total = done then 1 else 0 end as status from (select  count(distinct t.id) as total,count( distinct t1.id) as done from ht_eq_mclbr_plan_detail t left join ht_eq_mclbr_plan_detail t1 on t1.id = t.id and t1.status = '5' where t.main_id = '" + txtCode.Text + "')", "status");
        if (alter == "1")
        {
            opt.UpDateOra("update HT_EQ_MCLBR_PLAN set TASK_STATUS = '5' where PZ_CODE = '" + txtCode.Text + "'");
            bindGrid1();
        }
        bindGrid2(txtCode.Text);
    }


    protected void btnTrack_Click(object sender, EventArgs e)//
    {
           MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
           //if (opt.GetSegValue("select flow_status from ht_eq_mclbr_plan where pz_code = '" + txtCode.Text + "'", "flow_status") != "2")
           //{
           // ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", "$('#dspcthor').hide();", true);
           //    return;
           //}
        for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
        {
            if (((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked)
            {
                string ID = GridView2.DataKeys[i].Value.ToString();
                string query = "update HT_EQ_MCLBR_PLAN_detail set STATUS = '3'  where ID = '" + ID + "' and status = '2'";
             
                opt.UpDateOra(query);
            }
        }
        string alter = opt.GetSegValue("select case  when total = done then 1 else 0 end as status from (select  count(distinct t.id) as total,count( distinct t1.id) as done from ht_eq_mclbr_plan_detail t left join ht_eq_mclbr_plan_detail t1 on t1.id = t.id and t1.status = '3' where t.main_id = '" + txtCode.Text + "')", "status");
        if (alter == "1")
        {
            opt.UpDateOra("update HT_EQ_MCLBR_PLAN set TASK_STATUS = '3' where PZ_CODE = '" + txtCode.Text + "'");
            bindGrid1();
        }
        bindGrid2(txtCode.Text);
    }

    protected void btnDspcth_Click(object sender, EventArgs e)//
    {

        bool ck = false;
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        //if (opt.GetSegValue("select flow_status from ht_eq_mclbr_plan where pz_code = '" + txtCode.Text + "'", "flow_status") != "2")
        //{
            // ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", "$('#dspcthor').hide();", true);
            //    return;
        //}
        for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
        {
            if (((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked)
            {
                ck = true;
                string ID = GridView2.DataKeys[i].Value.ToString();
                string query = "update HT_EQ_MCLBR_PLAN_detail set STATUS = '1' ,RESPONER = '" + listdspcth.SelectedValue + "' where ID = '" + ID + "' and status = '0'";

                opt.UpDateOra(query);
            }
        }
        string alter = opt.GetSegValue("select case  when total = done then 1 else 0 end as status from (select  count(distinct t.id) as total,count( distinct t1.id) as done from ht_eq_mclbr_plan_detail t left join ht_eq_mclbr_plan_detail t1 on t1.id = t.id and t1.status = '1' where t.main_id = '" + txtCode.Text + "'  and t1.is_del = '0')", "status");
        if (alter == "1")
        {
            opt.UpDateOra("update HT_EQ_MCLBR_PLAN set TASK_STATUS = '1' where PZ_CODE = '" + txtCode.Text + "'");
            bindGrid1();
        }
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", "$('#dspcthor').hide();", true);
        if (ck == true)
            bindGrid2(txtCode.Text);
        else
            ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", "alert('请选择派工任务');", true);

    }
    protected string getType()
    {
        if (rdType1.Checked)
            return "0";
        else
            return "1";
    }
    protected void setType(string type)
    {
        if (type == "0")
        {
            rdType1.Checked = true;
            rdType2.Checked = false;
        }
        else
        {
            rdType2.Checked = true;
            rdType1.Checked = false;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)//
    {

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        string[] seg = { "PZ_CODE", "MT_NAME", "CREATE_ID", "CREATE_DEPT_ID", "EXPIRED_DATE", "REMARK", "IS_MODEL", "CREATE_TIME", "CLBRT_TYPE" };
        string[] value = { txtCode.Text, txtName.Text, listEditor.SelectedValue, listApt.SelectedValue, txtExptime.Text, txtdscrpt.Text, Convert.ToInt16(ckModel.Checked).ToString(), System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), getType() };
        opt.MergeInto(seg, value, 1, "HT_EQ_MCLBR_PLAN");
        bindGrid1();

    }

    protected void btnGrid2Save_Click(object sender, EventArgs e)//
    {
        try
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int rowIndex = row.RowIndex;
            string id = GridView2.DataKeys[rowIndex].Value.ToString();
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            List<String> commandlist = new List<String>();
            commandlist.Add("delete from HT_EQ_MCLBR_PLAN_detail where ID = '" + id + "'");

            string[] seg = { "section", "equipment_id", "point", "exp_finish_time", "remark", "CREATE_TIME", "MAIN_ID" };

            string[] value = { ((DropDownList)row.FindControl("listGridsct")).SelectedValue, ((DropDownList)row.FindControl("listGridEq")).SelectedValue, ((DropDownList)row.FindControl("listGridPoint")).SelectedValue, ((TextBox)row.FindControl("txtGridExptime")).Text, ((TextBox)row.FindControl("txtGridremark")).Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), txtCode.Text };
            commandlist.Add(opt.InsertDatastr(seg, value, "HT_EQ_MCLBR_PLAN_detail"));
            opt.TransactionCommand(commandlist);

         
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select * from HT_EQ_MCLBR_PLAN_detail where MAIN_ID = '" + listModel.SelectedValue + "' and is_del = '0'";
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in data.Tables[0].Rows)
            {
                string[] seg = { "section", "equipment_id", "point", "exp_finish_time", "remark", "CREATE_TIME", "MAIN_ID" };
                string[] value = { row["section"].ToString(), row["equipment_id"].ToString(), row["point"].ToString(), System.DateTime.Now.AddMonths(1).ToString("yyyy-MM") + "-01", row["remark"].ToString(), System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), txtCode.Text };

                string log_message = opt.InsertData(seg, value, "HT_EQ_MCLBR_PLAN_detail") == "Success" ? "新建校准计划明细成功" : "新建校准计划明细失败";
                log_message += "详情:" + string.Join(",", value);
                InsertTlog(log_message);
       
            }

        }
        bindGrid2(txtCode.Text);
    }

}