using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Craft_Inspect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindGrid();
            initView();
        }
 
    }
    protected void bindGrid()
    {
        string query = "select proj_code as 检查项目编码,proj_name as 检查项目,s.section_name as 所属工段,r.proj_area as 所属区域,r.inspect_type_code as 检查类型,r.create_name as 编制人,r.remark as 备注 from ht_qlt_inspect_proj r left join ht_pub_tech_section s on s.section_code = r.section_code where r.is_del = '0' and r.is_valid = '1' ";
        if(txtProj.Text != "")
            query += " and  r.proj_name like '%" + txtProj.Text + "%'";
        if(listSection.SelectedValue != "")
            query += " and  r.section_code = '" + listSection.SelectedValue + "'";
        if(listtype.SelectedValue != "")
            query += " and r.inspect_type_code = '" + listtype.SelectedValue + "'";
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();
            


    }
    protected void initView()
    {        
       DataBaseOperator opt =new DataBaseOperator();
        opt.bindDropDownList(listSection, "select section_code,section_name  from ht_pub_tech_section t where is_del = '0' and is_valid = '1' order by section_code", "section_name", "section_code");
        opt.bindDropDownList(listSection2, "select section_code,section_name  from ht_pub_tech_section t where is_del = '0' and is_valid = '1' order by section_code", "section_name", "section_code");
        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
       DataBaseOperator opt =new DataBaseOperator();
        opt.UpDateOra("delete from ht_qlt_inspect_proj where PROJ_CODE = '" + txtCode.Text + "'");
            string[] seg = { "SECTION_CODE","PROJ_CODE","PROJ_NAME","PROJ_AREA","INSPECT_TYPE_CODE","REMARK","CREATE_ID","CREATE_NAME","CREATE_TIME" };
            string[] value = {listSection2.SelectedValue, txtCode.Text,txtName.Text,listArea.SelectedValue,listType2.SelectedValue,txtRemark.Text,"cookieID","cookieName",System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")};
            opt.InsertData(seg, value, "ht_qlt_inspect_proj");
            bindGrid();
       
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
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
                    string query = "update ht_qlt_inspect_proj set IS_DEL = '1'  where PROJ_CODE = '" + projcode + "'";
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
            string query = "select * from ht_qlt_inspect_proj where PROJ_CODE = '" + projcode + "' and  is_del = '0'";
           DataBaseOperator opt =new DataBaseOperator();
            DataSet data = opt.CreateDataSetOra(query);
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                txtCode.Text = data.Tables[0].Rows[0]["PROJ_CODE"].ToString();
                txtName.Text = data.Tables[0].Rows[0]["PROJ_NAME"].ToString();
                listSection2.SelectedValue = data.Tables[0].Rows[0]["SECTION_CODE"].ToString();
                listArea.SelectedValue = data.Tables[0].Rows[0]["PROJ_AREA"].ToString();
                listType2.SelectedValue = data.Tables[0].Rows[0]["INSPECT_TYPE_CODE"].ToString();
                txtRemark.Text = data.Tables[0].Rows[0]["REMARK"].ToString();
                txtEditor.Text = data.Tables[0].Rows[0]["CREATE_NAME"].ToString();
            }
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "GridClick();", true);
            UpdatePanel2.Update();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
}