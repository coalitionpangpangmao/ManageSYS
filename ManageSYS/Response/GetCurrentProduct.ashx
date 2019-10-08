<%@ WebHandler Language="C#" Class="GetCurrentProduct" %>

using System;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Text;

public class GetCurrentProduct : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        byte[] postData = new byte[context.Request.InputStream.Length];
        context.Request.InputStream.Read(postData, 0, postData.Length);
        string postDataString = System.Text.Encoding.UTF8.GetString(postData);
        JObject js = (JObject)JsonConvert.DeserializeObject(postDataString);

        JArray productlist = getProduct(js["time"].ToString());
        
        context.Response.ContentType = "json";
        context.Response.Write(productlist.ToString());
    }

    public JArray getProduct(string time) {
        string sql = "select distinct t.prod_code, prod_name from ht_qlt_inspect_record t left join ht_pub_prod_design s on t.prod_code = s.prod_code where record_time='" + time + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(sql);
        var productlist = new JArray();
        for (int i = 0; i < data.Tables[0].Rows.Count; i++) {
            var item = new JObject();
            item.Add("prodcode" ,data.Tables[0].Rows[i][0].ToString());
             item.Add("prodname", data.Tables[0].Rows[i][1].ToString());
             productlist.Add(item);
        }
        return productlist;
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}