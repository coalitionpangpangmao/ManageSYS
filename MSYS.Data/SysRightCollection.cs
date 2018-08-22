using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace MSYS.Data
{

    public class SysRightCollection : ArrayList
    {
        // 摘要:
        //     初始化权限集合对象
        public SysRightCollection()
        {
        }


        // 摘要:
        //     获取或设置指定索引位置的权限对象
        //
        // 参数:
        //   index:
        //     索引值
        //
        // 返回结果:
        //     权限对象
        public  SysRight this[int index]
        {
            get
            {
                return ((SysRight)base[index]);
            }
            set
            {
                base[index] = value;
            }
        }

        // 摘要:
        //     添加权限对象到集合中
        //
        // 参数:
        //   sysRole:
        //     权限对象
        //
        // 返回结果:
        //     当前权限对象在集合中的位置
        public int Add(SysRight sysRight)
        {
            return base.Add(sysRight);
        }
        //
        // 摘要:
        //     从集合中移除指定权限对象
        //
        // 参数:
        //   sysRole:
        //     权限对象
        public void Remove(SysRight sysRight)
        {
            base.Remove(sysRight);
        }
        //
        // 摘要:
        //     移除集合中指定位置的权限对象
        //
        // 参数:
        //   index:
        //     权限对象在集合中的位置
        public override void RemoveAt(int index)
        {
            base.RemoveAt(index);
        }
    }

}
