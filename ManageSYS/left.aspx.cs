using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DbOperator;
public partial class left : System.Web.UI.Page
{
    protected string SysHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string userID = "0700001";// Session["UserID"].ToString();
            initPrtMenu(userID);
         
        }
    }
    protected void initPrtMenu(string userID)
    {
         SysHtml = " <ul id='navigation'>";
       string query = "select distinct r.ID,t.f_role ,t.f_right,s.f_prnt_menu from ht_SVR_USER r left join ht_svr_sys_role t on r.role = t.f_id left join ht_svr_sys_menu s on substr(t.f_right,to_number(s.f_ID),1) ='1' where r.ID = '" + userID + "'";
       DataBaseOperator opt =new DataBaseOperator();
           DataSet data = opt.CreateDataSetOra(query);
           if (data != null && data.Tables[0].Rows.Count > 0)
           {
               foreach (DataRow row in data.Tables[0].Rows)
               {
                   SysHtml += " <li> <a class='head'>" + row["f_prnt_menu"].ToString() + "</a>";
                   SysHtml += addChildMenu(row["f_prnt_menu"].ToString(), userID);
                   SysHtml += "</li>";
               }
           }
           SysHtml += "</ul>";

    }
    protected string addChildMenu(string prtmenu,string userID)
    {
          string menu = "";
          string query = "select distinct r.ID,r.name,t.f_role ,t.f_right,s.f_prnt_menu ,s.f_menu,s.f_url from ht_SVR_USER r left join ht_svr_sys_role t on r.role = t.f_id left join ht_svr_sys_menu s on substr(t.f_right,to_number(s.f_ID),1) ='1' where r.ID = '" + userID + "' and s.f_prnt_menu = '" + prtmenu + "'";
          DataBaseOperator opt =new DataBaseOperator();
           DataSet data = opt.CreateDataSetOra(query);
           if (data != null && data.Tables[0].Rows.Count > 0)
           {
               menu = "<ul>";
               foreach (DataRow row in data.Tables[0].Rows)
               {
                   string temp = addChildMenu(row["f_menu"].ToString(),userID);
                   if(temp == "")
                   menu += "<li><a href='" + row["f_menu"].ToString() + "' target='rightFrame'>" + row["f_menu"].ToString() + "</a></li>";
                   else
                   {
                       menu += "<li><cite></cite><a class='subhead'>" + row["f_menu"].ToString() + "</a>";
                       menu += temp;
                       menu += "</li>";
                   }
               }
               menu += "</ul>";
           }
        return menu;
    }
}