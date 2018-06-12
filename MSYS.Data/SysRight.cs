using System;
using System.Text;
using System.Data;
using MSYS.DAL;
namespace MSYS.Data
{
    // 摘要:
    //     系统权限对象
    [Serializable]
    public class SysRight : SysBaseObject
    {
        
        public enum RightType:uint { Menu, Button}

        public RightType eType;
        public string mapID;
        // 摘要:
        //     初始化权限对象
        public SysRight()
        {
        }
        //
        // 摘要:
        //     初始化权限对象
        //
        // 参数:
        //   sysRoleRow:
        //     权限数据行
        public SysRight(DataRow sysRightRow)
        {
            createRightFromDatarow(sysRightRow);
        }
        
        //
        // 摘要:
        //     初始化权限对象
        //
        // 参数:
        //   sysRoleId:
        //     权限ID
        public SysRight(string sysRightId)
        {
            IDbOperator opt = new OracleOperator();
            DataSet data = opt.CreateDataSet("select * from HT_SVR_SYS_MENU where F_ID = '" + sysRightId + "'");
            if(data != null && data.Tables[0].Rows.Count > 0)
                createRightFromDatarow(data.Tables[0].Rows[0]);
        }


        private void createRightFromDatarow(DataRow sysRightRow)
        {
            if ((sysRightRow != null) && (sysRightRow.ItemArray.Length > 0))
            {
                this.Id = sysRightRow["F_ID"].ToString();
                this.Name = sysRightRow["F_MENU"].ToString();
                this.eType = (RightType)Convert.ToUInt16(sysRightRow["F_TYPE"]);
                this.mapID = sysRightRow["F_MAPID"].ToString();
            }
        }



     
    }
}
