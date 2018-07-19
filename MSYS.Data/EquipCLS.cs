using System;
using System.Collections.Specialized;
using System.Data;
using System.Collections.Generic;
using MSYS.DAL;
namespace MSYS.Data
{
    // 摘要:
    //     系统用户对象
    [Serializable]
    public class EquipCLS : SysBaseObject
    {
        private List<EquipCLS> childCls;
       
        public EquipCLS()
        {
            this.id = string.Empty;
            this.name = string.Empty;
            this.childCls = null;
        }
        public EquipCLS(string id)
        {
            CreateObjfromDB(id);
        }

        public List<EquipCLS> children 
        {
            get
            {
                return this.childCls;
            }
            set
            {
                this.childCls = value;
            }
        }     

        protected override void CreateObjfromDB(string id)
        {
            DbOperator opt = new DbOperator();
            DataSet data = opt.CreateDataSetOra("select * from ht_eq_eqp_cls where id_key ='" + id  +"' and is_del ='0'");
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                DataRow row = data.Tables[0].Rows[0];
                this.id = row["ID_KEY"].ToString();
                this.name = row["NODE_NAME"].ToString();
                DataSet subdata = opt.CreateDataSetOra("select * from ht_eq_eqp_cls where parent_id ='" + row["ID_KEY"].ToString() + "' and is_del = '0'");
                if (subdata != null && subdata.Tables[0].Rows.Count > 0)
                {
                    this.childCls = new List<EquipCLS>();
                    foreach( DataRow subrow in subdata.Tables[0].Rows)
                    this.childCls.Add(new EquipCLS(subrow["ID_KEY"].ToString()));
                }             
            }
        }
    }
}

