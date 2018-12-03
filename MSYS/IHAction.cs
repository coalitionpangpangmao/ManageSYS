
namespace MSYS
{
    using MSYS.DAL;
    using System.Collections.Generic;
    using System;
    using System.Data;
    public partial class IHAction
    {
        //获取计算结果

        public enum TimeSegType { BEGIN, END, BOTH, ALL };
        public struct TimeSeg
        {
            public string starttime;
            public string endtime;
            public TimeSegType type;//BEGIN 只有任务头，END 只有任务尾，BOTH 头尾都有，ALL生产进行中
            public string planno;
            public string nodecode;
            public TimeSeg(string a, string b, TimeSegType c, string d, string e)
            {
                this.starttime = a;
                this.endtime = b;
                this.type = c;
                this.planno = d;
                this.nodecode = e;

            }
        };
        public struct Gaptime
        {
            public string starttime;
            public string endtime;
            public int gaptime;
            public Gaptime(string a, string b, int c)
            {
                this.starttime = a;
                this.endtime = b;
                this.gaptime = c;
            }
        }
        public struct ParaInfo
        {
            public string timestamp;
            public double value;
          //  public string status;
        }
        public struct ParaRes
        {
            public string timestamp;
            public double value;
            public string status;
        }
        private List<Gaptime> gaptime = null;
   
        public IHAction()
        {

        }
        /// <summary>
        /// 获取指定时间段的数据，由于IH所限，按每两小时进行循环读取将数据全部取出来
        /// </summary>
        /// <param name="tagname">IH标签值</param>
        /// <param name="interval">时间间隔</param>
        /// <param name="Btime">开始时间</param>
        /// <param name="Etime">结束时间</param>
        /// <returns>读取数据列表</returns>
        protected List<ParaInfo> getParaRecord(string tagname,string interval,DateTime Btime, DateTime Etime)
        {
            if (DateTime.Compare(Btime, Etime) > 0)
                return null;
            List<ParaInfo> paralist = new List<ParaInfo>();
            MSYS.DAL.DbOperator ihopt = new MSYS.DAL.DbOperator(new MSYS.DAL.OledbOperator());
                DateTime starttime = Btime;
            DateTime stoptime = Etime;
            if (Etime - Btime > TimeSpan.FromHours(2))            
            { 
                starttime = Btime;
                stoptime = Btime.AddHours(2);
            }
            do
            {
                string query = "SELECT  timestamp ,value  FROM ihrawdata where tagname = '" + tagname + "' and timestamp between '" + starttime.ToString("yyyy/MM/dd HH:mm:ss") + "' and '" + stoptime.ToString("yyyy/MM/dd HH:mm:ss") + "' and intervalmilliseconds =  " + interval + "s order by timestamp ASC";
                DataSet data = ihopt.CreateDataSetOra(query);
                if (data != null && data.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        ParaInfo info = new ParaInfo();
                        info.timestamp = row["timestamp"].ToString();
                        info.value =Convert.ToDouble( row["value"].ToString());
                        paralist.Add(info);
                    }
                }
                starttime = stoptime;
                stoptime = starttime.AddHours(2);
                if (DateTime.Compare(stoptime, Etime) > 0)
                    stoptime = Etime;
            }
            while (Etime - starttime > TimeSpan.FromHours(2));
           
            return paralist;
        }

