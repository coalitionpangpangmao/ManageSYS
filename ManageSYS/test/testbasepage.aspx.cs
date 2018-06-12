using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MSYS.Web;
using System.Web.SessionState;
using MSYS.Data;
public partial class test_testbasepage : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        string mapid = base.MappingId;
        TextBox1.Text = base.MappingId + "_" + base.HasRight;
        TextBox2.Text = base.SessionId;
        if (Session["User"] != null)
        {
            SysUser m = (SysUser)Session["user"];
        }
    }
}