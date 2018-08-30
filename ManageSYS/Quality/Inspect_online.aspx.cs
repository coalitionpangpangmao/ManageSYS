using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
public partial class Quality_Inspect_online : MSYS.Web.BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            initView();
        }
    }
    protected void initView()
    {
        txtBtime.Text = System.DateTime.Now.ToString("yyyy-MM");
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listProd, "select t.prod_code,r.prod_name from ht_prod_report t left join ht_pub_prod_design r on r.prod_code = t.prod_code where r.is_valid = '1' and r.is_del = '0' and  substr(t.starttime,1,7) = '" + txtBtime.Text + "' or substr(t.endtime,1,7) = '" + txtBtime.Text + "'", "prod_name", "prod_code");
        opt.bindDropDownList(listSection, "select section_code,section_name from ht_pub_tech_section  where is_valid = '1' and is_del = '0' order by section_code", "section_name", "section_code");
        opt.bindDropDownList(listPoint, "select para_code,para_name from ht_pub_tech_para t where  para_type like '___1%' and is_del = '0'", "para_name", "para_code");
        bindgrid();
    }
    protected string getQuerystr()
    {
        string query1 = "select distinct h.prod_name as 产品,j.para_name as 工艺点,k.team_name as 班组,g.avg as 均值,g.count as 采样点数,g.min as 最小值,g.max as 最大值,g.quarate as 合格率,g.uprate as 超上限率,g.downrate as 超下限率,g.stddev as 标准差,g.absdev as 绝对差,g.range as 范围,g.cpk as cpk,g.b_time as 开始时间,g.e_time as 结束时间  from (select t.prod_code,t.para_code,t.team,round(sum(t.avg*t.count)/sum(t.count),2) as avg,sum(t.count) as count,min(t.min) as min,max(t.max) as max,round(sum(t.quanum)/sum(t.count),2) as quarate,round(sum(t.upcount)/sum(t.count),2) as uprate,round(sum(t.downcount)/sum(t.count),2) as downrate,round(sqrt(sum(t.stddev*t.stddev*(t.count-1))/(sum(t.count)-1)),2) as stddev,round(sum(t.absdev *(t.count-1))/(sum(t.count)-1),2) as absdev,round((max(t.max) - min(t.min)),2) as range ,null as cpk,min(b_time) as b_time,max(e_time) as e_time  from ht_qlt_data_record t where substr(t.b_time,1,7) = '" + txtBtime.Text + "' ";
        string query2 = " union select distinct t.prod_code,t.para_code,t.team,t.avg,t.count,t.min,t.max,t.quarate,t.uprate,t.downrate,t.stddev,t.absdev,t.range,t.cpk,t.b_time,t.e_time from ht_qlt_data_record t where substr(t.b_time,1,7) = '" + txtBtime.Text + "' ";

        if (listProd.SelectedValue != "")
        {
            query1 += " and t.prod_code = '" + listProd.SelectedValue + "'";
            query2 += " and t.prod_code = '" + listProd.SelectedValue + "'";
        }
        if (listPoint.SelectedValue != "")
        {
            query1 += " and t.para_code = '" + listPoint.SelectedValue + "'";
            query2 += " and t.para_code = '" + listPoint.SelectedValue + "'";
        }
        else
        {
            if (listSection.SelectedValue != "")
            {
                query1 += " and substr(t.para_code,1,5) = '" + listSection.SelectedValue + "'";
                query2 += " and substr(t.para_code,1,5) = '" + listSection.SelectedValue + "'";
            }
        }

        query1 += "group by (t.plan_id,t.para_code,t.prod_code,t.team)  ";
        query2 += ") g left join ht_pub_prod_design h on h.prod_code = g.prod_code left join ht_pub_tech_para j on j.para_code = g.para_code left join ht_sys_team k on k.team_code = g.team order by h.prod_name,j.para_name,k.team_name,g.count";
        return query1 + query2;
    }
    protected void bindgrid()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = getQuerystr();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();

        if (data == null || data.Tables[0].Rows.Count <= 1)
        {
            return;
        }

        for (int j = GridView1.PageIndex * GridView1.PageCount; j < (GridView1.PageIndex+1) * GridView1.PageCount && j<GridView1.Rows.Count; j++)
        {
            DataRowView myv = (DataRowView)data.Tables[0].DefaultView[j];
            int i = j - GridView1.PageIndex * GridView1.PageCount; 
            if (myv["cpk"].ToString() == "")
                GridView1.Rows[i].BackColor = Color.FromArgb(20, 0, 200, 100);
        }
      
            for(int i = 0;i<2;i++)
            {
                TableCell oldtc = GridView1.Rows[0].Cells[i];
                oldtc.RowSpan = 1;               
                for (int j = 1; j < GridView1.Rows.Count; j++)
                {
                    TableCell newtc = GridView1.Rows[j].Cells[i];                   
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

        bindgrid();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindgrid();
    }


    protected void txtBtime_TextChanged(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listProd, "select t.prod_code,r.prod_name from ht_prod_report t left join ht_pub_prod_design r on r.prod_code = t.prod_code where r.is_valid = '1' and  r.is_del = '0' and substr(t.starttime,1,7) = '" + txtBtime.Text + "' or substr(t.endtime,1,7) = '" + txtBtime.Text + "'", "prod_name", "prod_code");
    }

    protected void listSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listPoint, "select para_code,para_name from ht_pub_tech_para  where is_valid = '1' and is_del = '0' and substr(para_code,1,5) = '" + listSection.SelectedValue + "' and para_type like '___1%'", "para_name", "para_code");
    }
}