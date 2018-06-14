using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Quality_Statistic : MSYS.Web.BasePage
{
    protected string tvHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            tvHtml = InitTree();
            txtEtime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            txtBtime.Text = System.DateTime.Now.AddHours(-2).ToString("yyyy-MM-dd HH:mm:ss");
          
        }
    }



    public string InitTree()
    {
       DataBaseOperator opt =new DataBaseOperator();
        string query = "select g.section_code,g.section_name from ht_pub_tech_section g where g.IS_VALID = '1' and g.IS_DEL = '0' order by g.section_code ";       
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul id='browser' class='filetree treeview-famfamfam'>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                // tvHtml += "<li ><a href='Tech_Session.aspx?session_code=" + row["section_code"].ToString() + "' target='sessionFrame'><span class='folder'  onclick = \"$('#tabtop1').click()\">" + row["section_name"].ToString() + "</span></a>";                 
                tvHtml += "<li ><span class='folder'  onclick = \"treeClick(" + row["section_code"].ToString() + ")\">" + row["section_name"].ToString() + "</span>";
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
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra("select para_code,para_name from ht_pub_tech_para where substr(para_code,1,5) =  '" + section_code + "' and IS_VALID = '1' and IS_DEL = '0'   order by para_code");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                // tvHtml += "<li ><a href='Tech_Para.aspx?para_code=" + row["para_code"].ToString() + "' target='ProcessFrame'><span class='file'  onclick = \"$('#tabtop4').click()\">" + row["para_name"].ToString() + "</span></a>";
                tvHtml += "<li ><span class='file'  onclick = \"treeClick(" + row["para_code"].ToString() + ")\">" + row["para_name"].ToString() + "</span>";
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
       DataBaseOperator opt =new DataBaseOperator();
        string query = "select s.para_name as 统计项,r.b_time as 开始时间,r.e_time as 结束时间,r.shift as 班组,r.plan_id as 计划号,r.avg as 均值,r.min as 最小值,r.max as 最大值,r.range as 跨距,r.count as 总点数,r.quarate as 合格率,r.uprate as 超上限率,r.downrate as 超下限率,r.stddev as 标准差,r.absdev as 绝对差,r.var as 方差,r.cpk as CPK,r.is_gap as 断流,r.gap_time as  断流时间  from HT_QLT_DATA_RECORD r left join ht_pub_tech_para s on s.para_code = r.para_code where r.b_time > '" + txtBtime.Text + "' and r.e_time < '" + txtEtime.Text + "'";
        if (hdPrcd.Value.Length == 10)
            query += " and r.para_code = '" + hdPrcd.Value + "'";
        if (hdPrcd.Value.Length == 5)
            query += " and substr(r.para_code,1,5) = '" + hdPrcd.Value + "'";
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();

    }


   
}