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
        string query = "select distinct g.id, g.plan_name as 计划名,g.adjust_status 是否有调整,g1.name as 审批状态,g2.issue_name  as 下发状态 ,g3.name as 编制人  from ht_prod_month_plan g left join ht_inner_aprv_status g1 on g1.id = g.b_flow_status left join HT_INNER_BOOL_DISPLAY g2 on g2.id = g.issued_status left join ht_svr_user g3 on g3.id = g.create_id  where g.is_del = '0'";
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
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "Detail", "$('#tabtop2').click();", true);
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
        string query = " select plan_Sort as 顺序号, plan_no as 计划号, prod_code as 产品名称,plan_output as 计划产量,path_code as  路径编码 from ht_prod_month_plan_detail where is_del = '0' and  MONTH_PLAN_ID = " + planID + " order by plan_Sort";

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView2.DataSource = data;
        GridView2.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {

            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                ((TextBox)GridView2.Rows[i].FindControl("txtOrder")).Text = mydrv["顺序号"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtPlanNo")).Text = mydrv["计划号"].ToString();
                ((DropDownList)GridView2.Rows[i].FindControl("listProd")).SelectedValue = mydrv["产品名称"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtOutput")).Text = mydrv["计划产量"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtPathCode")).Text = mydrv["路径编码"].ToString();
            }

        }

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
        }
        else
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "alert('请通过审批后再下发');", true);
        }

        bindGrid1();

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
        if (GridView2.Columns.Count == 9)
        {
            GridView2.Columns[6].Visible = status;
            GridView2.Columns[7].Visible = status;
            GridView2.Columns[8].Visible = status;
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

            ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "Detail", "$('#tabtop2').click();", true);
            //   string query = "update HT_QA_MATER_FORMULA_DETAIL set IS_DEL = '1'  where FORMULA_CODE = '" + txtCode.Text + "' and MATER_CODE = '" + mtr_code + "'";
            //  MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            //   opt.UpDateOra(query);
            //   bindGrid(txtCode.Text);
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
           

            ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "Detail", "$('#tabtop2').click();", true);
          
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

        if (!Regex.IsMatch(hidePlanID.Value, @"^[+-]?/d*$"))
            hidePlanID.Value = hidePlanID.Value.Substring(hidePlanID.Value.LastIndexOf(',') + 1);
        string query = " select plan_Sort as 顺序号, plan_no as 计划号, prod_code as 产品名称,plan_output as 计划产量,path_code as  路径编码 from ht_prod_month_plan_detail where is_del = '0' and  MONTH_PLAN_ID = " + hidePlanID.Value + " order by plan_Sort";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet set = opt.CreateDataSetOra(query);
        DataTable data = new DataTable();
        if (set == null)
        {
            data.Columns.Add("顺序号");
            data.Columns.Add("计划号");
            data.Columns.Add("产品名称");
            data.Columns.Add("计划产量");
            data.Columns.Add("路径编码");
        }
        else
            data = set.Tables[0];

        object[] value = { "", "", "", 0, " " };
        data.Rows.Add(value);
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

        string log_message = opt.InsertData(seg, value, "ht_prod_month_plan") == "Success" ? "新建月度生产计划成功" : "新建月度生产计划失败";
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
                    commandlist.Add("update HT_PROD_MONTH_PLAN set ADJUST_STATUS = '1' where ID = '" + hidePlanID.Value + "'");
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
                commandlist.Add("update HT_PROD_MONTH_PLAN set ADJUST_STATUS = '1' where ID = '" + hidePlanID.Value + "'");
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
            string path_code = ((TextBox)row.FindControl("txtPathCode")).Text;
          
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
                commandlist.Add("update HT_PROD_MONTH_PLAN set ADJUST_STATUS = '1' where ID = '" + hidePlanID.Value + "'");

            string log_message = opt.TransactionCommand(commandlist) == "Success" ? "保存生产计划详情成功" : "生产计划详情失败";
            log_message += "--详情:" + string.Join(",", value);
            InsertTlog(log_message);

            if (path_code != " ")
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

    private void insertSectionPath(string path_code, string planno)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet pathdata = opt.CreateDataSetOra("select  pathcode  from ht_pub_path_plan where prod_plan = '" + planno + "' order by section_code ");
        string path = "";
        if (pathdata != null && pathdata.Tables[0].Rows.Count > 0)
        {
            bool first = false;
            foreach(DataRow row in pathdata.Tables[0].Rows)
            {
                if(first) path += "-";
                path += row["pathcode"].ToString();
                first = true;
            }
        }      
        if (path == path_code)
            return;
        string[] seg = { "SECTION_CODE", "PROD_PLAN", "PATHCODE", "PATHNAME", "CREATE_TIME" };
        List<String> commandlist = new List<String>();
      
        string[] subpath = path_code.Split('-');
        DataSet data = opt.CreateDataSetOra("select g.section_name , g.section_code from ht_pub_tech_section g  where g.is_valid = '1' and g.is_del = '0' and g.IS_PATH_CONFIG = '1' order by g.section_code");
        if (data != null && data.Tables[0].Rows.Count == subpath.Length)
        {
            for (int i = 0; i < subpath.Length; i++)
            {
                DataRow row = data.Tables[0].Rows[i];
                string sectioncode = row["section_code"].ToString();
                string pathname = opt.GetSegValue("select pathname from ht_pub_path_section  where section_code = '" + sectioncode + "' and pathcode = '" + subpath[i] + "'", "pathname");
                if (subpath[i] != " ")
                {
                    string[] value = { sectioncode, planno, subpath[i], pathname, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
                    commandlist.Add(opt.getMergeStr(seg, value, 2, "HT_PUB_PATH_PLAN"));
                }
            }
        }
        string log_message = opt.TransactionCommand(commandlist) == "Success" ? "配置生产任务路径成功" : "配置生产任务路径失败";
        log_message += "--标识:" + planno;
        InsertTlog(log_message);
    }

    //////////////////////////////////////////////////////////////////////////////////
    protected void btnPath_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;

        hidePzcode.Value = rowIndex.ToString();
        bindGrid4();
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", "$('#pathinfo').fadeIn(200);", true);
    }
    protected string createQuery(string section)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select nodename from ht_pub_path_node where section_code = '" + section + "' and is_del = '0' order by orders");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string query = "select PATHNAME as 路径名称";
            int i = 1;
            foreach (DataRow row in data.Tables[0].Rows)
            {
                query += ",substr(pathcode," + i.ToString() + ",1) as " + row[0].ToString();
                i++;
            }
            query += ",SECTION_CODE,pathcode  from ht_pub_path_section where section_code = '" + section + "'";
            return query;
        }
        else
            return "";
    }
    protected void bindGrid4()
    {

        string query = "select g.section_name as 工艺段, nvl(g1.pathname,'') as 路径选择, nvl(g1.pathcode,'') as 路径详情,g.section_code from ht_pub_tech_section g left join  ht_pub_path_plan g1 on g1.section_code = g.section_code and g1.prod_plan = '" + hidePzcode.Value + "' and g1.is_del = '0' where g.is_valid = '1' and g.is_del = '0' and g.IS_PATH_CONFIG = '1' order by g.section_code";
        //string query = "select g.section_name as 工艺段, nvl(g1.pathname,'') as 路径选择, nvl(g1.pathcode,'') as 路径详情,g.section_code from ht_pub_tech_section g left join  ht_pub_path_plan g1 on g1.section_code = g.section_code  and g1.is_del = '0' where g.is_valid = '1' and g.is_del = '0' order by g.section_code";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView4.DataSource = data;
        GridView4.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < GridView4.Rows.Count; i++)
            {
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                ((TextBox)GridView4.Rows[i].FindControl("txtSection")).Text = mydrv["工艺段"].ToString();
                DropDownList list = (DropDownList)GridView4.Rows[i].FindControl("listpath");
                opt.bindDropDownList(list, "select pathname,pathcode from ht_pub_path_section where section_code = '" + mydrv["section_code"].ToString() + "'", "pathname", "pathcode");
                list.SelectedValue = mydrv["路径详情"].ToString();
                query = createQuery(mydrv["section_code"].ToString());
                if (query != "")
                {
                    query += " and pathcode = '" + list.SelectedValue + "'";
                
                    DataSet set = opt.CreateDataSetOra(query);
                    for (int j = 1; j < set.Tables[0].Columns.Count - 2; j++)
                    {
                        CheckBox ck = new CheckBox();
                        // ck.Enabled = false;
                        if (0 == set.Tables[0].Rows.Count)
                            ck.Checked = false;
                        else
                            ck.Checked = (set.Tables[0].Rows[0][j].ToString() == "1");

                        ck.Text = set.Tables[0].Columns[j].Caption;
                        GridView4.Rows[i].Cells[2].Controls.Add(ck);
                    }
                }               
            }
        }


    }//绑定GridView4数据源


    protected void listpath_SelectedIndexChanged(object sender, EventArgs e)
    {

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        for (int i = 0; i < GridView4.Rows.Count; i++)
        {
            DataSet set = opt.CreateDataSetOra("select * from HT_PUB_PATH_NODE where SECTION_CODE ='" + GridView4.DataKeys[i].Value.ToString() + "' and is_del = '0'");
            DropDownList list = (DropDownList)GridView4.Rows[i].FindControl("listpath");
            string pathcode = list.SelectedValue;
            if (set != null && set.Tables[0].Rows.Count > 0)
            {
                if (pathcode.Length < set.Tables[0].Rows.Count)
                    pathcode = pathcode.PadRight(set.Tables[0].Rows.Count, '0');
                for (int j = 0; j < set.Tables[0].Rows.Count; j++)
                {
                    CheckBox ck = new CheckBox();
                    // ck.Enabled = false;               
                    ck.Text = set.Tables[0].Rows[j]["NODENAME"].ToString();
                    GridView4.Rows[i].Cells[2].Controls.Add(ck);
                    if (pathcode.Length > 0)
                        ck.Checked = (pathcode.Substring(j, 1) == "1");
                    else
                        ck.Checked = false;
                }
            }
        }

    }
    protected void btnSavePath_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        int index = Convert.ToInt16(hidePzcode.Value);
        string planno = GridView2.DataKeys[index].Value.ToString();

        string pathcode = "";
        for (int i = 0; i < GridView4.Rows.Count; i++)
        {
            if (i > 0)
                pathcode += "-";
            DropDownList list = (DropDownList)GridView4.Rows[i].FindControl("listpath");
            if (list.SelectedValue != "")
            {
                pathcode += list.SelectedValue;
            }
            else
                pathcode += " ";
        }
        ((TextBox)GridView2.Rows[index].FindControl("txtPathCode")).Text = pathcode;
        if (planno != "")
        {
            string[] seg = { "SECTION_CODE", "PROD_PLAN", "PATHCODE", "PATHNAME", "CREATE_TIME" };
            List<String> commandlist = new List<String>();
            for (int i = 0; i < GridView4.Rows.Count; i++)
            {
                if (((DropDownList)GridView4.Rows[i].FindControl("listpath")).SelectedValue != "")
                {
                    string[] value = { GridView4.DataKeys[i].Value.ToString(), planno, ((DropDownList)GridView4.Rows[i].FindControl("listpath")).SelectedValue, ((DropDownList)GridView4.Rows[i].FindControl("listpath")).SelectedItem.Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };

                    commandlist.Add(opt.getMergeStr(seg, value, 2, "HT_PUB_PATH_PLAN"));
                }

            }
            string log_message = opt.TransactionCommand(commandlist) == "Success" ? "配置生产任务路径成功" : "配置生产任务路径失败";
            log_message += "--标识:" + planno;
            InsertTlog(log_message);
        }

        ScriptManager.RegisterStartupScript(UpdatePanel3, this.Page.GetType(), "close", "$('.shade').fadeOut(100);", true);
    }

}