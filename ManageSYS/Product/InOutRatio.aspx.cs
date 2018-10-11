using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Product_InOutRatio : MSYS.Web.BasePage
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
       base.PageLoad(sender, e);
        if (!IsPostBack)
        {
          
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listProd, "select Prod_code,Prod_name from ht_pub_prod_design  where is_del = '0' ", "PROD_NAME", "PROD_CODE");
            rdSort1.Checked = true;
            txtEtime.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            txtBtime.Text = System.DateTime.Now.ToString("yyyy-MM") + "-01";
        }
    }  

    protected void rdSort1_CheckedChanged(object sender, EventArgs e)
    {
        txtEtime.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
        txtBtime.Text = System.DateTime.Now.ToString("yyyy-MM") + "-01";
    }
    protected void rdSort2_CheckedChanged(object sender, EventArgs e)
    {
        txtEtime.Text = System.DateTime.Now.ToString("yyyy-MM");
        txtBtime.Text = System.DateTime.Now.ToString("yyyy-01");
    }
    protected void rdSort3_CheckedChanged(object sender, EventArgs e)
    {
        txtEtime.Text = "";
        txtBtime.Text = "";
    }
    protected void rdSort4_CheckedChanged(object sender, EventArgs e)
    {
        txtEtime.Text = "";
        txtBtime.Text = "";
    }

  
}