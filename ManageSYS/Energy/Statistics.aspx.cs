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

public partial class Statistics : System.Web.UI.Page
{
    protected string tvHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        // base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void BindData()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "SELECT DISTINCT ENG_CODE, ENG_NAME FROM HT_ENG_CONSUMPTION_ITEM WHERE IS_VALID=1 AND IS_DEL !=1";
        opt.bindDropDownList(ENG_NAME, query, "ENG_NAME", "ENG_CODE");
        opt.bindDropDownList(PROCESS_NAME, "SELECT DISTINCT PROCESS_CODE ,PROCESS_NAME FROM HT_ENG_CONSUMPTION_ITEM WHERE IS_VALID=1 AND IS_DEL !=1", "PROCESS_NAME", "PROCESS_CODE");
        opt.bindDropDownList(UNIT_NAME, "SELECT DISTINCT UNIT_CODE, UNIT_NAME FROM HT_ENG_CONSUMPTION_ITEM WHERE IS_VALID=1 AND IS_DEL!=1", "UNIT_NAME", "UNIT_CODE");
    }

    protected void QueryData(object sender, EventArgs e)
    {
        string eng = (ENG_NAME.SelectedItem.Text!="") ? (" AND ENG_CODE = "+ENG_NAME.SelectedItem.Value) : "";
        System.Diagnostics.Debug.WriteLine("eng_code is "+ ENG_NAME.SelectedItem.Value);
        string process = (PROCESS_NAME.SelectedItem.Text != "") ? ("AND PROCESS_CODE = " +PROCESS_NAME.SelectedItem.Value) : "";
        System.Diagnostics.Debug.WriteLine("process_code is " + PROCESS_NAME.SelectedItem.Value);
        string unit = (UNIT_NAME.SelectedItem.Text != "") ? ("AND UNIT_CODE="+UNIT_NAME.SelectedItem.Value) : "";
        System.Diagnostics.Debug.WriteLine("unit_code is " + UNIT_NAME.SelectedItem.Value);
        if (StartTime.Text == "" || EndTime.Text == "" || unit=="")
        {
            Response.Write("测试能否接收到数据");
        }
        string query = "SELECT ID as 记录ID, ENG_NAME as 能耗点, PROCESS_NAME as 工序编码, TIME 日期, AMOUNT as 能耗总量, UNIT_NAME as 单位 FROM HT_ENG_MANUAL_DATA WHERE IS_DEL != 1 AND IS_VALID = 1 AND TIME >='" + StartTime.Text + "' AND TIME<='" + EndTime.Text+"'";
        string querycount = "SELECT SUM(AMOUNT) FROM HT_ENG_MANUAL_DATA WHERE IS_DEL = 0 AND IS_VALID = 1 AND TIME >='" + StartTime.Text + "' AND TIME<='" + EndTime.Text+"'";
        query = query + eng + process + unit;
        querycount = querycount + eng + process + unit;
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data == null)
        {
            System.Diagnostics.Debug.WriteLine("从数据库获取信息失败");
        }
        else
        {
            GridView1.DataSource = data;
            GridView1.DataBind();
        }
        data = opt.CreateDataSetOra(querycount);
        COUNT.Text = data.Tables[0].Rows[0][0].ToString();
        return;
    }


}