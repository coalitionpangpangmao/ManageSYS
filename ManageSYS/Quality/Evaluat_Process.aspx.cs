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
            initView();
        }
    }


    protected void bindgrid()
    {
        StringBuilder query = new StringBuilder();
        query.Append(createSql(true, txtBtime.Text, txtEtime.Text));
        query.Append(" union ");
        query.Append(createSql(false, txtBtime.Text, txtEtime.Text));
        query.Append(" order by 产品 ");
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query.ToString());
        GridView1.DataSource = data;
        GridView1.DataBind();

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindgrid();

    }

    protected void initView()
    {
        txtBtime.Text = System.DateTime.Now.ToString("yyyy-MM") + "-01";
        txtEtime.Text = System.DateTime.Now.AddMonths(1).ToString("yyyy-MM") + "-01";
        createView();
        bindgrid();

    }

    protected void btngridview_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        initTable(row);
    }

    protected void btngridDetail_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        bindgrid2(row.Cells[1].Text, row.Cells[2].Text);
    }
    protected void initTable(GridViewRow row)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select t.section_name , r.inspect_code,r.inspect_name,s.lower_value||'~'||s.upper_value||r.unit as range from ht_qlt_inspect_proj r left join ht_qlt_inspect_stdd s on s.inspect_code = r.inspect_code left join ht_pub_tech_section t on t.section_code = r.inspect_group where r.INSPECT_TYPE = '0' and r.is_del = '0' order by r.inspect_code";
        DataTable dt = opt.CreateDataSetOra(query).Tables[0];


        StringBuilder str = new StringBuilder();
        str.Append(" <table class = 'reporttable'>");
        str.Append("<thead>  <tr><th colspan ='4'> 鑫源再造梗丝线过程检验报告</th></tr></thead>");
        str.Append("<tbody>");
        str.Append(" <tr><th  width ='25%'>产品名称</th><td>");
        str.Append(row.Cells[1].Text);
        str.Append("</td>");
        str.Append("  <th  width ='25%'>班组</th><td>");
        str.Append(row.Cells[2].Text);
        str.Append("</td></tr>");
        str.Append("<tr><th>评分</th><td>");
        str.Append(row.Cells[row.Cells.Count - 1].Text);
        str.Append("</td>");
        str.Append("<th  width ='25%'>检测日期</th><td>");
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
                str.Append(row.Cells[i + 3].Text);
                str.Append("</td>");
                str.Append(" </tr>");
                i++;
                j++;
            }
        }
        str.Append(" </table>");
        htmltable = str.ToString();
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "showreport", "   $('#tabtop2').parent().show(); $('#tabtop2').click();$('#btnPrint').show();", true);


    }

    protected void bindgrid2(string prod_name, string team_name)
    {
        if (prod_name == "&nbsp;")
            return;

      
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select * from  hv_Process_daily_report where 产品 = '" + prod_name + "'  and  检测时间 between '" + txtBtime.Text + "' and '" + txtEtime.Text + "'";
        if (team_name != "&nbsp;")
            query += " and 班组 = '" + team_name + "'";

        DataSet data = opt.CreateDataSetOra(query);

        GridView2.DataSource = data;
        GridView2.DataBind();

        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "showreport", "   $('#tabtop3').parent().show(); $('#tabtop3').click();", true);

    }

    private void createView()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        string query = "select inspect_code,inspect_name from ht_qlt_inspect_proj  where INSPECT_TYPE = '0' and is_del = '0' order by inspect_code";
        DataSet data = opt.CreateDataSetOra(query);
        StringBuilder sql = new StringBuilder();
        StringBuilder str = new StringBuilder();
        StringBuilder temp = new StringBuilder();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            int i = 1;
            temp.Append("100");

            sql.Append("select p.prod_name as 产品,t.team_name as 班组,q.shift_name as 班时,g1.record_time as 检测时间");
            foreach (DataRow row in data.Tables[0].Rows)
            {
                string name = row["inspect_name"].ToString();
                string code = row["inspect_code"].ToString();
                sql.Append(",g");
                sql.Append(i.ToString());
                sql.Append(".");
                sql.Append(name);


                temp.Append("-nvl(g");
                temp.Append(i.ToString());
                temp.Append(".score,0)");

                if (i > 1)
                    str.Append(" left join ");

                str.Append("(select   a.prod_code,a.team_id,a.shift_ID,a.record_time ,nvl(b.score,0) as score, nvl(a.inspect_value,0) as ");
                str.Append(name);
                str.Append("  from ht_qlt_inspect_record a left join ht_qlt_inspect_event b on b.record_id = a.id where a.inspect_code = '");
                str.Append(code);
                str.Append("')g");


                str.Append(i.ToString());
                if (i > 1)
                {
                    str.Append(" on g1.prod_code = g");
                    str.Append(i.ToString());
                    str.Append(".prod_code  and g1.team_id = g");
                    str.Append(i.ToString());
                    str.Append(".team_id   and g1.record_time = g");
                    str.Append(i.ToString());
                    str.Append(".record_time  ");
                }
                i++;
            }
            temp.Append(" as 得分");
            sql.Append(",");
            sql.Append(temp);
            sql.Append(" from ");
            sql.Append(str.ToString());
            sql.Append("left join ht_pub_prod_design p on p.prod_code = g1.prod_code left join ht_sys_team t on t.team_code = g1.team_id left join ht_sys_shift q on q.shift_code = g1.shift_id order by t.team_name");
            opt.UpDateOra("create or replace view hv_Process_daily_report as " + sql.ToString());
        }

    }

    private string createSql(bool Isteamgroup, string btime, string etime)
    {
        StringBuilder sql = new StringBuilder();
        StringBuilder str = new StringBuilder();
        StringBuilder temp = new StringBuilder();
        sql.Append("select  p.prod_name as 产品 ");

        //班组可选
        if (Isteamgroup)
            sql.Append(",t.team_name as 班组 ");
        else
            sql.Append(",'' as 班组");

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select inspect_code,inspect_name from ht_qlt_inspect_proj  where INSPECT_TYPE = '0' and is_del = '0' order by inspect_code";
        DataSet data = opt.CreateDataSetOra(query);

        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            int i = 1;
            temp.Append("100");

            foreach (DataRow row in data.Tables[0].Rows)
            {
                string name = row["inspect_name"].ToString();
                string code = row["inspect_code"].ToString();
                sql.Append(",round(avg(nvl(g");
                sql.Append(i.ToString());
                sql.Append(".");
                sql.Append(name);
                sql.Append(",0)),2) as ");
                sql.Append(name);


                temp.Append("-round(avg(nvl(g");
                temp.Append(i.ToString());
                temp.Append(".score,0)),2)");

                if (i > 1)
                    str.Append(" left join ");

                str.Append("(select   a.prod_code,a.team_id,a.shift_ID,a.record_time ,nvl(b.score,0) as score, nvl(a.inspect_value,0) as ");
                str.Append(name);
                str.Append("  from ht_qlt_inspect_record a left join ht_qlt_inspect_event b on b.record_id = a.id where a.inspect_code = '");
                str.Append(code);
                str.Append("' and a.record_time between  '");
                str.Append(btime);
                str.Append("' and '");
                str.Append(etime);
                str.Append("')g");


                str.Append(i.ToString());
                if (i > 1)
                {
                    str.Append(" on g1.prod_code = g");
                    str.Append(i.ToString());
                    str.Append(".prod_code  and g1.team_id = g");
                    str.Append(i.ToString());
                    str.Append(".team_id   and g1.record_time = g");
                    str.Append(i.ToString());
                    str.Append(".record_time  ");
                }
                i++;
            }
            temp.Append(" as 得分");
            sql.Append(",");
            sql.Append(temp);
            sql.Append(" from ");
            sql.Append(str.ToString());
            sql.Append("left join ht_pub_prod_design p on p.prod_code = g1.prod_code left join ht_sys_team t on t.team_code = g1.team_id ");

            sql.Append(" group by p.prod_name ");
            if (Isteamgroup)
                sql.Append(",t.team_name");
            return sql.ToString();
        }
        else return null;

    }

}