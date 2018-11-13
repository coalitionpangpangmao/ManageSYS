using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSYS.Web.MateriaService;
using System.Data;
using System.Xml;
using System.Collections;
using System.Diagnostics;

namespace MSYS.Web
{
    public class UpdateProduct : UpdateFromMaster
    {
        public UpdateProduct()
        {

        }

        public override string InsertLocalFromMaster()
        {

            MSYS.Web.MateriaService.WsBaseDataInterfaceService service = new MSYS.Web.MateriaService.WsBaseDataInterfaceService();
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            List<string> commandlist = new List<string>();
            productEntity[] prods = service.getAllProductList(new productEntity());
            commandlist.Clear();
            string[] matseg = { "PROD_CODE", "PROD_NAME", "PACK_NAME", "HAND_MODE", "TECH_STDD_CODE", "MATER_FORMULA_CODE", "AUX_FORMULA_CODE", "COAT_FORMULA_CODE", "REMARK", "CREATEOR_ID", "CREATE_TIME", "MODIFY_ID", "MODIFY_TIME", "STANDARD_VALUE", "XY_PROD_CODE" };
            int SucCount = 0;

            foreach (productEntity prod in prods)
            {
                
                string[] value = { prod.prodCode, prod.prodName, prod.packName, prod.handMode, prod.techStddId, prod.materFormulaId, prod.auxFormulaId, prod.coatFormulaId, prod.remark, prod.createorId, prod.createTime.ToString("yyyy-MM-dd HH:mm:ss"), prod.modifyId, prod.modifyTime.ToString("yyyy-MM-dd HH:mm:ss"), prod.standardValue, prod.xyProdCode };

                string temp = opt.getMergeStr(matseg, value, 1, "HT_PUB_PROD_DESIGN");
                commandlist.Add(temp);
                if (opt.UpDateOra(temp) != "Success")
                    System.Diagnostics.Debug.Write(temp);
                commandlist.Concat(getTechstdd_SQL(prod.techStddId,prod.prodCode)); // java.math.BigInteger cannot be cast to java.lang.String
                commandlist.Concat(getMaterFormalu_SQL(prod.materFormulaId, prod.prodCode));
                commandlist.Concat(getAuxFormalu_SQL(prod.auxFormulaId, prod.prodCode)); //java.math.BigDecimal cannot be cast to java.lang.Double
                commandlist.Concat(getCoatFormalu_SQL(prod.coatFormulaId, prod.prodCode));
                if (opt.TransactionCommand(commandlist) == "Success")
                {
                    System.Diagnostics.Debug.Write("产品更新成功" + prod.prodCode + prod.prodName);
                    SucCount++;
                }
            }
            return SucCount.ToString() + "项产品更新成功,总记录条数：" + prods.Length;

        }
        //根据产品ID获取工艺标准信息并更新。
        protected List<string> getTechstdd_SQL(string id,string prodcode)
        {
            MSYS.Web.MateriaService.WsBaseDataInterfaceService service = new MSYS.Web.MateriaService.WsBaseDataInterfaceService();
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            List<string> commandlist = new List<string>();
            try
            {
                techStddInfoVO tech = service.getTechStdById(id);
          
           if (tech != null)
                {
                    tQaTechStdd info = tech.techStddInfo;
                    string[] seg = { "ID", "TECH_CODE", "TECH_NAME", "STANDARD_VOL", "REMARK", "PROD_CODE", "MODIFY_ID", "MODIFY_TIME", "IS_VALID", "IS_DEL", "E_DATE", "CREATE_ID", "CREATE_DEPT_ID", "CREATE_DATE", "CONTROL_STATUS", "B_DATE" };
                    string[] value = { id, info.standardCode, info.techStddName, info.standardVol, info.remark, prodcode, info.modifyId, info.modifyTime.ToString("yyyy-MM-dd HH:mm:ss"), info.isValid, info.isDel, info.EDate.ToString("yyyy-MM-dd HH:mm:ss"), info.createId, info.createDept, info.createDate.ToString("yyyy-MM-dd HH:mm:ss"), info.controlStatus, info.BDate.ToString("yyyy-MM-dd HH:mm:ss") };
                  
                    string temp = opt.getMergeStr(seg, value, 2, "HT_TECH_STDD_CODE");
                    commandlist.Add(temp);
                    if (opt.UpDateOra(temp) != "Success")
                        System.Diagnostics.Debug.Write(temp);
                    if (tech.techStdDetails != null && tech.techStdDetails.Length > 0)
                    {
                        string[] subseg = { "ID", "TECH_CODE", "PARA_CODE", "PARA_TYPE", "REMARK", "VALUE", "UPPER_LIMIT", "LOWER_LIMIT", "UNIT" };
                        foreach (techStddDetail detail in tech.techStdDetails)
                        {
                            if (detail.baseDown != "" && detail.baseUp != "")
                            {
                                string stdvalue = detail.centerVal == "" ? ((Convert.ToDouble(detail.baseDown) + Convert.ToDouble(detail.baseUp)) / 2).ToString() : detail.centerVal;
                                string[] subvalue = { detail.id, id, detail.projCode, detail.techParmType, detail.remark, stdvalue, detail.baseUp, detail.baseDown, detail.bzUnit };
                                temp = opt.getMergeStr(subseg, subvalue, 1, "HT_TECH_STDD_CODE_DETAIL");
                                commandlist.Add(temp);
                                if (opt.UpDateOra(temp) != "Success")
                                    System.Diagnostics.Debug.Write(temp);
                            }
                        }

                    }
                    /*            "TECH_STDD_ID" IS '工艺技术标准主表id';
                "TECH_PRAM_TYPE" IS '技术参数类型';
                "PROJ_CODE" IS '工序项目编码';
                "PARM_STDD" IS '工艺参数或质量标准';
                "CENTER_VAL" IS '中心值';
                "BIAS_UP" IS '上偏值';
                "BIAS_DOWN" IS '下偏值';
                "STDD_VALUE" IS '工艺标准值';
                "PASS_PERCENT" IS '合格率';
                "BZ_UNIT" IS '计量单位';
                "SORT" IS '排序';
                "REMARK" IS '备注';*/
                }
           return commandlist;
            }
             catch (Exception ee)
            {
                return null;
            }
          
           
        }

