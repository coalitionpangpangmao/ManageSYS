using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Threading;
using System.Diagnostics;

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
}