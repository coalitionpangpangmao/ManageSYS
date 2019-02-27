using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
public partial class Craft_Inspect : MSYS.Web.BasePage
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
        string query = "";
        if (listtype.SelectedValue == "")
            query = "select distinct h.inspect_type as 检查类型,s.section_name as 分组, r.inspect_code as 检查项目编码,r.inspect_name as 检查项目,r.remark as 备注 from ht_qlt_inspect_proj r left join ht_pub_tech_section s on s.section_code = r.INspect_Group and r.inspect_type = '0'  left join ht_inner_bool_display h on h.id = r.inspect_type  where r.is_del = '0' and r.is_valid = '1' and r.inspect_type = '0' union select h.inspect_type as 检查类型,t.name as 分组, r.inspect_code as 检查项目编码,r.inspect_name as 检查项目,r.remark as 备注 from ht_qlt_inspect_proj r  left join ht_INner_INSPECT_GROUP t on t.id = r.INspect_Group and r.inspect_type = '1' left join ht_inner_bool_display h on h.id = r.inspect_type  where r.is_del = '0' and r.is_valid = '1' and r.inspect_type = '1'" ;
        else
        {
            if (listtype.SelectedValue == "0")
                query = "select distinct h.inspect_type as 检查类型,s.section_name as 分组, r.inspect_code as 检查项目编码,r.inspect_name as 检查项目,r.remark as 备注 from ht_qlt_inspect_proj r left join ht_pub_tech_section s on s.section_code = r.INspect_Group and r.inspect_type = '0'  left join ht_inner_bool_display h on h.id = r.inspect_type  where r.is_del = '0' and r.is_valid = '1' and r.inspect_type = '0' ";
            else
                query = "select distinct h.inspect_type as 检查类型,t.name as 分组, r.inspect_code as 检查项目编码,r.inspect_name as 检查项目,r.remark as 备注 from ht_qlt_inspect_proj r  left join ht_INner_INSPECT_GROUP t on t.id = r.INspect_Group and r.inspect_type = '1' left join ht_inner_bool_display h on h.id = r.inspect_type  where r.is_del = '0' and r.is_valid = '1' and r.inspect_type = '1' ";
            if (listSection.SelectedValue != "")
                query += " and  r.INspect_Group = '" + listSection.SelectedValue + "'";
        }

        query += " order by r.inspect_code";
        System.Diagnostics.Debug.WriteLine(query);
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();
    }
    protected void initView()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listtype, "select distinct ID,inspect_type from ht_inner_bool_display t", "inspect_type", "ID");
        opt.bindDropDownList(listType2, "select distinct ID,inspect_type from ht_inner_bool_display t", "inspect_type", "ID");
        opt.bindDropDownList(listCreator, "select s.name,s.id from ht_svr_sys_role t left join ht_svr_sys_menu r on substr(t.f_right,r.f_id,1) = '1' left join ht_svr_user s on s.role = t.f_id where r.f_id = '" + this.RightId + "' union select q.name,q.id from ht_svr_sys_role t left join ht_svr_sys_menu r on substr(t.f_right,r.f_id,1) = '1' left join ht_svr_org_group  s on s.f_role = t.f_id  left join ht_svr_user q on q.levelgroupid = s.f_code  where r.f_id = '" + this.RightId + "'  order by id desc", "Name", "ID");

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (listtype.SelectedValue != "" && listSection.SelectedValue != "")
        {
            listType2.SelectedValue = listtype.SelectedValue;
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            if (listType2.SelectedValue == "0")
            {
                opt.bindDropDownList(listSection2, "select Section_code,Section_name from ht_pub_tech_section where is_valid = '1' and is_del = '0' order by section_code", "Section_name", "Section_code");

            }
            else
            {
                opt.bindDropDownList(listSection2, "select ID,Name from ht_inner_inspect_group t", "Name", "ID");
            }
            listSection2.SelectedValue = listSection.SelectedValue;
            string temp = opt.GetSegValue("select nvl(max(substr(inspect_code,10,3)),0) + 1 as code from Ht_Qlt_Inspect_Proj where inspect_type = '" + listtype.SelectedValue + "' and inspect_group = '" + listSection.SelectedValue + "'", "code");
            txtCode.Text = "703" + listtype.SelectedValue + listSection.SelectedValue.PadLeft(5, '0') + temp.PadLeft(3, '0');
            listCreator.SelectedValue = ((MSYS.Data.SysUser)Session["User"]).id;
            txtName.Text = "";
            ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", " $('.shade').fadeIn(200);", true);
        }
        else
            ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "noselect", "alert('请选择检查项目类型及所属分组');", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string[] seg = { "INSPECT_CODE", "INSPECT_GROUP", "INSPECT_NAME", "INSPECT_TYPE", "REMARK", "CREATE_ID", "CREATE_TIME", "UNIT" };
        string[] value = { txtCode.Text, listSection2.SelectedValue, txtName.Text, listType2.SelectedValue, txtRemark.Text, listCreator.SelectedValue, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), txtUnit.Text };

        string log_message = opt.MergeInto(seg, value, 1, "ht_qlt_inspect_proj") == "Success" ? "保存工艺检查项目成功" : "保存工艺检查项目失败";
        if (log_message == "保存工艺检查项目成功")
        {
            string[] procseg = { };
            object[] procvalues = { };
            if (listSection2.SelectedValue == "4")
            {               
                opt.ExecProcedures("Create_Sensor_Report", procseg, procvalues);
            }
            else if (listSection2.SelectedValue.Length == 1 && listSection2.SelectedValue != "4")
            {                
                opt.ExecProcedures("Create_phychem_Report", procseg, procvalues);
            }
            else
            {               
                opt.ExecProcedures("Create_process_Report", procseg, procvalues);
            }
        }
        log_message += "--详情:" + string.Join(",", value);
        InsertTlog(log_message);

       
        bindGrid();

        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", " $('.shade').fadeOut(100);", true);
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
                    string projcode = GridView1.DataKeys[i].Value.ToString();
                    string query = "delete from  ht_qlt_inspect_proj   where INSPECT_CODE = '" + projcode + "'";
                    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                    string log_message = opt.UpDateOra(query) == "Success" ? "删除工艺检查项目成功" : "删除工艺检查项目失败";
                    if (log_message == "删除工艺检查项目成功")
                    {
                        string[] procseg = { };
                        object[] procvalues = { };
                        if (GridView1.Rows[i].Cells[2].Text == "感观评测")
                        {                            
                            opt.ExecProcedures("Create_Sensor_Report", procseg, procvalues);
                        }
                        else if (GridView1.Rows[i].Cells[1].Text == "成品检验" && GridView1.Rows[i].Cells[2].Text != "感观评测")
                        {                           
                            opt.ExecProcedures("Create_phychem_Report", procseg, procvalues);
                        }
                        else
                        {                           
                            opt.ExecProcedures("Create_process_Report", procseg, procvalues);
                        }
                    }
                    log_message += "--标识:" + projcode;
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

    protected void btnEdit_Click(object sender, EventArgs e)//
    {

        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string projcode = GridView1.DataKeys[rowIndex].Value.ToString();
        string query = "select * from ht_qlt_inspect_proj where INSPECT_CODE = '" + projcode + "' and  is_del = '0'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            txtCode.Text = data.Tables[0].Rows[0]["INSPECT_CODE"].ToString();
            txtName.Text = data.Tables[0].Rows[0]["INSPECT_NAME"].ToString();
            listType2.SelectedValue = data.Tables[0].Rows[0]["INSPECT_TYPE"].ToString();
            if (listType2.SelectedValue == "0")
            {
                opt.bindDropDownList(listSection2, "select Section_code,Section_name from ht_pub_tech_section where is_valid = '1' and is_del = '0' order by section_code", "Section_name", "Section_code");

            }
            else
            {
                opt.bindDropDownList(listSection2, "select ID,Name from ht_inner_inspect_group t", "Name", "ID");
            }
            listSection2.SelectedValue = data.Tables[0].Rows[0]["INSPECT_GROUP"].ToString();
            txtRemark.Text = data.Tables[0].Rows[0]["REMARK"].ToString();
            listCreator.SelectedValue = data.Tables[0].Rows[0]["CREATE_ID"].ToString();
            txtUnit.Text = data.Tables[0].Rows[0]["UNIT"].ToString();
        }
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", " $('.shade').fadeIn(200);", true);

    }

    protected void listtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        if (listtype.SelectedValue == "0")
        {
            opt.bindDropDownList(listSection, "select Section_code,Section_name from ht_pub_tech_section where is_valid = '1' and is_del = '0' order by section_code", "Section_name", "Section_code");
        }
        else
        {
            opt.bindDropDownList(listSection, "select ID,Name from ht_inner_inspect_group t", "Name", "ID");
        }

    }

    protected void listType2_SelectedIndexChanged(object sender, EventArgs e)
    {

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        if (listType2.SelectedValue == "0")
        {
            opt.bindDropDownList(listSection2, "select Section_code,Section_name from ht_pub_tech_section where is_valid = '1' and is_del = '0' order by section_code", "Section_name", "Section_code");

        }
        else
        {
            opt.bindDropDownList(listSection2, "select ID,Name from ht_inner_inspect_group t", "Name", "ID");
        }
    }
}