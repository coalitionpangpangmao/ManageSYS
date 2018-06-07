using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Energy_Comparison : System.Web.UI.Page
{
    protected string tvHtml;
    protected string JavaHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtEtime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            txtBtime.Text = System.DateTime.Now.AddHours(-2).ToString("yyyy-MM-dd HH:mm:ss");
           DataBaseOperator opt =new DataBaseOperator();
            opt.bindDropDownList(listpara, "select para_code,para_name from ht_pub_tech_para where para_Type like '__0_%'", "para_name", "Para_CODE");
            tvHtml = InitTree();
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
              //  tvHtml += "<li ><input type='checkbox' onselect = \"treeClick(" + row["para_code"].ToString() + ")\"/>" + row["para_name"].ToString();
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
       DataBaseOperator opt =new DataBaseOperator();
        string paraname = opt.GetSegValue("select * from ht_pub_tech_para where para_code = '" + hidecode.Value + "'","para_name");
        ListItem item = new ListItem(paraname+ "_" + txtBtime.Text + "~" + txtEtime.Text , hidecode.Value); 
        cklistPara.Items.Add(item);

    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        listpara.SelectedValue = hidecode.Value;
       DataBaseOperator opt =new DataBaseOperator();
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
        try
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
        catch (Exception ee)
        {
        }
           
        
    }
    


    protected string initData()
    {
        try
        {
            string datahtml = "";
            foreach (ListItem item in cklistPara.Items)
            {
                datahtml += "{ name: '" + item.Text.Substring(0, item.Text.IndexOf('_')) + "',";
                string paracode = item.Value;
                string starttime = item.Text.Substring(item.Text.IndexOf('_') + 1, item.Text.IndexOf('~') - item.Text.IndexOf('_'));
                string stoptime = item.Text.Substring(item.Text.IndexOf('~'));
                IHDataOpt ihopt = new IHDataOpt();
                DataRowCollection rows = ihopt.GetData(starttime, stoptime, paracode);
                datahtml += " data:[";
                foreach (DataRow row in rows)
                {
                    datahtml += row[1].ToString() + ",";
                }
                datahtml = datahtml.Substring(0, datahtml.LastIndexOf(','));
                datahtml += "]},";
            }
            datahtml = datahtml.Substring(0, datahtml.LastIndexOf(','));
           
            return datahtml;
        }
        catch (Exception ee)
        {
            return "";
        }

    }
 
}