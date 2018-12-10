<%@ WebHandler Language="C#" Class="InspectSave" %>

using System;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;


public class InspectSave : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string data = context.Request["data"];
        string inspectID = context.Request["inspectID"];
        byte[] postData = new byte[context.Request.InputStream.Length];
        context.Request.InputStream.Read(postData, 0, postData.Length);
        string postDataString = System.Text.Encoding.UTF8.GetString(postData);
        System.Diagnostics.Debug.WriteLine("正在写入数据");
        System.Diagnostics.Debug.WriteLine(postDataString);
        JObject j = (JObject)JsonConvert.DeserializeObject(postDataString);
        System.Diagnostics.Debug.WriteLine(j["data"][0]);
        System.Diagnostics.Debug.WriteLine(j["inspectID"]);
        System.Diagnostics.Debug.WriteLine(j["isUpdate"]);
       // string sql = "select prod_code from ht_pub_prod_design where is_del=0 and prod_name='" + j["data"][1] + "'";
        //DataSet code = opt.CreateDataSetOra(sql);
        //string prod_code = code.Tables[0].Rows[0][0].ToString();
        string teamid = "01";
         if(j["data"][2].ToString() == "乙班")
         {
             teamid = "02";
         }
         if(j["data"][2].ToString() == "丙班")
         {
             teamid = "03";
         }
        for (int i = 0; i < int.Parse(j["count"].ToString()); i++)
        {
            string[] seg = { "INSPECT_VALUE" };
            string[] value = { j["data"][i].ToString() };
            //string log_message = opt.UpDateData(seg, value, "HT_QLT_INSPECT_RECORD", " where  RECORD_TIME = '" + j["data"][0] + "' and INSPECT_CODE='" + j["inspectID"][i] + "' and prod_code = '" + prod_code + "' and is_del=0 and team_id='" + teamid + "'") == "Success" ? "更新路径节点成功" : "更新路径节点失败";
            //log_message += "--详情:" + string.Join(",", value);
            if (j["isUpdate"].ToString()=="1")
            {   string sql = "select STATUS from HT_QLT_INSPECT_EVENT t WHERE t.Creat_Id = '"+j["recordId"]+"'";
                DataSet code = opt.CreateDataSetOra(sql);
                //string isUpdatable = code.Tables[0].Rows[0][0].ToString();
                if(0!=code.Tables[0].Rows.Count && code.Tables[0].Rows[0][0].ToString()!="0")
                {
                    return;
                } 
                string update = "UPDATE HT_QLT_INSPECT_RECORD SET INSPECT_VALUE= '" + j["data"][i].ToString() + "' where  RECORD_TIME = '" + j["dates"][0].ToString() + "' and INSPECT_CODE='" + j["inspectID"][i] + "' and prod_code = '" + j["prod"] + "' and is_del=0 and team_id='" + j["dates"][3] + "'";
                System.Diagnostics.Debug.WriteLine(j["data"][i].ToString());
                System.Diagnostics.Debug.WriteLine(j["inspectID"][i].ToString());
                System.Diagnostics.Debug.WriteLine("结束");
                System.Diagnostics.Debug.WriteLine("update" + update);
                opt.CreateDataSetOra(update);
            }
            else {
                System.Diagnostics.Debug.WriteLine("正在插入");
                string insert = "INSERT INTO HT_QLT_INSPECT_RECORD(inspect_code, prod_code, shift_id, team_id, inspect_value, record_time, creat_id,create_time, is_del) Values('" + j["inspectID"][i] + "', '" + j["prod"] + "', '" + j["dates"][1] + "', '" + j["dates"][3] + "', '" + j["data"][i].ToString() + "', '" + j["dates"][0].ToString() + "', '" + j["createId"] + "', '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "', 0 )";
                System.Diagnostics.Debug.WriteLine("insert"+insert);
                opt.CreateDataSetOra(insert);
            }
            //InsertTlog(log_message);
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write("Hello World");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}