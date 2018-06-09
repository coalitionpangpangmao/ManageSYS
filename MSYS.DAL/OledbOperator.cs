using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.OleDb;
using System.Data.Common;
using System.Data;

namespace MSYS.DAL
{
    public class OledbOperator : IDbOperator
    {
        public OledbOperator()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public string UpDate(string query)
        {
            DbCommand sqlcom = null;
            DbConnection myConn = null;
            IFactoryDbPool pool = OledbConnectionSingletion.CreateInstance();
            try
            {
                if(ConfigurationManager.ConnectionStrings["OledbConnectionString"] != null)
                {
                    ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["OledbConnectionString"];
                    OledbConnectionSingletion.ConnectionString = settings.ConnectionString;
                }
                //Borrow the SqlConnection object from the pool
                myConn = pool.BorrowDBConnection();
                sqlcom = new OleDbCommand(query, (OleDbConnection)myConn);
                sqlcom.ExecuteNonQuery();
                return "Success";
            }
            catch (Exception ee)
            {
                if (sqlcom != null)
                    sqlcom.Dispose();
                return ee.Message;
            }
            finally
            {
                pool.ReturnDBConnection(myConn);
            }
        }
        public DataSet CreateDataSet(string query)
        {
            DbCommand sqlcom = null;
            DbConnection myConn = null;
            IFactoryDbPool pool = OledbConnectionSingletion.CreateInstance();
            try
            {
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["OracleConnectionString"];
                OledbConnectionSingletion.ConnectionString = settings.ConnectionString;
                //Borrow the SqlConnection object from the pool
                myConn = pool.BorrowDBConnection();
                sqlcom = new OleDbCommand(query, (OleDbConnection)myConn);

                DataSet ds = new DataSet();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                oda.SelectCommand = (OleDbCommand)sqlcom;
                oda.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                if (sqlcom != null)
                    sqlcom.Dispose();
                return null;
            }
            finally
            {
                pool.ReturnDBConnection(myConn);
            }
        }

    }
}