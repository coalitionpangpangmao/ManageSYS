using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Craft_Model : MSYS.Web.BasePage
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

       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select g.section_code,g.section_name from ht_pub_tech_section g where g.IS_VALID = '1' and g.IS_DEL = '0' order by g.section_code ");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
              string tvHtml = "<ul id='browser' class='filetree treeview-famfamfam'>";
            DataRow[] rows = data.Tables[0].Select();
          foreach (DataRow row in rows)
            {               
               // tvHtml += "<li ><a href='Tech_Session.aspx?session_code=" + row["section_code"].ToString() + "' target='sessionFrame'><span class='folder'  onclick = \"$('#tabtop1').click()\">" + row["section_name"].ToString() + "</span></a>";  
                tvHtml += "<li ><span class='folder'  onclick = \"tab1Click(" + row["section_code"].ToString() + ")\">" + row["section_name"].ToString() + "</span>";
                tvHtml += InitTreeSectionPara(row["section_code"].ToString());
                tvHtml += InitTreeEquip(row["section_code"].ToString());
                tvHtml += "</li>";
            }
          tvHtml += "</ul>";
          return tvHtml;
        }
        else
            return "";
    }

    

    public string InitTreeEquip(string section_code)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select IDKEY,EQ_NAME from ht_eq_eqp_tbl where section_code  = '" + section_code + "' and IS_VALID = '1' and IS_DEL = '0' order by IDKEY");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                // tvHtml += "<li ><a href='Tech_Process.aspx?process_code=" + row["process_code"].ToString() + "'  target='ProcessFrame'><span class='folder'  onclick = \"$('#tabtop2').click()\">" + row["process_name"].ToString() + "</span></a>";        
                tvHtml += "<li ><span class='folder'  onclick = \"tab2Click(" + row["IDKEY"].ToString() + ")\">" + row["EQ_NAME"].ToString() + "</span>";

                tvHtml += InitTreePara(row["IDKEY"].ToString());
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }
    public string InitTreePara(string IDkey)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select Para_code,para_name from ht_pub_tech_para where equip_code = '" + IDkey + "' and IS_VALID = '1' and IS_DEL = '0'  order by para_code");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {                
                tvHtml += "<li ><span class='file'  onclick = \"tab3Click(" + row["para_code"].ToString() + ")\">" + row["para_name"].ToString() + "</span>";
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }
    public string InitTreeSectionPara(string section_code)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select Para_code,para_name from ht_pub_tech_para where substr(para_code,1,5) = '" + section_code + "' and Equip_code is null and IS_VALID = '1' and IS_DEL = '0'  order by para_code");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                tvHtml += "<li ><span class='file'  onclick = \"tab3Click(" + row["para_code"].ToString() + ")\">" + row["para_name"].ToString() + "</span>";
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        tvHtml = InitTree();

        ScriptManager.RegisterStartupScript(UpdatePanel1,this.Page.GetType(),"updatetree"," $('#browser').treeview({ toggle: function () { console.log('%s was toggled.', $(this).find('>span').text());},  persist: 'cookie', collapsed: true });",true);
    }
    //public string InitTreePara( string process_code)
    //{
    //   MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
    //    DataSet data = opt.CreateDataSetOra("select para_code,para_name from ht_pub_tech_para where substr(para_code,1,7) =  '" + process_code + "' and IS_VALID = '1' and IS_DEL = '0'  order by para_code");
    //    if (data != null && data.Tables[0].Rows.Count > 0)
    //    {
    //        string tvHtml = "<ul>";
    //        DataRow[] rows = data.Tables[0].Select();
    //        foreach (DataRow row in rows)
    //        {
    //           // tvHtml += "<li ><a href='Tech_Para.aspx?para_code=" + row["para_code"].ToString() + "' target='ProcessFrame'><span class='file'  onclick = \"$('#tabtop4').click()\">" + row["para_name"].ToString() + "</span></a>";
    //            tvHtml += "<li ><span class='file'  onclick = \"tab3Click(" + row["para_code"].ToString() + ")\">" + row["para_name"].ToString() + "</span>";
    //            tvHtml += "</li>";
    //        }
    //        tvHtml += "</ul>";
    //        return tvHtml;
    //    }
    //    else
    //        return "";          
    //}


    //public string InitTreeProcess(string section_code)
    //{
    //    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
    //    DataSet data = opt.CreateDataSetOra("select h.process_code,h.process_name from  ht_pub_inspect_process h where substr(h.process_code,1,5) = '" + section_code + "' and h.IS_VALID = '1' and h.IS_DEL = '0' order by h.process_code");
    //    if (data != null && data.Tables[0].Rows.Count > 0)
    //    {
    //        string tvHtml = "<ul>";
    //        DataRow[] rows = data.Tables[0].Select();
    //        foreach (DataRow row in rows)
    //        {
    //            // tvHtml += "<li ><a href='Tech_Process.aspx?process_code=" + row["process_code"].ToString() + "'  target='ProcessFrame'><span class='folder'  onclick = \"$('#tabtop2').click()\">" + row["process_name"].ToString() + "</span></a>";        
    //            tvHtml += "<li ><span class='folder'  onclick = \"tab2Click(" + row["process_code"].ToString() + ")\">" + row["process_name"].ToString() + "</span>";

    //            tvHtml += InitTreePara(row["process_code"].ToString());
    //            tvHtml += "</li>";
    //        }
    //        tvHtml += "</ul>";
    //        return tvHtml;
    //    }
    //    else
    //        return "";
    //}
   
}