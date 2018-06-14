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
            tvHtml = InitTree();
            SetPara("00000");
        }
    }

     
    public string  InitTree()
    {

       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra("select F_ID,F_NAME  from HT_SYS_EXCEL_BOOK  order by F_ID ");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
              string tvHtml = "<ul id='browser' class='filetree treeview-famfamfam'>";
            DataRow[] rows = data.Tables[0].Select();
          foreach (DataRow row in rows)
            {               
               // tvHtml += "<li ><a href='Tech_Session.aspx?session_code=" + row["section_code"].ToString() + "' target='sessionFrame'><span class='folder'  onclick = \"$('#tabtop1').click()\">" + row["section_name"].ToString() + "</span></a>";  
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
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra("select distinct F_SHEETINDEX,F_SHEET  from HT_SYS_EXCEL_SEG where is_del = '0' and F_BOOK_ID = '" + F_ID + "' order by F_SHEETINDEX ");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
               // tvHtml += "<li ><a href='Tech_Process.aspx?process_code=" + row["process_code"].ToString() + "'  target='ProcessFrame'><span class='folder'  onclick = \"$('#tabtop2').click()\">" + row["process_name"].ToString() + "</span></a>";        
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

    public string InitTreeS(string sheetIndex,string bookID)
    {
       DataBaseOperator opt =new DataBaseOperator();
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
        string[] paratable = new string[] { "$batch$", "$brand$", "$startDate$", "$endDate$", "$shift$" };
        ArrayList plist = new ArrayList();
        ArrayList tlist = new ArrayList();
        for (int i = 0; i < 5; i++)
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
        string para = "00000";
        string[] paratable = new string[] { "$batch$", "$brand$", "$startDate$", "$endDate$", "$shift$" };
        ListItemCollection list = new ListItemCollection();
        list = Paralist.Items;
        for (int i = 0; i < 5; i++)
        {
            if (list.FindByValue(paratable[i]) != null)
            {
                if (i == 0)
                    para = "10000";
                else if (i == 4)
                    para = para.Substring(0, 4) + "1";
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
           DataBaseOperator opt =new DataBaseOperator();
            DataSet data = opt.CreateDataSetOra("select * from HT_SYS_EXCEL_BOOK where F_NAME = '" + ReportName.Text + "'");
            if (data != null && data.Tables[0].Select().GetLength(0) > 0)
            {
                string[] seg = { "F_PARA" };
                string[] value = { GetPara() };
                if(opt.UpDateData(seg, value, "HT_SYS_EXCEL_BOOK", " where F_NAME = '" + ReportName.Text + "'")=="Success")
                    opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "更新成功，更新报名为" + ReportName.Text + "的参数信息");
                else
                    opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "更新失败，更新报名为" + ReportName.Text + "的参数信息");
                
            }
            else
            {
                string[] seg = { "F_PARA", "F_NAME", "F_SYNCHRO_TIME" };
                string[] value = { GetPara(), ReportName.Text, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
                if(opt.InsertData(seg, value, "HT_SYS_EXCEL_BOOK")=="Success")
                    opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "插入成功，插入报名为" + ReportName.Text + "的记录");
                else
                    opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "插入失败，插入报名为" + ReportName.Text + "的记录");
            }
            tvHtml = InitTree();
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
        try
        {
            string query;
           DataBaseOperator opt =new DataBaseOperator();
           
                query = "delete from G_EXCEL_WORKBOOK where F_NAME = '" + ReportName.Text.Trim() + "'";
                opt.UpDateOra(query);
                query = "delete from G_EXCEL_WORKSEG where F_BOOK_ID = '" + opt.GetSegValue("select * from HT_SYS_EXCEL_BOOK where F_NAME = '" + ReportName.Text.Trim() + "'","F_ID");
                opt.UpDateOra(query);
                opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "删除报名为" + ReportName.Text + "的所有配置信息");
                tvHtml = InitTree();
                ScriptManager.RegisterStartupScript(UpdatePanel3, this.Page.GetType(), "", "initTreetoggle();", true);
        }
        catch (Exception ee)
        {
           
        }

    }
    //保存报表字段
    protected void Save_Click(object sender, EventArgs e)
    {
          try
          {
             DataBaseOperator opt =new DataBaseOperator();
             DataSet temp = opt.CreateDataSetOra("select * from HT_SYS_EXCEL_SEG where F_SHEETINDEX = '" + Index.Text + "' and F_BOOK_ID = '" + opt.GetSegValue("select * from HT_SYS_EXCEL_BOOK where F_NAME = '" + txtReport.Text.Trim() + "'", "F_ID") + "' and f_des = '" + DesC.Text.Trim() + DesR.Text.Trim() + "'");
              
              if (temp != null && temp.Tables[0].Select().GetLength(0) > 0)
              {
                  string[] seg = { "F_SQL", "F_SHEET", "F_SYNCHRO_TIME" };
                  string[] value = { SQLText.Text, Sheet1.Text, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
                  string condition = " where F_SHEET='"
                    + Sheet1.Text + "' and F_BOOK_ID = '" + opt.GetSegValue("select * from HT_SYS_EXCEL_BOOK where F_NAME = '" + txtReport.Text.Trim() + "'", "F_ID") + "' and f_des = '" + DesC.Text.Trim() + DesR.Text.Trim() + "'";
                  if(opt.UpDateData(seg, value,"HT_SYS_EXCEL_SEG", condition)=="Success")
                    opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "更新成功，更新报名为" + ReportName.Text + "工作表名为" + Sheet1.Text + "位置为" + DesC.Text.Trim() + DesR.Text.Trim() + "的记录");
                  else
                      opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "更新失败，更新报名为" + ReportName.Text + "工作表名为" + Sheet1.Text + "位置为" + DesC.Text.Trim() + DesR.Text.Trim() + "的记录");
              }
              else
              {
                  
                    string[] seg = {"F_BOOK_ID","F_SHEET","F_SQL","F_DES","F_DESX","F_DESY","F_SHEETINDEX","F_SYNCHRO_TIME"};
                    string[] value = { opt.GetSegValue("select * from HT_SYS_EXCEL_BOOK where F_NAME = '" + txtReport.Text.Trim() + "'", "F_ID"), Sheet1.Text , SQLText.Text ,DesC.Text.Trim()+ DesR.Text.Trim() ,
 DesR.Text , DesC.Text ,Index.Text ,DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
                    if(opt.InsertData(seg, value, "HT_SYS_EXCEL_SEG")=="Success")
                       opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "插入成功，插入报名为" + ReportName.Text + "工作表名为" + Sheet1.Text + "位置为" + DesC.Text.Trim() + DesR.Text.Trim() + "的记录");
                    else
                       opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "插入失败，插入报名为" + ReportName.Text + "工作表名为" + Sheet1.Text + "位置为" + DesC.Text.Trim() + DesR.Text.Trim() + "的记录");
              }
              tvHtml = InitTree();
              ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", "initTreetoggle();", true);
          }
          catch (Exception ee)
          {
              Response.Write(ee.Message);
          }

    }
    //点击树更新显示数据
    protected void btnUpdTab1_Click(object sender, EventArgs e)
    {
       DataBaseOperator opt =new DataBaseOperator();
        DataSet temp = opt.CreateDataSetOra("select *  from HT_SYS_EXCEL_BOOK where F_ID = '" + hideTreeSel.Value + "'");
        if(temp != null && temp.Tables[0].Rows.Count > 0)
        {
            ReportName.Text = temp.Tables[0].Rows[0]["F_NAME"].ToString();
            SetPara(temp.Tables[0].Rows[0]["F_PARA"].ToString());
        }
      
    }
    
    protected void btnUpdTab2_Click(object sender, EventArgs e)
    {
       DataBaseOperator opt =new DataBaseOperator();
         DataSet data = opt.CreateDataSetOra("select * from HT_SYS_EXCEL_SEG where F_ID = '" + hideTreeSel2.Value + "'");
         if (data.Tables[0].Select().GetLength(0) > 0)
         {
             DataRow row = data.Tables[0].Select()[0];
             Sheet1.Text = row["F_SHEET"].ToString();
             DesR.Text = row["F_DESX"].ToString();
             DesC.Text = row["F_DESY"].ToString();
             Index.Text = row["F_SHEETINDEX"].ToString();
             SQLText.Text = row["F_SQL"].ToString();            
             txtReport.Text = opt.GetSegValue("select * from HT_SYS_EXCEL_BOOK where F_ID = '" + row["F_BOOK_ID"].ToString() + "'", "F_NAME");
             ReportName.Text = txtReport.Text;
             SetPara(opt.GetSegValue("select * from HT_SYS_EXCEL_BOOK where F_ID = '" + row["F_BOOK_ID"].ToString() + "'", "F_PARA"));
         }
       
    }
    protected void btnSegDel_Click(object sender, EventArgs e)
    {
       DataBaseOperator opt =new DataBaseOperator();
       string query = "delete from HT_SYS_EXCEL_SEG where  F_SHEETINDEX='"
                     + Index.Text + "' and F_BOOK_ID = '" + opt.GetSegValue("select * from HT_SYS_EXCEL_BOOK where F_NAME = '" + txtReport.Text.Trim() + "'", "F_ID") + "' and f_des = '" + DesC.Text.Trim() + DesR.Text.Trim() + "'";
        if (opt.UpDateOra(query) == "Success")
            opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "数据填充保存成功，数据参数：" + Sheet1.Text + opt.GetSegValue("select * from HT_SYS_EXCEL_BOOK where F_NAME = '" + txtReport.Text.Trim() + "'", "F_ID"));
        else
            opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "数据填充保存失败，数据参数：" + Sheet1.Text + opt.GetSegValue("select * from HT_SYS_EXCEL_BOOK where F_NAME = '" + txtReport.Text.Trim() + "'", "F_ID"));
        tvHtml = InitTree();
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

   

  

  
 /*   protected void ExportExcel2003(string filename, DataTable dt)
    {
        ExcelSaveAs openXMLExcel = null;
        FileStream file = null;
        try
        {
            if (!System.IO.Directory.Exists(@"C:\TEMP"))
            {
                // 目录不存在，建立目录 
                System.IO.Directory.CreateDirectory(@"C:\TEMP");
            }
            String sourcePath = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"HT18\" + filename + ".xls";
            String filepath = @"C:\TEMP\" + filename + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";



            openXMLExcel = new ExcelSaveAs(sourcePath, true);
            openXMLExcel.SetCurrentSheet(0);
            openXMLExcel.WriteDataIntoWorksheet(2, 1, dt);
            FileInfo fi = new FileInfo(filepath);
            if (fi.Exists)     //判断文件是否已经存在,如果存在就删除!
            {
                fi.Delete();
            }
            openXMLExcel.SaveAs(filepath);
            openXMLExcel.Dispose();
            openXMLExcel = null;

            file = new FileStream(filepath, FileMode.Open);
            Response.Clear();
            long fileSize = file.Length;
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename + ".xls", System.Text.Encoding.UTF8));
            Response.AddHeader("Content-Length", fileSize.ToString());
            byte[] bytes = new byte[fileSize];
            file.Read(bytes, 0, (int)fileSize);
            file.Close();
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            if (openXMLExcel != null)
            {
                openXMLExcel.Dispose();
            }
            if (file != null)
            {
                file.Close();
            }
        }
        finally
        {
        }
    }*/


}