using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Craft_Tech_Std : System.Web.UI.Page
{
    protected string tvHtml;
    protected string subtvHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           DataBaseOperator opt =new DataBaseOperator();
            opt.bindDropDownList(listCrtApt, "select F_CODE,F_NAME from HT_SVR_ORG_GROUP", "F_NAME", "F_CODE");
            opt.bindDropDownList(listtech, "select * from HT_TECH_STDD_CODE where is_valid = '1' and is_del = '0'", "TECH_NAME", "TECH_CODE");
            opt.bindDropDownList(listtechC,"select * from HT_TECH_STDD_CODE where is_valid = '1' and is_del = '0'", "TECH_NAME", "TECH_CODE");
            tvHtml = InitTree();
            subtvHtml = InitTreePrcss();
            
        }
    }
    public string InitTree()
    {

       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra("select prod_code,prod_name from ht_pub_prod_design where is_del = '0' order by prod_code");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul id='browser' class='filetree treeview-famfamfam'>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                tvHtml += "<li ><span class='folder' >" + row["prod_name"].ToString() + "</span></a>";

                tvHtml += InitTreeTech(row["prod_code"].ToString());
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }
    public string InitTreeTech(string prod_code)
    {
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra("select tech_code,tech_name from ht_tech_stdd_code where prod_code =  '" + prod_code + "'  and is_del ='0' ");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                tvHtml += "<li  ><span class='file' onclick = \"treeClick('" + row["tech_code"].ToString() + "')\">" + row["tech_name"].ToString() + "</span></a>";

                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }

    public string InitTreePrcss()
    {

       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra("select g.section_code,g.section_name from ht_pub_tech_section g where g.IS_VALID = '1' and g.IS_DEL = '0' order by g.section_code ");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul id='browser' class='filetree treeview-famfamfam'>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                // tvHtml += "<li ><a href='Tech_Session.aspx?session_code=" + row["section_code"].ToString() + "' target='sessionFrame'><span class='folder'  onclick = \"$('#tabtop1').click()\">" + row["section_name"].ToString() + "</span></a>";  
                tvHtml += "<li ><span class='folder' >" + row["section_name"].ToString() + "</span></a>";
                tvHtml += InitTreeProcess(row["section_code"].ToString());
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }

    public string InitTreeProcess(string section_code)
    {
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra("select h.process_code,h.process_name from  ht_pub_inspect_process h where substr(h.process_code,1,5) = '" + section_code + "' and h.IS_VALID = '1' and h.IS_DEL = '0' order by h.process_code");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                // tvHtml += "<li ><a href='Tech_Process.aspx?process_code=" + row["process_code"].ToString() + "'  target='ProcessFrame'><span class='folder'  onclick = \"$('#tabtop2').click()\">" + row["process_name"].ToString() + "</span></a>";        
                tvHtml += "<li ><span class='folder'  onclick = \"tabClick(" + row["process_code"].ToString() + ")\">" + row["process_name"].ToString() + "</span></a>";               
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }
    //从一个标准复制为另一标准
    protected void btnCopy_Click(object sender, EventArgs e)
    {
       DataBaseOperator opt =new DataBaseOperator();
        string query = "select * from HT_TECH_STDD_CODE_DETAIL where TECH_CODE = '" + listtech.SelectedValue + "' and is_del = '0'";
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in data.Tables[0].Rows)
            {
               string[] seg = { "PARA_CODE", "PARA_NAME", "VALUE", "UPPER_LIMIT", "LOWER_LIMIT", "EER_DEV", "UNIT", "PROCESS_CODE",  "TECH_CODE" };
               string[] value = { row["PARA_CODE"].ToString(), row["PARA_NAME"].ToString(), row["VALUE"].ToString(), row["UPPER_LIMIT"].ToString(), row["LOWER_LIMIT"].ToString(), row["EER_DEV"].ToString(), row["UNIT"].ToString(), row["PROCESS_CODE"].ToString(), listtechC.SelectedValue};
                string log_message = opt.InsertData(seg, value, "HT_TECH_STDD_CODE_DETAIL")=="Success" ? "复制标准成功":"复制标准失败";
                log_message += ",复制数据："+string.Join(" ", value);
                opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
              
            }
           
        }
        
        bindGrid(listtechC.SelectedValue, hideprc.Value);
        tvHtml = InitTree();

    }
    //保存标准版本
    protected void btnModify_Click(object sender, EventArgs e)
    {
       DataBaseOperator opt =new DataBaseOperator();
        string query = "select * from HT_TECH_STDD_CODE where TECH_CODE = '" + txtCode.Text + "'";
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string[] seg = { "TECH_NAME", "PROD_CODE", "STANDARD_VOL", "B_DATE", "E_DATE", "CONTROL_STATUS", "CREATOR", "CREATE_DATE", "CREATE_DEPT", "REMARK" };
            string[] value = { txtName.Text, txtPro.Text, txtVersion.Text, txtExeDate.Text, txtEndDate.Text, listStatus.SelectedValue, txtCreator.Text, txtCrtDate.Text, listCrtApt.SelectedValue, txtDscpt.Text};
            string condition = " where FORMULA_CODE = '" + txtCode.Text + "'";
            string log_message = opt.UpDateData(seg, value, "HT_TECH_STDD_CODE", condition)=="Success" ? "保存标准成功":"保存标准失败";
            log_message += ", 保存数据：" + string.Join(" ", value);
            opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);

        }
        else
        {

            string[] seg = { "TECH_CODE", "TECH_NAME", "PROD_CODE", "STANDARD_VOL", "B_DATE", "E_DATE", "CONTROL_STATUS", "CREATOR", "CREATE_DATE", "CREATE_DEPT", "REMARK" };
            string[] value = { txtCode.Text, txtName.Text, txtPro.Text, txtVersion.Text, txtExeDate.Text, txtEndDate.Text, listStatus.SelectedValue, txtCreator.Text, txtCrtDate.Text, listCrtApt.SelectedValue, txtDscpt.Text };
            string log_message = opt.InsertData(seg, value, "HT_TECH_STDD_CODE")=="Success" ? "保存标准成功":"保存标准失败";
            log_message += ",保存数据：" + string.Join(" ", value);
            opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
        }
        bindGrid(txtCode.Text, hideprc.Value);
        tvHtml = InitTree();

    }
    
    protected void bindData(string rcpcode)
    {
        string query = "select tech_code  as 标准编码,tech_name  as 标准名称,PROD_CODE  as 产品编码,STANDARD_VOL  as 标准版本号,B_DATE  as 执行日期,E_DATE  as 结束日期,CONTROL_STATUS  as 受控状态,CREATOR  as 编制人,CREATE_DATE  as 编制日期,CREATE_DEPT  as 编制部门,REMARK  as 备注,is_valid from ht_tech_stdd_code where is_del = '0' and tech_code  = '" + rcpcode + "'";
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            txtCode.Text = rcpcode;
            txtName.Text = data.Tables[0].Rows[0]["标准名称"].ToString();
            txtPro.Text = data.Tables[0].Rows[0]["产品编码"].ToString();
            txtVersion.Text = data.Tables[0].Rows[0]["标准版本号"].ToString();
            txtExeDate.Text = data.Tables[0].Rows[0]["执行日期"].ToString();
            txtEndDate.Text = data.Tables[0].Rows[0]["结束日期"].ToString();
            listStatus.SelectedValue = data.Tables[0].Rows[0]["受控状态"].ToString();
            txtCreator.Text = data.Tables[0].Rows[0]["编制人"].ToString();
            txtCrtDate.Text = data.Tables[0].Rows[0]["编制日期"].ToString();
            listCrtApt.SelectedValue = data.Tables[0].Rows[0]["编制部门"].ToString();
            txtDscpt.Text = data.Tables[0].Rows[0]["备注"].ToString();
            ckValid.Checked = Convert.ToBoolean(Convert.ToInt16(data.Tables[0].Rows[0]["is_valid"].ToString()));
        }
        bindGrid(rcpcode, hideprc.Value);
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        bindData(txtCode.Text);

    }
    protected void UpdateGrid_Click(object sender, EventArgs e)
    {
        bindGrid(txtCode.Text, hideprc.Value);
    }

    protected void bindGrid(string rcpcode, string prccode)
    {

        string query = "select PARA_CODE as 参数编码,PARA_NAME as 参数名,VALUE as 标准值,UPPER_LIMIT as 上限,LOWER_LIMIT as 下限,EER_DEV as 允差,UNIT as 单位  from ht_tech_stdd_code_detail where IS_DEL = '0' and    tech_code = '" + rcpcode + "'";
            if (prccode.Length >= 7)
                query += " and   PROCESS_CODE = '" + prccode.Substring(0, 7) + "'";
           DataBaseOperator opt =new DataBaseOperator();
            DataSet set = opt.CreateDataSetOra(query);
            DataTable data = set.Tables[0];
            if (data == null)
            {
                data = new DataTable();
                data.Columns.Add("参数编码");
                data.Columns.Add("参数名");
                data.Columns.Add("标准值");
                data.Columns.Add("上限");
                data.Columns.Add("下限");
                data.Columns.Add("允差");
                data.Columns.Add("单位");
               
            }
            object[] value = { "", "", 0, 0, 0, 0, ""};
            data.Rows.Add(value);
            GridView1.DataSource = data;
            GridView1.DataBind();
            if (data != null && data.Rows.Count > 0)
            {
                for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
                {
                    DataRowView mydrv = data.DefaultView[i];
                    DropDownList list =  (DropDownList)GridView1.Rows[i].FindControl("listParaName");
                    opt.bindDropDownList(list, "select * from HT_PUB_TECH_PARA where is_del = '0' and substr(PARA_CODE,1,7) = '" + prccode + "'", "PARA_NAME", "PARA_CODE");
                    ((TextBox)GridView1.Rows[i].FindControl("txtCodeM")).Text = mydrv["参数编码"].ToString();
                    list.SelectedValue = mydrv["参数编码"].ToString();
                    ((TextBox)GridView1.Rows[i].FindControl("txtValueM")).Text = mydrv["标准值"].ToString();
                    ((TextBox)GridView1.Rows[i].FindControl("txtUlimitM")).Text = mydrv["上限"].ToString();
                    ((TextBox)GridView1.Rows[i].FindControl("txtLlimitM")).Text = mydrv["下限"].ToString();
                    ((TextBox)GridView1.Rows[i].FindControl("txtDevM")).Text = mydrv["允差"].ToString();
                    ((TextBox)GridView1.Rows[i].FindControl("txtUnitM")).Text = mydrv["单位"].ToString();
                 
                    if (i < GridView1.Rows.Count - 1)
                    {
                        list.Enabled = false;
                        
                    }
                    else
                    {
                        list.Enabled = true;
                       
                    }
                }

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
    protected void btnDelSel_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked)
                {
                    string mtr_code = ((TextBox)GridView1.Rows[i].FindControl("txtCodeM")).Text;
                    string query = "update HT_TECH_STDD_CODE_DETAIL set IS_DEL = '1'  where TECH_CODE = '" + txtCode.Text + "' and PARA_CODE = '" + mtr_code + "'";
                   DataBaseOperator opt =new DataBaseOperator();
                    string log_message = opt.UpDateOra(query) == "Success" ? "参数删除成功":"参数删除失败";
                    log_message += "，参数编码：" + txtCode.Text;
                    opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
                }
            }
            bindGrid(txtCode.Text,  hideprc.Value);
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
            string mtr_code = ((TextBox)GridView1.Rows[Rowindex].FindControl("txtCodeM")).Text;
            string query = "update HT_TECH_STDD_CODE_DETAIL set IS_DEL = '1'  where TECH_CODE = '" + txtCode.Text + "' and PARA_CODE = '" + mtr_code + "'";
           DataBaseOperator opt =new DataBaseOperator();
            string log_message = opt.UpDateOra(query)== "Success" ? "删除参数成功":"删除参数失败";
            log_message += ",参数编码" + txtCode.Text;
            opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
            bindGrid(txtCode.Text, hideprc.Value);
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
            string mtr_code = ((TextBox)GridView1.Rows[Rowindex].FindControl("txtCodeM")).Text;
            if (Rowindex >= 0)
            {
               DataBaseOperator opt =new DataBaseOperator();
                string query = "select * from HT_TECH_STDD_CODE_DETAIL  where TECH_CODE = '" + txtCode.Text + "' and PARA_CODE = '" + mtr_code + "'";
                DataSet data = opt.CreateDataSetOra(query);
                if (data != null && data.Tables[0].Rows.Count > 0)
                {                    
                    string[] seg = {"PARA_CODE", "PARA_NAME", "VALUE", "UPPER_LIMIT", "LOWER_LIMIT","EER_DEV", "UNIT", "PROCESS_CODE" ,"IS_DEL"};
                    string[] value = { ((TextBox)GridView1.Rows[Rowindex].FindControl("txtCodeM")).Text, ((DropDownList)GridView1.Rows[Rowindex].FindControl("listParaName")).SelectedItem.Text, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtValueM")).Text, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtUlimitM")).Text, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtLlimitM")).Text, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtDevM")).Text, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtUnitM")).Text, hideprc.Value ,"0"};
                    string condition =  "where TECH_CODE = '" + txtCode.Text + "' and PARA_CODE = '" + mtr_code + "'";
                    string log_message = opt.UpDateData(seg, value, "HT_TECH_STDD_CODE_DETAIL", condition)=="Success" ? "保存参数成功":"保存参数失败";
                    log_message += ", 保存数据:" + string.Join(" ", value);
                    opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
                }
                else
                {
                    string[] seg = { "PARA_CODE", "PARA_NAME", "VALUE", "UPPER_LIMIT", "LOWER_LIMIT", "EER_DEV", "UNIT", "PROCESS_CODE",  "TECH_CODE" };
                    string[] value = { ((TextBox)GridView1.Rows[Rowindex].FindControl("txtCodeM")).Text, ((DropDownList)GridView1.Rows[Rowindex].FindControl("listParaName")).SelectedItem.Text, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtValueM")).Text, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtUlimitM")).Text, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtLlimitM")).Text, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtDevM")).Text, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtUnitM")).Text, hideprc.Value, txtCode.Text };
                    string log_message = opt.InsertData(seg, value, "HT_TECH_STDD_CODE_DETAIL")=="Success" ? "保存参数成功":"保存参数失败";
                    log_message += ", 保存数据：" + string.Join(" ", value);
                    opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
                }
                bindGrid(txtCode.Text,  hideprc.Value);
            }
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }


    }


    protected void listParaName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList list = (DropDownList)sender;
            int Rowindex = ((GridViewRow)list.NamingContainer).RowIndex;//获得行号  
            ((TextBox)GridView1.Rows[Rowindex].FindControl("txtCodeM")).Text = list.SelectedValue;
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }

    }
}