<%@ WebHandler Language="C#" Class="FlowChartHandler" %>

using System;
using System.Web;

public class FlowChartHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        var area = Microsoft.JScript.GlobalObject.unescape(context.Request["area"]).Trim();

        var responseData = getResponseData(area);

        context.Response.Write(responseData);
    }
    protected string getResponseData(string area)
    {

        switch (area)
        {
            case "ZZGS1":
                return "<div id = 'flowchart' class = 'ZZGS1flow' > <div class = 'back' > <input id='Button1' type='button' value='返回'  class = 'btnBack' onclick = 'clickArea(\"All\")'/></div> <div class = 'areaDev1' onclick =  'clickEquip()'> </div> </div>";
            case "ZZGS2":
                return "<div id = 'flowchart' class = 'ZZGS2flow' > <div class = 'back' > <input id='Button1' type='button' value='返回'  class = 'btnBack' onclick = 'clickArea(\"All\")'/></div> <div class = 'areaDev1' onclick =  'clickEquip()'> </div> </div>";
            case "ZZGS3":
                return "<div id = 'flowchart' class = 'ZZGS3flow' >  <div class = 'back' > <input id='Button1' type='button' value='返回'  class = 'btnBack' onclick = 'clickArea(\"All\")'/></div><div class = 'areaDev1' onclick =  'clickEquip()'> </div> </div>";
            case "ZZGS4":
                return "<div id = 'flowchart' class = 'ZZGS4flow' > <div class = 'back' > <input id='Button1' type='button' value='返回'  class = 'btnBack' onclick = 'clickArea(\"All\")'/></div> <div class = 'areaDev1' onclick =  'clickEquip()' </div> </div>";
            case "ZZGS5":
                return "<div id = 'flowchart' class = 'ZZGS5flow' >  <div class = 'back' > <input id='Button1' type='button' value='返回'  class = 'btnBack' onclick = 'clickArea(\"All\")'/></div><div class = 'areaDev1' onclick =  'clickEquip()'> </div> </div>";
            default:
                return "<div id = 'flowchart' class = 'mainflow' > <div class = 'areaZZGS1' onclick = 'clickArea(\"ZZGS1\")'></div><div class = 'areaZZGS2' onclick = 'clickArea(\"ZZGS2\")'> </div><div class = 'areaZZGS3' onclick = 'clickArea(\"ZZGS3\")'> </div><div class = 'areaZZGS4' onclick = 'clickArea(\"ZZGS4\")'></div><div class = 'areaZZGS5' onclick = 'clickArea(\"ZZGS5\")'></div></div>";
        }

    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}