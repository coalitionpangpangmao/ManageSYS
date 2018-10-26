using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSYS.Web.MateriaService;
using System.Data;
using System.Xml;
using System.Collections;
namespace MSYS.Web
{
    public class UpdateMaterialInfo:UpdateFromMaster
    {
        public UpdateMaterialInfo()
        {
          
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
                 
                 List<string> commandlist = new List<string>();
                 string[] seg = { "ID", "MATTREE_CODE", "MATTREE_NAME", "IS_DEL", "PK_CLASS", "PK_PARENT_CLASS" };
                 
                 tPubMateriel[] mat = service.getAllMaterialtList();
                 commandlist.Clear();
                 string[] matseg ={ "ID","MATERIAL_CODE","MATERIAL_NAME","TYPE_CODE","SPEC_VAL","MODEL_VAL","IS_VALID","IS_DEL",
                             "TYPE_FLAG","UNIT_CODE","DATA_ORIGIN_FLAG","PK_MATERIAL","FACTORY","MAT_YEAR","MAT_CATEGORY","MAT_TYPE",
                             "MAT_LEVEL","MAT_VARIETY","MAT_PACK","MAT_PLACE","REMARK","MAT_TYPE2","MAT_PLACE_NAME","MAT_PROVINCE",
                             "MAT_CITY","PK_MARBASCLASS","LAST_UPDATE_TIME","COSTPRICE","XY_MATERIAL_CODE","PK_MATTAXES","PIECE_WEIGHT"};
                 foreach (tPubMateriel materia in mat)
                 {
                     string[] value = {materia.id.ToString(),materia.materialCode,materia.materialName,materia.typeCode,materia.specVal,materia.modelVal,materia.isValid,materia.isDel,
                                 materia.typeFlag,materia.unitCode,materia.dataOriginFlag, materia.pkMaterial,materia.factory,materia.matYear,materia.matCategory,materia.matType,
                                 materia.matLevel,materia.matVariety,materia.matPack,materia.matPlace,materia.remark,materia.matType2,materia.matPlaceName,materia.matProvince,
                                 materia.matCity, materia.pkMarbasclass,System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),materia.costprice.ToString(),materia.xyMaterialCode,materia.pkMattaxes, materia.pieceWeight};
                     commandlist.Add(opt.getMergeStr(matseg, value, 2, "HT_PUB_MATERIEL"));
                 }
                return opt.TransactionCommand(commandlist);

             }
             catch (Exception error)
             {
                 return error.Message;
             }


         }
    }


}