        /// <summary>
        /// 重载方法 获取指定时间段的数据，由于IH所限，按每两小时进行循环读取将数据全部取出来
        /// </summary>
        /// <param name="para_code">工艺点编码</param>
        /// <param name="Btime">开始时间</param>
        /// <param name="Etime">结束时间</param>
        /// <returns></returns>
        protected List<ParaInfo> getParaRecord(string para_code, DateTime Btime, DateTime Etime)
        {
            DbOperator opt = new DbOperator();
            DataSet data = opt.CreateDataSetOra("select t.value_tag,r.gap_hdelay,r.gap_tdelay,r.ctrl_point,r.is_gap_judge,r.periodic,r.rst_value,r.gap_time,r.head_delay,r.tail_delay,r.batch_head_delay,r.batch_tail_delay from   HT_QLT_COLLECTION  r left join ht_pub_tech_para t on r.para_code = t.para_code where r.PARA_CODE = '" + para_code + "'");
            if (data == null || data.Tables[0].Rows.Count == 0)
                return null;
            DataRow drow = data.Tables[0].Rows[0];
            ////////////////////工艺点标签//////////////////////////////////////////////
            string tagname = drow["value_tag"].ToString();
            string interval = drow["periodic"].ToString();           
            if (DateTime.Compare(Btime, Etime) > 0)
                return null;
            List<ParaInfo> paralist = new List<ParaInfo>();
            MSYS.DAL.DbOperator ihopt = new MSYS.DAL.DbOperator(new MSYS.DAL.OledbOperator());
            DateTime starttime = Btime;
            DateTime stoptime = Etime;
            if (Etime - Btime > TimeSpan.FromHours(2))
            {
                starttime = Btime;
                stoptime = Btime.AddHours(2);
            }
            do
            {
                string query = "SELECT  timestamp ,value  FROM ihrawdata where tagname = '" + tagname + "' and timestamp between '" + starttime.ToString("yyyy/MM/dd HH:mm:ss") + "' and '" + stoptime.ToString("yyyy/MM/dd HH:mm:ss") + "' and intervalmilliseconds =  " + interval + "s order by timestamp ASC";
                data = ihopt.CreateDataSetOra(query);
                if (data != null && data.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        ParaInfo info = new ParaInfo();
                        info.timestamp = row["timestamp"].ToString();
                        info.value = Convert.ToDouble(row["value"].ToString());
                        paralist.Add(info);
                    }
                }
                starttime = stoptime;
                stoptime = starttime.AddHours(2);
                if (DateTime.Compare(stoptime, Etime) > 0)
                    stoptime = Etime;
            }
            while (Etime - starttime > TimeSpan.FromHours(2));

