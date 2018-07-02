using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Threading;
using System.Diagnostics;
//using DbOperator;
using System.Data;

public partial class monitor_monitor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string ReGetCpuInfo() //获取cpu使用率
    {
        PerformanceCounter cpucounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        float cpu = 0;
        while (cpu == 0)
        {
            cpu = cpucounter.NextValue();
            Thread.Sleep(500);
        }

        return cpu.ToString();

    }


    [WebMethod]
    public static string ReGetMemInfo()//获取内存使用率
    {
        PerformanceCounter memCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
        memCounter.NextValue();
        return memCounter.NextValue().ToString();
    }


    [WebMethod]
    public static string ReGetOracleInfo()//获取ORACLE表空间使用率
    {
        string query = "SELECT a.tablespace_name,total,free,( total - free ),Round(( total - free ) / total, 4) * 100  FROM   (SELECT tablespace_name,Sum(bytes) free FROM   DBA_FREE_SPACE GROUP  BY tablespace_name) a, (SELECT tablespace_name,Sum(bytes) total FROM   DBA_DATA_FILES GROUP  BY tablespace_name) b WHERE  a.tablespace_name = b.tablespace_name AND a.tablespace_name='ZS_DATA'";
        //string query = "SELECT * FROM HT_EQ_MT_PLAN ";
        DataBaseOperator opt = new DataBaseOperator();
        DataSet data = null;
        data = opt.CreateDataSetOra(query);
        if (data == null)
            System.Diagnostics.Debug.WriteLine("没有获取到数据库数据");
        return data.Tables[0].Rows[0][4].ToString();
    }
}