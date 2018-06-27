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
    public class UpdateDeptInfo:UpdateFromMater
    {
        public UpdateDeptInfo()
        {
            this.seg = new string[] { "F_KEY", "F_NAME", "F_PATH", "F_PARENTID", "F_CODE" };
            this.tablename = "HT_SVR_ORG_GROUP";
            this.rootname = "DEPTINFO";
        }
         public override string GetXmlStr()
         {
             StringBuilder buffer = new StringBuilder();
             buffer.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
             buffer.Append("<request>");
             buffer.Append("</request>");
             MisMasterDataServiceInterfaceService service = new MisMasterDataServiceInterfaceService();
             string str = service.getDeptInfo(buffer.ToString());
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
                     int count = childList.Count+1;
                     if (count == seg.Length)
                     {
                         string[] segvalue = new string[count];
                         for (int i = 0; i < count-1; i++)
                         {
                             segvalue[i] = childList[i].InnerText;
                         }
                         segvalue[2] = "";
                         if (segvalue[1] == "江苏鑫源烟草薄片有限公司")
                             segvalue[count - 1] = "00700000";
                         else
                         {
                             segvalue[3] = opt.GetSegValue("select F_code from ht_svr_org_group where f_key = '" + segvalue[3] + "'","F_CODE");
                             string code;
                             if(segvalue[3] == "00700000")
                              code = "007" +  opt.GetSegValue("select nvl(Max(substr(F_CODE,4,3))+1,1) as code from Ht_Svr_Org_Group where F_parentid = '00700000'","CODE").PadLeft(3,'0') + "00";                             
                             else
                                 code = segvalue[3].Substring(0, 6) + opt.GetSegValue("select nvl( Max(substr(F_CODE,7,2))+1,1) as code from Ht_Svr_Org_Group where F_parentid = '" + segvalue[3] + "'", "CODE").PadLeft(2, '0');
                             segvalue[count - 1] = code;
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

