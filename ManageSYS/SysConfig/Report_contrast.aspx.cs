using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
public partial class SysConfig_Report_contrast : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {           
            bindGrid();
            initView();
        }
    }

    protected void initView()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listSection, "select distinct r.section_code,r.section_name  from ht_pub_tech_section r left join ht_pub_tech_para s on substr(s.para_code,1,5) = r.section_code and s.is_del = '0' and s.is_valid = '1' where r.is_del = '0' and r.is_valid = '1' and s.para_type like '1%'  order by r.section_code", "section_name", "section_code");

       
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
    
    protected DataSet bindInspect()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select distinct r.section_code,r.section_name  from ht_pub_tech_section r left join ht_pub_tech_para s on substr(s.para_code,1,5) = r.section_code and s.is_del = '0' and s.is_valid = '1' where r.is_del = '0' and r.is_valid = '1' and s.para_type like '1%' union select '' as section_code,'' as section_name  from dual order by section_code desc");
    }

    protected DataSet bindSeg()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra(" select COLUMN_NAME from user_tab_columns where table_name = 'HT_PROD_REPORT' and column_name like 'TECH_PARA%' union select '' as column_name from dual");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid();
    }
    protected void bindGrid()
    {
        string query = "select * from (select section_code,seg_name,seg_info,unit,para_code  from ht_inner_report_contrast where is_del = '0' union select substr(para_code,1,5),'' as seg_name,'',r.para_unit,para_code  from ht_pub_tech_para r where r.para_type like '1%' and  r.para_code in (select para_code from ht_pub_tech_para where para_type like '1%' minus select para_code from ht_inner_report_contrast))";
        if (listSection.SelectedValue != "")
            query += " where section_code = '" + listSection.SelectedValue + "'";

        query += " order by section_code,para_code";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null)
        {
            gridbind(data.Tables[0]);
        }

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string query = "select section_code,seg_name,seg_info,unit,para_code  from ht_inner_report_contrast where is_del = '0' ";
        if (listSection.SelectedValue != "")
            query += " and section_code = '" + listSection.SelectedValue + "'";
        query += " order by section_code,para_code";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet set = opt.CreateDataSetOra(query);
        DataTable data = set.Tables[0];
        if (data == null)
        {
            data = new DataTable();
            data.Columns.Add("section_code");
            data.Columns.Add("seg_name");
            data.Columns.Add("seg_info");
            data.Columns.Add("unit");
            data.Columns.Add("para_code");           
        }
        object[] value = { "", "", "", "","" };
        data.Rows.Add(value);
        gridbind(data);      
    }
    protected void gridbind(DataTable data)
    {       
        GridView1.DataSource = data;
        GridView1.DataBind();
        if (data != null && data.Rows.Count > 0)
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            for (int i = GridView1.PageSize * GridView1.PageIndex; i < GridView1.PageSize * (GridView1.PageIndex + 1) && i < data.Rows.Count; i++)
            {
                int j = i - GridView1.PageSize * GridView1.PageIndex;
                DataRowView mydrv = data.DefaultView[i];
                GridViewRow row = GridView1.Rows[j];
                ((DropDownList)row.FindControl("listType")).SelectedValue = mydrv["section_code"].ToString();
                DropDownList list = (DropDownList)row.FindControl("listPara");
                opt.bindDropDownList(list, "select para_code,para_name from ht_pub_tech_para  where para_type like '1%' and substr(para_code,1,5) = '" + mydrv["section_code"].ToString() + "' and is_del = '0'", "para_name", "para_code");
                list.SelectedValue = mydrv["para_code"].ToString();

                ((DropDownList)row.FindControl("listSeg")).SelectedValue = mydrv["seg_name"].ToString();
                ((TextBox)row.FindControl("txtname")).Text = mydrv["seg_info"].ToString();
                ((TextBox)row.FindControl("txtUnit")).Text = mydrv["unit"].ToString();

            }
        }
    }
    

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int rowindex = row.RowIndex;
        string section_code = ((DropDownList)row.FindControl("listType")).SelectedValue;
        string para_code = ((DropDownList)row.FindControl("listPara")).SelectedValue;  

        string[] seg = { "section_code", "seg_name", "para_code", "seg_info", "unit", "is_del" };
      
       string[] value = { section_code, ((DropDownList)row.FindControl("listSeg")).SelectedValue, para_code, ((TextBox)row.FindControl("txtname")).Text, ((TextBox)row.FindControl("txtUnit")).Text,"0"};
       MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
       string log_message = (opt.MergeInto(seg, value, 2, "ht_inner_report_contrast") == "Success")? "保存报表字段成功":"保存报表字段失败";       
       log_message += "--数据详情:" + string.Join("-",value);
        InsertTlog(log_message);        
            bindGrid();
    }

    protected void btnGrid1CkAll_Click(object sender, EventArgs e)//全选
    {
        int ckno = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (((CheckBox)GridView1.Rows[i].FindControl("ck")).Checked)
                ckno++;
        }
        bool check = (ckno < GridView1.Rows.Count);
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            ((CheckBox)GridView1.Rows[i].FindControl("ck")).Checked = check;

        }
    }

    protected void btnGrid1DelSel_Click(object sender, EventArgs e)//删除选中记录
    {
        try
        {
            //    createGridView();
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("ck")).Checked)
                {
                    string section_code = GridView1.DataKeys[i].Values[0].ToString();
                    string para_code = GridView1.DataKeys[i].Values[1].ToString();
                    GridViewRow row = GridView1.Rows[i];                 
                  
                    string query = "delete from  ht_inner_report_contrast   where section_code = '" + section_code + "' and seg_name = '" + ((DropDownList)row.FindControl("listSeg")).SelectedValue + "' and para_code = '" + para_code + "'";
                   MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
                   string log_message = opt.UpDateOra(query) == "Success" ? "删除报表字段成功" : "删除报表字段失败";
                   log_message += "--标识:" + section_code + para_code;
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

    protected void listtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList list = (DropDownList)sender;
        int rowindex = ((GridViewRow)list.NamingContainer).RowIndex;
        DropDownList listpara = (DropDownList)GridView1.Rows[rowindex].FindControl("listPara");
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        opt.bindDropDownList(listpara, "select para_code,para_name from ht_pub_tech_para  where para_type like '1%' and substr(para_code,1,5) = '" +list.SelectedValue + "' and is_del = '0'", "para_name", "para_code");
       

    }


  

}