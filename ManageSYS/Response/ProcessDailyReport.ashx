<%@ WebHandler Language="C#" Class="ProcessDailyReport" %>

using System;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Text;

public class ProcessDailyReport : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        JArray result = new JArray();
        byte[] postData = new byte[context.Request.InputStream.Length];
        context.Request.InputStream.Read(postData, 0, postData.Length);
        string postDataString = System.Text.Encoding.UTF8.GetString(postData);
        JObject requestParam = (JObject)JsonConvert.DeserializeObject(postDataString);
        
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        System.Diagnostics.Debug.WriteLine(requestParam["methodName"]);
        if (requestParam["methodName"].ToString() == "getDailyValue") {
            //result = this.getDailyValue(opt, requestParam["time"].ToString());
            result = this.getDailyValue(opt, requestParam["time"].ToString());
        }
        if (requestParam["methodName"].ToString() == "getRawData")
        {
            //result = this.getDailyValue(opt, requestParam["time"].ToString());
            result = this.getRawData(opt, requestParam["time"].ToString());
        }
        if (requestParam["methodName"].ToString() == "getStandar") {
            result = this.getStandar(opt, requestParam["prodcode"].ToString());
        }
        if (requestParam["methodName"].ToString() == "getMonthData")
        {
            result = this.getMonthData(opt, requestParam["time"].ToString());
        }
        System.Diagnostics.Debug.WriteLine(result.ToString());
        context.Response.ContentType = "json";
        context.Response.Write(result.ToString());
    }

    //查询每日检测值
    public JArray getDailyValue(MSYS.DAL.DbOperator opt, string time) {
        JArray result = new JArray();
        String query = "select * from HV_QLT_PROCESS_DAILY_REPORT where record_time='" + time + "'  order by team_id";
        DataTable dt = opt.CreateDataSetOra(query).Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++) {
            JArray temp = new JArray();
            for (int j = 0; j < dt.Columns.Count; j++) {
                if (j <3) {
                    continue;
                }
                temp.Add(dt.Rows[i][j].ToString());
            }
            result.Add(temp);
        }

        return result;
        
    }

    public JArray getRawData(MSYS.DAL.DbOperator opt, string time) {
        JArray result = new JArray();
        String query = "select * from ht_qlt_process_avg_sub t where record_time = '"+time+"' order by inspect_code";
        DataTable dt = opt.CreateDataSetOra(query).Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            JObject tem = new JObject();
            tem.Add("inspect_name", dt.Rows[i][2].ToString());
            tem.Add("qua", dt.Rows[i][4].ToString());
            tem.Add("std", dt.Rows[i][5].ToString());
            tem.Add("avgs", dt.Rows[i][6].ToString());
            tem.Add("quas", dt.Rows[i][7].ToString());
            result.Add(tem);
        }

        return result;
    }

    public JArray getStandar(MSYS.DAL.DbOperator opt, string prodcode) {
        JArray result = new JArray();
        string query = "select t.section_name , r.inspect_code,r.inspect_name,s.lower_value||'~'||s.upper_value||r.unit as range ,s.lower_value, s.upper_value from ht_qlt_inspect_proj r left join ht_qlt_inspect_stdd_sub s on s.inspect_code = r.inspect_code left join ht_pub_tech_section t on t.section_code = r.inspect_group left join ht_pub_prod_design hpp on hpp.inspect_stdd = s.stdd_code where r.INSPECT_TYPE = '0' and r.is_del = '0' and hpp.prod_code = '"+ prodcode +"' order by r.inspect_code";
        DataTable dt = opt.CreateDataSetOra(query).Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++) {
            JObject tem = new JObject();
            tem.Add("name", dt.Rows[i][2].ToString());
            tem.Add("standar", dt.Rows[i][3].ToString());
            tem.Add("lower", dt.Rows[i][4].ToString());
            tem.Add("upper", dt.Rows[i][5].ToString());
            result.Add(tem);
        }
        return result;
    }

    //获取月度数据
    public JArray getMonthData(MSYS.DAL.DbOperator opt, string month) {
        JArray result = new JArray();
        string query = "select * from hv_qlt_process_daily_report t where record_time like '" + month + "'";
        DataTable dt = opt.CreateDataSetOra(query).Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++) {
            JArray list = new JArray();
            for (int j = 3; j < dt.Columns.Count-1; j++) {
                list.Add(dt.Rows[i][j].ToString());
            }
            result.Add(list);
        }
        return result;
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}