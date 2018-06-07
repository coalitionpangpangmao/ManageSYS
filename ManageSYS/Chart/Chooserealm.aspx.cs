using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.JScript;
using System.Data;

public partial class Service_Chooserealm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string key = Microsoft.JScript.GlobalObject.unescape(Request["key"]).Trim();
        string Remark = "1=1";
        if (key == null || key == "")
        {
            Remark = "1=1";
        }
        else
        {
            Remark = " ERROR_DESCRIPTION like '%" + key + "%'";
        }
        //DataTable dt = new chat.dal.chatconnect().chat_service_getlistremark(1, Remark);
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra("select * from HT_EQ_FAULT_DB where " + Remark);
        DataTable dt = data.Tables[0];
        string result = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            result += "<p onMouseOver=\"this.style.backgroundColor='#B4D7E9';\" onMouseOut=\"this.style.backgroundColor=''\" onclick=\"clickkey('" + dt.Rows[i]["ERROR_DESCRIPTION"].ToString().Trim() + "'," + dt.Rows[i]["id"].ToString().Trim() + ")\" style=\"cursor:pointer\">" + dt.Rows[i]["ERROR_DESCRIPTION"].ToString().Trim() + "</p>";
        }

        Response.Clear();
        Response.Write(result);
        Response.End();
    }
}