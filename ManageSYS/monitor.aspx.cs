using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.Threading;

public partial class monitor : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
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