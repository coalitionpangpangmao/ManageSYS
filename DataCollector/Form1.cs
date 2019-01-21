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
            QltRecoder.insertQuaReport(Convert.ToDateTime(QuaTime.Text));
            ProdRecoder.ProdRecord(QuaTime.Text,"");
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
        //private void InsertQuaReport(object source)
        //{
                
        //    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        //    DataSet times = opt.CreateDataSetOra("select date_begin  as time from ht_prod_schedule where substr(date_begin,1,10) = '" + System.DateTime.Now.ToString("yyyy-MM-dd") + "' union select date_end  as time  from ht_prod_schedule where substr(date_end,1,10) = '" + System.DateTime.Now.ToString("yyyy-MM-dd") + "'");
        //    if (times != null && times.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow time in times.Tables[0].Rows)
        //        {
        //            if (System.DateTime.Now - Convert.ToDateTime(time["time"].ToString()) > new TimeSpan(3,50, 0) && System.DateTime.Now - Convert.ToDateTime(time["time"].ToString()) < new TimeSpan(4, 40, 0))
        //            {
        //                //对每一班组的生产过程数据进行统计分析计算     
        //                insertQuaReport(System.DateTime.Now.AddHours(-8));
        //            }
        //        }
        //    }          
        //}
        //private void insertQuaReport(DateTime date)
        //{
        //    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
           
        //   // string datetime = System.DateTime.Now.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss");
        //    string datetime = date.ToString("yyyy-MM-dd HH:mm:ss");
        //    //  string datetime = "2018-09-13 09:30:00";
        //    DataSet data = opt.CreateDataSetOra("select * from ht_prod_schedule t  where date_begin <='" + datetime + "' and date_end >= '" + datetime + "'");
        //    if (data != null && data.Tables[0].Rows.Count > 0)
        //    {
        //        DataRow row = data.Tables[0].Rows[0];
        //        string starttime = row["date_begin"].ToString();
        //        string midtime = Convert.ToDateTime(starttime).AddHours(4).ToString("yyyy-MM-dd HH:mm:ss");
        //        string endtime = row["date_end"].ToString();
        //        InsertSectionQuaReport(starttime, midtime);
        //        InsertSectionQuaReport(midtime, endtime);
        //    }
        //}
        //private void InsertQuaReport(object source, System.Timers.ElapsedEventArgs e)
        //{
        //    //对每一班组的生产过程数据进行统计分析计算
        //    string datetime = System.DateTime.Now.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss");
        //    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        //    DataSet data = opt.CreateDataSetOra("select * from ht_prod_schedule t  where date_begin <='" + datetime + "' and date_end >= '" + datetime + "'");
        //    if (data != null && data.Tables[0].Rows.Count > 0)
        //    {
        //        DataRow row = data.Tables[0].Rows[0];
        //        string starttime = row["date_begin"].ToString();
        //        string midtime = Convert.ToDateTime(starttime).AddHours(4).ToString("yyyy-MM-dd HH:mm:ss");
        //        string endtime = row["date_end"].ToString();
        //        InsertSectionQuaReport(starttime, midtime);
        //        InsertSectionQuaReport(midtime, endtime);
        //    }
        //}

        //private void InsertSectionQuaReport(string starttime, string endtime)
        //{
        //    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        //    DataSet data = opt.CreateDataSetOra("select distinct r.section_code ,r.section_name from ht_pub_tech_section r left join ht_pub_tech_para s on substr(s.para_code,1,5) = r.section_code and s.is_del = '0' and s.is_valid = '1' where r.is_del = '0' and r.is_valid = '1' and  s.para_type like '___1%'   order by r.section_code");
        //    IHDataOpt ihopt = new IHDataOpt();
        //    foreach (DataRow row in data.Tables[0].Rows)
        //    {
        //        //判断该工艺段是否有批次报告记录
        //        int count =Convert.ToInt16( opt.GetSegValue( "select count(rowid) as count from ht_qlt_data_record t where substr(para_code,1,5) = '" + row["section_code"].ToString() + "' and b_time <= '" + starttime + "' and e_time >= '" + endtime + "'","count"));
        //        string query = "select r.para_code from ht_pub_tech_para r where substr(r.para_code,1,5) = '" + row["section_code"].ToString() + "' and r.para_type like '___1%'";
        //        DataSet pointsets = opt.CreateDataSetOra(query);                
        //        if (count < pointsets.Tables[0].Rows.Count && ihopt.TaskShiftNum(starttime, endtime, row["section_code"].ToString()) > 0)
        //        {                    
        //            foreach (DataRow prow in pointsets.Tables[0].Rows)
        //            {
        //                insertPointQuaReport(starttime, endtime, prow["para_code"].ToString());
        //            }
        //        }
        //    }
        //}

        //private void insertPointQuaReport(string starttime, string endtime, string para_code)
        //{
        //    IHDataOpt ihopt = new IHDataOpt();
        //    List<IHDataOpt.TimeSeg> timesegs = ihopt.TimeCut(starttime, endtime, para_code);
        //    foreach (IHDataOpt.TimeSeg seg in timesegs)
        //    {
        //        InsertRecord(seg);
        //    }
        //}
        ///// <summary>
        ///// 插入某工艺点质量统计报告
        ///// </summary>
        ///// <param name="seg">开始、结束时间、数据点code、计划号</param>     
        //protected void InsertRecord(IHDataOpt.TimeSeg seg)
        //{
            
        //    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        //    int recordnum = Convert.ToInt16( opt.GetSegValue("select count(rowid) as count  from ht_qlt_data_record  where plan_id = '" + seg.planno + "' and para_code = '" + seg.nodecode + "'  and b_time = '" + seg.starttime + "' and e_time = '" + seg.endtime + "'", "count"));
        //    //if (recordnum > 0)
        //    //    return;

        //    string teamcode = opt.GetSegValue("select team_code  from ht_prod_schedule where date_begin <='" + seg.starttime + "' and date_end >='" + seg.endtime + "' and work_staus = '1'", "team_code");

        //    IHDataOpt ihopt = new IHDataOpt();
        //    //根据开始、结束时间、数据点code、计划号获取原始数据，返回的数据为三列：时间、值、状态；状态为料头、料尾、过程值、断流
        //    DataTable dt = ihopt.GetIHOrgDataSet(seg);
        //    if (dt == null)
        //        return;
        //    string gaptime = "";
        //    string is_gap = "0";
        //    List<IHDataOpt.Gaptime> gaplist = ihopt.gaptimes();
        //    if (gaplist == null) gaptime = "0";
        //    else
        //    {
        //        int timecount = 0;
        //        foreach (IHDataOpt.Gaptime time in gaplist)
        //        {
        //            timecount += time.gaptime;
        //        }
        //        gaptime = timecount.ToString();
        //    }
        //    if (gaptime != "0")
        //        is_gap = "1";
        //    //////////////////////////////////////////////////////////////////////////////////////////
        //    double[] samples = null;
        //    if (dt != null && dt.Rows.Count > 0)
        //    {

        //        var Rows = from dt1 in dt.AsEnumerable()
        //                   where dt1.Field<string>("状态") == "过程值"
        //                   select dt1;
        //        DataView rsl = Rows.AsDataView();
        //        int count = rsl.Count;
        //        samples = new double[count];
        //        int i = 0;
        //        foreach (DataRow row in Rows)
        //        {
        //            samples[i] = Convert.ToDouble(row["值"].ToString());
        //            i++;
        //        }
        //    }
        //    ///////////////根据原始数据计算算统计数据///////////////////////////////////////////////////////

        //    if (samples != null && samples.GetLength(0) > 0)
        //    {
        //        DataSet set = opt.CreateDataSetOra("select s.para_code,s.value,s.upper_limit,s.lower_limit from ht_pub_prod_design r left join ht_tech_stdd_code_detail s on r.tech_stdd_code = s.tech_code  where r.prod_code = '" + seg.planno.Substring(8, 7) + "' and s.para_code = '" + seg.nodecode + "'");
        //        if (set != null && set.Tables[0].Select().GetLength(0) > 0 && set.Tables[0].Select()[0]["value"].ToString() != "" && set.Tables[0].Select()[0]["upper_limit"].ToString() != "" && set.Tables[0].Select()[0]["lower_limit"].ToString() != "")
        //        {
        //            string CL = set.Tables[0].Select()[0]["value"].ToString();
        //            string upvalue = set.Tables[0].Select()[0]["upper_limit"].ToString();
        //            string lowvalue = set.Tables[0].Select()[0]["lower_limit"].ToString();

        //            SPCFunctions spc = new SPCFunctions(samples, Convert.ToDouble(upvalue), Convert.ToDouble(lowvalue));
        //            string[] tableseg = { "PLAN_ID", "PROD_CODE", "PARA_CODE", "TEAM", "B_TIME", "E_TIME", "AVG", "COUNT", "MIN", "MAX", "QUANUM", "QUARATE", "UPCOUNT", "UPRATE", "DOWNCOUNT", "DOWNRATE", "STDDEV", "ABSDEV", "IS_GAP", "CPK", "VAR", "RANGE", "GAP_TIME"};
        //            string[] tablevalue = { seg.planno, seg.planno.Substring(8, 7), seg.nodecode, teamcode, seg.starttime, seg.endtime, spc.avg.ToString(), spc.count.ToString(), spc.min.ToString(), spc.max.ToString(), spc.passcount.ToString(), spc.passrate.ToString("f4"), spc.upcount.ToString(), spc.uprate.ToString("f4"), spc.downcount.ToString(), spc.downrate.ToString("f4"), spc.stddev.ToString("f4"), spc.absdev.ToString("f4"), is_gap,spc.Cpk.ToString("f4"), spc.var.ToString("f4"), spc.Range.ToString("f4"), gaptime };
        //            opt.MergeInto(tableseg, tablevalue, 6,"HT_QLT_DATA_RECORD");
        //        }
        //    }


        //}


        private void InsertQuaReport(object source)
        {

            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataSet times = opt.CreateDataSetOra("select date_begin  as time from ht_prod_schedule where substr(date_begin,1,10) = '" + System.DateTime.Now.ToString("yyyy-MM-dd") + "' union select date_end  as time  from ht_prod_schedule where substr(date_end,1,10) = '" + System.DateTime.Now.ToString("yyyy-MM-dd") + "'");
            if (times != null && times.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow time in times.Tables[0].Rows)
                {
                    if (System.DateTime.Now - Convert.ToDateTime(time["time"].ToString()) > new TimeSpan(3, 50, 0) && System.DateTime.Now - Convert.ToDateTime(time["time"].ToString()) < new TimeSpan(4, 40, 0))
                    {
                        //对每一班组的生产过程数据进行统计分析计算     
                        QltRecoder.insertQuaReport(System.DateTime.Now.AddHours(-8));
                      //  insertQuaReport(System.DateTime.Now.AddHours(-8));
                    }
                }
            }
        }
