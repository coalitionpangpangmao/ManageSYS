using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Quality_CollectSet : MSYS.Web.BasePage
{
    protected string tvHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            tvHtml = InitTree();
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listSection, "select * from ht_pub_tech_section where is_del = '0' and is_valid = '1' order by section_code", "Section_NAME", "SECTION_CODE");


        }
    }


    public string InitTree()
    {

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select distinct section_code,section_name  from ht_pub_tech_section r left join ht_pub_tech_para s on substr(s.para_code,1,5) = r.section_code and s.is_del = '0' and s.is_valid = '1' where r.is_del = '0' and r.is_valid = '1' and  s.para_type like '___1%'   order by r.section_code ");
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
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select para_code,para_name from ht_pub_tech_para where substr(para_code,1,5) =  '" + section_code + "' and  para_type like '___1%' and IS_VALID = '1' and IS_DEL = '0'   order by para_code");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
              
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
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string[] seg = { "PARA_CODE", "PARA_TYPE", "WEIGHT", "PERIODIC", "RST_VALUE", "HEAD_DELAY", "TAIL_DELAY", "BATCH_HEAD_DELAY", "BATCH_TAIL_DELAY", "IS_GAP_JUDGE", "DESCRIPT", "SYNCHRO_TIME", "CTRL_POINT", "GAP_HDELAY", "GAP_TDELAY","GAP_TIME" };
        string[] value = { txtID.Text, listStyle.SelectedValue, txtWeight.Text, txtPeriodic.Text, txtRstValue.Text, txtHeadDelay.Text, txtTailDelay.Text, txtBatchHDelay.Text, txtBatchTDelay.Text, Convert.ToInt16(rdYes.Checked).ToString(), txtDescript.Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), listGappoint.SelectedValue, txtGapHeadDelay.Text, txtGapTailDelay.Text,txtGapTime.Text };

        string log_message = opt.MergeInto(seg, value, 1, "ht_qlt_collection") == "Success" ? "保存质量采集条件成功" : "保存质量采集条件失败";
        log_message += "--详情:" + string.Join(",", value);
        InsertTlog(log_message);

    }
    protected void Delete_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "delete from ht_qlt_collection where para_code = '" + txtID.Text + "'";
        string log_message = opt.UpDateOra(query) == "Success" ? "删除数采条件成功" : "删除数采条件失败";
        log_message += "--标识:" + txtID.Text;
        InsertTlog(log_message);
        setBlank();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        if (txtID.Text == hidecode.Value)
            return;
        setBlank();
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        txtID.Text = hidecode.Value;
        listSection.SelectedValue = txtID.Text.Substring(0, 5);
        opt.bindDropDownList(listPointname, "select * from ht_pub_tech_para where is_del = '0' and is_valid = '1' and  para_type like '___1%' and  substr(para_code,1,5) = '" + listSection.SelectedValue + "' order by para_code", "PARA_NAME", "PARA_CODE");
        opt.bindDropDownList(listGappoint, "select r.para_code,s.para_name from ht_qlt_collection r left join ht_pub_tech_para s on r.para_code = s.para_code where  r.is_del = '0' and r.is_gap_judge = '1'   and   substr(r.para_code,1,5) = '" + listSection.SelectedValue + "' order by r.para_code", "PARA_NAME", "PARA_CODE");
        listPointname.SelectedValue = txtID.Text;
        DataSet data = opt.CreateDataSetOra("select * from ht_qlt_collection g1  where g1.is_del = '0' and g1.is_valid = '1' and  g1.para_code = '" + txtID.Text + "'");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            txtWeight.Text = data.Tables[0].Rows[0]["WEIGHT"].ToString();
            listStyle.SelectedValue = data.Tables[0].Rows[0]["PARA_TYPE"].ToString();           
            txtPeriodic.Text = data.Tables[0].Rows[0]["PERIODIC"].ToString();
            txtRstValue.Text = data.Tables[0].Rows[0]["RST_VALUE"].ToString();         

            txtHeadDelay.Text = data.Tables[0].Rows[0]["HEAD_DELAY"].ToString();
            txtTailDelay.Text = data.Tables[0].Rows[0]["TAIL_DELAY"].ToString();
            txtBatchHDelay.Text = data.Tables[0].Rows[0]["BATCH_HEAD_DELAY"].ToString();
            txtBatchTDelay.Text = data.Tables[0].Rows[0]["BATCH_TAIL_DELAY"].ToString();
           
            listGappoint.SelectedValue = data.Tables[0].Rows[0]["CTRL_POINT"].ToString();
            txtGapHeadDelay.Text = data.Tables[0].Rows[0]["GAP_HDELAY"].ToString();
            txtGapTailDelay.Text = data.Tables[0].Rows[0]["GAP_TDELAY"].ToString();
            txtDescript.Text = data.Tables[0].Rows[0]["DESCRIPT"].ToString();
            txtGapTime.Text = data.Tables[0].Rows[0]["GAP_TIME"].ToString();
            bool isgappoint = ("1" == data.Tables[0].Rows[0]["IS_GAP_JUDGE"].ToString());
            if (isgappoint)
            {
                rdYes.Checked = true;
                rdNo.Checked = false;
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "showgap", "$('#rdYes').click();", true);
            }
            else
            {
                rdYes.Checked = false;
                rdNo.Checked = true;
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "showgap", "$('#rdNo').click();", true);
            }
        }
     
    }
    protected void listSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listPointname, "select * from ht_pub_tech_para where is_del = '0' and is_valid = '1' and  para_type like '___1%' and  substr(para_code,1,5) = '" + listSection.SelectedValue + "' order by para_code", "PARA_NAME", "PARA_CODE");
        opt.bindDropDownList(listGappoint, "select r.para_code,s.para_name from ht_qlt_collection r left join ht_pub_tech_para s on r.para_code = s.para_code where  r.is_del = '0' and r.is_gap_judge = '1'   and   substr(r.para_code,1,5) = '" + listSection.SelectedValue + "' order by r.para_code", "PARA_NAME", "PARA_CODE");
   
    }
    protected void listPointname_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtID.Text = listPointname.SelectedValue;
    }
    protected void setBlank()
    {
        txtWeight.Text = "";
        listStyle.SelectedValue = "";
    
        txtPeriodic.Text = "0";
        txtRstValue.Text = "0";   
        txtHeadDelay.Text = "0";
        txtTailDelay.Text = "0";
        txtBatchHDelay.Text = "0";
        txtBatchTDelay.Text = "0";
        txtGapHeadDelay.Text = "0";
        txtGapTailDelay.Text = "0";
        txtDescript.Text = "";

        txtID.Text = "";      
        txtGapTime.Text = "0";
    }
}