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
        DataSet data = opt.CreateDataSetOra("select para_code,para_name from ht_pub_tech_para where substr(para_code,1,5) =  '" + section_code + "' and IS_VALID = '1' and IS_DEL = '0'   order by para_code");
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

    protected void btnPara_Click(object sender, EventArgs e)
    {
        ///////////////////从IH数据库中读取数据////////////////////////////////////////////////
        MSYS.Common.IHDataOpt ihopt = new MSYS.Common.IHDataOpt();
        DataTable data = ihopt.GetIHOrgDataSet(ihopt.GetTimeSeg(txtBtime.Text, txtEtime.Text, hdPrcd.Value, listPlanno.SelectedValue));
        GridView1.DataSource = data;
        GridView1.DataBind();
        
        /////////////////////////////////////////////////////////////////
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        DataSet set = opt.CreateDataSetOra("select g1.para_code,g1.value,g1.upper_limit,g1.lower_limit from ht_prod_month_plan_detail g left join ht_pub_prod_design g2 on g2.prod_code = g.prod_code left join ht_tech_stdd_code_detail g1 on g1.tech_code = g2.tech_stdd_code where g1.para_code = '" + hdPrcd.Value + "' and g.plan_no = '" + listPlanno.SelectedValue + "'");
        if (set != null && set.Tables[0].Rows.Count > 0)
        {
            txtUPValue.Text = set.Tables[0].Rows[0]["upper_limit"].ToString();
            txtLowValue.Text = set.Tables[0].Rows[0]["lower_limit"].ToString();
            txtStandardValue.Text = set.Tables[0].Rows[0]["value"].ToString();
        }
        ///////////////////////////////////////////////////////////////////         
        set = opt.CreateDataSetOra("select * from HT_QLT_COLLECTION where PARA_CODE = '" + hdPrcd.Value + "'");
        if (set != null && set.Tables[0].Rows.Count > 0)
        {

            DataRow row = set.Tables[0].Rows[0];

            txtTagname.Text = row["CUTOFF_POINT_TAG"].ToString();
            txtPeriodic.Text = row["PERIODIC"].ToString();
            txtTagRst.Text = row["TAILLOGIC_TAG"].ToString() + row["TAILLOGIC_RST"].ToString() + row["TAILLOGIC_RST_VALUE"].ToString();
            txtHeadDelay.Text = row["HEAD_DELAY"].ToString();//料头
            txtTailDelay.Text = row["TAIL_DELAY"].ToString();//料尾
        }
        /////////////////////////////////////////////////////////////////////////////////////
        if (data != null && txtUPValue.Text != "" && txtLowValue.Text != "")
        {
        object[] nums =data.AsEnumerable().Select(r => r["值"]).ToArray();
        SPCFunctions spc = new SPCFunctions(nums, Convert.ToDouble(txtUPValue.Text), Convert.ToDouble(txtLowValue.Text));
         txtAverg.Text = spc.avg.ToString("f2");
            txtTotalCount.Text = spc.count.ToString("f2");
            txtQuaNum.Text = spc.passcount.ToString();
            txtQuaRate.Text = spc.passrate.ToString("f2");
            txtUpcount.Text = spc.upcount.ToString();
            txtUprate.Text = spc.uprate.ToString("f2");
            txtDownCount.Text = spc.downcount.ToString();
            txtDownRate.Text = spc.downrate.ToString("f2");
            txtStdDev.Text = spc.stddev.ToString("f2");
            txtMin.Text = spc.min.ToString("f2");
            txtAbsDev.Text = spc.absdev.ToString("f2");
            txtMax.Text = spc.max.ToString("f2");
            txtCak.Text = spc.K.ToString("f2");
            txtCp.Text = spc.Cp.ToString("f2");
            txtCpk.Text = spc.Cpk.ToString("f2");
            txtR.Text = spc.Range.ToString("f2");
        }
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