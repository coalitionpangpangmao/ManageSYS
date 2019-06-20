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
            txtRecordtime.Text = System.DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            txtEndtime.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            bindGrid();
            bindGrid3();
            initView();
        }

    }
    protected void initView()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        opt.bindDropDownList(listProd, "select prod_code,prod_name from ht_pub_prod_design where is_del = '0' and is_valid = '1' order by prod_code", "prod_name", "prod_code");
        opt.bindDropDownList(listProd2, "select prod_code,prod_name from ht_pub_prod_design where is_del = '0' and is_valid = '1' order by prod_code", "prod_name", "prod_code");
        opt.bindDropDownList(listTeam, "select team_code,team_name from ht_sys_team where is_del = '0' and is_valid = '1' order by team_code", "team_name", "team_code");
        opt.bindDropDownList(listTeam2, "select team_code,team_name from ht_sys_team where is_del = '0' and is_valid = '1' order by team_code", "team_name", "team_code");

    }

    public DataSet bindpara()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select para_code,para_name  from ht_pub_tech_para where is_del = '0' and is_valid = '1' and para_type like '____1%' order by para_code");
    }
    protected void bindGrid()
    {
        string query = "select r.rowid, r.planno as 计划号,t.prod_name as 产品,q.para_name as 记录项目,round(r.value,0) as 记录值,s.team_name as 班组,r.time as 记录时间,r.creator as 记录人 from HT_PROD_REPORT_DETAIL r left join ht_pub_prod_design t on t.prod_code = r.prod_code left join ht_sys_team s on s.team_code = r.team left join ht_pub_tech_para q on q.para_code = r.para_code where r.is_del = '0'";

        if (listProd.SelectedValue != "")
            query += " and r.prod_code = '" + listProd.SelectedValue + "'";
        if (listTeam.SelectedValue != "")
            query += " and r.team = '" + listTeam.SelectedValue + "'";
        if (txtRecordtime.Text != "" && txtEndtime.Text != "")
            query += " and substr(r.time,0,10) between '" + txtRecordtime.Text + "' and '" + txtEndtime.Text + "'";

        query += " order by r.time";

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();

    }

    protected void bindGrid3()
    {
        string query = "select r.seg_name,r.section_code, s.planno as 计划号, t.prod_name as 产品,r.seg_info as 记录项目,s.seg_value as 记录值 from ht_inner_report_contrast r left join hv_prod_report s on r.section_code = s.section_code and r.seg_name = s.seg left join ht_pub_prod_design t on t.prod_code = substr(s.planno,9,7) where s.seg_value is not null";

        if (listProd.SelectedValue != "")
            query += " and substr(s.planno,9,7) = '" + listProd.SelectedValue + "'";
        if (txtRecordtime.Text != "" && txtEndtime.Text != "")
            query += " and substr(s.starttime,0,10) between '" + txtRecordtime.Text + "' and '" + txtEndtime.Text + "'";
        query += " order by s.planno";


        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView3.DataSource = data;
        GridView3.DataBind();
    }

    protected void bindGrid2()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = " select r.para_code,r.value AS SEG_VALUE,r.rowid as id  from ht_prod_report_detail r where r.planno = '" + listPlanno.SelectedValue + "' and r.team = '" + listTeam2.SelectedValue + "' and r.time = '" + txtDate.Text + "' and r.prod_code = '" + listProd2.SelectedValue + "'  union select r.para_code,null as seg_value,r.rowid as id   from ht_pub_tech_para r where r.para_code in ( select para_code from ht_pub_tech_para  where r.para_type like '____1%' and is_del = '0' minus select para_code from ht_prod_report_detail where planno =  '" + listPlanno.SelectedValue + "' and team = '" + listTeam2.SelectedValue + "' and time = '" + txtDate.Text + "' and prod_code = '" + listProd2.SelectedValue + "') order by para_code";

        if (rd2.Checked)
        {
            query = "select r.para_code,t.seg_value,s.seg_name as id  from ht_pub_tech_para r left join ht_inner_report_contrast s on s.para_code = r.para_code left join hv_prod_report t on t.section_code = s.section_code and s.seg_name = t.seg and t.planno = '" + listPlanno.SelectedValue + "' where r.para_type like '____1%' and r.is_del = '0' order by r.para_code";
        }
        DataSet data = opt.CreateDataSetOra(query);

        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            GridView2.DataSource = data;
            GridView2.DataBind();
            int i = 0;
            foreach (DataRow row in data.Tables[0].Rows)
            {
                ((DropDownList)GridView2.Rows[i].FindControl("listPara")).SelectedValue = row["para_code"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtParavalue")).Text = row["seg_value"].ToString();
                i++;
            }
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

        bindGrid();
    }

    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
        bindGrid3();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid();
        bindGrid3();
    }




    protected void setBlank()
    {
        listProd2.SelectedValue = "";
        listTeam2.SelectedValue = "";
        listPlanno.SelectedValue = "";

    }

    protected void ListProd2_SelectedIndexChanged(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listPlanno, "select distinct planno from ht_prod_report  where prod_code = '" + listProd2.SelectedValue + "' order by planno desc", "planno", "planno");
    }

 

    protected void btnView_Click(object sender, EventArgs e)
    {
        bindGrid2();
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        List<string> commandlist = new List<string>();
        if (rd1.Checked)
        {
            if (listProd2.SelectedValue == "" || listPlanno.SelectedValue == "" || listTeam2.SelectedValue == "" || txtDate.Text == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "message", "alert('请录入完整的数据信息')", true);

                return;
            }
            else
            {
                MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
                string[] seg = { "PLANNO", "PROD_CODE", "SECTION_CODE", "TEAM", "TIME", "PARA_CODE", "CREATOR", "VALUE" };
                foreach (GridViewRow row in GridView2.Rows)
                {
                    string paravalue = ((TextBox)row.FindControl("txtParavalue")).Text;
                    string paracode = ((DropDownList)row.FindControl("listPara")).SelectedValue;
                    if (((TextBox)row.FindControl("txtParavalue")).Text != "")
                    {
                        string[] value = { listPlanno.SelectedValue, listProd2.SelectedValue, paracode.Substring(0, 5), listTeam2.SelectedValue, txtDate.Text, paracode, user.text, paravalue };
                        commandlist.Add(opt.getMergeStr(seg, value, 6, "HT_PROD_REPORT_DETAIL"));
                    }
                }
            }
        }
        else
        {
            if (listProd2.SelectedValue == "" || listPlanno.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "message", "alert('请录入完整的数据信息')", true);

                return;
            }
            else
            {
                foreach (GridViewRow row in GridView2.Rows)
                {
                    string paravalue = ((TextBox)row.FindControl("txtParavalue")).Text;
                    string paracode = ((DropDownList)row.FindControl("listPara")).SelectedValue;
                    string[] seg = { "SECTION_CODE", "PLANNO", GridView2.DataKeys[row.RowIndex].Value.ToString() };
                    string[] value = { paracode.Substring(0, 5), listPlanno.SelectedValue, paravalue };
                    commandlist.Add(opt.getMergeStr(seg, value, 2, "HT_PROD_REPORT"));
                }
            }
        }

        string log_message = opt.TransactionCommand(commandlist) == "Success" ? "保存物料过程数据成功," : "保存物料过程数据失败,";
        log_message += "--详情产品：" + listProd2.SelectedValue + "计划号:" + listPlanno.SelectedValue + "班组：" + listTeam.SelectedValue + "日期：" + txtDate.Text;
        InsertTlog(log_message);
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", " $('.shade').fadeOut(100);", true);
        bindGrid();
        bindGrid3();




    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        rd1.Checked = true;
        rd2.Checked = false;
        setBlank();
        bindGrid2();
   
            listTeam2.Enabled = true;
            listTeam2.CssClass = "drpdwnlist";
        txtDate.Enabled = true;
     //   txtDate.Visible = true;
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", " $('.shade').fadeIn(200);", true);
    }
    protected void btnCkAll_Click(object sender, EventArgs e)
    {
        int ckno = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked)
                ckno++;
        }
        bool check = (ckno < GridView1.Rows.Count);
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            ((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked = check;

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
                    string query = "delete from HT_PROD_REPORT_DETAIL   where rowid = '" + rowid + "'";
                    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                    string log_message = opt.UpDateOra(query) == "Success" ? "删除人工录入记录成功" : "删除人工录入记录失败";
                    log_message += "--标识:" + rowid;
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


    protected void btnAdd2_Click(object sender, EventArgs e)
    {
        rd1.Checked = false;
        rd2.Checked = true;
        setBlank();
        bindGrid2();
        listTeam2.CssClass = "disableinput";
        listTeam2.Enabled = false;
     //   listTeam2.Visible = false;
        txtDate.Enabled = false;
     //   txtDate.Visible = true;
        ScriptManager.RegisterStartupScript(UpdatePanel3, this.Page.GetType(), "", " $('.shade').fadeIn(200);", true);
    }
    protected void btnCkAll2_Click(object sender, EventArgs e)
    {
        int ckno = 0;
        for (int i = 0; i < GridView3.Rows.Count; i++)
        {
            if (((CheckBox)GridView3.Rows[i].FindControl("chk")).Checked)
                ckno++;
        }
        bool check = (ckno < GridView3.Rows.Count);
        for (int i = 0; i < GridView3.Rows.Count; i++)
        {
            ((CheckBox)GridView3.Rows[i].FindControl("chk")).Checked = check;

        }


    }
    protected void btnDelSel2_Click(object sender, EventArgs e)
    {
        try
        {
            foreach(GridViewRow row in GridView3.Rows)
            {
                if (((CheckBox)row.FindControl("chk")).Checked)
                {
                    int index = row.RowIndex;
                    string planno = GridView3.DataKeys[index].Values[0].ToString();
                    string section = GridView3.DataKeys[index].Values[1].ToString();
                    string seg = GridView3.DataKeys[index].Values[2].ToString();
                    string query = "update  HT_PROD_REPORT set " + seg + " = ''  where PLANNO = '" + planno + "' and SECTION_CODE = '" + section + "'";
                    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                    string log_message = opt.UpDateOra(query) == "Success" ? "删除人工录入记录成功" : "删除人工录入记录失败";
                    log_message += "--标识--计划号:" + planno + "工艺段:" + section + "字段:" +  seg;
                    InsertTlog(log_message);
                }
            }        
            bindGrid3();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }


}