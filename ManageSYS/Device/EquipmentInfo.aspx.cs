using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

public partial class Device_EquipmentInfo :MSYS.Web.BasePage
{   
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {  
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listMGdept, "select * from ht_svr_org_group", "F_NAME", "F_CODE");
            opt.bindDropDownList(listUseDept, "select * from ht_svr_org_group", "F_NAME", "F_CODE");
            opt.bindDropDownList(listSection, "select section_code,section_name from ht_pub_tech_section where is_del = '0' and is_valid = '1'", "section_name", "section_code");
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
   
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (hdcode.Value.Length < 11)
        {
            bindGrid();
        }
        else
            bindData(hdcode.Value);
    }
    
    protected void bindGrid()
    {
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        string query = "select g.IDKEY as 设备编号,g.EQ_NAME as 设备名称,g.EQ_TYPE as 企业设备分类,g.EQ_STATUS as 设备状态,g.ZG_DATE as 转固日期,g.EQ_MODEL as 设备型号,g.USED_DATE as 投入使用日期,g.RATED_POWER as 额定生产能力,g.POWER_UNIT as 能力单位,g1.f_name as 设备管理部门, g2.f_name as 设备使用部门 from ht_eq_eqp_tbl g left join ht_svr_org_group g1 on g1.f_code = g.mgt_dept_code left join ht_svr_org_group g2 on g2.f_code = g.use_dept_code where g.is_del = '0' and is_valid = '1'";
        if (hdcode.Value != "")
            query += " and g.CLS_CODE = '" + hdcode.Value.ToString() + "'";
        if (txtNameS.Text != "")
            query += " and g.EQ_NAME = '" + txtNameS.Text + "'";
        if (txtType.Text != "")
            query += " and g.EQ_TYPE = '" + txtType.Text + "'";
        DataSet data = opt.CreateDataSetOra(query);
        try
        {
            GridView1.DataSource = data;
            GridView1.DataBind();
        }
        catch (Exception e)
        {

        }

    }

    protected void btnAdd_Click(object sender,EventArgs e)
    {
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "addequip", " $('#tabs').tabs('select', '设备详情');", true);
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
                ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "sel", " bindcombo('" + row["CLS_CODE"].ToString() + "');", true);
            }
     
    
    }

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
        opt.UpDateOra("delete from HT_EQ_EQP_TBL where IDKEY = '" + txtIDKey.Text + "'");
        string[] seg = { "IDKEY", "CLS_CODE",  "EQ_NAME", "SGS_CODE", "NC_CODE", "FINANCE_EQ_NAME", "EQ_TYPE", "EQ_STATUS", "ZG_DATE", "EQ_MODEL", "ORI_WORTH", "NET_WORTH", " USED_DATE", "RATED_POWER", "REAL_POWER", "POWER_UNIT", "OWNER_NAME", "EQP_FROM", "ORI_OWNER_NAME", "MANUFACTURER", "SERIAL_NUMBER", "SUPPLIER", "IS_SPEC_EQP", "IS_MADEINCHINA", "MGT_DEPT_CODE", "MGT_DEPT_NAME", "USE_DEPT_CODE", "USE_DEPT_NAME", "DUTY_NAME", "EQP_IP", " EQP_MAC", "EQP_SN", "EQP_SYS", "REMARK", "CREATOR", "CREATE_TIME", "PROCESS_CODE" };
        string[] value = { txtIDKey.Text, txtCLS.Text, txtEqname.Text, txtSGSCode.Text, txtNCCode.Text, txtFncName.Text, txtEQType.Text, listEQStatus.SelectedValue, txtZGDate.Text, txtEQModel.Text, txtOriWorth.Text, txtNetWorth.Text, txtUsedDate.Text, txtRatedPower.Text, txtRealPower.Text, txtPowerUnit.Text, txtOwner.Text, txtEQSource.Text, txtOriOwner.Text, txtManufct.Text, txtSerialNo.Text, txtSupplier.Text, Convert.ToInt16(rdSpecEQ.Checked).ToString(), Convert.ToInt16(rdMadeChina.Checked).ToString(), listMGdept.SelectedValue, listMGdept.SelectedItem.Text, listUseDept.SelectedValue, listUseDept.SelectedItem.Text, txtDutier.Text, txtIp.Text, txtMAC.Text, txtSN.Text, txtOpSYS.Text, txtDscpt.Text, "", System.DateTime.Now.ToString("yyyy-MM-hh"), listSection.SelectedValue };
        //Session["UserID"].ToString()
       
        opt.InsertData(seg, value, "HT_EQ_EQP_TBL");
        bindGrid();

    }
 
    /// <summary>
    /// tab3操作
    /// </summary>
    /// <param name="nodecode"></param>
    /// <param name="Nd"></param>
  

    protected void btn3Save_Click(object sender, EventArgs e)
    {
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        opt.UpDateOra("delete from ht_eq_eqp_cls where ID_KEY = '" + txt3Code.Text + "'");
        string[] seg = { "NODE_NAME", "ID_KEY", "TYPE", "PATH", "PARENT_ID" };
        string[] value = { txt3Name.Text, txt3Code.Text, list3Type.SelectedValue, list3Path.SelectedValue, "" };
        opt.InsertData(seg, value, "ht_eq_eqp_cls");
 
     //   tvHtml = InitTree("",true);
        ScriptManager.RegisterStartupScript(UpdatePanel4, this.Page.GetType(), "", "togtreeview();", true);
    }

    protected void btn3Del_Click(object sender, EventArgs e)
    {
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        opt.UpDateOra("delete from ht_eq_eqp_cls where ID_KEY = '" + txt3Code.Text + "'");
 
     //   tvHtml = InitTree("",true);
        ScriptManager.RegisterStartupScript(UpdatePanel4, this.Page.GetType(), "", "togtreeview();", true);
    }


  
}