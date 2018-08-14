using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using MSYS.Common;

using System.Diagnostics;
namespace DataCollect
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(InsertQuaReport);  //到达时间的时候执行事件；
            
            aTimer.Interval = 120 * 60 * 1000;
            aTimer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            aTimer.Enabled = true; //是否执行System.Timers.Timer.Elapsed事件；
            Qua_Start.Enabled = false;
        }

        public void Qua_Start_Click(object sender, EventArgs e)
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(InsertQuaReport);  //到达时间的时候执行事件；
            // 设置引发时间的时间间隔 此处设置为1秒（1000毫秒） 
            aTimer.Interval = 120 * 60 * 1000;
            aTimer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            aTimer.Enabled = true; //是否执行System.Timers.Timer.Elapsed事件；
            Qua_Start.Enabled = false;

            //  InsertQuaReport();
        }

        public void Qua_show_Click(object sender, EventArgs e)
        {
            string endtime = Convert.ToDateTime(QuaTime.Text).AddDays(1).ToString("yyyy-MM-dd") + " 00:00:00";
            string starttime = Convert.ToDateTime(QuaTime.Text).ToString("yyyy-MM-dd") + " 00:00:00";
            string query = "select * from HT_QLT_DATA_RECORD where B_TIME >= '" + starttime + "' and  E_TIME <= '" + endtime + "'";
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataSet data = opt.CreateDataSetOra(query);
            dataGridView1.DataSource = data.Tables[0];

        }


        private void InsertQuaReport(object source, System.Timers.ElapsedEventArgs e)
        {
            string datetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataSet data = opt.CreateDataSetOra("select * from ht_prod_schedule t  where date_begin <='" + datetime + "' and date_end >= '" + datetime + "'");
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                DataRow row = data.Tables[0].Rows[0];
                string endtime = row["date_begin"].ToString();
                string starttime = row["date_end"].ToString();
                InsertSectionQuaReport(starttime, endtime);
            }

        }

        private void InsertSectionQuaReport(string starttime, string endtime)
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataSet data = opt.CreateDataSetOra("select distinct section_code from ht_pub_tech_section");
            IHDataOpt ihopt = new IHDataOpt();
            foreach (DataRow row in data.Tables[0].Rows)
            {
                //判断该工艺段是否有批次报告记录
                if (ihopt.TaskShiftNum(starttime, endtime, row["section_code"].ToString()) > 0)
                {
                    string query = "select r.para_code from ht_pub_tech_para r where substr(r.para_code,1,5) = '" + row["section_code"].ToString() + "' and r.para_type like '___1_'";
                    DataSet pointsets = opt.CreateDataSetOra(query);
                    foreach (DataRow prow in pointsets.Tables[0].Rows)
                    {
                        insertPointQuaReport(starttime, endtime, prow["para_code"].ToString());
                    }
                }
            }
        }

        private void insertPointQuaReport(string starttime, string endtime, string para_code)
        {
            IHDataOpt ihopt = new IHDataOpt();
            List<IHDataOpt.TimeSeg> timesegs = ihopt.TimeCut(starttime, endtime, para_code);
            foreach (IHDataOpt.TimeSeg seg in timesegs)
            {
                InsertRecord(seg);
            }
        }
        /// <summary>
        /// 插入某工艺点质量统计报告
        /// </summary>
        /// <param name="seg">开始、结束时间、数据点code、计划号</param>     
        protected void InsertRecord(IHDataOpt.TimeSeg seg)
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string teamcode = opt.GetSegValue("select team_code  from ht_prod_schedule where date_begin <='" + seg.starttime + "' and date_end >='" + seg.endtime + "' and work_staus = '1'", "team_code");

            IHDataOpt ihopt = new IHDataOpt();
            //根据开始、结束时间、数据点code、计划号获取原始数据，返回的数据为三列：时间、值、状态；状态为料头、料尾、过程值、断流
            DataTable dt = ihopt.GetIHOrgDataSet(seg);
            string gaptime = "";
            string is_gap = "0";
            List<IHDataOpt.Gaptime> gaplist = ihopt.gaptimes();
            if (gaplist == null) gaptime = "0";
            else
            {
                int timecount = 0;
                foreach (IHDataOpt.Gaptime time in gaplist)
                {
                    timecount += time.gaptime;
                }
                gaptime = timecount.ToString();
            }
            if (gaptime != "0")
                is_gap = "1";
            //////////////////////////////////////////////////////////////////////////////////////////
            double[] samples = null;
            if (dt != null && dt.Rows.Count > 0)
            {

                var Rows = from dt1 in dt.AsEnumerable()
                           where dt1.Field<string>("状态") == "过程值"
                           select dt1;
                DataView rsl = Rows.AsDataView();
                int count = rsl.Count;
                samples = new double[count];
                int i = 0;
                foreach (DataRow row in Rows)
                {
                    samples[i] = Convert.ToDouble(row["值"].ToString());
                    i++;
                }
            }
            ///////////////根据原始数据计算算统计数据///////////////////////////////////////////////////////

            if (samples != null && samples.GetLength(0) > 0)
            {
                DataSet set = opt.CreateDataSetOra("select s.para_code,s.value,s.upper_limit,s.lower_limit from ht_pub_prod_design r left join ht_tech_stdd_code_detail s on r.tech_stdd_code = s.tech_code  where r.prod_code = '" + seg.planno.Substring(8, 7) + "' and s.para_code = '" + seg.nodecode + "'");
                if (set != null && set.Tables[0].Select().GetLength(0) > 0 && set.Tables[0].Select()[0]["value"].ToString() != "" && set.Tables[0].Select()[0]["upper_limit"].ToString() != "" && set.Tables[0].Select()[0]["lower_limit"].ToString() != "")
                {
                    string CL = set.Tables[0].Select()[0]["value"].ToString();
                    string upvalue = set.Tables[0].Select()[0]["upper_limit"].ToString();
                    string lowvalue = set.Tables[0].Select()[0]["lower_limit"].ToString();

                    SPCFunctions spc = new SPCFunctions(samples, Convert.ToDouble(upvalue), Convert.ToDouble(lowvalue));
                    string[] tableseg = { "PLAN_ID", "PROD_CODE", "PARA_CODE", "TEAM", "B_TIME", "E_TIME", "AVG", "COUNT", "MIN", "MAX", "QUANUM", "QUARATE", "UPCOUNT", "UPRATE", "DOWNCOUNT", "DOWNRATE", "STDDEV", "ABSDEV", "IS_GAP", "CPK", "VAR", "RANGE", "GAP_TIME"};
                    string[] tablevalue = { seg.planno, seg.planno.Substring(8, 7), seg.nodecode, teamcode, seg.starttime, seg.endtime, spc.avg.ToString(), spc.count.ToString(), spc.min.ToString(), spc.max.ToString(), spc.passcount.ToString(), spc.passrate.ToString("f4"), spc.upcount.ToString(), spc.uprate.ToString("f4"), spc.downcount.ToString(), spc.downrate.ToString("f4"), spc.stddev.ToString("f4"), spc.absdev.ToString("f4"), is_gap, spc.var.ToString("f4"), spc.Range.ToString("f4"), gaptime };
                    opt.InsertData(tableseg, tablevalue, "HT_QLT_DATA_RECORD");
                }
            }


        }





    }
}

