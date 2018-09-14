using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Product_Report : MSYS.Web.BasePage
{
    protected string tvHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            tvHtml = InitTree("生产");
            
        }

    }


    public string InitTree(string type)
    {

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listProd, "select prod_code,prod_name,CREATE_TIME from ht_pub_prod_design where is_del = '0' and is_valid = '1' order by CREATE_TIME", "prod_name", "prod_code");
        opt.bindDropDownList(listTeam, "select team_code,team_name from ht_sys_team  where is_del = '0' and is_valid = '1' order by team_code", "team_name", "team_code");
        DataSet data = opt.CreateDataSetOra("select F_ID,F_NAME  from HT_SYS_EXCEL_BOOK where F_TYPE = '" + type + "'  order by F_ID ");

        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul id='browser' class='filetree treeview-famfamfam'>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                tvHtml += "<li ><span class='file'  onclick = \"tab1Click('" + row["F_ID"].ToString() + "')\">" + row["F_NAME"].ToString() + "</span></a>";
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string filename = opt.GetSegValue("select F_NAME from ht_sys_excel_book where f_id = '" + hidebookid.Value + "'", "F_NAME");       
        if (filename != "NoRecord")
        {
            DateTime time = DateTime.Now;            
                CreateExcel(filename, listProd.SelectedValue, txtStartTime.Text, txtEndTime.Text, listTeam.SelectedValue, ".htm", time, hideMerge.Value=="0");           
            string path = "../TEMP/" + filename + time.ToString("HHmmss") + ".htm";           
            ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "refresh", " $('#Frame1').attr('src','" + path + "');", true);

        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
         MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string filename = opt.GetSegValue("select F_NAME from ht_sys_excel_book where f_id = '" + hidebookid.Value + "'", "F_NAME");
        ExportExcel(filename, listProd.SelectedValue, txtStartTime.Text, txtEndTime.Text, listTeam.SelectedValue, ".xlsx", DateTime.Now, hideMerge.Value == "0");
        //ExportExcel("再造梗丝车间交接班记录", "", "2018-08-21", "", "02", ".xlsx");
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string para = opt.GetSegValue("select F_PARA from ht_sys_excel_book where f_id = '" + hidebookid.Value + "'", "F_PARA");
        if (para.Length < 5) para = para.PadRight(5, '0');
        if (para.Length >= 4)
        {
            if (para.Substring(0, 1) == "1")
            {
                listProd.Visible = true;
                lab1.Visible = true;
            }
            else
            {
                listProd.Visible = false;
                lab1.Visible = false;
                listProd.SelectedValue = "";
            }

            if (para.Substring(1, 1) == "1")
            {
                txtStartTime.Visible = true;
                lab2.Visible = true;
            }
            else
            {
                txtStartTime.Visible = false;
                lab2.Visible = false;
                txtStartTime.Text = "";
            }

            if (para.Substring(2, 1) == "1")
            {
                txtEndTime.Visible = true;
                lab3.Visible = true;
            }
            else
            {
                txtEndTime.Visible = false;
                lab3.Visible = false;
                txtEndTime.Text = "";
            }

            if (para.Substring(3, 1) == "1")
            {
                listTeam.Visible = true;
                lab4.Visible = true;
            }
            else
            {
                listTeam.Visible = false;
                lab4.Visible = false;
                listTeam.SelectedValue = "";
            }
            hideMerge.Value = para.Substring(4, 1);
        }
    }
}