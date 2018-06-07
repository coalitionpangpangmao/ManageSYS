using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class subleft : System.Web.UI.Page
{
    public string SysHtml = "";
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void initNavTree(string sortID)
    {
        SysHtml = "";
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra("select t.caption,t.sysmodelid,t.parentsysmodelid from sysmodel t where t.isdisabled = 0 and t.isdeleted = 0 order by t.displayorder");
        string strModeID = "";
        if (sortID == "1")
            strModeID = "B14E3815-D789-A76B-E102-64258907D417";
        else if (sortID == "2")
            strModeID = "9B7CB112-E51D-37CB-9523-1B10C5F3E916";
        else if (sortID == "3")
            strModeID = "F9F48BBF87DE4889A5A2A5CEF2840C15";
        else
            strModeID = "B14E3815-D789-A76B-E102-64258907D417";
        DataRow[] rows = data.Tables[0].Select("parentsysmodelid ='" + strModeID + "'");
        foreach (DataRow row in rows)
        {
            SysHtml += "<dd>";
            SysHtml += "<div  class='title'>";
            SysHtml += "<span><img src='../images/leftico01.png' /></span>";
            SysHtml += row["caption"].ToString();
            SysHtml += "</div>";
            InitTreeLeaf(row["sysmodelid"].ToString());
            SysHtml += "</dd>";
        }
    }


    public void InitTreeLeaf(string parentID)
    {
        try
        {
           DataBaseOperator opt =new DataBaseOperator();
            DataSet set = opt.CreateDataSetOra("select r.caption,r.entity,s.sysmodelid,r.sysmappingid from sysmapping  r left join sysmodel s on r.sysmodelid = s.sysmodelid where s.isdisabled = 0 and r.isdisabled = 0 and s.isdeleted = 0 order by s.displayorder");
            DataRow[] trows = set.Tables[0].Select("sysmodelid = '" + parentID + "'");
            if (trows.Length > 0)
            {
                SysHtml += "<ul class='menuson'>";
                foreach (DataRow row in trows)
                {
                    SysHtml += "<li class = 'menu0' ><cite></cite><a  href='";
                    SysHtml += "../Report/Grid.aspx?mapid = ";
                    SysHtml += row["sysmappingid"].ToString() + "' ";
                    SysHtml += "target='rightFrame'>";
                    SysHtml += row["caption"].ToString();
                    SysHtml += "</a><i></i></li>";

                }
                SysHtml += "</ul>";
            }
        }
        catch (Exception e)
        {
            String error = e.Message;
        }

    }
}