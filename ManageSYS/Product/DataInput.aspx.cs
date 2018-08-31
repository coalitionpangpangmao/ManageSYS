using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Product_DataInput : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            bindGrid();
            initView();
        }

    }
    protected void initView()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        txtRecordtime.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
        opt.bindDropDownList(listProd, "select prod_code,prod_name from ht_pub_prod_design where is_del = '0' and is_valid = '1' order by prod_code", "prod_name", "prod_code");
        opt.bindDropDownList(listProd2, "select prod_code,prod_name from ht_pub_prod_design where is_del = '0' and is_valid = '1' order by prod_code", "prod_name", "prod_code");
        opt.bindDropDownList(listTeam, "select team_code,team_name from ht_sys_team where is_del = '0' and is_valid = '1' order by team_code", "team_name", "team_code");
        opt.bindDropDownList(listTeam2, "select team_code,team_name from ht_sys_team where is_del = '0' and is_valid = '1' order by team_code", "team_name", "team_code");
        opt.bindDropDownList(listPara, "select para_code,para_name  from ht_pub_tech_para where is_del = '0' and is_valid = '1' and para_type like '____1%' order by para_code", "para_name", "para_code");
    }
    protected void bindGrid()
    {
        string query = "select r.rowid, r.plan_no as 计划号,t.prod_name as 产品,q.para_name as 记录项目,r.value as 记录值,s.team_name as 班组,r.create_time as 记录时间,r.creator as 记录人 from ht_prod_manual_record r left join ht_pub_prod_design t on t.prod_code = r.prod_code left join ht_sys_team s on s.team_code = r.team left join ht_pub_tech_para q on q.para_code = r.para_code where r.is_del = '0'";
        
            if(listProd.SelectedValue != "" )
                query += " and r.prod_code = '" + listProd.SelectedValue + "'";
        if(listTeam.SelectedValue != "")
            query += " and r.team = '" + listTeam.SelectedValue + "'";
        if(txtRecordtime.Text != "")
            query += " and r.create_time = '" + txtRecordtime.Text + "'";
      
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();       

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

        bindGrid();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid();

    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        setBlank();
        txtCreator.Text = ((MSYS.Data.SysUser)Session["User"]).text;        
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", " $('.shade').fadeIn(200);", true);

    }

    protected void setBlank()
    {
        listProd2.SelectedValue = listProd.SelectedValue;
        listTeam2.SelectedValue = listTeam.SelectedValue;
        listPara.SelectedValue = "";
        txtPlanno.Text = "";
        txtValue.Text = "";       
        
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        if (txtPlanno.Text.Length == 17 && txtPlanno.Text.Substring(8, 7) == listProd2.SelectedValue)
        {
            string[] seg = { "PLAN_NO", "PROD_CODE", "PARA_CODE", "VALUE", "TEAM", "CREATOR", "CREATE_TIME" };
            string[] value = { txtPlanno.Text, listProd2.SelectedValue, listPara.SelectedValue, txtValue.Text, listTeam2.SelectedValue, txtCreator.Text, System.DateTime.Now.ToString("yyyy-MM-dd") };
            string log_message = opt.InsertData(seg, value, "HT_PROD_MANUAL_RECORD") == "Success" ? "保存物料过程数据成功," : "保存物料过程数据失败,";
            log_message += "记录：" + string.Join(" ", value);
            InsertTlog(log_message);
            bindGrid();
        }
        else
            ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "message", "alert('产品或计划号输入有误，请重新输入')", true);

    }

    protected void btnCkAll_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                ((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked = true;
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
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked)
                {
                    string rowid = GridView1.DataKeys[i].Value.ToString();
                    string query = "update ht_prod_manual_record set IS_DEL = '1'  where rowid = '" + rowid + "'";
                    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                    string log_message = opt.UpDateOra(query) == "Success" ? "删除人工录入记录成功" : "删除人工录入记录失败";
                    log_message += "标识:" + rowid;
                    InsertTlog(log_message);
                }
            }
            bindGrid();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }





}