using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Craft_RecipeCoat : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listPro, "select PROD_CODE,PROD_NAME from ht_pub_prod_design t where is_del = '0' ", "PROD_NAME", "PROD_CODE");
            opt.bindDropDownList(listStatus, "select * from HT_INNER_BOOL_DISPLAY t", "CTRL_NAME", "ID");
            opt.bindDropDownList(listCrtApt, "select F_CODE,F_NAME from ht_svr_org_group order by f_code  ", "F_NAME", "F_CODE");
            opt.bindDropDownList(listCreator, "select s.name,s.id from ht_svr_sys_role t left join ht_svr_sys_menu r on substr(t.f_right,r.f_id,1) = '1' left join ht_svr_user s on s.role = t.f_id where r.f_id = '" + this.RightId + "' union select q.name,q.id from ht_svr_sys_role t left join ht_svr_sys_menu r on substr(t.f_right,r.f_id,1) = '1' left join ht_svr_org_group  s on s.f_role = t.f_id  left join ht_svr_user q on q.levelgroupid = s.f_code  where r.f_id = '" + this.RightId + "'  order by id desc", "Name", "ID");
        }
    }
    protected void bindData()
    {
        string query;
        if (hdcode.Value.Length == 8)
            query = "select FORMULA_CODE  as 配方编号,FORMULA_NAME  as 配方名称,PROD_CODE  as 产品编码,STANDARD_VOL  as 标准版本号,B_DATE  as 执行日期,E_DATE  as 结束日期,CONTROL_STATUS  as 受控状态,CREATE_ID  as 编制人,CREATE_DATE  as 编制日期,CREATE_DEPT_ID  as 编制部门,REMARK  as 备注,is_valid from ht_qa_coat_formula where is_del = '0' and FORMULA_CODE = '" + hdcode.Value + "'";
        else
            query = "select FORMULA_CODE  as 配方编号,FORMULA_NAME  as 配方名称,PROD_CODE  as 产品编码,STANDARD_VOL  as 标准版本号,B_DATE  as 执行日期,E_DATE  as 结束日期,CONTROL_STATUS  as 受控状态,CREATE_ID  as 编制人,CREATE_DATE  as 编制日期,CREATE_DEPT_ID  as 编制部门,REMARK  as 备注,is_valid from ht_qa_coat_formula where is_del = '0' and PROD_CODE = '" + hdcode.Value + "'";
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
        bindGrid1();
        bindGrid2();
    }
    protected void bindGrid1()
    {
        string query;
        if (hdcode.Value.Length == 8)
            query = "select r.MATER_CODE as 香料种类,r.coat_scale as 比例,r.need_size as 每罐调配所需 from ht_qa_coat_formula_detail r  left join ht_pub_materiel s on s.material_code = r.mater_code   where r.coat_flag = 'XJ' and r.is_del = '0' and r.is_valid = '1'  and r.formula_code  = '" + hdcode.Value + "'";
        else
            query = "select r.MATER_CODE as 香料种类,r.coat_scale as 比例,r.need_size as 每罐调配所需 from ht_qa_coat_formula_detail r  left join ht_pub_materiel s on s.material_code = r.mater_code left join ht_qa_coat_formula t on t.formula_code = r.formula_code  where r.coat_flag = 'XJ' and r.is_del = '0' and r.is_valid = '1'  and t.PROD_CODE  = '" + hdcode.Value + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                ((DropDownList)GridView1.Rows[i].FindControl("listGridName")).SelectedValue = mydrv["香料种类"].ToString();
                ((TextBox)GridView1.Rows[i].FindControl("txtCodeM")).Text = mydrv["香料种类"].ToString();
                ((TextBox)GridView1.Rows[i].FindControl("txtScale")).Text = mydrv["比例"].ToString();
                ((TextBox)GridView1.Rows[i].FindControl("txtPercent")).Text = mydrv["每罐调配所需"].ToString();


            }

        }
    }

    protected void bindGrid2()
    {
        string query;
        if (hdcode.Value.Length == 8)
            query = "select r.MATER_CODE as 回填液编码,r.coat_scale as 比例,r.REMARK as 备注 from ht_qa_coat_formula_detail r where r.coat_flag = 'TPY' and r.is_del = '0' and r.is_valid = '1'  and r.formula_code  = '" + hdcode.Value + "'";
        else
            query = " r.MATER_CODE as 回填液编码,r.coat_scale as 比例,r.REMARK as 备注 from ht_qa_coat_formula_detail r left join ht_pub_materiel s on s.material_code = r.mater_code left join ht_qa_coat_formula t on t.formula_code = r.formula_code  where r.coat_flag = 'TPY' and r.is_del = '0' and r.is_valid = '1'  and t.PROD_CODE  = '" + hdcode.Value + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView2.DataSource = data;
        GridView2.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                ((DropDownList)GridView2.Rows[i].FindControl("listGridName2")).SelectedValue = mydrv["回填液编码"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtCodeM")).Text = mydrv["回填液编码"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtScale2")).Text = mydrv["比例"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtRemark")).Text = mydrv["备注"].ToString();


            }

        }
    }

    protected DataSet gridXJXLbind()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select material_code,material_name from ht_pub_materiel where mat_type = '香精香料' and is_del = '0' union select '' as material_code, '' as material_name from dual order by material_code desc");
    }

    protected DataSet gridHTYbind()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select material_code,material_name from ht_pub_materiel where mat_type = '香精香料' and is_del = '0' union select '' as material_code, '' as material_name from dual order by material_code desc");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)//没有实现
    {
        bindData();
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        hdcode.Value = txtCode.Text;

        {

            string[] seg = { "FORMULA_CODE", "FORMULA_NAME", "PROD_CODE", "STANDARD_VOL", "B_DATE", "E_DATE", "CONTROL_STATUS", "CREATE_ID", "CREATE_DATE", "CREATE_DEPT_ID", "REMARK" };
            string[] value = { txtCode.Text, txtName.Text, listPro.SelectedValue, txtVersion.Text, txtExeDate.Text, txtEndDate.Text, listStatus.SelectedValue, listCreator.SelectedValue, txtCrtDate.Text, listCrtApt.SelectedValue, txtDscpt.Text, };
            List<String> commandlist = new List<string>();
            commandlist.Add(opt.getMergeStr(seg, value, 1, "ht_qa_coat_formula"));
            commandlist.Add("update ht_pub_prod_design set coat_formula_code = '" + txtCode.Text + "' where prod_code = '" + listPro.SelectedValue + "'");
            string log_message = opt.TransactionCommand(commandlist) == "Success" ? "回填夜配方保存成功" : "回填夜配方保存失败";
            log_message += ",保存参数：" + string.Join(",", value);
            InsertTlog(log_message);
        }
        bindGrid1();
        bindGrid2();
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "updatetree", " window.parent.update();", true);
    }

    protected void listGirdName_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList list = (DropDownList)sender;
        int rowIndex = ((GridViewRow)list.NamingContainer).RowIndex;
        ((TextBox)GridView1.Rows[rowIndex].FindControl("txtCodeM")).Text = list.SelectedValue;
    }

    protected void btnAdd_Click(object sender, EventArgs e)  //没有实现
    {

        string query;
        if (hdcode.Value.Length == 8)
            query = "select r.MATER_CODE as 香料种类,r.coat_scale as 比例,r.need_size as 每罐调配所需 from ht_qa_coat_formula_detail r  left join ht_pub_materiel s on s.material_code = r.mater_code   where r.coat_flag = 'XJ' and r.is_del = '0' and r.is_valid = '1'  and r.formula_code  = '" + hdcode.Value + "'";
        else
            query = "select r.MATER_CODE as 香料种类,r.coat_scale as 比例,r.need_size as 每罐调配所需 from ht_qa_coat_formula_detail r  left join ht_pub_materiel s on s.material_code = r.mater_code left join ht_qa_coat_formula t on t.formula_code = r.formula_code  where r.coat_flag = 'XJ' and r.is_del = '0' and r.is_valid = '1'  and t.PROD_CODE  = '" + hdcode.Value + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet set = opt.CreateDataSetOra(query);
        DataTable data = set.Tables[0];
        if (data == null)
        {
            data = new DataTable();
            data.Columns.Add("香料种类");
            data.Columns.Add("比例");
            data.Columns.Add("每罐调配所需");

        }
        object[] value = { "", 0, 0 };
        data.Rows.Add(value);

        GridView1.DataSource = data;
        GridView1.DataBind();

        if (data != null && data.Rows.Count > 0)
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                DataRowView mydrv = data.DefaultView[i];
                ((DropDownList)GridView1.Rows[i].FindControl("listGridName")).SelectedValue = mydrv["香料种类"].ToString();
                ((TextBox)GridView1.Rows[i].FindControl("txtCodeM")).Text = mydrv["香料种类"].ToString();
                ((TextBox)GridView1.Rows[i].FindControl("txtScale")).Text = mydrv["比例"].ToString();
                ((TextBox)GridView1.Rows[i].FindControl("txtPercent")).Text = mydrv["每罐调配所需"].ToString();

            }

        }
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号  
            string mtr_code = ((TextBox)GridView1.Rows[Rowindex].FindControl("txtCodeM")).Text;
            string query = "update ht_qa_coat_formula_detail set IS_DEL = '1'  where FORMULA_CODE = '" + txtCode.Text + "' and MATER_CODE = '" + mtr_code + "'";
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string log_message = opt.UpDateOra(query) == "Success" ? "物料删除成功" : "物料删除失败";
            log_message += ",物料编号：" + txtCode.Text;
            InsertTlog(log_message);
            bindGrid1();
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
                    string mtr_code = ((DropDownList)GridView1.Rows[i].FindControl("listGridName")).SelectedValue;
                    string query = "update ht_qa_coat_formula_detail set IS_DEL = '1'  where FORMULA_CODE = '" + txtCode.Text + "' and MATER_CODE = '" + mtr_code + "'";
                    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                    string log_message = opt.UpDateOra(query) == "Success" ? "物料删除成功" : "物料删除失败";
                    log_message += "，物料编号：" + txtCode.Text;
                    InsertTlog(log_message);
                }
            }
            bindGrid1();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void btnGridSave_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (((CheckBox)row.FindControl("chk")).Checked)
                {
                    string mtr_code = ((DropDownList)row.FindControl("listGridName")).SelectedValue;
                    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                    string[] seg = { "FORMULA_CODE", "MATER_CODE", "coat_scale", "need_size", "coat_flag" };
                    string[] value = { txtCode.Text, mtr_code, ((TextBox)row.FindControl("txtScale")).Text, ((TextBox)row.FindControl("txtPercent")).Text, "XJ" };
                    string log_message = opt.MergeInto(seg, value, 2, "ht_qa_coat_formula_detail") == "Success" ? "物料保存成功" : "物料保存失败";
                    log_message += "，物料编号:" + txtCode.Text;
                    InsertTlog(log_message);
                }
            }
            bindGrid1();
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
            string mtr_code = ((DropDownList)GridView1.Rows[Rowindex].FindControl("listGridName")).SelectedValue;
            if (Rowindex >= 0)
            {
                MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

                string[] seg = { "FORMULA_CODE", "MATER_CODE", "coat_scale", "need_size", "coat_flag" };
                string[] value = { txtCode.Text, mtr_code, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtScale")).Text, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtPercent")).Text, "XJ" };
                string log_message = opt.MergeInto(seg, value, 2, "ht_qa_coat_formula_detail") == "Success" ? "物料保存成功" : "物料保存失败";
                log_message += "，物料编号:" + txtCode.Text;
                InsertTlog(log_message);
                bindGrid1();
            }
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }


    }
    protected void listGirdName2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList list = (DropDownList)sender;
        int rowIndex = ((GridViewRow)list.NamingContainer).RowIndex;
        ((TextBox)GridView2.Rows[rowIndex].FindControl("txtCodeM")).Text = list.SelectedValue;
    }


    protected void btnAdd2_Click(object sender, EventArgs e)
    {
        string query;
        if (hdcode.Value.Length == 8)
            query = "select r.MATER_CODE as 回填液编码,r.coat_scale as 比例,r.REMARK as 备注 from ht_qa_coat_formula_detail r where r.coat_flag = 'TPY' and r.is_del = '0' and r.is_valid = '1'  and r.formula_code  = '" + hdcode.Value + "'";
        else
            query = " r.MATER_CODE as 回填液编码,r.coat_scale as 比例,r.REMARK as 备注 from ht_qa_coat_formula_detail r left join ht_pub_materiel s on s.material_code = r.mater_code left join ht_qa_coat_formula t on t.formula_code = r.formula_code  where r.coat_flag = 'TPY' and r.is_del = '0' and r.is_valid = '1'  and t.PROD_CODE  = '" + hdcode.Value + "'";

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet set = opt.CreateDataSetOra(query);
        DataTable data = set.Tables[0];
        if (data == null)
        {
            data = new DataTable();
            data.Columns.Add("回填液编码");
            data.Columns.Add("比例");
            data.Columns.Add("备注");

        }
        object[] value = { "", 0, "" };
        data.Rows.Add(value);
        GridView2.DataSource = data;
        GridView2.DataBind();
        if (data != null && data.Rows.Count > 0)
        {
            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                DataRowView mydrv = data.DefaultView[i];
                ((DropDownList)GridView2.Rows[i].FindControl("listGridName2")).SelectedValue = mydrv["回填液编码"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtCodeM")).Text = mydrv["回填液编码"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtScale2")).Text = mydrv["比例"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtRemark")).Text = mydrv["备注"].ToString();

            }

        }
    }
    protected void btnDel2_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号  
            string mtr_code = ((DropDownList)GridView2.Rows[Rowindex].FindControl("listGridName")).SelectedValue;
            string query = "update ht_qa_coat_formula_detail set IS_DEL = '1'  where FORMULA_CODE = '" + txtCode.Text + "' and MATER_CODE = '" + mtr_code + "'";
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string log_message = opt.UpDateOra(query) == "Success" ? "物料删除成功" : "物料删除失败";
            log_message += ",物料编号:" + txtCode.Text;
            InsertTlog(log_message);
            bindGrid2();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnCkAll2_Click(object sender, EventArgs e)
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
    protected void btnDelSel2_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked)
                {
                    string mtr_code = ((DropDownList)GridView2.Rows[i].FindControl("listGridName2")).SelectedValue;
                    string query = "update ht_qa_coat_formula_detail set IS_DEL = '1'  where FORMULA_CODE = '" + txtCode.Text + "' and MATER_CODE = '" + mtr_code + "'";
                    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                    string log_message = opt.UpDateOra(query) == "Success" ? "物料删除成功" : "物料删除失败";
                    log_message += ",物料编号：" + txtCode.Text;
                    InsertTlog(log_message);
                }
            }
            bindGrid2();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void btnGridSave2_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView2.Rows)
        {
            if (((CheckBox)row.FindControl("chk")).Checked)
            {

                string mtr_code = ((DropDownList)row.FindControl("listGridName2")).SelectedValue;

                MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

                string[] seg = { "FORMULA_CODE", "MATER_CODE", "coat_scale", "REMARK", "coat_flag" };
                string[] value = { txtCode.Text, mtr_code, ((TextBox)row.FindControl("txtScale2")).Text, ((TextBox)row.FindControl("txtRemark")).Text, "TPY" };
                string log_message = opt.MergeInto(seg, value, 2, "ht_qa_coat_formula_detail") == "Success" ? "物料保存成功" : "物料保存失败";
                log_message += ",物料编号：" + txtCode.Text;
                InsertTlog(log_message);
            }

        }
        bindGrid2();
    }
    protected void btnSave2_Click(object sender, EventArgs e)
    {

        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;

        string mtr_code = ((DropDownList)row.FindControl("listGridName2")).SelectedValue;
        if (row.RowIndex >= 0)
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

            string[] seg = { "FORMULA_CODE", "MATER_CODE", "coat_scale", "REMARK", "coat_flag" };
            string[] value = { txtCode.Text, mtr_code, ((TextBox)row.FindControl("txtScale2")).Text, ((TextBox)row.FindControl("txtRemark")).Text, "TPY" };
            string log_message = opt.MergeInto(seg, value, 2, "ht_qa_coat_formula_detail") == "Success" ? "物料保存成功" : "物料保存失败";
            log_message += ",物料编号：" + txtCode.Text;
            InsertTlog(log_message);
            bindGrid2();
        }
    }


    protected void btnAddR_Click(object sender, EventArgs e)
    {


        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string str = opt.GetSegValue("select Max(Formula_code) as code from ht_qa_coat_formula ", "CODE");
        if (str == "")
            str = "00000000";
        txtCode.Text = "70308" + (Convert.ToInt16(str.Substring(5)) + 1).ToString().PadLeft(3, '0');
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