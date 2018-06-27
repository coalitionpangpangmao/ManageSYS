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

    protected void bindGrid()
    {
        string query = "";     
        if (listtype.SelectedValue == "")
            query = "select h.inspect_type as 检查类型,s.section_name as 分组, r.inspect_code as 检查项目编码,r.inspect_name as 检查项目,r.remark as 备注 from ht_qlt_inspect_proj r left join ht_pub_tech_section s on s.section_code = r.INspect_Group and r.inspect_type = '0'  left join ht_inner_bool_display h on h.id = r.inspect_type  where r.is_del = '0' and r.is_valid = '1' and r.inspect_type = '0' union select h.inspect_type as 检查类型,t.name as 分组, r.inspect_code as 检查项目编码,r.inspect_name as 检查项目,r.remark as 备注 from ht_qlt_inspect_proj r  left join ht_INner_INSPECT_GROUP t on t.id = r.INspect_Group and r.inspect_type = '1' left join ht_inner_bool_display h on h.id = r.inspect_type  where r.is_del = '0' and r.is_valid = '1' and r.inspect_type = '1' ";
        else
        {
            if (listtype.SelectedValue == "0")
                query = "select h.inspect_type as 检查类型,s.section_name as 分组, r.inspect_code as 检查项目编码,r.inspect_name as 检查项目,r.remark as 备注 from ht_qlt_inspect_proj r left join ht_pub_tech_section s on s.section_code = r.INspect_Group and r.inspect_type = '0'  left join ht_inner_bool_display h on h.id = r.inspect_type  where r.is_del = '0' and r.is_valid = '1' and r.inspect_type = '0'";
            else
                query = "select h.inspect_type as 检查类型,t.name as 分组, r.inspect_code as 检查项目编码,r.inspect_name as 检查项目,r.remark as 备注 from ht_qlt_inspect_proj r  left join ht_INner_INSPECT_GROUP t on t.id = r.INspect_Group and r.inspect_type = '1' left join ht_inner_bool_display h on h.id = r.inspect_type  where r.is_del = '0' and r.is_valid = '1' and r.inspect_type = '1' ";
            if(listSection.SelectedValue != "")
                query += " and  r.INspect_Group = '" + listSection.SelectedValue + "'";
        }
     
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();
            


    }
    protected void initView()
    {        
       DataBaseOperator opt =new DataBaseOperator();
       opt.bindDropDownList(listtype, "select ID,inspect_type from ht_inner_bool_display t", "inspect_type", "ID");
       opt.bindDropDownList(listType2, "select ID,inspect_type from ht_inner_bool_display t", "inspect_type", "ID");
       opt.bindDropDownList(listCreator, "select ID,Name from ht_svr_user where is_DEL = '0'", "Name", "ID");
        
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (listType2.SelectedValue != "" && listSection2.SelectedValue != "")
        {
            DataBaseOperator opt = new DataBaseOperator();
            string temp = opt.GetSegValue("select nvl(max(substr(inspect_code,10,3)),0) + 1 as code from Ht_Qlt_Inspect_Proj where inspect_type = '" + listType2.SelectedValue + "' and inspect_group = '" + listSection2.SelectedValue + "'", "code");
            txtCode.Text = "703" + listType2.SelectedValue + listSection2.SelectedValue.PadLeft(5, '0') + temp.PadLeft(3, '0');
            listCreator.SelectedValue = ((MSYS.Data.SysUser)Session["User"]).Id;
            txtName.Text = "";
        }
        else
            ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "noselect", "alert('请选择检查项目类型及所属分组');", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
       DataBaseOperator opt =new DataBaseOperator();
       ArrayList commandlist = new ArrayList();
       commandlist.Add("delete from ht_qlt_inspect_proj where INSPECT_CODE = '" + txtCode.Text + "'");
       string[] seg = { "INSPECT_GROUP", "INSPECT_CODE", "INSPECT_NAME", "INSPECT_TYPE", "REMARK", "CREATE_ID", "CREATE_TIME", "UNIT" };
            string[] value = {listSection2.SelectedValue, txtCode.Text,txtName.Text,listType2.SelectedValue,txtRemark.Text,listCreator.SelectedValue,System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),txtUnit.Text};
            commandlist.Add(opt.InsertDatastr(seg, value, "ht_qlt_inspect_proj"));
            opt.TransactionCommand(commandlist);
            bindGrid();
    }

    protected void btnGrid1CkAll_Click(object sender, EventArgs e)//全选
    {
        try
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                ((CheckBox)GridView1.Rows[i].FindControl("ck")).Checked = true;
            }
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
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
                    string projcode = GridView1.DataKeys[i].Values.ToString();                   
                    string query = "update ht_qlt_inspect_proj set IS_DEL = '1'  where INSPECT_CODE = '" + projcode + "'";
                   DataBaseOperator opt =new DataBaseOperator();
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

    protected void btnEdit_Click(object sender, EventArgs e)//
    {
        try
        {
            Button btn = (Button)sender;
            int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
            string projcode = GridView1.DataKeys[rowIndex].Value.ToString();
            string query = "select * from ht_qlt_inspect_proj where INSPECT_CODE = '" + projcode + "' and  is_del = '0'";
           DataBaseOperator opt =new DataBaseOperator();
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
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "GridClick();", true);
            UpdatePanel2.Update();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void listtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataBaseOperator opt = new DataBaseOperator();
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

        DataBaseOperator opt = new DataBaseOperator();
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