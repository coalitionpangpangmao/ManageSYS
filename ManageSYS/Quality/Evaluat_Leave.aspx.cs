using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class Quality_Evaluat_Leave : MSYS.Web.BasePage
{
    protected string htmltable;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            txtBtime.Text = System.DateTime.Now.ToString("yyyy-MM");
            MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.bindDropDownList(prod_code, "select prod_code,prod_name from ht_pub_prod_design where is_del= '0'", "prod_name", "prod_code");
            opt.bindDropDownList(address, "select distinct factory_address from ht_qlt_inspect_factory where is_del='0' and is_valid='1'", "factory_address", "factory_address");
            opt.bindDropDownList(time, "select distinct factory_time from ht_qlt_inspect_factory where is_del='0' and is_valid='1'", "factory_time", "factory_time");
            //bindgrid();
            bindFactoryTables();
        }
    }

    protected void bindFactoryTables()
    {
        //string query = "select r.prod_name as 产品,t.* from hv_qlt_phychem_month_report t left join ht_pub_prod_design r on r.prod_code = t.prod_code where r.prod_code = '" + prod_code.Text + "' and t.month =  '" + txtBtime.Text + "' order by t.prod_code";
        string prod = prod_code.Text;
        string add = address.Text;
        string tim = time.Text;
        if (prod == "") {
            prod = "%";
        }
        if (add == "") {
            add = "%";
        }
        if (tim == "") {
            tim = "%";
        }
        string query = "SELECT id as 编码, f.* from hv_qlt_inspect_factory f where product_code LIKE '"+prod+"' AND 地址 LIKE '"+add+"' AND 日期 LIKE '"+tim+"'";
        System.Diagnostics.Debug.WriteLine(query);
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query.ToString());
        GridView1.DataSource = data;
        GridView1.DataBind();

    }
    protected void bindgrid2(string prod_code)
    {
        if (prod_code == "&nbsp;")
            return;
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select r.prod_name as 产品,t.Record_time as 检测时间,s.team_name as 班组,t.* from hv_qlt_phychem_daily_report t left join ht_pub_prod_design r on r.prod_code = t.prod_code left join ht_sys_team s on s.team_code = t.team_id where substr(t.record_time,1,7) ='" + txtBtime.Text + "' and t.prod_code = '" + prod_code + "'";
        DataSet data = opt.CreateDataSetOra(query);
        //GridView2.DataSource = data;
        //GridView2.DataBind();
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
      //  bindgrid();
        bindFactoryTables();

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
        //hideprod.Value = row.Cells[3].Text;
        //bindgrid2(hideprod.Value);
    }

    protected DataRowCollection getTableDataDetail(string tableId) {
        string query = "SELECT * FROM HT_QLT_INSPECT_FACTORY_DETAIL WHERE factory_id = '" + tableId + "' ORDER BY inspect_code";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet tableData = opt.CreateDataSetOra(query);
        return tableData.Tables[0].Rows;
    }

    protected DataRow  getTableData(string tableId) {
        string query = "SELECT * FROM HT_QLT_INSPECT_FACTORY WHERE id = '" + tableId + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        System.Diagnostics.Debug.WriteLine(query);
        DataSet tableData = opt.CreateDataSetOra(query);
        return tableData.Tables[0].Rows[0];
    }

    protected void initTable(GridViewRow row)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select t.name , r.inspect_code,r.inspect_name,s.lower_value||'~'||s.upper_value||r.unit as range from ht_qlt_inspect_proj r left join ht_qlt_inspect_stdd s on s.inspect_code = r.inspect_code left join ht_inner_inspect_group t on t.id = r.inspect_group where r.inspect_group in ('1','2','3') and r.is_del = '0' order by r.inspect_code";
        DataTable dt = opt.CreateDataSetOra(query).Tables[0];
        System.Diagnostics.Debug.WriteLine(row.Cells[2].Text);
        string factory_id = row.Cells[2].Text.ToString();
        DataRow tableData = getTableData(factory_id);
        DataRowCollection tableDataDetail = getTableDataDetail(factory_id);
       
        StringBuilder str = new StringBuilder();
        str.Append(" <table  id = 'Msysexport' class = 'reporttable'>");
        str.Append("<thead>  <tr><th colspan ='4'> 鑫源再造梗丝线出厂检测报告</th></tr></thead>");
        str.Append("<tbody>");
        str.Append(" <tr><th  width ='25%'>产品名称</th><td colspan ='3'>");
        //str.Append(row.Cells[5].Text);
        str.Append(tableData[4].ToString());
        str.Append("</td></tr>");
        str.Append("<tr><th>生产日期</th><td>");
        //str.Append(row.Cells[3].Text);
        str.Append(tableData[6].ToString());
        str.Append("</td>");
        str.Append("<th  width ='25%'>检测日期</th><td>");
        //str.Append(txtBtime.Text);
        str.Append(tableData[3].ToString());
        str.Append("</td>");
        str.Append("</tr>");
        str.Append("<tr><th>出厂日期</th><td>");
        str.Append(tableData[1].ToString());
        str.Append("</td><th width='25%'>出厂地址</th><td>");
        str.Append(tableData[2].ToString());
        str.Append("</td></tr>");
        str.Append("</tr>");
        str.Append("<tr >");
        str.Append("<th width ='25%'>检查类型</th>");
        str.Append("<th width ='25%'>检查项目</th>");
        str.Append("<th width ='25%'>技术要求</th>");
        str.Append("<th  width ='25%'>检测结果</th>");
        str.Append("</tr>");

        var result = dt.AsEnumerable().GroupBy(x =>
x.Field<String>("name")).Select(x => x.First()).ToList();
        int i = 0;
        foreach (var item in result)
        {
            int j = 0;
            var temp = item[0].ToString();
            var sub = dt.AsEnumerable().Where(p => p.Field<String>("name") == temp).ToList();

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

                //    DataRowView view = ((DataTable)GridView1.DataSourceObject).DefaultView[row.RowIndex];

                str.Append("<td >");
                //str.Append(row.Cells[i + 5].Text);
                if (tableDataDetail.Count>i)
                    str.Append(tableDataDetail[i][4].ToString());
                else
                    str.Append("");
                str.Append("</td>");
                str.Append(" </tr>");
                i++;
                j++;
            }
        }
        str.Append("<tr><td>检测人：</td><td></td><td></td><td>日期：</td></tr>");
        str.Append(" </table>");
        System.Diagnostics.Debug.WriteLine(str);
        htmltable = str.ToString();



    }

  
}