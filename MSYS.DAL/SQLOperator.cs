using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace MSYS.DAL
{
    public class SQLOperator : IDbOperator
    {
        public SQLOperator()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public string UpDate(string query)
        {
            DbCommand sqlcom = null;
            DbConnection myConn = null;
            IFactoryDbPool pool = SQLConnectionSingletion.CreateInstance();
            try
            {
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["SQLConnectionString"];
                SQLConnectionSingletion.ConnectionString = settings.ConnectionString;
                //Borrow the SqlConnection object from the pool
                myConn = pool.BorrowDBConnection();
                sqlcom = new SqlCommand(query, (SqlConnection)myConn);
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
            IFactoryDbPool pool = SQLConnectionSingletion.CreateInstance();
            try
            {
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["OracleConnectionString"];
                SQLConnectionSingletion.ConnectionString = settings.ConnectionString;
                //Borrow the SqlConnection object from the pool
                myConn = pool.BorrowDBConnection();
                sqlcom = new SqlCommand(query, (SqlConnection)myConn);

                DataSet ds = new DataSet();
                SqlDataAdapter oda = new SqlDataAdapter();
                oda.SelectCommand = (SqlCommand)sqlcom;
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