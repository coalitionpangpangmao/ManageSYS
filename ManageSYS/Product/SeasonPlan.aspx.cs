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
        string query = "select distinct g.id,g.plan_name as 计划名, g.plan_year as 年份,g.quarter as 季度,g4.total as 计划总产量,'吨' as 单位,g1.name as 审批状态,g2.issue_name as 下发状态 ,g3.name as 编制人  from ht_prod_season_plan g  left join ht_inner_aprv_status g1 on g1.id = g.flow_status left join HT_INNER_BOOL_DISPLAY g2 on g2.id = g.issued_status left join ht_svr_user g3 on g3.id = g.create_id and g3.is_del = '0' left join (select sum(s.total_output) as total,r.id from ht_prod_season_plan r left join ht_prod_season_plan_detail s on s.quarter_plan_id = r.id  and s.is_del = '0' where r.is_del = '0' group by r.id) g4 on g4.id = g.id where g.is_del = '0'";
        if (txtYears.Text != "")
            query += " and g.plan_year = '" + txtYears.Text + "'";
        if (listSeason.SelectedValue != "")
            query += " and g.quarter = '" + listSeason.SelectedValue + "'";
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
        string query = "select r.prod_code as 产品,round(r.TOTAL_OUTPUT,3) as 计划数量,round(r.plan_output_1,3) as month1,round(r.plan_output_2,3) as month2,round(r.plan_output_3,3) as month3,r.id   from ht_prod_season_plan_Detail r  where r.is_del = '0' and  r.QUARTER_PLAN_ID = " + hidePlanID.Value + "order by r.id"; ;

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.UpDateOra("delete from ht_prod_season_plan_Detail where is_valid = '0'");
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null)
            datagrid2Bind(data.Tables[0]);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (!Regex.IsMatch(hidePlanID.Value, @"^[+-]?/d*$"))
            hidePlanID.Value = hidePlanID.Value.Substring(hidePlanID.Value.LastIndexOf(',') + 1);
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        foreach (GridViewRow row in GridView2.Rows)
        {
            if (!(((TextBox)row.FindControl("txtAmount1")).Text == "" && ((TextBox)row.FindControl("txtAmount2")).Text == "" && ((TextBox)row.FindControl("txtAmount3")).Text == ""))
            {
                if (((TextBox)row.FindControl("txtAmount1")).Text == "")
                    ((TextBox)row.FindControl("txtAmount1")).Text = "0";
                if (((TextBox)row.FindControl("txtAmount2")).Text == "")
                    ((TextBox)row.FindControl("txtAmount2")).Text = "0";
                if (((TextBox)row.FindControl("txtAmount3")).Text == "")
                    ((TextBox)row.FindControl("txtAmount3")).Text = "0";
                ((TextBox)row.FindControl("txtOutput")).Text = (Convert.ToDouble(((TextBox)row.FindControl("txtAmount1")).Text) + Convert.ToDouble(((TextBox)row.FindControl("txtAmount2")).Text) + Convert.ToDouble(((TextBox)row.FindControl("txtAmount3")).Text)).ToString("0.000");
                string[] seg = { "QUARTER_PLAN_ID", "ID", "prod_code", "TOTAL_OUTPUT", "plan_output_1", "plan_output_2", "plan_output_3", "IS_DEL" };
                string[] value = { hidePlanID.Value, GridView2.DataKeys[row.RowIndex].Value.ToString(), ((DropDownList)row.FindControl("listProd")).SelectedValue, ((TextBox)row.FindControl("txtOutput")).Text, ((TextBox)row.FindControl("txtAmount1")).Text, ((TextBox)row.FindControl("txtAmount2")).Text, ((TextBox)row.FindControl("txtAmount3")).Text, "0" };
                opt.MergeInto(seg, value, 2, "ht_prod_season_plan_Detail");
            }

        }

        string mtr_code = opt.GetSegValue("select seasondt_id_seq.nextval  as id from dual", "id");
        string[] subseg = { "QUARTER_PLAN_ID", "ID", "IS_VALID" };
        string[] subvalue = { hidePlanID.Value, mtr_code, "0" };
        opt.MergeInto(subseg, subvalue, 2, "ht_prod_season_plan_Detail");




        string query = "select  r.prod_code as 产品,round(r.TOTAL_OUTPUT,3) as 计划数量,round(r.plan_output_1,3) as month1,round(r.plan_output_2,3) as month2,round(r.plan_output_3,3) as month3,r.id   from ht_prod_season_plan_Detail r  where r.is_del = '0' and  r.QUARTER_PLAN_ID = " + hidePlanID.Value + "order by r.id";

        DataSet set = opt.CreateDataSetOra(query);

        if (set != null)
        {
            datagrid2Bind(set.Tables[0]);
        }


    }

    protected void datagrid2Bind(DataTable data)
    {
        GridView2.DataSource = data;
        GridView2.DataBind();
        if (data != null && data.Rows.Count > 0)
        {
            switch (listSeason2.SelectedValue)
            {
                case "1":
                    GridView2.HeaderRow.Cells[3].Text = "<table width='270px' align='center'><tr><td colspan='3' align='center'>生产月份</td></tr> <tr><td width='90px' align='center'>1月份</td><td width='90px' align='center'> 2月份 </td> <td width='90px' align='center'>3月份</td></tr></table>";
                    break;
                case "2":
                    GridView2.HeaderRow.Cells[3].Text = "<table width='270px' align='center'><tr><td colspan='3' align='center'>生产月份</td></tr> <tr><td width='90px' align='center'>4月份</td><td width='90px' align='center'> 5月份 </td> <td width='90px' align='center'>6月份</td></tr></table>";
                    break;
                case "3":
                    GridView2.HeaderRow.Cells[3].Text = "<table width='270px' align='center'><tr><td colspan='3' align='center'>生产月份</td></tr> <tr><td width='90px' align='center'>7月份</td><td width='90px' align='center'> 8月份 </td> <td width='90px' align='center'>9月份</td></tr></table>";
                    break;
                case "4":
                    GridView2.HeaderRow.Cells[3].Text = "<table width='270px' align='center'><tr><td colspan='3' align='center'>生产月份</td></tr> <tr><td width='90px' align='center'>10月份</td><td width='90px' align='center'> 11月份 </td> <td width='90px' align='center'>12月份</td></tr></table>";
                    break;
                default: break;
            }
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
    public DataSet ddlbind()
    {
        string sqlstr = "select prod_name as 产品规格,prod_code from ht_pub_prod_design where is_valid = '1' and is_del  = '0' order by Prod_code DESC";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
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
        int rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string id = GridView1.DataKeys[rowindex].Value.ToString();

        List<String> commandlist = new List<String>();
        commandlist.Add("update ht_prod_Season_plan set IS_DEL = '1'  where ID = '" + id + "'");
        commandlist.Add("update ht_prod_season_plan_detail set is_del = '1' where QUARTER_PLAN_ID =  '" + id + "'");
        commandlist.Add("delete from ht_pub_aprv_flowinfo where BUSIN_ID = '" + id + "'");
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string log_message = opt.TransactionCommand(commandlist) == "Success" ? "删除季度生产任务成功" : "删除季度生产任务失败";
        log_message += "--标识:" + id;
        InsertTlog(log_message);
        bindGrid1();
    }
    protected void SetEnable(bool status, string adjust)
    {
        btnAdd.Visible = status;
        btnDelSel.Visible = status;
        btnGrid2Modify.Visible = status;
        btnModify.Visible = status;
        if (GridView2.Columns.Count == 6)
        {
            GridView2.Columns[4].Visible = status;
            GridView2.Columns[5].Visible = status;
        }
        hideAdjust.Value = adjust;
    }
    protected void btnGridEdit_Click(object sender, EventArgs e)//编制计划
    {
        Button btn = (Button)sender;
        int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号  
        string id = GridView1.DataKeys[Rowindex].Value.ToString();
        txtYear.Text = GridView1.Rows[Rowindex].Cells[2].Text;
        listSeason2.SelectedValue = GridView1.Rows[Rowindex].Cells[3].Text;

        string aprvstatus = ((Label)GridView1.Rows[Rowindex].FindControl("labAprv")).Text;
        bindGrid2(id);
        if (aprvstatus == "未提交")
            SetEnable(true, "0");
        else
            SetEnable(false,"0");
       

    }
    protected void btnGridAlter_Click(object sender, EventArgs e)//调整计划
    {
        try
        {
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号  
            string id = GridView1.DataKeys[Rowindex].Value.ToString();
            txtYear.Text = GridView1.Rows[Rowindex].Cells[2].Text;
            listSeason2.SelectedValue = GridView1.Rows[Rowindex].Cells[3].Text;

            string aprvstatus = ((Label)GridView1.Rows[Rowindex].FindControl("labAprv")).Text;
            bindGrid2(id);
          
                SetEnable(true,"1");
           
          

        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void btnAddPlan_Click(object sender, EventArgs e)//新增计划
    {
        SetEnable(true,"0");     
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
       
    }

    protected void btnSubmit_Click(object sender, EventArgs e)//提交审批
    {
        try
        {
            Button btn = (Button)sender;
            int index = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号                 
            string id = GridView1.DataKeys[index].Value.ToString();
            /*启动审批TB_ZT标题,TBR_ID填报人id,TBR_NAME填报人name,TB_BM_ID填报部门id,TB_BM_NAME填报部门name,TB_DATE申请时间创建日期,MODULENAME审批类型编码,URL 单独登录url,BUSIN_ID业务数据id*/
            string[] subvalue = { GridView1.Rows[index].Cells[1].Text, "12", id, Page.Request.UserHostName.ToString() };
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



    protected void btnModify_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string planname = txtYear.Text + "-" + listSeason2.SelectedValue + "季度生产计划";

        opt.UpDateOra("delete from  HT_PROD_SEASON_PLAN   where plan_name = '" + planname + "' and  is_del = '0'");
        string[] seg = { "PLAN_YEAR", "QUARTER", "PLAN_NAME", "CREATE_ID", "CREATE_TIME", "REMARK" };
        string[] value = { txtYear.Text, listSeason2.SelectedValue, planname, ((MSYS.Data.SysUser)Session["User"]).id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), txtRemark.Text };
        string log_message = opt.InsertData(seg, value, "HT_PROD_SEASON_PLAN") == "Success" ? "新增季度生产计划成功" : "新增季度生产计划失败";
        log_message += "--详情:" + string.Join(",", value);
        InsertTlog(log_message);
        hidePlanID.Value = opt.GetSegValue("select * from HT_PROD_SEASON_PLAN   where plan_name = '" + planname + "' and  is_del = '0'", "ID");
        bindGrid1();

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
                    string mtr_code = GridView2.DataKeys[i].Value.ToString();
                    string query = "update HT_PROD_SEASON_PLAN_DETAIL set IS_DEL = '1'  where id = '" + mtr_code + "'";
                    commandlist.Add(query);
                    planlist += mtr_code + ",";                   
                }   
            }
            if (commandlist.Count > 0)
            {
                if (hideAdjust.Value == "1")
                    commandlist.Add("update HT_PROD_SEASON_PLAN set ADJUST_STATUS = '1',MODIFY_ID = '" + ((MSYS.Data.SysUser)Session["User"]).id + "' where ID = '" + hidePlanID.Value + "'");
                string log_message = opt.TransactionCommand(commandlist) == "Success" ? "删除季度生产计划明细成功" : "删除季度生产计划明细失败";
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
            List<string> commandlist = new List<string>();
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号             
            string mtr_code = GridView2.DataKeys[Rowindex].Value.ToString();
            commandlist.Add( "update HT_PROD_SEASON_PLAN_DETAIL set IS_DEL = '1'  where id = '" + mtr_code + "'");
            if (hideAdjust.Value == "1")
                commandlist.Add("update HT_PROD_SEASON_PLAN set ADJUST_STATUS = '1',MODIFY_ID = '" + ((MSYS.Data.SysUser)Session["User"]).id + "' where ID = '" + hidePlanID.Value + "'");
            string log_message = opt.TransactionCommand(commandlist) == "Success" ? "删除季度生产计划明细成功" : "删除季度生产计划明细失败";
            log_message += "--标识:" + ID;
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
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int Rowindex = row.RowIndex;//获得行号             
            string mtr_code = GridView2.DataKeys[Rowindex].Value.ToString();
            if (!Regex.IsMatch(hidePlanID.Value, @"^[+-]?/d*$"))
                hidePlanID.Value = hidePlanID.Value.Substring(hidePlanID.Value.LastIndexOf(',') + 1);

            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

            string log_message;

            if (!(((TextBox)row.FindControl("txtAmount1")).Text == "" && ((TextBox)row.FindControl("txtAmount2")).Text == "" && ((TextBox)row.FindControl("txtAmount3")).Text == ""))
            {
                if (((TextBox)row.FindControl("txtAmount1")).Text == "")
                    ((TextBox)row.FindControl("txtAmount1")).Text = "0";
                if (((TextBox)row.FindControl("txtAmount2")).Text == "")
                    ((TextBox)row.FindControl("txtAmount2")).Text = "0";
                if (((TextBox)row.FindControl("txtAmount3")).Text == "")
                    ((TextBox)row.FindControl("txtAmount3")).Text = "0";
                ((TextBox)row.FindControl("txtOutput")).Text = (Convert.ToDouble(((TextBox)row.FindControl("txtAmount1")).Text) + Convert.ToDouble(((TextBox)row.FindControl("txtAmount2")).Text) + Convert.ToDouble(((TextBox)row.FindControl("txtAmount3")).Text)).ToString("0.000");
                string[] seg = { "QUARTER_PLAN_ID", "ID", "prod_code", "TOTAL_OUTPUT", "plan_output_1", "plan_output_2", "plan_output_3", "IS_DEL", "IS_VALID" };
                string[] value = { hidePlanID.Value, GridView2.DataKeys[row.RowIndex].Value.ToString(), ((DropDownList)row.FindControl("listProd")).SelectedValue, ((TextBox)row.FindControl("txtOutput")).Text, ((TextBox)row.FindControl("txtAmount1")).Text, ((TextBox)row.FindControl("txtAmount2")).Text, ((TextBox)row.FindControl("txtAmount3")).Text, "0", "1" };

                log_message = opt.MergeInto(seg, value, 2, "ht_prod_season_plan_Detail") == "Success" ? "保存季度生产计划明细成功" : "保存季度生产计划明细失败";
                log_message += "--详情:" + string.Join(",", value);
                InsertTlog(log_message);
                if (hideAdjust.Value == "1")
                    opt.UpDateOra("update HT_PROD_SEASON_PLAN set ADJUST_STATUS = '1',MODIFY_ID = '" + ((MSYS.Data.SysUser)Session["User"]).id + "' where ID = '" + hidePlanID.Value + "'");
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
                string mtr_code = GridView2.DataKeys[Rowindex].Value.ToString();
                if (!Regex.IsMatch(hidePlanID.Value, @"^[+-]?/d*$"))
                    hidePlanID.Value = hidePlanID.Value.Substring(hidePlanID.Value.LastIndexOf(',') + 1);                string log_message;
                if (!(((TextBox)row.FindControl("txtAmount1")).Text == "" && ((TextBox)row.FindControl("txtAmount2")).Text == "" && ((TextBox)row.FindControl("txtAmount3")).Text == ""))
                {
                    if (((TextBox)row.FindControl("txtAmount1")).Text == "")
                        ((TextBox)row.FindControl("txtAmount1")).Text = "0";
                    if (((TextBox)row.FindControl("txtAmount2")).Text == "")
                        ((TextBox)row.FindControl("txtAmount2")).Text = "0";
                    if (((TextBox)row.FindControl("txtAmount3")).Text == "")
                        ((TextBox)row.FindControl("txtAmount3")).Text = "0";
                    ((TextBox)row.FindControl("txtOutput")).Text = (Convert.ToDouble(((TextBox)row.FindControl("txtAmount1")).Text) + Convert.ToDouble(((TextBox)row.FindControl("txtAmount2")).Text) + Convert.ToDouble(((TextBox)row.FindControl("txtAmount3")).Text)).ToString("0.000");
                    string[] seg = { "QUARTER_PLAN_ID", "ID", "prod_code", "TOTAL_OUTPUT", "plan_output_1", "plan_output_2", "plan_output_3", "IS_DEL", "IS_VALID" };
                    string[] value = { hidePlanID.Value, GridView2.DataKeys[row.RowIndex].Value.ToString(), ((DropDownList)row.FindControl("listProd")).SelectedValue, ((TextBox)row.FindControl("txtOutput")).Text, ((TextBox)row.FindControl("txtAmount1")).Text, ((TextBox)row.FindControl("txtAmount2")).Text, ((TextBox)row.FindControl("txtAmount3")).Text, "0", "1" };

                    log_message = opt.MergeInto(seg, value, 2, "ht_prod_season_plan_Detail") == "Success" ? "保存季度生产计划明细成功" : "保存季度生产计划明细失败";
                    log_message += "--详情:" + string.Join(",", value);
                    InsertTlog(log_message);
                }    
              
            }
            if (hideAdjust.Value == "1")
                opt.UpDateOra("update HT_PROD_SEASON_PLAN set ADJUST_STATUS = '1',MODIFY_ID = '" + ((MSYS.Data.SysUser)Session["User"]).id + "' where ID = '" + hidePlanID.Value + "'");
            bindGrid2(hidePlanID.Value);
            bindGrid1();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

}