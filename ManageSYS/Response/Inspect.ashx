<%@ WebHandler Language="C#" Class="Inspect" %>

using System;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;


public class Inspect : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        System.Diagnostics.Debug.WriteLine("getTitle 收到请求");
        var json = new JObject();
        var titles = new JArray();
        string sql = "select r.inspect_code,s.name as insgroup,r.inspect_name,0 as value,t.lower_value,t.upper_value,r.unit,'' as status,t.minus_score from ht_qlt_inspect_proj r left join ht_inner_inspect_group s on s.id = r.inspect_group left join ht_qlt_inspect_stdd t on t.inspect_code = r.inspect_code and t.is_del = '0' where r.inspect_group in('1','2','3') and r.is_del = '0' order by r.inspect_code";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(sql);
        //string[] title = new string[data.Tables[0].Rows.Count];
        for (int i = 0; i < data.Tables[0].Rows.Count; i++)
        {
            // title[i] = data.Tables[0].Rows[i][2].ToString();
            titles.Add(data.Tables[0].Rows[i][2].ToString());
        }
        json.Add("titles", titles);
        //context.Response.ContentType = "text/plain";
        context.Response.ContentType = "json";
        context.Response.Write(json.ToString());
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}