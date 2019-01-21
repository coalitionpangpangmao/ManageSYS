using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

public partial class Product_StorageOut : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
          

        }

    }
    protected void bindGrid1()
    {
        MSYS.Web.StorageOpt strg = new MSYS.Web.StorageOpt();
        DataTable data = strg.queryStorage(txtYear.Text, txtname.Text, txtcatgory.Text, txttype.Text, txtprovince.Text, txtwarehouse.Text);
        GridView1.DataSource = data;
        GridView1.DataBind();

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid1();
    }
}
     
      