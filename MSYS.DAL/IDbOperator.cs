using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
namespace MSYS.DAL
{
    public interface IDbOperator
    {        
        string UpDate(string query);      
        DataSet CreateDataSet(string query);  
        string TransactionCommand(List<String> commandStringList);
        string ExecProcedures(string procedure, string[] seg, object[] value);
    }
}
