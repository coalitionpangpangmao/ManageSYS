using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class Quality_Evaluat_Process : MSYS.Web.BasePage
{
    protected string htmltable;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            txtBtime.Text = System.DateTime.Now.ToString("yyyy-MM");

            bindgrid();
        }
    }

      
    protected void bindgrid()
    {
        string query = "select r.prod_name as 产品,t.* from hv_qlt_process_month_report t left join ht_pub_prod_design r on r.prod_code = t.prod_code where t.month =  '" + txtBtime.Text + "' order by t.prod_code";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query.ToString());
        //System.Diagnostics.Debug.WriteLine("月报sql");
        //System.Diagnostics.Debug.WriteLine(query);
        GridView1.DataSource = data;
        if (IsPostBack && data.Tables[0].Rows.Count <= 0)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "alert('无数据');", true);
            return;
        }
        GridView1.DataBind();
    }

    protected void bindgrid2(string prod_code)
    {
        if (prod_code == "&nbsp;")
            return;
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select r.prod_name as 产品,t.Record_time as 检测时间,s.team_name as 班组,t.* from hv_qlt_process_daily_report t left join ht_pub_prod_design r on r.prod_code = t.prod_code left join ht_sys_team s on s.team_code = t.team_id where  substr(t.record_time,1,7) ='" + txtBtime.Text + "' and t.prod_code = '" + prod_code + "'";
        DataSet data = opt.CreateDataSetOra(query);

        GridView2.DataSource = data;
        GridView2.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindgrid();

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[3].Visible = false;

    }
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[3].Visible = false;
        e.Row.Cells[4].Visible = false;
        e.Row.Cells[5].Visible = false;
    }


    protected void btngridview_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        //System.Diagnostics.Debug.WriteLine(row[2].ToString());
        initTable(row);
    }

    protected void btngridDetail_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;

        hideprod.Value = row.Cells[3].Text;

        bindgrid2(hideprod.Value);
    }


   /* protected void initTable(GridViewRow row)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query1 = "select prod_code from ht_pub_prod_design where prod_name = '"+row.Cells[1].Text+"'";

        DataTable dt = opt.CreateDataSetOra(query).Tables[0];
        string prodcode = opt.CreateDataSetOra(query1).Tables[0].Rows[0][0].ToString();
        //string query = "select t.section_name , r.inspect_code,r.inspect_name,s.lower_value||'~'||s.upper_value||r.unit as range from ht_qlt_inspect_proj r left join ht_qlt_inspect_stdd_sub s on s.inspect_code = r.inspect_code left join ht_pub_tech_section t on t.section_code = r.inspect_group left join ht_pub_prod_design hpp on hpp.inspect_stdd = s.stdd_code  where hpp.prod_code = '"+prodcode+"' and r.INSPECT_TYPE = '0' and r.is_del = '0' order by r.inspect_code";
        string query = "select t.section_name , r.inspect_code,r.inspect_name,s.lower_value||'~'||s.upper_value||r.unit as range from ht_qlt_inspect_proj r left join ht_qlt_inspect_stdd_sub s on s.inspect_code = r.inspect_code left join ht_pub_tech_section t on t.section_code = r.inspect_group  where r.INSPECT_TYPE = '0' and r.is_del = '0' order by r.inspect_code";

        query = "select t.section_name , r.inspect_code,r.inspect_name,s.lower_value||'~'||s.upper_value||r.unit as range from ht_qlt_inspect_proj r left join ht_qlt_inspect_stdd_sub s on s.inspect_code = r.inspect_code left join ht_pub_tech_section t on t.section_code = r.inspect_group where r.INSPECT_TYPE = '0' and r.is_del = '0' and   s.stdd_code in (select inspect_stdd from ht_pub_prod_design where prod_name = '" + row.Cells[1].Text + "' )  order by r.inspect_code";

      // string query2 = "select distinct t.inspect_code, r.avrg, r.std, r.quarate from HV_QLT_PROCESS_INSPECT_CODE t left outer join  hv_qlt_process_avg r on t.inspect_code = r.inspect_code left join ht_pub_prod_design hpp on hpp.inspect_stdd = t.stdd_code  where hpp.prod_code = '"+ prodcode +"' and r.record_time='"+txtBtime.Text+"-01"+"' or  r.record_time is null  order by t.inspect_code";

        string query2 = "select distinct t.inspect_code, r.avrg, r.std, r.quarate from HV_QLT_PROCESS_INSPECT_CODE t left outer join  hv_qlt_process_avg r on t.inspect_code = r.inspect_code  where  r.record_time='" + txtBtime.Text + "-01" + "' or  r.record_time is null  order by t.inspect_code";

        DataTable dt = opt.CreateDataSetOra(query).Tables[0];
        DataTable insertData = opt.CreateDataSetOra(query2).Tables[0];

        StringBuilder str = new StringBuilder();
        str.Append(" <table id = 'Msysexport' class = 'reporttable'>");
        str.Append("<thead>  <tr><th colspan ='4'> 鑫源再造梗丝线过程检验报告</th></tr></thead>");
        str.Append("<tbody>");
        str.Append(" <tr><th  width ='25%'>产品名称</th><td colspan ='3'>");
        str.Append(row.Cells[1].Text);
        str.Append("</td></tr>");
        str.Append("<tr><th>评分</th><td>");
        str.Append(row.Cells[row.Cells.Count - 1].Text);
        str.Append("</td>");
        str.Append("<th  width ='25%'>检测月份</th><td>");
        str.Append(txtBtime.Text);
        str.Append("</td>");
        str.Append("</tr>");
        str.Append("<tr>");
        str.Append("<th rowspan=2 width ='25%'>检查类型</th>");
        str.Append("<th rowspan=2 width ='25%'>检查项目</th>");
        str.Append("<th rowspan=2 width ='25%'>技术要求</th>");
        str.Append("<th colspan=3  width ='25%'>检测结果</th>");
        str.Append("</tr>");
        str.Append("<tr>");
        str.Append("<th >平均值</th>");
        str.Append("<th >标准差</th>");
        str.Append("<th>合格率</th>");
        str.Append("</tr>");

        var result = dt.AsEnumerable().GroupBy(x =>
x.Field<String>("section_name")).Select(x => x.First()).ToList();
        int i = 0;
        foreach (var item in result)
        {
            var temp = item[0].ToString();
            var sub = dt.AsEnumerable().Where(p => p.Field<String>("section_name") == temp).ToList();
            int j = 0;
            foreach (var subitem in sub)
            {

                str.Append("<tr>");
                if (j > 0)
                    str.Append("<td style='border-top-style: none; border-bottom-style: none' ></td>");
                else
                {
                    str.Append("<td style=' border-bottom-style: none'>");
                    str.Append(item[0]);
                    str.Append("</td>");
                }
                str.Append("<td >");
                str.Append(subitem[2].ToString());
                str.Append("</td>");
                str.Append("<td >");
                str.Append(subitem[3].ToString());
                str.Append("</td>");


                str.Append("<td >");
                //str.Append(row.Cells[i + 5].Text);
                str.Append(insertData.Rows[i][1].ToString());
                str.Append("</td>");
                str.Append("<td >");
                //str.Append(row.Cells[i + 5].Text);
                str.Append(insertData.Rows[i][2].ToString());
                str.Append("</td>");
                str.Append("<td >");
                //str.Append(row.Cells[i + 5].Text);
                str.Append(insertData.Rows[i][3].ToString());
                str.Append("</td>");
                str.Append(" </tr>");
                i++;
                j++;
            }
        }
        str.Append(" </table>");
        htmltable = str.ToString();

    }*/

    protected void initTable(GridViewRow row)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select t.section_name , r.inspect_code,r.inspect_name,s.lower_value||'~'||s.upper_value||r.unit as range from ht_qlt_inspect_proj r left join ht_qlt_inspect_stdd_sub s on s.inspect_code = r.inspect_code left join ht_pub_tech_section t on t.section_code = r.inspect_group where r.INSPECT_TYPE = '0' and r.is_del = '0' and   s.stdd_code in (select inspect_stdd from ht_pub_prod_design where prod_name = '" + row.Cells[1].Text + "' )  order by r.inspect_code";
        DataTable dt = opt.CreateDataSetOra(query).Tables[0];


        StringBuilder str = new StringBuilder();
        str.Append(" <table id = 'Msysexport' class = 'reporttable'>");
        str.Append("<thead>  <tr><th colspan ='4'> 鑫源再造梗丝线过程检验报告</th></tr></thead>");
        str.Append("<tbody>");
        str.Append(" <tr><th  width ='25%'>产品名称</th><td colspan ='3'>");
        str.Append(row.Cells[1].Text);
        str.Append("</td></tr>");
        str.Append("<tr><th>评分</th><td>");
        str.Append(row.Cells[row.Cells.Count - 1].Text);
        str.Append("</td>");
        str.Append("<th  width ='25%'>检测月份</th><td>");
        str.Append(txtBtime.Text);
        str.Append("</td>");
        str.Append("</tr>");
        str.Append("<tr >");
        str.Append("<th width ='25%'>检查类型</th>");
        str.Append("<th width ='25%'>检查项目</th>");
        str.Append("<th width ='25%'>技术要求</th>");
        str.Append("<th  width ='25%'>检测结果</th>");
        str.Append("</tr>");

        var result = dt.AsEnumerable().GroupBy(x =>
x.Field<String>("section_name")).Select(x => x.First()).ToList();
        int i = 0;
        foreach (var item in result)
        {
            var temp = item[0].ToString();
            var sub = dt.AsEnumerable().Where(p => p.Field<String>("section_name") == temp).ToList();
            int j = 0;
            foreach (var subitem in sub)
            {

                str.Append("<tr>");
                if (j > 0)
                    str.Append("<td style='border-top-style: none; border-bottom-style: none' ></td>");
                else
                {
                    str.Append("<td style=' border-bottom-style: none'>");
                    str.Append(item[0]);
                    str.Append("</td>");
                }
                str.Append("<td >");
                str.Append(subitem[2].ToString());
                str.Append("</td>");
                str.Append("<td >");
                str.Append(subitem[3].ToString());
                str.Append("</td>");


                str.Append("<td >");
                str.Append(row.Cells[i + 5].Text);
                str.Append("</td>");
                str.Append(" </tr>");
                i++;
                j++;
            }
        }
        str.Append(" </table>");
        htmltable = str.ToString();

    }

}