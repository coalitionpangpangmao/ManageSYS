using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.IO;
public partial class Craft_Event : MSYS.Web.BasePage
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
        txtBtime.Text = System.DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
        txtEtime.Text = System.DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listProd, "select distinct t.prod_code,r.prod_name from ht_prod_report t left join ht_pub_prod_design r on r.prod_code = t.prod_code where r.is_valid = '1' and r.is_del = '0' and  substr(t.starttime,1,10) between '" + txtBtime.Text + "' and '" + txtEtime.Text + "' or substr(t.endtime,1,7) between '" + txtBtime.Text + "' and '" + txtEtime.Text + "' or (t.starttime > '" + txtBtime.Text + "' and t.endtime < '" + txtEtime.Text + "') and t.prod_code is not null  and r.prod_name is not null", "prod_name", "prod_code");
        opt.bindDropDownList(listSection, "select section_code,section_name from ht_pub_tech_section  where is_valid = '1' and is_del = '0' order by section_code", "section_name", "section_code");
        opt.bindDropDownList(listPoint, "select para_code,para_name from ht_pub_tech_para t where  para_type like '___1%' and is_del = '0'", "para_name", "para_code");
        opt.bindDropDownList(listBatch, "select distinct plan_id from ht_qlt_data_record t where b_time between '" + txtBtime.Text + "' and '" + txtEtime.Text + "'", "plan_id", "plan_id");
       
        bindgrid();
    }
    protected string getQuerystr()
    {
        string query;
        query = "select t.ID,t.para_id,k.prod_name as 产品,t.para_des as 工艺点,t.timestart as 开始时间,t.timeend as 结束时间,t.planno as 批次号 from ht_tech_event t left join ht_pub_prod_design k on substr(t.planno,9,7) = k.prod_code where t.timestart between '" + txtBtime.Text + "' and '" + txtEtime.Text + "'"; 

        if (listProd.SelectedValue != "")
        {
            query += " and substr(t.planno,9,7) = '" + listProd.SelectedValue + "'";
        }
        if (listPoint.SelectedValue != "")
        {
            query += " and t.para_id = '" + listPoint.SelectedValue + "'";
        }
        if (listBatch.SelectedValue != "")
        {
            query += " and t.planno = '" + listBatch.SelectedValue + "'";
        }
        if (listSection.SelectedValue != "")
        {
            query += " and substr(t.para_id,1,5) = '" + listSection.SelectedValue + "'";
        }
        query += " order by t.timestart DESC";
        return query;
    }
    protected void bindgrid()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = getQuerystr();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();      

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
        opt.bindDropDownList(listProd, "select distinct t.prod_code,r.prod_name from ht_prod_report t left join ht_pub_prod_design r on r.prod_code = t.prod_code where r.is_valid = '1' and r.is_del = '0' and  substr(t.starttime,1,10) between '" + txtBtime.Text + "' and '" + txtEtime.Text + "' or substr(t.endtime,1,7) between '" + txtBtime.Text + "' and '" + txtEtime.Text + "' or (t.starttime > '" + txtBtime.Text + "' and t.endtime < '" + txtEtime.Text + "') and t.prod_code is not null and r.prod_name is not null", "prod_name", "prod_code");
        opt.bindDropDownList(listBatch, "select distinct plan_id from ht_qlt_data_record t where b_time between '" + txtBtime.Text + "' and '" + txtEtime.Text + "'", "plan_id", "plan_id");
    }

    protected void listSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listPoint, "select para_code,para_name from ht_pub_tech_para  where is_valid = '1' and is_del = '0' and substr(para_code,1,5) = '" + listSection.SelectedValue + "' and para_type like '___1%'", "para_name", "para_code");
    }
    protected void txtEtime_TextChanged(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listProd, "select distinct t.prod_code,r.prod_name from ht_prod_report t left join ht_pub_prod_design r on r.prod_code = t.prod_code where r.is_valid = '1' and r.is_del = '0' and  substr(t.starttime,1,10) between '" + txtBtime.Text + "' and '" + txtEtime.Text + "' or substr(t.endtime,1,7) between '" + txtBtime.Text + "' and '" + txtEtime.Text + "' or (t.starttime > '" + txtBtime.Text + "' and t.endtime < '" + txtEtime.Text + "') and t.prod_code is not null  and r.prod_name is not null", "prod_name", "prod_code");
        opt.bindDropDownList(listBatch, "select distinct plan_id from ht_qlt_data_record t where b_time between '" + txtBtime.Text + "' and '" + txtEtime.Text + "'", "plan_id", "plan_id");
    }


    protected void btngrid1View_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int index = row.RowIndex;
        hideEventID.Value = GridView1.DataKeys[index].Values[0].ToString();
        txtPara.Text = row.Cells[2].Text;
        txtPlanno.Text = row.Cells[1].Text;
        txtStarttime.Text = row.Cells[3].Text;
        txtEndtime.Text = row.Cells[4].Text;
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select * from ht_tech_event where id = '" + hideEventID.Value + "'");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            DataRow dr = data.Tables[0].Rows[0];
            if (dr["EDITOR"].ToString() == "")
                txtEditor.Text = ((MSYS.Data.SysUser)Session["User"]).text;
            else
                txtEditor.Text = dr["EDITOR"].ToString();
            txtEdittime.Text = dr["EDITTIME"].ToString();
            txtReason.Text = dr["REASON"].ToString();
            txtDescrpt.Text = dr["COMMENTS"].ToString();
        }   
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string[] subseg = { "ID", "EDITOR", "EDITTIME", "REASON", "COMMENTS" };
        string[] subvalue = {hideEventID.Value,txtEditor.Text,System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),txtReason.Text,txtDescrpt.Text };
        opt.MergeInto(subseg, subvalue, 1, "HT_TECH_EVENT");
    }
}