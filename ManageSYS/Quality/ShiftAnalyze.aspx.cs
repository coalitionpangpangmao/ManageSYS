using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Quality_ShiftAnalyze : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
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