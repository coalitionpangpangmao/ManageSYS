using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SysConfig_ApprovalMode : System.Web.UI.Page
{
    protected string tvHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           
            tvHtml = InitTree();          
            
        }
    }
    public string InitTree()
    {

       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra("select pz_type,pz_type_name from ht_pub_aprv_type order by pz_type");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul id='browser' class='filetree treeview-famfamfam'>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                tvHtml += "<li  ><span class='file' onclick = \"treeClick('" + row["pz_type"].ToString() + "')\">" + row["pz_type_name"].ToString() + "</span></a>";               
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }

  
    //版本
    protected void btnModify_Click(object sender, EventArgs e)
    {
       DataBaseOperator opt =new DataBaseOperator();
        string query = "select * from ht_pub_aprv_type where pz_type = '" + txtCode.Text + "'";
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string[] seg = { "pz_type_name" };
            string[] value = { txtName.Text};
            string condition = " where pz_type = '" + txtCode.Text + "'";
            opt.UpDateData(seg, value, "ht_pub_aprv_type", condition);
        }
        else
        {

            string[] seg = { "pz_type", "pz_type_name"};
            string[] value = { txtCode.Text, txtName.Text };
            opt.InsertData(seg, value, "ht_pub_aprv_type");
        }
        bindData(txtCode.Text);
        tvHtml = InitTree();

    }
    
    protected void bindData(string rcpcode)
    {
        string query = "select g1.pz_type,g1.pz_type_name as 审批类型,g2.index_no as 顺序号,g2.role as 角色,g2.flow_name as 发送环节名 from ht_pub_aprv_type g1  left join ht_pub_aprv_model g2 on g2.pz_type = g1.pz_type where g1.pz_type = '" + rcpcode + "'";
       DataBaseOperator opt =new DataBaseOperator();
        DataSet set = opt.CreateDataSetOra(query);
        if (set != null && set.Tables[0].Rows.Count > 0)
        {
            txtCode.Text = rcpcode;
            DataTable data = set.Tables[0];
            txtName.Text = data.Rows[0]["审批类型"].ToString();            
            if (data == null)
            {
                data = new DataTable();
                data.Columns.Add("pz_type");
                data.Columns.Add("审批类型");
                data.Columns.Add("顺序号");
                data.Columns.Add("角色");
                data.Columns.Add("发送环节名");
            }
            object[] value = { "", "",null,"", "" };
            data.Rows.Add(value);
            GridView1.DataSource = data;
            GridView1.DataBind();
            if (data != null && data.Rows.Count > 0)
            {
                for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
                {
                    DataRowView mydrv = data.DefaultView[i];
                    DropDownList list1 = (DropDownList)GridView1.Rows[i].FindControl("listType");
                    opt.bindDropDownList(list1, "select * from ht_pub_aprv_type", "pz_type_name", "pz_type");
                    list1.SelectedValue = txtCode.Text;
                    ((TextBox)GridView1.Rows[i].FindControl("txtOrder")).Text = mydrv["顺序号"].ToString();
                    DropDownList list2 = (DropDownList)GridView1.Rows[i].FindControl("listRole");
                    opt.bindDropDownList(list2, "select * from ht_svr_sys_role", "F_ROLE", "F_ROLE");
                    list2.SelectedValue = mydrv["角色"].ToString(); 
                    ((TextBox)GridView1.Rows[i].FindControl("txtFlowname")).Text = mydrv["发送环节名"].ToString();                    
                     list1.Enabled = false;
                }

            }
        }      
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        bindData(txtCode.Text);

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
    protected void btnDelSel_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked)
                {
                    string query = "delete from ht_pub_aprv_model   where PZ_TYPE = '" + ((DropDownList)GridView1.Rows[i].FindControl("listType")).SelectedValue + "' and INDEX_NO = '" + ((TextBox)GridView1.Rows[i].FindControl("txtOrder")).Text + "'";
                   DataBaseOperator opt =new DataBaseOperator();
                    if (opt.UpDateOra(query) == "Success")
                        opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "审批流程表删除成功，删除参数：" + ((DropDownList)GridView1.Rows[i].FindControl("listType")).SelectedValue + " " + ((TextBox)GridView1.Rows[i].FindControl("txtOrder")).Text);
                    else
                        opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "审批流程表删除失败，删除参数：" + ((DropDownList)GridView1.Rows[i].FindControl("listType")).SelectedValue + " " + ((TextBox)GridView1.Rows[i].FindControl("txtOrder")).Text);
                }
            }
            bindData(txtCode.Text);
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号  
            string query = "delete from ht_pub_aprv_model   where PZ_TYPE = '" + ((DropDownList)GridView1.Rows[Rowindex].FindControl("listType")).SelectedValue + "' and INDEX_NO = '" + ((TextBox)GridView1.Rows[Rowindex].FindControl("txtOrder")).Text + "'";
           DataBaseOperator opt =new DataBaseOperator();
            if (opt.UpDateOra(query) == "Success")
                opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "审批流程表删除成功，删除参数：" + ((DropDownList)GridView1.Rows[Rowindex].FindControl("listType")).SelectedValue + " " + ((TextBox)GridView1.Rows[Rowindex].FindControl("txtOrder")).Text);
            else
                opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "审批流程表删除失败，删除参数：" + ((DropDownList)GridView1.Rows[Rowindex].FindControl("listType")).SelectedValue + " " + ((TextBox)GridView1.Rows[Rowindex].FindControl("txtOrder")).Text);
            bindData(txtCode.Text);
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号             
            if (Rowindex >= 0)
            {
               DataBaseOperator opt =new DataBaseOperator();
                string query = "select * from ht_pub_aprv_model   where PZ_TYPE = '" + ((DropDownList)GridView1.Rows[Rowindex].FindControl("listType")).SelectedValue + "' and INDEX_NO = '" + ((TextBox)GridView1.Rows[Rowindex].FindControl("txtOrder")).Text + "'";
                DataSet data = opt.CreateDataSetOra(query);
                if (data != null && data.Tables[0].Rows.Count > 0)
                {
                    string[] seg = { "ROLE", "FLOW_NAME"};
                    string[] value = { ((DropDownList)GridView1.Rows[Rowindex].FindControl("listRole")).SelectedValue, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtFlowname")).Text};
                    string condition ="  where PZ_TYPE = '" + ((DropDownList)GridView1.Rows[Rowindex].FindControl("listType")).SelectedValue + "' and INDEX_NO = '" + ((TextBox)GridView1.Rows[Rowindex].FindControl("txtOrder")).Text + "'";
                    if (opt.UpDateData(seg, value, "ht_pub_aprv_model", condition) == "Success")
                        opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "模板流程表保存成功， 保存参数:" + ((DropDownList)GridView1.Rows[Rowindex].FindControl("listType")).SelectedValue + " " + ((TextBox)GridView1.Rows[Rowindex].FindControl("txtOrder")).Text);
                    else
                        opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "模板流程表保存失败， 保存参数:" + ((DropDownList)GridView1.Rows[Rowindex].FindControl("listType")).SelectedValue + " " + ((TextBox)GridView1.Rows[Rowindex].FindControl("txtOrder")).Text);
                }
                else
                {
                    string[] seg = {"PZ_TYPE","INDEX_NO", "ROLE", "FLOW_NAME" };
                    string[] value = { ((DropDownList)GridView1.Rows[Rowindex].FindControl("listType")).SelectedValue, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtOrder")).Text, ((DropDownList)GridView1.Rows[Rowindex].FindControl("listRole")).SelectedValue, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtFlowname")).Text };
                    if (opt.InsertData(seg, value, "ht_pub_aprv_model") == "Success")
                        opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "模板流程表保存成功， 保存参数:" + ((DropDownList)GridView1.Rows[Rowindex].FindControl("listType")).SelectedValue + " " + ((TextBox)GridView1.Rows[Rowindex].FindControl("txtOrder")).Text);
                    else
                        opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "模板流程表保存失败， 保存参数:" + ((DropDownList)GridView1.Rows[Rowindex].FindControl("listType")).SelectedValue + " " + ((TextBox)GridView1.Rows[Rowindex].FindControl("txtOrder")).Text);
                }
                bindData(txtCode.Text);
            }
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
}