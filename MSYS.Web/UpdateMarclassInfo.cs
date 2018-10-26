using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSYS.Web.MateriaService;
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
             return "";
         }
         public override string InsertLocalFromMaster()
         {
             try
             {
                 MSYS.Web.MateriaService.WsBaseDataInterfaceService service = new MSYS.Web.MateriaService.WsBaseDataInterfaceService();
                 MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                 treeVO[] mattree = service.materialTree();
                 List<string> commandlist = new List<string>();
                 string[] seg = { "ID", "MATTREE_CODE", "MATTREE_NAME", "IS_DEL", "PK_CLASS", "PK_PARENT_CLASS" };
                 foreach (treeVO leaf in mattree)
                 {
                     string[] value = { leaf.id, leaf.classCode, leaf.name, "0", leaf.classCode, leaf.pId };
                     commandlist.Add(opt.getMergeStr(seg, value, 1, "HT_PUB_MATTREE"));

                 }
                 if (opt.TransactionCommand(commandlist) == "Success" && opt.UpDateOra("update ht_pub_mattree t set t.parent_code = (select mattree_code from ht_pub_mattree r where r.id = t.pk_parent_class)") == "Success")
                     return "Success";
                 else return "failed";

             }
             catch (Exception error)
             {
                 return error.Message;
             }


         }
    }


}

