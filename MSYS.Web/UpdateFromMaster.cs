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
        public UpdateFromMaster(string[] segname, string root, string table)
        {
            this.seg = segname;
            this.rootname = root;
            this.tablename = table;

        }
        public abstract string GetXmlStr();

        public virtual string InsertLocalFromMaster()
        {
            string Xmlstr = GetXmlStr();
            XmlDocument xx = new XmlDocument();
            xx.LoadXml(Xmlstr);//加载xml
            XmlNodeList xxList = xx.GetElementsByTagName(this.rootname); //取得节点名为DEPTINFO的XmlNode集合

            try
            {
                DbOperator opt = new DbOperator();
                foreach (XmlNode xxNode in xxList)
                {
                    XmlNodeList childList = xxNode.ChildNodes; //取得DEPTINFO下的子节点集合
                    int count = childList.Count;
                    if (count == seg.Length)
                    {
                        string[] segvalue = new string[count];
                        for (int i = 0; i < count; i++)
                        {
                            segvalue[i] = childList[i].InnerText;
                        }
                        opt.InsertData(this.seg, segvalue, this.tablename);
                    }
                    else
                    {
                        return "字段与值个数不匹配";
                    }
                }
                return "Success";

            }
            catch (Exception error)
            {
                return error.Message;
            }


        }

    
    }
}
