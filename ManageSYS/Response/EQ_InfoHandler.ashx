<%@ WebHandler Language="C#" Class="EQ_InfoHandler" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections.Generic;
public class EQ_InfoHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        List<MSYS.Data.EquipCLS> equipcls = new MSYS.Data.EquipCLS("0").children;

        var javaScriptSerializer = new JavaScriptSerializer();
        var responseData = javaScriptSerializer.Serialize(equipcls);
        context.Response.Write(responseData);
     
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}