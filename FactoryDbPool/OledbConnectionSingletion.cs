
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data.Common;
using System.Data;

namespace FactoryDbPool
{
    public sealed class OledbConnectionSingletion : ObjectPool, IFactoryDbPool
    {
        private static OledbConnectionSingletion instance = null;
        private static readonly object locker = new object();
        private OledbConnectionSingletion() { }

        private static string connectionString =
            @"Provider=IhOLEDB.iHistorian.1;Data Source=10.50.212.71;User ID=administrator;Password = CHANGkong18;";

        public static OledbConnectionSingletion CreateInstance()
        {
           if (instance == null)
           {
               lock (locker)
               {
                   if (instance == null)
                       instance = new OledbConnectionSingletion();
                   
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
            OleDbConnection conn = new OleDbConnection(connectionString);
            conn.Open();
            return conn;
        }

        protected override bool Validate(object o)
        {
            try
            {
                OleDbConnection conn = (OleDbConnection)o;
                return !conn.State.Equals(ConnectionState.Closed);
            }
            catch (OleDbException)
            {
                return false;
            }
        }

        protected override void Expire(object o)
        {
            try
            {
                OleDbConnection conn = (OleDbConnection)o;
                conn.Close();
            }
            catch (OleDbException) { }
        }

        public DbConnection BorrowDBConnection()
        {
            try
            {
                return (OleDbConnection)base.GetObjectFromPool();
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