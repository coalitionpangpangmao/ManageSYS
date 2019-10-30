using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using MSYS.Common;
using MSYS;
using System.Diagnostics;
namespace DataCollect
{
    public partial class Form1 : Form
    {
        private NotifyIcon notifyIcon = null;
        private System.Threading.Timer QualityTimer = null;//质量采集线程
        private System.Threading.Timer ReportTimer = null;//报表记录线程
        private System.Threading.Timer CheckTimer = null;//检查是否正常运行线程
        public Form1()
        {
            InitializeComponent();          
            //正式运行时取消注释
            //System.Timers.Timer aTimer = new System.Timers.Timer();
            //aTimer.Elapsed += new ElapsedEventHandler(InsertQuaReport);  //到达时间的时候执行事件；
            //aTimer.Interval =240*60* 1000;

            //aTimer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            //aTimer.Enabled = true; //是否执行System.Timers.Timer.Elapsed事件；
            //Qua_Start.Enabled = false;        
           
            InitialTray();
            startCollect();
        }

         ~Form1()
        {
            if (QualityTimer != null)
            {
                QualityTimer.Dispose();
                QualityTimer = null;
            }
            if (ReportTimer != null)
            {
                ReportTimer.Dispose();
                ReportTimer = null;
            }
        }

         private void startCollect()
         {
             if (QualityTimer == null)
             {
                 QualityTimer = new System.Threading.Timer(new System.Threading.TimerCallback(InsertQuaReport), null, 0, 30*60*1000);
             }
             if (ReportTimer == null)
             {
                 ReportTimer = new System.Threading.Timer(new System.Threading.TimerCallback(InsertProdReport), null, 0, 60 * 1000);
             }
             if (CheckTimer == null)
             {
                 CheckTimer = new System.Threading.Timer(new System.Threading.TimerCallback(CheckThread), null, 0, 30 * 60 * 1000);
             }
         }
        #region  隐藏托盘
        //默认隐藏
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();
        }
        //而是将窗体隐藏，实现一个“伪关闭”  
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        private void InitialTray()
        {
            //隐藏主窗体 
            this.Hide();
            //实例化一个NotifyIcon对象  
            notifyIcon = new NotifyIcon();
            //托盘图标气泡显示的内容  
            notifyIcon.BalloonTipText = "正在后台运行";
            //托盘图标显示的内容  
            notifyIcon.Text = "质量采集程序";
            //注意：下面的路径可以是绝对路径、相对路径。但是需要注意的是：文件必须是一个.ico格式  
            notifyIcon.Icon = new System.Drawing.Icon("../collector.ico");
            //true表示在托盘区可见，false表示在托盘区不可见  
            notifyIcon.Visible = true;
            //气泡显示的时间（单位是毫秒）  
            notifyIcon.ShowBalloonTip(2000);
            notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(notifyIcon_MouseClick);
            ////设置二级菜单  
            //MenuItem setting1 = new MenuItem("二级菜单1");  
            //MenuItem setting2 = new MenuItem("二级菜单2");  
            //MenuItem setting = new MenuItem("一级菜单", new MenuItem[]{setting1,setting2});              //帮助选项，这里只是“有名无实”在菜单上只是显示，单击没有效果，可以参照下面的“退出菜单”实现单击事件  
            //MenuItem help = new MenuItem("帮助");
            //关于选项  
            //MenuItem about = new MenuItem("关于");
            //退出菜单项  
            MenuItem exit = new MenuItem("退出");
            exit.Click += new EventHandler(exit_Click);
            ////关联托盘控件 
            //注释的这一行与下一行的区别就是参数不同，setting这个参数是为了实现二级菜单  
            //MenuItem[] childen = new MenuItem[] { setting, help, about, exit };
            MenuItem[] childen = new MenuItem[] { exit };
            notifyIcon.ContextMenu = new ContextMenu(childen);
            //窗体关闭时触发  
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);

        }
        private void notifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //鼠标左键单击  
            if (e.Button == MouseButtons.Left)
            {
                //如果窗体是可见的，那么鼠标左击托盘区图标后，窗体为不可见  
                if (this.Visible == true)
                {
                    this.Visible = false;
                }
                else
                {
                    this.Visible = true;
                    this.Activate();
                }
            }

        }
        //退出程序  
        private void exit_Click(object sender, EventArgs e)
        {  
            System.Environment.Exit(0);
        }  
        #endregion

        #region 点击响应
        public void Qua_Start_Click(object sender, EventArgs e)
        {
            InsertProdReportAsTime(QuaTime.Text.Substring(0, 10));
            QltRecoder.insertQuaReport(Convert.ToDateTime(QuaTime.Text));
            // ProdRecoder.ProdRecord(QuaTime.Text,"");
            //         MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            //DataSet times = opt.CreateDataSetOra("select date_begin  as time from ht_prod_schedule where date_begin between '2018-12-01' and '2018-12-15'");
            //         if (times != null && times.Tables[0].Rows.Count > 0)
            //         {

            //                 foreach (DataRow time in times.Tables[0].Rows)
            //                 {

            //                         ProdRecoder.ProdRecord(time["time"].ToString());
            //                 }

            //         }
        }
        private void Qua_Stop_Click(object sender, EventArgs e)
        {
            if (QualityTimer != null)
            {
                QualityTimer.Dispose();
                QualityTimer = null;
            }
            if (ReportTimer != null)
            {
                ReportTimer.Dispose();
                ReportTimer = null;
            }

        }
        public void Qua_show_Click(object sender, EventArgs e)
        {
            string endtime = Convert.ToDateTime(QuaTime.Text).AddDays(1).ToString("yyyy-MM-dd") + " 00:00:00";
            string starttime = Convert.ToDateTime(QuaTime.Text).ToString("yyyy-MM-dd") + " 00:00:00";
            string query = "select * from HT_QLT_DATA_RECORD where B_TIME >= '" + starttime + "' and  E_TIME <= '" + endtime + "'";
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataSet data = opt.CreateDataSetOra(query);
            if (data != null)
            dataGridView1.DataSource = data.Tables[0];

        }
