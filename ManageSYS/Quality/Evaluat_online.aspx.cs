using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class Quality_Evaluat_online : MSYS.Web.BasePage
{
    protected string htmltable;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listTeam, "select team_code,team_name from ht_sys_team where is_del = '0' order by team_code", "team_name", "team_code");
            txtBtime.Text = System.DateTime.Now.ToString("yyyy-MM");
            bindgrid();
        }
    }


    protected void bindgrid()
    {        
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
       string query ;
       if (listTeam.SelectedValue != "")
           query = "select r.prod_name as 产品,s.team_name as 班组,  t.*,t.基础分-t.工艺扣分 as 实际得分 from hv_qlt_online_month_scoreT t left join ht_pub_prod_design r on r.prod_code = t.prod_code left join ht_sys_team s on s.team_code = t.team where t.month =  '" + txtBtime.Text + "' and t.team = '" + listTeam.SelectedValue + "' order by t.prod_code,t.team";
       else
           query = "select * from (select  r.prod_name as 产品,s.team_name as 班组,  t.*,t.基础分-t.工艺扣分 as 实际得分 from hv_qlt_online_month_Score t left join ht_pub_prod_design r on r.prod_code = t.prod_code left join ht_sys_team s on s.team_code = t.team where t.month = '" + txtBtime.Text + "' union  select  r.prod_name as 产品,s.team_name as 班组, t.*,t.基础分-t.工艺扣分 as 实际得分 from hv_qlt_online_month_scoreT t left join ht_pub_prod_design r on r.prod_code = t.prod_code left join ht_sys_team s on s.team_code = t.team where t.month = '" + txtBtime.Text + "' ) order by prod_code,team ";
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();
         if (data == null || data.Tables[0].Rows.Count <= 1)
        {
            return;
        }
                TableCell oldtc = GridView1.Rows[0].Cells[1];
                oldtc.RowSpan = 1;
                for (int j = 1; j < GridView1.Rows.Count; j++)
                {
                    TableCell newtc = GridView1.Rows[j].Cells[1];
                    if (newtc.Text == oldtc.Text)
                    {
                        newtc.Visible = false;
                        oldtc.RowSpan = oldtc.RowSpan + 1;
                        oldtc.VerticalAlign = VerticalAlign.Middle;
                    }
                    else
                    {
                        oldtc = newtc;
                        oldtc.RowSpan = 1;
                    }
                }
           
    

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[3].Visible = false; 
        e.Row.Cells[4].Visible = false; 
        e.Row.Cells[5].Visible = false; 
       
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindgrid();
    }

   

    protected void btngridview_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        initTable(row);
    }
  
    protected void initTable(GridViewRow row)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string prod_code = GridView1.DataKeys[row.RowIndex].Values[0].ToString();
        string team_code = GridView1.DataKeys[row.RowIndex].Values[1].ToString();
        string query;
        if (team_code != "")         
         query = "select t.section_name,t.para_code,t.para_name,t.quarate,t.stddev,t.absdev,t.cpk from hv_qlt_online_month_trep t where t.month = '" + txtBtime.Text + "' and t.team = '" + team_code + "' and t.prod_code = '" + prod_code + "'";
        else
            query = "select t.section_name,t.para_code,t.para_name,t.quarate,t.stddev,t.absdev,t.cpk from hv_qlt_online_month_report t where t.month = '" + txtBtime.Text + "' and t.prod_code = '" + prod_code + "'";

        DataTable dt = opt.CreateDataSetOra(query).Tables[0];

        StringBuilder str = new StringBuilder();
        str.Append(" <table  id = 'Msysexport'  class = 'reporttable'>");
        str.Append("<thead>  <tr><th colspan ='4'> 鑫源再造梗丝线在线评测报告</th></tr></thead>");
        str.Append("<tbody>");
        str.Append(" <tr><th  width ='25%'>产品名称</th><td >");
        str.Append(row.Cells[1].Text);
        str.Append("</td>");
        str.Append("  <th  width ='25%'>班组</th><td >");
        str.Append(row.Cells[2].Text);
        str.Append("</td></tr>");
        str.Append("<tr><th>评分</th><td >");
        str.Append(row.Cells[row.Cells.Count - 1].Text);
        str.Append("</td>");
        str.Append("<th  width ='25%'>检测月份</th><td >");
        str.Append(txtBtime.Text);
        str.Append("</td>");
        str.Append("</tr>");
        str.Append("</tbody>");
        str.Append("</table>");
        str.Append(" <table class = 'reporttable'>");
        str.Append("<tbody>");
        str.Append("<tr >");
        str.Append("<th width ='25%'>工艺段</th>");
        str.Append("<th width ='10%'>得分</th>");
        str.Append("<th width ='25%'>工艺点</th>");
        str.Append("<th width ='10%'>合格率</th>");
        str.Append("<th  width ='10%'>标准差</th>");
        str.Append("<th  width ='10%'>绝对差</th>");
        str.Append("<th  width ='10%'>CPK</th>");
        str.Append("</tr>");

        var result = dt.AsEnumerable().GroupBy(x =>
x.Field<String>("section_name")).Select(x => x.First()).ToList();
        int j = 0;
        foreach (var item in result)
        {
            var temp = item[0].ToString();
            var sub = dt.AsEnumerable().Where(p => p.Field<String>("section_name") == temp).ToList();
            int i = 0;
           
            foreach (var subitem in sub)
            {
                str.Append("<tr>");
                if (i > 0)
                {
                    str.Append("<td style='border-top-style: none; border-bottom-style: none' ></td>");
                    str.Append("<td style='border-top-style: none; border-bottom-style: none' ></td>");
                }
                else
                {
                    str.Append("<td style=' border-bottom-style: none'>");
                    str.Append(item[0]);
                    str.Append("</td>");
                    str.Append("<td style=' border-bottom-style: none'>");
                    str.Append(row.Cells[j+6].Text);
                    str.Append("</td>");
                }
                str.Append("<td >");
                str.Append(subitem[2].ToString());
                str.Append("</td>");

                str.Append("<td >");
                str.Append(subitem[3].ToString());
                str.Append("</td>");
                str.Append("<td >");
                str.Append(subitem[4].ToString());
                str.Append("</td>");
                str.Append("<td >");
                str.Append(subitem[5].ToString());
                str.Append("</td>");
                str.Append("<td >");
                str.Append(subitem[6].ToString());
                str.Append("</td>");
                str.Append(" </tr>");
                i++;
            }
            j++;
        }
        str.Append(" </table>");
        htmltable = str.ToString();
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "showreport", "   $('#tabtop2').parent().show(); $('#tabtop2').click();$('#btnPrint').show();$('#btnExport').show();", true);


    }

   

}