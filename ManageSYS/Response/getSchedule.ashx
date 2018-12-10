<%@ WebHandler Language="C#" Class="getSchedule" %>

using System;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

public class getSchedule : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        var dates = new JArray();
        byte[] postData = new byte[context.Request.InputStream.Length];
        context.Request.InputStream.Read(postData, 0, postData.Length);
        string postDataString = System.Text.Encoding.UTF8.GetString(postData);
        JObject j = (JObject)JsonConvert.DeserializeObject(postDataString);
        string sql = "SELECT WORK_DATE, s.SHIFT_CODE, h.SHIFT_NAME ,s.TEAM_CODE , t.Team_Name FROM HT_PROD_SCHEDULE s LEFT JOIN HT_SYS_SHIFT h ON s.Shift_Code = h.Shift_Code  LEFT JOIN HT_SYS_TEAM t ON s.team_code = t.team_code WHERE WORK_DATE>='" + j["start_time"] + "' AND WORK_DATE < '" + j["end_time"] + "' AND s.IS_VALID=1 AND s.IS_DEL=0 ORDER BY WORK_DATE, s.team_code";
        DataSet data = opt.CreateDataSetOra(sql);
        for (int i = 0; i < data.Tables[0].Rows.Count; i++)
        {
            var arr = new JArray();
            for (int inner = 0; inner < data.Tables[0].Columns.Count; inner++)
            {
                arr.Add(data.Tables[0].Rows[i][inner].ToString());
            }
            dates.Add(arr);
        }
        context.Response.ContentType = "json";
        context.Response.Write(dates.ToString());
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}