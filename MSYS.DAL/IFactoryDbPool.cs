using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
namespace MSYS.DAL
{
    public interface IFactoryDbPool
    {  
        DbConnection BorrowDBConnection();  
        void ReturnDBConnection(DbConnection conn);
         IFactoryDbPool CreateInstanceI();
    }

}
