using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
public partial class Product_Plan : MSYS.Web.BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            bindGrid1();
        }
    }
    protected void bindGrid1()
    {
        string query = "select distinct g.id, g.plan_name as 计划名,case g.adjust_status when '1' then '是' else  '否' end as 是否有调整,g1.name as 审批状态,g2.issue_name  as 下发状态 ,g3.name as 编制人  from ht_prod_month_plan g left join ht_inner_aprv_status g1 on g1.id = g.b_flow_status left join HT_INNER_BOOL_DISPLAY g2 on g2.id = g.issued_status left join ht_svr_user g3 on g3.id = g.create_id  where g.is_del = '0'";
        if (txtStart.Text != "" && txtStart.Text != "")
            query += " and PLAN_TIME between '" + txtStart.Text + "' and  '" + txtStop.Text + "'";
        query += " order by g.id";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                DataRowView mydrv = data.Tables[0].DefaultView[i];

                ((Label)GridView1.Rows[i].FindControl("labAprv")).Text = mydrv["审批状态"].ToString();
                ((Label)GridView1.Rows[i].FindControl("labIssue")).Text = mydrv["下发状态"].ToString();
                if (!(mydrv["审批状态"].ToString() == "未提交" || mydrv["审批状态"].ToString() == "未通过"))
                {
                    ((Button)GridView1.Rows[i].FindControl("btnSubmit")).Enabled = false;
                    ((Button)GridView1.Rows[i].FindControl("btnSubmit")).CssClass = "btngrey";
                    ((Button)GridView1.Rows[i].FindControl("btnGridEdit")).Text = "查看计划";                  
                }
                else
                {
                    ((Button)GridView1.Rows[i].FindControl("btnSubmit")).Enabled = true;
                    ((Button)GridView1.Rows[i].FindControl("btnSubmit")).CssClass = "btn1 auth";
                    ((Button)GridView1.Rows[i].FindControl("btnGridEdit")).Text = "编制计划";                    
                }
                if (mydrv["下发状态"].ToString() != "未下发")
                {
                    ((Button)GridView1.Rows[i].FindControl("btnIssued")).Enabled = false;
                    ((Button)GridView1.Rows[i].FindControl("btnIssued")).CssClass = "btngrey";
                }
                else
                {
                    ((Button)GridView1.Rows[i].FindControl("btnIssued")).Enabled = true;
                    ((Button)GridView1.Rows[i].FindControl("btnIssued")).CssClass = "btn1 auth";
                }

            }
        }

    }

    protected void btnAddPlan_Click(object sender, EventArgs e)//新增计划
    {
        SetEnable(true,"0");
       
    }

    //查
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid1();
    }
    protected void bindGrid2(string planID)
    {
        if (Regex.IsMatch(planID, @"^[+-]?/d*$"))
            hidePlanID.Value = planID;
        else hidePlanID.Value = planID.Substring(planID.LastIndexOf(',') + 1);
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.UpDateOra("delete from ht_prod_month_plan_detail where is_valid = '0'");
        string query = " select t.plan_Sort as 顺序号, t.plan_no as 计划号, t.prod_code as 产品名称,round(t.plan_output,3) as 计划产量,t.path_code as  路径编码,r.name as 生产状态,(case t.mater_status when '1' then '要料中'  when '2' then '己出库' when '3' then '己到料' else ''  end) as 原料状态,(case t.coat_status when '1' then '要料中'  when '2' then '己出库' when '3' then '己到料' else ''  end) as 回填液状态 ,(case t.FLAVOR_STATUS when '1' then '要料中'  when '2' then '己出库' when '3' then '己到料' else ''  end) as 香精香料状态 from ht_prod_month_plan_detail t left join ht_inner_prodexe_status r on t.exe_status = r.id  where t.is_del = '0' and  t.MONTH_PLAN_ID = " + planID + " order by plan_Sort";

       
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null)
            databindGrid2(data.Tables[0]);       
    }
    public DataSet ddlbind()
    {
        string sqlstr = "select prod_name as 产品名称,prod_code from ht_pub_prod_design where is_valid = '1' and is_del  = '0' order by prod_code DESC";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra(sqlstr);
    }
    protected void btnIssued_Click(object sender, EventArgs e)//下发计划
    {

        Button btn = (Button)sender;
        int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号  
        string id = GridView1.DataKeys[Rowindex].Value.ToString();
        string aprv = ((Label)GridView1.Rows[Rowindex].FindControl("labAprv")).Text;
        if (aprv == "己通过")
        {
            string query = "update ht_prod_month_plan set ISSUED_STATUS = '1'  where ID = '" + id + "'";
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string log_message = opt.UpDateOra(query) == "Success" ? "下发计划成功" : "下发计划失败";
            log_message += "--标识:" + id;
            InsertTlog(log_message);
            bindGrid1();
        }
        else
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "alert('请通过审批后再下发');", true);
        }

       

    }
    protected void btnGridDel_Click(object sender, EventArgs e)//计划删除
    {
       
        Button btn = (Button)sender;
        int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号  
        string id = GridView1.DataKeys[Rowindex].Value.ToString();
        List<String> commandlist = new List<String>();
        commandlist.Add("delete from  ht_prod_month_plan  where ID = '" + id + "'");
        
        commandlist.Add("delete from ht_pub_aprv_flowinfo where BUSIN_ID = '" + id + "'");
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string log_message = opt.TransactionCommand(commandlist) == "Success" ? "删除月度生产计划成功" : "删除月度生产计划失败";
        log_message += "--标识:" + id;
        InsertTlog(log_message);

        bindGrid1();

    }
    protected void SetEnable(bool status,string adjust)
    {
        btnAdd.Visible = status;
        btnDelSel.Visible = status;
        btnGrid2Modify.Visible = status;
        if (GridView2.Columns.Count == 16)
        {
            GridView2.Columns[1].Visible = status;
            GridView2.Columns[2].Visible = status;
          
            GridView2.Columns[8].Visible = status;
            GridView2.Columns[9].Visible = status;
            GridView2.Columns[10].Visible = !status;
            GridView2.Columns[11].Visible = !status;
            GridView2.Columns[12].Visible = !status;
            GridView2.Columns[13].Visible = !status;
            GridView2.Columns[14].Visible = !status;
            GridView2.Columns[15].Visible = !status;
        }
        hideAdjust.Value = adjust;
    }
    protected void btnGridEdit_Click(object sender, EventArgs e)//编制计划
    {
        try
        {
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号  
            string id = GridView1.DataKeys[Rowindex].Value.ToString();
            txtYear.Text = GridView1.Rows[Rowindex].Cells[2].Text.Substring(0, 4);
            listMonth.SelectedValue = GridView1.Rows[Rowindex].Cells[2].Text.Substring(5, 2);
            string aprvstatus = ((Label)GridView1.Rows[Rowindex].FindControl("labAprv")).Text;
            bindGrid2(id);
            if (aprvstatus == "未提交")
                SetEnable(true,"0");
            else
                SetEnable(false,"0");

          
           
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }


    protected void btnGridAlter_Click(object sender, EventArgs e)//调整计划
    {
        try
        {
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号  
            string id = GridView1.DataKeys[Rowindex].Value.ToString();
            txtYear.Text = GridView1.Rows[Rowindex].Cells[2].Text.Substring(0, 4);
            listMonth.SelectedValue = GridView1.Rows[Rowindex].Cells[2].Text.Substring(5, 2);
            string aprvstatus = ((Label)GridView1.Rows[Rowindex].FindControl("labAprv")).Text;
            bindGrid2(id);
                SetEnable(true,"1");
           

          
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnFLow_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string ID = GridView1.DataKeys[rowIndex].Value.ToString();
        string query = "select pos as 顺序号, workitemid as 审批环节,username as 负责人,comments as 意见,opiniontime 审批时间,(case status when '0' then '未审批'  when '1' then '未通过' else '己通过' end)  as 审批状态  from ht_pub_aprv_opinion r left join ht_pub_aprv_flowinfo s on r.gongwen_id = s.id where s.busin_id  = '" + ID + "' order by pos";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        GridView3.DataSource = opt.CreateDataSetOra(query);
        GridView3.DataBind();
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "$('#flowinfo').fadeIn(200);", true);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)//提交审批
    {
        try
        {
            Button btn = (Button)sender;
            int index = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号                 
            string id = GridView1.DataKeys[index].Value.ToString();
            /*启动审批TB_ZT标题,TBR_ID填报人id,TBR_NAME填报人name,TB_BM_ID填报部门id,TB_BM_NAME填报部门name,TB_DATE申请时间创建日期,MODULENAME审批类型编码,URL 单独登录url,BUSIN_ID业务数据id*/
            string[] subvalue = { GridView1.Rows[index].Cells[2].Text, "06", id, Page.Request.UserHostName.ToString() };
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string log_message = MSYS.AprvFlow.createApproval(subvalue) ? "提交审批成功," : "提交审批失败，";
            log_message += ",业务数据ID：" + id;
            InsertTlog(log_message);

            bindGrid1();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
         MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        if (!Regex.IsMatch(hidePlanID.Value, @"^[+-]?/d*$"))
            hidePlanID.Value = hidePlanID.Value.Substring(hidePlanID.Value.LastIndexOf(',') + 1);
        foreach (GridViewRow row in GridView2.Rows)
        {
            if (((TextBox)row.FindControl("txtPlanNo")).Text.Substring(11, 4) == "TEMP")
            {
                string[] subseg = { "plan_no", "MONTH_PLAN_ID", "plan_Sort", "prod_code", "plan_output" };
                string[] subvalue = { ((TextBox)row.FindControl("txtPlanNo")).Text, hidePlanID.Value, ((TextBox)row.FindControl("txtOrder")).Text, ((DropDownList)row.FindControl("listProd")).SelectedValue, ((TextBox)row.FindControl("txtOutput")).Text };
                 opt.MergeInto(subseg, subvalue, 1, "HT_PROD_MONTH_PLAN_DETAIL");
            }
        }               
        string planno = opt.GetSegValue("select nvl(Max(substr(PLAN_NO,16,2)),0)+1 as CODE from ht_prod_month_plan_detail where month_plan_ID = '" + hidePlanID.Value + "'", "CODE");       
        string mtr_code = "PD" + txtYear.Text + listMonth.SelectedValue + "000TEMP" + planno.PadLeft(2, '0');
        string order = opt.GetSegValue("select nvl(max(plan_sort),0)+1 as ordernum  from ht_prod_month_plan_detail where month_plan_id= '" + hidePlanID.Value + "'", "ordernum");
    
      
        string[] seg = { "plan_no", "MONTH_PLAN_ID", "plan_Sort",  "is_del","is_valid" };
        string[] value = { mtr_code, hidePlanID.Value, order,  "0","0" };
        opt.MergeInto(seg, value, 1, "HT_PROD_MONTH_PLAN_DETAIL");
        string query = " select t.plan_Sort as 顺序号, t.plan_no as 计划号, t.prod_code as 产品名称,round(t.plan_output,3) as 计划产量,t.path_code as  路径编码,r.name as 生产状态,(case t.mater_status when '1' then '要料中'  when '2' then '己出库' when '3' then '己到料' else ''  end) as 原料状态,(case t.coat_status when '1' then '要料中'  when '2' then '己出库' when '3' then '己到料' else ''  end) as 回填液状态,(case t.FLAVOR_STATUS when '1' then '要料中'  when '2' then '己出库' when '3' then '己到料' else ''  end) as 香精香料状态 from ht_prod_month_plan_detail t left join ht_inner_prodexe_status r on t.exe_status = r.id  where t.is_del = '0' and  t.MONTH_PLAN_ID = " + hidePlanID.Value + " order by plan_Sort";
       
        DataSet set = opt.CreateDataSetOra(query);
        if (set != null)
        {
           DataTable data = set.Tables[0];
            databindGrid2(data);
        }
       
    }
    protected void databindGrid2(DataTable data)
    {
        GridView2.DataSource = data;
        GridView2.DataBind();
        if (data != null && data.Rows.Count > 0)
        {
            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                DataRowView mydrv = data.DefaultView[i];
                ((TextBox)GridView2.Rows[i].FindControl("txtOrder")).Text = mydrv["顺序号"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtPlanNo")).Text = mydrv["计划号"].ToString();
                ((DropDownList)GridView2.Rows[i].FindControl("listProd")).SelectedValue = mydrv["产品名称"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtOutput")).Text = mydrv["计划产量"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtPathCode")).Text = mydrv["路径编码"].ToString();
                ((Label)GridView2.Rows[i].FindControl("labexe")).Text = mydrv["生产状态"].ToString();
                ((Label)GridView2.Rows[i].FindControl("labmater")).Text = mydrv["原料状态"].ToString();
                ((Label)GridView2.Rows[i].FindControl("labcoat")).Text = mydrv["回填液状态"].ToString();
                ((Label)GridView2.Rows[i].FindControl("labflavor")).Text = mydrv["香精香料状态"].ToString();
                if (mydrv["生产状态"].ToString() == "未下发" || mydrv["生产状态"].ToString() == "")
                {
                    ((Button)GridView2.Rows[i].FindControl("btnGrid2Save")).Visible = true;
                    ((Button)GridView2.Rows[i].FindControl("btnGrid2Del")).Visible = true;
                }
                else
                {
                    ((Button)GridView2.Rows[i].FindControl("btnGrid2Save")).Visible = false;
                    ((Button)GridView2.Rows[i].FindControl("btnGrid2Del")).Visible = false;
                }
                /*     if (i < GridView1.Rows.Count - 1)
                     {
                         ((TextBox)GridView2.Rows[i].FindControl("txtOrder")).Enabled = false;
                         ((TextBox)GridView2.Rows[i].FindControl("txtPlanNo")).Enabled = false;
                         ((DropDownList)GridView2.Rows[i].FindControl("listProd")).Enabled = false;
                         ((TextBox)GridView2.Rows[i].FindControl("txtOutput")).Enabled = false;
                     }
                     else
                     {
                         ((TextBox)GridView2.Rows[i].FindControl("txtOrder")).Enabled = true;
                         ((TextBox)GridView2.Rows[i].FindControl("txtPlanNo")).Enabled = true;
                         ((DropDownList)GridView2.Rows[i].FindControl("listProd")).Enabled = true;
                         ((TextBox)GridView2.Rows[i].FindControl("txtOutput")).Enabled = true;
                     }*/
            }
        }
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string planname = txtYear.Text + "-" + listMonth.SelectedValue;
        string query = "select * from ht_prod_month_plan where plan_name = '" + planname + "生产月计划' and is_del = '0' ";
        hidePlanID.Value = opt.GetSegValue(query, "ID");
        if (hidePlanID.Value != "NoRecord")
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "exist", "alert('己存在该月计划，请核实！！')", true);
            return;
        }

        string[] seg = { "PLAN_NAME", "PLAN_TIME", "CREATE_ID", "CREATE_TIME", "REMARK" };
        string[] value = { planname + "生产月计划", planname, ((MSYS.Data.SysUser)Session["User"]).id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), txtRemark.Text };

        string log_message = opt.InsertData(seg, value, "ht_prod_month_plan") == "Success" ? "新增月度生产计划成功" : "新增月度生产计划失败";
        log_message += "--详情:" + string.Join(",", value);
        InsertTlog(log_message);        
        hidePlanID.Value = opt.GetSegValue(query, "ID");


        bindGrid1();
        bindGrid2(hidePlanID.Value);

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
            List<string> commandlist = new List<string>();
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string planlist = "";
            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked)
                {
                    string mtr_code = ((TextBox)GridView2.Rows[i].FindControl("txtPlanNo")).Text;
                    commandlist.Add("update HT_PROD_MONTH_PLAN_DETAIL set IS_DEL = '1'  where PLAN_NO = '" + mtr_code + "'");
                    planlist += mtr_code + ",";
                }
            }
            if (commandlist.Count > 0)
            {
                if (hideAdjust.Value == "1")
                    commandlist.Add("update HT_PROD_MONTH_PLAN set ADJUST_STATUS = '1',MODIFY_ID = '" + ((MSYS.Data.SysUser)Session["User"]).id + "' where ID = '" + hidePlanID.Value + "'");
                string log_message = opt.TransactionCommand(commandlist) == "Success" ? "删除生产计划明细成功" : "删除生产计划明细失败";
                log_message += "--标识:" + planlist;
                InsertTlog(log_message);
                bindGrid2(hidePlanID.Value);
                bindGrid1();
            }
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnGrid2Del_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号             
            string mtr_code = ((TextBox)GridView2.Rows[Rowindex].FindControl("txtPlanNo")).Text;
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            List<string> commandlist = new List<string>();
            commandlist.Add("update HT_PROD_MONTH_PLAN_DETAIL set IS_DEL = '1'  where PLAN_NO = '" + mtr_code + "'");
            if (hideAdjust.Value == "1")
                commandlist.Add("update HT_PROD_MONTH_PLAN set ADJUST_STATUS = '1',MODIFY_ID = '" + ((MSYS.Data.SysUser)Session["User"]).id + "' where ID = '" + hidePlanID.Value + "'");
            string log_message = opt.TransactionCommand(commandlist) == "Success" ? "删除月度生产计划明细成功" : "删除月度生产计划明细失败";
            log_message += "--标识:" + mtr_code;
            InsertTlog(log_message);
            bindGrid2(hidePlanID.Value);
            bindGrid1();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    
    protected void btnGrid2Save_Click(object sender, EventArgs e)
    {
        try
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int Rowindex = row.RowIndex;//获得行号             
            string mtr_code = ((TextBox)row.FindControl("txtPlanNo")).Text;
            string prod_code = ((DropDownList)row.FindControl("listProd")).SelectedValue;
            if (!Regex.IsMatch(hidePlanID.Value, @"^[+-]?/d*$"))
                hidePlanID.Value = hidePlanID.Value.Substring(hidePlanID.Value.LastIndexOf(',') + 1);
            string path_code = opt.GetSegValue("select path_code from ht_pub_prod_design where prod_code = '" + prod_code + "'","path_code") ;
           
            if (mtr_code == "" || mtr_code.Substring(8,7) != prod_code)
            {
                if (mtr_code.Length >= 17 && mtr_code.Substring(8, 7) != prod_code)
                    opt.UpDateOra("delete from ht_prod_month_plan_detail where plan_no = '" + mtr_code + "'");
                string planno = opt.GetSegValue("select nvl(Max(substr(PLAN_NO,16,2)),0)+1 as CODE from ht_prod_month_plan_detail where month_plan_ID = '" + hidePlanID.Value + "'", "CODE");
                mtr_code = "PD" + txtYear.Text + listMonth.SelectedValue + prod_code + planno.PadLeft(2, '0');
              
            }
            List<string> commandlist = new List<string>();
            string[] seg = { "plan_no", "MONTH_PLAN_ID", "plan_Sort", "prod_code", "plan_output", "path_code","is_del" };
            string[] value = { mtr_code, hidePlanID.Value, ((TextBox)row.FindControl("txtOrder")).Text, prod_code, ((TextBox)row.FindControl("txtOutput")).Text, path_code ,"0"};
            commandlist.Add(opt.getMergeStr(seg, value, 1, "HT_PROD_MONTH_PLAN_DETAIL"));
            if (hideAdjust.Value == "1")
                commandlist.Add("update HT_PROD_MONTH_PLAN set ADJUST_STATUS = '1' ,MODIFY_ID = '" + ((MSYS.Data.SysUser)Session["User"]).id + "' where ID = '" + hidePlanID.Value + "'");

            string log_message = opt.TransactionCommand(commandlist) == "Success" ? "保存生产计划详情成功" : "生产计划详情失败";
            log_message += "--详情:" + string.Join(",", value);
            InsertTlog(log_message);

            if (path_code != "")
            {
                insertSectionPath(path_code, mtr_code);
            }
            bindGrid2(hidePlanID.Value);
            bindGrid1();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnGrid2Modify_Click(object sender, EventArgs e)
    {
        try
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            foreach (GridViewRow row in GridView2.Rows)
            { 
                    int Rowindex = row.RowIndex;//获得行号             
                    string mtr_code = ((TextBox)row.FindControl("txtPlanNo")).Text;
                    string prod_code = ((DropDownList)row.FindControl("listProd")).SelectedValue;
                    if (!Regex.IsMatch(hidePlanID.Value, @"^[+-]?/d*$"))
                        hidePlanID.Value = hidePlanID.Value.Substring(hidePlanID.Value.LastIndexOf(',') + 1);
                    string path_code = opt.GetSegValue("select path_code from ht_pub_prod_design where prod_code = '" + prod_code + "'", "path_code");

                    if (mtr_code == "" || mtr_code.Substring(8, 7) != prod_code)
                    {
                        if (mtr_code.Length >= 17 && mtr_code.Substring(8, 7) != prod_code)
                            opt.UpDateOra("delete from ht_prod_month_plan_detail where plan_no = '" + mtr_code + "'");
                        string planno = opt.GetSegValue("select nvl(Max(substr(PLAN_NO,16,2)),0)+1 as CODE from ht_prod_month_plan_detail where month_plan_ID = '" + hidePlanID.Value + "'", "CODE");
                        mtr_code = "PD" + txtYear.Text + listMonth.SelectedValue + prod_code + planno.PadLeft(2, '0');

                    }
                    List<string> commandlist = new List<string>();
                    string[] seg = { "plan_no", "MONTH_PLAN_ID", "plan_Sort", "prod_code", "plan_output", "path_code", "is_del" };
                    string[] value = { mtr_code, hidePlanID.Value, ((TextBox)row.FindControl("txtOrder")).Text, prod_code, ((TextBox)row.FindControl("txtOutput")).Text, path_code, "0" };
                    commandlist.Add(opt.getMergeStr(seg, value, 1, "HT_PROD_MONTH_PLAN_DETAIL"));
                    if (hideAdjust.Value == "1")
                        commandlist.Add("update HT_PROD_MONTH_PLAN set ADJUST_STATUS = '1',MODIFY_ID = '" + ((MSYS.Data.SysUser)Session["User"]).id + "' where ID = '" + hidePlanID.Value + "'");

                    string log_message = opt.TransactionCommand(commandlist) == "Success" ? "保存生产计划详情成功" : "生产计划详情失败";
                    log_message += "--详情:" + string.Join(",", value);
                    InsertTlog(log_message);

                    if (path_code != " ")
                    {
                        insertSectionPath(path_code, mtr_code);
                    }
               
            }
            bindGrid2(hidePlanID.Value);
            bindGrid1();

        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
     protected void btnUp_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int index = row.RowIndex;
        if (index == 0)
            return;
        else
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            GridViewRow uprow = GridView2.Rows[index - 1];
            List<string> commandlist = new List<string>();
            commandlist.Add("update HT_PROD_MONTH_PLAN_DETAIL set plan_Sort ='" + ((TextBox)uprow.FindControl("txtOrder")).Text + "' where plan_no = '" + ((TextBox)row.FindControl("txtPlanNo")).Text + "'");
            commandlist.Add("update HT_PROD_MONTH_PLAN_DETAIL set plan_Sort ='" + ((TextBox)row.FindControl("txtOrder")).Text + "' where plan_no = '" + ((TextBox)uprow.FindControl("txtPlanNo")).Text + "'");
            string log_message = opt.TransactionCommand(commandlist) == "Success" ? "调整生产顺序成功" : "调整生产顺序失败";
            log_message += "--详情:" + string.Join(",", commandlist);
            InsertTlog(log_message);
            bindGrid2(hidePlanID.Value);
        }
    }
    protected void btnDown_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int index = row.RowIndex;
        if (index == GridView2.Rows.Count-1)
            return;
        else
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            GridViewRow downrow = GridView2.Rows[index + 1];
            List<string> commandlist = new List<string>();
            commandlist.Add("update HT_PROD_MONTH_PLAN_DETAIL set plan_Sort ='" + ((TextBox)downrow.FindControl("txtOrder")).Text + "' where plan_no = '" + ((TextBox)row.FindControl("txtPlanNo")).Text + "'");
            commandlist.Add("update HT_PROD_MONTH_PLAN_DETAIL set plan_Sort ='" + ((TextBox)row.FindControl("txtOrder")).Text + "' where plan_no = '" + ((TextBox)downrow.FindControl("txtPlanNo")).Text + "'");
            string log_message = opt.TransactionCommand(commandlist) == "Success" ? "调整生产顺序成功" : "调整生产顺序失败";
            log_message += "--详情:" + string.Join(",", commandlist);
            InsertTlog(log_message);
            bindGrid2(hidePlanID.Value);
        }
    }
    protected void btnGrid2Apply_Click(object sender, EventArgs e)
    {
        try
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int Rowindex = row.RowIndex;//获得行号  
            string planno = GridView2.DataKeys[Rowindex].Value.ToString() ;

           
                  List<string> commandlist = new List<string>();
          //  commandlist.Add("update HT_PROD_MONTH_PLAN_DETAIL set EXE_STATUS = '4',MATER_STATUS = '1' where PLAN_NO = '" + planno + "' and EXE_STATUS = '2'");
                  commandlist.Add("update HT_PROD_MONTH_PLAN_DETAIL set  MATER_STATUS = '1',COAT_STATUS = '1',Flavor_status = '1' where PLAN_NO = '" + planno + "' and EXE_STATUS <> '3'");
            string log_message = opt.TransactionCommand(commandlist) == "Success" ? "申请物料成功" : "申请物料失败";
            log_message += "--详情:" + planno;
            InsertTlog(log_message);
           
            bindGrid2(hidePlanID.Value);
          
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void btnGrid2Feed_Click(object sender, EventArgs e)
    {
        try
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int Rowindex = row.RowIndex;//获得行号  
            string planno = GridView2.DataKeys[Rowindex].Value.ToString();
            List<string> commandlist = new List<string>();
          //  commandlist.Add("update HT_PROD_MONTH_PLAN_DETAIL set EXE_STATUS = '2',MATER_STATUS = '2' where PLAN_NO = '" + planno + "' and EXE_STATUS = '4'");
            commandlist.Add("update HT_PROD_MONTH_PLAN_DETAIL set  MATER_STATUS = '3' where PLAN_NO = '" + planno + "' and MATER_STATUS = '2'");
            commandlist.Add("update HT_PROD_MONTH_PLAN_DETAIL set  COAT_STATUS = '3'  where PLAN_NO = '" + planno + "' and COAT_STATUS = '2'");
            commandlist.Add("update HT_PROD_MONTH_PLAN_DETAIL set  FLAVOR_STATUS = '3'  where PLAN_NO = '" + planno + "' and FLAVOR_STATUS = '2'");
            string log_message = opt.TransactionCommand(commandlist) == "Success" ? "确认到料" : "确认到料";
            log_message += "--详情:" + planno;
            InsertTlog(log_message);
            bindGrid2(hidePlanID.Value);
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

   
    private void insertSectionPath(string path_code, string planno)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet pathdata = opt.CreateDataSetOra("select  pathcode,section_code  from ht_pub_path_plan where prod_plan = '" + planno + "'  and pathcode is not null order by section_code ");
        string path = "";
        if (pathdata != null && pathdata.Tables[0].Rows.Count > 0)
        {
            bool first = false;
            foreach(DataRow row in pathdata.Tables[0].Rows)
            {
                if(first) path += "-";
                path +=row["section_code"].ToString()+ row["pathcode"].ToString();
                first = true;
            }
        }      
        if (path == path_code)
            return;
        string[] seg = { "SECTION_CODE", "PROD_PLAN", "PATHCODE", "PATHNAME", "CREATE_TIME" };
        List<String> commandlist = new List<String>();
      
        string[] subpath = path_code.Split('-');
        DataSet data = opt.CreateDataSetOra("select g.section_name , g.section_code from ht_pub_tech_section g  where g.is_valid = '1' and g.is_del = '0' and g.IS_PATH_CONFIG = '1' order by g.section_code");
        if (data != null)
        {           
            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                DataRow row = data.Tables[0].Rows[i];
                string sectioncode = row["section_code"].ToString();
                string pathname = "";
                string pathcode = "";
                foreach(string sub in subpath)
                {
                    if (sub.Substring(0, 5) == sectioncode)
                    {
                        pathname = opt.GetSegValue("select pathname from ht_pub_path_section  where section_code = '" + sectioncode + "' and pathcode = '" + sub.Substring(5) + "'", "pathname");
                        pathcode = sub.Substring(5);
                    }
                }
                string[] value = { sectioncode, planno, pathcode, pathname, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
                    commandlist.Add(opt.getMergeStr(seg, value, 2, "HT_PUB_PATH_PLAN"));
                
            }
        }
        string log_message = opt.TransactionCommand(commandlist) == "Success" ? "配置生产任务路径成功" : "配置生产任务路径失败";
        log_message += "--标识:" + planno;
        InsertTlog(log_message);
    }

    //////////////////////////////////////////////////////////////////////////////////
   
   
   

}