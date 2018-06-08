using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
public partial class Product_Plan : System.Web.UI.Page
{  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindGrid1();
           
        }
 
    }
    protected void bindGrid1()
    {
        string query = "select id, plan_year as 年份,quarter as 季度,TOTAL_OUTPUT as 计划总产量,UNIT as 单位,(case FLOW_STATUS when '-1' then '未提交' when '0' then '办理中' when '1' then '未通过' else '己通过' end) as 审批状态,case ISSUED_STATUS when '0' then '未下发' when '1' then '己下发' end  as 下发状态 ,creator as 编制人  from ht_prod_season_plan where is_del = '0'";
        if (txtYears.Text != "")
            query += " and plan_year = '" + txtYears.Text + "'";
        if (listSeason.SelectedValue != "")
            query += " and quarter = '" + listSeason.SelectedValue + "'";           
       DataBaseOperator opt =new DataBaseOperator();
        GridView1.DataSource = opt.CreateDataSetOra(query); ;
        GridView1.DataBind();

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
       
       DataBaseOperator opt =new DataBaseOperator();
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
        string sqlstr = "select prod_name as 产品规格,prod_code from ht_pub_prod_design where is_valid = '1' and is_del  = '0'";
       DataBaseOperator opt =new DataBaseOperator();
        return opt.CreateDataSetOra(sqlstr);
    }
    protected void btnIssued_Click(object sender, EventArgs e)//下发计划
    {
        try
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked)
                {
                    string id = GridView1.DataKeys[i].Value.ToString();
                    string aprv = GridView1.Rows[i].Cells[5].Text;
                    if (aprv == "己通过")
                    {
                        string query = "update ht_prod_Season_plan set ISSUED_STATUS = '1'  where ID = '" + id + "'";
                       DataBaseOperator opt =new DataBaseOperator();
                        string log_message = opt.UpDateOra(query)== "Success" ? "生产计划下发成功":"生产计划下发失败";
                        log_message += ",计划ID:" + id;
                        opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "alert('请通过审批后再下发');", true);
                    }
                }
            }
            bindGrid1();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnGridDel_Click(object sender, EventArgs e)//计划删除, 此处应该使用事物进行操作
    {
        try
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked)
                {
                    string id = GridView1.DataKeys[i].Value.ToString();
                    string query = "update ht_prod_Season_plan set IS_DEL = '1'  where ID = '" + id + "'";
                   DataBaseOperator opt =new DataBaseOperator();
                    opt.UpDateOra(query);
                    opt.UpDateOra("update ht_prod_season_plan_detail set is_del = '1' where QUARTER_PLAN_ID =  '" + id + "'");
                }
            }
            bindGrid1();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnGridEdit_Click(object sender, EventArgs e)//编制计划
    {
        try
        {
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号  
            string id = GridView1.DataKeys[Rowindex].Value.ToString();
            txtYear.Text = GridView1.Rows[Rowindex].Cells[1].Text;
            listSeason2.SelectedValue = GridView1.Rows[Rowindex].Cells[2].Text;    
            bindGrid2(id);
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "GridClick();", true);
        
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
       DataBaseOperator opt =new DataBaseOperator();
        GridView3.DataSource = opt.CreateDataSetOra(query);
        GridView3.DataBind();
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "Aprvlist();", true);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)//提交审批
    {
        try
        {
            Button btn = (Button)sender;
            int index =  ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号                 
            string id = GridView1.DataKeys[index].Value.ToString();
                    /*启动审批TB_ZT标题,TBR_ID填报人id,TBR_NAME填报人name,TB_BM_ID填报部门id,TB_BM_NAME填报部门name,TB_DATE申请时间创建日期,MODULENAME审批类型编码,URL 单独登录url,BUSIN_ID业务数据id*/
            string[] subvalue = { GridView1.Rows[index].Cells[1].Text, "12", id, Page.Request.UserHostName.ToString() };
           DataBaseOperator opt =new DataBaseOperator();
            if (opt.createApproval(subvalue))
            {
                string log_message = opt.UpDateOra("update ht_prod_Season_plan set FLOW_STATUS = '0'  where ID = '" + id + "'")== "Success" ? "提交审批成功":"提交审批失败";
                log_message += ",生产计划ID：" + id;
                opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
            }
            
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
           DataBaseOperator opt =new DataBaseOperator();
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
            object[] value = { "", 0, 0, 0,0,0 };
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
    protected void btnModify_Click(object sender, EventArgs e)//此处应该使用事物
    {
       DataBaseOperator opt =new DataBaseOperator();
        string planname = txtYear.Text + "-" + listSeason2.SelectedValue + "生产计划";
       
      opt.UpDateOra("delete from  HT_PROD_SEASON_PLAN   where plan_name = '" + planname + "' and  is_del = '0'");
      string[] seg = { "PLAN_YEAR", "QUARTER", "PLAN_NAME",  "CREATE_ID    ", "CREATOR", "CREATE_TIME" };
      string[] value = { txtYear.Text, listSeason2.SelectedValue, planname ,  "cookieID", "cookieNAME", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
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
                   DataBaseOperator opt =new DataBaseOperator();
                    string log_message = opt.UpDateOra(query) == "Success" ? "删除生产计划成功":"删除生产计划失败";
                    log_message += ",生产任务ID：" + mtr_code;
                    opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
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
           DataBaseOperator opt =new DataBaseOperator();
            string log_message = opt.UpDateOra(query)== "Success" ? "删除生产计划成功":"删除生产计划失败";
            log_message += ",生产任务ID：" + mtr_code;
            opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
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
           DataBaseOperator opt =new DataBaseOperator();
            DataSet data = opt.CreateDataSetOra(query);
            if(data!= null && data.Tables[0].Rows.Count > 0)
            {
                string[] seg = { "prod_code","TOTAL_OUTPUT", "plan_output_1", "plan_output_2", "plan_output_3" ,"IS_DEL"};
                string[] value = { ((DropDownList)GridView2.Rows[Rowindex].FindControl("listProd")).SelectedValue, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtOutput")).Text, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtAmount1")).Text, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtAmount2")).Text, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtAmount3")).Text, "0" };
                string log_message = opt.UpDateData(seg, value, "ht_prod_season_plan_Detail", " where MONTH_PLAN_ID = " + hidePlanID.Value + " and id = '" + mtr_code + "'") == "Success" ? "保存生产计划成功":"保存生产计划失败";
                log_message += ",参数:" + string.Join(" ", value);
                opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
            }         
          
            else
            {
                string[] seg = { "prod_code", "TOTAL_OUTPUT", "plan_output_1", "plan_output_2", "plan_output_3", "QUARTER_PLAN_ID" };
                string[] value = { ((DropDownList)GridView2.Rows[Rowindex].FindControl("listProd")).SelectedValue, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtOutput")).Text, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtAmount1")).Text, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtAmount2")).Text, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtAmount3")).Text, hidePlanID.Value };
                string log_message = opt.InsertData(seg, value, "ht_prod_season_plan_Detail") == "Success" ? "保存生产计划成功":"保存生产计划失败";
                log_message += ",参数;" + string.Join(" ", value);
            }
            bindGrid2(hidePlanID.Value);
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }





  
}