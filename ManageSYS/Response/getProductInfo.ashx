<%@ WebHandler Language="C#" Class="getProductInfo" %>

using System;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Text;

public class getProductInfo : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        var result = new JArray();
        string sql = "SELECT prod_code, prod_name FROM ht_pub_prod_design WHERE is_valid='1' AND is_del='0'";
        DataSet orclData = opt.CreateDataSetOra(sql);
        for (int i = 0; i < orclData.Tables[0].Rows.Count; i++) {
            var tempData = new JObject();
            tempData.Add("prodCode", orclData.Tables[0].Rows[i][0].ToString());
            tempData.Add("prodName", orclData.Tables[0].Rows[i][1].ToString());
            result.Add(tempData);
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