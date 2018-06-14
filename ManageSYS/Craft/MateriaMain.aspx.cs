using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Craft_MateriaMain : MSYS.Web.BasePage
{
    protected string tvHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            tvHtml = InitTree();
        }
    }

     
    public string  InitTree()
    {

       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra("select mattree_code,mattree_name  from ht_pub_mattree where IS_DEL = '0'  and length(mattree_code) = 2  order by mattree_code ");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
              string tvHtml = "<ul id='browser' class='filetree treeview-famfamfam'>";
            DataRow[] rows = data.Tables[0].Select();
          foreach (DataRow row in rows)
            {               
               // tvHtml += "<li ><a href='Tech_Session.aspx?session_code=" + row["section_code"].ToString() + "' target='sessionFrame'><span class='folder'  onclick = \"$('#tabtop1').click()\">" + row["section_name"].ToString() + "</span></a>";  
                tvHtml += "<li ><span class='folder'  onclick = \"tab1Click('" + row["mattree_code"].ToString() + "')\">" + row["mattree_name"].ToString() + "</span></a>";
                tvHtml += InitTreeM(row["mattree_code"].ToString());
                tvHtml += "</li>";
            }
          tvHtml += "</ul>";
          return tvHtml;
        }
        else
            return "";
    }

    public string InitTreeM(string mattree_code)
    {
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra("select mattree_code,mattree_name  from ht_pub_mattree where IS_DEL = '0' and  PK_PARENT_CLASS = '" + mattree_code + "' order by mattree_code ");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                tvHtml += "<li ><span class='folder'  onclick = \"tab1Click('" + row["mattree_code"].ToString() + "')\">" + row["mattree_name"].ToString() + "</span></a>";
                tvHtml += InitTreeM(row["mattree_code"].ToString());
               tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }


   
}