            return paralist;
        }

        /// <summary>
        /// //查取时间段内是否有任务报告记录
        /// </summary>
        /// <param name="Btime"></param>
        /// <param name="Etime"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public int TaskShiftNum(string Btime, string Etime, string section)
        {
            DbOperator opt = new DbOperator();
            DataSet data = opt.CreateDataSetOra("select count(rowid) from ht_prod_report t where t.section_code = '" + section + "' and STARTTIME between '" + Btime + "' and '" + Etime + "' or ENDTIME between '" + Btime + "' and '" + Etime + "' or (starttime <= '" + Btime + "' and endtime >= '" + Etime + "')");
            if (data != null && data.Tables[0].Rows.Count > 0)
                return Convert.ToInt16(data.Tables[0].Rows[0][0].ToString());
            else
                return 0;
        }
        /// <summary>
        /// /将一段时间按任务划分为不同的时间段
        /// </summary>
        /// <param name="btime"></param>
        /// <param name="etime"></param>
        /// <param name="nodeid"></param>
        /// <returns></returns>
        public List<TimeSeg> TimeCut(string btime, string etime, string nodeid)
        {
            List<TimeSeg> listTimeseg = new List<TimeSeg>();
            DbOperator opt = new DbOperator();
            string query = "select starttime as rstime, 'b' as tag,PLANNO from ht_prod_report t where t.section_code = '" + nodeid.Substring(0, 5) + "' and STARTTIME between '" + btime + "' and '" + etime + "' union select endtime as rstime,'e' as tag,PLANNO  from ht_prod_report t where t.section_code = '" + nodeid.Substring(0, 5) + "' and endtime between '" + btime + "' and '" + etime + "'  order by rstime";
            DataSet data = opt.CreateDataSetOra(query);
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                {
                    TimeSeg seg;
                    seg.nodecode = nodeid;
                    if (i == 0)
                    {
                        if (data.Tables[0].Rows[i]["tag"].ToString() == "e")
                        {
                            seg.starttime = btime;
                            seg.endtime = data.Tables[0].Rows[i]["rstime"].ToString();
                            seg.planno = data.Tables[0].Rows[i]["PLANNO"].ToString();
                            seg.type = TimeSegType.END;
                            listTimeseg.Add(seg);
                        }
                        else
                        {
                            seg.starttime = data.Tables[0].Rows[i]["rstime"].ToString();
                            seg.endtime = etime;
                            seg.planno = data.Tables[0].Rows[i]["PLANNO"].ToString();
                            seg.type = TimeSegType.BEGIN;
                            listTimeseg.Add(seg);

                        }

                    }
                    else if (i == data.Tables[0].Rows.Count - 1)
                    {
                        if (data.Tables[0].Rows[i]["tag"].ToString() == "b")
                        {
                            seg.starttime = data.Tables[0].Rows[i]["rstime"].ToString();
                            seg.endtime = etime;
                            seg.planno = data.Tables[0].Rows[i]["PLANNO"].ToString();
                            seg.type = TimeSegType.BEGIN;
                            listTimeseg.Add(seg);

                        }

                    }
                    else
                    {
                        if (data.Tables[0].Rows[i - 1]["tag"].ToString() == "e" && data.Tables[0].Rows[i]["tag"].ToString() == "b")
                            continue;
                        if (data.Tables[0].Rows[i - 1]["tag"].ToString() == "b" && data.Tables[0].Rows[i]["tag"].ToString() == "e")
                        {
                            seg.starttime = data.Tables[0].Rows[i - 1]["rstime"].ToString();
                            seg.endtime = data.Tables[0].Rows[i]["rstime"].ToString();
                            seg.planno = data.Tables[0].Rows[i]["PLANNO"].ToString();
                            seg.type = TimeSegType.BOTH;
                            listTimeseg.Add(seg);

                        }
                    }
                }

            }
            else
            {
                query = "select * from ht_prod_report where (starttime < '" + btime + "' and  endtime > '" + etime + "') or (starttime < '" + btime + "' and  endtime is null)";
                data = opt.CreateDataSetOra(query);
                if (data != null && data.Tables[0].Rows.Count > 0)
                {
                    TimeSeg seg = new TimeSeg(btime, etime, TimeSegType.ALL, data.Tables[0].Rows[0]["PLANNO"].ToString(), nodeid);
                    listTimeseg.Add(seg);
                }

            }

            return listTimeseg;
        }

        public TimeSeg GetTimeSeg(string btime, string etime, string nodeid, string planno)//显示一段时间内某一任务号某一批次数据对应时间段    
        {
            TimeSeg seg;
            seg.starttime = btime;
            seg.endtime = etime;
            seg.nodecode = nodeid;
            seg.planno = planno;
            DbOperator opt = new DbOperator();
            string query = "select starttime as rstime, 'b' as tag,PLANNO from ht_prod_report t where t.section_code = '" + nodeid.Substring(0, 5) + "' and planno = '" + planno + "' and STARTTIME between '" + btime + "' and '" + etime + "' union select endtime as rstime,'e' as tag,PLANNO  from ht_prod_report t where t.section_code = '" + nodeid.Substring(0, 5) + "' and planno = '" + planno + "' and  endtime between '" + btime + "' and '" + etime + "' order by rstime";
            DataSet data = opt.CreateDataSetOra(query);
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                if (data.Tables[0].Rows.Count == 1)
                {
                    if (data.Tables[0].Rows[0]["tag"].ToString() == "e")
                    {
                        seg.endtime = data.Tables[0].Rows[0]["rstime"].ToString();
                        seg.type = TimeSegType.END;
                    }
                    else
                    {
                        seg.starttime = data.Tables[0].Rows[0]["rstime"].ToString();
                        seg.type = TimeSegType.BEGIN;
                    }
                }
                else
                {
                    seg.starttime = data.Tables[0].Rows[0]["rstime"].ToString();
                    seg.endtime = data.Tables[0].Rows[1]["rstime"].ToString();
                    seg.type = TimeSegType.BOTH;
                }

            }
            else
            {
                seg.type = TimeSegType.ALL;
            }

            return seg;
        }

      
        public List<Gaptime> GetGapTime(List<ParaInfo> paralist, string tagvalue, int headdelay, int taildelay, int gaptime)//获取两段时间间的断流信息
        {
            if (paralist == null || paralist.Count < 20)
                return null;
            List<Gaptime> gaplist = null;
            string starttime = paralist.ToArray()[0].timestamp;
            string endtime = paralist.ToArray()[paralist.Count - 1].timestamp;
          List<ParaInfo> reslist =  paralist.FindAll(s => s.value < Convert.ToDouble(tagvalue));
            if (reslist != null && reslist.Count > gaptime/5)
            {
                gaplist = new List<Gaptime>();
                while (string.Compare(starttime,endtime)<0)
                {
                    string GapStarttime = "";
                    string GapEndtime = "";
                  
                    GapStarttime = reslist.ToArray()[0].timestamp;
                    GapEndtime = paralist.Find(s => String.Compare(s.timestamp, GapStarttime) > 0 && s.value > Convert.ToDouble(tagvalue)).timestamp;
                   
                    if (Convert.ToDateTime(GapEndtime) - Convert.ToDateTime(GapStarttime) >TimeSpan.FromSeconds(gaptime))
                    {                       
                            Gaptime gap;
                            gap.starttime = Convert.ToDateTime(GapStarttime).AddSeconds(headdelay).ToString("yyyy-MM-dd HH:mm:ss");
                            gap.endtime = Convert.ToDateTime(GapEndtime).AddSeconds(taildelay).ToString("yyyy-MM-dd HH:mm:ss");
                            gap.gaptime = (Convert.ToDateTime(GapEndtime) - Convert.ToDateTime(GapStarttime)).Seconds; 
                            gaplist.Add(gap);
                      
                        starttime = Convert.ToDateTime(GapEndtime).AddSeconds(taildelay).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        starttime = endtime;
                    }

                }
            }
            return gaplist;
        }
        public List<Gaptime> gaptimes()
        {
            return gaptime;
        }
        public List<ParaRes> GetIHOrgDataSet(TimeSeg seg)
        {
            #region    read collection condtion
            DbOperator opt = new DbOperator();
            DataSet data = opt.CreateDataSetOra("select t.value_tag,r.gap_hdelay,r.gap_tdelay,r.ctrl_point,r.is_gap_judge,r.periodic,r.rst_value,r.gap_time,r.head_delay,r.tail_delay,r.batch_head_delay,r.batch_tail_delay from   HT_QLT_COLLECTION  r left join ht_pub_tech_para t on r.para_code = t.para_code where r.PARA_CODE = '" + seg.nodecode + "'");
            if (data == null || data.Tables[0].Rows.Count == 0)
                return null;
            DataRow row = data.Tables[0].Rows[0];
            ////////////////////工艺点标签//////////////////////////////////////////////
            string tagname = row["value_tag"].ToString();
            string interval = row["periodic"].ToString();
            int timegap = Convert.ToInt32(row["gap_time"].ToString());
            string tailRst = row["rst_value"].ToString();
            ////////////////////是否为工艺段断流判定点/////////////////////////////////////////////
            bool gapctrl = ("1" == row["is_gap_judge"].ToString());

            //////////////////////偏移/////////////////////////////////////////////               
            int headDelay = Convert.ToInt32(row["head_delay"].ToString());//料头
            int tailDelay = Convert.ToInt32(row["tail_delay"].ToString());//料尾
            int batchheadDelay = Convert.ToInt32(row["batch_head_delay"].ToString());//批头
            int batchtailDelay = Convert.ToInt32(row["batch_tail_delay"].ToString());//批尾
            int gap_hdelay = Convert.ToInt32(row["gap_hdelay"].ToString());//断流前
            int gap_tdelay = Convert.ToInt32(row["gap_tdelay"].ToString());//断流后
            #endregion
            #region   //////////////////////寻找数据批头尾、料头尾  以及断流信息///////////////////////////////////////////////////////////////////////
         
            string batchBtime = "", batchEtime = "";//电子秤参与后判定的批头批尾时间
            string tailBtime = "", tailEtime = "";//电子秤参与后判定的料头料尾时间
            List<ParaInfo> paralist = getParaRecord(tagname, "5", Convert.ToDateTime(seg.starttime), Convert.ToDateTime(seg.endtime));
            if (gapctrl)
            {
                if (tagname != "" && tailRst != "" && interval != "")
                {

                    batchBtime = tailBtime = seg.starttime;//初始化料头时间
                    batchEtime = tailEtime = seg.endtime;//初始化料尾时间
                  
                    /////////自动寻找批头批尾/////////
                    if (seg.type == TimeSegType.BEGIN || seg.type == TimeSegType.BOTH)//寻找批头
                    {

                        batchBtime = Convert.ToDateTime(paralist.Find(s => s.value < Convert.ToDouble(tailRst)).timestamp).AddSeconds(batchheadDelay).ToString("yyyy-MM-dd HH:mm:ss") ;                     
                    }
                    if (seg.type == TimeSegType.END || seg.type == TimeSegType.BOTH)//寻找批尾
                    {
                      
                        double temp = paralist.ToArray()[paralist.Count-1].value;

                        if (temp >= Convert.ToDouble(tailRst))                        
                        {
                            paralist = getParaRecord(tagname,"5", Convert.ToDateTime(seg.starttime), Convert.ToDateTime(seg.endtime).AddHours(1));
                            batchEtime = Convert.ToDateTime(paralist.Find(s => s.value > Convert.ToDouble(tailRst)).timestamp).AddSeconds(batchtailDelay).ToString("yyyy-MM-dd HH:mm:ss") ;  
                        }
                        else
                            batchEtime = Convert.ToDateTime(paralist.FindLast(s => s.value > Convert.ToDouble(tailRst)).timestamp).AddSeconds(batchtailDelay).ToString("yyyy-MM-dd HH:mm:ss");
                        
                    }
                    ////////////////////////////////自动寻找料头料尾////////////////////////////////////////////////////////////////
                    if (seg.type == TimeSegType.BEGIN || seg.type == TimeSegType.BOTH)//寻找料头
                    {                     
                        tailBtime = Convert.ToDateTime(paralist.Find(s => s.value > Convert.ToDouble(tailRst) &&String.Compare( s.timestamp , batchBtime) >=0 &&String.Compare( s.timestamp , batchEtime) <=0).timestamp).AddSeconds(headDelay).ToString("yyyy-MM-dd HH:mm:ss") ; 
                    }
                    if (seg.type == TimeSegType.END || seg.type == TimeSegType.BOTH)//寻找料尾
                    {                       
                        tailEtime = Convert.ToDateTime(paralist.FindLast(s => s.value > Convert.ToDouble(tailRst) && String.Compare(s.timestamp, batchBtime) > 00 && String.Compare(s.timestamp, batchEtime) <= 0).timestamp).AddSeconds(tailDelay).ToString("yyyy-MM-dd HH:mm:ss"); 
                    }
                    string[] bseg = { "ORDERNO", "PLANNO", "STARTTIME", "ENDTIME", "SECTION_CODE", "BATCH_BTIME", "BATCH_ETIME", "GAP_BTIME", "GAP_ETIME", "TYPE", };
                    string[] bvalue = { "0", seg.planno, seg.starttime, seg.endtime, seg.nodecode.Substring(0, 5), batchBtime, batchEtime, tailBtime, tailEtime, "0" };
                    opt.MergeInto(bseg, bvalue, 5, "HT_QLT_GAP_COLLECTION");
                    ////////////////////选取数据/判断是否断料,并记录下相应的断料时间//////              
                   
                    gaptime = GetGapTime(paralist, tailRst, gap_hdelay, gap_tdelay, timegap);
                    int orderno = 1;
                    string[] gapseg = { "ORDERNO", "PLANNO", "STARTTIME", "ENDTIME", "SECTION_CODE", "GAPTIME ", "GAP_BTIME", "GAP_ETIME", "TYPE" };
                    if (gaptime != null)
                    {
                        foreach (Gaptime time in gaptime)
                        {
                            string[] gapvalue = { (orderno++).ToString(), seg.planno, seg.starttime, seg.endtime, seg.nodecode.Substring(0, 5), time.gaptime.ToString(), time.starttime, time.endtime, "1" };
                            opt.MergeInto(gapseg, gapvalue, 5, "HT_QLT_GAP_COLLECTION");
                        }
                    }
                }
                else
                    return null;
            }
            else
            {
                //如果不是断流判定点，则读取该工艺段判定点的信息，获取批头批尾、料头料尾，以及断流区间信息
                string ctrlpoint = row["ctrl_point"].ToString();
                if (ctrlpoint != "")
                {
                    DataSet gapdata = opt.CreateDataSetOra("select * from HT_QLT_GAP_COLLECTION where TYPE = '1' and  PLANNO = '" + seg.planno + "' and STARTTIME = '" + seg.starttime + "' and ENDTIME = '" + seg.endtime + "' and SECTION_CODE = '" + ctrlpoint.Substring(0, 5) + "' ");
                    if (gapdata != null && gapdata.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow grow in gapdata.Tables[0].Rows)
                        {
                            Gaptime time = new Gaptime();
                            time.gaptime = Convert.ToInt32(grow["gaptime"].ToString());
                            time.starttime = Convert.ToDateTime(grow["STARTTIME"].ToString()).AddSeconds(gap_hdelay).ToString("yyyy-MM-dd HH:mm:ss");
                            time.endtime = Convert.ToDateTime(grow["ENDTIME"].ToString()).AddSeconds(gap_tdelay).ToString("yyyy-MM-dd HH:mm:ss");
                            if (gaptime == null)
                                gaptime = new List<Gaptime>();
                            gaptime.Add(time);
                        }
                    }
                    gapdata = opt.CreateDataSetOra("select * from HT_QLT_GAP_COLLECTION where TYPE = '0' and  PLANNO = '" + seg.planno + "' and STARTTIME = '" + seg.starttime + "' and ENDTIME = '" + seg.endtime + "' and SECTION_CODE = '" + ctrlpoint.Substring(0, 5) + "' ");
                    if (gapdata != null && gapdata.Tables[0].Rows.Count > 0)
                    {
                        DataRow brow = gapdata.Tables[0].Rows[0];
                        batchBtime = brow["BATCH_BTIME"].ToString();
                        batchEtime = brow["BATCH_ETIME"].ToString();
                        tailBtime = brow["GAP_BTIME"].ToString();
                        tailEtime = brow["GAP_ETIME"].ToString();
                    }
                }
            }
            #endregion

            #region 选择数据
            if (batchBtime == "" || batchEtime == "")
                return null;
            if(string.Compare(seg.endtime,batchEtime)<0)
                paralist =  getParaRecord(tagname, "5", Convert.ToDateTime(batchBtime), Convert.ToDateTime(batchEtime));
           
            if (paralist != null && paralist.Count >20)
            {
               
                int count = paralist.Count;
                List<ParaRes> reslist = new List<ParaRes>();
                int countN = 0;
                foreach (ParaInfo res in paralist)
                {

                    if (countN == Convert.ToInt16( interval) / 5)
                    {
                        ParaRes para = new ParaRes();
                        para.timestamp = res.timestamp;
                        para.value = res.value;
                        string tempstr = Convert.ToDateTime(res.timestamp).ToString("yyyy-MM-dd HH:mm:ss");
                        if (string.Compare(tempstr, tailBtime) < 0)
                        {
                            para.status = "料头";
                        }
                        else if (tailEtime != seg.starttime && string.Compare(tempstr, tailEtime) > 0)
                        {
                            para.status = "料尾";
                        }
                        else
                        {
                            para.status = "过程值";
                            if (gaptime != null)
                            {
                                int h = 0;
                                while (h < gaptime.Count)
                                {
                                    if (string.Compare(tempstr, ((Gaptime)gaptime[h]).starttime) > 0 && string.Compare(tempstr, ((Gaptime)gaptime[h]).endtime) < 0)
                                        para.status = "断流值";
                                    h++;
                                }
                            }
                        }
                        countN = 0;
                        reslist.Add(para);
                    }
                    else
                        countN++;
                }
                return reslist;
            }
            else
                return null;
            #endregion
        }

        public List<ParaInfo> GetData(string btime, string etime, string nodeid, string planno)
        {
            TimeSeg seg = GetTimeSeg(btime, etime, nodeid, planno);
            List<ParaInfo> paralist = new List<ParaInfo>();
            paralist = getParaRecord(nodeid,Convert.ToDateTime(seg.starttime),Convert.ToDateTime( seg.endtime));
            return paralist;
           
        }

        public List<ParaInfo> GetData(string btime, string etime, string nodeid)
        {
            
            List<ParaInfo> paralist = new List<ParaInfo>();
            paralist = getParaRecord(nodeid, Convert.ToDateTime(btime), Convert.ToDateTime(etime));
            return paralist;
        }

        public int GetGroupCount(double[] Samples)//根据采样点的数量，设定分组组数
        {
            int Datacount = Samples.Length;
            int GroupCount;
            if (Datacount <= 50)
                GroupCount = 5;
            else if (Datacount > 50 && Datacount <= 100)
                GroupCount = 10;
            else if (Datacount > 100 && Datacount <= 250)
                GroupCount = 15;
            else
                GroupCount = 20;
            return GroupCount;
        }
        public double[][] GetGroup(double[] Samples)//对采样点进行分组
        {
            int m = GetGroupCount(Samples);
            int n = (int)(Samples.Length / m + 1);
            double[][] SampleByGroup = new double[m][];
            for (int i = 0; i < m; i++)
            {
                SampleByGroup[i] = new double[n];
            }
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i * n + j < Samples.Length)
                        SampleByGroup[i][j] = Samples[i * n + j];
                    else
                        SampleByGroup[i][j] = SampleByGroup[i - 1][j];
                }
            }
            return SampleByGroup;
        }
    }
}


