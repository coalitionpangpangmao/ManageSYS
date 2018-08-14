<%@ WebHandler Language="C#" Class="faultDbHandler" %>

using System;
using System.Web;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Web.Script.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
[Serializable]
public struct RequestDataJSON
{
    public string type { get; set; }
    public string point { get; set; }
    public string startTime { get; set; }
    public string stopTime { get; set; }
}
[Serializable]
public struct ResponseData
{
    public string pointname;
    public double upper;
    public double lower;
    public double value;
    public double errdev;
    public List<string> xAxis;
    public List<double> yAxis;
    public string statics;
}
[Serializable]
public struct pointData
{
    public string timetag;
    public double value;
}
public class faultDbHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        var data = context.Request;
        var sr = new StreamReader(data.InputStream);
        var stream = sr.ReadToEnd();
        var javaScriptSerializer = new JavaScriptSerializer();
        var PostedData = javaScriptSerializer.Deserialize<RequestDataJSON>(stream);
        List<ResponseData> datainfo = new List<ResponseData>();
        if (PostedData.type == "Para")
        {
            datainfo.Add(handleParaData(PostedData));
        }
        else
            datainfo = handleEquipData(PostedData);
        try
        {

            var responseData = javaScriptSerializer.Serialize(datainfo);
            context.Response.ContentType = "text/plain";
            context.Response.Write(responseData);
        }
        catch (Exception error)
        {

        }
    }
    protected ResponseData getData(string prodcode, string point,string starttime,string endtime)
    {
        ResponseData datainfo = new ResponseData();
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet pointinfo;
        if (prodcode != "NoRecord")
            pointinfo = opt.CreateDataSetOra("select t.para_name,s.upper_limit,s.lower_limit,s.value,s.eer_dev,t.value_tag from  ht_tech_stdd_code r left join Ht_Tech_Stdd_Code_Detail s on r.tech_code = s.tech_code  left join ht_pub_tech_para t on s.para_code = t.para_code where r.prod_code = '" + prodcode + "' and t.para_code = '" + point + "'");
        else
            pointinfo = opt.CreateDataSetOra("select t.para_name,t.value_tag from    ht_pub_tech_para t where t.para_code = '" + point + "'");
        if (pointinfo != null && pointinfo.Tables[0].Rows.Count > 0)
        {
            DataRow row = pointinfo.Tables[0].Rows[0];
            string tag = row["value_tag"].ToString();
            datainfo.pointname = row["para_name"].ToString();
            if (pointinfo.Tables[0].Columns.Count >2 )
            {                
                datainfo.upper = Convert.ToDouble(row["upper_limit"].ToString());
                datainfo.lower = Convert.ToDouble(row["lower_limit"].ToString());
                datainfo.value = Convert.ToDouble(row["value"].ToString());
                datainfo.errdev = Convert.ToDouble(row["eer_dev"].ToString());
            }
            if(tag != "")
            {
                 //get rawdata from IHistorian 
                 MSYS.Common.IHDataOpt ihopt = new MSYS.Common.IHDataOpt();
                 DataRowCollection Rows = ihopt.GetData(starttime, endtime, point);
                 if (Rows != null)
                 {
                     datainfo.xAxis = new List<string>();
                     datainfo.yAxis = new List<double>();
                     foreach (DataRow srow in Rows)
                     {
                         datainfo.xAxis.Add(srow[0].ToString().Substring(11));
                         datainfo.yAxis.Add(Convert.ToDouble(srow[1].ToString()));
                     }

                     datainfo.statics = getStatics(datainfo.pointname, datainfo.yAxis.ToArray(), datainfo.upper, datainfo.lower, starttime, endtime);
                 }
            }

        }
        return datainfo;
    }
    protected ResponseData handleParaData(RequestDataJSON PostedData)
    {       
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        //query prod_code
        string prodcode = opt.GetSegValue("select prod_code,starttime from ht_prod_report t where t.starttime <= '" + PostedData.startTime + "' and t.endtime >='" + PostedData.stopTime + "' union select prod_code,starttime from ht_prod_report t where t.endtime > '" + PostedData.startTime + "' and t.endtime <'" + PostedData.stopTime + "' union select prod_code,starttime from  ht_prod_report t where t.starttime >'" + PostedData.startTime + "' and t.starttime <'" + PostedData.stopTime + "' order by starttime", "Prod_code");
        return getData(prodcode, PostedData.point,PostedData.startTime,PostedData.stopTime);
     
    }

    protected List<ResponseData> handleEquipData(RequestDataJSON PostedData)
    {
        List<ResponseData> datainfo = new List<ResponseData>();
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        //query prod_code
        string prodcode = opt.GetSegValue("select prod_code,starttime from ht_prod_report t where t.starttime <= '" + PostedData.startTime + "' and t.endtime >='" + PostedData.stopTime + "' union select prod_code,starttime from ht_prod_report t where t.endtime > '" + PostedData.startTime + "' and t.endtime <'" + PostedData.stopTime + "' union select prod_code,starttime from  ht_prod_report t where t.starttime >'" + PostedData.startTime + "' and t.starttime <'" + PostedData.stopTime + "' order by starttime", "Prod_code");
        //query poininfo
        DataSet pointinfo = opt.CreateDataSetOra("select t.para_name, t.para_code from ht_pub_tech_para t  where  t.EQUIP_CODE = '" + PostedData.point + "'");
        if (pointinfo != null && pointinfo.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in pointinfo.Tables[0].Rows)
            {
                datainfo.Add(getData(prodcode,row["para_code"].ToString(),PostedData.startTime,PostedData.stopTime));
              
            }
        }
        return datainfo;
    }



    protected string getStatics(string name, double[] array, double upper, double lower, string start, string end)
    {

        MSYS.Common.SPCFunctions spc = new MSYS.Common.SPCFunctions(array, upper, lower);
        string[] colname = { "工艺点", "总点数", "最大值", "最小值", "均值", "合格率", "超上限率", "超下限率", "标准差", "绝对差", "CPK", "开始时间", "结束时间" };
        object[] colvalue = { name, spc.count, spc.max, spc.min, spc.avg.ToString("0.00"), spc.passrate.ToString("0.00"), spc.uprate.ToString("0.00"), spc.downrate.ToString("0.00"), spc.absdev.ToString("0.00"), spc.stddev.ToString("0.00"), spc.Cpk.ToString("0.00"), start, end };
        if (colname.Length == colvalue.Length)
        {
            StringBuilder str = new StringBuilder("");
            str.Append("<table width='100%' border='0' cellpadding='0' cellspacing='1' bgcolor='#a8c7ce'>");
            str.Append("<tr>");
            foreach (string item in colname)
            {
                str.Append(" <td  height='20' bgcolor='d3eaef' class='staticHead'><div align='center'><span class='staticHeadtext'>");
                str.Append(item);
                str.Append("</span></div></td>");
            }
            str.Append("</tr>");

            str.Append("<tr>");
            foreach (object item in colvalue)
            {
                str.Append("  <td height='20' bgcolor='#FFFFFF' class='staticHead'><div align='center'><span class='staticRow'>");
                str.Append(item.ToString());
                str.Append("</span></div></td>");
            }
            str.Append("</tr>");
            str.Append("</table>");
            return str.ToString();
        }
        else
            return null;

    }


    protected string DataTableToJsonUsingJsSerializer(DataTable table)
    {
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
        Dictionary<string, object> childRow;
        foreach (DataRow row in table.Rows)
        {
            childRow = new Dictionary<string, object>();
            foreach (DataColumn col in table.Columns)
            {
                childRow.Add(col.ColumnName, row[col]);
            }
            parentRow.Add(childRow);
        }
        return jsSerializer.Serialize(parentRow);
    }

    protected string DataTableToJson(DataTable table)
    {
        StringBuilder str = new StringBuilder();

        bool head = true;
        foreach (DataRow row in table.Rows)
        {
            if (head == false)
                str.Append(",[");
            else
            {
                str.Append("[");
                head = false;
            }
            //  str.Append("\"Time\":");
            str.Append(row[0].ToString());
            str.Append(",");
            //  str.Append(",\"Value\":");
            str.Append(row[1].ToString());
            str.Append("]");
        }

        return str.ToString(); ;
    }
    protected List<pointData> DataTableToList(DataTable table)
    {
        List<pointData> list = new List<pointData>();
        foreach (DataRow row in table.Rows)
        {
            pointData data = new pointData();
            data.timetag = row[0].ToString();
            data.value = Convert.ToDouble(row[1].ToString());
            list.Add(data);
        }

        return list;
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}