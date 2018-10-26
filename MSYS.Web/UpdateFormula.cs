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
    public class UpdateFormula:UpdateFromMaster
    {
        public UpdateFormula()
        {
            this.seg = new string[] { "ID", "LOGINNAME", "NAME", "LEVELGROUPID","PASSWORD" };
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
             string str = service.getFormulaInfo(buffer.ToString());
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
                 MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                 
                 foreach (XmlNode xxNode in xxList)
                 {
                     XmlNodeList childList = xxNode.ChildNodes; //取得DEPTINFO下的子节点集合
                     int count = childList.Count+1;
                     if (count == seg.Length)
                     {
                         string[] segvalue = new string[count];
                         segvalue[0] = childList[2].InnerText;
                         segvalue[1] = childList[0].InnerText;
                         segvalue[2] = childList[1].InnerText;
                         segvalue[3] = childList[3].InnerText;
                         string dpno = opt.GetSegValue("select F_CODE  from Ht_Svr_Org_Group where F_KEY = '" + segvalue[3] + "'", "F_CODE");
                         if (dpno != "NoRecord")
                             segvalue[3] = dpno;
                         string psd = opt.GetSegValue("select Password from HT_SVR_USER where ID = '" + segvalue[0] + "'","PassWord");
                         if (psd == "NoRecord" || psd == "")
                             segvalue[4] = "e10adc3949ba59abbe56e057f20f883e";
                         else
                             segvalue[4] = psd;
                         opt.MergeInto(this.seg, segvalue, 1, this.tablename);
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

