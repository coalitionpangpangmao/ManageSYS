using System;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Collections;
/// <summary>
///Class1 的摘要说明
/// </summary>
namespace SysCode
{
    public class DBOpt
    {
        public DBOpt()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //

        }
        //连接数据库 SQl 操作
        public SqlConnection GetConnSQL()
        {
            String conn = "Data Source=CLIENT3\\SQLEXPRESS;Initial Catalog=ZS18;Integrated Security=True";
            SqlConnection myConn = new SqlConnection(conn);
            return myConn;
        }
        public DataSet CreateDataSetSQL(string query)
        {
            String conn = "Data Source=CLIENT3\\SQLEXPRESS;Initial Catalog=ZS18;Integrated Security=True";
            SqlConnection myConn = null;
            try
            {
                myConn = new SqlConnection(conn);
                DataSet set = new DataSet();
                SqlDataAdapter myAdapter = new SqlDataAdapter(query, myConn);
                myAdapter.Fill(set);
                myConn.Close();
                return set;
            }
            catch (Exception)
            {
                if (myConn != null)
                    myConn.Close();
                return null;
            }
            finally
            {
                myConn.Dispose();
            }
        }
        public string UpDateSQL(string query)
        {
            String conn = "Data Source=CLIENT3\\SQLEXPRESS;Initial Catalog=ZS18;Integrated Security=True";
            SqlConnection myConn = null;
            SqlCommand sqlcom = null;
            try
            {
                myConn = new SqlConnection(conn);
                sqlcom = new SqlCommand(query, myConn);
                myConn.Open();
                sqlcom.ExecuteNonQuery();
                myConn.Close();
                return "Success";
            }
            catch (Exception ee)
            {
                if (myConn != null)
                    myConn.Close();
                if (sqlcom != null)
                    sqlcom.Dispose();
                return ee.Message;
            }
            finally
            {
                myConn.Dispose();
            }
        }

        //连接数据库 Oracle 操作
        public OracleConnection GetConnOra()
        {
            String conn = ConfigurationManager.AppSettings["ConnectionString"].ToString();
            OracleConnection myConn = new OracleConnection(conn);
            return myConn;
        }
        public DataSet CreateDataSetOra(string query)
        {
            string conn = ConfigurationManager.AppSettings["ConnectionString"].ToString();//写连接串        
            OracleConnection myConn = null;
            try
            {
                myConn = new OracleConnection(conn);
                OracleCommand cmd = new OracleCommand(query, myConn);
                DataSet ds = new DataSet();
                OracleDataAdapter oda = new OracleDataAdapter();
                oda.SelectCommand = cmd;
                oda.Fill(ds);
                myConn.Close();
                return ds;
            }
            catch (Exception)
            {
                if (myConn != null)
                    myConn.Close();
                return null;
            }
            finally
            {
                myConn.Dispose();
            }
        }

