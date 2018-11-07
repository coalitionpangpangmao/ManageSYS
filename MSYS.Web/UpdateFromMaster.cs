using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
namespace MSYS.Web
{
    public abstract class UpdateFromMaster
    {
        protected string[] seg = null;
        protected string rootname;
        protected string tablename;
        public UpdateFromMaster()
        {

        }      

        public abstract  string InsertLocalFromMaster();   
    }
}
