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
                                    if (merge)
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
                    openXMLExcel.SaveAsHtm(filepath);
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
        private void KillProcess(string processname)
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

        private int getColumn(string Col)
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
