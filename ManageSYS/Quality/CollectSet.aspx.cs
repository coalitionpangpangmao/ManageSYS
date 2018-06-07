using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Quality_CollectSet : System.Web.UI.Page
{
    protected string tvHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tvHtml = InitTree();
           DataBaseOperator opt =new DataBaseOperator();
            opt.bindDropDownList(listSection, "select * from ht_pub_tech_section where is_del = '0' and is_valid = '1' order by section_code", "Section_NAME", "SECTION_CODE");
          

        }
    }


    public string InitTree()
    {

       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra("select g.section_code,g.section_name from ht_pub_tech_section g where g.IS_VALID = '1' and g.IS_DEL = '0' order by g.section_code ");
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

 

    protected void Save_Click(object sender, EventArgs e)
    {
       DataBaseOperator opt =new DataBaseOperator();
        opt.UpDateOra("delete from ht_qlt_collection where para_code = '" + txtID.Text + "'");
        string[] seg = { "PARA_CODE", "WEIGHT","PERIODIC","VARMONITOR_TAG","DESCRIPT","TAILLOGIC_TAG","TAILLOGIC_RST","TAILLOGIC_RST_VALUE","HEAD_DELAY","TAIL_DELAY","CUTOFF_POINT_TAG","CUTOFF_RST","CUTOFF_RST_VALUE","BATCH_HEAD_DELAY","BATCH_TAIL_DELAY","CUTOFF_TIMEGAP" ,"PARA_TYPE"};
        string[] value = { txtID.Text, txtWeight.Text, txtPeriodic.Text, txtVarTag.Text, txtDescript.Text, TailList.SelectedValue, TailSignList.SelectedValue, txtTailValue.Text, txtHeadDelay.Text, txtTailDelay.Text, LogicList.SelectedValue, SignList.SelectedValue, txtDesValue.Text, txtBatchHeadDelay.Text, txtBatchTailDelay.Text, txtTimeseg.Text ,listStyle.SelectedValue};
        opt.InsertData(seg, value, "ht_qlt_collection");

    }
    protected void Delete_Click(object sender, EventArgs e)
    {
       DataBaseOperator opt =new DataBaseOperator();
        opt.UpDateOra("delete from ht_qlt_collection where para_code = '" + txtID.Text + "'");
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
       DataBaseOperator opt =new DataBaseOperator();
        txtID.Text = hidecode.Value;
        listSection.SelectedValue = txtID.Text.Substring(0, 5);
        opt.bindDropDownList(listPointname, "select * from ht_pub_tech_para where is_del = '0' and is_valid = '1' and  substr(para_code,1,5) = '" + listSection.SelectedValue + "' order by para_code", "PARA_NAME", "PARA_CODE");
        opt.bindDropDownList(TailList, "select * from ht_pub_tech_para where is_del = '0' and is_valid = '1' and  substr(para_code,1,5) = '" + listSection.SelectedValue + "' order by para_code", "PARA_NAME", "PARA_CODE");
        opt.bindDropDownList(LogicList, "select * from ht_pub_tech_para where is_del = '0' and is_valid = '1' and  substr(para_code,1,5) = '" + listSection.SelectedValue + "' order by para_code", "PARA_NAME", "PARA_CODE");
        listPointname.SelectedValue = txtID.Text;
        DataSet data = opt.CreateDataSetOra("select * from ht_qlt_collection g1  where g1.is_del = '0' and g1.is_valid = '1' and  g1.para_code = '" + txtID.Text + "'");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {

            txtWeight.Text = data.Tables[0].Rows[0]["WEIGHT"].ToString();           
            listStyle.SelectedValue = data.Tables[0].Rows[0]["PARA_TYPE"].ToString();
            txtPeriodic.Text = data.Tables[0].Rows[0]["PERIODIC"].ToString();
            txtVarTag.Text = data.Tables[0].Rows[0]["VARMONITOR_TAG"].ToString();
            txtCutoffTag.Text = data.Tables[0].Rows[0]["CUTOFF_POINT_TAG"].ToString();
            txtDescript.Text = data.Tables[0].Rows[0]["DESCRIPT"].ToString();

            TailList.SelectedValue = data.Tables[0].Rows[0]["TAILLOGIC_TAG"].ToString(); 
            TailSignList.SelectedValue = data.Tables[0].Rows[0]["TAILLOGIC_RST"].ToString();
            txtTailValue.Text = data.Tables[0].Rows[0]["TAILLOGIC_RST_VALUE"].ToString();
            txtHeadDelay.Text = data.Tables[0].Rows[0]["HEAD_DELAY"].ToString();
            txtTailDelay.Text = data.Tables[0].Rows[0]["TAIL_DELAY"].ToString(); 

            LogicList.SelectedValue = data.Tables[0].Rows[0]["CUTOFF_POINT_TAG"].ToString();
            SignList.SelectedValue = data.Tables[0].Rows[0]["CUTOFF_RST"].ToString();
            txtDesValue.Text = data.Tables[0].Rows[0]["CUTOFF_RST_VALUE"].ToString();
            txtBatchHeadDelay.Text = data.Tables[0].Rows[0]["BATCH_HEAD_DELAY"].ToString(); 
            txtBatchTailDelay.Text = data.Tables[0].Rows[0]["BATCH_TAIL_DELAY"].ToString();
            txtTimeseg.Text = data.Tables[0].Rows[0]["CUTOFF_TIMEGAP"].ToString();

            data = opt.CreateDataSetOra("select   * from ht_qlt_collection  where is_del = '0' and is_valid = '1' and  para_code = '" + txtID.Text + "'");
            GridView1.DataSource = data;
            GridView1.DataBind();

        }
    }
    protected void listSection_SelectedIndexChanged(object sender, EventArgs e)
    {
       DataBaseOperator opt =new DataBaseOperator();        
        opt.bindDropDownList(listPointname, "select * from ht_pub_tech_para where is_del = '0' and is_valid = '1' and  substr(para_code,1,5) = '" + listSection.SelectedValue + "' order by para_code", "PARA_NAME", "PARA_CODE");
        opt.bindDropDownList(TailList, "select * from ht_pub_tech_para where is_del = '0' and is_valid = '1' and  substr(para_code,1,5) = '" + listSection.SelectedValue + "' order by para_code", "PARA_NAME", "PARA_CODE");
        opt.bindDropDownList(LogicList, "select * from ht_pub_tech_para where is_del = '0' and is_valid = '1' and  substr(para_code,1,5) = '" + listSection.SelectedValue + "' order by para_code", "PARA_NAME", "PARA_CODE");
    }
    protected void listPointname_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtID.Text = listPointname.SelectedValue;
    }
}