using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MSYS;
public partial class Quality_Optimize : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public struct ParaInfo
    {
        public string timestamp;
        public double value;
        //  public string status;
    }

    protected List<ParaInfo> getParaRecord(string tagname, string interval, DateTime Btime, DateTime Etime)
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
            System.Diagnostics.Debug.WriteLine(query);
            DataSet data = ihopt.CreateDataSetOra(query);
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

    protected void Search_Click(object sender, EventArgs e) {
        string tagname = "FIX.Z4_A085_TOTAL.F_CV";
        string interval = "6";
        List<ParaInfo> result = new List<ParaInfo>();
        DateTime Etime = Convert.ToDateTime("2019-02-28 09:37:00");
        DateTime Btime = Convert.ToDateTime("2019-02-28 09:35:30");
        result = getParaRecord(tagname, interval, Btime, Etime);
        System.Diagnostics.Debug.WriteLine(result.Count);
        //ParaInfo tem = new ParaInfo();
        foreach( ParaInfo tem in result){
            System.Diagnostics.Debug.WriteLine(tem.value);
        }
    }
}