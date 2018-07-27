using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Device_FRDB : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            bindGrid();
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

        bindGrid();
    }
    protected void bindGrid()
    {
        string query = "select t.error_name as 故障名,t.specific_location as 故障位置,t.editor_id as 编制人,t.ID   from ht_eq_fault_db t where t.is_del = '0'";
        if (listType1.SelectedValue != "")
            query += " and FAULT_TYPE1 = '" + listType1.SelectedValue + "'";
        if (listType2.SelectedValue != "")
            query += " and FAULT_TYPE2 = '" + listType2.SelectedValue + "'";
        if (listType3.SelectedValue != "")
            query += " and FAULT_TYPE3 = '" + listType3.SelectedValue + "'";
        if (listType4.SelectedValue != "")
            query += " and FAULT_TYPE4 = '" + listType4.SelectedValue + "'";
        if (listType5.SelectedValue != "")
            query += " and FAULT_TYPE5 = '" + listType5.SelectedValue + "'";
        if (listType6.SelectedValue != "")
            query += " and FAULT_TYPE6 = '" + listType6.SelectedValue + "'";
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid();
    }

    protected void setBlank()
    {
        txtName.Text = "";
        listEqType.SelectedValue = "";
        txtEditor.Text = "";
        txtLocation.Text = "";
        listSection.SelectedValue = "";
        listStyle1.SelectedValue = "";
        listStyle2.SelectedValue = "";
        listStyle3.SelectedValue = "";
        listStyle4.SelectedValue = "";
        listStyle5.SelectedValue = "";
        listStyle6.SelectedValue = "";
        txtScean.Text = "";
        txtDescpt.Text = "";
        txtDescpt.Text = "";
        txtReason.Text = "";
        txtSolution.Text = "";
        hdcode.Value = "";
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        setBlank();
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", "treeClick();", true);
    }

    protected void btnDelSel_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked)
                {
                    string id = GridView1.DataKeys[i].Value.ToString();
                    string query = "update HT_EQ_FAULT_DB set IS_DEL = '1'  where ID = '" + id + "'";
                   MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
                    opt.UpDateOra(query);
                }
            }
            bindGrid();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void btnCkAll_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                ((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked = true;
            }
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void btnGridDel_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号  
            string id = GridView1.DataKeys[Rowindex].Value.ToString();
            string query = "update HT_EQ_FAULT_DB set IS_DEL = '1'  where ID = '" + id + "'";
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            opt.UpDateOra(query);
            bindGrid();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void btnGridView_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号  
        hdcode.Value = GridView1.DataKeys[Rowindex].Value.ToString();
        string query = "select * from HT_EQ_FAULT_DB where id = '" + hdcode.Value.ToString() + "'";
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            DataRow row = data.Tables[0].Rows[0];
            txtName.Text = row["ERROR_NAME"].ToString();
            listEqType.SelectedValue = row["EQP_TYPE"].ToString();
            txtEditor.Text = row["EDITOR_ID"].ToString();
            txtLocation.Text = row["SPECIFIC_LOCATION"].ToString();
            listSection.SelectedValue = row["SECTION_CODE"].ToString();
            listStyle1.SelectedValue = row["FAULT_TYPE1"].ToString();
            listStyle2.SelectedValue = row["FAULT_TYPE2"].ToString();
            listStyle3.SelectedValue = row["FAULT_TYPE3"].ToString();
            listStyle4.SelectedValue = row["FAULT_TYPE4"].ToString();
            listStyle5.SelectedValue = row["FAULT_TYPE5"].ToString();
            listStyle6.SelectedValue = row["FAULT_TYPE6"].ToString();
            txtScean.Text = row["SCEAN"].ToString();
            txtDescpt.Text = row["ERROR_DESCRIPTION"].ToString();
            txtReason.Text = row["FAILURE_CAUSE"].ToString();
            txtSolution.Text = row["SOLUTION"].ToString();
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "treeClick();", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try{
               MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            if(hdcode.Value != "")
                opt.UpDateOra("delete from HT_EQ_FAULT_DB  where id = '" + hdcode.Value.ToString() + "'");
             
                string[] seg = { "ERROR_NAME", "EQP_TYPE", "EDITOR_ID", "SPECIFIC_LOCATION", "SECTION_CODE","FAULT_TYPE1","FAULT_TYPE2","FAULT_TYPE3","FAULT_TYPE4","FAULT_TYPE5","FAULT_TYPE6","SCEAN","ERROR_DESCRIPTION","FAILURE_CAUSE","SOLUTION" };
                string[] value = {txtName.Text,listEqType.SelectedValue,txtEditor.Text,txtLocation.Text,listSection.SelectedValue ,listStyle1.SelectedValue,listStyle2.SelectedValue, listStyle3.SelectedValue,listStyle4.SelectedValue,listStyle5.SelectedValue,listStyle6.SelectedValue ,txtScean.Text,txtDescpt.Text ,txtReason.Text,txtSolution.Text };
                opt.InsertData(seg, value, "HT_EQ_FAULT_DB");
                bindGrid();  
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }


    }
 
  
    
}