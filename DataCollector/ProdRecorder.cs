using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics;
namespace DataCollect
{

    public class ProdRecoder
    {
        public struct PointProperty
        {
            public string para_code;
            public string tag;
        }
        public ProdRecoder()
        {

        }


        protected static List<PointProperty> getParaList()
        {
            List<PointProperty> readlist = new List<PointProperty>();
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataSet data = opt.CreateDataSetOra("select r.para_code,r.VALUE_TAG from ht_pub_tech_para r  where r.para_type like '1___0%' and r.is_del = '0' order by r.para_code ");
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    PointProperty p = new PointProperty();
                    p.para_code = row["para_code"].ToString();
                    p.tag = row["VALUE_TAG"].ToString();
                    readlist.Add(p);
                }
            }
            return readlist;
        }

        public static void ProdRecord(string time, string team_code)
        {
            //从数据库中读取需要定时记录过程信息的数据点
            List<PointProperty> readlist = getParaList();
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            MSYS.DAL.DbOperator ihopt = new MSYS.DAL.DbOperator(new MSYS.DAL.OledbOperator());
            string[] seg = { "SECTION_CODE", "PLANNO", "TIME", "PARA_CODE", "PROD_CODE", "TEAM", "VALUE" };
            if (team_code == "")
                team_code = opt.GetSegValue("select team_code  from ht_prod_schedule where date_begin <='" + time + "' and date_end >='" + time + "' and work_staus = '1'", "team_code");


            foreach (PointProperty p in readlist)
            {
                string planno = opt.GetSegValue("select planno  from ht_prod_report where section_code = '" + p.para_code.Substring(0, 5) + "' and starttime < '" + time + "' and endtime  > '" + time + "'", "planno");
                if (planno != "NoRecord")
                {
                    string Y = ihopt.GetSegValue("SELECT  timestamp,tagname,value  FROM ihrawdata  where  tagname = '" + p.tag + "' and timestamp between '" + Convert.ToDateTime(time).AddMinutes(-1).ToString("yyyy/MM/dd HH:mm:ss") + "' and '" + Convert.ToDateTime(time).AddMinutes(1).ToString("yyyy/MM/dd HH:mm:ss") + "'", "value");
                    Y = (Y == "NoRecord" ? "0" : Y);
                    // if (p.para_code == "7030500029" || p.para_code == "7030500030")
                    //     Y = Convert.ToDouble("12.23").ToString("0"); 

                    string[] value = { p.para_code.Substring(0, 5), planno, time, p.para_code, planno.Substring(8, 7), team_code, Y };
                    opt.MergeInto(seg, value, 6, "HT_PROD_REPORT_DETAIL");
                }
            }
        }

        public static void ProdRecord(DateTime datetime)
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataSet times = opt.CreateDataSetOra("select date_begin  as time ,team_code from ht_prod_schedule where substr(date_begin,1,10) = '" + datetime.ToString("yyyy-MM-dd") + "' union select date_end as time,team_code  from ht_prod_schedule where substr(date_end,1,10) ='" + datetime.ToString("yyyy-MM-dd") + "' union select starttime  as time ,''  from ht_prod_report where substr(starttime,1,10) = '" + datetime.ToString("yyyy-MM-dd") + "' union select endtime  as time,''  from ht_prod_report where substr(endtime,1,10) = '" + datetime.ToString("yyyy-MM-dd") + "'");
            if (times != null && times.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow time in times.Tables[0].Rows)
                {
                    if (datetime - Convert.ToDateTime(time["time"].ToString()) >= new TimeSpan(0, -1, -20) && datetime - Convert.ToDateTime(time["time"].ToString()) <= new TimeSpan(0, 1, 20))
                        ProdRecord(time["time"].ToString(), time["team_code"].ToString());
                }
            }

        }
    }
    
}
