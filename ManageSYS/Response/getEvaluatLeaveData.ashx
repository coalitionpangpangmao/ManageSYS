<%@ WebHandler Language="C#" Class="getEvaluatLeaveData" %>

using System;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;


public class getEvaluatLeaveData : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        var resultData = new JObject();
        JObject requestData = getJsonData(context);
        string tableQuery = "SELECT id, factory_address, factory_time, inspect_time, produce_time, product_code, product_name FROM ht_qlt_inspect_factory WHERE id='"+requestData["factoryId"]+"'";
        string tableDetailQuery = "SELECT inspect_code, inspect_value FROM ht_qlt_inspect_factory_detail WHERE factory_id = '" + requestData["factoryId"] + "' ORDER BY inspect_code";
        
        System.Diagnostics.Debug.WriteLine(tableQuery);
        System.Diagnostics.Debug.WriteLine(tableDetailQuery);
        DataSet tableData = opt.CreateDataSetOra(tableQuery);
       var resultTable = new JArray();
       for (int i = 0; i < 7; i++)
       {
           resultTable.Add(tableData.Tables[0].Rows[0][i].ToString());
       }
        
        DataSet tableDataDetail = opt.CreateDataSetOra(tableDetailQuery);
        var resultTableDetail = new JArray();
        for(int i=0; i<tableDataDetail.Tables[0].Rows.Count; i++){
            //var tempData = new JObject();
            //tempData.Add("inspectCode", tableDataDetail.Tables[0].Rows[i][0].ToString());
            //tempData.Add("inspectValue", tableDataDetail.Tables[0].Rows[i][1].ToString());
            //resultTableDetail.Add(tempData);
            resultTableDetail.Add(tableDataDetail.Tables[0].Rows[i][1].ToString());
        }
        
        resultData.Add("table", resultTable);
        resultData.Add("tableDetail", resultTableDetail);
        
        context.Response.ContentType = "json";
        context.Response.Write(resultData.ToString());
        
    }

    public JObject getJsonData(HttpContext context)
    {
        byte[] postData = new byte[context.Request.InputStream.Length];
        context.Request.InputStream.Read(postData, 0, postData.Length);
        string postDataString = System.Text.Encoding.UTF8.GetString(postData);
        System.Diagnostics.Debug.WriteLine("正在写入数据");
        System.Diagnostics.Debug.WriteLine(postDataString);
        JObject j = (JObject)JsonConvert.DeserializeObject(postDataString);
        return j;
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}