using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Data.Common;

namespace FactoryDbPool
{
    public sealed class OracleConnectionSingletion : ObjectPool, IFactoryDbPool
    {
        private static OracleConnectionSingletion instance = null;
        private static readonly object locker = new object();
        private OracleConnectionSingletion() { }       

        private static string connectionString =
            @"Data Source=ORCL;User ID=zs18;Password= zs18;Unicode=True";

        public static OracleConnectionSingletion CreateInstance()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                        instance = new OracleConnectionSingletion();

                }
            }

            return instance;
        }
        public IFactoryDbPool CreateInstanceI()
        {
            return CreateInstance();
        }
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

        protected override object Create()
        {
            OracleConnection conn = new OracleConnection(connectionString);
            conn.Open();
            return conn;
        }

        protected override bool Validate(object o)
        {
            try
            {
                OracleConnection conn = (OracleConnection)o;
                return !conn.State.Equals(ConnectionState.Closed);
            }
            catch (OracleException)
            {
                return false;
            }
        }

        protected override void Expire(object o)
        {
            try
            {
                OracleConnection conn = (OracleConnection)o;
                conn.Close();
            }
            catch (OracleException) { }
        }

        public DbConnection BorrowDBConnection()
        {
            try
            {
                return (OracleConnection)base.GetObjectFromPool();
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
