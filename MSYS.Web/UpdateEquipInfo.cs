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
    public class UpdateEquipInfo:UpdateFromMaster
    {
        public UpdateEquipInfo()
        {
          
        }
       
         public override string InsertLocalFromMaster()
         {            
             MSYS.Web.EquipService.EquipServiceInterfaceService service = new MSYS.Web.EquipService.EquipServiceInterfaceService();
             StringBuilder buffer = new StringBuilder();
           
             tEqEqpTbl[] tbs = service.getEquipList(new tEqEqpTbl());
             if (tbs.Length > 0)
             {
                 MSYS.DAL.DbOperator opt = new DAL.DbOperator();
                 List<string> commandlist = new List<string>();
                 string[] seg = { "IDKEY", "CLS_CODE",  "CREATE_TIME", "CREATOR", "DUTY_NAME","EQ_MODEL","EQ_NAME","EQP_FROM","EQP_IP","EQP_MAC","EQP_SN","EQP_SYS","EQ_STATUS","EQ_TYPE","FLAG","IS_DEL","IS_MADEINCHINA","IS_SPEC_EQP","MANUFACTURER","MGT_DEPT_CODE","NC_CODE","NET_WORTH","ORI_OWNER_NAME","ORI_WORTH","OWNER_NAME","POWER_UNIT","RATED_POWER","REAL_POWER","REMARK","SERIAL_NUMBER","SGS_CODE","SUPPLIER","USED_DATE","USE_DEPT_CODE","ZG_DATE","FINANCE_EQ_NAME" };
                 foreach (tEqEqpTbl item in tbs)
                 {
                     
                     string[] value = {item.idkey,item.clsCode,item.createTime,item.creator,item.dutyName,item.eqModel,item.eqName,item.eqpFrom,item.eqpIp,item.eqpMac,item.eqpSn,item.eqpSys,item.eqStatus,item.eqType,item.flag.ToString(),item.isDel,item.isMadeinchina,item.isSpecEqp,item.manufacturer,item.mgtDeptCode,item.ncCode,item.netWorth,item.oriOwnerName,item.oriWorth,item.ownerName,item.powerUnit,item.ratedPower,item.realPower,item.remark,item.serialNumber,item.sgsCode,item.supplier,item.usedDate,item.useDeptCode,item.zgDate,item.financeEqName,};
                     string temp = opt.getMergeStr(seg, value, 1, "HT_EQ_EQP_TBL");
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
            service.getEquipListCompleted += new  getEquipListCompletedEventHandler(service_Completed);
            service.getEquipListAsync(new tEqEqpTbl());

        }

          private static void service_Completed(object sender, getEquipListCompletedEventArgs e)
          {
              tEqEqpTbl[] tbs = e.Result ;
              if (tbs.Length > 0)
              {
                  MSYS.DAL.DbOperator opt = new DAL.DbOperator();
                //  List<string> commandlist = new List<string>();
                  string[] seg = { "IDKEY", "CLS_CODE", "CREATE_TIME", "CREATOR", "DUTY_NAME", "EQ_MODEL", "EQ_NAME", "EQP_FROM", "EQP_IP", "EQP_MAC", "EQP_SN", "EQP_SYS", "EQ_STATUS", "EQ_TYPE", "FLAG", "IS_DEL", "IS_MADEINCHINA", "IS_SPEC_EQP", "MANUFACTURER", "MGT_DEPT_CODE", "NC_CODE", "NET_WORTH", "ORI_OWNER_NAME", "ORI_WORTH", "OWNER_NAME", "POWER_UNIT", "RATED_POWER", "REAL_POWER", "REMARK", "SERIAL_NUMBER", "SGS_CODE", "SUPPLIER", "USED_DATE", "USE_DEPT_CODE", "ZG_DATE", "FINANCE_EQ_NAME" };
                  foreach (tEqEqpTbl item in tbs)
                  {

                      string[] value = { item.idkey, item.clsCode, item.createTime, item.creator, item.dutyName, item.eqModel, item.eqName, item.eqpFrom, item.eqpIp, item.eqpMac, item.eqpSn, item.eqpSys, item.eqStatus, item.eqType, item.flag.ToString(), item.isDel, item.isMadeinchina, item.isSpecEqp, item.manufacturer, item.mgtDeptCode, item.ncCode, item.netWorth, item.oriOwnerName, item.oriWorth, item.ownerName, item.powerUnit, item.ratedPower, item.realPower, item.remark, item.serialNumber, item.sgsCode, item.supplier, item.usedDate, item.useDeptCode, item.zgDate, item.financeEqName, };
                      string temp = opt.getMergeStr(seg, value, 1, "HT_EQ_EQP_TBL");
                   //   commandlist.Add(temp);
                      if (opt.UpDateOra(temp) != "Success")
                          System.Diagnostics.Debug.Write(temp);
                  }
                //  return opt.TransactionCommand(commandlist);
              }
             
          }
    }


}

