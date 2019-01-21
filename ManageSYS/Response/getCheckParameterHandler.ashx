<%@ WebHandler Language="C#" Class="getCheckParameterHandler" %>


using System;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;


public class getCheckParameterHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        //context.Response.Write("Hello World");
        var result = new JArray();
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string querySQL = "SELECT inspect_code, inspect_name, inspect_group  FROM HT_QLT_INSPECT_PROJ WHERE inspect_type = '1' AND inspect_group IN(1,2,3) AND is_valid='1' AND is_del!='1' ORDER BY inspect_code";
        DataSet orcldata = opt.CreateDataSetOra(querySQL);
        for (int i=0; i < orcldata.Tables[0].Rows.Count; i++) {
            var tempdata = new JObject();
            tempdata.Add("code", orcldata.Tables[0].Rows[i][0].ToString());
            tempdata.Add("name", orcldata.Tables[0].Rows[i][1].ToString());
            tempdata.Add("group", orcldata.Tables[0].Rows[i][2].ToString());
            result.Add(tempdata);
        }
        context.Response.ContentType = "json";
        context.Response.Write(result.ToString());
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}