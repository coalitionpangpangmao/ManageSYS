<%@ WebHandler Language="C#" Class="getProcessStd" %>

using System;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

public class getProcessStd : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        //JObject result = new JObject();
        JArray result = new JArray();
        
        string query = " select r.inspect_code,s.section_name ,r.inspect_name,0 as value,t.lower_value,t.upper_value,r.unit,'' as status,t.minus_score from ht_qlt_inspect_proj r left join ht_pub_tech_section s on s.section_code = r.inspect_group left join ht_qlt_inspect_stdd_sub t on t.inspect_code = r.inspect_code and t.is_del = '0' where r.inspect_type = '0' and r.is_del = '0' order by t.inspect_code";
        DataSet queryData = opt.CreateDataSetOra(query);
        for (int i = 0; i < queryData.Tables[0].Rows.Count; i++) {
            JObject temp = new JObject();
            temp.Add("inspectId", queryData.Tables[0].Rows[i][0].ToString());
            temp.Add("high", queryData.Tables[0].Rows[i][5].ToString());
            temp.Add("low", queryData.Tables[0].Rows[i][4].ToString());
            temp.Add("inspectName", queryData.Tables[0].Rows[i][2].ToString());
            result.Add(temp);
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