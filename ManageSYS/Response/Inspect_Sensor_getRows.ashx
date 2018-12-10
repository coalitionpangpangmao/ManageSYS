<%@ WebHandler Language="C#" Class="Inspect_Sensor_getRows" %>

using System;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Text;

public class Inspect_Sensor_getRows : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        System.Diagnostics.Debug.WriteLine("getRows is running");
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        var json = new JObject();
        var rows = new JArray();
        StringBuilder qu = new StringBuilder();
        //qu.Append(createSql3(true, context.Request["prod_code"], context.Request["start_time"], context.Request["end_time"], context.Request["team_code"], context.Request["schedule_time"]));
        byte[] postData = new byte[context.Request.InputStream.Length];
        context.Request.InputStream.Read(postData, 0, postData.Length);
        string postDataString = System.Text.Encoding.UTF8.GetString(postData);
        JObject js = (JObject)JsonConvert.DeserializeObject(postDataString);
        qu.Append(createSql3(true, js["prod_code"].ToString(), js["start_time"].ToString(), js["end_time"].ToString(), js["team_code"].ToString(), js["schedule_time"].ToString()));
        //qu.Append(createSql3(true,"7031003", "2018-10-01", "2018-11-13", "00", "00"));
        System.Diagnostics.Debug.WriteLine("sql" + qu);
        DataSet data = opt.CreateDataSetOra(qu.ToString());
        int col = data.Tables[0].Columns.Count;
        for (int i = 0; i < data.Tables[0].Rows.Count; i++)
        {
            DataRow dr = data.Tables[0].Rows[i];
            var row = new JArray();
            for (int j = 0; j < col - 1; j++)
            {
                row.Add(dr[j]);
            }
            rows.Add(row);
        }
        json.Add("rows", rows);
        //System.Diagnostics.Debug.WriteLine(json.ToString());
        //return json.ToString();
        context.Response.ContentType = "json";
        context.Response.Write(json.ToString());
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    public string createSql3(bool Isteamgroup, string prodCode, string startTime, string endTime, string teamcode, string timecode)
    {
        StringBuilder sql = new StringBuilder();
        StringBuilder str = new StringBuilder();
        StringBuilder temp = new StringBuilder();
        //sql.Append("select g1.record_time as 记录时间,  p.prod_name as 产品 ");
        sql.Append("select g1.record_time as record_time_2, p.prod_name as product_2");

        //班组可选
        if (Isteamgroup)
            // sql.Append(",t.team_name as 班组, case when g1.shift_id='01' then '早班' when g1.shift_id='02' then '中班' when g1.shift_id='03' then '晚班' end as 班时 ");
            //sql.Append(", t.team_name as team_2, case when g1.shift_id='01' then '早班' when g1.shift_id='02' then '中班' when g1.shift_id ='03' then '晚班' end as 班时");
            sql.Append(", t.team_name as team_2, z.shift_name");
        else
            sql.Append(",'' as 班组, case  when g1.shift_id='01' then '早班' when g1.shift_id='02' then '中班' when g1.shift_id='03' then '晚班' end as 班时 ");

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        //string query = "select inspect_code,inspect_name from ht_qlt_inspect_proj  where inspect_code in (select r.inspect_code from ht_qlt_inspect_proj r left join ht_inner_inspect_group s on s.id = r.inspect_group left join ht_qlt_inspect_stdd t on t.inspect_code = r.inspect_code and t.is_del = '0' where r.inspect_group in('1','2','3') and r.is_del = '0' order by r.inspect_group)  and is_del = '0' order by inspect_code";
        string query = "select inspect_code,inspect_name from ht_qlt_inspect_proj  where inspect_code in (select r.inspect_code from ht_qlt_inspect_proj r left join ht_inner_inspect_group s on s.id = r.inspect_group left join ht_qlt_inspect_stdd t on t.inspect_code = r.inspect_code and t.is_del = '0' where r.inspect_group = '4' and r.is_del = '0')  and is_del = '0' order by inspect_code";
        DataSet data = opt.CreateDataSetOra(query);

        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            int i = 1;
            temp.Append("100");

            foreach (DataRow row in data.Tables[0].Rows)
            {
                string name = row["inspect_name"].ToString();
                string code = row["inspect_code"].ToString();
                sql.Append(",round(avg(nvl(g");
                sql.Append(i.ToString());
                sql.Append(".");
                sql.Append(name);
                sql.Append(",0)),2) as ");
                sql.Append(name);


                temp.Append("-round(avg(nvl(g");
                temp.Append(i.ToString());
                temp.Append(".score,0)),2)");

                if (i > 1)
                    str.Append(" left join ");
                str.Append("(select a.id,  a.prod_code,a.team_id,a.shift_ID,a.record_time ,nvl(b.score,0) as score, nvl(a.inspect_value,0) as ");
                // str.Append("(select   a.prod_code,a.team_id,a.shift_ID,a.record_time ,nvl(b.score,0) as score, nvl(a.inspect_value,0) as ");
                str.Append(name);
                str.Append("  from ht_qlt_inspect_record a left join ht_qlt_inspect_event b on b.record_id = a.id where a.inspect_code = '");
                str.Append(code);
                str.Append("' and substr(a.record_time,1,10) >= '" + startTime + "' and substr(a.record_time,1,10)<'" + endTime + "'");// 添加参数
                if (i == 1)
                {
                    str.Append(" and a.prod_code='" + prodCode + "'");
                }
                if (teamcode != "00" || teamcode == null)
                {
                    str.Append(" and a.team_id='" + teamcode + "'");
                }
                if (timecode != "00" || timecode == null)
                {
                    str.Append(" and a.shift_id = '" + timecode + "'");
                }
                str.Append(")g");


                str.Append(i.ToString());
                if (i > 1)
                {
                    str.Append(" on g1.prod_code = g");
                    str.Append(i.ToString());
                    str.Append(".prod_code  and g1.team_id = g");
                    str.Append(i.ToString());
                    str.Append(".team_id  and g1.shift_id = g");
                    str.Append(i.ToString());
                    str.Append(".shift_id  and g1.record_time = g");
                    str.Append(i.ToString());
                    str.Append(".record_time  ");
                }
                i++;
            }
            temp.Append(" as 得分");
            sql.Append(", g1.id, ");
            sql.Append(temp);
            sql.Append(" from ");
            sql.Append(str.ToString());
            sql.Append("left join ht_pub_prod_design p on p.prod_code = g1.prod_code left join ht_sys_team t on t.team_code = g1.team_id ");
            sql.Append("join ht_sys_shift z on z.shift_code = g1.shift_id ");
            
            sql.Append(" group by p.prod_name");
            if (Isteamgroup)
                sql.Append(",t.team_name, g1.record_time, g1.shift_id, z.shift_name,g1.id");
            sql.Append(" order by g1.record_time, t.team_name");
            return sql.ToString();
        }
        else return null;

    }

}