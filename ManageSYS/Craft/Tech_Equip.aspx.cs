using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Device_EquipmentInfo : MSYS.Web.BasePage
{
   
    protected string tvHtml2;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listSection, "select section_code,section_name from ht_pub_tech_section where is_del = '0' and is_valid = '1'", "section_name", "section_code");
           
        }
    }

  
   
   

    protected void btnUpdate_Click(object sender, EventArgs e)
    {        
            bindData(hdcode.Value);
    }   
  
    protected void bindData(string eqcode)
    {
        string query = "select * from HT_EQ_EQP_TBL where  IDKEY = '" + eqcode + "'";
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            DataRow row = data.Tables[0].Rows[0];                 
            txtDscpt.Text = row["REMARK"].ToString();
            listSection.SelectedValue = row["SECTION_CODE"].ToString();
            opt.bindDropDownList(listEquip, "select t.idkey,t.eq_name  from ht_eq_eqp_tbl t where t.section_code = '" + listSection.SelectedValue + "'", "eq_name", "IDKEY");
            listEquip.SelectedValue = eqcode;
        }
    }

  
}