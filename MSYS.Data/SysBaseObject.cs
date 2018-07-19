using System;
using System.Collections.Generic;
using System.Reflection;
namespace MSYS.Data
{
    // 摘要:
    //     系统基础对象
    [Serializable]
    public abstract class SysBaseObject
    {
       public  struct Attribute
        {
          public  string name { get; set; }
          public  string value { get; set; }
        }
        protected string ID;
        protected string name;
        protected List<Attribute> attrs;
        
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
        public List<Attribute> getAttributes()
        {
            
                Type t = this.GetType();
                PropertyInfo[] PropertyList = t.GetProperties();
                if (PropertyList.Length <= 0)
                {
                    return null;
                }
                attrs = new List<Attribute>();
                foreach (PropertyInfo item in PropertyList)
                {                    
                   
                    if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String")||item.Name != "text")
                    {
                        Attribute attr = new Attribute();
                        attr.name = item.Name;
                        attr.value = item.GetValue(this, null).ToString();
                        attrs.Add(attr);
                    }
                }
                return attrs;
         
        }

        //
        // 摘要:
        //     获取或设置名称
        public string text 
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

        public string id
        {
            get
            {
                return this.ID;
            }
            set
            {
                this.ID = value;
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

        protected abstract void CreateObjfromDB(string id);

        

    }
}

