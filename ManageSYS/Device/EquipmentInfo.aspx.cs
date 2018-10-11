using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Collections;
public partial class Device_EquipmentInfo :MSYS.Web.BasePage
{
    protected string tvHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {  
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listMGdept, "select * from ht_svr_org_group", "F_NAME", "F_CODE");
            opt.bindDropDownList(listUseDept, "select * from ht_svr_org_group", "F_NAME", "F_CODE");
            opt.bindDropDownList(listSection, "select section_code,section_name from ht_pub_tech_section where is_del = '0' and is_valid = '1' order by section_code", "section_name", "section_code");
            opt.bindDropDownList(listSectionM, "select section_code,section_name from ht_pub_tech_section where is_del = '0' and is_valid = '1'  order by section_code", "section_name", "section_code");
            tvHtml = InitTree();
            try
            {
                string eqcode = Request["idkey"].ToString();
                if (eqcode != "")
                {
                    bindData(eqcode);
                }
            }
            catch
            { }
        }
    }
    public string InitTree()
    {

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select g.section_code,g.section_name from ht_pub_tech_section g where g.IS_VALID = '1' and g.IS_DEL = '0' order by g.section_code ");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul id='browser' class='filetree treeview-famfamfam'>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                tvHtml += "<li ><span class='folder'  onclick = \"treeClick(" + row["section_code"].ToString() + ")\">" + row["section_name"].ToString() + "</span>";              
               // tvHtml += InitTreeEquip(row["section_code"].ToString());
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }



    public string InitTreeEquip(string section_code)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select IDKEY,EQ_NAME from ht_eq_eqp_tbl where section_code  = '" + section_code + "' and IS_VALID = '1' and IS_DEL = '0' order by IDKEY");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {

                tvHtml += "<li ><span class='folder'  onclick = \"treeClick(" + row["IDKEY"].ToString() + ")\">" + row["EQ_NAME"].ToString() + "</span>";               
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        if (hdcode.Value.Length == 5)
        {
            listSectionM.SelectedValue = hdcode.Value;
            bindGrid();
        }
        else
            bindData(hdcode.Value);
    }

    protected void bindGrid()
    {
        
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
      //  string query = "select g.IDKEY as 设备编号,g.EQ_NAME as 设备名称,g.EQ_TYPE as 企业设备分类,g.EQ_STATUS as 设备状态,g.ZG_DATE as 转固日期,g.EQ_MODEL as 设备型号,g.USED_DATE as 投入使用日期,g.RATED_POWER as 额定生产能力,g.POWER_UNIT as 能力单位,g1.f_name as 设备管理部门, g2.f_name as 设备使用部门 from ht_eq_eqp_tbl g left join ht_svr_org_group g1 on g1.f_code = g.mgt_dept_code left join ht_svr_org_group g2 on g2.f_code = g.use_dept_code where g.is_del = '0' and g.is_valid = '1'";
        string query = "select g.IDKEY as 设备编号,g.EQ_NAME as 设备名称,g.EQ_TYPE as 企业设备分类,g.EQ_STATUS as 设备状态,g.EQ_MODEL as 设备型号,g.USED_DATE as 投入使用日期,g.RATED_POWER as 额定生产能力,g.POWER_UNIT as 能力单位  from ht_eq_eqp_tbl g  where g.is_del = '0' and g.is_valid = '1'";
        if (listSectionM.SelectedValue != "")
        {
            query += " and g.section_code =  '" + listSectionM.SelectedValue + "'";           
        }
        if (txtType.Text != "")
            query += " and g.EQ_TYPE = '" + txtType.Text + "'";
        DataSet data = opt.CreateDataSetOra(query);

        GridView1.DataSource = data;
        GridView1.DataBind();

    }


    protected void bindData(string eqcode)
    {

        string query = "select * from HT_EQ_EQP_TBL where  IDKEY = '" + eqcode + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            DataRow row = data.Tables[0].Rows[0];
            txtIDKey.Text = eqcode;
            //txtCLS.Text = row["CLS_CODE"].ToString();

            txtEqname.Text = row["EQ_NAME"].ToString();
            txtSGSCode.Text = row["SGS_CODE"].ToString();
            txtNCCode.Text = row["NC_CODE"].ToString();
            txtFncName.Text = row["FINANCE_EQ_NAME"].ToString();
            txtEQType.Text = row["EQ_TYPE"].ToString();
            listEQStatus.SelectedValue = row["EQ_STATUS"].ToString();
            txtZGDate.Text = row["ZG_DATE"].ToString();
            txtEQModel.Text = row["EQ_MODEL"].ToString();
            txtOriWorth.Text = row["ORI_WORTH"].ToString();
            txtNetWorth.Text = row["NET_WORTH"].ToString();
            txtUsedDate.Text = row["USED_DATE"].ToString();
            txtRatedPower.Text = row["RATED_POWER"].ToString();
            txtRealPower.Text = row["REAL_POWER"].ToString();
            txtPowerUnit.Text = row["POWER_UNIT"].ToString();
            txtOwner.Text = row["OWNER_NAME"].ToString();
            txtEQSource.Text = row["EQP_FROM"].ToString();
            txtOriOwner.Text = row["ORI_OWNER_NAME"].ToString();
            txtManufct.Text = row["MANUFACTURER"].ToString();
            txtSerialNo.Text = row["SERIAL_NUMBER"].ToString();
            txtSupplier.Text = row["SUPPLIER"].ToString();
            rdSpecEQ.Checked = ("1" == row["IS_SPEC_EQP"].ToString());
            rdMadeChina.Checked = ("1" == row["IS_MADEINCHINA"].ToString());
            listMGdept.SelectedValue = row["MGT_DEPT_CODE"].ToString();
            listUseDept.SelectedValue = row["USE_DEPT_CODE"].ToString();
            txtDutier.Text = row["DUTY_NAME"].ToString();
            txtIp.Text = row["EQP_IP"].ToString();
            txtMAC.Text = row["EQP_MAC"].ToString();
            txtSN.Text = row["EQP_SN"].ToString();
            txtOpSYS.Text = row["EQP_SYS"].ToString();
            txtDscpt.Text = row["REMARK"].ToString();
            listSection.SelectedValue = row["SECTION_CODE"].ToString();
            ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "showinfo", " $('.shade').fadeIn(200);", true);
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
   
    protected void btnAdd_Click(object sender,EventArgs e)
    {
        setBlank();
       MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        txtIDKey.Text = listSectionM.SelectedValue + opt.GetSegValue("select nvl(max(substr(idkey,6,3)),0)+1 as code from ht_eq_eqp_tbl where SECTION_CODE = '" + listSectionM.SelectedValue + "'", "CODE").PadLeft(3, '0');
        listSection.SelectedValue = listSectionM.SelectedValue;
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "showinfo", " $('.shade').fadeIn(200);", true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid();
    }

    protected void btnDelSel_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked)
                {
                    string id = GridView1.DataKeys[i].Value.ToString();
                    string query = "update HT_EQ_EQP_TBL set IS_DEL = '1'  where IDKEY = '" + id + "'";
                   MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
                   string log_message = opt.UpDateOra(query) == "Success" ? "删除设备成功" : "删除设备失败";
                   log_message += "--标识:" + id;
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

    protected void btnCkAll_Click(object sender, EventArgs e)
    {       
            int ckno = 0;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked)
                    ckno++;
            }
            bool check = (ckno < GridView1.Rows.Count);
            for (int i = 0; i < GridView1.Rows.Count ; i++)
            {
                ((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked = check;
                   
            }
       
    }
    protected void btnGridview_Click(object sender, EventArgs e)//查看设备详情
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string code = GridView1.DataKeys[rowIndex].Value.ToString();
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "addequip", " $('#tabs').tabs('select', '设备详情');", true);
        bindData(code);
        
    }
    protected void btnGridrh_Click(object sender, EventArgs e)//查看润滑记录
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string code = GridView1.DataKeys[rowIndex].Value.ToString();
    }
    protected void btnGridwb_Click(object sender, EventArgs e)//查看维保记录
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string code = GridView1.DataKeys[rowIndex].Value.ToString();
    }
    /// <summary>
    /// /tab2操作
    /// </summary>
    /// <param name="eqcode"></param>
 

    protected void setBlank()
    {
        txtIDKey.Text = "";
        txtCLS.Text = "";
        txtEqname.Text = "";
        txtSGSCode.Text = "";
        txtNCCode.Text = "";
        txtFncName.Text = "";
        txtEQType.Text = "";
        listEQStatus.SelectedValue = "";
        txtZGDate.Text = "";
        txtEQModel.Text = "";
        txtOriWorth.Text = "";
        txtNetWorth.Text = "";
        txtUsedDate.Text = "";
        txtRatedPower.Text = "";
        txtRealPower.Text = "";
        txtPowerUnit.Text = "";
        txtOwner.Text = "";
        txtEQSource.Text = "";
        txtOriOwner.Text = "";
        txtManufct.Text = "";
        txtSerialNo.Text = "";
        txtSupplier.Text = "";
        rdSpecEQ.Checked = false;
        rdMadeChina.Checked = false;
        listMGdept.SelectedValue = "";
        listUseDept.SelectedValue = "";
        txtDutier.Text = "";
        txtIp.Text = "";
        txtMAC.Text = "";
        txtSN.Text = "";
        txtOpSYS.Text = "";
        txtDscpt.Text = "";
        listSection.SelectedValue = "";
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        setBlank();
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {       
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();      
      
       string[] seg = { "IDKEY", "CLS_CODE", "EQ_NAME", "SGS_CODE", "NC_CODE", "FINANCE_EQ_NAME", "EQ_TYPE", "EQ_STATUS", "ZG_DATE", "EQ_MODEL", "ORI_WORTH", "NET_WORTH", " USED_DATE", "RATED_POWER", "REAL_POWER", "POWER_UNIT", "OWNER_NAME", "EQP_FROM", "ORI_OWNER_NAME", "MANUFACTURER", "SERIAL_NUMBER", "SUPPLIER", "IS_SPEC_EQP", "IS_MADEINCHINA", "MGT_DEPT_CODE", "USE_DEPT_CODE", "DUTY_NAME", "EQP_IP", " EQP_MAC", "EQP_SN", "EQP_SYS", "REMARK", "CREATOR", "CREATE_TIME", "SECTION_CODE" };
        string[] value = { txtIDKey.Text, txtCLS.Text, txtEqname.Text, txtSGSCode.Text, txtNCCode.Text, txtFncName.Text, txtEQType.Text, listEQStatus.SelectedValue, txtZGDate.Text, txtEQModel.Text, txtOriWorth.Text, txtNetWorth.Text, txtUsedDate.Text, txtRatedPower.Text, txtRealPower.Text, txtPowerUnit.Text, txtOwner.Text, txtEQSource.Text, txtOriOwner.Text, txtManufct.Text, txtSerialNo.Text, txtSupplier.Text, Convert.ToInt16(rdSpecEQ.Checked).ToString(), Convert.ToInt16(rdMadeChina.Checked).ToString(), listMGdept.SelectedValue, listUseDept.SelectedValue, txtDutier.Text, txtIp.Text, txtMAC.Text, txtSN.Text, txtOpSYS.Text, txtDscpt.Text, "", System.DateTime.Now.ToString("yyyy-MM-hh"), listSection.SelectedValue };


        string log_message = opt.MergeInto(seg, value, 1, "HT_EQ_EQP_TBL") == "Success" ? "保存设备信息成功" : "保存设备信息失败";
        log_message += "--详情：" + string.Join(",", value);
        InsertTlog(log_message);

        bindGrid();

        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "close", "$('.shade').fadeOut(100)", true);

    }
 
    /// <summary>
    /// tab3操作
    /// </summary>
    /// <param name="nodecode"></param>
    /// <param name="Nd"></param>
  

 
  

  
}