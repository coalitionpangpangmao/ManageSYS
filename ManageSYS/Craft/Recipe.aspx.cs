using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Craft_Recipe : MSYS.Web.BasePage
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
        DataSet data = opt.CreateDataSetOra("select prod_code,prod_name from ht_pub_prod_design where is_del = '0' order by prod_code");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
              string tvHtml = "<ul id='browser' class='filetree treeview-famfamfam'>";
            DataRow[] rows = data.Tables[0].Select();
          foreach (DataRow row in rows)
            {
                tvHtml += "<li ><span class='folder'  onclick = \"tab1Click(" +row["prod_code"].ToString() + ")\">" + row["prod_name"].ToString() + "</span>";

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
        DataSet data = opt.CreateDataSetOra("select formula_code as 配方编码，formula_name as 配方名称,b_date as 启用时间,CREATE_ID as 编辑人员,is_valid as 是否有效  from ht_qa_mater_formula where prod_code ='" + prod_code + "' and is_del ='0' union select formula_code as 配方编码，formula_name as 配方名称,b_date as 启用时间,CREATE_ID as 编辑人员,is_valid as 是否有效  from ht_qa_aux_formula where prod_code = '" + prod_code + "' and is_del ='0'  union select formula_code as 配方编码，formula_name as 配方名称,b_date as 启用时间,CREATE_ID as 编辑人员,is_valid as 是否有效  from ht_qa_coat_formula  where prod_code = '" + prod_code + "'  and is_del ='0'");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
             string tvHtml = "<ul>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {                
                if (row["配方编码"].ToString().Substring(0, 5) == "70306")
                    tvHtml += "<li ><span class='file'  onclick = \"tab2Click(" + row["配方编码"].ToString() + ")\">" + row["配方名称"].ToString() + "</span></li>";
                else if (row["配方编码"].ToString().Substring(0, 5) == "70307")
                    tvHtml += "<li ><span class='file'  onclick = \"tab3Click(" + row["配方编码"].ToString() + ")\">" + row["配方名称"].ToString() + "</span></li>";
                else
                    tvHtml += "<li ><span class='file'  onclick = \"tab4Click(" + row["配方编码"].ToString() + ")\">" + row["配方名称"].ToString() + "</span></li>";      
               
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";            
    }


   
}