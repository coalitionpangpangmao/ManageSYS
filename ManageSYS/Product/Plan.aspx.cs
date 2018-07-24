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
        string query = "select g.id, g.plan_name as 计划名,g.adjust_status 是否有调整,g1.name as 审批状态,g2.issue_name  as 下发状态 ,g3.name as 编制人  from ht_prod_month_plan g left join ht_inner_aprv_status g1 on g1.id = g.b_flow_status left join HT_INNER_BOOL_DISPLAY g2 on g2.id = g.issued_status left join ht_svr_user g3 on g3.id = g.create_id  where g.is_del = '0'";
        if (txtStart.Text != "" && txtStart.Text != "")
            query += " and PLAN_TIME between '" + txtStart.Text + "' and  '" + txtStop.Text + "'";
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
                    ((Button)GridView1.Rows[i].FindControl("btnSubmit")).Enabled = false;
                if (mydrv["下发状态"].ToString() != "未下发")
                    ((Button)GridView1.Rows[i].FindControl("btnIssued")).Enabled = false;

            }
        }

    }

    protected void btnAddPlan_Click(object sender, EventArgs e)//新增计划
    {
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
        string query = " select plan_Sort as 顺序号, plan_no as 计划号, prod_code as 产品规格,plan_output as 计划产量 from ht_prod_month_plan_detail where is_del = '0' and  MONTH_PLAN_ID = " + planID + " order by plan_Sort";

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
                ((DropDownList)GridView2.Rows[i].FindControl("listProd")).SelectedValue = mydrv["产品规格"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtOutput")).Text = mydrv["计划产量"].ToString();

            }

        }

    }
    public DataSet ddlbind()
    {
        string sqlstr = "select prod_name as 产品规格,prod_code from ht_pub_prod_design where is_valid = '1' and is_del  = '0' order by prod_code DESC";
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
            opt.UpDateOra(query);
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
        ArrayList commandlist = new ArrayList();
        commandlist.Add("update ht_prod_month_plan set IS_DEL = '1'  where ID = '" + id + "'");
        commandlist.Add("update ht_prod_month_plan_detail set is_del = '1' where month_plan_id = '" + id + "'");     
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.TransactionCommand(commandlist);

        bindGrid1();

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
            bindGrid2(id);
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
    //查看审批单
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
            if (MSYS.Common.AprvFlow.createApproval(subvalue))
            {
                opt.UpDateOra("update ht_prod_month_plan set B_FLOW_STATUS = '0'  where ID = '" + id + "'");
            }
            bindGrid1();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (!Regex.IsMatch(hidePlanID.Value, @"^[+-]?/d*$"))
                hidePlanID.Value = hidePlanID.Value.Substring(hidePlanID.Value.LastIndexOf(',') + 1);
            string query = "select plan_Sort as 顺序号, plan_no as 计划号, prod_code as 产品规格,plan_output as 计划产量 from ht_prod_month_plan_detail where is_del = '0' and  MONTH_PLAN_ID = " + hidePlanID.Value + " order by plan_Sort";
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataSet set = opt.CreateDataSetOra(query);
            DataTable data = new DataTable();
            if (set == null)
            {
                data.Columns.Add("顺序号");
                data.Columns.Add("计划号");
                data.Columns.Add("产品规格");
                data.Columns.Add("计划产量");
            }
            else
                data = set.Tables[0];
            string planno = opt.GetSegValue("select nvl(Max(substr(PLAN_NO,9,2)),0)+1 as CODE from ht_prod_month_plan_detail where month_plan_ID = '" + hidePlanID.Value + "'", "CODE");
            planno = "PD" + txtYear.Text + listMonth.SelectedValue + planno.PadLeft(2, '0');
            object[] value = { "", planno, "", 0 };
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
                    ((DropDownList)GridView2.Rows[i].FindControl("listProd")).SelectedValue = mydrv["产品规格"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtOutput")).Text = mydrv["计划产量"].ToString();
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
        catch (Exception ee)
        {
            string str = ee.Message;
        }
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string planname = txtYear.Text + "-" + listMonth.SelectedValue;
        string query = "select * from ht_prod_month_plan where plan_name = '" + planname + "生产月计划' and  is_del = '0'";

        string[] seg = { "PLAN_NAME", "PLAN_TIME", "CREATE_ID    ", "CREATE_TIME", "REMARK" };
        string[] value = { planname + "生产月计划", planname, ((MSYS.Data.SysUser)Session["User"]).id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), txtRemark.Text };
        opt.InsertData(seg, value, "ht_prod_month_plan");
        hidePlanID.Value = opt.GetSegValue(query, "ID");


        bindGrid1();
        bindGrid2(hidePlanID.Value);

    }

    protected void btnCkAll_Click(object sender, EventArgs e)
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
    protected void btnDelSel_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked)
                {
                    string mtr_code = ((TextBox)GridView2.Rows[i].FindControl("txtPlanNo")).Text;
                    string query = "update HT_PROD_MONTH_PLAN_DETAIL set IS_DEL = '1'  where PLAN_NO = '" + mtr_code + "'";
                    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                    opt.UpDateOra(query);
                }
            }
            bindGrid2(hidePlanID.Value);
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
            string query = "update HT_PROD_MONTH_PLAN_DETAIL set IS_DEL = '1'  where PLAN_NO = '" + mtr_code + "'";
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.UpDateOra(query);
            bindGrid2(hidePlanID.Value);
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
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号             
            string mtr_code = ((TextBox)GridView2.Rows[Rowindex].FindControl("txtPlanNo")).Text;
            if (!Regex.IsMatch(hidePlanID.Value, @"^[+-]?/d*$"))
                hidePlanID.Value = hidePlanID.Value.Substring(hidePlanID.Value.LastIndexOf(',') + 1);
            string query = "select * from HT_PROD_MONTH_PLAN_DETAIL where MONTH_PLAN_ID = " + hidePlanID.Value + " and plan_no = '" + mtr_code + "'";
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataSet data = opt.CreateDataSetOra(query);
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                string[] seg = { "plan_Sort", "prod_code", "plan_output", "is_del" };
                string[] value = { ((TextBox)GridView2.Rows[Rowindex].FindControl("txtOrder")).Text, ((DropDownList)GridView2.Rows[Rowindex].FindControl("listProd")).SelectedValue, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtOutput")).Text, "0" };
                opt.UpDateData(seg, value, "HT_PROD_MONTH_PLAN_DETAIL", " where MONTH_PLAN_ID = " + hidePlanID.Value + " and plan_no = '" + mtr_code + "'");
            }

            else
            {
                string[] seg = { "plan_Sort", "plan_no", "prod_code", "plan_output", "MONTH_PLAN_ID" };
                string[] value = { ((TextBox)GridView2.Rows[Rowindex].FindControl("txtOrder")).Text, mtr_code, ((DropDownList)GridView2.Rows[Rowindex].FindControl("listProd")).SelectedValue, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtOutput")).Text, hidePlanID.Value };
                opt.InsertData(seg, value, "HT_PROD_MONTH_PLAN_DETAIL");
            }
            bindGrid2(hidePlanID.Value);
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }


    //////////////////////////////////////////////////////////////////////////////////
     protected void btnPath_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string ID = GridView1.DataKeys[rowIndex].Value.ToString();
        hidePlanID.Value = ID;
        bindGrid4();
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "$('#pathinfo').fadeIn(200);", true);
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
       
            //string query = "select g.section_name as 工艺段, nvl(g1.pathname,'') as 路径选择, nvl(g1.pathcode,'') as 路径详情,g.section_code from ht_pub_tech_section g left join  ht_pub_path_plan g1 on g1.section_code = g.section_code and g1.prod_plan = '" + hidePlanID.Value + "' and g1.is_del = '0' where g.is_valid = '1' and g.is_del = '0' order by g.section_code";
            string query = "select g.section_name as 工艺段, nvl(g1.pathname,'') as 路径选择, nvl(g1.pathcode,'') as 路径详情,g.section_code from ht_pub_tech_section g left join  ht_pub_path_plan g1 on g1.section_code = g.section_code  and g1.is_del = '0' where g.is_valid = '1' and g.is_del = '0' order by g.section_code";
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

  
    protected void btnGrid4Save_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int index = ((GridViewRow)btn.NamingContainer).RowIndex;
        string[] seg = { "SECTION_CODE", "PATHCODE", "PATHNAME", "CREATE_TIME", "PROD_PLAN" };
        string[] value = { GridView4.DataKeys[index].Value.ToString(), ((DropDownList)GridView4.Rows[index].FindControl("listpath")).SelectedValue, ((DropDownList)GridView4.Rows[index].FindControl("listpath")).SelectedItem.Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), hidePlanID.Value };
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.UpDateOra("delete from HT_PUB_PATH_PLAN where section_code = '" + GridView4.DataKeys[index].Value.ToString() + "' and PROD_PLAN = '" + hidePlanID.Value + "'");
        opt.InsertData(seg, value, "HT_PUB_PATH_PLAN");
        bindGrid4();

    }
    protected void listpath_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
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
        catch (Exception ee)
        {
        }

    }
    protected void btnSavePath_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string[] seg = { "SECTION_CODE", "PATHCODE", "PATHNAME", "CREATE_TIME", "PROD_PLAN" };
        for (int i = 0; i < GridView4.Rows.Count; i++)
        {
            string[] value = { GridView4.DataKeys[i].Value.ToString(), ((DropDownList)GridView4.Rows[i].FindControl("listpath")).SelectedValue, ((DropDownList)GridView4.Rows[i].FindControl("listpath")).SelectedItem.Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), hidePlanID.Value };
            opt.UpDateOra("delete from HT_PUB_PATH_PLAN where section_code = '" + GridView4.DataKeys[i].Value.ToString() + "' and PROD_PLAN = '" + hidePlanID.Value + "'");
            opt.InsertData(seg, value, "HT_PUB_PATH_PLAN");
        }
        bindGrid4();
    }

}