using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MSYS.Common;
using System.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public partial class Comparison : MSYS.Web.BasePage
{
    protected string tvHtml;
    protected string JavaHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            System.Diagnostics.Debug.WriteLine("正在初始化下拉菜单");
            opt.bindDropDownList(energyConsumptionPoint,"SELECT DISTINCT ENG_CODE, ENG_NAME FROM HT_ENG_CONSUMPTION_ITEM WHERE IS_DEL!=1 AND IS_VALID =1", "ENG_NAME", "ENG_CODE");
            System.Diagnostics.Debug.WriteLine("绑定结束");
        }
    }

    protected void bindProcessList(object sender, EventArgs e) //当能耗点变动时更新工艺段可选内容
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(processName, "SELECT DISTINCT PROCESS_CODE, PROCESS_NAME FROM HT_ENG_CONSUMPTION_ITEM WHERE IS_DEL=0 AND ENG_CODE=" + energyConsumptionPoint.SelectedValue.ToString(), "PROCESS_NAME", "PROCESS_CODE");

    }

    protected void bindUnitList(object senders, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(department, "SELECT DISTINCT UNIT_CODE ,UNIT_NAME FROM HT_ENG_CONSUMPTION_ITEM WHERE IS_DEL =0 AND ENG_CODE = " + energyConsumptionPoint.SelectedValue.ToString() + " AND PROCESS_CODE=" + processName.SelectedValue.ToString(), "UNIT_NAME", "UNIT_CODE");
    }

    [WebMethod]
    public static string GetComparisonByTimeData(string btime, string etime)
    {
        var json = new JObject();
        var categories = new JArray();
        var consumptiondata = new JArray();
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "SELECT PROCESS_NAME, SUM(AMOUNT) FROM HT_ENG_MANUAL_DATA WHERE IS_DEL!=1 AND IS_VALID = 1 AND TIME > '"+btime+"' AND TIME < '"+etime+"' GROUP BY PROCESS_NAME";
        DataSet data = opt.CreateDataSetOra(query);
        for (int i = 0; i < data.Tables[0].Rows.Count; i++)
        {
            categories.Add(data.Tables[0].Rows[i][0]);
            var temp = new JObject { { "name", data.Tables[0].Rows[i][0].ToString() }, {"y", data.Tables[0].Rows[i][1].ToString() } };
            consumptiondata.Add(temp);
        }
        json.Add("categories", categories);
        json.Add("comparisonData",consumptiondata);
        return json.ToString(); 
    }

    [WebMethod]
    public static string GetComparisonByProcessName(string btime, string etime, string consumptionPoint, string processName, string unitName) {
        var json = new JObject();
        var time = new JArray();
        var comparisonData = new JArray();
        System.Diagnostics.Debug.WriteLine("btime内容值："+btime);
        System.Diagnostics.Debug.WriteLine("etiem内容："+etime);
        System.Diagnostics.Debug.WriteLine("consumptionPoint 内容值"+consumptionPoint);
        System.Diagnostics.Debug.WriteLine("processName 内容值" + processName);
        System.Diagnostics.Debug.WriteLine("unitName 内容值" + unitName);
        string query = "SELECT TIME, AMOUNT, PROCESS_NAME FROM HT_ENG_MANUAL_DATA WHERE IS_DEL!=1 AND IS_VALID =1 AND TIME >= '" + btime + "' AND TIME <= '" + etime + "'" + " AND ENG_CODE = " + consumptionPoint + " AND PROCESS_CODE=" + processName + " AND UNIT_CODE = " + unitName + "";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        System.Diagnostics.Debug.WriteLine(data.Tables[0].Rows.Count);
        for (int i = 0; i < data.Tables[0].Rows.Count; i++)
        {
            time.Add(data.Tables[0].Rows[i][0]);
            var temp = new JObject{{"name",data.Tables[0].Rows[i][2].ToString()}, {"y", data.Tables[0].Rows[i][1].ToString()}};
            comparisonData.Add(temp);
        }
        json.Add("time", time);
        json.Add("comparisonData", comparisonData);
        return json.ToString();
    }

    [WebMethod]
    public static string GetStatisticData(string type, string time)
    {
        var json = new JObject();
        var units = new JArray();
        var statisticData = new JArray();
        string query = "";
        System.Diagnostics.Debug.WriteLine("postdata: " + type + time);
        if (type == "0")
        {
            query = "SELECT UNIT_NAME, SUM(AMOUNT), UNIT_CODE FROM HT_ENG_MANUAL_DATA WHERE IS_DEL!=1 AND IS_VALID = 1 AND TIME= '" + time + "' GROUP BY UNIT_CODE, UNIT_NAME";
        }
        else 
        {            
            query = "SELECT UNIT_NAME, SUM(AMOUNT), UNIT_CODE FROM HT_ENG_MANUAL_DATA WHERE IS_DEL!=1 AND IS_VALID = 1 AND TIME LIKE '" + time + "%' GROUP BY UNIT_CODE, UNIT_NAME";
        }
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        for (int i = 0; i < data.Tables[0].Rows.Count; i++)
        {
             units.Add(data.Tables[0].Rows[i][0].ToString());
            var temp = new JObject { { "name", data.Tables[0].Rows[i][0].ToString() }, { "y", data.Tables[0].Rows[i][1].ToString() } };
            statisticData.Add(temp);
        }
        json.Add("units", units);
        json.Add("statisticData", statisticData);
        return json.ToString();
        
    }
   
}