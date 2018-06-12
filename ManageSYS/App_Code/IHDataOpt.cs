using MSYS.DAL;
using System.Collections;
using System;
using System.Data;
using System.Drawing;

public partial class IHDataOpt
{
    //获取计算结果
   

    public struct TimeSeg
    {
        public string starttime;
        public string endtime;
        public string type;//BEGIN 只有任务头，END 只有任务尾，BOTH 头尾都有，ALL生产进行中
        public string planno;
        public string nodecode;
        public TimeSeg(string a, string b, string c, string d, string e)
        {
            this.starttime = a;
            this.endtime = b;
            this.type = c;
            this.planno = d;
            this.nodecode = e;

        }
    };
    struct Gaptime
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
    public IHDataOpt()
    {
        opt = new OledbOperator();
    }

    public DataSet CreateDataSetIH(string query)//连接IH服务器，并根据查询SQL获取数据
    {
        return opt.CreateDataSet(query);
    }

    public int TaskShiftNum(string Btime, string Etime, string nodeid)//查取时间段内是否有任务报告记录
    {
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra("select count(rowid) from ht_prod_report t where t.section_code = '" + nodeid.Substring(0, 5) + "' and STARTTIME between '" + Btime + "' and '" + Etime + "' or ENDTIME between '" + Btime + "' and '" + Etime + "'");
        return Convert.ToInt16(data.Tables[0].Rows[0][0].ToString());

    }

