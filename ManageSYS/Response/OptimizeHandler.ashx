<%@ WebHandler Language="C#" Class="OptimizeHandler" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MSYS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class OptimizeHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        byte[] postData = new byte[context.Request.InputStream.Length];
        context.Request.InputStream.Read(postData, 0, postData.Length);
        string postDataString = System.Text.Encoding.UTF8.GetString(postData);
        JObject jsonPara = (JObject)JsonConvert.DeserializeObject(postDataString);

        System.Diagnostics.Debug.WriteLine("输出时间");
        System.Diagnostics.Debug.WriteLine(jsonPara["time"]);
        DateTime dt = Convert.ToDateTime(jsonPara["time"]);
        Double rito =  getResponseData(dt);

        var jsonData = new JObject();
        jsonData.Add("data", rito);
        context.Response.ContentType = "json";
        context.Response.Write(jsonData.ToString());
    }

    protected Double getResponseData(DateTime dt) {
        Double spiceData = getSpiceAccumulateData(dt);
        Double materialData = getMaterialAccumulateData(dt);
        if (materialData == 0) {
            return 0;
        }
        return spiceData / materialData;
    }
    
    //获取主称值
    protected Double getMaterialMainData(DateTime dt) {
        string tagname = "FIX.Z4_A085_TOTAL.F_CV";
        string interval="1s";
        DateTime Btime = dt.AddSeconds(-1);
        List<ParaInfo> datalist = getParaRecord(tagname, interval, Btime, dt);
        return datalist[datalist.Count - 1].value;
    }
    
    //获取物料30s累积值
    protected Double getMaterialAccumulateData(DateTime dt) {
        string tagname1 = "FIX.Z4_A085_TOTAL.F_CV";
        return getAccumulateData(tagname1, dt);
    }
    
    //获取香料30s累积值
    protected Double getSpiceAccumulateData(DateTime dt) {
        string tagname1 = "FIX.Z5_A110_Total.F_CV";
        return getAccumulateData(tagname1, dt);
    }
    
    //获取标签30秒累积值
    protected Double getAccumulateData(string tagname, DateTime Etime) { 
        string interval = "1s";
        DateTime Btime = Etime.AddSeconds(-30);
        List<ParaInfo> dataList =  getParaRecord(tagname, interval, Btime, Etime);
       // Double result = 0;
        /*foreach (ParaInfo data in dataList) {
            result += data.value;
        }*/
        return dataList[dataList.Count-1].value - dataList[0].value;
    }
    
    public struct ParaInfo
    {
        public string timestamp;
        public double value;
        //  public string status;
    }

    protected List<ParaInfo> getParaRecord(string tagname, string interval, DateTime Btime, DateTime Etime)
    {
        if (DateTime.Compare(Btime, Etime) > 0)
            return null;
        List<ParaInfo> paralist = new List<ParaInfo>();
        MSYS.DAL.DbOperator ihopt = new MSYS.DAL.DbOperator(new MSYS.DAL.OledbOperator());
        DateTime starttime = Btime;
        DateTime stoptime = Etime;
        if (Etime - Btime > TimeSpan.FromHours(2))
        {
            starttime = Btime;
            stoptime = Btime.AddHours(2);
        }
        do
        {
            string query = "SELECT  timestamp ,value  FROM ihrawdata where tagname = '" + tagname + "' and timestamp between '" + starttime.ToString("yyyy/MM/dd HH:mm:ss") + "' and '" + stoptime.ToString("yyyy/MM/dd HH:mm:ss") + "' and intervalmilliseconds =  " + interval + "s order by timestamp ASC";
            System.Diagnostics.Debug.WriteLine(query);
            DataSet data = ihopt.CreateDataSetOra(query);
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    ParaInfo info = new ParaInfo();
                    info.timestamp = row["timestamp"].ToString();
                    info.value = Convert.ToDouble(row["value"].ToString());
                    paralist.Add(info);
                }
            }
            starttime = stoptime;
            stoptime = starttime.AddHours(2);
            if (DateTime.Compare(stoptime, Etime) > 0)
                stoptime = Etime;
        }
        while (Etime - starttime > TimeSpan.FromHours(2));

        return paralist;
    }

    /*protected void Search_Click(object sender, EventArgs e)
    {
        string tagname = "FIX.Z4_A085_TOTAL.F_CV";
        string interval = "6";
        List<ParaInfo> result = new List<ParaInfo>();
        DateTime Etime = Convert.ToDateTime("2019-02-28,09:37:00");
        DateTime Btime = Convert.ToDateTime("2019-02-28,09:35:30");
        result = getParaRecord(tagname, interval, Btime, Etime);
        System.Diagnostics.Debug.WriteLine(result.Count);
        //ParaInfo tem = new ParaInfo();
        foreach (ParaInfo tem in result)
        {
            System.Diagnostics.Debug.WriteLine(tem.value);
        }
    }*/
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}