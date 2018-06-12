using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MSYS.Data;
public partial class top : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] != null)
        {
            SysUser user = (SysUser)Session["User"];
            labUserName.Text = user.Name;
            labRole.Text = user.UserRole;

        }
    }
}