        public DataSet CreateDataSetXLCF(string query)
        {
            string conn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.50.212.121)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));User Id=CHUZHOUXLCF;Password=123456; ";//写连接串        
            OracleConnection myConn = null;
            try
            {
                myConn = new OracleConnection(conn);
                OracleCommand cmd = new OracleCommand(query, myConn);
                DataSet ds = new DataSet();
                OracleDataAdapter oda = new OracleDataAdapter();
                oda.SelectCommand = cmd;
                oda.Fill(ds);
                myConn.Close();
                return ds;
            }
            catch (Exception)
            {
                if (myConn != null)
                    myConn.Close();
                return null;
            }
            finally
            {
                myConn.Dispose();
            }
        }
        public string UpDateOra(string query)
        {
            String conn = ConfigurationManager.AppSettings["ConnectionString"].ToString();
            OracleConnection myConn = null;
            OracleCommand sqlcom = null;
            try
            {
                myConn = new OracleConnection(conn);
                myConn.Open();
                sqlcom = new OracleCommand(query, myConn);
                sqlcom.ExecuteNonQuery();
                myConn.Close();
                return "Success";
            }
            catch (Exception ee)
            {
                if (myConn != null)
                    myConn.Close();
                if (sqlcom != null)
                    sqlcom.Dispose();
                return ee.Message;
            }
            finally
            {
                myConn.Dispose();
            }
        }
        public DataTable ProcedureOra(string[] paraname, string[] paravalue, string ProcedureName)
        {

            String conn = ConfigurationManager.AppSettings["ConnectionString"].ToString();
            OracleConnection connection = new OracleConnection(conn);
            OracleDataReader odr;
            connection.Open();
            OracleCommand command = new OracleCommand(ProcedureName, connection);
            command.CommandType = CommandType.StoredProcedure;
            int count = paraname.Length;
            for (int i = 0; i < count; i++)
            {
                command.Parameters.Add(paraname[i], OracleType.VarChar).Value = paravalue[i];
            }
            odr = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(odr);
            return dt;

        }

        public string InsertTlog(string user, string cmt, string record)
        {
            string query = "insert into G_LOGINRECORD(f_user,f_computer,f_time,F_DESCRIPT)values('"
                    + user + "','"
                    + cmt + "','"
                    + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','"
                    + record + "')";
            return UpDateOra(query);
        }

        public string SqlConvert(string sqlstr, Hashtable ht)
        {
            sqlstr.Replace("$batch$", ht["$batch$"].ToString());
            sqlstr.Replace("$brand$", ht["$brand$"].ToString());
            sqlstr.Replace("$startdate$", ht["$startdate$"].ToString());
            sqlstr.Replace("$enddate$", ht["$enddate$"].ToString());
            sqlstr.Replace("$shift$", ht["$shift$"].ToString());
            return sqlstr;
        }
        //绑定DROPDOWNLIST控件

        //根据某个字段获取另一字段的值
        public string GetKey(string table, string prikey, string reskey, string privalue)
        {
            string query = "select " + reskey + " from " + table + " where " + prikey + " = '" + privalue + "'";
            DataSet myds = CreateDataSetOra(query);
            if (myds.Tables[0].Select().GetLength(0) > 0)
                return myds.Tables[0].Rows[0][0].ToString();
            else
                return null;
        }

        public string GetRate(string StandardID, string PointID)
        {
            string query = "select * from G_RECIPE where f_standard_ID = '" + StandardID + "' and F_PARA_ID = '" + PointID + "'";
            DataSet myds = CreateDataSetOra(query);
            if (myds.Tables[0].Select().GetLength(0) > 0)
                return myds.Tables[0].Rows[0]["F_INITIAL_VALUE"].ToString();
            else
                return null;
        }

        public string GetMax(string table, string key, int seg)
        {
            string query = "select Max(" + key + ") from " + table;
            DataSet data = CreateDataSetOra(query);
            query = data.Tables[0].Select()[0][0].ToString();
            query = (Convert.ToInt32(query) + 1).ToString();
            if (query.Length > seg)
                throw new Exception("ID号错误，超出最大界限");
            int len = seg - query.Length;
            for (int i = 0; i < len; i++)
                query = "0" + query;
            return query;
        }

        public string GetID(string query, int seg, int leng)
        {
            DataSet data = CreateDataSetOra(query);
            string temp = data.Tables[0].Select()[0][0].ToString();
            temp = (Convert.ToInt32(temp) + 1).ToString();
            string tail = "";
            while (leng-- > 0)
            {
                tail += "0";
            }
            if (temp.Length > leng && temp.Substring(temp.Length - leng - 1) == "0")
                throw new Exception("值超出限定范围！");
            if (temp.Length > seg)
                throw new Exception("ID号错误，超出最大界限");
            int len = seg - temp.Length;
            for (int i = 0; i < len; i++)
                temp = "0" + temp;
            return temp;
        }

        public string GetID(string query, int seg)
        {
            DataSet data = CreateDataSetOra(query);
            string temp = data.Tables[0].Select()[0][0].ToString();
            temp = (Convert.ToInt32(temp) + 1).ToString();
            if (temp.Length > seg)
                throw new Exception("ID号错误，超出最大界限");
            int len = seg - temp.Length;
            for (int i = 0; i < len; i++)
                temp = "0" + temp;
            return temp;
        }

        public string GetValueIH(string tagname, string time)
        {
            string ConnectionString = "Provider=IhOLEDB.iHistorian.1;Data Source=10.50.212.71;User ID=administrator;Password = CHANGkong18;";
            string strSQL = "select value from ihRawData where Tagname = 'SCADA." + tagname + ".F_CV' where timestamp between '" + time + "' and '" + Convert.ToDateTime(time).AddMinutes(-2).ToString("yyyy/MM/dd hh:mm:ss") + "'";
            OleDbConnection cn_IH = new OleDbConnection(ConnectionString);
            OleDbCommand cmd_IH = new OleDbCommand(strSQL, cn_IH);
            DataSet ds = new DataSet();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            oda.SelectCommand = cmd_IH;
            oda.Fill(ds);
            cn_IH.Close();
            if (ds.Tables[0].Select().GetLength(0) > 0)
                return ds.Tables[0].Select()[0][0].ToString();
            else
                return "";
        }
        public DataSet CreateDataSetIH(string query)
        {
            string ConnectionString = "Provider=IhOLEDB.iHistorian.1;Data Source=10.50.212.71;User ID=administrator;Password = CHANGkong18;";//写连接串        
            OleDbConnection cn_IH = null;
            try
            {
                cn_IH = new OleDbConnection(ConnectionString);
                OleDbCommand cmd_IH = new OleDbCommand(query, cn_IH);
                DataSet ds = new DataSet();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                oda.SelectCommand = cmd_IH;
                oda.Fill(ds);
                cn_IH.Close();
                return ds;
            }
            catch (Exception ee)
            {
                if (cn_IH != null)
                    cn_IH.Close();
                return null;
            }
            finally
            {
                cn_IH.Dispose();
            }
        }

        public string GetReportName(string id)
        {
            string area;
            if (id.Length == 5)
                area = GetKey("G_POINTS", "F_ID", "F_AREA", id);
            else if (id.Length == 3)
                area = id;
            else
                area = "";
            string name;
            if (area.Substring(0, 3) == "110")
                name = "G_REPORT_YT602_G1";
            else if (area.Substring(0, 3) == "111")
                name = "G_REPORT_YT602_G2";
            else if (area.Substring(0, 3) == "130")
                name = "G_REPORT_YT603_G1";
            else if (area.Substring(0, 3) == "131")
                name = "G_REPORT_YT603_G2";
            else if (area.Substring(0, 3) == "132")
                name = "G_REPORT_YT607_G1";
            else if (area.Substring(0, 3) == "133")
                name = "G_REPORT_YT607_G2";
            else if (area.Substring(0, 3) == "134")
                name = "G_REPORT_YT607_G3";
            else if (area.Substring(0, 3) == "210")
                name = "G_REPORT_YT605_G1";
            else
                name = "G_REPORT_YT605_G2";
            return name;
        }
        public string GetBatchTag(string id)
        {
            string area = GetKey("G_POINTS", "F_ID", "F_AREA", id);
            string name;
            if (area.Substring(0, 3) == "110")
                name = "SCADA.YT602_1CP1P.A_CV";
            else if (area.Substring(0, 3) == "111")
                name = "SCADA.YT602_2CP1P.A_CV";
            else if (area.Substring(0, 3) == "130")
                name = "SCADA.YT603_1CP1P.A_CV";
            else if (area.Substring(0, 3) == "131")
                name = "SCADA.YT603_2CP1P.A_CV";
            else if (area.Substring(0, 3) == "132")
                name = "SCADA.YT607_1CP1P.A_CV";
            else if (area.Substring(0, 3) == "133")
                name = "SCADA.YT607_2CP1P.A_CV";
            else if (area.Substring(0, 3) == "134")
                name = "SCADA.YT607_3CP1P.A_CV";
            else if (area.Substring(0, 3) == "210")
                name = "SCADA.YT605_1CP1P.A_CV";
            else
                name = "SCADA.YT605_2CP1P.A_CV";
            return name;
        }
        public DataTable GetGrid(string Seg)
        {
            DataTable TabTemp = new DataTable();
            TabTemp.Columns.Add("f_silo_ID");
            TabTemp.Columns.Add("f_batch_ID");
            TabTemp.Columns.Add("f_ph_ID");
            TabTemp.Columns.Add("f_oper_number");
            TabTemp.Columns.Add("f_status");
            TabTemp.Columns.Add("f_in_control_status");
            TabTemp.Columns.Add("f_out_control_status");
            TabTemp.Columns.Add("f_set_reserve_time");
            TabTemp.Columns.Add("f_real_reserve_time");
            TabTemp.Columns.Add("f_in_begin_time");
            TabTemp.Columns.Add("f_in_end_time");
            TabTemp.Columns.Add("f_out_begin_time");
            TabTemp.Columns.Add("f_out_end_time");
            TabTemp.Columns.Add("f_reserve_total");
            TabTemp.Columns.Add("f_remain_per");
            TabTemp.Columns.Add("f_synchro_time");
            string query;
            if (Seg == "00")
                query = "select * from G_SECTION_ID where F_STYLE = '4'";
            else
                query = "select * from G_SECTION_ID where F_STYLE = '4' and F_Line like '" + Seg + "%'";
            DataSet data = CreateDataSetOra(query);
            int count = data.Tables[0].Select().GetLength(0);
            for (int i = 0; i < count; i++)
            {
                string Sid = data.Tables[0].Rows[i]["F_SECTION_ID"].ToString();
                query = "select * from G_SILO_TAG where F_SILOID = '" + Sid + "' order by F_ORDER";
                DataSet ds = CreateDataSetOra(query);
                DataRow[] tRows = ds.Tables[0].Select();
                query = GetQuery(tRows);
                string ConnectionString = "Provider=IhOLEDB.iHistorian.1;Data Source=10.50.212.71;User ID=administrator;Password = CHANGkong18;";
                OleDbConnection cn_IH = new OleDbConnection(ConnectionString);
                OleDbCommand cmd_IH = new OleDbCommand(query, cn_IH);
                OleDbDataAdapter oda = new OleDbDataAdapter();
                oda.SelectCommand = cmd_IH;
                oda.Fill(ds);
                cn_IH.Close();
                if (ds.Tables[0].Select().GetLength(0) > 0)
                {
                    DataRow tr = TabTemp.NewRow();
                    tr[0] = GetKey("G_SECTION_ID", "F_SECTION_ID", "F_NAME", Sid);
                    DataRow temprow = ds.Tables[0].Rows[0];
                    for (int m = 1, n = 0; n < temprow.ItemArray.Length; n++)
                    {
                        n++;
                        string temp = temprow[n].ToString();
                        if (m == 9)
                        {
                            if (temp != "")
                                tr[m] += temp + "/";
                            if (temp == "")
                                tr[m] += "00/";
                            if (n == 12)
                                m++;
                        }
                        else if (m == 10)
                        {
                            if (temp != "")
                                tr[m] += temp + "/";
                            if (temp == "")
                                tr[m] += "00/";
                            if (n == 16)
                                m++;
                        }
                        else if (m == 11)
                        {
                            if (temp != "")
                                tr[m] += temp + "/";
                            if (temp == "")
                                tr[m] += "00/";
                            if (n == 20)
                                m++;
                        }
                        else if (m == 12)
                        {
                            if (temp != "")
                                tr[m] += temp + "/";
                            if (temp == "")
                                tr[m] += "00/";
                            if (n == 24)
                                m++;
                        }
                        else
                            tr[m++] = temp;
                    }
                    tr["f_synchro_time"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    TabTemp.Rows.Add(tr);
                }
            }
            return TabTemp;
        }
        protected string GetQuery(DataRow[] Rows)
        {
            int count = Rows.GetLength(0);
            string query = "select ";
            for (int i = 1; i < count; i++)
            {
                query += "F" + i.ToString() + ".value,";
            }
            query += "F" + count.ToString() + ".value from ";
            int m = 1;
            foreach (DataRow row in Rows)
            {
                if (m == 1)
                    query += "ihRawData F" + m.ToString() + " on F" + m.ToString() + ".Tagname = 'SCADA." + row["F_TAGNAME"].ToString() + ".F_CV' and F" + m.ToString() + ".timestamp between '" + DateTime.Now.ToString("yyyy/MM/dd hh:ss:mm") + "' and '" + DateTime.Now.AddMinutes(-2).ToString("yyyy/MM/dd hh:mm:ss") + "'";
                else
                    query += " left join ihRawData F" + m.ToString() + " on F" + m.ToString() + ".Tagname = 'SCADA." + row["F_TAGNAME"].ToString() + ".F_CV' and F" + m.ToString() + ".timestamp between '" + DateTime.Now.ToString("yyyy/MM/dd hh:ss:mm") + "' and '" + DateTime.Now.AddMinutes(-2).ToString("yyyy/MM/dd hh:mm:ss") + "' and F" + m.ToString() + ".timestamp = F1.timestamp";
                m++;

            }
            return query;
        }
    }
}
    

