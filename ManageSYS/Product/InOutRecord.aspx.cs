using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Product_InoutRecord : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        { 
            initView();
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid();
      
    }
    protected void initView()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        txtRecordtime.Text = System.DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
        txtEndtime.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
        opt.bindDropDownList(listProd, "select prod_code,prod_name from ht_pub_prod_design where is_del = '0' and is_valid = '1' order by prod_code", "prod_name", "prod_code");
        opt.bindDropDownList(listProd2, "select prod_code,prod_name from ht_pub_prod_design where is_del = '0' and is_valid = '1' order by prod_code", "prod_name", "prod_code");
        opt.bindDropDownList(listTeam, "select team_code,team_name from ht_sys_team where is_del = '0' and is_valid = '1' order by team_code", "team_name", "team_code");
        opt.bindDropDownList(listTeam2, "select team_code,team_name from ht_sys_team where is_del = '0' and is_valid = '1' order by team_code", "team_name", "team_code");
        bindGrid();         

    }

    public DataSet bindpara()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select para_code,para_name  from ht_pub_tech_para where is_del = '0' and is_valid = '1' and para_type like '________1%' order by para_code");
    }
    protected void bindGrid()
    {
        string query = "select r.prod_name as 产品,t.prod_date as 当班时间,s.team_name as 班组,o.para_name as 记录项目,t.para_value as 记录值,t.record_time as 记录时间,p.name as 记录人,t.rowid  from ht_prod_inout_report t left join ht_pub_prod_design r on r.prod_code = t.prod_code left join ht_sys_team s on s.team_code = t.team left join ht_pub_tech_para o on o.para_code = t.para_code left join Ht_Svr_User p on p.id = t.editor";

        //if (txtRecordtime.Text != "" && txtEndtime.Text != "")
            query += " where t.prod_date between '" + txtRecordtime.Text + "' and '" + txtEndtime.Text + "'";
        if (listProd.SelectedValue != "")
            query += " and t.prod_code = '" + listProd.SelectedValue + "'";
        if (listTeam.SelectedValue != "")
            query += " and t.team = '" + listTeam.SelectedValue + "'";
       

        query += " order by t.para_code,t.prod_date";

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();

    }

 

    protected void bindGrid2()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select t.para_code,to_char( t.para_value ) as para_value from ht_prod_inout_report t where t.prod_code ='" + listProd2.SelectedValue + "' and t.team = '" + listTeam2.SelectedValue + "'  and t.prod_date = '" + txtDate.Text + "'  union select r.para_code,'' as seg_value from ht_pub_tech_para r where r.para_code in ( select para_code from ht_pub_tech_para  where para_type like '________1%'  and is_del = '0' minus select para_code from ht_prod_inout_report  where prod_code ='" + listProd2.SelectedValue + "' and team = '" + listTeam2.SelectedValue + "'  and prod_date = '" + txtDate.Text + "' ) order by para_code";
       
        DataSet data = opt.CreateDataSetOra(query);

        if (data != null && data.Tables[0].Rows.Count > 0)
        {
           
                GridView2.DataSource = data;
                GridView2.DataBind();
                int i = 0;
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    ((DropDownList)GridView2.Rows[i].FindControl("listPara")).SelectedValue = row["para_code"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtParavalue")).Text = row["para_value"].ToString();
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

   

    protected void setBlank()
    {
        listProd2.SelectedValue = "";
        listTeam2.SelectedValue = "";      

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        bindGrid2();
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        List<string> commandlist = new List<string>();
       
            if (listProd2.SelectedValue == "" ||   listTeam2.SelectedValue == "" || txtDate.Text == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "message", "alert('请录入完整的数据信息')", true);
                return;
            }
              
            else
            {
                MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
                string[] seg = { "PROD_CODE", "PROD_DATE", "TEAM", "PARA_CODE", "PARA_VALUE", "RECORD_TIME", "EDITOR" };
                foreach (GridViewRow row in GridView2.Rows)
                {
                    string paravalue = ((TextBox)row.FindControl("txtParavalue")).Text;
                    string paracode = ((DropDownList)row.FindControl("listPara")).SelectedValue;
                    if (((TextBox)row.FindControl("txtParavalue")).Text != "")
                    {
                        string[] value = { listProd2.SelectedValue, txtDate.Text, listTeam2.SelectedValue, paracode, paravalue,System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), user.id, };
                        commandlist.Add(opt.getMergeStr(seg, value, 4, "HT_PROD_INOUT_REPORT"));
                    }
                }
            }



            string log_message = opt.TransactionCommand(commandlist) == "Success" ? "保存投入产出人工录入数据成功," : "保存投入产出人工录入数据失败,";
        log_message += "--详情产品：" + listProd2.SelectedValue + listProd2.SelectedItem.Text +  "班组：" + listTeam.SelectedValue + "日期：" + txtDate.Text;
        InsertTlog(log_message);
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", "alert('" + log_message + "'); ", true);
        bindGrid();
       




    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {      
        setBlank();
        bindGrid2();
   
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", " $('.shade').fadeIn(200);", true);
    }

    protected void ListProd2_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGrid2();
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
                    string query = "delete from HT_PROD_INOUT_REPORT   where rowid = '" + rowid + "'";
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


   
  
   

}