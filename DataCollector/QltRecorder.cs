using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics;
using MSYS.DAL;
using MSYS;
using MSYS.Common;
namespace DataCollect
{    
    public class QltRecoder
    {
        public struct PointProperty
        {
            public string para_code;
            public string tag;
        }
        public QltRecoder()
        {
          
        }      

       
      
        public static void insertQuaReport(DateTime date)
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

            // string datetime = System.DateTime.Now.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss");
            string datetime = date.ToString("yyyy-MM-dd HH:mm:ss");
          
            DataSet data = opt.CreateDataSetOra("select * from ht_prod_schedule t  where date_begin <='" + datetime + "' and date_end >= '" + datetime + "'");
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                DataRow row = data.Tables[0].Rows[0];
                string starttime = row["date_begin"].ToString();
             
                string endtime = row["date_end"].ToString();
            
                InsertSectionQuaReport(starttime, endtime);
            }
        }


        //private static void InsertSectionQuaReport(string starttime, string endtime)
        //{
        //    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        //    DataSet data = opt.CreateDataSetOra("select distinct r.section_code ,r.section_name from ht_pub_tech_section r left join ht_pub_tech_para s on substr(s.para_code,1,5) = r.section_code and s.is_del = '0' and s.is_valid = '1' where r.is_del = '0' and r.is_valid = '1' and  s.para_type like '___1%'   order by r.section_code");
        //  //  IHDataOpt ihopt = new IHDataOpt();
        //    IHAction ihopt = new IHAction();
        //    foreach (DataRow row in data.Tables[0].Rows)
        //    {
        //        //判断该工艺段是否有批次报告记录
        //        int count = Convert.ToInt16(opt.GetSegValue("select count(rowid) as count from ht_qlt_data_record t where substr(para_code,1,5) = '" + row["section_code"].ToString() + "' and b_time <= '" + starttime + "' and e_time >= '" + endtime + "'", "count"));
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

        private static void InsertSectionQuaReport(string starttime, string endtime)
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataSet data = opt.CreateDataSetOra("select distinct r.section_code ,r.section_name from ht_pub_tech_section r left join ht_pub_tech_para s on substr(s.para_code,1,5) = r.section_code and s.is_del = '0' and s.is_valid = '1' where r.is_del = '0' and r.is_valid = '1' and  s.para_type like '___1%'   order by r.section_code");           
            IHAction ihopt = new IHAction();
            foreach (DataRow row in data.Tables[0].Rows)
            {
                List<IHAction.SectionTimeSeg> segs = ihopt.sectionTimeCut(starttime, endtime, row["section_code"].ToString());
                foreach (IHAction.SectionTimeSeg seg in segs)
                {
                    int count = Convert.ToInt16(opt.GetSegValue("select count(rowid) as count from ht_qlt_data_record t where substr(para_code,1,5) = '" + row["section_code"].ToString() + "' and b_time <= '" + starttime + "' and e_time >= '" + endtime + "'", "count"));
                    DataSet pointsets = opt.CreateDataSetOra("select r.pathname,s.nodename,t.para_name,t.para_code from ht_pub_path_section r left join ht_pub_path_node s on s.section_code = r.section_code and substr(r.pathcode,s.orders,1) = '1' and s.is_del = '0' left join ht_pub_tech_para t on t.path_node = s.id and t.is_del = '0' where r.section_code = '" + seg.sectioncode + "' and r.is_del = '0' and r.pathcode = '" + seg.pathcode + "' and t.para_type like '___1%' union select r.para_name,r.para_code  from ht_pub_tech_para r where r.para_type like '___1%' and r.is_del = '0' and r.para_code like '" + seg.sectioncode + "%' and r.path_node is null;");
                    if (count < pointsets.Tables[0].Rows.Count)
                    {
                        foreach (DataRow prow in pointsets.Tables[0].Rows)
                        {
                            insertPointQuaReport(seg, prow["para_code"].ToString());
                        }
                    }
                }
            }
           
        }

        private static void insertPointQuaReport(string starttime, string endtime, string para_code)
        {         
            IHAction ihopt = new IHAction();
            List<IHAction.TimeSeg> timesegs = ihopt.TimeCut(starttime, endtime, para_code);
            foreach (IHAction.TimeSeg seg in timesegs)
            {
                InsertRecord(seg);
            }
        }

        private static void insertPointQuaReport(IHAction.SectionTimeSeg seg, string para_code)
        {
            IHAction ihopt = new IHAction();
            IHAction.TimeSeg timeseg = new IHAction.TimeSeg(seg.starttime, seg.endtime, seg.type, seg.planno, para_code);
            InsertRecord(timeseg);
           
        }
        /// <summary>
        /// 插入某工艺点质量统计报告
        /// </summary>
        /// <param name="seg">开始、结束时间、数据点code、计划号</param>     
        protected static void InsertRecord(MSYS.IHAction.TimeSeg seg)
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            int recordnum = Convert.ToInt16(opt.GetSegValue("select count(rowid) as count  from ht_qlt_data_record  where plan_id = '" + seg.planno + "' and para_code = '" + seg.nodecode + "'  and b_time = '" + seg.starttime + "' and e_time = '" + seg.endtime + "'", "count"));
            //if (recordnum > 0)
            //    return;

            string teamcode = opt.GetSegValue("select team_code  from ht_prod_schedule where date_begin <='" + seg.starttime + "' and date_end >='" + seg.endtime + "' and work_staus = '1'", "team_code");

            IHAction ihopt = new IHAction();
            //根据开始、结束时间、数据点code、计划号获取原始数据，返回的数据为三列：时间、值、状态；状态为料头、料尾、过程值、断流
            List<IHAction.ParaRes> dt  = ihopt.GetIHOrgDataSet(seg);
            if (dt == null)
                return;
            string gaptime = "";
            string is_gap = "0";
            List<IHAction.Gaptime> gaplist = ihopt.gaptimes();
            if (gaplist == null) gaptime = "0";
            else
            {
                int timecount = 0;
                foreach (IHAction.Gaptime time in gaplist)
                {
                    timecount += time.gaptime;
                }
                gaptime = timecount.ToString();
            }
            if (gaptime != "0")
                is_gap = "1";
            //////////////////////////////////////////////////////////////////////////////////////////
            double[] samples = null;
            if (dt != null && dt.Count > 0)
            {

                List<IHAction.ParaRes> Rows = dt.FindAll(s=>s.status == "过程值");

                samples = Rows.Select(s => s.value).ToArray();
               
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
                    string[] tableseg = { "PLAN_ID", "PROD_CODE", "PARA_CODE", "TEAM", "B_TIME", "E_TIME", "AVG", "COUNT", "MIN", "MAX", "QUANUM", "QUARATE", "UPCOUNT", "UPRATE", "DOWNCOUNT", "DOWNRATE", "STDDEV", "ABSDEV", "IS_GAP", "CPK", "VAR", "RANGE", "GAP_TIME" };
                    string[] tablevalue = { seg.planno, seg.planno.Substring(8, 7), seg.nodecode, teamcode, seg.starttime, seg.endtime, spc.avg.ToString(), spc.count.ToString(), spc.min.ToString(), spc.max.ToString(), spc.passcount.ToString(), spc.passrate.ToString("f4"), spc.upcount.ToString(), spc.uprate.ToString("f4"), spc.downcount.ToString(), spc.downrate.ToString("f4"), spc.stddev.ToString("f4"), spc.absdev.ToString("f4"), is_gap, spc.Cpk.ToString("f4"), spc.var.ToString("f4"), spc.Range.ToString("f4"), gaptime };
                    opt.MergeInto(tableseg, tablevalue, 6, "HT_QLT_DATA_RECORD");
                }
            }


        }
      
    }
}
