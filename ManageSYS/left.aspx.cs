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
            string userID = "0700001";// Session["UserID"].ToString();
            SysHtml = initLevel1Menu(userID);
         
        }
    }
    protected string initLevel1Menu(string userID)
    {
        string resultHtml = " <ul id='navigation'>";
        string query = "select distinct r.ID as userID,t.f_role ,t.f_right,q.name as prtName,q.menulevel ,q.ID as prtID from ht_SVR_USER r left join ht_svr_sys_role t on r.role = t.f_id left join ht_svr_sys_menu s on substr(t.f_right,to_number(s.f_ID),1) ='1' and s.f_type = '0'  left join ht_svr_prt_menu q on q.id = s.f_pid  where  q.menulevel = '1' and r.ID = '" + userID + "' order by q.ID";
       DataBaseOperator opt =new DataBaseOperator();
           DataSet data = opt.CreateDataSetOra(query);
           if (data != null && data.Tables[0].Rows.Count > 0)
           {
               foreach (DataRow row in data.Tables[0].Rows)
               {                  
                   resultHtml += " <li> <a class='head'>" + row["prtName"].ToString() + "</a>";
                   resultHtml += initLevel2Menu(row["prtID"].ToString(), userID);
                   resultHtml += addChildMenu(row["prtID"].ToString(), userID);
                   resultHtml += "</li>";
               }
           }
           resultHtml += "</ul>";
           return resultHtml;

    }

    protected string initLevel2Menu(string prtID, string userID)
    {
        string resultHtml = "";
        string query = "select distinct r.ID as userID,t.f_role ,t.f_right,q.name as prtName,q.menulevel ,q.ID as prtID from ht_SVR_USER r left join ht_svr_sys_role t on r.role = t.f_id left join ht_svr_sys_menu s on substr(t.f_right,to_number(s.f_ID),1) ='1' and s.f_type = '0'  left join ht_svr_prt_menu q on q.id = s.f_pid  and q.pid = '" + prtID + "'  where  q.menulevel = '2' and r.ID = '" + userID + "' order by q.ID";
        DataBaseOperator opt = new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
             resultHtml += "<ul>";
            foreach (DataRow row in data.Tables[0].Rows)
            {              
                    resultHtml += " <li><cite></cite><a class='subhead'>" + row["prtName"].ToString() + "</a>";
                resultHtml += addChildMenu(row["prtID"].ToString(), userID);
                resultHtml += "</li>";
            }
            resultHtml += "</ul>";
        }
        
        return resultHtml;

    }
    protected string addChildMenu(string prtID,string userID)
    {
          string menu = "";
          string query = "select distinct r.ID,r.name,t.f_role ,t.f_right,s.f_pid ,s.f_menu,q.url ,s.f_id from ht_SVR_USER r left join ht_svr_sys_role t on r.role = t.f_id left join ht_svr_sys_menu s on substr(t.f_right,to_number(s.f_ID),1) ='1' and s.f_type = '0'  left join ht_inner_map q on q.mapid = s.f_mapid where   r.ID = '" + userID + "' and s.f_pid = '" + prtID + "' order by s.f_id";
          DataBaseOperator opt =new DataBaseOperator();
           DataSet data = opt.CreateDataSetOra(query);
           if (data != null && data.Tables[0].Rows.Count > 0)
           {
               menu = "<ul>";
               foreach (DataRow row in data.Tables[0].Rows)
               {
                   menu += "<li><a href='" + row["url"].ToString() + "' target='rightFrame'>" + row["f_menu"].ToString() + "</a></li>";                  
               }
               menu += "</ul>";
           }
        return menu;
    }
}