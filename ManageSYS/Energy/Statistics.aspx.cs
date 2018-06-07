using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Energy_Statistics : System.Web.UI.Page
{
    protected string tvHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
             //   tvHtml += InitTreePara(row["section_code"].ToString());
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }


   
}