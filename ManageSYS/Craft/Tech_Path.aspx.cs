using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
public partial class Craft_Tech_Path : MSYS.Web.BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {

           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
           opt.bindDropDownList(listSection1, "select section_code,section_name from ht_pub_tech_section where is_valid = '1' and is_del= '0' and IS_PATH_CONFIG = '1' order by section_code", "section_name", "section_code");
           opt.bindDropDownList(listSection2, "select section_code,section_name from ht_pub_tech_section where is_valid = '1' and is_del= '0'  and IS_PATH_CONFIG = '1'  order by section_code", "section_name", "section_code");
          
          
        }
        createGridView();
        bindGrid1();
    }
    /// <summary>
    /// Tab3 操作
    /// </summary>
    protected void bindGrid2()
    {

        string query = " select section_code as 工艺段,nodeName as 节点名,orders as 顺序号,descript as 描述,create_time as 创建时间,tag as 控制标签,ID from ht_pub_path_node where is_DEL = '0' and section_code = '" + listSection2.SelectedValue + "'";
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView2.DataSource = data;
        GridView2.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                ((TextBox)GridView2.Rows[i].FindControl("txtSection")).Text = listSection2.SelectedItem.Text;
                ((TextBox)GridView2.Rows[i].FindControl("txtNodeName")).Text = mydrv["节点名"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtOrder")).Text = mydrv["顺序号"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtDscrpt")).Text = mydrv["描述"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtCreatetime")).Text = mydrv["创建时间"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtTag")).Text = mydrv["控制标签"].ToString();

            }

        }

    }//绑定GridView2数据源
    protected void listSection2_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGrid2();
    }
    protected void btnGrid2Add_Click(object sender, EventArgs e)
    {
        try
        {
            string query = " select section_code as 工艺段,nodeName as 节点名,orders as 顺序号,descript as 描述,create_time as 创建时间,tag as 控制标签,ID from ht_pub_path_node where is_DEL = '0' and section_code = '" + listSection2.SelectedValue + "'";
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            DataSet set = opt.CreateDataSetOra(query);
            DataTable data = new DataTable();
            if (set != null && set.Tables[0].Rows.Count > 0)
                data = set.Tables[0];

            else
            {
                data.Columns.Add("工艺段");
                data.Columns.Add("节点名");
                data.Columns.Add("顺序号");
                data.Columns.Add("描述");
                data.Columns.Add("创建时间");
                data.Columns.Add("控制标签");
                data.Columns.Add("ID");
            }
            object[] value = { "", "", "", "", "", "", "0" };
            data.Rows.Add(value);
            GridView2.DataSource = data;
            GridView2.DataBind();
            if (data != null && data.Rows.Count > 0)
            {
                for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
                {
                    DataRowView mydrv = data.DefaultView[i];
                    ((TextBox)GridView2.Rows[i].FindControl("txtSection")).Text = listSection2.SelectedValue;
                    ((TextBox)GridView2.Rows[i].FindControl("txtNodeName")).Text = mydrv["节点名"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtOrder")).Text = mydrv["顺序号"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtDscrpt")).Text = mydrv["描述"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtCreatetime")).Text = mydrv["创建时间"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtTag")).Text = mydrv["控制标签"].ToString();

                }

            }
        }
        catch (Exception ee)
        {
            string str = ee.Message;
        }
    }
    protected void btnGrid2CkAll_Click(object sender, EventArgs e)//全选
    {
        try
        {
            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                ((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked = true;
            }
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnGrid2DelSel_Click(object sender, EventArgs e)//删除选中记录
    {
        try
        {
            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked)
                {
                    string ID = GridView2.DataKeys[i].Value.ToString();
                    string query = "update HT_PUB_PATH_NODE set IS_DEL = '1'  where ID = '" + ID + "'";
                   MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
                    opt.UpDateOra(query);
                }
            }
            bindGrid2();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnGrid2Save_Click(object sender, EventArgs e)
    {
        try
        {
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号             
            string ID = GridView2.DataKeys[Rowindex].Value.ToString();
            string[] seg = { "SECTION_CODE", "NODENAME", "ORDERS", "DESCRIPT", "CREATE_TIME", "TAG" };
            string[] value = { ((TextBox)GridView2.Rows[Rowindex].FindControl("txtSection")).Text, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtNodeName")).Text, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtOrder")).Text, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtDscrpt")).Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ((TextBox)GridView2.Rows[Rowindex].FindControl("txtTag")).Text };
            if (ID == "0")
            {
                opt.InsertData(seg, value, "ht_pub_path_node");
            }
            else
            {
                opt.UpDateData(seg, value, "ht_pub_path_node", " where ID = '" + ID + "'");
            }

            bindGrid2();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    /// <summary>
    /// tab2操作
    /// </summary>
    protected void bindGrid1()
    {
      
            string query = hideQuery.Value;
            if (query != "")
            {
               MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();

                DataSet set = opt.CreateDataSetOra(query);
                DataTable data = new DataTable();
                data = set.Tables[0];
                
                    object[] value = new object[data.Columns.Count];
                    value[0] = "";
                    for (int i = 1; i < value.Length - 2; i++)
                    { value[i] = "0"; }
                    value[data.Columns.Count - 2] = listSection1.SelectedValue;
                    value[data.Columns.Count - 1] = "";
                    data.Rows.Add(value);
              
                GridView1.DataSource = data;
                GridView1.DataBind();
                if (data != null && data.Rows.Count > 0)
                {

                    for (int k = 0; k < GridView1.Rows.Count; k++)
                    {
                        DataRowView mydrv = data.DefaultView[k];
                        ((TextBox)GridView1.Rows[k].FindControl("txt_Pathname")).Text = mydrv["路径名称"].ToString();
                        for (int j = 1; j < data.Columns.Count - 2; j++)
                        {
                            ((CheckBox)GridView1.Rows[k].FindControl("ck_" + j.ToString())).Checked = (mydrv[data.Columns[j].ColumnName].ToString() == "1");
                        }

                    }


                }
            }
       
    }
    protected void createGridView()
    {
        string query = createQuery(listSection1.SelectedValue);
        if (query != "")
        {
            hideQuery.Value = query;
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            DataSet data = opt.CreateDataSetOra(query);

            GridView1.Columns.Clear();
            TemplateField customField = new TemplateField();
            ////增加当前选择列
            customField = new TemplateField();
            customField.ShowHeader = true;
            customField.HeaderTemplate = new MSYS.Common.GridViewTemplate(DataControlRowType.Header, "选择", "");
            customField.ItemTemplate = new MSYS.Common.GridViewTemplate(DataControlRowType.DataRow, "sel", "CheckBox");
            ViewState["ck_sel"] = true;
            GridView1.Columns.Add(customField);
            /////增加路径名列
            customField = new TemplateField();
            customField.ShowHeader = true;
            customField.HeaderTemplate = new MSYS.Common.GridViewTemplate(DataControlRowType.Header, "路径名称", "");
            customField.ItemTemplate = new MSYS.Common.GridViewTemplate(DataControlRowType.DataRow, "Pathname", "TextBox");
            ViewState["txt_Pathname"] = true;
            GridView1.Columns.Add(customField);
            //增加节点列
            for (int j = 1; j < data.Tables[0].Columns.Count-2; j++)
            {
                customField = new TemplateField();
                customField.ShowHeader = true;
                customField.HeaderTemplate = new MSYS.Common.GridViewTemplate(DataControlRowType.Header, data.Tables[0].Columns[j].ColumnName, "");
                customField.ItemTemplate = new MSYS.Common.GridViewTemplate(DataControlRowType.DataRow, j.ToString(), "CheckBox");
                ViewState["ck_" + j.ToString()] = true;
                GridView1.Columns.Add(customField);
            }
            //增加保存按钮
            customField = new TemplateField();
            customField.ShowHeader = true;
            customField.HeaderTemplate = new MSYS.Common.GridViewTemplate(DataControlRowType.Header, "操作", "");
            customField.ItemTemplate = new MSYS.Common.GridViewTemplate(DataControlRowType.DataRow, "Grid1Save", "Button");
            ViewState["btn_Grid1Save"] = true;
            
            GridView1.Columns.Add(customField);
           
    
        }
    }
    protected string createQuery(string section)
    {
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select nodename from ht_pub_path_node where section_code = '" + section + "' and is_del = '0' order by orders");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string query = "select PATHNAME as 路径名称";
            int i = 1;
            foreach (DataRow row in data.Tables[0].Rows)
            {
                query += ",substr(pathcode," + i.ToString() + ",1) as " + row[0].ToString();
                i++;
            }
            query += ",SECTION_CODE,pathcode  from ht_pub_path_section where section_code = '" + section + "' and is_del = '0'";
            return query;
        }
        else
            return "";
    }
    protected void listSection1_SelectedIndexChanged(object sender, EventArgs e)
    {
        createGridView();
        bindGrid1();
    }
    protected void btnGrid1CkAll_Click(object sender, EventArgs e)//全选
    {
        try
        {            
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                ((CheckBox)GridView1.Rows[i].FindControl("ck_sel")).Checked = true;
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
                if (((CheckBox)GridView1.Rows[i].FindControl("ck_sel")).Checked)
                {
                    string sectioncode = GridView1.DataKeys[i].Values[0].ToString();
                    string pathcode = GridView1.DataKeys[i].Values[1].ToString();
                    string query = "update ht_pub_path_section set IS_DEL = '1'  where SECTION_CODE = '" + sectioncode + "' and pathcode = '" + pathcode + "'";
                   MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
                    opt.UpDateOra(query);
                }
            }
            createGridView();
            bindGrid1();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnGrid1Add_Click(object sender, EventArgs e)
    {
        createGridView();
        bindGrid1();
    }
 



   

}
