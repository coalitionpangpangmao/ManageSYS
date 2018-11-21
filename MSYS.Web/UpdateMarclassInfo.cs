using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSYS.Web.MateriaService;
using System.Xml;
using System.Collections;
namespace MSYS.Web
{
    public class UpdateMarclassInfo : UpdateFromMaster
    {
        public UpdateMarclassInfo()
        {

        }

        public override string InsertLocalFromMaster()
        {

            MSYS.Web.MateriaService.WsBaseDataInterfaceService service = new MSYS.Web.MateriaService.WsBaseDataInterfaceService();
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            treeVO[] mattree = service.materialTree();
            List<string> commandlist = new List<string>();
            string[] seg = { "ID", "MATTREE_CODE", "MATTREE_NAME", "IS_DEL", "PK_CLASS", "PK_PARENT_CLASS" };

            foreach (treeVO leaf in mattree)
            {
                string[] value = { leaf.id, leaf.classCode, leaf.name, "0", leaf.classCode, leaf.pId };
                string temp = opt.getMergeStr(seg, value, 1, "HT_PUB_MATTREE");
                commandlist.Add(temp);
                if (opt.UpDateOra(temp) != "Success")
                    System.Diagnostics.Debug.Write(temp);
            }
            commandlist.Add("update ht_pub_mattree t set t.parent_code = (select mattree_code from ht_pub_mattree r where r.id = t.pk_parent_class)");
            return opt.TransactionCommand(commandlist);
        }

        protected override void InsertLocalFromMasterAsyn()
        {
            MSYS.Web.MateriaService.WsBaseDataInterfaceService service = new MSYS.Web.MateriaService.WsBaseDataInterfaceService();

            service.materialTreeCompleted += new materialTreeCompletedEventHandler(service_Completed);
            service.materialTreeAsync();

        }

        private static void service_Completed(object sender, materialTreeCompletedEventArgs e)
        {
            treeVO[] mattree = e.Result;
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            //   List<string> commandlist = new List<string>();
            string[] seg = { "ID", "MATTREE_CODE", "MATTREE_NAME", "IS_DEL", "PK_CLASS", "PK_PARENT_CLASS" };

            foreach (treeVO leaf in mattree)
            {
                string[] value = { leaf.id, leaf.classCode, leaf.name, "0", leaf.classCode, leaf.pId };
                string temp = opt.getMergeStr(seg, value, 1, "HT_PUB_MATTREE");
                //  commandlist.Add(temp);
                if (opt.UpDateOra(temp) != "Success")
                    System.Diagnostics.Debug.Write(temp);
            }
            opt.UpDateOra("update ht_pub_mattree t set t.parent_code = (select mattree_code from ht_pub_mattree r where r.id = t.pk_parent_class)");
            // return opt.TransactionCommand(commandlist);
        }
    }


}

