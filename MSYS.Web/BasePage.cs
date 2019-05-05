using System;
using System.Text;
using System.Web.UI;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.SessionState;
using MSYS.DAL;
using MSYS.Data;
using System.Linq;
using System.IO;
using System.Diagnostics;
using MSYS;
using System.Text.RegularExpressions;

namespace MSYS.Web
{
    public class BasePage : Page
    {
        // Fields
        private static HttpSessionState _session;
        private string m_sessionId;
        private string m_mappingId;
        private string m_mappingUrl;
        private string m_uniqId;
        private bool m_isHasRight;
        private string m_rightID;
        /* 用户是否具有当前页面的操作权限
      系统有两种操作权限：菜单与按钮，两种权限均与页面有关。在数据库中建立内部库，存储每个页面的URL以及ID值
      在权限操作中指定该权限对应的页面ID，在页面初始化时通过比较页面URL获取当前操作页面的权限值
       例：HttpContext.Current.Request.Url.PathAndQuery
       1、通过ASP.NET获取
       如果测试的url地址是http://www.test.com/testweb/default.aspx, 结果如下：
       Request.ApplicationPath: /testweb
       Request.CurrentExecutionFilePath: /testweb/default.aspx
       Request.FilePath: /testweb/default.aspx
       Request.Path: /testweb/default.aspx
       Request.PhysicalApplicationPath: E:\WWW\testwebRequest.PhysicalPath: E:\WWW\testweb\default.aspx
       Request.RawUrl: /testweb/default.aspx
       Request.Url.AbsolutePath: /testweb/default.aspx
       Request.Url.AbsoluteUrl: http://www.test.com/testweb/default.aspx
       Request.Url.Host: http://www.test.com/
       Request.Url.LocalPath: /testweb/default.aspx * */


        //构造函数
        static BasePage()
        {
        }

        public BasePage()
        {


        }

