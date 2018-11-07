using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Craft_RecipeAux : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listPro, "select PROD_CODE,PROD_NAME from ht_pub_prod_design t where is_del = '0' ", "PROD_NAME", "PROD_CODE");
            opt.bindDropDownList(listStatus, "select * from HT_INNER_BOOL_DISPLAY t", "CTRL_NAME", "ID");
            opt.bindDropDownList(listCrtApt, "select F_CODE,F_NAME from ht_svr_org_group ", "F_NAME", "F_CODE");
            opt.bindDropDownList(listCreator, "select s.name,s.id from ht_svr_sys_role t left join ht_svr_sys_menu r on substr(t.f_right,r.f_id,1) = '1' left join ht_svr_user s on s.role = t.f_id where r.f_id = '" + this.RightId + "' union select q.name,q.id from ht_svr_sys_role t left join ht_svr_sys_menu r on substr(t.f_right,r.f_id,1) = '1' left join ht_svr_org_group  s on s.f_role = t.f_id  left join ht_svr_user q on q.levelgroupid = s.f_code  where r.f_id = '" + this.RightId + "'  order by id desc", "Name", "ID");
        }
    }
    protected void bindData()
    {
        string query;
        if(hdcode.Value.Length == 8)
         query = "select FORMULA_CODE  as 配方编号,FORMULA_NAME  as 配方名称,PROD_CODE  as 产品编码,STANDARD_VOL  as 标准版本号,B_DATE  as 执行日期,E_DATE  as 结束日期,CONTROL_STATUS  as 受控状态,CREATE_ID  as 编制人,CREATE_DATE  as 编制日期,CREATE_DEPT_ID  as 编制部门,REMARK  as 备注,is_valid from ht_qa_aux_formula where is_del = '0' and FORMULA_CODE = '" + hdcode.Value + "'";
        else
            query = "select FORMULA_CODE  as 配方编号,FORMULA_NAME  as 配方名称,PROD_CODE  as 产品编码,STANDARD_VOL  as 标准版本号,B_DATE  as 执行日期,E_DATE  as 结束日期,CONTROL_STATUS  as 受控状态,CREATE_ID  as 编制人,CREATE_DATE  as 编制日期,CREATE_DEPT_ID  as 编制部门,REMARK  as 备注,is_valid from ht_qa_aux_formula where is_del = '0' and prod_code = '" + hdcode.Value + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            txtCode.Text = hdcode.Value;
            txtName.Text = data.Tables[0].Rows[0]["配方名称"].ToString();
            listPro.SelectedValue = data.Tables[0].Rows[0]["产品编码"].ToString();
            txtVersion.Text = data.Tables[0].Rows[0]["标准版本号"].ToString();
            txtExeDate.Text = data.Tables[0].Rows[0]["执行日期"].ToString();
            txtEndDate.Text = data.Tables[0].Rows[0]["结束日期"].ToString();
            listStatus.SelectedValue = data.Tables[0].Rows[0]["受控状态"].ToString();
            listCreator.SelectedValue = data.Tables[0].Rows[0]["编制人"].ToString();
            txtCrtDate.Text = data.Tables[0].Rows[0]["编制日期"].ToString();
            listCrtApt.SelectedValue = data.Tables[0].Rows[0]["编制部门"].ToString();
            txtDscpt.Text = data.Tables[0].Rows[0]["备注"].ToString();
            ckValid.Checked = ("1" == data.Tables[0].Rows[0]["is_valid"].ToString());
        }
        bindGrid();
    }
    protected void bindGrid()
    {
        string query;
        if(hdcode.Value.Length == 8)
         query = "select r.mater_type as 类别, r.MATER_CODE   as 物料编码,s.material_name as 物料名称,r.aux_scale  as 比例,r.aux_percent   as 占原料比例 from ht_qa_aux_formula_detail r left join ht_pub_materiel s on s.material_code = r.mater_code  where r.is_del = '0'  and r.FORMULA_CODE = '" + hdcode.Value + "'";
        else
            query = "select r.mater_type as 类别, r.MATER_CODE   as 物料编码,s.material_name as 物料名称,r.aux_scale  as 比例,r.aux_percent   as 占原料比例 from ht_qa_aux_formula_detail r left join ht_pub_materiel s on s.material_code = r.mater_code left join ht_qa_aux_formula t on t.FORMULA_CODE = r.FORMULA_CODE where r.is_del = '0'  and t.PROD_CODE = '" + hdcode.Value + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                GridViewRow row = GridView1.Rows[i];
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                DropDownList list = (DropDownList)row.FindControl("listGridType");
                list.SelectedValue = mydrv["类别"].ToString();
                opt.bindDropDownList((DropDownList)row.FindControl("listGridName"), "select material_code,material_name from ht_pub_materiel  where  is_del = '0'  and mat_type ='" + list.SelectedValue + "'", "material_name", "material_code");
                ((TextBox)row.FindControl("txtCodeM")).Text = mydrv["物料编码"].ToString();
                ((DropDownList)row.FindControl("listGridName")).SelectedValue = mydrv["物料编码"].ToString();
                ((TextBox)row.FindControl("txtScale")).Text = mydrv["比例"].ToString();
                ((TextBox)row.FindControl("txtPercent")).Text = mydrv["占原料比例"].ToString();
                ((DropDownList)row.FindControl("listGridType")).SelectedValue = mydrv["类别"].ToString();

            }

        }
    }

    protected DataSet gridTypebind()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select mattree_name,mattree_code from ht_pub_mattree t where length(mattree_code) = 4 and parent_code = '04' and mattree_code <>'0410' union select '' as mattree_name,'' as mattree_code from dual  order by mattree_code desc");
    }

    protected void listGridType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList list = (DropDownList)sender;
        GridViewRow row = (GridViewRow)list.NamingContainer;
        if (list.SelectedValue != "")
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.bindDropDownList((DropDownList)row.FindControl("listGridName"), "select material_code,material_name from ht_pub_materiel  where  is_del = '0'  and mat_type ='" + list.SelectedValue + "'", "material_name", "material_code");
        }
    }
    protected void listGridName_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList list = (DropDownList)sender;
        int rowIndex = ((GridViewRow)list.NamingContainer).RowIndex;
        ((TextBox)GridView1.Rows[rowIndex].FindControl("txtCodeM")).Text = list.SelectedValue;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string query;
        if (hdcode.Value.Length == 8)
            query = "select r.mater_type as 类别, r.MATER_CODE   as 物料编码,s.material_name as 物料名称,r.aux_scale  as 比例,r.aux_percent   as 占原料比例 from ht_qa_aux_formula_detail r left join ht_pub_materiel s on s.material_code = r.mater_code  where r.is_del = '0'  and r.FORMULA_CODE = '" + hdcode.Value + "'";
        else
            query = "select r.mater_type as 类别, r.MATER_CODE   as 物料编码,s.material_name as 物料名称,r.aux_scale  as 比例,r.aux_percent   as 占原料比例 from ht_qa_aux_formula_detail r left join ht_pub_materiel s on s.material_code = r.mater_code left join ht_qa_aux_formula t on t.FORMULA_CODE = r.FORMULA_CODE where r.is_del = '0'  and t.PROD_CODE = '" + hdcode.Value + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet set = opt.CreateDataSetOra(query);
        DataTable data = set.Tables[0];
        if (data == null)
        {
            data = new DataTable();
            data.Columns.Add("类别");
            data.Columns.Add("物料编码");
            data.Columns.Add("物料名称");
            data.Columns.Add("比例");
            data.Columns.Add("占原料比例");
        }
        object[] value = { "", "", "", 0, 0 };
        data.Rows.Add(value);
        GridView1.DataSource = data;
        GridView1.DataBind();
        if (data != null && data.Rows.Count > 0)
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                DataRowView mydrv = data.DefaultView[i];
                GridViewRow row = GridView1.Rows[i];
                DropDownList list = (DropDownList)row.FindControl("listGridType");
                list.SelectedValue = mydrv["类别"].ToString();
                opt.bindDropDownList((DropDownList)row.FindControl("listGridName"), "select material_code,material_name from ht_pub_materiel  where  is_del = '0'  and mat_type ='" + list.SelectedValue + "'", "material_name", "material_code");
                ((TextBox)row.FindControl("txtCodeM")).Text = mydrv["物料编码"].ToString();
                ((DropDownList)row.FindControl("listGridName")).SelectedValue = mydrv["物料编码"].ToString();
                ((TextBox)row.FindControl("txtScale")).Text = mydrv["比例"].ToString();
                ((TextBox)row.FindControl("txtPercent")).Text = mydrv["占原料比例"].ToString();
                ((DropDownList)row.FindControl("listGridType")).SelectedValue = mydrv["类别"].ToString();
            }

        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        bindData();
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {      
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();          
            hdcode.Value = txtCode.Text;         

                string[] seg = { "FORMULA_CODE", "FORMULA_NAME", "PROD_CODE", "STANDARD_VOL", "B_DATE", "E_DATE", "CONTROL_STATUS", "CREATE_ID", "CREATE_DATE", "CREATE_DEPT_ID", "REMARK" };
                string[] value = { txtCode.Text, txtName.Text, listPro.SelectedValue, txtVersion.Text, txtExeDate.Text, txtEndDate.Text, listStatus.SelectedValue, listCreator.SelectedValue, txtCrtDate.Text, listCrtApt.SelectedValue, txtDscpt.Text, };
                List<String> commandlist = new List<string>();
                commandlist.Add(opt.getMergeStr(seg, value, 1, "ht_qa_aux_formula"));
                commandlist.Add("update ht_pub_prod_design set aux_formula_code = '" + txtCode.Text + "' where prod_code = '" + listPro.SelectedValue + "'"); 
                string log_message = opt.TransactionCommand(commandlist) == "Success" ? "辅料配方保存成功" : "辅料配方保存失败";
                log_message += ",辅料配方保存信息：" + string.Join(",", value);
                InsertTlog(log_message);
        
            bindGrid();
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "updatetree", " window.parent.update();", true);


    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号  
            string mtr_code = ((TextBox)GridView1.Rows[Rowindex].FindControl("txtCodeM")).Text;
            string query = "update ht_qa_aux_formula_DETAIL set IS_DEL = '1'  where FORMULA_CODE = '" + txtCode.Text + "' and MATER_CODE = '" + mtr_code + "'";
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string log_message = opt.UpDateOra(query) == "Success" ? "物料删除成功" : "物料删除失败";
            log_message += ",物料编号：" + txtCode.Text;
            InsertTlog(log_message);
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
    protected void btnDelSel_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked)
                {
                    string mtr_code = ((TextBox)GridView1.Rows[i].FindControl("txtCodeM")).Text;
                    string query = "update ht_qa_aux_formula_DETAIL set IS_DEL = '1'  where FORMULA_CODE = '" + txtCode.Text + "' and MATER_CODE = '" + mtr_code + "'";
                    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                    string log_message = opt.UpDateOra(query) == "Success" ? " 物料删除成功" : "物料删除失败";
                    log_message += ",物料编号:" + txtCode.Text;
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号  
            string mtr_code = ((TextBox)GridView1.Rows[Rowindex].FindControl("txtCodeM")).Text;
            if (Rowindex >= 0)
            {
                MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
             
                string[] seg = { "FORMULA_CODE", "MATER_CODE", "aux_scale", "aux_percent", "mater_type" };
                string[] value = { txtCode.Text, mtr_code, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtScale")).Text, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtPercent")).Text, ((DropDownList)GridView1.Rows[Rowindex].FindControl("listGridType")).SelectedValue };
                string log_message = opt.MergeInto(seg, value,2, "ht_qa_aux_formula_detail") == "Success" ? "物料保存成功" : "物料保存失败";
                log_message += ",详情：" +string.Join("-",value);
                bindGrid();
            }
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }


    }


    protected void btnAddR_Click(object sender, EventArgs e)
    {
       
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string str = opt.GetSegValue("select Max(Formula_code) as code from ht_qa_aux_formula ", "CODE");
            if (str == "")
                str = "00000000";
            txtCode.Text = "70307" + (Convert.ToInt16(str.Substring(5)) + 1).ToString().PadLeft(3, '0');
            MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
            listCreator.SelectedValue = user.id;
            txtCrtDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            listCrtApt.SelectedValue = user.OwningBusinessUnitId;

            txtName.Text = "";
           
            txtVersion.Text = "";
            txtExeDate.Text = "";
            txtEndDate.Text = "";
            listStatus.SelectedValue = "";
            txtDscpt.Text = "";
            ckValid.Checked = false;
     
    }
}