    public ArrayList TimeCut(string btime, string etime, string nodeid)//将一段时间按任务划分为不同的时间段
    {
        ArrayList listTimeseg = new ArrayList();
       DataBaseOperator opt =new DataBaseOperator();
        string query = "select starttime as rstime, 'b' as tag,PLANNO from ht_prod_report t where t.section_code = '' and STARTTIME between '" + btime + "' and '" + etime + "' union select endtime as rstime,'e' as tag,PLANNO  from ht_prod_report t where t.section_code = '' and endtime between '" + btime + "' and '" + etime + "' order by rstime";
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                TimeSeg seg;
                seg.nodecode = nodeid;
                if (i == 0)
                {
                    if (data.Tables[0].Rows[i - 1]["tag"].ToString() == "e")
                    {
                        seg.starttime = btime;
                        seg.endtime = data.Tables[0].Rows[i]["rstime"].ToString();
                        seg.planno = data.Tables[0].Rows[i]["PLANNO"].ToString();
                        seg.type = "END";
                        listTimeseg.Add(seg);
                    }
                    else
                    {
                        seg.starttime = data.Tables[0].Rows[i]["rstime"].ToString();
                        seg.endtime = etime;
                        seg.planno = data.Tables[0].Rows[i]["PLANNO"].ToString();
                        seg.type = "BEGIN";
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
                        seg.type = "BEGIN";
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
                        seg.type = "BOTH";
                        listTimeseg.Add(seg);

                    }
                }
            }

        }
        else
        {
            query = "select * from ht_prod_report where (starttime < '" + btime + "' and  endtime > '" + etime + "') or (starttime < '" + btime + "' and  endtime is null'";
            data = opt.CreateDataSetOra(query);
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                TimeSeg seg = new TimeSeg(btime, etime, "ALL", data.Tables[0].Rows[0]["PLANNO"].ToString(), nodeid);
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
       DataBaseOperator opt =new DataBaseOperator();
        string query = "select starttime as rstime, 'b' as tag,PLANNO from ht_prod_report t where t.section_code = '" + nodeid.Substring(0, 5) + "' and planno = '" + planno + "' and STARTTIME between '" + btime + "' and '" + etime + "' union select endtime as rstime,'e' as tag,PLANNO  from ht_prod_report t where t.section_code = '" + nodeid.Substring(0, 5) + "' and planno = '" + planno + "' and  endtime between '" + btime + "' and '" + etime + "' order by rstime";
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            if (data.Tables[0].Rows.Count == 1)
            {
                if (data.Tables[0].Rows[0]["tag"].ToString() == "e")
                {
                    seg.endtime = data.Tables[0].Rows[0]["rstime"].ToString();
                    seg.type = "END";
                }
                else
                {
                    seg.starttime = data.Tables[0].Rows[0]["rstime"].ToString();
                    seg.type = "BEGIN";
                }
            }
            else
            {
                seg.starttime = data.Tables[0].Rows[0]["rstime"].ToString();
                seg.endtime = data.Tables[0].Rows[1]["rstime"].ToString();
                seg.type = "BOTH";
            }

        }
        else
        {
            seg.type = "ALL";
        }

        return seg;
    }

    public ArrayList GetGapTime(string starttime, string endtime, string tag, string tagvalue, int headdelay, int taildelay)//获取两段时间间的断流信息
    {
        ArrayList gaplist = new ArrayList();
        string query;
        DataSet data = new DataSet();
       DataBaseOperator opt =new DataBaseOperator();
        query = "SELECT  timestamp,tagname,value  FROM ihrawdata  where  tagname = '" + tag + "' and timestamp between '" + starttime + "' and '" + endtime + "' and intervalmilliseconds =  2s and value < " + tagvalue;
        data = CreateDataSetIH(query);
        if (data != null && data.Tables[0].Select().Length > 30)
        {
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
                    if (data != null && data.Tables[0].Select().Length > 30)//只有断流超过一定时间才视为断流成立
                    {
                        Gaptime gap;
                        gap.starttime = Convert.ToDateTime(GapStarttime).AddSeconds(headdelay).ToString("yyyy-MM-dd HH:mm:ss");
                        gap.endtime = Convert.ToDateTime(GapEndtime).AddSeconds(taildelay).ToString("yyyy-MM-dd HH:mm:ss");
                        gap.gaptime = data.Tables[0].Select().Length * 2;
                        gaplist.Add(gap);

                    }
                    starttime = Convert.ToDateTime(GapEndtime).AddSeconds(taildelay).ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
        }
        return gaplist;
    }

    public DataTable GetIHOrgDataSet(TimeSeg seg)
    {
        ///////////读取数据库中的采集条件
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra("select * from HT_QLT_COLLECTION where PARA_CODE = '" + seg.nodecode + "'");
        if (data == null || data.Tables[0].Rows.Count == 0)
            return null;
        DataRow row = data.Tables[0].Rows[0];
        ////////////////////工艺点标签//////////////////////////////////////////////
        string tagname = row["CUTOFF_POINT_TAG"].ToString();
        string interval = row["PERIODIC"].ToString();
        int timegap = Convert.ToInt32(row["CUTOFF_TIMEGAP"].ToString());
        ////////////////////断流参考点标签/////////////////////////////////////////////
        string tag = row["CUTOFF_POINT_TAG"].ToString();
        string Rst = row["CUTOFF_RST"].ToString();
        string RstValue = row["CUTOFF_RST_VALUE"].ToString();
        ////////////////////批头批尾判定点标签////////////////////////////////////////////////
        string tailtag = row["TAILLOGIC_TAG"].ToString();
        if (tailtag == "")
            tailtag = tag;
        string tailRst = row["TAILLOGIC_RST"].ToString();
        if (tailRst == "")
            tailRst = Rst;
        string tailRstValue = row["TAILLOGIC_RST_VALUE"].ToString();
        if (tailRst == "0")
            tailRst = RstValue;
        ///////////////////////////////////////////////////////////////////               
        int headDelay = Convert.ToInt32(row["HEAD_DELAY"].ToString());//料头
        int tailDelay = Convert.ToInt32(row["TAIL_DELAY"].ToString());//料尾
        int batchheadDelay = Convert.ToInt32(row["BATCH_HEAD_DELAY"].ToString());//批头
        int batchtailDelay = Convert.ToInt32(row["BATCH_TAIL_DELAY"].ToString());//批尾
        string query;
        if (tagname != "" && interval != "")
        {
            string tailBtime, tailEtime;//电子秤参与后判定的料头料尾时间


            tailBtime = seg.starttime;//初始料头时间
            tailEtime = seg.endtime;//初始化料尾时间
            ///////////////////////////自动寻找批头批尾/////////////////////////////////////////////////

            if (tailtag != "" && tailRst != "")
            {
                if (seg.type == "BEGIN" || seg.type == "BOTH")//寻找批头
                {
                    query = "SELECT  Min(timestamp),tagname  FROM ihrawdata where tagname = '" + tailtag + "' and timestamp between '" + seg.starttime + "' and '" + seg.endtime + "'  and value  <" + tailRst + " group by tagname";
                    data = CreateDataSetIH(query);
                    if (data != null && data.Tables[0].Select().Length > 0)
                        seg.starttime = Convert.ToDateTime(data.Tables[0].Rows[0]["Min of timestamp"].ToString()).AddSeconds(-20).ToString("yyyy-MM-dd HH:mm:ss"); ;//自动获取批头时间，向前推20秒
                }
                if (seg.type == "END" || seg.type == "BOTH")//寻找批尾
                {
                    string temptime = Convert.ToDateTime(seg.endtime).AddHours(1).ToString("yyyy-MM-dd HH:mm:ss");
                    DataSet tempdata = CreateDataSetIH("select value from ihrawdata where tagname = '" + tailtag + "' and timestamp between '" + seg.endtime + "' and '" + Convert.ToDateTime(seg.endtime).AddSeconds(60).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                    double temp = 0;
                    if (tempdata != null && tempdata.Tables[0].Select().Length > 0)
                        temp = Convert.ToDouble(tempdata.Tables[0].Select()[0][0].ToString());
                    if (temp >= Convert.ToDouble(tailRst))
                        query = "SELECT  Min(timestamp),tagname  FROM ihrawdata where tagname = '" + tailtag + "' and timestamp between '" + seg.endtime + "' and '" + temptime + "'  and value  <" + tailRst + " group by tagname";
                    else
                        query = "SELECT  Max(timestamp),tagname  FROM ihrawdata where tagname = '" + tailtag + "' and timestamp between '" + tailBtime + "' and '" + seg.endtime + "'  and value  >" + tailRst + " group by tagname";
                    data = CreateDataSetIH(query);
                    if (data != null && data.Tables[0].Select().Length > 0)
                    {
                        if (temp >= Convert.ToDouble(tailRst))
                            seg.endtime = Convert.ToDateTime(data.Tables[0].Rows[0]["Min of timestamp"].ToString()).AddSeconds(90).ToString("yyyy-MM-dd HH:mm:ss");//自动获取批尾时间
                        else
                            seg.endtime = Convert.ToDateTime(data.Tables[0].Rows[0]["Max of timestamp"].ToString()).AddSeconds(90).ToString("yyyy-MM-dd HH:mm:ss");//自动获取批尾时间
                    }
                }
                tailBtime = seg.starttime;//初始料头时间
                tailEtime = seg.endtime;//初始化料尾时间
                if (seg.type == "BEGIN" || seg.type == "BOTH")//寻找料头
                {
                    query = "SELECT  Min(timestamp),tagname  FROM ihrawdata where tagname = '" + tag + "' and timestamp between '" + seg.starttime + "' and '" + seg.endtime + "' and intervalmilliseconds =  " + interval + "s and value  >" + RstValue + " group by tagname";
                    data = CreateDataSetIH(query);
                    if (data != null && data.Tables[0].Select().Length > 0)
                        tailBtime = Convert.ToDateTime(data.Tables[0].Rows[0]["Min of timestamp"].ToString()).AddSeconds(headDelay).ToString("yyyy-MM-dd HH:mm:ss");//经延时料头时间
                }
                if (seg.type == "END" || seg.type == "BOTH")//寻找料尾
                {
                    query = "SELECT  Max(timestamp),tagname  FROM ihrawdata where tagname = '" + tag + "' and timestamp between '" + seg.starttime + "' and '" + seg.endtime + "' and intervalmilliseconds =  " + interval + "s and value  >" + RstValue + " group by tagname";
                    data = CreateDataSetIH(query);
                    if (data != null && data.Tables[0].Select().Length > 0)
                        tailEtime = Convert.ToDateTime(data.Tables[0].Rows[0]["Max of timestamp"].ToString()).AddSeconds(tailDelay).ToString("yyyy-MM-dd HH:mm:ss");//经延时料尾时间   
                }
            }
            ////////////////////选取数据/判断是否断料,并记录下相应的断料时间//////
            ArrayList gaptime = GetGapTime(tailBtime, tailEtime, tag, RstValue, batchheadDelay, batchtailDelay);
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
            else return null;
        }
        else return null;
    }

    public DataRowCollection GetData(string btime, string etime, string nodeid, string planno)
    {
        TimeSeg seg = GetTimeSeg(btime, etime, nodeid, planno);
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra("select * from HT_QLT_COLLECTION where PARA_CODE = '" + seg.nodecode + "'");
        if (data == null || data.Tables[0].Rows.Count == 0)
            return null;
        DataRow row = data.Tables[0].Rows[0];
        ////////////////////工艺点标签//////////////////////////////////////////////
        string tagname = row["CUTOFF_POINT_TAG"].ToString();
        string interval = row["PERIODIC"].ToString();
        int timegap = Convert.ToInt32(row["CUTOFF_TIMEGAP"].ToString());
      
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
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra("select * from HT_QLT_COLLECTION where PARA_CODE = '" + nodeid + "'");
        if (data == null || data.Tables[0].Rows.Count == 0)
            return null;
        DataRow row = data.Tables[0].Rows[0];
        ////////////////////工艺点标签//////////////////////////////////////////////
        string tagname = row["CUTOFF_POINT_TAG"].ToString();
        string interval = row["PERIODIC"].ToString();
        int timegap = Convert.ToInt32(row["CUTOFF_TIMEGAP"].ToString());

        string query = "SELECT  timestamp as 时间,value as 值  FROM ihrawdata where tagname = '" + tagname + "' and timestamp between '" + btime + "' and '" + etime + "' and intervalmilliseconds =  " + interval + "s order by timestamp ASC";
        data = CreateDataSetIH(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            return data.Tables[0].Rows;
        }
        else return null;
    }


    /// <summary>
    /// /从IH中直接取数据
    /// </summary>
    /// <param name="batch"></param>
    /// <param name="nodeId"></param>
    /// <returns></returns>





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
   

