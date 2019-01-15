using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
namespace MSYS.Web
{
    public abstract class UpdateFromMaster
    {
      
        public UpdateFromMaster()
        {

        }      

        public abstract  string InsertLocalFromMaster();
        protected abstract void InsertLocalFromMasterAsyn();
        
    }
}
