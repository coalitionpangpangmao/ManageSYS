using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSYS.Web.StoreService;
using System.Data;
using System.Xml;
using System.Collections;
namespace MSYS.Web
{
    public class StorageOpt
    {
        protected Dictionary<string, string> team_dic = new Dictionary<string, string>();
        protected Dictionary<string, string> shift_dic = new Dictionary<string, string>();
        protected Dictionary<string, string> MIOT = new Dictionary<string, string>();
        protected Dictionary<string, string> CIOT = new Dictionary<string, string>();
        protected Dictionary<string, string> FIOT = new Dictionary<string, string>();

        public StorageOpt()
        {
            //Dictionary<string, string> team_dic = new Dictionary<string, string>();
            this.team_dic.Add("01", "700611");
            this.team_dic.Add("02", "700612");
            this.team_dic.Add("03", "700613");

            //Dictionary<string, string> shift_dic = new Dictionary<string, string>();
            this.shift_dic.Add("01", "1001");
            this.shift_dic.Add("02", "1002");
            this.shift_dic.Add("03", "1003");

            this.MIOT.Add("0", "YL");
            this.MIOT.Add("1", "YT");

            this.CIOT.Add("0", "FL");
            this.CIOT.Add("1", "FT");

            this.FIOT.Add("0", "FL");
            this.FIOT.Add("1", "FT");


        }

        public tWaOnhand queryMater(string materCode) {
            MSYS.Web.StoreService.StoreServiceInterfaceService service = new MSYS.Web.StoreService.StoreServiceInterfaceService();
            tWaOnhand[] lists = service.getOnhandNoBjPageList("", materCode, "", "", "", "", "");
            if (lists == null)
                return null;
            return lists[0];
            /*
            tWaOnhand test = new tWaOnhand();
            test.mName = "贵州长梗";
            test.cwarehouseid = "03";
            test.locationName = "货位03";
            test.warehouseName = "原料库";
            test.sumonhand = 2500000;
            return test;*/
        }

