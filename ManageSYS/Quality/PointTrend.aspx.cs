using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MSYS.Common;
public partial class Quality_PointTrend : MSYS.Web.BasePage
{
    protected string tvHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            tvHtml = InitTree("");
            txtEtime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            txtBtime.Text = System.DateTime.Now.AddHours(-2).ToString("yyyy-MM-dd HH:mm:ss");
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listPlanno, "select * from ht_prod_report t where  STARTTIME between '" + txtBtime.Text + "' and '" + txtEtime.Text + "' or ENDTIME between '" + txtBtime.Text + "' and '" + txtEtime.Text + "' or (STRATTIME < '" + txtBtime.Text + "' and  ENDTIME > '" + txtEtime.Text + "'" , "PLANNO", "PLANNO");
        }
    }



    public string InitTree(string planno)
    {
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        string query ="select g.section_code,g.section_name from ht_pub_tech_section g where g.IS_VALID = '1' and g.IS_DEL = '0' order by g.section_code ";
        if (planno != "")
            query = "select  g.section_code,g.section_name from ht_prod_report g1 left join  ht_pub_tech_section g on g1.section_code = g.section_code and  g.IS_VALID = '1' and g.IS_DEL = '0' where g1.planno ='" + planno + "' order by g.section_code ";
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul id='browser' class='filetree treeview-famfamfam'>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                // tvHtml += "<li ><a href='Tech_Session.aspx?session_code=" + row["section_code"].ToString() + "' target='sessionFrame'><span class='folder'  onclick = \"$('#tabtop1').click()\">" + row["section_name"].ToString() + "</span></a>";  
                tvHtml += "<li ><span class='folder'>" + row["section_name"].ToString() + "</span>";
                tvHtml += InitTreePara(row["section_code"].ToString());
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }


    public string InitTreePara(string section_code)
    {
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
       DataSet data = opt.CreateDataSetOra("select para_code,para_name from ht_pub_tech_para where substr(para_code,1,5) =  '" + section_code + "' and IS_VALID = '1' and IS_DEL = '0' and  para_type like '___1_'   order by para_code");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                // tvHtml += "<li ><a href='Tech_Para.aspx?para_code=" + row["para_code"].ToString() + "' target='ProcessFrame'><span class='file'  onclick = \"$('#tabtop4').click()\">" + row["para_name"].ToString() + "</span></a>";
                tvHtml += "<li ><span class='file'  onclick = \"treeClick(" + row["para_code"].ToString() + ")\">" + row["para_name"].ToString() + "</span></a>";
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }

 
    protected void txtBtime_TextChanged(object sender, EventArgs e)
    {
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listPlanno, "select * from ht_prod_report t where  STARTTIME between '" + txtBtime.Text + "' and '" + txtEtime.Text + "' or ENDTIME between '" + txtBtime.Text + "' and '" + txtEtime.Text + "' or (STRATTIME < '" + txtBtime.Text + "' and  ENDTIME > '" + txtEtime.Text + "'", "PLANNO", "PLANNO");
    }
    protected void txtEtime_TextChanged(object sender, EventArgs e)
    {
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listPlanno, "select * from ht_prod_report t where  STARTTIME between '" + txtBtime.Text + "' and '" + txtEtime.Text + "' or ENDTIME between '" + txtBtime.Text + "' and '" + txtEtime.Text + "' or (STRATTIME < '" + txtBtime.Text + "' and  ENDTIME > '" + txtEtime.Text + "'", "PLANNO", "PLANNO");
    }
    protected void listPlanno_SelectedIndexChanged(object sender, EventArgs e)
    {
        tvHtml = InitTree(listPlanno.SelectedValue);
    }
}