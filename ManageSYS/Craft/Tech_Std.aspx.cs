using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Craft_Tech_Std : MSYS.Web.BasePage
{    
    protected string subtvHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
           opt.bindDropDownList(listVersion, "select TECH_CODE,TECH_NAME from HT_TECH_STDD_CODE where is_valid = '1' and is_del= '0'", "TECH_NAME", "TECH_CODE");

            opt.bindDropDownList(listCrtApt, "select F_CODE,F_NAME from HT_SVR_ORG_GROUP", "F_NAME", "F_CODE");
            opt.bindDropDownList(listtech, "select * from HT_TECH_STDD_CODE where is_valid = '1' and is_del = '0'", "TECH_NAME", "TECH_CODE");
            opt.bindDropDownList(listtechC, "select * from HT_TECH_STDD_CODE where is_valid = '1' and is_del = '0'", "TECH_NAME", "TECH_CODE");
            opt.bindDropDownList(listStatus, "select * from HT_INNER_BOOL_DISPLAY t", "CTRL_NAME", "ID");
            opt.bindDropDownList(listCreator, "select id,name from  ht_svr_user    order by id ", "Name", "ID");

            opt.bindDropDownList(listAprv, "select * from ht_inner_aprv_status ", "NAME", "ID");
        
            subtvHtml = InitTreePrcss();
        }
    }

    protected void SetEnable(bool enable)
    {
     //   btnAdd.Visible = enable;
     //   btnModify.Visible = enable;
     ////   btnDelete.Visible = enable;
     //   btnSave2.Visible = enable;
     //   btnDelSel2.Visible = enable;
     //   btnSave.Visible = enable;
     //   btnDelSel.Visible = enable;
    }
    protected void initView()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listVersion, "select TECH_CODE,TECH_NAME from HT_TECH_STDD_CODE where is_valid = '1' and is_del= '0'", "TECH_NAME", "TECH_CODE");
        opt.bindDropDownList(listtech, "select * from HT_TECH_STDD_CODE where is_valid = '1' and is_del = '0'", "TECH_NAME", "TECH_CODE");
        opt.bindDropDownList(listtechC, "select * from HT_TECH_STDD_CODE where is_valid = '1' and is_del = '0'", "TECH_NAME", "TECH_CODE");
    }
    public string InitTreePrcss()
    {

       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
       DataSet data = opt.CreateDataSetOra("select distinct r.section_code,r.section_name  from ht_pub_tech_section r left join ht_pub_tech_para s on substr(s.para_code,1,5) = r.section_code and s.is_del = '0' and s.is_valid = '1' where r.is_del = '0' and r.is_valid = '1' and ( s.para_type like '_1%' or s.para_type like '__1%')  order by r.section_code");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul id='browser' class='filetree treeview-famfamfam'>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                 
                tvHtml += "<li ><span class='folder'  onclick = \"tabClick(" + row["section_code"].ToString() + ")\">" + row["section_name"].ToString() + "</span></a>";
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select * from HT_TECH_STDD_CODE_DETAIL where TECH_CODE = '" + listtech.SelectedValue + "' and is_del = '0'";
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in data.Tables[0].Rows)
            {
                string[] seg = { "PARA_CODE", "VALUE", "UPPER_LIMIT", "LOWER_LIMIT", "EER_DEV", "UNIT", "TECH_CODE" };
                string[] value = { row["PARA_CODE"].ToString(), row["VALUE"].ToString(), row["UPPER_LIMIT"].ToString(), row["LOWER_LIMIT"].ToString(), row["EER_DEV"].ToString(), row["UNIT"].ToString(), listtechC.SelectedValue };
                string log_message = opt.InsertData(seg, value, "HT_TECH_STDD_CODE_DETAIL") == "Success" ? "复制标准成功" : "复制标准失败";
                log_message += ",复制数据：" + string.Join(",", value);
                InsertTlog(log_message);

            }

        }

        bindGrid(listtechC.SelectedValue, hideprc.Value);
        bindGrid2(listtechC.SelectedValue, hideprc.Value);
        initView();

    }
    //保存标准版本
    protected void btnModify_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        {

            string[] seg = { "TECH_CODE", "TECH_NAME", "STANDARD_VOL", "B_DATE", "E_DATE", "CONTROL_STATUS", "CREATE_ID", "CREATE_DATE", "CREATE_DEPT_ID", "REMARK" };
            string[] value = { txtCode.Text, txtName.Text, txtVersion.Text, txtExeDate.Text, txtEndDate.Text, listStatus.SelectedValue, listCreator.SelectedValue, txtCrtDate.Text, listCrtApt.SelectedValue, txtDscpt.Text };
            string log_message = opt.MergeInto(seg, value, 1, "HT_TECH_STDD_CODE") == "Success" ? "保存标准成功" : "保存标准失败";
            log_message += ",保存数据：" + string.Join(",", value);
            InsertTlog(log_message);
            ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "alert", "alert('" + log_message + "')", true);
        }
        bindGrid(txtCode.Text, hideprc.Value);      
       
        listAprv.SelectedValue = "-1";
        initView();
       


    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        List<String> commandlist = new List<string>();
        commandlist.Add("delete HT_TECH_STDD_CODE  where TECH_CODE = '" + txtCode.Text + "'");
        commandlist.Add("delete from ht_pub_aprv_flowinfo where BUSIN_ID = '" + txtCode.Text + "'");
        string log_message = opt.TransactionCommand(commandlist) == "Success" ? "删除工艺标准成功" : "删除工艺标准失败";
        log_message += "--标识:" + txtCode.Text;
        InsertTlog(log_message);
        initView();
    }
    protected void bindData(string rcpcode)
    {
        string query = "select tech_code  as 标准编码,tech_name  as 标准名称,PROD_CODE  as 产品编码,STANDARD_VOL  as 标准版本号,B_DATE  as 执行日期,E_DATE  as 结束日期,CONTROL_STATUS  as 受控状态,CREATE_ID  as 编制人,CREATE_DATE  as 编制日期,CREATE_DEPT_ID  as 编制部门,REMARK  as 备注,is_valid ,FLOW_STATUS from ht_tech_stdd_code where is_del = '0' and tech_code  = '" + rcpcode + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            txtCode.Text = rcpcode;
            txtName.Text = data.Tables[0].Rows[0]["标准名称"].ToString();
            txtVersion.Text = data.Tables[0].Rows[0]["标准版本号"].ToString();
            txtExeDate.Text = data.Tables[0].Rows[0]["执行日期"].ToString();
            txtEndDate.Text = data.Tables[0].Rows[0]["结束日期"].ToString();
            listStatus.SelectedValue = data.Tables[0].Rows[0]["受控状态"].ToString();
            listCreator.SelectedValue = data.Tables[0].Rows[0]["编制人"].ToString();
            txtCrtDate.Text = data.Tables[0].Rows[0]["编制日期"].ToString();
            listCrtApt.SelectedValue = data.Tables[0].Rows[0]["编制部门"].ToString();
            txtDscpt.Text = data.Tables[0].Rows[0]["备注"].ToString();
            ckValid.Checked = ("1" == data.Tables[0].Rows[0]["is_valid"].ToString());
            listAprv.SelectedValue = data.Tables[0].Rows[0]["FLOW_STATUS"].ToString();
            if (!(listAprv.SelectedItem.Text == "未提交" || listAprv.SelectedItem.Text == "未通过"))
            {
                btnSubmit.Enabled = false;
                btnSubmit.CssClass = "btngrey";
                SetEnable(false);

            }
            else
            {
                btnSubmit.Enabled = true;
                btnSubmit.CssClass = "btn1 auth";
                SetEnable(true);
            }
        }
        bindGrid(rcpcode, hideprc.Value);
        bindGrid2(rcpcode, hideprc.Value);
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        bindData(txtCode.Text);

    }
    protected void UpdateGrid_Click(object sender, EventArgs e)
    {
        bindGrid(txtCode.Text, hideprc.Value);
        bindGrid2(txtCode.Text, hideprc.Value);
    }
   
    protected void bindGrid(string rcpcode, string prccode)
    {
        if (prccode.Length == 5)
        {

            string query = "select r.PARA_CODE as 参数编码,r.VALUE as 标准值,r.UPPER_LIMIT as 上限,r.LOWER_LIMIT as 下限,r.EER_DEV as 允差,s.para_unit as 单位,s.BUSS_ID  from ht_tech_stdd_code_detail r left join ht_pub_tech_para s on s.para_code = r.para_code  where s.para_type like '__1%' and  r.IS_DEL = '0' and r.tech_code = '" + rcpcode + "' and   substr(r.PARA_CODE,1,5) = '" + prccode + "' union select PARA_CODE as 参数编码,0 as 标准值,0 as 上限,0 as 下限,0 as 允差,'' as 单位,BUSS_ID   from ht_pub_tech_para where substr(para_code,1,5) = '" + prccode + "' and para_code in   (select para_code from ht_pub_tech_para where substr(para_code,1,5) = '" + prccode + "' and para_type like '__1%' and is_del ='0' minus select para_code from ht_tech_stdd_code_detail where IS_DEL = '0' and    tech_code = '" + rcpcode + "' and   substr(PARA_CODE,1,5) = '" + prccode + "')";

            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataSet set = opt.CreateDataSetOra(query);
            DataTable data = set.Tables[0];

            GridView1.DataSource = data;
            GridView1.DataBind();
            if (data != null && data.Rows.Count > 0)
            {
                for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
                {
                    DataRowView mydrv = data.DefaultView[i];
                    DropDownList list = (DropDownList)GridView1.Rows[i].FindControl("listParaName");
                    opt.bindDropDownList(list, "select r.para_code,case when length(r.para_name) >= 8 then r.para_name else  s.eq_name ||r.para_name end as name ,r.equip_code from ht_pub_tech_para r left join ht_eq_eqp_tbl s  on  r.equip_code = s.idkey where  r.is_del = '0' and substr(r.PARA_CODE,1,5) = '" + prccode + "'", "name", "PARA_CODE");
                    ((TextBox)GridView1.Rows[i].FindControl("txtCodeM")).Text = mydrv["参数编码"].ToString();
                    list.SelectedValue = mydrv["参数编码"].ToString();
                    ((TextBox)GridView1.Rows[i].FindControl("txtValueM")).Text = mydrv["标准值"].ToString();
                    ((TextBox)GridView1.Rows[i].FindControl("txtUlimitM")).Text = mydrv["上限"].ToString();
                    ((TextBox)GridView1.Rows[i].FindControl("txtLlimitM")).Text = mydrv["下限"].ToString();
                    ((TextBox)GridView1.Rows[i].FindControl("txtDevM")).Text = mydrv["允差"].ToString();
                    ((TextBox)GridView1.Rows[i].FindControl("txtUnitM")).Text = mydrv["单位"].ToString();

                    list.Enabled = false;
                    if (mydrv["BUSS_ID"].ToString() == ((MSYS.Data.SysUser)Session["User"]).OwningBusinessUnitId || ((MSYS.Data.SysUser)Session["User"]).UserRole == "系统管理员")
                    {
                        ((TextBox)GridView1.Rows[i].FindControl("txtCodeM")).Enabled = true;
                        ((TextBox)GridView1.Rows[i].FindControl("txtValueM")).Enabled = true;
                        ((TextBox)GridView1.Rows[i].FindControl("txtUlimitM")).Enabled = true;
                        ((TextBox)GridView1.Rows[i].FindControl("txtLlimitM")).Enabled = true;
                        ((TextBox)GridView1.Rows[i].FindControl("txtDevM")).Enabled = true;
                        ((TextBox)GridView1.Rows[i].FindControl("txtUnitM")).Enabled = true;
                    }
                    else
                    {
                        ((TextBox)GridView1.Rows[i].FindControl("txtCodeM")).Enabled = false;
                        ((TextBox)GridView1.Rows[i].FindControl("txtValueM")).Enabled = false;
                        ((TextBox)GridView1.Rows[i].FindControl("txtUlimitM")).Enabled = false;
                        ((TextBox)GridView1.Rows[i].FindControl("txtLlimitM")).Enabled = false;
                        ((TextBox)GridView1.Rows[i].FindControl("txtDevM")).Enabled = false;
                        ((TextBox)GridView1.Rows[i].FindControl("txtUnitM")).Enabled = false;
                    }

                }

            }
        }

    }
    protected void bindGrid2(string rcpcode, string prccode)
    {
        if (prccode.Length == 5)
        {

            string query = "select r.PARA_CODE as 参数编码,r.VALUE as 标准值,r.UPPER_LIMIT as 上限,r.LOWER_LIMIT as 下限,r.EER_DEV as 允差,r.UNIT as 单位 ,s.BUSS_ID  from ht_tech_stdd_code_detail r left join ht_pub_tech_para s on s.para_code = r.para_code  where s.para_type like '_1%' and  r.IS_DEL = '0' and r.tech_code = '" + rcpcode + "' and   substr(r.PARA_CODE,1,5) = '" + prccode + "' union select PARA_CODE as 参数编码,0 as 标准值,0 as 上限,0 as 下限,0 as 允差,'' as 单位 ,BUSS_ID  from ht_pub_tech_para where substr(para_code,1,5) = '" + prccode + "' and para_code in   (select para_code from ht_pub_tech_para where substr(para_code,1,5) = '" + prccode + "' and para_type like '_1%' and is_del ='0' minus select para_code from ht_tech_stdd_code_detail where IS_DEL = '0' and    tech_code = '" + rcpcode + "' and   substr(PARA_CODE,1,5) = '" + prccode + "')";

            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataSet set = opt.CreateDataSetOra(query);
            DataTable data = set.Tables[0];

            GridView2.DataSource = data;
            GridView2.DataBind();
            if (data != null && data.Rows.Count > 0)
            {
                for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
                {
                    DataRowView mydrv = data.DefaultView[i];
                    DropDownList list = (DropDownList)GridView2.Rows[i].FindControl("listParaName");
                    opt.bindDropDownList(list, "select r.para_code,case when length(r.para_name) >= 8 then r.para_name else  s.eq_name ||r.para_name end as name ,r.equip_code from ht_pub_tech_para r left join ht_eq_eqp_tbl s  on  r.equip_code = s.idkey where  r.is_del = '0' and substr(r.PARA_CODE,1,5) = '" + prccode + "'", "name", "PARA_CODE");
                    ((TextBox)GridView2.Rows[i].FindControl("txtCodeM")).Text = mydrv["参数编码"].ToString();
                    list.SelectedValue = mydrv["参数编码"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtValueM")).Text = mydrv["标准值"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtUlimitM")).Text = mydrv["上限"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtLlimitM")).Text = mydrv["下限"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtDevM")).Text = mydrv["允差"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtUnitM")).Text = mydrv["单位"].ToString();
                    list.Enabled = false;
                    if (mydrv["BUSS_ID"].ToString() == ((MSYS.Data.SysUser)Session["User"]).OwningBusinessUnitId || ((MSYS.Data.SysUser)Session["User"]).UserRole == "系统管理员")
                    {
                        ((TextBox)GridView2.Rows[i].FindControl("txtCodeM")).Enabled = true;
                        ((TextBox)GridView2.Rows[i].FindControl("txtValueM")).Enabled = true;
                        ((TextBox)GridView2.Rows[i].FindControl("txtUlimitM")).Enabled = true;
                        ((TextBox)GridView2.Rows[i].FindControl("txtLlimitM")).Enabled = true;
                        ((TextBox)GridView2.Rows[i].FindControl("txtDevM")).Enabled = true;
                        ((TextBox)GridView2.Rows[i].FindControl("txtUnitM")).Enabled = true;
                    }
                    else
                    {
                        ((TextBox)GridView2.Rows[i].FindControl("txtCodeM")).Enabled = false;
                        ((TextBox)GridView2.Rows[i].FindControl("txtValueM")).Enabled = false;
                        ((TextBox)GridView2.Rows[i].FindControl("txtUlimitM")).Enabled = false;
                        ((TextBox)GridView2.Rows[i].FindControl("txtLlimitM")).Enabled = false;
                        ((TextBox)GridView2.Rows[i].FindControl("txtDevM")).Enabled = false;
                        ((TextBox)GridView2.Rows[i].FindControl("txtUnitM")).Enabled = false;
                    }

                }

            }
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
    protected void btnCkAll2_Click(object sender, EventArgs e)
    {
        int ckno = 0;
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            if (((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked)
                ckno++;
        }
        bool check = (ckno < GridView2.Rows.Count);
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            ((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked = check;

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
                    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                    string log_message = opt.UpDateOra(query) == "Success" ? "参数删除成功" : "参数删除失败";
                    log_message += "，参数编码：" + txtCode.Text;
                    InsertTlog(log_message);
                }
            }
            bindGrid(txtCode.Text, hideprc.Value);
            bindGrid2(txtCode.Text, hideprc.Value);
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
                    string mtr_code = ((TextBox)GridView2.Rows[i].FindControl("txtCodeM")).Text;
                    string query = "update HT_TECH_STDD_CODE_DETAIL set IS_DEL = '1'  where TECH_CODE = '" + txtCode.Text + "' and PARA_CODE = '" + mtr_code + "'";
                    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                    string log_message = opt.UpDateOra(query) == "Success" ? "参数删除成功" : "参数删除失败";
                    log_message += "，参数编码：" + txtCode.Text;
                    InsertTlog(log_message);
                }
            }
            bindGrid(txtCode.Text, hideprc.Value);
            bindGrid2(txtCode.Text, hideprc.Value);
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
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string log_message = opt.UpDateOra(query) == "Success" ? "删除参数成功" : "删除参数失败";
            log_message += ",参数编码" + txtCode.Text;
            InsertTlog(log_message);
            bindGrid(txtCode.Text, hideprc.Value);
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string suc = "保存成功";
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            GridViewRow row = GridView1.Rows[i];
            string mtr_code = ((TextBox)row.FindControl("txtCodeM")).Text;

            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

            {
                string[] seg = { "PARA_CODE", "TECH_CODE", "VALUE", "UPPER_LIMIT", "LOWER_LIMIT", "EER_DEV", "UNIT", "IS_DEL" };
                string[] value = { ((TextBox)row.FindControl("txtCodeM")).Text, txtCode.Text, ((TextBox)row.FindControl("txtValueM")).Text, ((TextBox)row.FindControl("txtUlimitM")).Text, ((TextBox)row.FindControl("txtLlimitM")).Text, ((TextBox)row.FindControl("txtDevM")).Text, ((TextBox)row.FindControl("txtUnitM")).Text ,"0"};
                string log_message;
                if (opt.MergeInto(seg, value, 2, "HT_TECH_STDD_CODE_DETAIL") == "Success")
                    log_message = "保存参数成功";
                else
                {
                    log_message = "保存参数失败";
                    suc = "部分参数保存失败，请检查！！";
                }
                log_message += ", 保存数据：" + string.Join(",", value);
                InsertTlog(log_message);
              
            }
        }
        bindGrid(txtCode.Text, hideprc.Value);
        bindGrid2(txtCode.Text, hideprc.Value);
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "alert", "alert('" + suc + "')", true);
    }
    protected void btnSave2_Click(object sender, EventArgs e)
    {
        string suc = "保存成功";
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            GridViewRow row = GridView2.Rows[i];
            string mtr_code = ((TextBox)row.FindControl("txtCodeM")).Text;

            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

            {
                string[] seg = { "PARA_CODE", "TECH_CODE", "VALUE", "UPPER_LIMIT", "LOWER_LIMIT", "EER_DEV", "UNIT","IS_DEL" };
                string[] value = { ((TextBox)row.FindControl("txtCodeM")).Text, txtCode.Text, ((TextBox)row.FindControl("txtValueM")).Text, ((TextBox)row.FindControl("txtUlimitM")).Text, ((TextBox)row.FindControl("txtLlimitM")).Text, ((TextBox)row.FindControl("txtDevM")).Text, ((TextBox)row.FindControl("txtUnitM")).Text ,"0"};
                string log_message;
                if (opt.MergeInto(seg, value, 2, "HT_TECH_STDD_CODE_DETAIL") == "Success")
                    log_message = "保存参数成功";
                else
                {
                    log_message = "保存参数失败";
                    suc = "部分参数保存失败，请检查！！";
                }
                log_message += ", 保存数据：" + string.Join(",", value);
                InsertTlog(log_message);
               
            }
        }
        bindGrid(txtCode.Text, hideprc.Value);
        bindGrid2(txtCode.Text, hideprc.Value);
        ScriptManager.RegisterStartupScript(UpdatePanel5, this.Page.GetType(), "alert", "alert('" + suc + "')", true);

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string str = opt.GetSegValue("select nvl(Max(substr(tech_CODE,7,3)),0)+1 as code  from ht_tech_stdd_code t where is_del='0'", "CODE");

        txtCode.Text = "TCH703" + str.PadLeft(3, '0');
        MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
        listCreator.SelectedValue = user.id;
        txtCrtDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
        listCrtApt.SelectedValue = user.OwningBusinessUnitId;
        btnSubmit.Enabled = true;
        btnSubmit.CssClass = "btn1 auth";
        txtName.Text = "";
        txtVersion.Text = "";
        txtExeDate.Text = "";
        txtEndDate.Text = "";
        listStatus.SelectedValue = "";
        txtDscpt.Text = "";
        ckValid.Checked = false;
        listAprv.SelectedValue = "";

    }

    protected void btnFLow_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;

        string ID = txtCode.Text;
        string query = "select pos as 顺序号, workitemid as 审批环节,username as 负责人,comments as 意见,opiniontime 审批时间,(case status when '0' then '未审批'  when '1' then '未通过' else '己通过' end)  as 审批状态  from ht_pub_aprv_opinion r left join ht_pub_aprv_flowinfo s on r.gongwen_id = s.id where s.busin_id  = '" + ID + "' order by pos";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        GridView3.DataSource = opt.CreateDataSetOra(query);
        GridView3.DataBind();
        ScriptManager.RegisterStartupScript(UpdatePanel4, this.Page.GetType(), "", "$('#flowinfo').fadeIn(200);", true);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)//提交审批
    {

        /*启动审批TB_ZT标题,MODULENAME审批类型编码,BUSIN_ID业务数据id,URL 单独登录url*/
        //"TB_ZT", "MODULENAME", "BUSIN_ID",  "URL"
        string[] subvalue = { "工艺标准:" + txtName.Text, "04", txtCode.Text, Page.Request.UserHostName.ToString() };
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        if (MSYS.AprvFlow.createApproval(subvalue))
        {
            string log_message = ",工艺标准:" + txtName.Text + "提交审批成功";
            listAprv.SelectedValue = "0";
            InsertTlog(log_message);
            btnSubmit.Enabled = false;
            btnSubmit.CssClass = "btngrey";

        }
    }
    protected void listVersion_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindData(listVersion.SelectedValue);
       
    }


}