        protected List<string> getMaterFormalu_SQL(string id, string prodCode)
        {
            MSYS.Web.MateriaService.WsBaseDataInterfaceService service = new MSYS.Web.MateriaService.WsBaseDataInterfaceService();
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            List<string> commandlist = new List<string>();
            materFormulaVO info = service.getMatFormulaById(id);
            string temp;
            if (info != null)
            {
                string[] seg = { "ID", "FORMULA_CODE", "FORMULA_NAME", "ADJUST", "B_DATE", "CABO_SUM", "CONTROL_STATUS", "CREATE_DATE", "CREATE_DEPT_ID", "CREATE_ID", "E_DATE", "EXECUTEBATCH", "FLOW_STATUS" , "IS_DEL", "IS_VALID", "MODIFY_ID", "MODIFY_TIME", "PIECE_NUM", "PIECES_SUM", "PROD_CODE", "REMARK", "SMALLS_NUM", "STANDARD_VOL", "STEM_NUM", "STICKS_NUM"};
                string[] value = { id,info.formulaCode,info.formulaName,info.adjust,info.BDate.ToString("yyyy-MM-dd HH:mm:ss"),info.caboSum.ToString(),info.controlStatus,info.createDate,info.createDept,info.createId,info.EDate.ToString("yyyy-MM-dd HH:mm:ss"),info.executeBatch.ToString(),info.flowStatus,info.isDel,info.isValid,
                                     info.modifyId,info.modifyTime,info.pieceNum.ToString(),info.piecesSum.ToString(),prodCode,info.remark,info.smallsNum.ToString(),info.standardVol,info.stemNum.ToString(),info.sticksNum.ToString()};
                temp =opt.getMergeStr(seg, value, 2, "HT_QA_MATER_FORMULA");
                commandlist.Add(temp);
                if (opt.UpDateOra(temp) != "Success")
                System.Diagnostics.Debug.Write(temp); 

               
                if(info.ygSubList != null && info.ygSubList.Length >0)
                {
                     string[] subseg = {"ID", "MATER_CODE", "BATCH_SIZE", "FRONT_GROUP", "IS_DEL", "MATER_FLAG", "FORMULA_CODE", "MATER_SORT", "REMARK"};
                     foreach(tQaMaterFormulaDetail detail in info.ygSubList)
                    {                    
                    string[] subvalue = {detail.id.ToString(),detail.materCode,detail.batchSize.ToString(),detail.frontGroup,detail.isDel,detail.materFlag,id,detail.materSort.ToString(),detail.remark };
                       temp = opt.getMergeStr(subseg,subvalue,2,"HT_QA_MATER_FORMULA_DETAIL");
                       commandlist.Add(temp);
                       if (opt.UpDateOra(temp) != "Success")
                           System.Diagnostics.Debug.Write(temp); 
                    }
                }
                if (info.spSubList != null && info.spSubList.Length > 0)
                {
                    string[] subseg = { "ID", "MATER_CODE", "BATCH_SIZE", "FRONT_GROUP", "IS_DEL", "MATER_FLAG", "FORMULA_CODE", "MATER_SORT", "REMARK" };
                    foreach (tQaMaterFormulaDetail detail in info.spSubList)
                    {
                        string[] subvalue = { detail.id.ToString(), detail.materCode, detail.batchSize.ToString(), detail.frontGroup, detail.isDel, detail.materFlag, id,detail.materSort.ToString(), detail.remark };
                        temp = opt.getMergeStr(subseg, subvalue, 2, "HT_QA_MATER_FORMULA_DETAIL");
                        commandlist.Add(temp);
                        if (opt.UpDateOra(temp) != "Success")
                            System.Diagnostics.Debug.Write(temp); 
                    }
                }       
            }
            return commandlist;
        }


