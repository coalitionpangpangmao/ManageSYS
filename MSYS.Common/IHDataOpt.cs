
namespace MSYS.Common
{
    using MSYS.DAL;
    using System.Collections.Generic;
    using System;
    using System.Data;
    public partial class IHDataOpt
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
        private OledbOperator opt = null;
        private List<Gaptime> gaptime = null;
        public List<Gaptime> getGaptime()
        {
            return gaptime;
        }
        public IHDataOpt()
        {
            opt = new OledbOperator();
        }

        public DataSet CreateDataSetIH(string query)//连接IH服务器，并根据查询SQL获取数据
        {
            return opt.CreateDataSet(query);
        }

        public int TaskShiftNum(string Btime, string Etime, string section)//查取时间段内是否有任务报告记录
        {
            DbOperator opt = new DbOperator();
            DataSet data = opt.CreateDataSetOra("select count(rowid) from ht_prod_report t where t.section_code = '" +section + "' and STARTTIME between '" + Btime + "' and '" + Etime + "' or ENDTIME between '" + Btime + "' and '" + Etime + "' or (starttime <= '" + Btime + "' and endtime >= '" + Etime + "')");
            return Convert.ToInt16(data.Tables[0].Rows[0][0].ToString());
        }

        public List<TimeSeg> TimeCut(string btime, string etime, string nodeid)//将一段时间按任务划分为不同的时间段
        {
            List<TimeSeg> listTimeseg = new List<TimeSeg>();
            DbOperator opt = new DbOperator();
            string query = "select starttime as rstime, 'b' as tag,PLANNO from ht_prod_report t where t.section_code = '" + nodeid.Substring(0,5) + "' and STARTTIME between '" + btime + "' and '" + etime + "' union select endtime as rstime,'e' as tag,PLANNO  from ht_prod_report t where t.section_code = '" + nodeid.Substring(0,5)+ "' and endtime between '" + btime + "' and '" + etime + "'  order by rstime";
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

        public List<Gaptime> GetGapTime(string starttime, string endtime, string tag, string tagvalue, int headdelay, int taildelay,int gaptime)//获取两段时间间的断流信息
        {
            List<Gaptime> gaplist = null;
            string query;
            DataSet data = new DataSet();
            DbOperator opt = new DbOperator();
            query = "SELECT  timestamp,tagname,value  FROM ihrawdata  where  tagname = '" + tag + "' and timestamp between '" + starttime + "' and '" + endtime + "' and intervalmilliseconds =  2s and value < " + tagvalue;
            data = CreateDataSetIH(query);
            if (data != null && data.Tables[0].Select().Length > 30)
            {
                gaplist = new List<Gaptime>();
                while (Convert.ToDateTime(starttime) < Convert.ToDateTime(endtime))
                {
                    string GapStarttime = "";
                    string GapEndtime = "";
                    query = "SELECT  Min(timestamp),tagname  FROM ihrawdata where tagname = '" + tag + "' and timestamp between '" + starttime + "' and '" + endtime + "' and intervalmilliseconds =  2s and value <" + tagvalue + " group by tagname";
                    data = CreateDataSetIH(query);
                    if (data != null && data.Tables[0].Select().Length > 0)
                        GapStarttime = Convert.ToDateTime(data.Tables[0].Rows[0]["Min of timestamp"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    else
                        break;
                    if (GapStarttime != "")
                    {
                        query = "SELECT  Min(timestamp),tagname  FROM ihrawdata where tagname = '" + tag + "' and timestamp between '" + GapStarttime + "' and '" + endtime + "' and intervalmilliseconds =  2s and value >" + tagvalue + " group by tagname";
                        data = CreateDataSetIH(query);
                        if (data != null && data.Tables[0].Select().Length > 0)
                            GapEndtime = Convert.ToDateTime(data.Tables[0].Rows[0]["Min of timestamp"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (GapStarttime != "" && GapEndtime != "")
                    {

                        query = "SELECT  timestamp,tagname,value  FROM ihrawdata  where  tagname = '" + tag + "' and timestamp between '" + GapStarttime + "' and '" + GapEndtime + "' and intervalmilliseconds =  2s and value < " + tagvalue;
                        data = CreateDataSetIH(query);
                        if (data != null && data.Tables[0].Select().Length > gaptime)//只有断流超过一定时间才视为断流成立
                        {
                            Gaptime gap;
                            gap.starttime = Convert.ToDateTime(GapStarttime).AddSeconds(headdelay).ToString("yyyy-MM-dd HH:mm:ss");
                            gap.endtime = Convert.ToDateTime(GapEndtime).AddSeconds(taildelay).ToString("yyyy-MM-dd HH:mm:ss");
                            gap.gaptime = data.Tables[0].Select().Length * 2;
                            gaplist.Add(gap);

                        }
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
        public DataTable GetIHOrgDataSet(TimeSeg seg)
        {            
#region    ///////////读取数据库中的采集条件////////////////////////////////////////////////////////////////////////////////////////////////
            DbOperator opt = new DbOperator();
            DataSet data = opt.CreateDataSetOra("select t.value_tag,r.gap_hdelay,r.gap_tdelay,r.ctrl_point,r.is_gap_judge,r.periodic,r.rst_value,r.gap_time,r.head_delay,r.tail_delay,r.batch_head_delay,r.batch_tail_delay from   HT_QLT_COLLECTION  r left join ht_pub_tech_para t on r.para_code = t.para_code where r.PARA_CODE = '" + seg.nodecode + "'");
            if (data == null || data.Tables[0].Rows.Count == 0)
                return null;
            DataRow row = data.Tables[0].Rows[0];
            ////////////////////工艺点标签//////////////////////////////////////////////
            string tagname = row["value_tag"].ToString();
            string interval = row["periodic"].ToString();
            int timegap = Convert.ToInt32(row["gap_time"].ToString());
            string  tailRst = row["rst_value"].ToString();
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
            string query;
             string batchBtime="", batchEtime="";//电子秤参与后判定的批头批尾时间
                string tailBtime="", tailEtime="";//电子秤参与后判定的料头料尾时间
            if (gapctrl)
            {
                if(tagname != "" && tailRst != "" && interval != "")
                {
               
                    batchBtime =tailBtime = seg.starttime;//初始化料头时间
                    batchEtime = tailEtime = seg.endtime;//初始化料尾时间
                    ///////////////////////////自动寻找批头批尾/////////////////////////////////////////////////
                    if (seg.type == TimeSegType.BEGIN || seg.type == TimeSegType.BOTH)//寻找批头
                    {
                       
                        query = "SELECT  Min(timestamp),tagname  FROM ihrawdata where tagname = '" + tagname + "' and timestamp between '" + seg.starttime + "' and '" + seg.endtime + "'  and value  <" + tailRst + " group by tagname";
                        data = CreateDataSetIH(query);
                        if (data != null && data.Tables[0].Select().Length > 0)
                            batchBtime = Convert.ToDateTime(data.Tables[0].Rows[0]["Min of timestamp"].ToString()).AddSeconds(batchheadDelay).ToString("yyyy-MM-dd HH:mm:ss"); ;//自动获取批头时间，向前推20秒
                    }
                    if (seg.type == TimeSegType.END || seg.type == TimeSegType.BOTH)//寻找批尾
                    {
                        string temptime = Convert.ToDateTime(seg.endtime).AddHours(1).ToString("yyyy-MM-dd HH:mm:ss");
                        DataSet tempdata = CreateDataSetIH("select value from ihrawdata where tagname = '" + tagname + "' and timestamp between '" + seg.endtime + "' and '" + Convert.ToDateTime(seg.endtime).AddSeconds(60).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                        double temp = 0;
                        if (tempdata != null && tempdata.Tables[0].Select().Length > 0)
                            temp = Convert.ToDouble(tempdata.Tables[0].Select()[0][0].ToString());
                        if (temp >= Convert.ToDouble(tailRst))
                            query = "SELECT  Min(timestamp),tagname  FROM ihrawdata where tagname = '" + tagname + "' and timestamp between '" + seg.endtime + "' and '" + temptime + "'  and value  <" + tailRst + " group by tagname";
                        else
                            query = "SELECT  Max(timestamp),tagname  FROM ihrawdata where tagname = '" + tagname + "' and timestamp between '" + tailBtime + "' and '" + seg.endtime + "'  and value  >" + tailRst + " group by tagname";
                        data = CreateDataSetIH(query);
                        if (data != null && data.Tables[0].Select().Length > 0)
                        {
                            if (temp >= Convert.ToDouble(tailRst))
                                batchEtime = Convert.ToDateTime(data.Tables[0].Rows[0]["Min of timestamp"].ToString()).AddSeconds(batchtailDelay).ToString("yyyy-MM-dd HH:mm:ss");//自动获取批尾时间
                            else
                                batchEtime = Convert.ToDateTime(data.Tables[0].Rows[0]["Max of timestamp"].ToString()).AddSeconds(batchtailDelay).ToString("yyyy-MM-dd HH:mm:ss");//自动获取批尾时间
                        }
                    }
                    ////////////////////////////////自动寻找料头料尾////////////////////////////////////////////////////////////////
                    if (seg.type == TimeSegType.BEGIN || seg.type == TimeSegType.BOTH)//寻找料头
                    {
                        query = "SELECT  Min(timestamp),tagname  FROM ihrawdata where tagname = '" + tagname + "' and timestamp between '" + batchBtime + "' and '" + batchEtime + "' and intervalmilliseconds =  " + interval + "s and value  >" + tailRst + " group by tagname";
                        data = CreateDataSetIH(query);
                        if (data != null && data.Tables[0].Select().Length > 0)
                            tailBtime = Convert.ToDateTime(data.Tables[0].Rows[0]["Min of timestamp"].ToString()).AddSeconds(headDelay).ToString("yyyy-MM-dd HH:mm:ss");//经延时料头时间
                    }
                    if (seg.type == TimeSegType.END || seg.type == TimeSegType.BOTH)//寻找料尾
                    {
                        query = "SELECT  Max(timestamp),tagname  FROM ihrawdata where tagname = '" + tagname + "' and timestamp between '" + batchBtime + "' and '" + batchEtime + "' and intervalmilliseconds =  " + interval + "s and value  >" + tailRst + " group by tagname";
                        data = CreateDataSetIH(query);
                        if (data != null && data.Tables[0].Select().Length > 0)
                            tailEtime = Convert.ToDateTime(data.Tables[0].Rows[0]["Max of timestamp"].ToString()).AddSeconds(tailDelay).ToString("yyyy-MM-dd HH:mm:ss");//经延时料尾时间   
                    }
                   string[] bseg = {  "ORDERNO","PLANNO", "STARTTIME", "ENDTIME", "SECTION_CODE", "BATCH_BTIME","BATCH_ETIME","GAP_BTIME", "GAP_ETIME", "TYPE",};
                   string[] bvalue = {"0",seg.planno, seg.starttime, seg.endtime, seg.nodecode.Substring(0, 5), batchBtime,batchEtime,tailBtime,tailEtime,"0"};
                   opt.MergeInto(bseg, bvalue, 5, "HT_QLT_GAP_COLLECTION");
                    ////////////////////选取数据/判断是否断料,并记录下相应的断料时间//////              
                    gaptime = GetGapTime(tailBtime, tailEtime, tagname, tailRst, gap_hdelay, gap_tdelay, timegap);
                    int orderno = 1;
                    string[] gapseg = { "ORDERNO", "PLANNO", "STARTTIME", "ENDTIME", "SECTION_CODE", "GAPTIME ", "GAP_BTIME", "GAP_ETIME", "TYPE"};
                    foreach (Gaptime time in gaptime)
                    {
                        string[] gapvalue = { (orderno++).ToString(), seg.planno, seg.starttime, seg.endtime, seg.nodecode.Substring(0, 5), time.gaptime.ToString(), time.starttime, time.endtime,"1" };
                        opt.MergeInto(gapseg, gapvalue, 5, "HT_QLT_GAP_COLLECTION");
                    }
                }
                 else 
                    return null;
            }
            else
            {
                string ctrlpoint = row["ctrl_point"].ToString();
                DataSet gapdata = opt.CreateDataSetOra("select * from HT_QLT_GAP_COLLECTION where TYPE = '1' and  PLANNO = '" + seg.planno + "' and STARTTIME = '" + seg.starttime + "' and ENDTIME = '" + seg.endtime + "' and SECTION_CODE = '" + ctrlpoint.Substring(0, 5) + "' ");
                if (gapdata != null && gapdata.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow grow in gapdata.Tables[0].Rows)
                    {
                    Gaptime time = new Gaptime();
                    time.gaptime = Convert.ToInt32(grow["gaptime"].ToString());
                    time.starttime =Convert.ToDateTime( grow["startime"].ToString()).AddSeconds(gap_hdelay).ToString("yyyy-MM-dd HH:mm:ss");
                    time.endtime = Convert.ToDateTime(grow["endtime"].ToString()).AddSeconds(gap_tdelay).ToString("yyyy-MM-dd HH:mm:ss");
                    gaptime.Add(time);
                    }
                }
                gapdata = opt.CreateDataSetOra("select * from HT_QLT_GAP_COLLECTION where TYPE = '0' and  PLANNO = '" + seg.planno + "' and STARTTIME = '" + seg.starttime + "' and ENDTIME = '" + seg.endtime + "' and SECTION_CODE = '" + ctrlpoint.Substring(0, 5) + "' ");
                if(gapdata != null && gapdata.Tables[0].Rows.Count > 0)
                {
                    DataRow brow = gapdata.Tables[0].Rows[0];
                    batchBtime = brow["BATCH_BTIME"].ToString();
                    batchEtime = brow["BATCH_ETIME"].ToString();
                     tailBtime = brow["GAP_BTIME"].ToString();
                    tailEtime = brow["GAP_ETIME"].ToString();              
                }
            }
#endregion
#region  //////////////////////////////选择数据///////////////////////////////////////////////////////////////////////////////////////////////
            if(batchBtime != "")
                seg.starttime = batchBtime;
            if(batchEtime!= "")
                seg.endtime = batchEtime;
            query = "SELECT  timestamp as 时间,value as 值  FROM ihrawdata where tagname = '" + tagname + "' and timestamp between '" + seg.starttime + "' and '" + seg.endtime + "' and intervalmilliseconds =  " + interval + "s order by timestamp ASC";
            data = CreateDataSetIH(query);
            if (data != null && data.Tables[0].Select().Length > 0)
            {
                DataRow[] ResRows = data.Tables[0].Select();
                int count = ResRows.Length;
                DataTable ResT = new DataTable();
                ResT = data.Tables[0];
                ResT.Columns.Add("状态");
                foreach (DataRow Res in ResRows)
                {
                    string tempstr = Convert.ToDateTime(Res["时间"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    if (string.Compare(tempstr, tailBtime) < 0)
                    {
                        Res["状态"] = "料头";
                    }
                    else if (tailEtime != seg.starttime && string.Compare(tempstr, tailEtime) > 0)
                    {
                        Res["状态"] = "料尾";
                    }
                    else
                    {
                        Res["状态"] = "过程值";
                        int h = 0;
                        while (h < gaptime.Count)
                        {
                            if (string.Compare(tempstr, ((Gaptime)gaptime[h]).starttime) > 0 && string.Compare(tempstr, ((Gaptime)gaptime[h]).endtime) < 0)
                                Res["状态"] = "断流值";
                            h++;
                        }
                    }
                }
                return ResT;
            }
            else 
                return null;
#endregion
        }

        public DataRowCollection GetData(string btime, string etime, string nodeid, string planno)
        {
            TimeSeg seg = GetTimeSeg(btime, etime, nodeid, planno);
            DbOperator opt = new DbOperator();
            DataSet data = opt.CreateDataSetOra("select t.value_tag,r.periodic from   HT_QLT_COLLECTION  r left join ht_pub_tech_para t on r.para_code = t.para_code where r.PARA_CODE = '" + seg.nodecode + "'");
            if (data == null || data.Tables[0].Rows.Count == 0)
                return null;
            DataRow row = data.Tables[0].Rows[0];
            ////////////////////工艺点标签//////////////////////////////////////////////
            string tagname = row["value_tag"].ToString();
            string interval = row["periodic"].ToString();
          
            string query = "SELECT  timestamp as 时间,value as 值  FROM ihrawdata where tagname = '" + tagname + "' and timestamp between '" + seg.starttime + "' and '" + seg.endtime + "' and intervalmilliseconds =  " + interval + "s order by timestamp ASC";
            data = CreateDataSetIH(query);
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                return data.Tables[0].Rows;
            }
            else return null;
        }

        public DataRowCollection GetData(string btime, string etime, string nodeid)
        {
            DbOperator opt = new DbOperator();
            DataSet data = opt.CreateDataSetOra("select s.value_tag,r.periodic from HT_QLT_COLLECTION r left join ht_pub_tech_para s on s.para_code = r.para_code where r.PARA_CODE = '" + nodeid + "'");
            if (data == null || data.Tables[0].Rows.Count == 0)
                return null;
            DataRow row = data.Tables[0].Rows[0];
            ////////////////////工艺点标签//////////////////////////////////////////////
            string tagname = row["value_tag"].ToString();
            string interval = row["PERIODIC"].ToString();
          

            string query = "SELECT  timestamp,value  FROM ihrawdata where tagname = '" + tagname + "' and timestamp between '" + btime + "' and '" + etime + "' and intervalmilliseconds =  " + interval + "s order by timestamp ASC";
            try
            {
                data = CreateDataSetIH(query);
                if (data != null && data.Tables[0].Rows.Count > 0)
                {
                    return data.Tables[0].Rows;
                }
                else return null;
            }
            catch (Exception)
            {
                return null;
            }
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
   

