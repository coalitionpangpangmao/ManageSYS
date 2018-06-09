
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
namespace MSYS.DAL
{
    public sealed class SQLConnectionSingletion : ObjectPool, IFactoryDbPool
    {
        private static SQLConnectionSingletion instance = null;
        private static readonly object locker = new object();
        private SQLConnectionSingletion() { }
        private static string connectionString =
            @"server=(local);Trusted Connection=yes;database=northwind";

        public static string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }

        public static SQLConnectionSingletion CreateInstance()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                        instance = new SQLConnectionSingletion();

                }
            }

            return instance;
        }
        public IFactoryDbPool CreateInstanceI()
        {
            return CreateInstance();
        }
        protected override object Create()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }

        protected override bool Validate(object o)
        {
            try
            {
                SqlConnection conn = (SqlConnection)o;
                return !conn.State.Equals(ConnectionState.Closed);
            }
            catch (SqlException)
            {
                return false;
            }
        }

        protected override void Expire(object o)
        {
            try
            {
                SqlConnection conn = (SqlConnection)o;
                conn.Close();
            }
            catch (SqlException) { }
        }

        public DbConnection BorrowDBConnection()
        {
            try
            {
                return (SqlConnection)base.GetObjectFromPool();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ReturnDBConnection(DbConnection conn)
        {
            base.ReturnObjectToPool(conn);
        }

    }
}
