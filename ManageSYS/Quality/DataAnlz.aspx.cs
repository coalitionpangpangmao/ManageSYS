using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Quality_DataAnlz : System.Web.UI.Page
{
    protected string tvHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["Equip_Code"] != null)
            {
                hdEquip.Value = Request["Equip_Code"].ToString();
                MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                labEquip.Text = opt.GetSegValue("select IDKEY,EQ_NAME  from ht_eq_eqp_tbl where IDKEY = '" + hdEquip.Value + "'", "EQ_NAME");
                opt.bindDropDownList(listpoint, "select Para_code,Para_name from ht_pub_tech_para where Equip_code = '" + hdEquip.Value + "'  and IS_VALID = '1' and IS_DEL = '0' and  para_type like '___1%'   order by para_code", "Para_name", "Para_code");
                txtstartTime.Text = System.DateTime.Now.AddHours(-2).ToString("yyyy-MM-dd HH:mm:ss");
                txtendTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }           
        
        }
       
    }



  
   
}