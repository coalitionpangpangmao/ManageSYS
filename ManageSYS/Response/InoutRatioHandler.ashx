<%@ WebHandler Language="C#" Class="InoutRatioHandler" %>

using System;
using System.Web;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Web.Script.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

[Serializable]
public struct RequestInoutData
{
    public int type { get; set; }
    public string prod_code { get; set; }
    public string startTime { get; set; }
    public string stopTime { get; set; }
}
[Serializable]
public struct ResponseInoutData
{
    public List<string> xAxis;
    public List<double> inAxis;
    public List<double> outAxis;
    public List<double> ratioAxis;
    public string statics;
}

public class InoutRatioHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        var data = context.Request;
        var sr = new StreamReader(data.InputStream);
        var stream = sr.ReadToEnd();
        var javaScriptSerializer = new JavaScriptSerializer();
        try
        {
            var PostedData = javaScriptSerializer.Deserialize<RequestInoutData>(stream);

            ResponseInoutData datainfo = new ResponseInoutData();

            datainfo = handleData(PostedData);
            var responseData = javaScriptSerializer.Serialize(datainfo);
            context.Response.ContentType = "text/plain";
            context.Response.Write(responseData);
        }
        catch (Exception ee)
        {
            var e = ee.Message;
        }
    }

    protected ResponseInoutData handleData(RequestInoutData PostedData)
    {

        string query;
        string plus = "";
             if (PostedData.prod_code != "")
            plus = " and  prod_code = '" + PostedData.prod_code + "'";
        if (PostedData.type == 1)
        {
            query = "select * from hv_prod_inout_ratio  where datetime between '" + PostedData.startTime + "' and '" + PostedData.stopTime + "'" + plus;
        }
        else if (PostedData.type == 2)
            query = "select substr(datetime,1,7) as datetime,prod_code,sum(inweight) as inweight ,sum(outweight) as outweight ,round(sum(outweight)/sum(inweight),2) as rate from hv_prod_inout_ratio  where datetime between '" + PostedData.startTime + "' and '" + PostedData.stopTime + "'" + plus + " group by substr(datetime,1,7),prod_code ";
        else if (PostedData.type == 3)
            query = "select substr(datetime,1,7) as datetime,prod_code,sum(inweight) as inweight ,sum(outweight) as outweight ,round(sum(outweight)/sum(inweight),2) as rate from hv_prod_inout_ratio  where datetime between '" + PostedData.startTime + "' and '" + PostedData.stopTime + "'" + plus + "  group by substr(datetime,1,7),prod_code ";
        else
            query = "select substr(datetime,1,4) as datetime,prod_code,sum(inweight) as inweight ,sum(outweight) as outweight ,round(sum(outweight)/sum(inweight),2) as rate from hv_prod_inout_ratio  where datetime between '" + PostedData.startTime + "' and '" + PostedData.stopTime + "'" + plus + "  group by substr(datetime,1,4),prod_code ";
        query += "order by datetime";
       
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        ResponseInoutData res = new ResponseInoutData();
       
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
             res.inAxis = new List<double>();
             res.outAxis = new List<double>();
             res.ratioAxis = new List<double>();
             res.xAxis = new List<string>();
            foreach (DataRow row in data.Tables[0].Rows)
            {
                res.inAxis.Add(Convert.ToDouble(row[2].ToString()));
                res.outAxis.Add(Convert.ToDouble(row[3].ToString()));
                res.ratioAxis.Add(Convert.ToDouble(row[4].ToString()));
                res.xAxis.Add(row[0].ToString());
            }
            res.statics = getStatics(data.Tables[0]);
        }
        return res;
    }

    protected string getStatics(DataTable data)
    {
        StringBuilder str = new StringBuilder("");
        str.Append("<table width='100%' border='1' cellpadding='0' cellspacing='1' bgcolor='#a8c7ce'>");

        str.Append("<tr>");
        str.Append(" <td  height='25px' bgcolor='d3eaef' class='staticHead' width = '70px' border='1'><div align='center'><span class='staticHeadtext'>");
        str.Append("时间");
        str.Append("</span></div></td>");
        for (int i = 0; i < data.Rows.Count; i++)
        {
            str.Append(" <td  height='25px' bgcolor='d3eaef' class='staticHead' border='1' width = '80px'><div align='center'><span class='staticHeadtext'>");
            str.Append(data.Rows[i]["datetime"].ToString());
            str.Append("</span></div></td>");
        }
        str.Append("</tr>");
        for (int j = 1; j < data.Columns.Count; j++)
        {
            str.Append("<tr>");
            str.Append(" <td  height='25px' bgcolor='d3eaef' class='staticHead' width = '70px' border='1'><div align='center'><span class='staticHeadtext'>");
            if(j == 1)
            str.Append("产品");
             else if(j == 2)
                 str.Append("投入");
             else if(j == 3)
                 str.Append("产出");
             else
                 str.Append("投入产出比");
            str.Append("</span></div></td>");
            for (int i = 0; i < data.Rows.Count; i++)
            {
                str.Append("  <td height='25px' bgcolor='#FFFFFF' class='staticHead' border='1'><div align='center'><span class='staticRow'>");
                str.Append(data.Rows[i][j].ToString());
                str.Append("</span></div></td>");
            }
            str.Append("</tr>");          
          
        }
        str.Append("</table>");
        return str.ToString();

    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}