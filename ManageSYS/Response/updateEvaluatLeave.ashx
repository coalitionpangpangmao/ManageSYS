<%@ WebHandler Language="C#" Class="updateEvaluatLeave" %>

using System;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

public class updateEvaluatLeave : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        
        JObject requestData = getJsonData(context);
        if (int.Parse(requestData["isInsert"].ToString()) == 1)
        {
            insertData(requestData);
        }
        else if (int.Parse(requestData["isInsert"].ToString()) == 0)
        {
            updateData(requestData);
        }
        else {
            deleteData(opt, requestData);
        }    
        
        context.Response.ContentType = "text/plain";
        context.Response.Write("Hello World");
    }

    public void deleteData(MSYS.DAL.DbOperator opt, JObject requestData){
        string deletesql = "DELETE FROM ht_qlt_inspect_factory WHERE id = '" + requestData["id"] + "'";
        System.Diagnostics.Debug.WriteLine(deletesql);
        opt.CreateDataSetOra(deletesql);
    }
    
    public string getCurrentTime() {
        return DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
    }
    
    public JObject getJsonData(HttpContext context) {
        byte[] postData = new byte[context.Request.InputStream.Length];
        context.Request.InputStream.Read(postData, 0, postData.Length);
        string postDataString = System.Text.Encoding.UTF8.GetString(postData);
        System.Diagnostics.Debug.WriteLine("正在写入数据");
        System.Diagnostics.Debug.WriteLine(postDataString);
        JObject j = (JObject)JsonConvert.DeserializeObject(postDataString);
        return j;
    }

    public MSYS.DAL.DbOperator getDbOperator() {
        return new MSYS.DAL.DbOperator();
    }
    
    public void insertData(JObject requestData) {
        MSYS.DAL.DbOperator opt = getDbOperator();
        string insertTable = "INSERT INTO HT_QLT_INSPECT_FACTORY(id, factory_time, factory_address, inspect_time, product_name, product_code, produce_time, create_time, is_valid, is_del) VALUES('" + requestData["id"] + "', '" + requestData["factoryTime"] + "', '" + requestData["factoryAddress"] + "', '" + requestData["inspectTime"] + "', '" + requestData["productName"] + "', '" + requestData["productCode"] + "', '" + requestData["productTime"] + "', '" + getCurrentTime() + "', '1', '0')";
        System.Diagnostics.Debug.WriteLine("insertTable "+insertTable);
        opt.CreateDataSetOra(insertTable);
        insertDataDetail(opt, requestData);
    }

    public void insertDataDetail(MSYS.DAL.DbOperator opt, JObject requestData)
    {
        string value = "0";
        System.Diagnostics.Debug.WriteLine(requestData["detailDataCount"].ToString());
        for (int i = 0; i < int.Parse(requestData["detailDataCount"].ToString()); i++) {
            System.Diagnostics.Debug.WriteLine(requestData["id"]);
            System.Diagnostics.Debug.WriteLine(requestData["parameters"]);
            System.Diagnostics.Debug.WriteLine(requestData["detailData"][i].ToString());

            if (requestData["detailData"][i]!=null)
                value = requestData["detailData"][i].ToString();
            else
                value = "0";
            string insertDataDetail = "INSERT INTO HT_QLT_INSPECT_FACTORY_DETAIL(id, factory_id, inspect_code, inspect_name, inspect_value, is_del, is_valid, create_time) VALUES(FACTORY_DETAIL.NEXTVAL, '" + requestData["id"] + "', '" + requestData["parameters"][i]["code"] + "', '" + requestData["parameters"][i]["name"] + "', '" + requestData["detailData"][i] + "', '0', '1', '" + getCurrentTime() + "')";
            System.Diagnostics.Debug.WriteLine(insertDataDetail);
            opt.CreateDataSetOra(insertDataDetail);
        }
    }

    public void updateData(JObject requestData) {
        MSYS.DAL.DbOperator opt = getDbOperator();
        string updateTable ="UPDATE HT_QLT_INSPECT_FACTORY SET factory_time='"+requestData["factoryTime"]+"', factory_address='"+requestData["factoryAddress"]+"', inspect_time='"+requestData["inspectTime"]+"', product_name='"+requestData["productName"]+"', product_code='"+requestData["productCode"]+"', produce_time='"+requestData["produceTime"]+"' WHERE id='"+requestData["id"]+"'";
        System.Diagnostics.Debug.WriteLine("updateTable "+updateTable);
        opt.CreateDataSetOra(updateTable);
        updateDataDetail(opt, requestData);
    }

    public void updateDataDetail(MSYS.DAL.DbOperator opt, JObject requestData) {
        for (int i = 0; i < int.Parse(requestData["detailDataCount"].ToString()); i++)
        {
            string updateDataDetail = "UPDATE HT_QLT_INSPECT_FACTORY_DETAIL SET inspect_value='"+requestData["detailData"][i]+"' WHERE factory_id='"+requestData["id"]+"' AND inspect_code = '"+requestData["parameters"][i]["code"]+"'";
            System.Diagnostics.Debug.WriteLine(updateDataDetail);
            opt.CreateDataSetOra(updateDataDetail);
        }
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}