        protected List<string> getAuxFormalu_SQL(string id, string prodCode)
        {
            MSYS.Web.MateriaService.WsBaseDataInterfaceService service = new MSYS.Web.MateriaService.WsBaseDataInterfaceService();
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            List<string> commandlist = new List<string>();
            try
            {
                auxFormulaVO info = service.getAuxFormulaById(id);
                string temp;
                if (info != null)
                {
                    string[] seg = { "ID", "FORMULA_CODE", "FORMULA_NAME", "B_DATE", "CONTROL_STATUS", "CREATE_DATE", "CREATE_DEPT_ID", "CREATE_ID", "E_DATE", "IS_DEL", "IS_VALID", "MODIFY_ID", "MODIFY_TIME", "PROD_CODE", "REMARK", "STANDARD_VOL" };
                    string[] value = {id, info.formulaCode, info.formulaName, info.BDate.ToString("yyyy-MM-dd HH:mm:ss"), info.controlStatus, info.createDate, info.createDept, info.createId, info.EDate.ToString("yyyy-MM-dd HH:mm:ss"), info.isDel, info.isValid, info.modifyId, info.modifyTime, prodCode, info.remark, info.standardVol };
                    temp = opt.getMergeStr(seg, value, 2, "HT_QA_AUX_FORMULA");
                    commandlist.Add(temp);
                    if (opt.UpDateOra(temp) != "Success")
                        System.Diagnostics.Debug.Write(temp);
                    if (info.auxSubList != null && info.auxSubList.Length > 0)
                    {
                        string[] subseg = { "ID", "MATER_CODE", "FORMULA_CODE", "AUX_PERCENT", "AUX_SCALE", "AUX_SORT", "IS_DEL", "MATER_TYPE", "mattreeName", "REMARK" };
                        foreach (tQaAuxFormulaDetail detail in info.auxSubList)
                        {
                            string[] subvalue = { detail.id.ToString(), detail.materCode, id, detail.auxPercent.ToString(), detail.auxScale.ToString(), detail.auxSort.ToString(), detail.isDel, detail.materType, detail.mattreeName, detail.remark };
                            temp = opt.getMergeStr(subseg, subvalue, 2, "HT_QA_AUX_FORMULA_DETAIL");
                            commandlist.Add(temp);
                            if (opt.UpDateOra(temp) != "Success")
                                System.Diagnostics.Debug.Write(temp);
                        }
                    }
                }
                return commandlist;
            }
            catch (Exception ee)
            {
                string msg = ee.Message;
                return null;
            }
           
        }


