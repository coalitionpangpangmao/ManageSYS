using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSYS.Web.EquipService;
using System.Data;
using System.Xml;
using System.Collections;
namespace MSYS.Web
{
    public class UpdateEquipCLS:UpdateFromMaster
    {
        public UpdateEquipCLS()
        {
            
        }

        public override string InsertLocalFromMaster()
        {
            MSYS.Web.EquipService.EquipServiceInterfaceService service = new MSYS.Web.EquipService.EquipServiceInterfaceService();
            StringBuilder buffer = new StringBuilder();
            buffer.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            buffer.Append("<request>");
            buffer.Append("</request>");
            tEqEqpCls[] cls=  service.getEquipClsList(buffer.ToString());
            if (cls.Length > 0)
            {
                MSYS.DAL.DbOperator opt = new DAL.DbOperator();
                List<string> commandlist = new List<string>();
                string[] seg = { "ID_KEY", "NODE_NAME", "NODE_VALUE", "PARENT_ID", "PATH", "TYPE" };
                foreach (tEqEqpCls item in cls)
                {
                    string[] value = { item.idKey, item.nodeName, item.nodeValue, item.parentId, item.path, item.type };
                    string temp = opt.getMergeStr(seg, value, 1, "HT_EQ_EQP_CLS");
                    commandlist.Add(temp);
                    if (opt.UpDateOra(temp) != "Success")
                        System.Diagnostics.Debug.Write(temp); 
                }
                return opt.TransactionCommand(commandlist);
            }
            else
                return "未获取更新";
        }

        protected override void InsertLocalFromMasterAsyn()
        {
           MSYS.Web.EquipService.EquipServiceInterfaceService service = new MSYS.Web.EquipService.EquipServiceInterfaceService();
            StringBuilder buffer = new StringBuilder();
            buffer.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            buffer.Append("<request>");
            buffer.Append("</request>");
           
            service.getEquipClsListCompleted += new getEquipClsListCompletedEventHandler(service_Completed);
            service.getEquipClsListAsync(buffer.ToString());

        }

         private static void service_Completed(object sender, getEquipClsListCompletedEventArgs e)
         {
             tEqEqpCls[] cls = e.Result;
             if (cls.Length > 0)
             {
                 MSYS.DAL.DbOperator opt = new DAL.DbOperator();
               //  List<string> commandlist = new List<string>();
                 string[] seg = { "ID_KEY", "NODE_NAME", "NODE_VALUE", "PARENT_ID", "PATH", "TYPE" };
                 foreach (tEqEqpCls item in cls)
                 {
                     string[] value = { item.idKey, item.nodeName, item.nodeValue, item.parentId, item.path, item.type };
                     string temp = opt.getMergeStr(seg, value, 1, "HT_EQ_EQP_CLS");
                //     commandlist.Add(temp);
                     if (opt.UpDateOra(temp) != "Success")
                         System.Diagnostics.Debug.Write(temp);
                 }
                // return opt.TransactionCommand(commandlist);

             }
             
         }
    }


}

