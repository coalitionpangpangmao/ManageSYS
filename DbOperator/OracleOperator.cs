using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.OracleClient;
using System.Data.Common;
using System.Data;
using FactoryDbPool;
using System.Web.UI.WebControls;

namespace DbOperator
{
    public class OracleOperator : IDbOperator
    {
        public OracleOperator()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public string UpDate(string query)
        {
            DbCommand sqlcom = null;
            DbConnection myConn = null;
            IFactoryDbPool pool = OracleConnectionSingletion.CreateInstance();
            try
            {
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["OracleConnectionString"];
                OracleConnectionSingletion.ConnectionString = settings.ConnectionString;
                //Borrow the SqlConnection object from the pool
                myConn = pool.BorrowDBConnection();
                sqlcom = new OracleCommand(query, (OracleConnection)myConn);
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
            IFactoryDbPool pool = OracleConnectionSingletion.CreateInstance();
            try
            {
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["OracleConnectionString"];
                OracleConnectionSingletion.ConnectionString = settings.ConnectionString;
                //Borrow the SqlConnection object from the pool
                myConn = pool.BorrowDBConnection();
                sqlcom = new OracleCommand(query, (OracleConnection)myConn);

                DataSet ds = new DataSet();
                OracleDataAdapter oda = new OracleDataAdapter();
                oda.SelectCommand = (OracleCommand)sqlcom;
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