        public string InOrOut(string PZ_code,string name,string nameno)//出入库单据号及当前操作人员
        {
            MSYS.Web.StoreService.StoreServiceInterfaceService service = new MSYS.Web.StoreService.StoreServiceInterfaceService();
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string query = "select t.*,r.name as creator,s.name as modifier,q.formula_code  from ht_strg_materia t left join ht_svr_user r on r.id = t.creator_id left join ht_svr_user s on s.id = t.modify_id left join ht_qa_mater_formula q on q.prod_code = substr(t.monthplanno,9,7) where t.ORDER_SN = '" + PZ_code + "'";
            DataSet data = opt.CreateDataSetOra(query);
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                DataRow row = data.Tables[0].Rows[0];
                materInAndOutVO r = new materInAndOutVO();
                r.loginname = nameno;
                r.loginname = row["creator_id"].ToString();
                r.name = row["creator"].ToString();
                r.planNo = row["MONTHPLANNO"].ToString();
                r.prodCode = row["MONTHPLANNO"].ToString().Substring(8,7);
                try
                {
                    string masql = "select xy_prod_code from ht_pub_prod_design where prod_code = '" + r.prodCode + "'";
                    DataSet mads = opt.CreateDataSetOra(masql);
                    r.prodCode = mads.Tables[0].Rows[0][0].ToString();
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("查询xyprodcode失败");
                }
                if(row["SHIFT_CODE"].ToString()!=null && row["SHIFT_CODE"].ToString() !="")
                    r.shiftCode = this.shift_dic[row["SHIFT_CODE"].ToString()];
                if(row["TEAM_CODE"].ToString() != null && row["TEAM_CODE"].ToString() != "")
                    r.teamCode = this.team_dic[row["TEAM_CODE"].ToString()];
               // if(row["FDATE"].ToString() != null && row["FDATE"].ToString() != "")
                 //   r.FDate =Convert.ToDateTime( row["FDATE"].ToString());
                r.createId = row["CREATOR_ID"].ToString();
                //string sqlid = "select loginname from ht_svr_user where id = '" + r.createId + "'";
                //r.createId = opt.CreateDataSetOra(sqlid).Tables[0].Rows[0][0].ToString();
               // r.loginname = r.createId;
                r.creator = row["creator"].ToString();
                r.modifyId = row["Modify_ID"].ToString();
                r.modifyMan = row["modifier"].ToString();
                r.bomType = "1";
                r.modifyTime = row["MODIFY_TIME"].ToString();
                if(row["STRG_TYPE"].ToString()=="0")
                    r.vouNo = "YLLY" + row["ORDER_SN"].ToString().Substring(2);
                if(row["STRG_TYPE"].ToString() == "1")
                    r.vouNo = "YLLT" + row["ORDER_SN"].ToString().Substring(2);


                if(row["BATCHNUM"].ToString() != null && row["BATCHNUM"].ToString() != "")
                    r.batchNumber =Convert.ToDouble( row["BATCHNUM"].ToString());
                if(row["CABOSUM"].ToString() != null && row["CABOSUM"].ToString() != "")
                    r.caboSum = Convert.ToDouble(row["CABOSUM"].ToString());
                
                //if (row["PEICESSUM"].ToString() != null && row["PEICESSUM"].ToString() != "")
                    //r.piecesSum = Convert.ToDouble(row["PEICESSUM"].ToString());
                r.remark = row["REMARK"].ToString();
                if (row["PEICESSUM"].ToString() != null && row["PEICESSUM"].ToString() != "")
                    r.piecesSum = Convert.ToDouble(row["PEICESSUM"].ToString());
                if (row["STRG_TYPE"].ToString() != null && row["STRG_TYPE"].ToString() != "")
                    r.inoutType = this.MIOT[row["STRG_TYPE"].ToString()];
                r.cwarehouseid = row["WARE_HOUSE_ID"].ToString();
                if (row["FORMULA_CODE"].ToString() != null && row["FORMULA_CODE"].ToString() != "")
                    r.formulaId = Convert.ToInt64(row["formula_code"].ToString());  
                List<tShopMaterInoutSubVO> s = new List<tShopMaterInoutSubVO>();
                //DataSet details = opt.CreateDataSetOra("select t.*,r.material_name from ht_strg_mater_sub t left join ht_pub_materiel r on r.material_code = t.mater_code  where t.main_code = '" + PZ_code + "' and t.is_del = '0'");
                DataSet details = opt.CreateDataSetOra("select t.*,r.material_name from ht_strg_mater_sub t left join ht_pub_materiel r on r.material_code = t.mater_code  where t.main_code = '" + PZ_code + "' and t.is_del = '0'");
                if (details != null && details.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in details.Tables[0].Rows)
                    {
                       tWaOnhand info = queryMater(drow["MATER_CODE"].ToString());

                       // if(drow["MATER_FLAG"].ToString() == "SP" ||  drow["MATER_FLAG"].ToString() == "碎片")
                         //   continue;
                        tShopMaterInoutSubVO sub = new tShopMaterInoutSubVO();
                       if (info != null)
                        {
                           sub.materName = info.mName.ToString();//接口
                           if (info.cwarehouseid.ToString() != "")
                           {
                               sub.warehouseCode = info.cwarehouseid.ToString();//从接口获得
                               r.cwarehouseid = sub.warehouseCode;
                               sub.warehouseName = info.warehouseName.ToString();
                           }  
                           if(info.locationName.ToString()!="")
                                sub.materLocation = info.locationName.ToString();
                            //if (row["STRG_TYPE"].ToString() == "0" && (Convert.ToDouble(info.sumonhand) < Convert.ToDouble(drow["ORIGINAL_DEMAND"].ToString())))
                            //{
                              //  return "notenough" + info.materName.ToString();
                            //}
                        }
                        sub.mainId = drow["MAIN_CODE"].ToString();
                        sub.materCode = drow["MATER_CODE"].ToString();
                        //sub.materName = drow["material_name"].ToString();//接口
                        sub.materType = drow["MATER_FLAG"].ToString();//
                        sub.unitCode = drow["UNIT_CODE"].ToString();
                       // sub.unitName = drow["TEAM_CODE"].ToString();
                        sub.occurQty = drow["ORIGINAL_DEMAND"].ToString();
                        sub.remark = drow["REMARK"].ToString();
                        //sub.warehouseCode = drow["WAREHOUSECODE"].ToString();//从接口获得
                        sub.packingNumbers = drow["PACKNUM"].ToString();
                        sub.substance =drow["SUBSTANCE"].ToString();
                        sub.oddQty =  drow["ODDQTY"].ToString();
                        sub.materName = drow["MATERIAL_NAME"].ToString();
                        s.Add(sub);
                    }                    
                }
                r.subList = s.ToArray();
                DateTime d = new DateTime();
                string date =  date = DateTime.Now.ToString("yyyy-MM-dd");
                return service.yuanliaoInAndOut4ws_03(r, date, r.batchNumber).status ;
               // return "";
            }
            else
                return "Falied";
        }

        public string FuInOrOut(string PZ_code, string name, string nameno)//出入库单据号及当前操作人员
        {
            MSYS.Web.StoreService.StoreServiceInterfaceService service = new MSYS.Web.StoreService.StoreServiceInterfaceService();
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string query = "select t.*,r.name as creator,s.name as modifier,q.formula_code  from ht_strg_coat t left join ht_svr_user r on r.id = t.creator_id left join ht_svr_user s on s.id = t.modify_id left join ht_qa_mater_formula q on q.prod_code = substr(t.monthplanno,9,7) where t.ORDER_SN = '" + PZ_code + "'";
            DataSet data = opt.CreateDataSetOra(query);
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                DataRow row = data.Tables[0].Rows[0];
                materInAndOutVO r = new materInAndOutVO();
                r.loginname = nameno;
                //r.name = name;
                r.name = row["creator"].ToString();
                r.planNo = row["MONTHPLANNO"].ToString();
                r.prodCode = row["MONTHPLANNO"].ToString().Substring(8, 7);
                try
                {
                    string masql = "select xy_prod_code from ht_pub_prod_design where prod_code = '" + r.prodCode + "'";
                    DataSet mads = opt.CreateDataSetOra(masql);
                    r.prodCode = mads.Tables[0].Rows[0][0].ToString();
                }
                catch {
                    System.Diagnostics.Debug.WriteLine("查询xyprodcode失败");
                }
                if (row["SHIFT_CODE"].ToString() != null && row["SHIFT_CODE"].ToString() != "")
                    r.shiftCode = this.shift_dic[row["SHIFT_CODE"].ToString()];
                if (row["TEAM_CODE"].ToString() != null && row["TEAM_CODE"].ToString() != "")
                    r.teamCode = this.team_dic[row["TEAM_CODE"].ToString()];
                // if(row["FDATE"].ToString() != null && row["FDATE"].ToString() != "")
                //   r.FDate =Convert.ToDateTime( row["FDATE"].ToString());
                r.createId = row["CREATOR_ID"].ToString();
                r.creator = row["creator"].ToString();
                r.modifyId = row["Modify_ID"].ToString();
                r.modifyMan = row["modifier"].ToString();
                r.bomType = "2";
                r.modifyTime = row["MODIFY_TIME"].ToString();
                if (row["CABOSUM"].ToString() != null && row["CABOSUM"].ToString() != "")
                    r.caboSum = Convert.ToDouble(row["CABOSUM"].ToString());

                if (row["PEICESSUM"].ToString() != null && row["PEICESSUM"].ToString() != "")
                    r.piecesSum = Convert.ToDouble(row["PEICESSUM"].ToString());
                r.remark = row["REMARK"].ToString();
                if (row["STRG_TYPE"].ToString() != null && row["STRG_TYPE"].ToString() != "")
                    r.inoutType = this.CIOT[row["STRG_TYPE"].ToString()];
                r.cwarehouseid = row["WARE_HOUSE_ID"].ToString();
                if (row["FORMULA_CODE"].ToString() != null && row["FORMULA_CODE"].ToString() != "")
                    r.formulaId = Convert.ToInt64(row["formula_code"].ToString());  
                if (row["STRG_TYPE"].ToString() == "0")
                    r.vouNo = "FLLY" + row["ORDER_SN"].ToString().Substring(2);
                if (row["STRG_TYPE"].ToString() == "1")
                    r.vouNo = "FLLT" + row["ORDER_SN"].ToString().Substring(2);
                List<tShopMaterInoutSubVO> s = new List<tShopMaterInoutSubVO>();
               // DataSet details = opt.CreateDataSetOra("select t.*,r.material_name from ht_strg_coat_sub t left join ht_pub_materiel r on r.material_code = t.mater_code  where t.main_code = '" + PZ_code + "' and t.is_del = '0'");
                DataSet details = opt.CreateDataSetOra("select t.*,r.material_name from ht_strg_coat_sub t left join ht_pub_materiel r on r.material_code = t.mater_code  where t.main_code = '" + PZ_code + "' and t.is_del = '0'");
                if (details != null && details.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in details.Tables[0].Rows)
                    {
                        tShopMaterInoutSubVO sub = new tShopMaterInoutSubVO();
                        tWaOnhand info = queryMater(drow["MATER_CODE"].ToString());
                        if (info != null) {
                                                    sub.materName = info.mName.ToString();//接口
                           if (info.cwarehouseid.ToString() != "")
                           {
                               sub.warehouseCode = info.cwarehouseid.ToString();//从接口获得
                               r.cwarehouseid = sub.warehouseCode;
                               sub.warehouseName = info.warehouseName.ToString();
                               //System.Diagnostics.Debug.WriteLine(info.unitName.ToString());
                           }  
                           if(info.locationName.ToString()!="")
                                sub.materLocation = info.locationName.ToString();
                            //if (row["STRG_TYPE"].ToString() == "0" && (Convert.ToDouble(info.sumonhand) < Convert.ToDouble(drow["ORIGINAL_DEMAND"].ToString())))
                            //{
                              //  return "notenough" + info.materName.ToString();
                            //}
                        }

                        sub.mainId = drow["MAIN_CODE"].ToString();
                        sub.materCode = drow["MATER_CODE"].ToString();
                        //sub.materName = drow["material_name"].ToString();
                        sub.materType = drow["MATER_FLAG"].ToString();
                        sub.unitCode = drow["UNIT_CODE"].ToString();
                       // sub.unitName = drow["TEAM_CODE"].ToString();
                        sub.occurQty = drow["ORIGINAL_DEMAND"].ToString() == null ? "" : drow["ORIGINAL_DEMAND"].ToString();
                        sub.remark = drow["REMARK"].ToString();
                        //sub.warehouseCode = drow["WAREHOUSECODE"].ToString();
                        sub.packingNumbers = drow["PACKNUM"].ToString();
                        sub.substance = drow["SUBSTANCE"].ToString();
                        sub.oddQty = drow["ODDQTY"].ToString();
                        sub.materName = drow["MATERIAL_NAME"].ToString();
                        s.Add(sub);
                    }
                }
                r.subList = s.ToArray();
                string date = date = DateTime.Now.ToString("yyyy-MM-dd");
                return service.fuliaoInAndOut4ws_03(r,date).status;
                
            }
            else
                return "Falied";
        }

        public string XiangInOrOut(string PZ_code, string name, string nameno)//出入库单据号及当前操作人员
        {
            MSYS.Web.StoreService.StoreServiceInterfaceService service = new MSYS.Web.StoreService.StoreServiceInterfaceService();
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string query = "select t.*,r.name as creator,s.name as modifier,q.formula_code  from ht_strg_flavor t left join ht_svr_user r on r.id = t.creator_id left join ht_svr_user s on s.id = t.modify_id left join ht_qa_mater_formula q on q.prod_code = substr(t.monthplanno,9,7) where t.ORDER_SN = '" + PZ_code + "'";
            DataSet data = opt.CreateDataSetOra(query);
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                DataRow row = data.Tables[0].Rows[0];
                materInAndOutVO r = new materInAndOutVO();
                r.loginname = nameno;
                //r.name = name;
                r.name = row["creator"].ToString();
                r.planNo = row["MONTHPLANNO"].ToString();
                r.prodCode = row["MONTHPLANNO"].ToString().Substring(8, 7);
                try
                {
                    string masql = "select xy_prod_code from ht_pub_prod_design where prod_code = '" + r.prodCode + "'";
                    DataSet mads = opt.CreateDataSetOra(masql);
                    r.prodCode = mads.Tables[0].Rows[0][0].ToString();
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("查询xyprodcode失败");
                }
                if (row["SHIFT_CODE"].ToString() != null && row["SHIFT_CODE"].ToString() != "")
                    r.shiftCode = this.shift_dic[row["SHIFT_CODE"].ToString()];
                if (row["TEAM_CODE"].ToString() != null && row["TEAM_CODE"].ToString() != "")
                    r.teamCode = this.team_dic[row["TEAM_CODE"].ToString()];
                // if(row["FDATE"].ToString() != null && row["FDATE"].ToString() != "")
                //   r.FDate =Convert.ToDateTime( row["FDATE"].ToString());
                r.createId = row["CREATOR_ID"].ToString();
                r.creator = row["creator"].ToString();
                r.modifyId = row["Modify_ID"].ToString();
                r.bomType = "2";
                r.modifyMan = row["modifier"].ToString();
                r.modifyTime = row["MODIFY_TIME"].ToString();
                if (row["CABOSUM"].ToString() != null && row["CABOSUM"].ToString() != "")
                    r.caboSum = Convert.ToDouble(row["CABOSUM"].ToString());

                if (row["PEICESSUM"].ToString() != null && row["PEICESSUM"].ToString() != "")
                    r.piecesSum = Convert.ToDouble(row["PEICESSUM"].ToString());
                r.remark = row["REMARK"].ToString();
                if (row["STRG_TYPE"].ToString() != null && row["STRG_TYPE"].ToString() != "")
                    r.inoutType = this.FIOT[row["STRG_TYPE"].ToString()];
                r.cwarehouseid = row["WARE_HOUSE_ID"].ToString();
                if (row["FORMULA_CODE"].ToString() != null && row["FORMULA_CODE"].ToString() != "")
                    r.formulaId = Convert.ToInt64(row["formula_code"].ToString());  
                if (row["STRG_TYPE"].ToString() == "0")
                    r.vouNo = "FLLY" + row["ORDER_SN"].ToString().Substring(2,8)+"1"+row["ORDER_SN"].ToString().Substring(11,2);
                if (row["STRG_TYPE"].ToString() == "1")
                    r.vouNo = "FLLT" +  row["ORDER_SN"].ToString().Substring(2,8)+"1"+row["ORDER_SN"].ToString().Substring(11,2);
                List<tShopMaterInoutSubVO> s = new List<tShopMaterInoutSubVO>();
                //DataSet details = opt.CreateDataSetOra("select t.*,r.material_name from ht_strg_flavor_sub t left join ht_pub_materiel r on r.material_code = t.mater_code  where t.main_code = '" + PZ_code + "' and t.is_del = '0'");
                DataSet details = opt.CreateDataSetOra("select t.*,r.material_name from ht_strg_flavor_sub t left join ht_pub_materiel r on r.material_code = t.mater_code  where t.main_code = '" + PZ_code + "' and t.is_del = '0'");
                if (details != null && details.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in details.Tables[0].Rows)
                    {
                        tShopMaterInoutSubVO sub = new tShopMaterInoutSubVO();
                        tWaOnhand info = queryMater(drow["MATER_CODE"].ToString());
                        if (info != null)
                        {
                           sub.materName = info.mName.ToString();//接口
                           if (info.cwarehouseid.ToString() != "")
                           {
                               sub.warehouseCode = info.cwarehouseid.ToString();//从接口获得
                               r.cwarehouseid = sub.warehouseCode;
                               sub.warehouseName = info.warehouseName.ToString();
                           }  
                           if(info.locationName.ToString()!="")
                                sub.materLocation = info.locationName.ToString();
                           /* if (row["STRG_TYPE"].ToString() == "0" && (Convert.ToDouble(info.sumonhand) < Convert.ToDouble(drow["ORIGINAL_DEMAND"].ToString())))
                            {
                                return "notenough" + info.materName.ToString();
                            }*/
                        }

                        sub.mainId = drow["MAIN_CODE"].ToString();
                        sub.materCode = drow["MATER_CODE"].ToString();
                        //sub.materName = drow["material_name"].ToString();
                        sub.materType = drow["MATER_FLAG"].ToString();
                        sub.unitCode = drow["UNIT_CODE"].ToString();
                       // sub.unitName = drow["TEAM_CODE"].ToString();
                        sub.occurQty = drow["ORIGINAL_DEMAND"].ToString();
                        sub.remark = drow["REMARK"].ToString();
                        //sub.warehouseCode = drow["WAREHOUSECODE"].ToString();
                        
                        sub.packingNumbers = drow["PACKNUM"].ToString();
                        sub.substance = drow["SUBSTANCE"].ToString();
                        sub.oddQty = drow["ODDQTY"].ToString();
                        sub.materName = drow["MATERIAL_NAME"].ToString();
                        s.Add(sub);
                    }
                }
                r.subList = s.ToArray();
                string date = date = DateTime.Now.ToString("yyyy-MM-dd");
                return service.fuliaoInAndOut4ws_03(r, date).status;
                //return "";
            }
            else
                return "Falied";
        }


        public DataTable queryStorage(string year, string matername, string category, string matertype, string place, string warehouse)
        {
            MSYS.Web.StoreService.StoreServiceInterfaceService service = new MSYS.Web.StoreService.StoreServiceInterfaceService();
            tWaOnhand[] lists = service.getMatOnhandNoPageList(year, matername, category, matertype, place, warehouse);
            DataTable data = new DataTable();
            
            data.Columns.Add("物料名称");
            data.Columns.Add("物料编码");
            data.Columns.Add("仓库");
            data.Columns.Add("货位号");
            data.Columns.Add("类别");
            data.Columns.Add("类型");
             data.Columns.Add("产地");
            data.Columns.Add("库存(kg)");
            data.Columns.Add("使用叶组");
            data.Columns.Add("件数");
            data.Columns.Add("单重(kg)");
         
            foreach (tWaOnhand mat in lists)
            {
                string[] paras = new string[] { mat.materName, mat.materCode, mat.warehouseName, mat.clocationid, mat.typeCode, mat.categoryCode, mat.provinceCode, mat.pkOnhandnum, mat.formulaName, mat.matPack, mat.pack };
                data.Rows.Add(paras);                
            }
            return data;
         
        }

        protected  void updateWarehouse()
        {
            MSYS.Web.StoreService.StoreServiceInterfaceService service = new MSYS.Web.StoreService.StoreServiceInterfaceService();

            service.getMaterialWarehouseListCompleted += new getMaterialWarehouseListCompletedEventHandler(wareHouseUpdate_Completed);
            service.getMaterialWarehouseListAsync();

        }

        private static void wareHouseUpdate_Completed(object sender, getMaterialWarehouseListCompletedEventArgs e)
        {
            hashMap[] lists = e.Result;
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
           
           // string[] seg = { "ID", "MATTREE_CODE", "MATTREE_NAME", "IS_DEL", "PK_CLASS", "PK_PARENT_CLASS" };

            foreach (hashMap list in lists)
            {
                string[] value = {};
             //   string temp = opt.getMergeStr(seg, value, 1, "HT_PUB_MATTREE");
                //  commandlist.Add(temp);
                //if (opt.UpDateOra(temp) != "Success")
                //    System.Diagnostics.Debug.Write(temp);
            }
            
        }
    }


}

