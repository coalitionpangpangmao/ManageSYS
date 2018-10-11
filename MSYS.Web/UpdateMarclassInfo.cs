using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSYS.Web.MasterService;
using System.Xml;
using System.Collections;
namespace MSYS.Web
{
   public class UpdateMarclassInfo:UpdateFromMaster
    {
        public UpdateMarclassInfo()
        {
            this.seg = new string[] { "MATTREE_CODE", "MATTREE_NAME", "PARENT_CODE"};
            this.tablename = "HT_PUB_MATTREE";
            this.rootname = "MARBASCLASS";
        }
         public override string GetXmlStr()
         {
             StringBuilder buffer = new StringBuilder();
             buffer.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
             buffer.Append("<request>");
             buffer.Append("</request>");
             MisMasterDataServiceInterfaceService service = new MisMasterDataServiceInterfaceService();
             string str = service.getMarbasclassInfo(buffer.ToString());
             return str;
         }
         public override string InsertLocalFromMaster()
         {
             string Xmlstr = GetXmlStr();
             XmlDocument xx = new XmlDocument();
             xx.LoadXml(Xmlstr);//加载xml
             XmlNodeList xxList = xx.GetElementsByTagName(this.rootname); 

             try
             {
                 MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                 foreach (XmlNode xxNode in xxList)
                 {
                     XmlNodeList childList = xxNode.ChildNodes; 
                     int count = childList.Count;
                     if (count == seg.Length)
                     {
                         string[] segvalue = new string[count];
                         for (int i = 0; i < count - 1; i++)
                         {
                             segvalue[i] = childList[i].InnerText;
                         }
                         if (segvalue[2] == "null")
                             segvalue[2] = "";                       
                           
                         opt.MergeInto(this.seg, segvalue,1, this.tablename);
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

