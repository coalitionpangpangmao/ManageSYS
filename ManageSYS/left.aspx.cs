using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MSYS.DAL;
public partial class left : System.Web.UI.Page
{
    protected string SysHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["User"] != null)
            {
                string userRoleID = ((MSYS.Data.SysUser)Session["User"]).UserRoleID;
                SysHtml = initLevel1Menu(userRoleID);
            }
        }
    }
    protected string initLevel1Menu(string RoleID)
    {
        string resultHtml = " \t<ul id='navigation'>\r\n";
        string query = "select distinct q.name as prtName ,q.ID as prtID from  ht_svr_sys_role t  left join ht_svr_sys_menu s on substr(t.f_right,to_number(s.f_ID),1) ='1' and s.f_type = '0' and s.is_del = '0'  left join ht_svr_prt_menu q on q.id = s.f_pid and q.menulevel = '1' where    t.f_id = '" + RoleID + "' and q.id is not null union select distinct q1.name as prtName,q1.ID as prtID from  ht_svr_sys_role t  left join ht_svr_sys_menu s on substr(t.f_right,to_number(s.f_ID),1) ='1' and s.f_type = '0' and s.is_del = '0'  left join ht_svr_prt_menu q on q.id = s.f_pid and q.menulevel = '2' left join ht_svr_prt_menu q1 on q1.id = q.pid   where  t.f_id = '" + RoleID + "'  and q1.id is not null order by prtID";
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
           DataSet data = opt.CreateDataSetOra(query);
           if (data != null && data.Tables[0].Rows.Count > 0)
           {
               foreach (DataRow row in data.Tables[0].Rows)
               {                  
                   resultHtml += " \t\t<li> <a class='head'>" + row["prtName"].ToString() + "</a>\r\n";
                   resultHtml += initLevel2Menu(row["prtID"].ToString(), RoleID);                   
                   resultHtml += "\t\t</li>\r\n";
               }
           }
           resultHtml += "\t</ul>\r\n";
           return resultHtml;

    }

    protected string initLevel2Menu(string prtID, string roleID)
    {
        string resultHtml = "";
        string query = "select distinct t.f_role ,t.f_right,q.name as prtName,q.menulevel ,q.ID as prtID from  ht_svr_sys_role t left join ht_svr_sys_menu s on substr(t.f_right,to_number(s.f_ID),1) ='1' and s.f_type = '0' and s.is_del = '0'  left join ht_svr_prt_menu q on q.id = s.f_pid  and q.pid = '" + prtID + "'  where  q.menulevel = '2' and t.f_id = '" + roleID + "' order by q.ID";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        resultHtml += "<ul style='height: 300px;'>\r\n";
        resultHtml += addChildMenu(prtID, roleID);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in data.Tables[0].Rows)
            {              
                resultHtml += " <li><cite></cite><a class='subhead'>" + row["prtName"].ToString() + "</a>\r\n";
                resultHtml += "<ul>\r\n";
                resultHtml += addChildMenu(row["prtID"].ToString(), roleID);
                resultHtml += "</ul>\r\n";
                resultHtml += "</li>\r\n";
            }          
        }
            resultHtml += "</ul>\r\n";
        
        return resultHtml;

    }
    protected string addChildMenu(string prtID,string roleID)
    {
          string menu = "";
          string query = "select distinct t.f_role ,t.f_right,s.f_pid ,s.f_menu,q.url ,s.f_id from  ht_svr_sys_role t left join ht_svr_sys_menu s on substr(t.f_right,to_number(s.f_ID),1) ='1' and s.f_type = '0' and s.is_del = '0'  left join ht_inner_map q on q.mapid = s.f_mapid where   t.f_id = '" + roleID + "'  and s.f_pid = '" + prtID + "' order by s.f_id";
          MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
           DataSet data = opt.CreateDataSetOra(query);
           if (data != null && data.Tables[0].Rows.Count > 0)
           {               
               foreach (DataRow row in data.Tables[0].Rows)
               {
                   menu += "<li><a class='child' href='" + row["url"].ToString() + "' target='rightFrame'>" + row["f_menu"].ToString() + "</a></li>\r\n";                  
               }
               
           }
        return menu;
    }
}