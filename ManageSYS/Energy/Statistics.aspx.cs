using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Text;
using System.Drawing;
using Newtonsoft.Json;

public partial class Energy_Statistics : System.Web.UI.Page
{
    protected string tvHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
       // base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            tvHtml = InitTree();
        }
    }


    public string InitTree()
    {

        DataBaseOperator opt = new DataBaseOperator();
        //DataSet data = opt.CreateDataSetOra("select g.section_code,g.section_name from ht_pub_tech_section g where g.IS_VALID = '1' and g.IS_DEL = '0' order by g.section_code ");
        DataSet data = opt.CreateDataSetOra("select NAME, ENG_CODE from HT_ENG_STATISTICS where IS_VALID =1 and IS_DEL =0 group by NAME, ENG_CODE");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul id='browser' class='filetree treeview-famfamfam'>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                // tvHtml += "<li ><a href='Tech_Session.aspx?session_code=" + row["section_code"].ToString() + "' target='sessionFrame'><span class='folder'  onclick = \"$('#tabtop1').click()\">" + row["section_name"].ToString() + "</span></a>";  
                
                //tvHtml += "<li ><span class='folder'  onclick = \"tab1Click(" + row["NAME"].ToString() + ")\">" + row["NAME"].ToString() + "</span>";
                tvHtml += "<li ><span class='folder'  onclick = \"requestColumnChart()\">" + row["NAME"].ToString() + "</span>";
                tvHtml += InitTreeProcess(row["ENG_CODE"].ToString());
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }

    public string InitTreeProcess(string section_code)
    {
        DataBaseOperator opt = new DataBaseOperator();
        //DataSet data = opt.CreateDataSetOra("select h.process_code,h.process_name from  ht_pub_inspect_process h where substr(h.process_code,1,5) = '" + section_code + "' and h.IS_VALID = '1' and h.IS_DEL = '0' order by h.process_code");
        
        DataSet data = opt.CreateDataSetOra("select distinct(PROCESS_CODE) from HT_ENG_STATISTICS where ENG_CODE='" + section_code+"'");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                // tvHtml += "<li ><a href='Tech_Process.aspx?process_code=" + row["process_code"].ToString() + "'  target='ProcessFrame'><span class='folder'  onclick = \"$('#tabtop2').click()\">" + row["process_name"].ToString() + "</span></a>";        
                //tvHtml += "<li ><span class='file'  onclick = \"tab2Click(" + row["PROCESS_CODE"].ToString() + ")\">" + row["PROCESS_CODE"]+"</span>";
                tvHtml += "<li ><span class='file'  onclick = \"requestOracleData()\">" + row["PROCESS_CODE"] + "</span>";

                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }


    [WebMethod]
    public static string getConsumption(string name, string unit)
    {
        string result = "";
        string query = "select PROCESS_CODE as name, sum(AMOUNT) as y  from HT_ENG_STATISTICS where IS_DEL =0 and IS_VALID=1 and NAME = '" + name+"'" +"and" +"UNIT='" +unit+"'" + "group by PROCESS_CODE";
        DataBaseOperator opt = new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data == null)
        {
            System.Diagnostics.Debug.WriteLine("没有获取到数据");
        }
        else {
            result = ToJson(data.Tables[0]);
        }


        return result;
    }


    [WebMethod]
    public static  string getProcessConsumption(string name, string process, string unit)
    {
        string result = "";
        System.Diagnostics.Debug.WriteLine("running:"+name+process+unit);
        string query = "select PROCESS_CODE as name, sum(AMOUNT) as y  from HT_ENG_STATISTICS where IS_DEL =0 and IS_VALID=1 and NAME = '" + name +"' " + "and " + "UNIT=" + unit  + " and PROCESS_CODE="+process+" group by PROCESS_CODE";
        DataBaseOperator opt = new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data == null)
        {
            System.Diagnostics.Debug.WriteLine("没有获取到数据");
        }
        else
        {
            result = ToJson(data.Tables[0]);
            System.Diagnostics.Debug.WriteLine(result);
        }


        return result;
    }

    public static string ToJson(DataTable dt)
    {
        string JsonString = string.Empty;
        JsonString = JsonConvert.SerializeObject(dt);
        return JsonString;
    }
   
}