using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSYS.Web.MasterService;
using System.Data;
using System.Xml;
using System.Collections;
namespace MSYS.Web
{
    public class UpdateEquipInfo:UpdateFromMaster
    {
        public UpdateEquipInfo()
        {
            this.seg = new string[] { "LOGINNAME", "NAME", "ID", "LEVELGROUPID"};
            this.tablename = "HT_SVR_USER";
            this.rootname = "USERINFO";
        }
         public override string GetXmlStr()
         {
             StringBuilder buffer = new StringBuilder();
             buffer.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
             buffer.Append("<request>");
             buffer.Append("</request>");
             MisMasterDataServiceInterfaceService service = new MisMasterDataServiceInterfaceService();
             string str = service.getEquiInfo(buffer.ToString());
             return str;
         }
         public override string InsertLocalFromMaster()
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
                         for (int i = 0; i < count ; i++)
                         {
                             segvalue[i] = childList[i].InnerText;
                         }
                         segvalue[3] = opt.GetSegValue("select F_CODE  from Ht_Svr_Org_Group where F_KEY = '" + segvalue[3] + "'", "F_CODE");
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

