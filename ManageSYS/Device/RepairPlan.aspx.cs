using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
public partial class Device_RepairPlan : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            txtStart.Text = System.DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd");
            txtStop.Text = System.DateTime.Now.AddDays(45).ToString("yyyy-MM-dd");
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listEditor, "select ID,name  from ht_svr_user t where is_del = '0'", "name", "ID");
            opt.bindDropDownList(listApt, "select f_code,f_name  from ht_svr_org_group  ", "f_name", "f_code");
            opt.bindDropDownList(listdspcth, "select ID,name  from ht_svr_user ", "name", "ID");
            bindGrid1();

        }

    }
    protected void bindGrid1()
    {

        string query = "select t.mt_name as 维修计划,t1.f_name as 部门, t2.name as 审批状态,t3.name as 执行状态,t.remark as 备注,t.pz_code from HT_EQ_RP_PLAN t left join ht_svr_org_group t1 on t1.f_code = t.create_dept_id   left join ht_inner_aprv_status t2 on t2.id = t.flow_status left join ht_inner_eqexe_status t3 on t3.id = t.task_status  where t.expired_date between '" + txtStart.Text + "' and '" + txtStop.Text + "'  and t.IS_DEL = '0'";

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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
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
                    string query = "update HT_EQ_RP_PLAN set IS_DEL = '1'  where PZ_CODE = '" + order_sn + "'";
                    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                    opt.UpDateOra(query);
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
        txtCode.Text = "RP" + System.DateTime.Now.ToString("yyyyMMdd") + (Convert.ToInt16(opt.GetSegValue("select nvl( max(substr(pz_code,11,3)),0) as ordernum from HT_EQ_RP_PLAN where substr(pz_code,1,10) ='RP" + System.DateTime.Now.ToString("yyyyMMdd") + "'", "ordernum")) + 1).ToString().PadLeft(3, '0');
        MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
        listEditor.SelectedValue = user.id;
        listApt.SelectedValue = user.OwningBusinessUnitId;
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
            string[] subvalue = { GridView1.Rows[index].Cells[1].Text, "15", id, Page.Request.UserHostName.ToString() };
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            if (MSYS.Common.AprvFlow.createApproval(subvalue))
            {
                opt.UpDateOra("update HT_EQ_RP_PLAN set FLOW_STATUS = '0'  where PZ_CODE = '" + id + "'");
            }

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
        DataSet data = opt.CreateDataSetOra("select * from HT_EQ_RP_PLAN  where PZ_CODE =  '" + txtCode.Text + "'");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            DataRow row = data.Tables[0].Rows[0];
            txtName.Text = row["MT_NAME"].ToString();
            listEditor.SelectedValue = row["CREATE_ID"].ToString();
            listApt.SelectedValue = row["CREATE_DEPT_ID"].ToString();
            txtExptime.Text = row["EXPIRED_DATE"].ToString();
            txtdscrpt.Text = row["REMARK"].ToString();
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

    }
    protected void bindGrid2(string code)
    {

        string query = "select t.mech_area as 区域,t.equipment_id as 设备名称,t.reason as 维修原因,t.content as 维修内容,t.exp_finish_time as 期望完成时间,t.STATUS as 状态,t.remark as 备注 ,t.ID  from HT_EQ_RP_PLAN_detail t   where t.main_id = '" + code + "' and t.is_del = '0'";

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView2.DataSource = data;
        GridView2.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                ((DropDownList)GridView2.Rows[i].FindControl("listGridarea")).SelectedValue = mydrv["区域"].ToString();
                ((DropDownList)GridView2.Rows[i].FindControl("listGridEq")).SelectedValue = mydrv["设备名称"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtGridReason")).Text = mydrv["维修原因"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtGridcntnt")).Text = mydrv["维修内容"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtGridExptime")).Text = mydrv["期望完成时间"].ToString();
                ((DropDownList)GridView2.Rows[i].FindControl("listGrid2Status")).SelectedValue = mydrv["状态"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtGridremark")).Text = mydrv["备注"].ToString();

            }

        }

    }//绑定GridView2数据源
    protected DataSet eqbind()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select IDKEY,EQ_NAME from ht_eq_eqp_tbl where is_del = '0' and is_valid = '1'");
    }

    protected void btnCkAll_Click(object sender, EventArgs e)//全选
    {
        try
        {
            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                ((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked = true;
            }
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
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
                    string query = "update HT_EQ_RP_PLAN_detail set IS_DEL = '1'  where ID = '" + ID + "'";
                    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                    opt.UpDateOra(query);
                }
            }
            bindGrid2(txtCode.Text);
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)//
    {
        try
        {
            string query = "select t.mech_area as 区域,t.equipment_id as 设备名称,t.reason as 维修原因,t.content as 维修内容,t.exp_finish_time as 期望完成时间,t.STATUS as 状态,t.remark as 备注 ,t.ID  from HT_EQ_RP_PLAN_detail t   where t.main_id = '" + txtCode.Text + "' and t.is_del = '0' union select t.mech_area as 区域,t.equipment_id as 设备名称,t.reason as 维修原因,t.content as 维修内容,t.exp_finish_time as 期望完成时间,t.STATUS as 状态,t.remark as 备注 ,t.ID  from HT_EQ_RP_PLAN_detail t   where t.status = '-1' and t.is_del = '0'";
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataSet set = opt.CreateDataSetOra(query);
            DataTable data = new DataTable();
            if (set == null)
            {
                data.Columns.Add("区域");
                data.Columns.Add("设备名称");
                data.Columns.Add("维修原因");
                data.Columns.Add("维修内容");
                data.Columns.Add("期望完成时间");
                data.Columns.Add("状态");
                data.Columns.Add("备注");
                data.Columns.Add("ID");
            }
            else
                data = set.Tables[0];
            object[] value = { "", "", "", "", "", "", "", 0 };
            data.Rows.Add(value);
            GridView2.DataSource = data;
            GridView2.DataBind();
            if (data != null && data.Rows.Count > 0)
            {
                for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
                {
                    DataRowView mydrv = data.DefaultView[i];
                    ((DropDownList)GridView2.Rows[i].FindControl("listGridarea")).SelectedValue = mydrv["区域"].ToString();
                    ((DropDownList)GridView2.Rows[i].FindControl("listGridEq")).SelectedValue = mydrv["设备名称"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtGridReason")).Text = mydrv["维修原因"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtGridcntnt")).Text = mydrv["维修内容"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtGridExptime")).Text = mydrv["期望完成时间"].ToString();
                    ((DropDownList)GridView2.Rows[i].FindControl("listGrid2Status")).SelectedValue = mydrv["状态"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtGridremark")).Text = mydrv["备注"].ToString();

                }
            }
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void btnDone_Click(object sender, EventArgs e)//
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        if (opt.GetSegValue("select flow_status from HT_EQ_RP_PLAN where pz_code = '" + txtCode.Text + "'", "flow_status") != "2")
            return;
        for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
        {
            if (((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked)
            {
                string ID = GridView2.DataKeys[i].Value.ToString();
                string query = "update HT_EQ_RP_PLAN_detail set STATUS = '5'  where ID = '" + ID + "' and status = '4'";

                opt.UpDateOra(query);
            }
        }
        string alter = opt.GetSegValue("select case  when total = done then 1 else 0 end as status from (select  count(distinct t.id) as total,count( distinct t1.id) as done from HT_EQ_RP_PLAN_detail t left join HT_EQ_RP_PLAN_detail t1 on t1.id = t.id and t1.status = '5' where t.main_id = '" + txtCode.Text + "')", "status");
        if (alter == "1")
        {
            opt.UpDateOra("update HT_EQ_RP_PLAN set TASK_STATUS = '5' where PZ_CODE = '" + txtCode.Text + "'");
            bindGrid1();
        }
        bindGrid2(txtCode.Text);
    }


    protected void btnTrack_Click(object sender, EventArgs e)//
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        //if (opt.GetSegValue("select flow_status from HT_EQ_RP_PLAN where pz_code = '" + txtCode.Text + "'", "flow_status") != "2")
        //{
        // ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", "$('#dspcthor').hide();", true);
        //    return;
        //}
        for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
        {
            if (((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked)
            {
                string ID = GridView2.DataKeys[i].Value.ToString();
                string query = "update HT_EQ_RP_PLAN_detail set STATUS = '3'  where ID = '" + ID + "' and status = '2'";

                opt.UpDateOra(query);
            }
        }
        string alter = opt.GetSegValue("select case  when total = done then 1 else 0 end as status from (select  count(distinct t.id) as total,count( distinct t1.id) as done from HT_EQ_RP_PLAN_detail t left join HT_EQ_RP_PLAN_detail t1 on t1.id = t.id and t1.status = '3' where t.main_id = '" + txtCode.Text + "')", "status");
        if (alter == "1")
        {
            opt.UpDateOra("update HT_EQ_RP_PLAN set TASK_STATUS = '3' where PZ_CODE = '" + txtCode.Text + "'");
            bindGrid1();
        }
        bindGrid2(txtCode.Text);
    }

    protected void btnDspcth_Click(object sender, EventArgs e)//
    {

        bool ck = false;
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        //if (opt.GetSegValue("select flow_status from HT_EQ_RP_PLAN where pz_code = '" + txtCode.Text + "'", "flow_status") != "2")
        //{
        //    ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", "$('#dspcthor').hide();", true);
        //    return;
        //}
        for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
        {
            if (((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked)
            {
                ck = true;
                string ID = GridView2.DataKeys[i].Value.ToString();
                string query = "update HT_EQ_RP_PLAN_detail set STATUS = '1' ,RESPONER = '" + listdspcth.SelectedValue + "' where ID = '" + ID + "' and status = '0'";

                opt.UpDateOra(query);
            }
        }
        string alter = opt.GetSegValue("select case  when total = done then 1 else 0 end as status from (select  count(distinct t.id) as total,count( distinct t1.id) as done from HT_EQ_RP_PLAN_detail t left join HT_EQ_RP_PLAN_detail t1 on t1.id = t.id and t1.status = '1' where t.main_id = '" + txtCode.Text + "' and t1.is_del = '0')", "status");
        if (alter == "1")
        {
            opt.UpDateOra("update HT_EQ_RP_PLAN set TASK_STATUS = '1' where PZ_CODE = '" + txtCode.Text + "'");
            bindGrid1();
        }
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", "$('#dspcthor').hide();", true);
        if (ck == true)
            bindGrid2(txtCode.Text);
        else
            ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", "alert('请选择派工任务');", true);

    }
 
    protected void btnSave_Click(object sender, EventArgs e)//
    {
        try
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

            string[] seg = { "PZ_CODE", "MT_NAME", "CREATE_ID", "CREATE_DEPT_ID", "EXPIRED_DATE", "REMARK", "CREATE_TIME" };
            string[] value = { txtCode.Text, txtName.Text, listEditor.SelectedValue, listApt.SelectedValue, txtExptime.Text, txtdscrpt.Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
            opt.MergeInto(seg, value,1, "HT_EQ_RP_PLAN");
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected DataSet statusbind()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select ID, Name from ht_inner_eqexe_status order by ID ");
    }
    protected void btnGrid2Save_Click(object sender, EventArgs e)//
    {
        try
        {
            Button btn = (Button)sender;
            int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
            string id = GridView2.DataKeys[rowIndex].Value.ToString();
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            if (id == "0")
                id = opt.GetSegValue("select rpdetail_id_seq.nextval as id  from dual", "id");             
            string[] seg = {"ID", "mech_area", "equipment_id", "reason", "content", "exp_finish_time", "remark", "CREATE_TIME", "MAIN_ID"};
            string[] value = { id,((DropDownList)GridView2.Rows[rowIndex].FindControl("listGridarea")).SelectedValue, ((DropDownList)GridView2.Rows[rowIndex].FindControl("listGridEq")).SelectedValue, ((TextBox)GridView2.Rows[rowIndex].FindControl("txtGridReason")).Text, ((TextBox)GridView2.Rows[rowIndex].FindControl("txtGridcntnt")).Text, ((TextBox)GridView2.Rows[rowIndex].FindControl("txtGridExptime")).Text, ((TextBox)GridView2.Rows[rowIndex].FindControl("txtGridremark")).Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), txtCode.Text};
            opt.MergeInto(seg, value,1, "HT_EQ_RP_PLAN_detail");
            bindGrid2(txtCode.Text);

        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

}