using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
public partial class Quality_Evaluat_Sensor : MSYS.Web.BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listProd, "select t.prod_code,t.prod_name from ht_pub_prod_design t where t.is_del = '0' order by t.prod_code", "prod_name", "prod_code");

            txtMonth.Text = System.DateTime.Now.ToString("yyyy-MM");
            bindGrid1();

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid1();
    }
    protected void bindGrid1()
    {
        string query = "select s.sensor_month,wmsys.wm_concat(distinct  r.prod_name) as prod_name,wmsys.wm_concat(distinct o.name ) as editor     from hv_qlt_sensor_report t     left join ht_qlt_sensor_record s on s.id = t.main_id    left join ht_pub_prod_design r on r.prod_code = t.产品    left join ht_svr_user o on o.id = s.creat_id where r.is_del = '0'";
        if (listProd.SelectedValue != "")
            query += " and t.产品 = '" + listProd.SelectedValue + "'";
        if (txtMonth.Text != "")
            query += " and s.sensor_month = '" + txtMonth.Text + "'";
        query += " group by s.sensor_month order by s.sensor_month";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportExcel("再造梗丝产品感观评测报告", "", "", "", "", ".xls", hideMonth.Value.ToString(), DateTime.Now, true);
    }

    protected void btnGridEdit_Click(object sender, EventArgs e)//编制计划
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hideMonth.Value = row.Cells[1].Text;
        bindGrid2();
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "Detail", "$('#tabtop2').click();", true);

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
        bindGrid1();
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
        bindGrid2();
    }
    protected void bindGrid2()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet cols = opt.CreateDataSetOra("select distinct t.inspect_code,t.inspect_name||'('||r.minus_score||')' as inspect_name from ht_qlt_inspect_proj t left join ht_qlt_inspect_stdd r on r.inspect_code = t.inspect_code left join ht_qlt_sensor_record_sub s on s.inspect_code = t.inspect_code where t.inspect_group = '4'  and t.is_del = '0' order by inspect_code");

        string query = "select t.产品名称";
        if (cols != null && cols.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in cols.Tables[0].Rows)
            {
                query += ",t.\"" + row["inspect_name"].ToString()+ "\"";
            }
        }
        query += ",t.总得分,t.产品基础分,t.实际得分,r.平均分 from hv_qlt_sensor_RealRec t left join (select g.产品名称,g.sensor_month,round(avg(g.实际得分),2) as 平均分 from hv_qlt_sensor_realRec g group by g.产品名称,g.sensor_month） r on t.产品名称 = r.产品名称 and t.sensor_month = r.sensor_month  where t.sensor_month = '" + hideMonth.Value + "' order by t.产品 ";

        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            GridView2.DataSource = data;
            GridView2.DataBind();
            int avgcol = data.Tables[0].Columns.Count;
            if (avgcol > 5)
            {

                //合并产品名称列
                TableCell oldtc = GridView2.Rows[0].Cells[0];
                TableCell oldsc = GridView2.Rows[0].Cells[avgcol - 1];
                oldtc.RowSpan = 1;
                for (int j = 1; j < GridView2.Rows.Count; j++)
                {
                    TableCell newtc = GridView2.Rows[j].Cells[0];
                    TableCell newsc = GridView2.Rows[j].Cells[avgcol - 1];
                    if (newtc.Text == oldtc.Text)
                    {
                        newtc.Visible = false;
                        oldtc.RowSpan = oldtc.RowSpan + 1;
                        oldtc.VerticalAlign = VerticalAlign.Middle;

                        if (newsc.Text == oldsc.Text)
                        {
                            newsc.Visible = false;
                            oldsc.RowSpan = oldsc.RowSpan + 1;
                            oldsc.VerticalAlign = VerticalAlign.Middle;

                        }
                    }
                    else
                    {
                        oldtc = newtc;
                        oldtc.RowSpan = 1;
                        oldsc = newsc;
                        oldsc.RowSpan = 1;
                    }
                }
            }
        }

    }




}