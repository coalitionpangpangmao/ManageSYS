using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Quality_ShiftAnalyze : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStartTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            initView();
        }
    }
    protected void initView()
    {

    }
}