#endregion

        #region 生产报表记录       


        protected void InsertProdReport(object o)
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataSet times = opt.CreateDataSetOra("select date_begin  as time ,team_code from ht_prod_schedule where substr(date_begin,1,10) = '" + System.DateTime.Now.ToString("yyyy-MM-dd") + "' union select date_end as time,team_code  from ht_prod_schedule where substr(date_end,1,10) ='" + System.DateTime.Now.ToString("yyyy-MM-dd") + "' union select starttime  as time ,''  from ht_prod_report where substr(starttime,1,10) = '" + System.DateTime.Now.ToString("yyyy-MM-dd") + "' union select endtime  as time,''  from ht_prod_report where substr(starttime,1,10) = '" + System.DateTime.Now.ToString("yyyy-MM-dd") + "'");
            if (times != null && times.Tables[0].Rows.Count > 0)
            {
                try
                {
                   
                    foreach (DataRow time in times.Tables[0].Rows)
                    {
                        if (System.DateTime.Now - Convert.ToDateTime(time["time"].ToString()) >= new TimeSpan(0, 0, -20) && System.DateTime.Now - Convert.ToDateTime(time["time"].ToString()) <= new TimeSpan(0, 1, 20))
                            ProdRecoder.ProdRecord(time["time"].ToString(),time["team_code"].ToString());
                    }
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
        }
        #endregion

     




    }
}

