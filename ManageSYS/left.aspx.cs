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
                string userID = ((MSYS.Data.SysUser)Session["User"]).Id;
                SysHtml = initLevel1Menu(userID);
            }
        }
    }
    protected string initLevel1Menu(string userID)
    {
        string resultHtml = " \t<ul id='navigation'>\r\n";
        string query = "select distinct q.name as prtName ,q.ID as prtID from ht_SVR_USER r left join ht_svr_sys_role t on r.role = t.f_id left join ht_svr_sys_menu s on substr(t.f_right,to_number(s.f_ID),1) ='1' and s.f_type = '0' and s.is_del = '0'  left join ht_svr_prt_menu q on q.id = s.f_pid and q.menulevel = '1' where   r.ID = '" + userID + "' and q.id is not null union select distinct q1.name as prtName,q1.ID as prtID from ht_SVR_USER r left join ht_svr_sys_role t on r.role = t.f_id left join ht_svr_sys_menu s on substr(t.f_right,to_number(s.f_ID),1) ='1' and s.f_type = '0' and s.is_del = '0'  left join ht_svr_prt_menu q on q.id = s.f_pid and q.menulevel = '2' left join ht_svr_prt_menu q1 on q1.id = q.pid   where   r.ID = '" + userID + "' and q1.id is not null order by prtID";
       DataBaseOperator opt =new DataBaseOperator();
           DataSet data = opt.CreateDataSetOra(query);
           if (data != null && data.Tables[0].Rows.Count > 0)
           {
               foreach (DataRow row in data.Tables[0].Rows)
               {                  
                   resultHtml += " \t\t<li> <a class='head'>" + row["prtName"].ToString() + "</a>\r\n";
                   resultHtml += initLevel2Menu(row["prtID"].ToString(), userID);                   
                   resultHtml += "\t\t</li>\r\n";
               }
           }
           resultHtml += "\t</ul>\r\n";
           return resultHtml;

    }

    protected string initLevel2Menu(string prtID, string userID)
    {
        string resultHtml = "";
        string query = "select distinct r.ID as userID,t.f_role ,t.f_right,q.name as prtName,q.menulevel ,q.ID as prtID from ht_SVR_USER r left join ht_svr_sys_role t on r.role = t.f_id left join ht_svr_sys_menu s on substr(t.f_right,to_number(s.f_ID),1) ='1' and s.f_type = '0' and s.is_del = '0'  left join ht_svr_prt_menu q on q.id = s.f_pid  and q.pid = '" + prtID + "'  where  q.menulevel = '2' and r.ID = '" + userID + "' order by q.ID";
        DataBaseOperator opt = new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra(query);
        resultHtml += "<ul style='height: 300px;'>\r\n";
        resultHtml += addChildMenu(prtID, userID);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in data.Tables[0].Rows)
            {              
                resultHtml += " <li><cite></cite><a class='subhead'>" + row["prtName"].ToString() + "</a>\r\n";
                resultHtml += "<ul>\r\n";
                resultHtml += addChildMenu(row["prtID"].ToString(), userID);
                resultHtml += "</ul>\r\n";
                resultHtml += "</li>\r\n";
            }          
        }
            resultHtml += "</ul>\r\n";
        
        return resultHtml;

    }
    protected string addChildMenu(string prtID,string userID)
    {
          string menu = "";
          string query = "select distinct r.ID,r.name,t.f_role ,t.f_right,s.f_pid ,s.f_menu,q.url ,s.f_id from ht_SVR_USER r left join ht_svr_sys_role t on r.role = t.f_id left join ht_svr_sys_menu s on substr(t.f_right,to_number(s.f_ID),1) ='1' and s.f_type = '0' and s.is_del = '0'  left join ht_inner_map q on q.mapid = s.f_mapid where   r.ID = '" + userID + "' and s.f_pid = '" + prtID + "' order by s.f_id";
          DataBaseOperator opt =new DataBaseOperator();
           DataSet data = opt.CreateDataSetOra(query);
           if (data != null && data.Tables[0].Rows.Count > 0)
           {               
               foreach (DataRow row in data.Tables[0].Rows)
               {
                   menu += "<li><a href='" + row["url"].ToString() + "' target='rightFrame'>" + row["f_menu"].ToString() + "</a></li>\r\n";                  
               }
               
           }
        return menu;
    }
}