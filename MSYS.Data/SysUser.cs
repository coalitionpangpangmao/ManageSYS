using System;
using System.Collections.Specialized;
using System.Data;
using MSYS.DAL;
namespace MSYS.Data
{
    // 摘要:
    //     系统用户对象
    [Serializable]
    public class SysUser : SysBaseObject
    {
        // 摘要:
        //     初始化用户对象
        public SysUser()
        {
            this.Id = string.Empty;
            this.Name = string.Empty;
            this.IsAdmin = false;
            this.IsDisabled = true;
            this.LoginName = string.Empty;
            this.OwningBusinessUnitId = string.Empty;
            this.UserRoleID = string.Empty;
            UserRights = new SysRightCollection();
            this.UserRole = string.Empty;
        }
        //
        // 摘要:
        //     初始化用户对象
        //
        // 参数:
        //   sysUserRow:
        //     用户信息数据行
        public SysUser(DataRow sysUserRow)
        {
             CreateUserFromRow( sysUserRow);
        }
        //
        // 摘要:
        //     初始化用户对象
        //
        // 参数:
        //   sysUserId:
        //     用户Id
        public SysUser(string sysUserId)
        {
            DbOperator opt = new DbOperator();
            DataSet data = opt.CreateDataSetOra("select *   from HT_SVR_USER r left join ht_svr_sys_role t on r.role = t.f_id  where r.id = '" + sysUserId + "'");
            if(data != null && data.Tables[0].Rows.Count >0)
            {
                CreateUserFromRow(data.Tables[0].Rows[0]);
            }
        }

   
        //
        // 摘要:
        //     获取或设置是否管理员
        public bool IsAdmin { get; set; }
        //
        // 摘要:
        //     获取或设置是否禁用
        public bool IsDisabled { get; set; }
        //
        // 摘要:
        //     获取或设置登陆名称
        public string LoginName { get; set; }

        // 摘要:
        //     获取或设置所属部门对象Id
        public string OwningBusinessUnitId { get; set; }
        // 摘要:
        //     获取所属角色ID
        public string UserRoleID { get; set; }
        public string UserRole { get; set; }
        // 摘要:
        //     获取所属角色ID
        public SysRightCollection UserRights { get; set; }
        //


        // 摘要:
        //     获取指定登陆名称的用户信息
        //
        // 参数:
        //   loginName:
        //     登陆名称
        //
        // 返回结果:
        //     用户信息
        public static SysUser GetSysUser(string loginName)
        {
            DbOperator opt = new DbOperator();
            DataSet data = opt.CreateDataSetOra("select *   from HT_SVR_USER r left join ht_svr_sys_role t on r.role = t.f_id where r.LOGINNAME = '" + loginName + "'");
            if ((data != null) && (data.Tables[0].Rows.Count > 0))
            {
                return new SysUser(data.Tables[0].Rows[0]);
            }
            return null;
        }

        private void CreateUserFromRow(DataRow sysUserRow)
        {           
            this.Id = sysUserRow["ID"].ToString();
            this.Name = sysUserRow["NAME"].ToString();
            this.IsAdmin = false;
            this.IsDisabled = ("1" == sysUserRow["C_STATE"].ToString());
            this.LoginName = sysUserRow["LOGINNAME"].ToString();
            this.OwningBusinessUnitId = sysUserRow["LEVELGROUPID"].ToString();
            this.UserRoleID = sysUserRow["ROLE"].ToString();
            this.UserRole = sysUserRow["F_ROLE"].ToString();
            DbOperator opt = new DbOperator();
            DataSet data = opt.CreateDataSetOra("select r.id as userid,s.f_menu,s.f_type,s.F_MAPID,s.f_id   from HT_SVR_USER r left join HT_SVR_SYS_ROLE t on r.role = t.f_id left join HT_SVR_SYS_MENU s on substr (t.f_right,s.f_id,1) = '1'  where r.id = '" + this.Id + "' and s.f_menu is not null");
            UserRights = new SysRightCollection();
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    SysRight right = new SysRight(row);
                    UserRights.Add(right);
                }
            }
        }
    }
}

