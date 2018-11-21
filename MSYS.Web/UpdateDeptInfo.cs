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
    public class UpdateDeptInfo : UpdateFromMaster
    {
        protected static string[] seg = null;
        protected static string rootname;
        protected static string tablename;
        public UpdateDeptInfo()
        {
            seg = new string[] { "F_KEY", "F_NAME", "F_PATH", "F_PARENTID", "F_CODE" };
            tablename = "HT_SVR_ORG_GROUP";
            rootname = "DEPTINFO";
        }
      
        public override  string InsertLocalFromMaster()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            buffer.Append("<request>");
            buffer.Append("</request>");
            MisMasterDataServiceInterfaceService service = new MisMasterDataServiceInterfaceService();
         
            string Xmlstr = service.getDeptInfo(buffer.ToString());
            XmlDocument xx = new XmlDocument();
            xx.LoadXml(Xmlstr);//加载xml
            XmlNodeList xxList = xx.GetElementsByTagName(rootname); //取得节点名为DEPTINFO的XmlNode集合             
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            List<string> commandlist = new List<string>();
            foreach (XmlNode xxNode in xxList)
            {
                XmlNodeList childList = xxNode.ChildNodes; //取得DEPTINFO下的子节点集合
                int count = childList.Count + 1;
                if (count == seg.Length)
                {
                    string[] segvalue = new string[count];
                    for (int i = 0; i < count - 1; i++)
                    {
                        segvalue[i] = childList[i].InnerText;
                    }
                    segvalue[2] = "";
                    if (segvalue[1] == "江苏鑫源烟草薄片有限公司")
                        segvalue[count - 1] = "00700000";
                    else
                    {
                        segvalue[3] = opt.GetSegValue("select F_code from ht_svr_org_group where f_key = '" + segvalue[3] + "'", "F_CODE");
                        segvalue[4] = opt.GetSegValue("select F_code from ht_svr_org_group where f_key = '" + segvalue[0] + "'", "F_CODE");
                        if (segvalue[4] == "NoRecord")
                        {
                            string code;
                            if (segvalue[3] == "00700000")
                                code = "007" + opt.GetSegValue("select nvl(Max(substr(F_CODE,4,3))+1,1) as code from Ht_Svr_Org_Group where F_parentid = '00700000'", "CODE").PadLeft(3, '0') + "00";
                            else
                                code = segvalue[3].Substring(0, 6) + opt.GetSegValue("select nvl( Max(substr(F_CODE,7,2))+1,1) as code from Ht_Svr_Org_Group where F_parentid = '" + segvalue[3] + "'", "CODE").PadLeft(2, '0');
                            segvalue[4] = code;
                        }
                    }
                    string temp = opt.getMergeStr(seg, segvalue, 1, tablename);
                    commandlist.Add(temp);
                    if (opt.UpDateOra(temp) != "Success")
                        System.Diagnostics.Debug.Write(temp); 
                }
            }
            return opt.TransactionCommand(commandlist);


        }
        protected override void InsertLocalFromMasterAsyn()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            buffer.Append("<request>");
            buffer.Append("</request>");
            MisMasterDataServiceInterfaceService service = new MisMasterDataServiceInterfaceService();
            service.getDeptInfoCompleted += new getDeptInfoCompletedEventHandler(service_Completed);
            service.getDeptInfoAsync(buffer.ToString());

        }
  
       private static void service_Completed(object sender, getDeptInfoCompletedEventArgs e)
       {
           string Xmlstr = e.Result;
           XmlDocument xx = new XmlDocument();
           xx.LoadXml(Xmlstr);//加载xml
           XmlNodeList xxList = xx.GetElementsByTagName(rootname); //取得节点名为DEPTINFO的XmlNode集合             
           MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
          // List<string> commandlist = new List<string>();
           foreach (XmlNode xxNode in xxList)
           {
               XmlNodeList childList = xxNode.ChildNodes; //取得DEPTINFO下的子节点集合
               int count = childList.Count + 1;
               if (count == seg.Length)
               {
                   string[] segvalue = new string[count];
                   for (int i = 0; i < count - 1; i++)
                   {
                       segvalue[i] = childList[i].InnerText;
                   }
                   segvalue[2] = "";
                   if (segvalue[1] == "江苏鑫源烟草薄片有限公司")
                       segvalue[count - 1] = "00700000";
                   else
                   {
                       segvalue[3] = opt.GetSegValue("select F_code from ht_svr_org_group where f_key = '" + segvalue[3] + "'", "F_CODE");
                       segvalue[4] = opt.GetSegValue("select F_code from ht_svr_org_group where f_key = '" + segvalue[0] + "'", "F_CODE");
                       if (segvalue[4] == "NoRecord")
                       {
                           string code;
                           if (segvalue[3] == "00700000")
                               code = "007" + opt.GetSegValue("select nvl(Max(substr(F_CODE,4,3))+1,1) as code from Ht_Svr_Org_Group where F_parentid = '00700000'", "CODE").PadLeft(3, '0') + "00";
                           else
                               code = segvalue[3].Substring(0, 6) + opt.GetSegValue("select nvl( Max(substr(F_CODE,7,2))+1,1) as code from Ht_Svr_Org_Group where F_parentid = '" + segvalue[3] + "'", "CODE").PadLeft(2, '0');
                           segvalue[4] = code;
                       }
                   }
                   string temp = opt.getMergeStr(seg, segvalue, 1, tablename);
              //     commandlist.Add(temp);
                   if (opt.UpDateOra(temp) != "Success")
                       System.Diagnostics.Debug.Write(temp);
               }
           }
        //    opt.TransactionCommand(commandlist);          
       }
    }


}

