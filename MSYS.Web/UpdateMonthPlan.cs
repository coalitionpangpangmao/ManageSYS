using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSYS.Web.EquipService;
using System.Data;
using System.Xml;
using System.Collections;
using MSYS.Web.PlanService;

namespace MSYS.Web
{
    public class UpdateMonthPlan:UpdateFromMaster
    {
        public UpdateMonthPlan()
        {

        }
        public override string InsertLocalFromMaster()
        {
            /*MSYS.Web.EquipService.EquipServiceInterfaceService service = new MSYS.Web.EquipService.EquipServiceInterfaceService();
            StringBuilder buffer = new StringBuilder();
            buffer.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            buffer.Append("<request>");
            buffer.Append("</request>");
            tEqEqpCls[] cls = service.getEquipClsList(buffer.ToString());
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
                return "未获取更新";*/

            MSYS.Web.PlanService.WsPlanForGSInterfaceService service = new MSYS.Web.PlanService.WsPlanForGSInterfaceService();
            MSYS.DAL.DbOperator opt = new DAL.DbOperator();
            prodAssignPlan[] pb = service.getProdAssignForGS("", "");
            prodAssignVO[] pvo = service.getProdAssignListForGS("", "");
            string[] seg = { "id", "PLAN_NAME", "B_FLOW_STATUS", "ISSUED_STATUS", "PLAN_TIME", "IS_VALID", "REMARK" };
            string[] seg2 = { "month_plan_id", "prod_code ", "plan_year", "prod_month", "plan_type", "plan_outpu", "plan_sort", "exe_status" };
            foreach (prodAssignPlan p in pb)
            {
                if (p.planNo.Substring(0, 2) != "GS")
                    continue;
                string[] value = { p.id, p.planName, p.bFlowStatus, p.issuedStatus, p.planTime, "1", p.remark, };
              //  string[] value2 = { p.id };
                opt.getMergeStr(seg, value, 1, "HT_PROD_MONTH_PLAN");
               // opt.getMergeStr(seg2, value2, 1, "HT_PROD_MONTH_PLAN_DETAIL");
                // dt.Rows.Add(paras);
            }
            foreach (prodAssignVO p in pvo) {
                if (p.planNo.Substring(0, 2) != "GS")
                    continue;
                string[] value = { p.planNo, p.prodCode, p.jobYear, p.jobMonth, p.planType, p.jobOutput, p.jobSort, p.status };
                opt.getMergeStr(seg2, value, 1, "HT_PROD_MONTH_PLAN_DETAIL");
            }
            return "12";
        }

        protected override void InsertLocalFromMasterAsyn()
        {
            MSYS.Web.EquipService.EquipServiceInterfaceService service = new MSYS.Web.EquipService.EquipServiceInterfaceService();
            StringBuilder buffer = new StringBuilder();
            buffer.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            buffer.Append("<request>");
            buffer.Append("</request>");

            //service.getEquipClsListCompleted += new getEquipClsListCompletedEventHandler(service_Completed);
            //service.getEquipClsListAsync(buffer.ToString());

        }

    }


}
