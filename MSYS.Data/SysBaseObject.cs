using System;
namespace MSYS.Data
{
    // 摘要:
    //     系统基础对象
    [Serializable]
    public class SysBaseObject
    {
        private string id;
        private string name;
        // 摘要:
        //     初始化基础对象
        public SysBaseObject()
        {
        }
        //
        // 摘要:
        //     初始化基础对象
        //
        // 参数:
        //   id:
        //     唯一标识
        //
        //   name:
        //     名称
        public SysBaseObject(string id, string name)
        {
            this.id = id;
            this.name = name;
        }

        // 摘要:
        //     获取或设置唯一标识
        public string Id {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
        //
        // 摘要:
        //     获取或设置名称
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        //
        // 参数:
        //   obj:
        //     与当前的 System.Object 进行比较的 System.Object。
        //
        // 返回结果:
        //     如果指定的 System.Object 等于当前的 System.Object，则为 true；否则为 false。
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            SysBaseObject obj2 = (SysBaseObject)obj;
            return (this.id == obj2.id );
        }
    }
}

