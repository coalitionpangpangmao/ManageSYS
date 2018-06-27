using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
public partial class Product_SeasonPlan : MSYS.Web.BasePage
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
        string query = "select g.id,g.plan_name as 计划名, g.plan_year as 年份,g.quarter as 季度,g.TOTAL_OUTPUT as 计划总产量,g.UNIT as 单位,g1.name as 审批状态,g2.issue_name as 下发状态 ,g3.name as 编制人  from ht_prod_season_plan g left join ht_inner_aprv_status g1 on g1.id = g.flow_status left join HT_INNER_BOOL_DISPLAY g2 on g2.id = g.issued_status left join ht_svr_user g3 on g3.id = g.create_id and g3.is_del = '0' where g.is_del = '0'";
        if (txtYears.Text != "")
            query += " and g.plan_year = '" + txtYears.Text + "'";
        if (listSeason.SelectedValue != "")
            query += " and g.quarter = '" + listSeason.SelectedValue + "'";
        DataBaseOperator opt = new DataBaseOperator();
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
                if (mydrv["审批状态"].ToString() != "未提交")
                    ((Button)GridView1.Rows[i].FindControl("btnSubmit")).Enabled = false;
                if (mydrv["下发状态"].ToString() != "未下发")
                    ((Button)GridView1.Rows[i].FindControl("btnIssued")).Enabled = false;

            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid1();
    }

    protected void bindGrid2(string planID)
    {
        if (Regex.IsMatch(planID, @"^[+-]?/d*$"))
            hidePlanID.Value = planID;
        else hidePlanID.Value = planID.Substring(planID.LastIndexOf(',') + 1);
        string query = "select r.prod_code as 产品,r.TOTAL_OUTPUT as 计划数量,r.plan_output_1 as month1,r.plan_output_2 as month2,r.plan_output_3 as month3,r.id   from ht_prod_season_plan_Detail r  where r.is_del = '0' and  r.QUARTER_PLAN_ID = " + hidePlanID.Value;

        DataBaseOperator opt = new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView2.DataSource = data;
        GridView2.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {

            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                ((DropDownList)GridView2.Rows[i].FindControl("listProd")).SelectedValue = mydrv["产品"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtOutput")).Text = mydrv["计划数量"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtAmount1")).Text = mydrv["month1"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtAmount2")).Text = mydrv["month2"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtAmount3")).Text = mydrv["month3"].ToString();


            }

        }

    }

    public DataSet ddlbind()
    {
        string sqlstr = "select prod_name as 产品规格,prod_code from ht_pub_prod_design where is_valid = '1' and is_del  = '0' order by Prod_code DESC";
        DataBaseOperator opt = new DataBaseOperator();
        return opt.CreateDataSetOra(sqlstr);
    }

    protected void btnIssued_Click(object sender, EventArgs e)//下发计划
    {
        Button btn = (Button)sender;
        int rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;

        string id = GridView1.DataKeys[rowindex].Value.ToString();
        string aprv = ((Label)GridView1.Rows[rowindex].FindControl("labAprv")).Text;
        if (aprv == "己通过")
        {
            string query = "update ht_prod_Season_plan set ISSUED_STATUS = '1'  where ID = '" + id + "'";
            DataBaseOperator opt = new DataBaseOperator();
            opt.UpDateOra(query);
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
        int rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string id = GridView1.DataKeys[rowindex].Value.ToString();
        string query = "update ht_prod_Season_plan set IS_DEL = '1'  where ID = '" + id + "'";
        DataBaseOperator opt = new DataBaseOperator();
        ArrayList commandlist = new ArrayList();
        commandlist.Add("update ht_prod_Season_plan set IS_DEL = '1'  where ID = '" + id + "'");
        commandlist.Add("update ht_prod_season_plan_detail set is_del = '1' where QUARTER_PLAN_ID =  '" + id + "'");
        opt.TransactionCommand(commandlist);

    }

    protected void btnGridEdit_Click(object sender, EventArgs e)//编制计划
    {
        Button btn = (Button)sender;
        int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号  
        string id = GridView1.DataKeys[Rowindex].Value.ToString();
        txtYear.Text = GridView1.Rows[Rowindex].Cells[3].Text;
        listSeason2.SelectedValue = GridView1.Rows[Rowindex].Cells[4].Text;
        bindGrid2(id);
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "Detail", "GridClick();", true);


    }

    protected void btnAddPlan_Click(object sender, EventArgs e)//新增计划
    {
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "Detail", "GridClick();", true);
    }

    //查看审批单
    protected void btnFLow_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string ID = GridView1.DataKeys[rowIndex].Value.ToString();
        string query = "select pos as 顺序号, workitemid as 审批环节,username as 负责人,comments as 意见,opiniontime 审批时间,(case status when '0' then '未审批'  when '1' then '未通过' else '己通过' end)  as 审批状态  from ht_pub_aprv_opinion r left join ht_pub_aprv_flowinfo s on r.gongwen_id = s.id where s.busin_id  = '" + ID + "' order by pos";
        DataBaseOperator opt = new DataBaseOperator();
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
            string[] subvalue = { GridView1.Rows[index].Cells[2].Text, "12", id, Page.Request.UserHostName.ToString() };
            DataBaseOperator opt = new DataBaseOperator();
            if (opt.createApproval(subvalue))
            {
                opt.UpDateOra("update ht_prod_Season_plan set FLOW_STATUS = '0'  where ID = '" + id + "'");
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
            string query = "select r.prod_code as 产品,r.plan_output_1 + r.plan_output_2 + r.plan_output_3 as 计划数量,r.plan_output_1 as month1,r.plan_output_2 as month2,r.plan_output_3 as month3,r.id   from ht_prod_season_plan_Detail r  where r.is_del = '0' and  r.QUARTER_PLAN_ID = " + hidePlanID.Value;
            DataBaseOperator opt = new DataBaseOperator();
            DataSet set = opt.CreateDataSetOra(query);
            DataTable data = new DataTable();
            if (set == null)
            {
                data.Columns.Add("产品");
                data.Columns.Add("计划数量");
                data.Columns.Add("month1");
                data.Columns.Add("month2");
                data.Columns.Add("month3");
                data.Columns.Add("id");
            }
            else
                data = set.Tables[0];
            object[] value = { "", 0, 0, 0, 0, 0 };
            data.Rows.Add(value);
            GridView2.DataSource = data;
            GridView2.DataBind();
            if (data != null && data.Rows.Count > 0)
            {
                for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
                {
                    DataRowView mydrv = data.DefaultView[i];
                    ((DropDownList)GridView2.Rows[i].FindControl("listProd")).SelectedValue = mydrv["产品"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtOutput")).Text = mydrv["计划数量"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtAmount1")).Text = mydrv["month1"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtAmount2")).Text = mydrv["month2"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtAmount3")).Text = mydrv["month3"].ToString();

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
        DataBaseOperator opt = new DataBaseOperator();
        string planname = txtYear.Text + "-" + listSeason2.SelectedValue + "生产计划";

        opt.UpDateOra("delete from  HT_PROD_SEASON_PLAN   where plan_name = '" + planname + "' and  is_del = '0'");
        string[] seg = { "PLAN_YEAR", "QUARTER", "PLAN_NAME", "CREATE_ID", "CREATE_TIME", "REMARK" };
        string[] value = { txtYear.Text, listSeason2.SelectedValue, planname, ((MSYS.Data.SysUser)Session["User"]).Id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), txtRemark.Text };
        opt.InsertData(seg, value, "HT_PROD_SEASON_PLAN");
        hidePlanID.Value = opt.GetSegValue("select * from HT_PROD_SEASON_PLAN   where plan_name = '" + planname + "' and  is_del = '0'", "ID");

        bindGrid1();

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
                    string mtr_code = GridView2.DataKeys[i].Value.ToString();
                    string query = "update HT_PROD_SEASON_PLAN_DETAIL set IS_DEL = '1'  where id = '" + mtr_code + "'";
                    DataBaseOperator opt = new DataBaseOperator();
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
            string mtr_code = GridView2.DataKeys[Rowindex].Value.ToString();
            string query = "update HT_PROD_SEASON_PLAN_DETAIL set IS_DEL = '1'  where id = '" + mtr_code + "'";
            DataBaseOperator opt = new DataBaseOperator();
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
            string mtr_code = GridView2.DataKeys[Rowindex].Value.ToString();
            if (!Regex.IsMatch(hidePlanID.Value, @"^[+-]?/d*$"))
                hidePlanID.Value = hidePlanID.Value.Substring(hidePlanID.Value.LastIndexOf(',') + 1);
            string query = "select * from ht_prod_season_plan_Detail where MONTH_PLAN_ID = " + hidePlanID.Value + " and id = '" + mtr_code + "'";
            DataBaseOperator opt = new DataBaseOperator();
            DataSet data = opt.CreateDataSetOra(query);
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                string[] seg = { "prod_code", "TOTAL_OUTPUT", "plan_output_1", "plan_output_2", "plan_output_3", "IS_DEL" };
                string[] value = { ((DropDownList)GridView2.Rows[Rowindex].FindControl("listProd")).SelectedValue, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtOutput")).Text, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtAmount1")).Text, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtAmount2")).Text, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtAmount3")).Text, "0" };
                opt.UpDateData(seg, value, "ht_prod_season_plan_Detail", " where MONTH_PLAN_ID = " + hidePlanID.Value + " and id = '" + mtr_code + "'");
            }

            else
            {
                string[] seg = { "prod_code", "TOTAL_OUTPUT", "plan_output_1", "plan_output_2", "plan_output_3", "QUARTER_PLAN_ID" };
                string[] value = { ((DropDownList)GridView2.Rows[Rowindex].FindControl("listProd")).SelectedValue, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtOutput")).Text, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtAmount1")).Text, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtAmount2")).Text, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtAmount3")).Text, hidePlanID.Value };
                opt.InsertData(seg, value, "ht_prod_season_plan_Detail");
            }
            bindGrid2(hidePlanID.Value);
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
}