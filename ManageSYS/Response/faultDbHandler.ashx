<%@ WebHandler Language="C#" Class="faultDbHandler" %>

using System;
using System.Web;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Web.Script.Serialization;
[Serializable]
public struct KeyWordData
{
    public string Equip { get; set; }
    public string key { get; set; }
   
}
public class faultDbHandler : IHttpHandler {   
   
 
    public bool IsReusable {
        get {
            return false;
        }
    }
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        var data = context.Request;
        var sr = new StreamReader(data.InputStream);
        var stream = sr.ReadToEnd();
        var javaScriptSerializer = new JavaScriptSerializer();
        try
        {
            var PostedData = javaScriptSerializer.Deserialize<KeyWordData>(stream);
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            //////////////首先直接根据设备ID及故障名查找故障库中数据////////////////////////
            string query = "select ERROR_NAME,ID from HT_EQ_FAULT_DB where IS_DEL = '0'";
            if (PostedData.Equip != null && PostedData.Equip != "")
            {
                query += " and EQUIP_CODE = '" + PostedData.Equip + "'";
                if (PostedData.key != null && PostedData.key != "")
                    query += " and ERROR_NAME like '%" + PostedData.key + "%'";
            } 
            DataSet res = opt.CreateDataSetOra(query);
            ///////如果故障库中记录为空则横向查打同类设备有过的故障////////////
            if (res == null || res.Tables[0].Rows.Count == 0)
            {
               DataSet eqData = opt.CreateDataSetOra("select * from ht_eq_eqp_tbl where IDKEY = '"+ PostedData.Equip + "'");
                if(eqData != null && eqData.Tables[0].Rows.Count >0)
                {
                    DataRow row = eqData.Tables[0].Rows[0];
                    string model = row["EQ_MODEL"].ToString();//设备型号
                    string type = row["EQ_TYPE"].ToString();//企业设备分类
                    string code = row["CLS_CODE"].ToString();//分类编号
                    string mfct = row["MANUFACTURER"].ToString();//制造商
                    query = "select ERROR_NAME,ID from HT_EQ_FAULT_DB where EQUIP_CODE in ( select  IDKEY from ht_eq_eqp_tbl where is_del = '0'";
                    if (model != "")
                        query += " and EQ_MODEL like '" + model + "%'";
                    if (type != "")
                        query += " and EQ_TYPE = '" + type + "'";
                    if (code != "")
                        query += " and CLS_CODE = '" + code + "'";
                    if (mfct != "")
                        query += " and MANUFACTURER = '" + mfct + "'";
                    query += ")";
                    res = opt.CreateDataSetOra(query);
                }   
            }
            string result = "";
            if (res != null)
            {
                DataTable dt = res.Tables[0];               
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    result += "<p onMouseOver=\"this.style.backgroundColor='#B4D7E9';\" onMouseOut=\"this.style.backgroundColor=''\" onclick=\"fillData('" + dt.Rows[i]["ID"].ToString().Trim() + "')\" style=\"cursor:pointer\">" + dt.Rows[i]["ERROR_NAME"].ToString().Trim() + "</p>";
                }
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(result);
           
        }
        catch (Exception ee)
        {
            var e = ee.Message;
        }
    }
    /*
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
       
        var key = Microsoft.JScript.GlobalObject.unescape(context.Request["key"]).Trim();
        string Remark = "1=1";
        if (key == null || key == "")
        {
            Remark = "1=1";
        }
        else
        {
            Remark = " ERROR_DESCRIPTION like '%" + key + "%'";
        }
        //DataTable dt = new chat.dal.chatconnect().chat_service_getlistremark(1, Remark);
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select * from HT_EQ_FAULT_DB where " + Remark);
        DataTable dt = data.Tables[0];
        string result = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            result += "<p onMouseOver=\"this.style.backgroundColor='#B4D7E9';\" onMouseOut=\"this.style.backgroundColor=''\" onclick=\"clickkey('" + dt.Rows[i]["ERROR_DESCRIPTION"].ToString().Trim() + "'," + dt.Rows[i]["id"].ToString().Trim() + ");\" style=\"cursor:pointer\">" + dt.Rows[i]["ERROR_DESCRIPTION"].ToString().Trim() + "</p>";
        }      
        context.Response.Write(result);
        Debug.Assert(key != null );   
    }*/

 
}