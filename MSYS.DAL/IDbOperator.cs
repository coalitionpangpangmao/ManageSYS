using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Collections;
namespace MSYS.DAL
{
    public interface IDbOperator
    {        
        string UpDate(string query);      
        DataSet CreateDataSet(string query);
        string TransactionCommand(ArrayList commandStringList);
    }
}
