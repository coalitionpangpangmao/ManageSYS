using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.IO;
public partial class Quality_Comparison : MSYS.Web.BasePage
{
    protected string tvHtml;
    protected string JavaHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            txtEtime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            txtBtime.Text = System.DateTime.Now.AddHours(-2).ToString("yyyy-MM-dd HH:mm:ss");
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listpara, "select para_code,para_name from ht_pub_tech_para where  IS_VALID = '1' and IS_DEL = '0' and  para_type like '___1%'   order by para_code", "para_name", "Para_CODE");
            tvHtml = InitTree();
        }
    }

    public string InitTree()
    {

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select distinct r.section_code ,r.section_name from ht_pub_tech_section r left join ht_pub_tech_para s on substr(s.para_code,1,5) = r.section_code and s.is_del = '0' and s.is_valid = '1' where r.is_del = '0' and r.is_valid = '1' and  s.para_type like '___1%'   order by r.section_code");
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
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select para_code,para_name from ht_pub_tech_para where substr(para_code,1,5) =  '" + section_code + "' and IS_VALID = '1' and IS_DEL = '0' and  para_type like '___1%'   order by para_code");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                tvHtml += "<li ><input type='checkbox' value = '" + row["para_code"].ToString() + "' onclick = 'treeClick(this)'/>" + row["para_name"].ToString();
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
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string paraname = opt.GetSegValue("select * from ht_pub_tech_para where para_code = '" + hidecode.Value + "'", "para_name");
        ListItem item = new ListItem(paraname + "_" + txtBtime.Text + "~" + txtEtime.Text, hidecode.Value);
        cklistPara.Items.Add(item);

    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        listpara.SelectedValue = hidecode.Value;
        ListItem item = cklistPara.Items.FindByValue(hidecode.Value);
        while (item != null)
        {
            cklistPara.Items.Remove(item);
            item = cklistPara.Items.FindByValue(hidecode.Value);
        }


    }


    protected void btnAddtime_Click(object sender, EventArgs e)
    {
        if (listpara.SelectedValue != "")
        {
            ListItem item = new ListItem(listpara.SelectedItem.Text + "_" + txtBtime.Text + "~" + txtEtime.Text, listpara.SelectedValue);
            cklistPara.Items.Add(item);
        }
    }
    protected void btnDeltime_Click(object sender, EventArgs e)
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
        if (cklistPara.Items.FindByValue(hidecode.Value) == null)
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "del", "$(\"input[value$='" + hidecode.Value + "']\").attr('checked', false);", true);
    }



    protected void btnExport_Click(object sender, EventArgs e)
    {

        MSYS.Common.ExcelExport openXMLExcel = null;

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        string basedir = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
        string strFolderPath = basedir + @"\TEMP";
        if (!System.IO.Directory.Exists(strFolderPath))
        {
            // 目录不存在，建立目录 
            System.IO.Directory.CreateDirectory(strFolderPath);
        }

        DirectoryInfo dyInfo = new DirectoryInfo(strFolderPath);
        //获取文件夹下所有的文件
        foreach (FileInfo feInfo in dyInfo.GetFiles())
        {
            //判断文件日期是否小于今天，是则删除
            if (feInfo.CreationTime < DateTime.Now.AddMinutes(-2))
                feInfo.Delete();
        }
        foreach (DirectoryInfo dir in dyInfo.GetDirectories())
        {
            if (dir.CreationTime < DateTime.Now.AddMinutes(-2))
                dir.Delete(true);
        }
        //导出文件模板所在位置
        string filename = "原始数据" + System.DateTime.Now.ToString("HHmmss") + ".xls";
        String sourcePath = basedir + @"templates\原始数据.xls" ;
        String filepath = basedir + @"TEMP\" + filename;

        try
        {
            //申明一个ExcelSaveAs对象，该对象完成将数据写入Excel的操作           
            openXMLExcel = new MSYS.Common.ExcelExport(sourcePath, false);
            MSYS.IHAction ihopt = new MSYS.IHAction();
            List<MSYS.IHAction.ParaRes> paralist = getData(listpara.SelectedValue, txtBtime.Text, txtEtime.Text);
            DataTable dt = new DataTable();
           
            dt.Columns.Add("时间");
            dt.Columns.Add("值");
            dt.Columns.Add("状态");


            foreach (MSYS.IHAction.ParaRes info in paralist)
            {
                double resvalue = -9999 ;
                if(txtValue.Text != "")
                 resvalue = Convert.ToDouble(txtValue.Text);
                if ((resvalue != -9999&&info.value > resvalue)||resvalue == -9999 ) 
                {
                    
                        var values = new object[3];
                        values[0] = info.timestamp;
                        values[1] = info.value;
                        values[2] = info.status;                    
                        dt.Rows.Add(values);
                   
                }                
            }
            openXMLExcel.SetCurrentSheet(0);
          
            openXMLExcel.WriteData(2, 2,  listpara.SelectedItem.Text);
            openXMLExcel.WriteData(3, 2,  txtBtime.Text + "~" + txtEtime.Text);
            openXMLExcel.WriteDataIntoWorksheet(5,1, dt);


            ///客户端再下载该文件，在客户端进行浏览
            FileInfo fi = new FileInfo(filepath);
            if (fi.Exists)     //判断文件是否已经存在,如果存在就删除!
            {
                fi.Delete();
            }
            openXMLExcel.SaveAs(filepath);
            
            openXMLExcel.Dispose();
            openXMLExcel = null;
            KillProcess("EXCEL.EXE");

            Response.Clear();
            Response.Buffer = true;

            Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8));
            Response.ContentType = "application/vnd.ms-excel";
            Response.TransmitFile(filepath);

          //  Response.End();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch
        {
            if (openXMLExcel != null)
            {
                openXMLExcel.Dispose();
            }

        }
      
    }

    protected List<MSYS.IHAction.ParaRes> getData( string point, string starttime, string endtime)
    {
         MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        //query prod_code
        string prodcode = opt.GetSegValue("select prod_code,starttime from ht_prod_report t where t.starttime <= '" + starttime + "' and t.endtime >='" + endtime + "' union select prod_code,starttime from ht_prod_report t where t.endtime > '" + starttime + "' and t.endtime <'" + endtime + "' union select prod_code,starttime from  ht_prod_report t where t.starttime >'" + starttime + "' and t.starttime <'" + endtime + "' order by starttime", "Prod_code");
        List<MSYS.IHAction.ParaRes> Rows = new List<MSYS.IHAction.ParaRes>();
        if (prodcode != "NoRecord")
        {
            DataSet pointinfo;
            pointinfo = opt.CreateDataSetOra("select t.para_name,t.value_tag from    ht_pub_tech_para t where t.para_code = '" + point + "'");
            if (pointinfo != null && pointinfo.Tables[0].Rows.Count > 0)
            {
                DataRow row = pointinfo.Tables[0].Rows[0];
                string tag = row["value_tag"].ToString();

                if (tag != "")
                {
                    //get rawdata from IHistorian 
                    //   MSYS.IHDataOpt ihopt = new MSYS.IHDataOpt();
                    //  DataRowCollection Rows = ihopt.GetData(starttime, endtime, point);
                    MSYS.IHAction ihopt = new MSYS.IHAction();
                    MSYS.IHAction.TimeSeg seg = ihopt.GetTimeSegP(starttime, endtime, point, prodcode);
                     Rows = ihopt.GetIHRealDataSet(seg);

                }

            }
        }
        return Rows;
    }
 
}