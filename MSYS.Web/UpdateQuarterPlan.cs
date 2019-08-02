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
    public class UpdateQuarterPlan : UpdateFromMaster
    {
        public UpdateQuarterPlan() { 
        }

        public override string InsertLocalFromMaster()
        {
           MSYS.Web.PlanService.WsPlanForGSInterfaceService service = new MSYS.Web.PlanService.WsPlanForGSInterfaceService();
            MSYS.DAL.DbOperator opt = new DAL.DbOperator();
            quarterPlanVO[] pb = service.getQuarterPlanList("", "");
            prodAssignVO[] pvo = service.getProdAssignListForGS("", "");
            string[] seg = { "id", "PLAN_NAME", "B_FLOW_STATUS", "ISSUED_STATUS", "PLAN_YEAR","QUATER", "IS_VALID", "REMARK" };
            string[] seg2 = { "QUARTER_PLAN_ID", "prod_code ", "plan_OUTPUT_1", "PLAN_OUTPUT_2", "PLAN_OUTPUT_3", "TOTAL_OUTPUT", "IS_DEL" };
            foreach (quarterPlanVO p in pb)
            {
                string[] value = { p.quarterPlan.id, p.quarterPlan.planName, p.quarterPlan.flowStatus, p.quarterPlan.issuedStatus,p.quarterPlan.planYear, p.quarterPlan.quarter, "1", p.quarterPlan.remark, };
                //  string[] value2 = { p.id };
                opt.getMergeStr(seg, value, 1, "HT_PROD_SEASON_PLAN");
                foreach(tAmQuarterPlanDetail pd in p.subList){
                   string[] value2 = {p.quarterPlan.id, pd.prodCode, pd.planOutput1.ToString(), pd.planOutput2.ToString(), pd.planOutput3.ToString(), pd.totalOutput.ToString(), pd.isDel};
                    opt.getMergeStr(seg2, value2, 1, "HT_PROD_SEASON_PLAN_DETAIL");
                }
                // opt.getMergeStr(seg2, value2, 1, "HT_PROD_MONTH_PLAN_DETAIL");
                // dt.Rows.Add(paras);
            }
            return "12";
        }

        protected override void InsertLocalFromMasterAsyn()
        {
           /* MSYS.Web.EquipService.EquipServiceInterfaceService service = new MSYS.Web.EquipService.EquipServiceInterfaceService();
            StringBuilder buffer = new StringBuilder();
            buffer.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            buffer.Append("<request>");
            buffer.Append("</request>");

            //service.getEquipClsListCompleted += new getEquipClsListCompletedEventHandler(service_Completed);
            //service.getEquipClsListAsync(buffer.ToString());*/

        }
    }
}
