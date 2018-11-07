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
                return "<div id = 'flowchart' class = 'ZZGS1flow' > <div class = 'back' > <input id='Button1' type='button' value='返回'  class = 'btnBack hotArea' onclick = 'clickArea(\"All\")'/></div> <div class = 'GS1_areaDev1 hotArea' onclick =  'clickEquip(\"70301002\")'> </div><div class = 'GS1_areaDev2 hotArea' onclick =  'clickEquip(\"70301005\")'> </div><div class = 'GS1_areaDev3 hotArea' onclick =  'clickEquip(\"70301008\")'> </div></div>";
            case "ZZGS2":
                return "<div id = 'flowchart' class = 'ZZGS2flow' > <div class = 'back' > <input id='Button1' type='button' value='返回'  class = 'btnBack hotArea' onclick = 'clickArea(\"All\")'/></div><div class = 'GS2_areaDev1 hotArea' onclick =  'clickEquip(\"70302001\")'> </div><div class = 'GS2_areaDev2 hotArea' onclick =  'clickEquip(\"70302002\")'> </div><div class = 'GS2_areaDev3 hotArea' onclick =  'clickEquip(\"70302003\")'> </div> <div class = 'GS2_areaDev4 hotArea' onclick =  'clickEquip(\"70302008\")'> </div> <div class = 'GS2_areaDev5 hotArea' onclick =  'clickEquip(\"70302009\")'> </div> <div class = 'GS2_areaDev6 hotArea' onclick =  'clickEquip(\"7030200085\")'> </div> </div>";
            case "ZZGS3":
                return "<div id = 'flowchart' class = 'ZZGS3flow' >  <div class = 'back' > <input id='Button1' type='button' value='返回'  class = 'btnBack' onclick = 'clickArea(\"All\")'/></div><div class = 'GS3_areaDev1 hotArea' onclick =  'clickEquip(\"70303002\")'> </div><div class = 'GS3_areaDev2 hotArea' onclick =  'clickEquip(\"70303005\")'> </div><div class = 'GS3_areaDev3 hotArea' onclick =  'clickEquip(\"70303021\")'> </div> <div class = 'GS3_areaDev4 hotArea' onclick =  'clickEquip(\"70303025\")'> </div> <div class = 'GS3_areaDev5 hotArea' onclick =  'clickEquip(\"70303028\")'> </div></div>";
            case "ZZGS4":
                return "<div id = 'flowchart' class = 'ZZGS4flow' > <div class = 'back' > <input id='Button1' type='button' value='返回'  class = 'btnBack' onclick = 'clickArea(\"All\")'/></div><div class = 'GS4_areaDev1 hotArea' onclick =  'clickEquip(\"70304006\")'> </div><div class = 'GS4_areaDev2 hotArea' onclick =  'clickEquip(\"70304009\")'> </div><div class = 'GS4_areaDev3 hotArea' onclick =  'clickEquip(\"70304012\")'> </div> <div class = 'GS4_areaDev4 hotArea' onclick =  'clickEquip(\"70304017\")'> </div> <div class = 'GS4_areaDev5 hotArea' onclick =  'clickEquip(\"70304020\")'> </div> </div>";
            case "ZZGS5":
                return "<div id = 'flowchart' class = 'ZZGS5flow' >  <div class = 'back' > <input id='Button1' type='button' value='返回'  class = 'btnBack' onclick = 'clickArea(\"All\")'/></div><div class = 'GS5_areaDev1 hotArea' onclick =  'clickEquip(\"70305003\")'> </div><div class = 'GS5_areaDev2 hotArea' onclick =  'clickEquip(\"70305005\")'> </div><div class = 'GS5_areaDev3 hotArea' onclick =  'clickEquip(\"70305006\")'> </div> </div>";
            default:
                return "<div id = 'flowchart' class = 'mainflow' > <div class = 'areaZZGS1  hotArea' onclick = 'clickArea(\"ZZGS1\")'></div><div class = 'areaZZGS2  hotArea' onclick = 'clickArea(\"ZZGS2\")'> </div><div class = 'areaZZGS3  hotArea' onclick = 'clickArea(\"ZZGS3\")'> </div><div class = 'areaZZGS4  hotArea' onclick = 'clickArea(\"ZZGS4\")'></div><div class = 'areaZZGS5  hotArea' onclick = 'clickArea(\"ZZGS5\")'></div></div>";
        }

    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}