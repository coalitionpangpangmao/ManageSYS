using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics;
namespace DataCollect
{
    public struct PointProperty
    {
        public string para_code;
        public string tag;
        public string value;
    }
    public class FixRW
    {
        private static FixDataSystems.IFixDataSystem uniqFixRW;
        private static FixDataSystems.Group group;
        private static readonly object locker = new object();
        private static readonly object lockReader = new object();
        private static List<PointProperty> readlist = null;
        public FixRW()
        {
            initItem();
            initFixGroup();
        }

        protected void initFixGroup()
        {
            if (uniqFixRW == null)
            {
                lock (locker)
                {
                    if (uniqFixRW == null)
                    {
                        System.Type oType = System.Type.GetTypeFromProgID("FixDataSystems.Intellution FD Data System Control");
                        uniqFixRW = (FixDataSystems.IFixDataSystem)System.Activator.CreateInstance(oType);
                        if (group == null)
                        {
                            uniqFixRW.Groups.Add("DataGroup1");
                            group = uniqFixRW.Groups.Item("DataGroup1");
                        }
                    }
                }
                //Add the item to the group
                try
                {
                    var items = group.DataItems;
                    if (readlist != null)
                    {
                        foreach (PointProperty p in readlist)
                        {
                            items.Add(p.tag);
                        }
                    }
                }
                catch
                {
                    System.Diagnostics.Debug.Write("IFIX中未标签不存在");                    
                }
                finally
                { System.Environment.Exit(0); }

            }

        }

        private void initItem()
        {
            if (readlist == null)
            {
                readlist = new List<PointProperty>();
                MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                DataSet data = opt.CreateDataSetOra("select r.para_code,r.value_tag from ht_pub_tech_para r  where r.para_type like '1___0%' and r.is_del = '0' order by r.para_code ");
                if (data != null && data.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        PointProperty p = new PointProperty();
                        p.para_code = row["para_code"].ToString();
                        p.tag = row["value_tag"].ToString();
                        p.value = "0";
                        readlist.Add(p);
                    }
                }
            }

        }

        public void FixRecord()
        {
            try
            {
                lock (lockReader)
                {
                    if (group == null)
                    {
                        uniqFixRW.Groups.Add("DataGroup1");
                        group = uniqFixRW.Groups.Item("DataGroup1");
                    }
                    var items = group.DataItems;
                    if (items.Count != readlist.Count)
                    {
                        items.Clear();
                        foreach (PointProperty p in readlist)
                        {
                            items.Add(p.tag);
                        }
                    }
                    //Read all the items in the group 
                    group.Read();
                    int i = 1;
                    string[] seg = { "SECTION_CODE", "PLANNO", "TIME", "PARA_CODE", "PROD_CODE","TEAM","VALUE" };
                    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                  
                    foreach (PointProperty p in readlist)
                    {
                        string planno = opt.GetSegValue("select planno  from ht_prod_report where section_code = '" + p.para_code.Substring(0, 5) + "' and starttime < '" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' and endtime  > '" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "planno");
                        var Y = items.Item(i++).Value;
                        string[] value = { p.para_code.Substring(0, 5), planno, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), p.para_code,planno.Substring(8,7), Y.ToString("0.00") };
                        opt.InsertData(seg, value, "HT_PROD_REPORT_DETAIL");
                    }
                }

            }
            catch
            {
                System.Diagnostics.Debug.Write("录入数据失败！！");
            }
            finally
            { System.Environment.Exit(0); }
        }




    }
}