#endregion

        #region 质量统计
        


        private void InsertQuaReport(object source)
        {

            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataSet times = opt.CreateDataSetOra("select date_begin  as time from ht_prod_schedule where substr(date_begin,1,10) = '" + System.DateTime.Now.ToString("yyyy-MM-dd") + "' union select date_end  as time  from ht_prod_schedule where substr(date_end,1,10) = '" + System.DateTime.Now.ToString("yyyy-MM-dd") + "'");
            if (times != null && times.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow time in times.Tables[0].Rows)
                {
                    if (System.DateTime.Now - Convert.ToDateTime(time["time"].ToString()) > new TimeSpan(0, 20, 0) && System.DateTime.Now - Convert.ToDateTime(time["time"].ToString()) < new TimeSpan(0, 50, 0))
                    {
                        //对每一班组的生产过程数据进行统计分析计算 
                        try
                        {
                            QltRecoder.insertQuaReport(System.DateTime.Now.AddHours(-8));
                        }
                        catch (Exception )
                        {
                            if (QualityTimer != null)
                            {
                                QualityTimer.Dispose();
                                QualityTimer = null;
                            }
                        }
                    }
                }
            }
        }
#endregion

        #region 生产报表记录       


        protected void InsertProdReport(object o)
        {           
                try
                {                   
                    ProdRecoder.ProdRecord(System.DateTime.Now);
                }
                catch
                {
                    if (ReportTimer != null)
                    {
                        ReportTimer.Dispose();
                        ReportTimer = null;
                    }
                }
            }

        protected void InsertProdReportAsTime(string datetime)
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataSet times = opt.CreateDataSetOra("select date_begin  as time ,team_code from ht_prod_schedule where substr(date_begin,1,10) = '" + datetime + "' union select date_end as time,team_code  from ht_prod_schedule where substr(date_end,1,10) ='" + datetime + "' union select starttime  as time ,''  from ht_prod_report where substr(starttime,1,10) = '" + datetime + "' union select endtime  as time,''  from ht_prod_report where substr(endtime,1,10) = '" + datetime + "'");
            if (times != null && times.Tables[0].Rows.Count > 0)
            {
                try
                {

                    foreach (DataRow time in times.Tables[0].Rows)
                    {
                        ProdRecoder.ProdRecord(time["time"].ToString(), time["team_code"].ToString());
                    }
                }
                catch
                {

                }
            }
        }
        #endregion
        protected void CheckThread(object o)
        {
            if (QualityTimer == null)
            {
                QualityTimer = new System.Threading.Timer(new System.Threading.TimerCallback(InsertQuaReport), null, 0, 30 * 60 * 1000);
            }
            if (ReportTimer == null)
            {
                ReportTimer = new System.Threading.Timer(new System.Threading.TimerCallback(InsertProdReport), null, 0, 60 * 1000);
            }
        }
     




    }
}

