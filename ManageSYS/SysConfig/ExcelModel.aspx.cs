using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
public partial class SysConfig_ExcelModel : MSYS.Web.BasePage
{
    protected string tvHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            tvHtml = InitTreeR();
            SetPara("0000");
        }
    }

    public string InitTreeR()
    {

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select distinct F_TYPE from ht_sys_excel_book   order by F_TYPE ");
       
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul id='browser' class='filetree treeview-famfamfam'>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                tvHtml += "<li ><span class='folder'  >" + row["F_TYPE"].ToString() + "</span></a>";
                tvHtml += InitTree(row["F_TYPE"].ToString());
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }
    public string InitTree(string type)
    {

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select F_ID,F_NAME  from HT_SYS_EXCEL_BOOK where F_TYPE = '" + type + "'  order by F_ID ");
        opt.bindDropDownList(listReport, "select F_ID,F_NAME  from HT_SYS_EXCEL_BOOK  order by F_ID", "F_NAME", "F_ID");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul >";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {             
                tvHtml += "<li ><span class='folder'  onclick = \"tab1Click('" + row["F_ID"].ToString() + "')\">" + row["F_NAME"].ToString() + "</span></a>";
                tvHtml += InitTreeM(row["F_ID"].ToString());
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }

    public string InitTreeM(string F_ID)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select distinct F_SHEETINDEX,F_SHEET  from HT_SYS_EXCEL_SEG where is_del = '0' and F_BOOK_ID = '" + F_ID + "' order by F_SHEETINDEX ");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {                
                tvHtml += "<li ><span class='folder'>" + row["F_SHEET"].ToString() + "</span></a>";
                tvHtml += InitTreeS(row["F_SHEETINDEX"].ToString(), F_ID);
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }

    public string InitTreeS(string sheetIndex, string bookID)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select F_ID,F_DES  from HT_SYS_EXCEL_SEG where is_del = '0' and F_BOOK_ID = '" + bookID + "' and F_SHEETINDEX = '" + sheetIndex + "'  order by F_ID ");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                // tvHtml += "<li ><a href='Tech_Process.aspx?process_code=" + row["process_code"].ToString() + "'  target='ProcessFrame'><span class='folder'  onclick = \"$('#tabtop2').click()\">" + row["process_name"].ToString() + "</span></a>";        
                tvHtml += "<li ><span class='file'  onclick = \"tab2Click(" + row["F_ID"].ToString() + ")\">" + row["F_DES"].ToString() + "</span></a>";

                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }



    protected void Del_Click(object sender, EventArgs e)
    {
        try
        {
            Templist.Items.Add(Paralist.Items.FindByValue(parasel.Value));
            Templist.SelectedValue = parasel.Value;
            Paralist.Items.Remove(Paralist.Items.FindByValue(parasel.Value));
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void Add_Click(object sender, EventArgs e)
    {
        try
        {
            Paralist.Items.Add(Templist.Items.FindByValue(tempsel.Value));
            Paralist.SelectedValue = tempsel.Value;
            Templist.Items.Remove(Templist.Items.FindByValue(tempsel.Value));
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void Paralist_SelectedIndexChanged(object sender, EventArgs e)
    {
        parasel.Value = Paralist.SelectedValue;
    }
    protected void Templist_SelectedIndexChanged(object sender, EventArgs e)
    {
        tempsel.Value = Templist.SelectedValue;
    }
    private void SetPara(string para)
    {
        string[] paratable = new string[] {  "$brand$", "$startDate$", "$endDate$", "$team$" };
        ArrayList plist = new ArrayList();
        ArrayList tlist = new ArrayList();
        for (int i = 0; i < 4; i++)
        {
            if (para.Substring(i, 1) == "1")
                plist.Add(paratable[i]);
            else
                tlist.Add(paratable[i]);
        }
        Paralist.DataSource = plist;
        Paralist.DataBind();
        Templist.DataSource = tlist;
        Templist.DataBind();
    }
    private string GetPara()
    {
        string para = "0000";
        string[] paratable = new string[] {  "$brand$", "$startDate$", "$endDate$", "$team$" };
        ListItemCollection list = new ListItemCollection();
        list = Paralist.Items;
        for (int i = 0; i < 4; i++)
        {
            if (list.FindByValue(paratable[i]) != null)
            {
                if (i == 0)
                    para = "10000";
                else if (i == 3)
                    para = para.Substring(0, 3) + "1";
                else
                    para = para.Substring(0, i) + "1" + para.Substring(i + 1);
            }
        }
        return para;
    }

    //保存报表
    protected void SaveBook_Click(object sender, EventArgs e)
    {
        try
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataSet data = opt.CreateDataSetOra("select * from HT_SYS_EXCEL_BOOK where F_NAME = '" + ReportName.Text + "'");
            if (data != null && data.Tables[0].Select().GetLength(0) > 0)
            {
                string[] seg = { "F_PARA", "F_TYPE" };
                string[] value = { GetPara(),listType.SelectedValue };
                string log_message = opt.UpDateData(seg, value, "HT_SYS_EXCEL_BOOK", " where F_NAME = '" + ReportName.Text + "'") == "Success" ? "更新报表成功" : "更新报表失败";
                log_message += "标识:" + ReportName.Text;
                InsertTlog(log_message);               

            }
            else
            {
                string[] seg = { "F_PARA", "F_NAME", "F_SYNCHRO_TIME", "F_TYPE" };
                string[] value = { GetPara(), ReportName.Text, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),listType.SelectedValue };
                string log_message = opt.InsertData(seg, value, "HT_SYS_EXCEL_BOOK") == "Success" ? "插入报表成功" : "插入报表失败";
                log_message += "标识:" + ReportName.Text;
                InsertTlog(log_message);
            }
            tvHtml = InitTreeR();
            ScriptManager.RegisterStartupScript(UpdatePanel3, this.Page.GetType(), "", "initTreetoggle();", true);
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    //删除报表
    protected void Delete_Click(object sender, EventArgs e)//应该进行事务封装
    {

        string query;
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        query = "delete from HT_SYS_EXCEL_BOOK where F_NAME = '" + ReportName.Text.Trim() + "'";
        string log_message = opt.UpDateOra(query) == "Success" ? "删除报表成功" : "删除报表失败";
        log_message += "标识:" + ReportName.Text;
        InsertTlog(log_message);
        tvHtml = InitTreeR();
        ScriptManager.RegisterStartupScript(UpdatePanel3, this.Page.GetType(), "", "initTreetoggle();", true);


    }
    //保存报表字段
    protected void Save_Click(object sender, EventArgs e)
    {

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();


        string[] seg = { "F_BOOK_ID", "F_SHEET", "F_DES", "F_SQL", "F_DESX", "F_DESY", "F_SHEETINDEX", "F_SYNCHRO_TIME" };
        string[] value = { listReport.SelectedValue, Sheet1.Text , DesC.Text.Trim()+ DesR.Text.Trim() ,SQLText.Text.Replace("'","''") , DesR.Text , DesC.Text ,Index.Text ,DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
        string log_message = opt.MergeInto(seg, value, 3, "HT_SYS_EXCEL_SEG") == "Success" ? "插入报表记录成功" : "插入报表记录失败";
        log_message += "详情" + ReportName.Text + "工作表名为" + Sheet1.Text + "位置为" + DesC.Text.Trim() + DesR.Text.Trim();
        InsertTlog(log_message);
         
        tvHtml = InitTreeR();
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", "initTreetoggle();", true);

    }
    //点击树更新显示数据
    protected void btnUpdTab1_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet temp = opt.CreateDataSetOra("select *  from HT_SYS_EXCEL_BOOK where F_ID = '" + hideTreeSel.Value + "'");
        if (temp != null && temp.Tables[0].Rows.Count > 0)
        {
            ReportName.Text = temp.Tables[0].Rows[0]["F_NAME"].ToString();
            SetPara(temp.Tables[0].Rows[0]["F_PARA"].ToString());
            listType.SelectedValue = temp.Tables[0].Rows[0]["F_TYPE"].ToString();
        }

    }

    protected void btnUpdTab2_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select * from ht_sys_excel_seg t left join ht_sys_excel_book r on r.f_id = t.f_book_id where t.f_id = '" + hideTreeSel2.Value + "'");
        if (data.Tables[0].Select().GetLength(0) > 0)
        {
            DataRow row = data.Tables[0].Select()[0];
            Sheet1.Text = row["F_SHEET"].ToString();
            DesR.Text = row["F_DESX"].ToString();
            DesC.Text = row["F_DESY"].ToString();
            Index.Text = row["F_SHEETINDEX"].ToString();
            SQLText.Text = row["F_SQL"].ToString();
            listReport.SelectedValue= row["F_BOOK_ID"].ToString();
            ReportName.Text = listReport.SelectedItem.Text;
            listType.SelectedValue = row["F_TYPE"].ToString();
            SetPara(opt.GetSegValue("select * from HT_SYS_EXCEL_BOOK where F_ID = '" + row["F_BOOK_ID"].ToString() + "'", "F_PARA"));
        }

    }
    protected void btnSegDel_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "delete from HT_SYS_EXCEL_SEG where  F_SHEETINDEX='"
                      + Index.Text + "' and F_BOOK_ID = '" +listReport.SelectedValue + "' and f_des = '" + DesC.Text.Trim() + DesR.Text.Trim() + "'";
        string log_message = opt.UpDateOra(query) == "Success" ? "删除报表记录成功" : "删除报表记录失败";
        log_message += "标识:F_SHEETINDEX='" + Index.Text + "' and F_BOOK_ID = '" + listReport.SelectedValue + "' and f_des = '" + DesC.Text.Trim() + DesR.Text.Trim() + "'";
        InsertTlog(log_message);
        tvHtml = InitTreeR();
        ScriptManager.RegisterStartupScript(UpdatePanel3, this.Page.GetType(), "", "initTreetoggle();", true);
    }
    private int getColumn(string Col)
    {
        int ColNum;
        switch (Col)
        {
            case "A": ColNum = 0; break;
            case "B": ColNum = 1; break;
            case "C": ColNum = 2; break;
            case "D": ColNum = 3; break;
            case "E": ColNum = 4; break;
            case "F": ColNum = 5; break;
            case "G": ColNum = 6; break;
            case "H": ColNum = 7; break;
            case "I": ColNum = 8; break;
            case "J": ColNum = 9; break;
            case "K": ColNum = 10; break;
            case "L": ColNum = 11; break;
            case "M": ColNum = 12; break;
            case "N": ColNum = 13; break;
            case "O": ColNum = 14; break;
            case "P": ColNum = 15; break;
            case "Q": ColNum = 16; break;
            case "R": ColNum = 17; break;
            case "S": ColNum = 18; break;
            case "T": ColNum = 19; break;
            case "U": ColNum = 20; break;
            case "V": ColNum = 21; break;
            case "W": ColNum = 22; break;
            case "X": ColNum = 23; break;
            case "Y": ColNum = 24; break;
            case "Z": ColNum = 25; break;
            default: ColNum = 26; break;
        }
        return ColNum;
    }
    private void SetBlank()
    {
        ReportName.Text = "";
        DesR.Text = "";
        SQLText.Text = "";
        DesC.Text = "";
        Rows.Text = "";
        Columns.Text = "";
        Sheet1.Text = "";
        Index.Text = "";
        SetPara("00000");
    }
    private void Setable(bool IsEnable)
    {
        ReportName.Enabled = IsEnable;
        DesR.Enabled = IsEnable;
        SQLText.Enabled = IsEnable;
        DesC.Enabled = IsEnable;
        Rows.Enabled = IsEnable;
        Columns.Enabled = IsEnable;
        Sheet1.Enabled = IsEnable;
        Index.Enabled = IsEnable;
    }
    protected void Edit_Click(object sender, EventArgs e)
    {
        Setable(true);
    }




}