        protected void PageLoad(object sender, EventArgs e)
        {
            //判断HasRight是否为真，如果为真，即有当前页面操作权限，Show所有操作按钮设置属性，如果为假，Hide所有操作按钮  
            if (Session["User"] == null)
            {
                Response.Redirect("/ManageSYS/Login.aspx");
            }
           
                _session = HttpContext.Current.Session;
                this.SessionId = _session.SessionID;
                this.m_uniqId = Guid.NewGuid().ToString();
                this.m_isHasRight = false;
                this.m_mappingId = string.Empty;
                string str = HttpContext.Current.Request.Url.AbsolutePath;
                if (!string.IsNullOrEmpty(str))
                {
                    DbOperator opt = new DbOperator();
                    if (str.Contains("ManageSYS"))
                        str = str.Substring(11);
                    else
                        str = str.Substring(1);
                    m_mappingUrl = str;
                    this.m_mappingId = opt.GetSegValue("select * from ht_inner_map t where t.url = '" + str + "'", "MAPID");
                    SysRightCollection rightCol = ((SysUser)Session["User"]).UserRights;
                    var query = from SysRight right in rightCol
                                where right.mapID == this.m_mappingId && right.eType == SysRight.RightType.Button
                                select right;
                    foreach (SysRight s in query)
                    {
                        this.m_isHasRight = true;
                        this.m_rightID = s.id;
                    }
                }             
            
            ////////////////////////////////////////////////////////////////////////////////////////////
            Control myControl1 = FindControl("UpdatePanel1");
            Control myControl2 = FindControl("UpdatePanel2");
            if (this.m_isHasRight)
            {
                if (myControl1 == null && myControl2 == null)
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "show", "<script>$('.auth').show();</script>", false);
                else
                {
                    if (myControl1 != null)
                        ScriptManager.RegisterStartupScript(myControl1, this.Page.GetType(), "show1", "<script>$('.auth').show();</script>", false);
                    if (myControl2 != null)
                        ScriptManager.RegisterStartupScript(myControl2, this.Page.GetType(), "show2", "<script>$('.auth').show();</script>", false);
                }
            }
            else
            {
                if (myControl1 == null && myControl2 == null)
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "show", "<script>$('.auth').hide();</script>", false);
                else
                {
                    if (myControl1 != null)
                        ScriptManager.RegisterStartupScript(myControl1, this.Page.GetType(), "show1", "<script>$('.auth').hide();</script>", false);
                    if (myControl2 != null)
                        ScriptManager.RegisterStartupScript(myControl2, this.Page.GetType(), "show2", "<script>$('.auth').hide();</script>", false);
                }

            }
          

        }
        // Properties
        public virtual bool HasRight
        {
            get
            {
                return this.m_isHasRight;
            }
        }

        public virtual string MappingId
        {
            get
            {
                return this.m_mappingId;
            }
        }

        public virtual string SessionId
        {
            get
            {
                return this.m_sessionId;
            }
            set
            {
                this.m_sessionId = value;
            }
        }

        public virtual string UniqueId
        {
            get
            {
                return this.m_uniqId;
            }
        }

        public virtual string RightId
        {
            get
            {
                return this.m_rightID;
                    
            }
        }
        public virtual string MappingUrl
        {
            get
            {
                return this.m_mappingUrl;
            }
        }

        //方法
        //对Session键值操作
        public static void SetSession(string key, object value)
        {
            _session[key] = value;
        }
        public static int GetSessionNumber(string key)
        {
            int result = 0;
            if (_session[key] != null)
            {
                int.TryParse(_session[key].ToString(), out result);
            }
            return result;
        }
        public static string GetSessionString(string key)
        {
            string result = "";
            if (_session[key] != null)
            {
                result = _session[key].ToString();
            }
            return result;
        }
        public static void ClearSession()
        {
            _session.Clear();
        }


        //注册JS及Style
        public void RegisterScript(string path)
        {
            this.RegisterScript(path, HttpContext.Current.Application["ProjectName"].ToString() + "/js/");
        }

        public void RegisterScript(string path, string basePath)
        {
            Literal child = new Literal
            {
                Text = string.Format("<script language=\"javascript\" type=\"text/javascript\" src=\"{0}\"></script>", new object[] { basePath + path })
            };
            this.Page.Header.Controls.Add(child);
        }

        public void RegisterStyle(string path)
        {
            //  this.RegisterStyle(path, UserCache.Skin);
        }

        public void RegisterStyle(string path, string skin)
        {
            Literal child = new Literal
            {
                Text = string.Format("<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" ></link>", new object[] { HttpContext.Current.Application["ProjectName"].ToString() + "/Skins/" + skin + "/" + path })
            };
            this.Page.Header.Controls.Add(child);
        }

        //根据操作用户插入日志
        public void InsertTlog(string record)
        {
            MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string query = "insert into HT_SVR_LOGIN_RECORD(F_USER,F_COMPUTER,F_TIME,F_DESCRIPT)values('"
                  + user.text + "','"
                  + Page.Request.UserHostName.ToString()  + "','"
                  + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','"
                  + record + "')";
            opt.UpDateOra(query);
         
        
        }

  //在服务器中找到报表模板，从数据库中选择数据，将数据写入模板中，把该文件保存在服务器的C://temp目录下；客户端再下载该文件，就能在客户端进行浏览
        public string CreateExcel(string filename, string brand, string startDate, string endDate, string team,string style,string month,DateTime date,bool merge)
        {           
            MSYS.Common.ExcelExport openXMLExcel = null;
            try
            {
                MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                string bookid = opt.GetSegValue("select F_ID from ht_sys_excel_book where F_NAME = '" + filename + "'", "F_ID");
                string booktype = opt.GetSegValue("select F_TYPE from ht_sys_excel_book where F_NAME = '" + filename + "'", "F_TYPE");

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
                String sourcePath = basedir + @"templates\" + booktype + @"\" + filename + ".xls";

                String filepath = basedir + @"TEMP\" + filename + date.ToString("HHmmss") + style;
                //bool isrewrite = true; // true=覆盖已存在的同名文件,false则反之
                //System.IO.File.Copy(sourcePath, filepath, isrewrite);
                string query = "select * from ht_sys_excel_seg where F_BOOK_ID = '" + bookid + "' order by  F_DES";
                DataSet data = opt.CreateDataSetOra(query);

                //申明一个ExcelSaveAs对象，该对象完成将数据写入Excel的操作           
                openXMLExcel = new MSYS.Common.ExcelExport(sourcePath, false);
                if (data.Tables[0].Select().GetLength(0) > 0)
                {
                    DataRow[] rows = data.Tables[0].Select();

                    foreach (DataRow row in rows)
                    {
                        string sqlstr = row["F_SQL"].ToString();
                        //设定选择的数据的日期及牌号等信息                     
                        if (brand != "")
                            sqlstr = sqlstr.Replace("$brand$", brand);
                        if (startDate != "")
                            sqlstr = sqlstr.Replace("$startDate$", startDate);
                        if (endDate != "")
                            sqlstr = sqlstr.Replace("$endDate$", endDate);
                        if (team != "")
                            sqlstr = sqlstr.Replace("$team$", team);
                        if (month != "")
                            sqlstr = sqlstr.Replace("$month$", month);
                        if (sqlstr != "")
                        {
                            //将选择的数据写入Excel
                            
                            if (sqlstr.Length > 4 && sqlstr.Substring(0, 3) == "STR")
                            {
                                sqlstr = sqlstr.Substring(4);
                                Response.Write(openXMLExcel.SetCurrentSheet(Convert.ToInt32(row["F_SHEETINDEX"].ToString())));
                                Response.Write(openXMLExcel.WriteData(Convert.ToInt32(row["F_DESX"].ToString()), getColumn(row["F_DESY"].ToString()) + 1, sqlstr));
                            }
                            if (sqlstr.Length > 5 && sqlstr.Substring(0, 4) == "Proc")
                            {
                                int pos = sqlstr.IndexOf('@');
                                int pos2 = sqlstr.IndexOf('#');
                                string proc = sqlstr.Substring(5,pos -5);  
                                int paracount = Regex.Matches(sqlstr,@"@").Count;
                                List<string> seglist = new List<string>();
                                List<string> paralist = new List<string>();
                                for (int i = 0; i < paracount; i++)
                                {                                  
                                    seglist.Add(sqlstr.Substring(pos+1, pos2 - pos-1));
                                    pos = sqlstr.IndexOf('@',pos2);
                                    if (pos > 0)
                                    {
                                        paralist.Add(sqlstr.Substring(pos2 + 1, pos - pos2 - 1));
                                        pos2 = sqlstr.IndexOf('#', pos);
                                    }
                                    else
                                        paralist.Add(sqlstr.Substring(pos2 + 1));
                                    
                                }
                                opt.ExecProcedures(proc, seglist.ToArray(), paralist.ToArray());  
                            }
                            if (sqlstr.Length > 20 && sqlstr.Substring(0, 3) == "SQL")
                            {
                                sqlstr = sqlstr.Substring(4).Trim();
                                System.Diagnostics.Debug.WriteLine(sqlstr);
                                bool hasCaption = false;
                                if (sqlstr.Substring(0, 1) == "$")
                                {
                                    hasCaption = true;
                                    sqlstr = sqlstr.Substring(1);
                                }
                                DataSet set = new DataSet();
                                if (sqlstr.Substring(0, 5) == "SHIFT")
                                {
                                    sqlstr = sqlstr.Substring(6).Trim();
                                    set = opt.ShiftTable(sqlstr);
                                }
                                else
                                {
                                    set = opt.CreateDataSetOra(sqlstr);
                                }
                                if (set != null)
                                {
                                    DataTable dt = set.Tables[0];

                                    openXMLExcel.SetCurrentSheet(Convert.ToInt32(row["F_SHEETINDEX"].ToString()));
                                    if (filename == "再造梗丝产品过程检测日报")
                                    {
                                        
                                            //openXMLExcel.WriteData(4, 1, startDate.ToString());


                                        double[] avg = getAvg(startDate);
                                        for (int i = 0; i < avg.Length; i++) {
                                            System.Diagnostics.Debug.WriteLine(avg[i]);
                                        }
                                        double[] quarate = getQuaRate(startDate);
                                        int[] interval = getInterval(startDate);
                                        openXMLExcel.WriteData(1, 1, startDate + " 梗丝线日报表");
                                        string qu = "select * from hv_qlt_process_daily_report t where record_time='"+startDate+"' order by team_id";
                                        System.Diagnostics.Debug.WriteLine("日期"+startDate);
                                        //System.Diagnostics.Debug.WriteLine(Convert.ToInt32(row["F_DESY"].ToString()));
                                        DataTable dtt = opt.CreateDataSetOra(qu).Tables[0];
                                        int startX = 2;
                                        int startY = 3;
                                        for (int i = 3, y=0; i < dtt.Columns.Count -1 && y<dtt.Rows.Count; i++) {
                                            //openXMLExcel.WriteData(y+4, i+64, dtt.Rows[y][i].ToString());
                                            openXMLExcel.WriteData(y+4, i, dtt.Rows[y][i].ToString());
                                            //System.Diagnostics.Debug.WriteLine(Convert.ToInt32("A").ToString());
                                            System.Diagnostics.Debug.WriteLine(dtt.Rows[y][i].ToString());
                                            if (i == (dtt.Columns.Count - 2) && y < dtt.Rows.Count) {
                                                i = 2;
                                                y++;
                                                continue;
                                            }
                                        }
                                        qu = "select distinct t.inspect_code, r.range, t.value, r.avrg, r.std, r.quarate from HV_QLT_PROCESS_INSPECT_CODE t left outer join  hv_qlt_process_avg r on t.inspect_code = r.inspect_code where r.record_time='"+startDate+"' or  r.record_time is null  order by t.inspect_code";
                                        DataTable dttt = opt.CreateDataSetOra(qu).Tables[0];
                                        qu = "select t.section_name , r.inspect_code,r.inspect_name,s.lower_value||'~'||s.upper_value||r.unit as range from ht_qlt_inspect_proj r left join ht_qlt_inspect_stdd s on s.inspect_code = r.inspect_code left join ht_pub_tech_section t on t.section_code = r.inspect_group where r.INSPECT_TYPE = '0' and r.is_del = '0' order by r.inspect_code";
                                        DataTable uplower = opt.CreateDataSetOra(qu).Tables[0];
                                        for (int i = 0; i < dttt.Rows.Count; i++) {
                                            openXMLExcel.WriteData(9, i+3, dttt.Rows[i][3].ToString());
                                            openXMLExcel.WriteData(10, i+3, dttt.Rows[i][5].ToString());
                                            openXMLExcel.WriteData(11, i+3, dttt.Rows[i][4].ToString());
                                            
                                            
                                        }
                                       int j = 3;
                                        int d=0;
                                        for (int i = 0; i < interval.Length; i++) { 
                                            openXMLExcel.WriteData(7, j, avg[i].ToString());
                                            openXMLExcel.WriteData(8, j, quarate[i].ToString());
                                            openXMLExcel.WriteData(3, j, uplower.Rows[d][3].ToString());
                                            j = j + interval[i];
                                            d = d + interval[i];
                                        }

                                    }
                                    else if (merge)
                                    {
                                        if(hasCaption)
                                            openXMLExcel.WriteDataRerangeWithCaption(Convert.ToInt32(row["F_DESX"].ToString()), getColumn(row["F_DESY"].ToString()) + 1, dt);
                                        else
                                        openXMLExcel.WriteDataRerange(Convert.ToInt32(row["F_DESX"].ToString()), getColumn(row["F_DESY"].ToString()) + 1, dt);
                                    }
                                    else
                                    {
                                        if (hasCaption)
                                            openXMLExcel.WriteDataIntoWorksheetWithCaption(Convert.ToInt32(row["F_DESX"].ToString()), getColumn(row["F_DESY"].ToString()) + 1, dt);
                                        else
                                            openXMLExcel.WriteDataIntoWorksheet(Convert.ToInt32(row["F_DESX"].ToString()), getColumn(row["F_DESY"].ToString()) + 1, dt);
                                    }
                                }

                            }
                        }
                    }
                }

                ///客户端再下载该文件，在客户端进行浏览
                FileInfo fi = new FileInfo(filepath);
                if (fi.Exists)     //判断文件是否已经存在,如果存在就删除!
                {
                    fi.Delete();
                }
                if (style == ".xlsx" || style == ".xls")
                    openXMLExcel.SaveAs(filepath);
                else
                {
                    openXMLExcel.SaveAsHtm(filepath);
                    System.Diagnostics.Debug.WriteLine("路径");
                    System.Diagnostics.Debug.WriteLine(filepath);
                    if (filename == "再造梗丝产品过程检测日报")
                    {
                        string fp = basedir + @"TEMP\" + filename + date.ToString("HHmmss") + @".files\sheet001.htm";
                        System.Diagnostics.Debug.WriteLine(fp);
                        addCSS(fp);
                    }
                    
                }
                openXMLExcel.Dispose();
                openXMLExcel = null;
                KillProcess("EXCEL.EXE");
                return "Success";
            }
            catch(Exception e)
            {
                if (openXMLExcel != null)
                {
                    openXMLExcel.Dispose();
                }
                return e.Message;
            }
            finally
            {
            }
        }


        public double[] getAvg(String data){
            List<string> items = new List<string>();
            string query = "select * from hv_qlt_process_inspect_code order by inspect_code";
            string que = "select * from hv_qlt_process_avg where record_time='"+data+"' order by inspect_code";
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataTable dt = opt.CreateDataSetOra(query).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++) {
                String temp = dt.Rows[i][2].ToString().Substring(0,3);
                System.Diagnostics.Debug.WriteLine(temp);
                if (!items.Contains(temp))
                {
                    items.Add(temp);
                }

            }
            double[] sum = new double[items.Count];
            double[] count = new double[items.Count];
            double[] quarate = new double[items.Count];
            //items.Clear();
            DataTable dtt = opt.CreateDataSetOra(que).Tables[0];
            int j = -1;
            for (int i = 0; i < dtt.Rows.Count; i++) {
                String temp = dtt.Rows[i][2].ToString().Substring(0, 3);
                j=items.FindIndex(a=>a==temp);
                if (-1==j)
                {
                    continue;
              
                    
                }
                sum[j] += double.Parse(dtt.Rows[i][9].ToString());
                
                //quarate[j] += Convert.ToInt32(dt.Rows[i][4].ToString());
                count[j]++;
            }
            for (int i = 0; i < sum.Length; i++) {
                if (count[i] == 0) {
                    sum[i] = 0;
                    continue;
                }
                sum[i] = sum[i] / count[i];
            }
            return sum;
        }

        public double[] getQuaRate(string data)
        {
            List<string> items = new List<string>();
            string query = "select * from hv_qlt_process_inspect_code order by inspect_code";
            string que = "select * from hv_qlt_process_avg where record_time='"+data+"' order by inspect_code";
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataTable dt = opt.CreateDataSetOra(query).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String temp = dt.Rows[i][2].ToString().Substring(0, 3);
                System.Diagnostics.Debug.WriteLine(temp);
                if (!items.Contains(temp))
                {
                    items.Add(temp);
                }

            }
            double[] sum = new double[items.Count];
            double[] count = new double[items.Count];
            double[] quarate = new double[items.Count];
            //items.Clear();
            DataTable dtt = opt.CreateDataSetOra(que).Tables[0];
            int j = -1;
            for (int i = 0; i < dtt.Rows.Count; i++)
            {
                String temp = dtt.Rows[i][2].ToString().Substring(0, 3);
                j = items.FindIndex(a => a == temp);
                if (-1 == j)
                {
                    continue;


                }
                quarate[j] += double.Parse(dtt.Rows[i][4].ToString());

                //quarate[j] += Convert.ToInt32(dt.Rows[i][4].ToString());
                count[j]++;
            }
            for (int i = 0; i < sum.Length; i++)
            {
                if (count[i] == 0)
                {
                    quarate[i] = 0;
                    continue;
                }
                quarate[i] = quarate[i] / count[i];
            }
            return quarate;
        }


        public int[] getInterval(String data)
        {
            List<string> items = new List<string>();
            string query = "select * from hv_qlt_process_inspect_code order by inspect_code";
            string que = "select * from hv_qlt_process_avg where record_time='"+data+"' order by inspect_code";
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataTable dt = opt.CreateDataSetOra(query).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String temp = dt.Rows[i][2].ToString().Substring(0, 3);
                System.Diagnostics.Debug.WriteLine(temp);
                if (!items.Contains(temp))
                {
                    items.Add(temp);
                }

            }
            double[] sum = new double[items.Count];
            int[] count = new int[items.Count];
            double[] quarate = new double[items.Count];
            items.Clear();
            DataTable dtt = opt.CreateDataSetOra(que).Tables[0];
            int j = -1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string temp = dt.Rows[i][2].ToString().Substring(0, 3);
                if (!items.Contains(temp))
                {
                    items.Add(temp);
                    j++;
                }
                count[j]++;
                
            }
            return count;
        }
        public void addCSS(String path) {
            StreamReader sr = new StreamReader(path, System.Text.Encoding.GetEncoding("GB2312"));
            string str = sr.ReadToEnd();
            sr.Close();
            int index = str.IndexOf("<head>");
            string css = "<style>table{text-align: center;}</style>";
            str = str.Insert(index+6, css);
            
            StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.GetEncoding("GB2312"));
            sw.WriteLine(str);
            sw.Close();
        }

        public void ExportExcel(string filename, string brand, string startDate, string endDate, string team, string style,string month,DateTime date,bool merge)
        {
            try
            {
                CreateExcel(filename, brand, startDate, endDate, team, style,month,date,merge);
                String filepath = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"TEMP\" + filename + date.ToString("HHmmss") + style;

                //Response.ContentType = "application/octet-stream";
                //Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename + style, System.Text.Encoding.UTF8));
                //Response.TransmitFile(filepath);


                Response.Clear();

                Response.Buffer = true;

                Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename + style, System.Text.Encoding.UTF8));
                if(style == ".xls")
                  Response.ContentType = "application/vnd.ms-excel";
                else
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                Response.TransmitFile(filepath);

                Response.End();
            }
            catch
            {
                Response.Write("文件下载出错！！");
            }
        }
        public void KillProcess(string processname)
        {
            Process[] allProcess = Process.GetProcesses();
            foreach (Process p in allProcess)
            {
                if (p.ProcessName.ToLower() + ".exe" == processname.ToLower())
                {
                    for (int i = 0; i < p.Threads.Count; i++)
                    {
                       if((System.DateTime.Now - p.Threads[i].StartTime).Seconds >30)
                        p.Threads[i].Dispose();
                    }
                   // p.Kill();

                    // break;
                }
            }
        }

        public int getColumn(string Col)
        {
            int ColNum;
            switch (Col.Trim())
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





    }

}
