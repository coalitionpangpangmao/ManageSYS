using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using MSYS.Web;
using MSYS.Data;
using MSYS.Security;
using System.Data;
using System.IO;
public partial class Login : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["#$LoginUser$#"];
            if (cookie != null) 
             RemoteCheck(cookie);
        }
    
    }
    protected void btnLogin_Click(object sender, ImageClickEventArgs e)
    {
        Submit();
    }
    protected void btnReset_Click(object sender, ImageClickEventArgs e)
    {
        FormsAuthentication.SignOut();
        HttpContext.Current.Session.Clear();
        user.Text = "";
        pwd.Text = "";
    }

    //登录操作

    private void Submit()
    {        
        FormsAuthentication.SignOut();
        HttpContext.Current.Session.Clear();

        string userID = user.Text;
         
        string userPwd =  MSYS.Security.Encrypt.GetMD5String(pwd.Text);
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataTable userTable = opt.CreateDataSetOra("select * from ht_svr_user t where t.id = '" + userID + "'").Tables[0];
         
        bool isError = false;
        if (userTable != null && userTable.Rows.Count > 0)
        {
            if (userID == userTable.Rows[0]["ID"].ToString() && userPwd == userTable.Rows[0]["PASSWORD"].ToString())
            {
                //记住密码
                if (CheckBox1.Checked)
                {                    
                    this.Response.Cookies.Remove("#$LoginUser$#");
                    HttpCookie hc = new HttpCookie("#$LoginUser$#");                 
                    hc.Expires = DateTime.MaxValue;
                    hc.Values.Add("uID", userID);
                    hc.Values.Add("uPwd", userPwd);
                    this.Response.Cookies.Add(hc);
                }
              //  else
              //  {
              //      DelCookies("127.0.0.1");
              //      DelCookies("nlocalhost");
              ////      HttpContext.Current.Request.Cookies["#$LoginUser$#"];
              //      HttpCookie hc = new HttpCookie(userID, "");
              //      hc.Expires = DateTime.MinValue;
              //      this.Response.Cookies.Add(hc);
              //  }

                //记住最后一次登录用户
                //string cName = "#$LastLoginUser$#";
                //this.Response.Cookies.Remove(cName);
                //HttpCookie Lhc = new HttpCookie(cName);
                //Lhc.Values.Add("uID", userID);
                //Lhc.Expires = DateTime.MaxValue;
                //this.Response.Cookies.Add(Lhc);

                //设置Session 并跳转
                FormsAuthentication.RedirectFromLoginPage(userID, false);
               //设置Session              
                Session["User"] = new SysUser(userID);
                InsertTlog( "登入");
                string returnUrl = Request["ReturnUrl"];
                if (string.IsNullOrEmpty(returnUrl))
                    returnUrl = "main.aspx";
                Response.Redirect(returnUrl);
            }
            else
                isError = true;
        }
        else
            isError = true;

        if (isError)
        {
            Response.Write("<script language=\"javascript\" type=\"text/javascript\">alert('用户名或密码出错.'); window.attachEvent('onload',function(){document.getElementById(\"Submit1\").disabled=false;});</script>");
        }
    }
    //判断本地是否有Cookies 如果有直接登陆跳转入主页面
    private void RemoteCheck(HttpCookie cookie)
    {
        try
        {
            string userID = cookie["uID"].ToString();
            string userPwd = cookie["uPwd"].ToString();
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataTable userTable = opt.CreateDataSetOra("select * from ht_svr_user t where t.id = '" + userID + "'").Tables[0];
            if (userTable != null && userTable.Rows.Count > 0)
            {
                if (userID == userTable.Rows[0]["ID"].ToString() && userPwd == userTable.Rows[0]["Password"].ToString())
                {

                    Session["User"] = new SysUser(userID);
                    InsertTlog("登入");
                    string returnUrl = Request["ReturnUrl"];
                    if (string.IsNullOrEmpty(returnUrl))
                        returnUrl = "main.aspx";
                    Response.Redirect(returnUrl);
                }
                else
                {
                    user.Text = userID;
                    pwd.Text = userPwd;
                }
            }
        }
        catch (Exception e)
        {
            return;
        }

        
    }

    static void DelCookies(string domain)     //domain是cookies所属域,此方法是通过所属域过滤清除cookies
    {
        //获取目录中文件路径
        string[] cookies = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Cookies));

        foreach (string file in cookies)
        {           
                StreamReader sr = new StreamReader(file);
                string txt = sr.ReadToEnd();
                sr.Close();
                if (txt.IndexOf(domain) != -1)        //判断是否删除的cookies文件
                {
                    File.Delete(file);
                }
         
        }
    }
}