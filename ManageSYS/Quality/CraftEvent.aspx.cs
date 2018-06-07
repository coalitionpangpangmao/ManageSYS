using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Quality_CraftEvent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStartTime.Text = System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 00:00:00";
            txtEndTime.Text = System.DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 00:00:00";
           DataBaseOperator opt =new DataBaseOperator();
            opt.bindDropDownList(listpara, "select para_code,para_name from ht_pub_tech_para where para_type like '____1%' and is_del = '0' and is_valid = '1' order by para_code", "para_name", "para_code");
            opt.bindDropDownList(listShift, "select team_code,team_name from ht_sys_team where is_del = '0' and is_valid = '1' order by team_code", "team_name", "team_code");
            opt.bindDropDownList(listShift2, "select team_code,team_name from ht_sys_team where is_del = '0' and is_valid = '1' order by team_code", "team_name", "team_code");
            opt.bindDropDownList(listExcept, "select proj_code,proj_name from ht_qlt_inspect_proj  where is_del = '0' and is_valid = '1'", "proj_name", "proj_code");
            bindGrid();
        }
    }
    protected void bindGrid1()
    {
       DataBaseOperator opt =new DataBaseOperator();
        GridView1.DataSource = opt.CreateDataSetOra("select s.prod_name as 产品名,t.para_name as 工艺点,r.sort as 类型,r.value as 值,r.score as 扣分,case(r.done) when '0' then '未处理' else '己处理' end  as 是否处理 from ht_qlt_auto_event r left join ht_pub_tech_para t on t.para_code = r.para_code left join ht_pub_prod_design s on s.prod_code = r.prod_code where r.event_time between '" + txtStartTime.Text + "' and '" + txtEndTime.Text + "'");
        GridView1.DataBind();
    
    }
    protected void bindGrid2()
    {
       DataBaseOperator opt =new DataBaseOperator();
        GridView2.DataSource = opt.CreateDataSetOra("select s.prod_name as 产品名,t.section_name as 工艺段,r.cutoff_time as 断流时间,r.score as 扣分,case(r.done) when '0' then '未处理' when '1' then '己忽略' else '己处理' end  as 是否处理 from ht_qlt_cutoff_record r left join ht_pub_tech_section t on t.section_code = r.section_code left join ht_pub_prod_design s on s.prod_code = r.prod_code where r.event_time between '" + txtStartTime.Text + "' and '" + txtEndTime.Text + "'");
        GridView2.DataBind();


    }
    protected void bindGrid3()
    {
       DataBaseOperator opt =new DataBaseOperator();
        GridView3.DataSource = opt.CreateDataSetOra("select s.prod_name as 产品名,t.para_name as 工艺点,r.value as 抽检值,r.status as 状态,r.score as 扣分,case(r.done) when '0' then '未处理' else '己处理' end  as 是否处理 from ht_qlt_manual_record r left join ht_pub_tech_para t on t.para_code = r.para_code left join ht_pub_prod_design s on s.prod_code = r.prod_code where r.event_time between '" + txtStartTime.Text + "' and '" + txtEndTime.Text + "'");
        GridView3.DataBind();

    }
    protected void bindGrid4()
    {
       DataBaseOperator opt =new DataBaseOperator();
        GridView4.DataSource = opt.CreateDataSetOra("select s.prod_name as 产品名,t.proj_name as 工艺检查项,r.status as 状态,r.score as 扣分,case(r.done) when '0' then '未处理' else '己处理' end  as 是否处理 from ht_qlt_inspect_record r left join ht_qlt_inspect_proj t on t.proj_code = r.inspect_code left join ht_pub_prod_design s on s.prod_code = r.prod_code where r.event_time between '" + txtStartTime.Text + "' and '" + txtEndTime.Text + "'");
        GridView4.DataBind();
    
    }
    protected void bindGrid()
    {
        DataBaseOperator opt =new DataBaseOperator();
        GridView1.DataSource = opt.CreateDataSetOra("select s.prod_name as 产品名,t.para_name as 工艺点,r.sort as 类型,r.value as 值,r.score as 扣分,case(r.done) when '0' then '未处理' else '己处理' end  as 是否处理 from ht_qlt_auto_event r left join ht_pub_tech_para t on t.para_code = r.para_code left join ht_pub_prod_design s on s.prod_code = r.prod_code where r.event_time between '" + txtStartTime.Text + "' and '" + txtEndTime.Text + "'");
        GridView1.DataBind();
   
        GridView2.DataSource = opt.CreateDataSetOra("select s.prod_name as 产品名,t.section_name as 工艺段,r.cutoff_time as 断流时间,r.score as 扣分,case(r.done) when '0' then '未处理' when '1' then '己忽略' else '己处理' end  as 是否处理 from ht_qlt_cutoff_record r left join ht_pub_tech_section t on t.section_code = r.section_code left join ht_pub_prod_design s on s.prod_code = r.prod_code where r.event_time between '" + txtStartTime.Text + "' and '" + txtEndTime.Text + "'");
        GridView2.DataBind();

        GridView3.DataSource = opt.CreateDataSetOra("select s.prod_name as 产品名,t.para_name as 工艺点,r.value as 抽检值,r.status as 状态,r.score as 扣分,case(r.done) when '0' then '未处理' else '己处理' end  as 是否处理 from ht_qlt_manual_record r left join ht_pub_tech_para t on t.para_code = r.para_code left join ht_pub_prod_design s on s.prod_code = r.prod_code where r.event_time between '" + txtStartTime.Text + "' and '" + txtEndTime.Text + "'");
        GridView3.DataBind();

        GridView4.DataSource = opt.CreateDataSetOra("select s.prod_name as 产品名,t.proj_name as 工艺检查项,r.status as 状态,r.score as 扣分,case(r.done) when '0' then '未处理' else '己处理' end  as 是否处理 from ht_qlt_inspect_record r left join ht_qlt_inspect_proj t on t.proj_code = r.inspect_code left join ht_pub_prod_design s on s.prod_code = r.prod_code where r.event_time between '" + txtStartTime.Text + "' and '" + txtEndTime.Text + "'");
        GridView4.DataBind();
    
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
    protected void btnAddData_Click(object sender, EventArgs e)
    {

    }
    protected void AddRecord_Click(object sender, EventArgs e)
    {

    }
}