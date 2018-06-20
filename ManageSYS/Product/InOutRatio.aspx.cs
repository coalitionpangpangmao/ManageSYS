using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Product_InOutRatio : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
     //   base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            txtEtime.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            txtBtime.Text = System.DateTime.Now.ToString("yyyy-MM") + "-01";
            DataBaseOperator opt = new DataBaseOperator();
            opt.bindDropDownList(listRecipe, "select Prod_code,Prod_name from ht_pub_prod_design  where is_del = '0' and B_FLOW_STATUS = '2'", "PROD_NAME", "PROD_CODE"); 
         
        }
    }



    //根据所选条件从数据库选取数据
    protected void initData()
    {
        try
        {
            //string query = "";
            //if (rdSort1.Checked == true)
            //    ;
            //else if (rdSort2.Checked == true)
            //    ;
            //else
            //    ;
            //hdcode1.Value = "";
            //hdcode2.Value = "";
            //hdcode3.Value = "";
        }
        catch (Exception ee)
        {
           
        }

    }


    protected void listRecipe_SelectedIndexChanged(object sender, EventArgs e)
    {
        initData();
    }
    protected void rdSort1_CheckedChanged(object sender, EventArgs e)
    {
        initData();
    }
    protected void rdSort2_CheckedChanged(object sender, EventArgs e)
    {
        initData();
    }
    protected void rdSort3_CheckedChanged(object sender, EventArgs e)
    {
        initData();
    }
    protected void txtBtime_TextChanged(object sender, EventArgs e)
    {
        initData();
    }
    protected void txtEtime_TextChanged(object sender, EventArgs e)
    {
        initData();
    }
}