﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Authority_GroupConfig : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindData();
            BindList();
            InitAuthorityList("");
        }
 
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string id = GridView1.DataKeys[rowIndex].Value.ToString();
        if (id == "")
            ScriptManager.RegisterStartupScript(UpdatePanel2, this.GetType(), "", "alert('请输入权限ID');", true);
        else
        {
           DataBaseOperator opt =new DataBaseOperator();
            opt.UpDateOra("delete from   ht_svr_sys_menu  where f_ID = '" + id + "'");

            string[] seg = { "F_ID", "F_MENU", "F_URL", "F_PRNT_MENU", "F_DESCRIPT", "F_TYPE" };
            string[] value = { id, ((TextBox)GridView1.Rows[rowIndex].FindControl("txtMenu")).Text, ((TextBox)GridView1.Rows[rowIndex].FindControl("txtURL")).Text, ((TextBox)GridView1.Rows[rowIndex].FindControl("txtPrntMenu")).Text, ((TextBox)GridView1.Rows[rowIndex].FindControl("txtDscrp")).Text, ((DropDownList)GridView1.Rows[rowIndex].FindControl("listType")).SelectedValue };
            opt.InsertData(seg, value, "ht_svr_sys_menu");
            bindData();
        }
      
       
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string id = GridView1.DataKeys[rowIndex].Value.ToString();
        string query = "update  ht_svr_sys_menu  set is_del = '1' where f_ID = '" + id + "'";
       DataBaseOperator opt =new DataBaseOperator();
        opt.UpDateOra(query);
        bindData();
    }

    protected void btnAdds_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "select t.F_ID as 权限ID,t.F_TYPE as 权限类型, t.f_prnt_menu as 父节点名,t.f_menu as 权限名称,t.f_url as URL ,t.F_DESCRIPT as 描述 from ht_svr_sys_menu t where is_del = '0'  order by t.F_ID ";
           DataBaseOperator opt =new DataBaseOperator();
            DataSet set = opt.CreateDataSetOra(query);
            DataTable data = new DataTable();
            string newID = (Convert.ToInt16(opt.GetSegValue("select max(F_ID) as ID from ht_svr_sys_menu", "ID")) + 1).ToString().PadLeft(5, '0');
            if (set == null)
            {
                data.Columns.Add("权限ID");
                data.Columns.Add("权限类型");
                data.Columns.Add("父节点名");
                data.Columns.Add("权限名称");
                data.Columns.Add("URL");
                data.Columns.Add("描述");               
            }
            else
                data = set.Tables[0];
            object[] value = { newID, "", "", "", "", "",  };
            data.Rows.Add(value);
            GridView1.DataSource = data;
            GridView1.DataBind();
            if (data != null && data.Rows.Count > 0)
            {
                for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
                {
                    DataRowView mydrv = data.DefaultView[i];

                    ((DropDownList)GridView1.Rows[i].FindControl("listType")).SelectedValue = mydrv["权限类型"].ToString();
                    ((TextBox)GridView1.Rows[i].FindControl("txtID")).Text = mydrv["权限ID"].ToString();
                    ((TextBox)GridView1.Rows[i].FindControl("txtPrntMenu")).Text = mydrv["父节点名"].ToString();
                    ((TextBox)GridView1.Rows[i].FindControl("txtMenu")).Text = mydrv["权限名称"].ToString();
                    ((TextBox)GridView1.Rows[i].FindControl("txtURL")).Text = mydrv["URL"].ToString();
                    ((TextBox)GridView1.Rows[i].FindControl("txtDscrp")).Text = mydrv["描述"].ToString();
                    if (mydrv["权限类型"].ToString() == "1")
                    {
                        ((Button)GridView1.Rows[i].FindControl("btnSave")).Enabled = false;
                        ((Button)GridView1.Rows[i].FindControl("btnDel")).Enabled = false;

                    }

                }
            }
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void bindData()
    {
        string query = "select t.F_ID as 权限ID,t.F_TYPE as 权限类型, t.f_prnt_menu as 父节点名,t.f_menu as 权限名称,t.f_url as URL ,t.F_DESCRIPT as 描述 from ht_svr_sys_menu t where is_del = '0'  order by t.F_ID ";
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                DataRowView mydrv = data.Tables[0].DefaultView[i];

                ((DropDownList)GridView1.Rows[i].FindControl("listType")).SelectedValue = mydrv["权限类型"].ToString();
                ((TextBox)GridView1.Rows[i].FindControl("txtID")).Text = mydrv["权限ID"].ToString();
                ((TextBox)GridView1.Rows[i].FindControl("txtPrntMenu")).Text = mydrv["父节点名"].ToString();
                ((TextBox)GridView1.Rows[i].FindControl("txtMenu")).Text = mydrv["权限名称"].ToString();
                ((TextBox)GridView1.Rows[i].FindControl("txtURL")).Text = mydrv["URL"].ToString();
                ((TextBox)GridView1.Rows[i].FindControl("txtDscrp")).Text = mydrv["描述"].ToString();
                if (mydrv["权限类型"].ToString() == "1")
                {
                    ((Button)GridView1.Rows[i].FindControl("btnSave")).Enabled = false;
                    ((Button)GridView1.Rows[i].FindControl("btnDel")).Enabled = false;

                }
            }
        }
 
    }

 

    protected void RightTree_SelectedNodeChanged(object sender, EventArgs e)
    {
        Right.Text = RightTree.SelectedValue;
        Role.Text = RightTree.SelectedNode.Text;
        InitAuthorityList(Right.Text);
    }
    protected void btnSave2_Click(object sender, EventArgs e)
    {
        try
        {
            if (Role.Text == "")
                ScriptManager.RegisterStartupScript(UpdatePanel2, this.GetType(), "", "alert('请输入角色名');", true);
            else
            {
               DataBaseOperator opt =new DataBaseOperator();
                string query = "select * from HT_SVR_SYS_Role where F_ROLE = '" + Role.Text.Trim() + "'";
                DataSet data = opt.CreateDataSetOra(query);
                if (data != null && data.Tables[0].Select().GetLength(0) > 0)
                {
                    query = "update HT_SVR_SYS_Role set F_Right = '"
                        + Right.Text + "',F_TIME = '"
                        + DateTime.Now.ToString("yyyy-MM-dd") + "' where F_ROLE = '"
                        + Role.Text.Trim() + "'";
                    opt.UpDateOra(query);
                    BindList();
                }
                else
                {
                    query = "insert into HT_SVR_SYS_Role(F_ROLE,F_RIGHT,F_TIME)values('"
                        + Role.Text + "','"
                        + Right.Text + "','"
                        + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    opt.UpDateOra(query);
                    BindList();
                }
            }
        }
        catch (Exception ex)
        {
            
        }
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        try
        {
           DataBaseOperator opt =new DataBaseOperator();
            string query = "delete from HT_SVR_SYS_Role where F_ROLE = '" + Role.Text + "'";
            opt.UpDateOra(query);
            BindList();
            SetBlank();
        }
        catch (Exception ex)
        {
            
        }
    }
    protected void BindList()
    {
        RightTree.Nodes.Clear();
       DataBaseOperator opt =new DataBaseOperator();
        string query = "select F_role,f_right from HT_SVR_SYS_Role";
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            DataRow[] Rows = data.Tables[0].Select();
            foreach (DataRow row in Rows)
            {
                TreeNode pnode = new TreeNode(row["F_ROLE"].ToString(), row["F_RIGHT"].ToString());
                RightTree.Nodes.Add(pnode);
            }
        }
    }

   
    protected void InitAuthorityList(string right)
    {
        Acesslist.Nodes.Clear();
        Denylist.Nodes.Clear();
       DataBaseOperator opt =new DataBaseOperator();
        DataSet temp = opt.CreateDataSetOra("select * from ht_svr_sys_menu order by F_ID");
        DataRow[] Rows = temp.Tables[0].Select();
        int strlength =  Convert.ToInt16(temp.Tables[0].Compute("Max(F_ID)", null));

        if (right.Length < strlength)
          right =   right.PadRight(strlength, '0');
        for (int i = 0; i < Rows.GetLength(0); i++)
        {
            int index = Convert.ToInt16(Rows[i]["F_ID"].ToString())-1;
            if (right.Substring(index, 1) == "0")
                AddListItem(Rows[i]["F_MENU"].ToString(), Rows[i]["F_ID"].ToString(), Denylist);
            else
                AddListItem(Rows[i]["F_MENU"].ToString(), Rows[i]["F_ID"].ToString(), Acesslist);
        }
        Right.Text = right;
    }
    protected void AddListItem(string text, string value, TreeView list)
    {
        TreeNode item = new TreeNode(text, value);
        list.Nodes.Add(item);
    }
  
 
    protected void SetBlank()
    {        
        Role.Text = "";
        InitAuthorityList("");
    }

    protected void Denylist_SelectedNodeChanged(object sender, EventArgs e)
    {
        SelValue.Text = Denylist.SelectedValue;
    }

    protected void Acesslist_SelectedNodeChanged(object sender, EventArgs e)
    {
        SelValue.Text = Acesslist.SelectedValue;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        int index = Convert.ToInt16(SelValue.Text);
        Right.Text = Right.Text.Substring(0, index - 1) + "1" + Right.Text.Substring(index, Right.Text.Length - index);
        InitAuthorityList(Right.Text);
    }

    protected void btnMinus_Click(object sender, EventArgs e)
    {
        int index = Convert.ToInt16(SelValue.Text);
        Right.Text = Right.Text.Substring(0, index - 1) + "0" + Right.Text.Substring(index, Right.Text.Length - index);
        InitAuthorityList(Right.Text);
    }


    protected void btnReset_Click(object sender, EventArgs e)
    {
        SetBlank();
    }
}