<%@ WebHandler Language="C#" Class="FlowChartHandler" %>

using System;
using System.Web;
using System.IO;
using System.Text;
public class FlowChartHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
     
        var responseData = getResponseData();

        context.Response.Write(responseData);
    }
    protected string getResponseData()
    {
          
        StreamReader sr = new StreamReader(System.Web.HttpRuntime.AppDomainAppPath + "test\\tree_data1.json", Encoding.Default);
        String line;
        string jsonobj = "";
        while ((line = sr.ReadLine()) != null)
        {
            jsonobj = jsonobj + line.ToString();
        }
        return jsonobj;

           

    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}