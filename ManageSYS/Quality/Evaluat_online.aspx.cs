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
            initView();
        }
    }


    protected void bindgrid()
    {
        string query = createSql(txtBtime.Text);
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindgrid();

    }

    protected void initView()
    {
        createView();
        txtBtime.Text = System.DateTime.Now.ToString("yyyy-MM");
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
        string prod_code = opt.GetSegValue("select prod_name,prod_code from ht_pub_prod_design where prod_name = '" + row.Cells[1].Text + "'", "prod_code");
        string team_code = opt.GetSegValue("select team_name,team_code from ht_sys_team where team_name = '" + row.Cells[2].Text + "'", "team_code");
        string query = "select s.section_name,r.para_code,r.para_name,h.quarate,h.stddev,h.absdev,h.cpk from  ht_pub_tech_section s left join ht_pub_tech_para r on s.section_code = substr(r.para_code,1,5) left join hv_qlt_online_score h on h.para_code = r.para_code and substr(h.prodday,1,7) = '" + txtBtime.Text + "' and h.prod_code = '" + prod_code + "' and h.team = '" + team_code + "'  where r.para_type like '___1_' and r.is_del = '0' order by  s.section_code,r.para_code";
        DataTable dt = opt.CreateDataSetOra(query).Tables[0];


        StringBuilder str = new StringBuilder();
        str.Append(" <table class = 'reporttable'>");
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
        str.Append("<th  width ='25%'>检测日期</th><td >");
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
                    str.Append(row.Cells[i+3].Text);
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
        }
        str.Append(" </table>");
        htmltable = str.ToString();
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "showreport", "   $('#tabtop2').parent().show(); $('#tabtop2').click();$('#btnPrint').show();", true);


    } 

  
    private string createSql( string month)
    {      

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select section_code,section_name,remark,WEIGHT from ht_pub_tech_section where is_del= '0' and is_valid = '1' order by section_code";
        DataSet data = opt.CreateDataSetOra(query);

        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            int i = 1;
            StringBuilder sql = new StringBuilder();
            StringBuilder str = new StringBuilder();
            StringBuilder temp = new StringBuilder();
            sql.Append("select  p.prod_name as 产品,t.team_name as 班组 ");
            temp.Append("round(exp(");
            foreach (DataRow row in data.Tables[0].Rows)
            {
                string name = row["remark"].ToString();
                string code = row["section_code"].ToString();
                string weight = row["WEIGHT"].ToString();
                sql.Append(",g");
                sql.Append(i.ToString());
                sql.Append(".score*100 as ");
                sql.Append(name);
                if (i > 1)
                {
                    str.Append(" left join ");
                    temp.Append("+");
                }
                temp.Append("round(ln(power(nvl(g");
                temp.Append(i.ToString());
                temp.Append(".score,1),");
                temp.Append(weight);
                temp.Append(")),3)");

                            
                str.Append("(select prod_code,team,section, round(exp(sum(ln(power(quarate,weight)))),4) as score from hv_qlt_online_score  where substr(prodday,1,7) = '");
                str.Append(txtBtime.Text);
                str.Append("' and section = '");
                str.Append(code);
                str.Append("'  group by prod_code,team,section) g");             
                str.Append(i.ToString());
                if (i > 1)
                {
                    str.Append(" on g1.prod_code = g");
                    str.Append(i.ToString());
                    str.Append(".prod_code  and g1.team = g");
                    str.Append(i.ToString());
                    str.Append(".team ");
                }
                i++;
            }
            temp.Append(")*100,2) as 总分");
            sql.Append(",");
            sql.Append(temp);
            sql.Append(" from ");
            sql.Append(str.ToString());
            sql.Append("left join ht_pub_prod_design p on p.prod_code = g1.prod_code left join ht_sys_team t on t.team_code = g1.team ");

          
            return sql.ToString();
        }
        else return null;

    }


    private void createView()
    {

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select section_code,section_name,remark,WEIGHT from ht_pub_tech_section where is_del= '0' and is_valid = '1' order by section_code";
        DataSet data = opt.CreateDataSetOra(query);

        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            int i = 1;
            StringBuilder sql = new StringBuilder();
            StringBuilder str = new StringBuilder();
            StringBuilder temp = new StringBuilder();
            sql.Append("select  p.prod_name as 产品,t.team_name as 班组,g1.prodday as 时间");
            temp.Append("round(exp(");
            foreach (DataRow row in data.Tables[0].Rows)
            {
                string name = row["remark"].ToString();
                string code = row["section_code"].ToString();
                string weight = row["WEIGHT"].ToString();
                sql.Append(",g");
                sql.Append(i.ToString());
                sql.Append(".score*100 as ");
                sql.Append(name);
                if (i > 1)
                {
                    str.Append(" left join ");
                    temp.Append("+");
                }
                temp.Append("round(ln(power(nvl(g");
                temp.Append(i.ToString());
                temp.Append(".score,1),");
                temp.Append(weight);
                temp.Append(")),3)");


                str.Append("(select prod_code,team,section, prodday,round(exp(sum(ln(power(quarate,weight)))),4) as score from hv_qlt_online_score  where  section = '");
                str.Append(code);
                str.Append("'  group by prod_code,team,section,prodday) g");
                str.Append(i.ToString());
                if (i > 1)
                {
                    str.Append(" on g1.prod_code = g");
                    str.Append(i.ToString());
                    str.Append(".prod_code  and g1.team = g");
                    str.Append(i.ToString());
                    str.Append(".team and g1.prodday = g");
                    str.Append(i.ToString());
                    str.Append(".prodday ");
                }
                i++;
            }
            temp.Append(")*100,2) as 总分");
            sql.Append(",");
            sql.Append(temp);
            sql.Append(" from ");
            sql.Append(str.ToString());
            sql.Append("left join ht_pub_prod_design p on p.prod_code = g1.prod_code left join ht_sys_team t on t.team_code = g1.team ");
            opt.UpDateOra("create or replace view hv_online_Daily_report as " + sql.ToString());

          
        }
       

    }  
}