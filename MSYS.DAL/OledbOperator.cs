using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.OleDb;
using System.Data.Common;
using System.Data;
using System.Collections;
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

        public string TransactionCommand(ArrayList commandStringList)
        {

            IFactoryDbPool pool = OracleConnectionSingletion.CreateInstance();
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["OracleConnectionString"];
            OledbConnectionSingletion.ConnectionString = settings.ConnectionString;
            //Borrow the SqlConnection object from the pool
            DbConnection myConn = pool.BorrowDBConnection();
            OleDbTransaction m_OraTrans = ((OleDbConnection)myConn).BeginTransaction();//创建事务对象
            DbCommand sqlcom = new OleDbCommand();
            sqlcom.Connection = myConn;
            sqlcom.Transaction = m_OraTrans;
            int influenceRowCount = 0;
            try
            {
                foreach (string commandString in commandStringList)
                {                   
                    sqlcom.CommandText = commandString;
                    influenceRowCount += sqlcom.ExecuteNonQuery();
                }
                if (influenceRowCount == commandStringList.Count)
                {
                    m_OraTrans.Commit();
                    return "Success";
                }
                else
                {
                    m_OraTrans.Rollback();
                    return "Failed";
                }
            }
            catch (Exception ee)
            {
                if (sqlcom != null)
                    sqlcom.Dispose();
                m_OraTrans.Rollback();
                return ee.Message;
            }
            finally
            {
                sqlcom.Dispose();
                pool.ReturnDBConnection(myConn);
            }

        }
    }
}