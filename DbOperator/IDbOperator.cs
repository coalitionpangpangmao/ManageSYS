using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DbOperator
{
    public interface IDbOperator
    {        
        string UpDate(string query);      
        DataSet CreateDataSet(string query);
        
    }
}
