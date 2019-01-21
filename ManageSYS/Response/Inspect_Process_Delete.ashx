<%@ WebHandler Language="C#" Class="Inspect_Process_Delete" %>

using System;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;


public class Inspect_Process_Delete : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");\
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string data = context.Request["data"];
        string inspectID = context.Request["inspectID"];
        byte[] postData = new byte[context.Request.InputStream.Length];
        context.Request.InputStream.Read(postData, 0, postData.Length);
        string postDataString = System.Text.Encoding.UTF8.GetString(postData);
        System.Diagnostics.Debug.WriteLine("正在写入数据");
        System.Diagnostics.Debug.WriteLine(postDataString);
        JObject j = (JObject)JsonConvert.DeserializeObject(postDataString);
        System.Diagnostics.Debug.WriteLine(j["inspectID"]);
        System.Diagnostics.Debug.WriteLine(j["isUpdate"]);
        System.Diagnostics.Debug.WriteLine(j["dates"]);
        System.Diagnostics.Debug.WriteLine(j["prod"]);
        System.Diagnostics.Debug.WriteLine(j["count"]);

            
       //对每项进行删除操作
        for (int i = 0; i < int.Parse(j["count"].ToString()); i++)
        {
            System.Diagnostics.Debug.WriteLine("正在删除");
            string delete = "UPDATE HT_QLT_INSPECT_RECORD SET IS_DEL = 1 WHERE RECORD_TIME = '" + j["dates"][0].ToString() + "' and INSPECT_CODE='" + j["inspectID"][i] + "' and prod_code = '" + j["prod"] + "' and is_del=0 and team_id='" + j["dates"][2] + "'";
            System.Diagnostics.Debug.WriteLine("deletesql "+delete);
            opt.CreateDataSetOra(delete);
        }
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}