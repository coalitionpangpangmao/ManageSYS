using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.SessionState;
using MSYS.DAL;
using MSYS.Data;
using System.Linq;


namespace MSYS.Web
{
    public class BasePage : Page
    {
        // Fields
        private static HttpSessionState _session;
        private string m_sessionId;
        private string m_mappingId;
        private string m_uniqId;
        private bool m_isHasRight;
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
                    this.m_mappingId = opt.GetSegValue("select * from ht_inner_map t where t.url = '" + str.Substring(11) + "'", "MAPID");
                    SysRightCollection rightCol = ((SysUser)Session["User"]).UserRights;
                    var query = from SysRight right in rightCol
                                where right.mapID == this.m_mappingId && right.eType == SysRight.RightType.Button
                                select right;
                    foreach (SysRight s in query)
                        this.m_isHasRight = true;
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






    }

}
