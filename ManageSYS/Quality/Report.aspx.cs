using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Quality_Report : MSYS.Web.BasePage
{
    protected string tvHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listProd, "select prod_code,prod_name,CREATE_TIME from ht_pub_prod_design where is_del = '0' and is_valid = '1' order by CREATE_TIME", "prod_name", "prod_code");
            opt.bindDropDownList(listTeam, "select team_code,team_name from ht_sys_team  where is_del = '0' and is_valid = '1' order by team_code", "team_name", "team_code");
        }
            tvHtml = InitTree("质量");
    }


    public string InitTree(string type)
    {

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
       
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
        string para = opt.GetSegValue("select F_PARA from ht_sys_excel_book where f_id = '" + hidebookid.Value + "'", "F_PARA");
        hideParaset.Value = "1";
        ////////查询条件设置与显示///////////////////////////
        if (para.Length < 6) para = para.PadRight(6, '0');
        if (para.Length >= 6)
        {
            if (para.Substring(0, 1) == "1")
            {
                listProd.Visible = true;
                lab1.Visible = true;
                if (listProd.SelectedValue == "")
                    hideParaset.Value = "0";
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
                if (txtStartTime.Text == "")
                    hideParaset.Value = "0";

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
                if (txtEndTime.Text == "")
                    hideParaset.Value = "0";
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
                if (listTeam.SelectedValue == "")
                    hideParaset.Value = "0";
            }
            else
            {
                listTeam.Visible = false;
                lab4.Visible = false;
                listTeam.SelectedValue = "";
            }
            hideMerge.Value = para.Substring(4, 1);
            if (para.Substring(5, 1) == "1")
            {
               txtMonth.Visible = true;
                lab5.Visible = true;
                if (txtMonth.Text == "")
                    hideParaset.Value = "0";
            }
            else
            {
                txtMonth.Visible = false;
                lab5.Visible = false;
                txtMonth.Text = "";
            }
        }
        if (hideParaset.Value == "1")
        {
            string filename = opt.GetSegValue("select F_NAME from ht_sys_excel_book where f_id = '" + hidebookid.Value + "'", "F_NAME");
            if (filename != "NoRecord")
            {
                DateTime time = DateTime.Now;
                CreateExcel(filename, listProd.SelectedValue, txtStartTime.Text, txtEndTime.Text, listTeam.SelectedValue, ".htm",txtMonth.Text, time, hideMerge.Value != "0");
                string path = "../TEMP/" + filename + time.ToString("HHmmss") + ".htm";
                ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "refresh", " $('#Frame1').attr('src','" + path + "');", true);
            }
        }
        else
            ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "refresh", " $('#Frame1').attr('src','../templates/paranotset.htm');", true);

    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string filename = opt.GetSegValue("select F_NAME from ht_sys_excel_book where f_id = '" + hidebookid.Value + "'", "F_NAME");
        ExportExcel(filename, listProd.SelectedValue, txtStartTime.Text, txtEndTime.Text, listTeam.SelectedValue, ".xls", txtMonth.Text, DateTime.Now, hideMerge.Value != "0");
        //ExportExcel("再造梗丝车间交接班记录", "", "2018-08-21", "", "02", ".xlsx");
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (hidebookid.Value != hideoldid.Value)
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string para = opt.GetSegValue("select F_PARA from ht_sys_excel_book where f_id = '" + hidebookid.Value + "'", "F_PARA");
            hideParaset.Value = "1";
            ////////查询条件设置与显示///////////////////////////
            if (para.Length < 6) para = para.PadRight(6, '0');
            if (para.Length >= 6)
            {
                if (para.Substring(0, 1) == "1")
                {
                    listProd.Visible = true;
                    lab1.Visible = true;
                    if (listProd.SelectedValue == "")
                        hideParaset.Value = "0";
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
                    if (txtStartTime.Text == "")
                        hideParaset.Value = "0";

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
                    if (txtEndTime.Text == "")
                        hideParaset.Value = "0";
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
                    if (listTeam.SelectedValue == "")
                        hideParaset.Value = "0";
                }
                else
                {
                    listTeam.Visible = false;
                    lab4.Visible = false;
                    listTeam.SelectedValue = "";
                }
                hideMerge.Value = para.Substring(4, 1);
                if (para.Substring(5, 1) == "1")
                {
                    txtMonth.Visible = true;
                    lab5.Visible = true;
                    if (txtMonth.Text == "")
                        hideParaset.Value = "0";
                }
                else
                {
                    txtMonth.Visible = false;
                    lab5.Visible = false;
                    txtMonth.Text = "";
                }
            }
            ///////////////////////////////////
            if (hideParaset.Value == "1")
            {
                string filename = opt.GetSegValue("select F_NAME from ht_sys_excel_book where f_id = '" + hidebookid.Value + "'", "F_NAME");
                if (filename != "NoRecord")
                {
                    DateTime time = DateTime.Now;
                    CreateExcel(filename, listProd.SelectedValue, txtStartTime.Text, txtEndTime.Text, listTeam.SelectedValue, ".htm",txtMonth.Text, time, hideMerge.Value != "0");
                    string path = "../TEMP/" + filename + time.ToString("HHmmss") + ".htm";
                    ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "refresh", " $('#Frame1').attr('src','" + path + "');", true);
                }
            }
            else
                ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "refresh", " $('#Frame1').attr('src','../templates/paranotset.htm');", true);

        }
    }
}