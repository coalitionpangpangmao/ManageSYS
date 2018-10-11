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
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listSection, "select section_code,section_name  from ht_pub_tech_section where is_del = '0' and is_valid = '1' order by section_code ", "section_name", "section_code");
            opt.bindDropDownList(listEq, "select IDKEY,EQ_NAME from ht_eq_eqp_tbl where is_del = '0' and is_valid = '1'", "EQ_NAME", "IDKEY");
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
        string query = "select t1.eq_name as 故障设备, t.error_name as 故障名,t.specific_location as 故障位置,t.editor as 编制人,t.CREATE_TIME as 记录时间,t.ID   from ht_eq_fault_db t left join ht_eq_eqp_tbl t1 on t1.idkey = t.equip_code  where t.is_del = '0'";
        if (listType1.SelectedValue != "")
            query += " and t.FAULT_TYPE1 = '" + listType1.SelectedValue + "'";
        if (listType2.SelectedValue != "")
            query += " and t.FAULT_TYPE2 = '" + listType2.SelectedValue + "'";
        if (listType3.SelectedValue != "")
            query += " and t.FAULT_TYPE3 = '" + listType3.SelectedValue + "'";
        if (listType4.SelectedValue != "")
            query += " and t.FAULT_TYPE4 = '" + listType4.SelectedValue + "'";
        if (listType5.SelectedValue != "")
            query += " and t.FAULT_TYPE5 = '" + listType5.SelectedValue + "'";
        if (listType6.SelectedValue != "")
            query += " and t.FAULT_TYPE6 = '" + listType6.SelectedValue + "'";
        query += " order by t.CREATE_TIME";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
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
        txtEditor.Text = ((MSYS.Data.SysUser)Session["User"]).text;
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
                    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                    string log_message = opt.UpDateOra(query) == "Success" ? "删除故障信息成功" : "删除故障信息失败";
                    log_message += "--标识:" + id;
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

    protected void btnCkAll_Click(object sender, EventArgs e)
    {
        int ckno = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked)
                ckno++;
        }
        bool check = (ckno < GridView1.Rows.Count);
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            ((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked = check;

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
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string log_message = opt.UpDateOra(query) == "Success" ? "删除故障信息成功" : "删除故障信息失败";
            log_message += "--标识:" + id;
            InsertTlog(log_message);
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
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            DataRow row = data.Tables[0].Rows[0];
            txtName.Text = row["ERROR_NAME"].ToString();
            listEq.SelectedValue = row["EQUIP_CODE"].ToString();
            listEqType.SelectedValue = row["EQP_TYPE"].ToString();
            txtEditor.Text = row["EDITOR"].ToString();
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
        try
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string ftID = "";
            string query = "select * from HT_EQ_FAULT_DB where Error_name = '" + txtName.Text + "' and eqp_TYpe = '" + listEqType.SelectedValue + "' and SPECIFIC_LOCATION = '" + txtLocation.Text + "' and SECTION_CODE = '" + listSection.SelectedValue + "' and FAULT_TYPE1 = '" + listStyle1.SelectedValue + "' and FAULT_TYPE2 = '" + listStyle2.SelectedValue + "' and FAULT_TYPE3 = '" + listStyle3.SelectedValue + "' and FAULT_TYPE4 = '" + listStyle4.SelectedValue + "' and FAULT_TYPE5 = '" + listStyle5.SelectedValue + "' and FAULT_TYPE6 = '" + listStyle6.SelectedValue + "' and SCEAN = '" + txtScean.Text + "' and ERROR_DESCRIPTION = '" + txtDescpt.Text + "' and FAILURE_CAUSE = '" + txtReason.Text + "' and SOLUTION = '" + txtSolution.Text + "' and EQUIP_CODE = '" + listEq.SelectedValue + "'";
            query = query.Replace("= ''", "is null");
            DataSet data = opt.CreateDataSetOra(query);
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                ftID = data.Tables[0].Rows[0]["ID"].ToString();
            }
            else
            {
                ftID = opt.GetSegValue("select fault_id_seq.nextval from dual", "nextval");

                string[] seg = { "ID", "ERROR_NAME", "EQP_TYPE", "SPECIFIC_LOCATION", "SECTION_CODE", "FAULT_TYPE1", "FAULT_TYPE2", "FAULT_TYPE3", "FAULT_TYPE4", "FAULT_TYPE5", "FAULT_TYPE6", "SCEAN", "ERROR_DESCRIPTION", "FAILURE_CAUSE", "SOLUTION", "CREATE_TIME", "EQUIP_CODE","EDITOR" };
                string[] value = { ftID, txtName.Text, listEqType.SelectedValue, txtLocation.Text, listSection.SelectedValue, listStyle1.SelectedValue, listStyle2.SelectedValue, listStyle3.SelectedValue, listStyle4.SelectedValue, listStyle5.SelectedValue, listStyle6.SelectedValue, txtScean.Text, txtDescpt.Text, txtReason.Text, txtSolution.Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), listEq.SelectedValue, ((MSYS.Data.SysUser)Session["User"]).text };

                string log_message = opt.InsertData(seg, value, "HT_EQ_FAULT_DB") == "Success" ? "故障信息入库成功" : "故障信息入库失败";
                log_message += "--详情:" + string.Join(",", value);
                InsertTlog(log_message);
                bindGrid();
            }
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }


    }



}