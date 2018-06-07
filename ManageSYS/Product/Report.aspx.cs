using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Product_Report : System.Web.UI.Page
{
    protected string tvHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tvHtml = InitTree();
        }      

    }

     
    public string  InitTree()
    {

       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra("select prod_code,prod_name from ht_pub_prod_design where is_del = '0' order by prod_code");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
              string tvHtml = "<ul id='browser' class='filetree treeview-famfamfam'>";
            DataRow[] rows = data.Tables[0].Select();
          foreach (DataRow row in rows)
            {
               // tvHtml += "<li ><a href='RecipeList.aspx?prod_code=" + row["prod_code"].ToString() + "' target='Frame1'><span class='folder'  onclick = \"$('#tabtop1').click()\">" + row["prod_name"].ToString() + "</span></a>";
                tvHtml += "<li ><span class='folder'  onclick = \"addclick()\">" + row["prod_name"].ToString() + "</span></a>";
                tvHtml += InitTreeRecipe(row["prod_code"].ToString());
                tvHtml += "</li>";
            }
          tvHtml += "</ul>";
          return tvHtml;
        }
        else
            return "";
    }


    public string InitTreeRecipe(string prod_code)
    {
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra("select formula_code as 配方编码，formula_name as 配方名称,b_date as 启用时间,creator as 编辑人员,is_valid as 是否有效  from ht_qa_mater_formula where prod_code = '" + prod_code + "' and is_del ='0'  union select formula_code as 配方编码，formula_name as 配方名称,b_date as 启用时间,creator as 编辑人员,is_valid as 是否有效  from ht_qa_aux_formula where prod_code = '" + prod_code + "' and is_del ='0'  union select formula_code as 配方编码，formula_name as 配方名称,b_date as 启用时间,creator as 编辑人员,is_valid as 是否有效  from ht_qa_fla_formula where prod_code ='" + prod_code + "' and is_del ='0'  union select formula_code as 配方编码，formula_name as 配方名称,b_date as 启用时间,creator as 编辑人员,is_valid as 是否有效  from ht_qa_backfill_formula  where prod_code = '" + prod_code + "'  and is_del ='0' ");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
             string tvHtml = "<ul>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                tvHtml += "<li  ><a href='";
                if (row["配方编码"].ToString().Substring(0, 2) == "06")
                    tvHtml += "MtrRecipe.aspx?rcp_code=" + row["配方编码"].ToString() + "' target='Frame2'><span class='file' onclick = \"$('#tabtop2').click()\">" + row["配方名称"].ToString() + "</span></a>";
                else if (row["配方编码"].ToString().Substring(0, 2) == "07")
                    tvHtml += "MtrRecipe.aspx?rcp_code=" + row["配方编码"].ToString() + "' target='Frame3'><span class='file' onclick = \"$('#tabtop3').click()\">" + row["配方名称"].ToString() + "</span></a>";
                else if (row["配方编码"].ToString().Substring(0, 2) == "08")
                    tvHtml += "MtrRecipe.aspx?rcp_code=" + row["配方编码"].ToString() + "' target='Frame4'><span class='file' onclick = \"$('#tabtop4').click()\">" + row["配方名称"].ToString() + "</span></a>";
                else
                    tvHtml += "MtrRecipe.aspx?rcp_code=" + row["配方编码"].ToString() + "' target='Frame5'><span class='file' onclick = \"$('#tabtop5').click()\">" + row["配方名称"].ToString() + "</span></a>";                                
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";            
    }


   
}