        protected List<string> getCoatFormalu_SQL(string id, string prodCode)
        {
            MSYS.Web.MateriaService.WsBaseDataInterfaceService service = new MSYS.Web.MateriaService.WsBaseDataInterfaceService();
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            List<string> commandlist = new List<string>();
            try
            {
                coatFormulaVO info = service.getCoatFormulaById(id);
                string temp;
                if (info != null)
                {
                    string[] seg = { "ID", "FORMULA_CODE", "FORMULA_NAME", "B_DATE", "CONTROL_STATUS", "CREATE_DATE", "CREATE_DEPT_ID", "CREATE_ID", "E_DATE", "IS_DEL", "IS_VALID", "MODIFY_ID", "MODIFY_TIME", "PROD_CODE", "REMARK", "STANDARD_VOL", "FORMULA_TPY", "FORMULA_XJ", "W_TOTAL" };
                    string[] value = { id, info.formulaCode, info.formulaName, info.BDate.ToString("yyyy-MM-dd HH:mm:ss"), info.controlStatus, info.createDate, info.createDept, info.createId, info.EDate.ToString("yyyy-MM-dd HH:mm:ss"), info.isDel, info.isValid, info.modifyId, info.modifyTime, prodCode, info.remark, info.standardVol, info.formulaTpy.ToString(), info.formulaXj.ToString(), info.WTotal.ToString() };
                    temp = opt.getMergeStr(seg, value, 2, "HT_QA_COAT_FORMULA");
                    commandlist.Add(temp);
                    if (opt.UpDateOra(temp) != "Success")
                        System.Diagnostics.Debug.Write(temp);

                    if (info.coatTBYSubList != null && info.coatTBYSubList.Length > 0)
                    {
                        string[] subseg = { "ID", "MATER_CODE", "CLASS_NAME", "COAT_FLAG", "FORMULA_CODE", "COAT_SCALE", "COAT_SORT", "IS_DEL", "IS_VALID", "NEED_SIZE", "REMARK" };
                        foreach (tQaCoatFormulaDetail detail in info.coatTBYSubList)
                        {
                            string[] subvalue = { detail.id.ToString(), detail.classCode, detail.className, detail.coatFlag, id, detail.coatScale, detail.coatSort.ToString(), detail.isDel, detail.isValid, detail.needSize.ToString(), detail.remark };
                            temp = opt.getMergeStr(subseg, subvalue, 2, "HT_QA_COAT_FORMULA_DETAIL");
                            commandlist.Add(temp);
                            if (opt.UpDateOra(temp) != "Success")
                                System.Diagnostics.Debug.Write(temp);
                        }
                    }
                    if (info.coatXJSubList != null && info.coatXJSubList.Length > 0)
                    {
                        string[] subseg = { "ID", "MATER_CODE", "CLASS_NAME", "COAT_FLAG", "FORMULA_CODE", "COAT_SCALE", "COAT_SORT", "IS_DEL", "IS_VALID", "NEED_SIZE", "REMARK" };
                        foreach (tQaCoatFormulaDetail detail in info.coatXJSubList)
                        {
                            string[] subvalue = { detail.id.ToString(), detail.classCode, detail.className, detail.coatFlag,id, detail.coatScale, detail.coatSort.ToString(), detail.isDel, detail.isValid, detail.needSize.ToString(), detail.remark };
                            temp = opt.getMergeStr(subseg, subvalue, 2, "HT_QA_COAT_FORMULA_DETAIL");
                            commandlist.Add(temp);
                            if (opt.UpDateOra(temp) != "Success")
                                System.Diagnostics.Debug.Write(temp);
                        }
                    }
                }
                return commandlist;
            }
            catch (Exception ee)
            {
                return null;
            }
        }
    }


}

