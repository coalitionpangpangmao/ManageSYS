using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Craft_Model : MSYS.Web.BasePage
{
    protected string tvHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            tvHtml = InitTree();
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listSection_2, "select section_code,section_name from ht_pub_tech_section where is_del = '0' and is_valid = '1' order by section_code", "section_name", "section_code");
            opt.bindDropDownList(listSection, "select section_code,section_name from ht_pub_tech_section where is_valid = '1' and is_del = '0' order by section_code", "section_name", "section_code");
            opt.bindDropDownList(listApt, "select F_CODE,F_NAME from ht_svr_org_group order by f_code  ", "F_NAME", "F_CODE");
        }
    }

    #region model
    public string  InitTree()
    {

       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select g.section_code,g.section_name from ht_pub_tech_section g where g.IS_VALID = '1' and g.IS_DEL = '0' order by g.section_code ");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
              string tvHtml = "<ul id='browser' class='filetree treeview-famfamfam'>";
            DataRow[] rows = data.Tables[0].Select();
          foreach (DataRow row in rows)
            {  
               // tvHtml += "<li ><span class='folder'  onclick = \"treeClick('" + row["section_code"].ToString() + "')\">" + row["section_name"].ToString() + "</span>";
                tvHtml += "<li ><span class='folder'  value = '" + row["section_code"].ToString() + "'>" + row["section_name"].ToString() + "</span>";
                tvHtml += InitTreeSectionPara(row["section_code"].ToString());
                tvHtml += InitTreeEquip(row["section_code"].ToString());
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
              //  tvHtml += "<li ><span class='folder'  onclick = \"treeClick('" + row["IDKEY"].ToString() + "')\">" + row["EQ_NAME"].ToString() + "</span>";
                tvHtml += "<li ><span class='folder'  value = '" + row["IDKEY"].ToString() + "'>" + row["EQ_NAME"].ToString() + "</span>";
                tvHtml += InitTreePara(row["IDKEY"].ToString());
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }
    public string InitTreePara(string IDkey)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select Para_code,para_name from ht_pub_tech_para where equip_code = '" + IDkey + "' and IS_VALID = '1' and IS_DEL = '0'  order by para_code");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
            //    tvHtml += "<li ><span class='file'  onclick = \"treeClick('" + row["para_code"].ToString() + "')\">" + row["para_name"].ToString() + "</span>";
                tvHtml += "<li ><span class='file'  value = '" + row["para_code"].ToString() + "'>" + row["para_name"].ToString() + "</span>";
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }
    public string InitTreeSectionPara(string section_code)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select Para_code,para_name from ht_pub_tech_para where substr(para_code,1,5) = '" + section_code + "' and Equip_code is null and IS_VALID = '1' and IS_DEL = '0'  order by para_code");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
              //  tvHtml += "<li ><span class='file'  onclick = \"treeClick('" + row["para_code"].ToString() + "')\">" + row["para_name"].ToString() + "</span>";
                tvHtml += "<li ><span class='file'  value = '" + row["para_code"].ToString() + "'>" + row["para_name"].ToString() + "</span>";
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }
   
    #endregion

    #region tab1
    protected void bindData1(string session_code)
    {
        string query = "select * from HT_PUB_TECH_SECTION where  SECTION_CODE = '" + session_code + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            txtCode_1.Text = session_code;
            txtName_1.Text = data.Tables[0].Rows[0]["SECTION_NAME"].ToString();
            txtDscrp_1.Text = data.Tables[0].Rows[0]["REMARK"].ToString();
            rdValid_1.Checked = Convert.ToBoolean(Convert.ToDecimal(data.Tables[0].Rows[0]["IS_PATH_CONFIG"].ToString()));
        }
    }
   
    protected void btnAdd1_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string str = opt.GetSegValue("select Max(Section_code) as Code from ht_pub_tech_section t", "CODE");
        if (str == "")
            str = "00000";
        txtCode_1.Text = "703" + (Convert.ToInt16(str.Substring(3)) + 1).ToString().PadLeft(2, '0');
        txtName_1.Text = "";
        txtDscrp_1.Text = "";
    }

    protected void btnModify1_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        {
            string[] seg = { "SECTION_CODE", "SECTION_NAME", "REMARK", "IS_PATH_CONFIG", "CREATE_ID", "CREATE_TIME" };
            string[] value = { txtCode_1.Text, txtName_1.Text, txtDscrp_1.Text, Convert.ToInt16(rdValid_1.Checked).ToString(), ((MSYS.Data.SysUser)Session["user"]).id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
            string log_message;
            if (opt.MergeInto(seg, value, 1, "HT_PUB_TECH_SECTION") == "Success")
            {
                log_message = "保存工艺段成功";
                tvHtml = InitTree();
                opt.bindDropDownList(listSection_2, "select section_code,section_name from ht_pub_tech_section where is_del = '0' and is_valid = '1' order by section_code", "section_name", "section_code");
                opt.bindDropDownList(listSection, "select section_code,section_name from ht_pub_tech_section where is_valid = '1' and is_del = '0' order by section_code", "section_name", "section_code");
              
                    string[] procseg = { };
                    object[] procvalues = { };
                    opt.ExecProcedures("Create_Online_month_Report", procseg, procvalues);
              
                ScriptManager.RegisterStartupScript(UpdatePanel4, this.Page.GetType(), "sucess", "initTree();alert('保存成功');", true);
            }
            else
                log_message = "保存工艺段失败";
            log_message += "--数据详情:" + string.Join("-",value);
            InsertTlog(log_message);
          
        }
    }
    protected void btnDel1_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        List<string> commandlist = new List<string>();
        commandlist.Add("update HT_PUB_TECH_SECTION set IS_DEL = '1' where SECTION_CODE = '" + txtCode_1.Text + "'");
        //commandlist.Add("update ht_pub_inspect_process set IS_DEL = '1' where substr(PROCESS_CODE,1,5) = '" + txtCode.Text + "'");
        commandlist.Add("update HT_PUB_TECH_PARA set IS_DEL = '1' where substr(PARA_CODE,1,5) =  '" + txtCode_1.Text + "'");      
        string log_message;
        if (opt.TransactionCommand(commandlist) == "Success")
        {
            log_message = "删除工艺段成功";
            tvHtml = InitTree();
            opt.bindDropDownList(listSection_2, "select section_code,section_name from ht_pub_tech_section where is_del = '0' and is_valid = '1' order by section_code", "section_name", "section_code");
            opt.bindDropDownList(listSection, "select section_code,section_name from ht_pub_tech_section where is_valid = '1' and is_del = '0' order by section_code", "section_name", "section_code");
            string[] procseg = { };
            object[] procvalues = { };
            opt.ExecProcedures("Create_Online_month_Report", procseg, procvalues);
            ScriptManager.RegisterStartupScript(UpdatePanel4, this.Page.GetType(), "sucess", "initTree();alert('删除成功');", true);
        }
        else
            log_message = "删除工艺段失败";
        log_message += "，工艺段ID:" + txtCode_1.Text;
        InsertTlog(log_message);
       

    }
    protected void btnUpdate1_Click(object sender, EventArgs e)
    {
        bindData1(hdcode.Value);

    }
    #endregion

    #region tab2
    protected void btnUpdate2_Click(object sender, EventArgs e)
    {
        bindData2(hdcode.Value);
    }

    protected void bindData2(string eqcode)
    {
        string query = "select * from HT_EQ_EQP_TBL where  IDKEY = '" + eqcode + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            DataRow row = data.Tables[0].Rows[0];
            txtDscpt_2.Text = row["REMARK"].ToString();
            listSection_2.SelectedValue = row["SECTION_CODE"].ToString();
            txtCode_2.Text = eqcode;
            txtName_2.Text = row["EQ_NAME"].ToString();
            ckCtrl_2.Checked = ("1" == row["IS_CTRL"].ToString());
        }
    }

    protected void btnAdd2_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        if (listSection_2.SelectedValue == "")
            ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "message", "alert('请选择设备所属工段及分类')", true);
        else
        {
            txtCode_2.Text = listSection_2.SelectedValue + opt.GetSegValue("select nvl(max(substr(idkey,6,3)),0)+1 as code from ht_eq_eqp_tbl where SECTION_CODE = '" + listSection_2.SelectedValue + "'", "CODE").PadLeft(3, '0');
            txtName_2.Text = "";

        }
    }

    protected void btnModify2_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        if (txtCode_2.Text.Length == 8 && txtCode_2.Text.Substring(0, 5) == listSection_2.SelectedValue)
        {
            string[] seg = { "IDKEY", "CLS_CODE", "EQ_NAME", "SECTION_CODE", "REMARK", "CREATOR", "CREATE_TIME", "IS_CTRL" };
            string[] value = { txtCode_2.Text, listSort_2.SelectedValue, txtName_2.Text, listSection_2.SelectedValue, txtDscpt_2.Text, ((MSYS.Data.SysUser)Session["user"]).id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Convert.ToInt16(ckCtrl_2.Checked).ToString() };
            
            string log_message;
            if (opt.MergeInto(seg, value, 1, "HT_EQ_EQP_TBL") == "Success")
            {
                log_message = "保存设备成功";
                tvHtml = InitTree();
                ScriptManager.RegisterStartupScript(UpdatePanel4, this.Page.GetType(), "sucess", "initTree();alert('保存成功');", true);
            }
            else
            {
                log_message = "保存设备失败";
                ScriptManager.RegisterStartupScript(UpdatePanel4, this.Page.GetType(), "sucess", "alert('保存失败');", true);
            }
            log_message += "--数据详情:" + string.Join("-",value);
            InsertTlog(log_message);
           
        }
        else
            ScriptManager.RegisterStartupScript(UpdatePanel4, this.Page.GetType(), "message", "alert('请确认设备所属工艺段是否正确')", true);
    }
    protected void btnDel2_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        List<String> commandlist = new List<String>();
        commandlist.Add("update HT_EQ_EQP_TBL set IS_DEL = '1' where IDKEY = '" + txtCode_2.Text + "'");
        commandlist.Add("update HT_PUB_TECH_PARA set IS_DEL = '1' where EQUIP_CODE =  '" + txtCode_2.Text + "'");
        string log_message;
        if (opt.TransactionCommand(commandlist) == "Success")
        {
            log_message = "删除设备成功";
            tvHtml = InitTree();
            ScriptManager.RegisterStartupScript(UpdatePanel4, this.Page.GetType(), "sucess", "initTree();alert('删除成功');", true);
        }
        else
            log_message = "删除设备失败";
        log_message += ",设备ID:" + txtCode_2.Text;
        InsertTlog(log_message);
       
    }
    #endregion

    #region tab3
    protected void bindData3(string paracode)
    {
        try
        {
            string query = "select * from HT_PUB_TECH_PARA where  PARA_CODE = '" + paracode + "'";
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataSet data = opt.CreateDataSetOra(query);
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                DataRow row = data.Tables[0].Rows[0];
                txtCode.Text = paracode;
                txtName.Text = row["PARA_NAME"].ToString();
                txtUnit.Text = row["PARA_UNIT"].ToString();
                txtSetTag.Text = row["SET_TAG"].ToString();
                txtValueTag.Text = row["VALUE_TAG"].ToString();
                setType(row["PARA_TYPE"].ToString());
                txtDscrp.Text = row["REMARK"].ToString();
                rdValid.Checked = ("1" == row["IS_VALID"].ToString());
                opt.bindDropDownList(listEquip, "select EQ_NAME,IDKEY from ht_eq_eqp_tbl t where t.section_code = '" + txtCode.Text.Substring(0, 5) + "'", "EQ_NAME", "IDKEY");
                listEquip.SelectedValue = row["EQUIP_CODE"].ToString();
                listSection.SelectedValue = paracode.Substring(0, 5);
                listApt.SelectedValue = row["BUSS_ID"].ToString();

            }
        }
        catch (Exception error)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel4, this.Page.GetType(), "error", "<Script>alert('" + error.Message + "')</Script>", false);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string str = opt.GetSegValue("select max(para_code) as CODE from ht_pub_tech_para where substr(para_code,0,5)= '" + listSection.SelectedValue + "'", "CODE");
        if (str == "")
            str = "000000000";
        txtCode.Text = listSection.SelectedValue + (Convert.ToInt16(str.Substring(5)) + 1).ToString().PadLeft(5, '0');
        txtName.Text = "";
        txtUnit.Text = "";
        txtSetTag.Text = "";
        txtValueTag.Text = "";
        setType("0000000");
        txtDscrp.Text = "";

    }
    protected string getType()
    {
        string type = "";
        if (ckCenterCtrl.Checked)
            type += "1";
        else
            type += "0";

        if (ckRecipePara.Checked)
            type += "1";
        else
            type += "0";

        if (ckSetPara.Checked)
            type += "1";
        else
            type += "0";

        if (ckQuality.Checked)
            type += "1";
        else
            type += "0";

        if (ckManul.Checked)
            type += "1";
        else
            type += "0";

        if (ckEqpara.Checked)
            type += "1";
        else
            type += "0";

        if (ckQuaAnalyze.Checked)
            type += "1";
        else
            type += "0";

        if (ckCalibrate.Checked)
            type += "1";
        else
            type += "0";

        if (ckProdOut.Checked)
            type += "1";
        else
            type += "0";
        return type;
    }
    protected void setType(string Type)
    {
        Type = Type.PadRight(9, '0');
        if (Type.Length >= 9)
        {
            ckCenterCtrl.Checked = ("1" == Type.Substring(0, 1));
            ckRecipePara.Checked = ("1" == Type.Substring(1, 1));
            ckSetPara.Checked = ("1" == Type.Substring(2, 1));
            ckQuality.Checked = ("1" == Type.Substring(3, 1));
            ckManul.Checked = ("1" == Type.Substring(4, 1));
            ckEqpara.Checked = ("1" == Type.Substring(5, 1));
            ckQuaAnalyze.Checked = ("1" == Type.Substring(6, 1));
            ckCalibrate.Checked = ("1" == Type.Substring(7, 1));
            ckProdOut.Checked = ("1" == Type.Substring(8, 1));
        }
        if (ckQuality.Checked)
            ckSetPara.Enabled = false;
        if (ckQuaAnalyze.Checked)
        {
            ckSetPara.Enabled = false;
            ckQuality.Enabled = false;
        }
        if (ckManul.Checked || ckProdOut.Checked)
            ckSetPara.Enabled = false;
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        if (txtCode.Text.Length == 10 && txtCode.Text.Substring(0, 5) == listSection.SelectedValue)
        {
            string[] seg = { "PARA_CODE", "PARA_NAME", "PARA_UNIT", "PARA_TYPE", "REMARK", "IS_VALID", "CREATE_ID", "CREATE_TIME", "EQUIP_CODE", "SET_TAG", "VALUE_TAG" ,"BUSS_ID"};
            string[] value = { txtCode.Text, txtName.Text, txtUnit.Text, getType(), txtDscrp.Text, Convert.ToInt16(rdValid.Checked).ToString(), ((MSYS.Data.SysUser)Session["User"]).id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), listEquip.SelectedValue, txtSetTag.Text, txtValueTag.Text,listApt.SelectedValue };
            
            string log_message;
            if (opt.MergeInto(seg, value, 1, "HT_PUB_TECH_PARA") == "Success")
            {
                log_message = "保存参数点成功";
                tvHtml = InitTree();
                string[] procseg = { };
                object[] procvalues = { };
                opt.ExecProcedures("Create_Online_month_Report", procseg, procvalues);
                ScriptManager.RegisterStartupScript(UpdatePanel4, this.Page.GetType(), "sucess", "initTree();alert('保存成功');", true);
            }
            else
                log_message = "保存参数点失败";
            log_message += "--数据详情:" + string.Join(",", value);
            InsertTlog(log_message);
           
        }
        else
            ScriptManager.RegisterStartupScript(UpdatePanel4, this.Page.GetType(), "message", "alert('请确认工艺参数所属工艺段是否正确')", true);

    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "delete from HT_PUB_TECH_PARA  where PARA_CODE =  '" + txtCode.Text + "'";
       
        string log_message;
        if (opt.UpDateOra(query) == "Success")
        {
            log_message = "删除工艺参数点成功";
            tvHtml = InitTree();
            string[] procseg = { };
            object[] procvalues = { };
            opt.ExecProcedures("Create_Online_month_Report", procseg, procvalues);
            ScriptManager.RegisterStartupScript(UpdatePanel4, this.Page.GetType(), "sucess", "initTree();alert('删除成功');", true);
        }
        else
            log_message = "删除工艺参数点失败";
        log_message += ",工艺参数点ID:" + txtCode.Text;
        InsertTlog(log_message);
       
    }
    protected void btnUpdate3_Click(object sender, EventArgs e)
    {
        ckSetPara.Enabled = true;
        ckQuality.Enabled = true;
        bindData3(hdcode.Value);

    }
    protected void listSection_SelectedIndexChanged(object sender, EventArgs e)
    {

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listEquip, "select EQ_NAME,IDKEY from ht_eq_eqp_tbl t where t.Section_code = '" + listSection.SelectedValue + "' and t.is_del = '0'", "EQ_NAME", "IDKEY");
        txtCode.Text = "";
    }
    protected void ckQuality_CheckedChanged(object sender, EventArgs e)
    {
        if (ckQuality.Checked)
        {
            ckSetPara.Checked = true;
            ckSetPara.Enabled = false;
        }
        else
            ckSetPara.Enabled = true;
    }
    protected void ckQuaAnalyze_CheckedChanged(object sender, EventArgs e)
    {
        if (ckQuaAnalyze.Checked)
        {
            ckSetPara.Checked = true;
            ckQuality.Checked = true;
            ckSetPara.Enabled = false;
            ckQuality.Enabled = false;
        }
        else
        {
            ckSetPara.Enabled = true;
            ckQuality.Enabled = true;
        }
    }
    protected void ckManul_CheckedChanged(object sender, EventArgs e)
    {
        if (ckManul.Checked)
        {
            ckCenterCtrl.Checked = true;          
            ckCenterCtrl.Enabled = false;          
        }
        else
        {
            ckCenterCtrl.Enabled = true;            
        }
    }
    
  

    protected void ckProdOut_CheckedChanged(object sender, EventArgs e)
    {
        if (ckProdOut.Checked)
        {
            ckCenterCtrl.Checked = true;
            ckCenterCtrl.Enabled = false;
        }
        else
        {
            ckCenterCtrl.Enabled = true;
        }
    }
    #endregion
}