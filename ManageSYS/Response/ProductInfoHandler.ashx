<%@ WebHandler Language="C#" Class="ProductInfoHandler" %>

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
public struct ProductInfo
{
    public string name { get; set; }
    public double y { get; set; }
    public string drilldown { get; set; }
}
[Serializable]
public struct ProductSeries
{
    public string name;
    public string id;
    public object[][] data;
}
[Serializable]
public struct DataInfo
{
    public ProductInfo[] productinfo;
    public ProductSeries[] productseries;    
}
public struct ResponseProductData
{
    public List<DataInfo> info;
    public string statics {get;set;}
}
public class ProductInfoHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        var data = context.Request["date"];
        try
        {
            ResponseProductData datainfo = getResponseData(data);
            var javaScriptSerializer = new JavaScriptSerializer();
               var responseData = javaScriptSerializer.Serialize(datainfo);
               context.Response.ContentType = "text/plain";
               context.Response.Write(responseData);
        }
        catch (Exception ee)
        {
            var e = ee.Message;
        }
    }
    protected ResponseProductData getResponseData(string date)
    {
        ResponseProductData data = new ResponseProductData();
        data.info = new List<DataInfo>();
        data.info.Add(getPlanDoneInfo(date));
        data.info.Add(getInoutInfo(date));
        data.statics = getStatics(date);
        return data;
    }
    protected DataInfo getPlanDoneInfo(string date)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select distinct t.prod_code,t.plan_output,t2.prod_name from ht_prod_month_plan_detail t left join ht_prod_month_plan t1 on t1.id = t.month_plan_id and t1.is_del = '0' left join ht_pub_prod_design t2 on t2.prod_code = t.prod_code where t.is_del = '0' and  t1.plan_time = '" + date.Substring(0, 7) + "'";
        DataSet data = opt.CreateDataSetOra(query);
        DataInfo res = new DataInfo();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            double total = Convert.ToDouble(opt.GetSegValue("select sum(t.plan_output) as total from ht_prod_month_plan_detail t left join ht_prod_month_plan t1 on t1.id = t.month_plan_id and t1.is_del = '0' left join ht_pub_prod_design t2 on t2.prod_code = t.prod_code where t.is_del = '0' and  t1.plan_time = '" + date.Substring(0, 7) + "'", "total"));
            List<ProductInfo> prdinfo = new List<ProductInfo>();
            List<ProductSeries> prdserie = new List<ProductSeries>();  
            foreach (DataRow row in data.Tables[0].Rows)
            {
                ProductInfo info = new ProductInfo();
                info.name = row["prod_name"].ToString();
                info.y = Convert.ToDouble(row["plan_output"].ToString())/total*100;
                info.drilldown = row["prod_code"].ToString();
                ProductSeries serie = new ProductSeries();
                serie.id = row["prod_code"].ToString();
                serie.name = row["prod_name"].ToString();
                string value = opt.GetSegValue("select prod_code ,sum(outweight) as value  from hv_prod_inout_ratio t where substr(datetime,0,7) = '" + date.Substring(0, 7) + "' and prod_code = '" + row["prod_code"].ToString() + "' group by prod_code", "value");
                value = value == "NoRecord" ? "0" : value;
                List<object[]> list = new List<object[]>();              
                list.Add(new object[2]{"己完成", Convert.ToDouble(value)/total*100});
                list.Add(new object[2]{"未完成", info.y - Convert.ToDouble(value)/total*100});
                serie.data = list.ToArray();
                prdinfo.Add(info);
                prdserie.Add(serie);             
                           
            }
            res.productinfo = prdinfo.ToArray();
            res.productseries = prdserie.ToArray();
        }
        return res;
    }
    protected DataInfo getInoutInfo(string date)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select t.prod_code ,sum(t.outweight) as realout  ,r.prod_name from hv_prod_inout_ratio t left join ht_pub_prod_design r on r.prod_code = t.prod_code  where substr(t.datetime,0,7) = '" + date.Substring(0, 7) + "' group by t.prod_code,r.prod_name";
        DataSet data = opt.CreateDataSetOra(query);
        DataInfo res = new DataInfo();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            List<ProductInfo> prdinfo = new List<ProductInfo>();
            List<ProductSeries> prdserie = new List<ProductSeries>();  
            foreach (DataRow row in data.Tables[0].Rows)
            {
                double total = Convert.ToDouble(opt.GetSegValue("select sum(t.outweight) as total  from hv_prod_inout_ratio t left join ht_pub_prod_design r on r.prod_code = t.prod_code  where substr(t.datetime,0,7) = '" + date.Substring(0, 7) + "'", "total"));
                ProductInfo info = new ProductInfo();
                info.name = row["prod_name"].ToString();
                info.y = Convert.ToDouble(row["realout"].ToString())/total*100;
                info.drilldown = row["prod_code"].ToString();
                ProductSeries serie = new ProductSeries();
                serie.id = row["prod_code"].ToString();
                serie.name = row["prod_name"].ToString();
               
                List<object[]> list = new List<object[]>();               
                DataSet listdata = opt.CreateDataSetOra("select datetime,outweight from hv_prod_inout_ratio where prod_code = '" + serie.id + "' and substr(datetime,1,7) = '" + date.Substring(0, 7) + "'");
                if (listdata != null && listdata.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow lrow in listdata.Tables[0].Rows)                      
                    list.Add(new object[2] { lrow["datetime"].ToString(), Convert.ToDouble(lrow["outweight"].ToString()) / total * 100 });
                }               
                serie.data = list.ToArray();
                prdinfo.Add(info);
                prdserie.Add(serie);  
                
            }
            res.productinfo = prdinfo.ToArray();
            res.productseries = prdserie.ToArray();
        }
        return res;
    }
    protected string getStatics(string date)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select g3.prod_name,g1.plan_output, g2.realout,g1.plan_output-g2.realout from (select t.prod_code,t.plan_output from ht_prod_month_plan_detail t left join ht_prod_month_plan t1 on t1.id = t.month_plan_id  where t1.plan_time = '" + date.Substring(0, 7) + "') g1 left join(select prod_code ,sum(outweight) as realout from hv_prod_inout_ratio t where substr(datetime,0,7) = '" + date.Substring(0, 7) + "' group by prod_code) g2 on g1.prod_code = g2.prod_code left join ht_pub_prod_design g3 on g3.prod_code = g1.prod_code");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            StringBuilder str = new StringBuilder("");
            str.Append("<table width='100%' border='1' cellpadding='0' cellspacing='1' bgcolor='#a8c7ce'>");

            str.Append("<tr>");
            for (int i = 0; i < 4; i++)
            {
                str.Append(" <td  height='25px' bgcolor='d3eaef' class='staticHead' width = '70px' border='1'><div align='center'><span class='staticHeadtext'>");
                if (i == 0)
                    str.Append("产品");
                else if (i == 1)
                    str.Append("计划产量");
                else if (i == 2)
                    str.Append("己完成");
                else
                    str.Append("未完成");
                str.Append("</span></div></td>");
            }
            str.Append("</tr>");
            for (int j = 0; j < data.Tables[0].Rows.Count; j++)
            {
                str.Append("<tr>");
                for (int i = 0; i < data.Tables[0].Columns.Count; i++)
                {
                    str.Append("  <td height='25px' bgcolor='#FFFFFF' class='staticHead' border='1'><div align='center'><span class='staticRow'>");
                    str.Append(data.Tables[0].Rows[j][i].ToString());
                    str.Append("</span></div></td>");
                }
                str.Append("</tr>");
            }
            str.Append("</table>");
            return str.ToString();
        }
        else return "";

    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}