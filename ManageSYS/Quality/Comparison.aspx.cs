using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
public partial class Quality_Comparison : MSYS.Web.BasePage
{
    protected string tvHtml;
    protected string JavaHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            txtEtime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            txtBtime.Text = System.DateTime.Now.AddHours(-2).ToString("yyyy-MM-dd HH:mm:ss");
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listpara, "select para_code,para_name from ht_pub_tech_para where  IS_VALID = '1' and IS_DEL = '0' and  para_type like '___1%'   order by para_code", "para_name", "Para_CODE");
            tvHtml = InitTree();
        }
    }

    public string InitTree()
    {

       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
       DataSet data = opt.CreateDataSetOra("select distinct r.section_code ,r.section_name from ht_pub_tech_section r left join ht_pub_tech_para s on substr(s.para_code,1,5) = r.section_code and s.is_del = '0' and s.is_valid = '1' where r.is_del = '0' and r.is_valid = '1' and  s.para_type like '___1%'   order by r.section_code");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul id='browser' class='filetree treeview-famfamfam'>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {                
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
       DataSet data = opt.CreateDataSetOra("select para_code,para_name from ht_pub_tech_para where substr(para_code,1,5) =  '" + section_code + "' and IS_VALID = '1' and IS_DEL = '0' and  para_type like '___1%'   order by para_code");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                tvHtml += "<li ><input type='checkbox' value = '" + row["para_code"].ToString() +  "' onclick = 'treeClick(this)'/>" + row["para_name"].ToString();
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        listpara.SelectedValue = hidecode.Value;
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        string paraname = opt.GetSegValue("select * from ht_pub_tech_para where para_code = '" + hidecode.Value + "'","para_name");
        ListItem item = new ListItem(paraname+ "_" + txtBtime.Text + "~" + txtEtime.Text , hidecode.Value); 
        cklistPara.Items.Add(item);

    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        listpara.SelectedValue = hidecode.Value;
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        string paraname = opt.GetSegValue("select * from ht_pub_tech_para where para_code = '" + hidecode.Value + "'", "para_name");
        ListItem item = new ListItem(paraname + "_" + txtBtime.Text + "~" + txtEtime.Text, hidecode.Value);
        cklistPara.Items.Remove(item);

    }


    protected void btnAddtime_Click(object sender, EventArgs e)
    {
        if (listpara.SelectedValue != "")
        {
            ListItem item = new ListItem(listpara.SelectedItem.Text + "_" + txtBtime.Text + "~" + txtEtime.Text,listpara.SelectedValue);
            cklistPara.Items.Add(item);
        }
    }
    protected void btnDeltime_Click(object sender, EventArgs e)
    {
       
            List<ListItem> items = new List<ListItem>();
            foreach (ListItem item in cklistPara.Items)
            {
                items.Add(item);
            }

            foreach (ListItem item in items)
            {
                if (item.Selected)
                    cklistPara.Items.Remove(item);
            }
     
    }


 
}