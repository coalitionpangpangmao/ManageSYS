using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSYS.Web
{
    public interface IBasePage
    {
        bool HasRight { get; }  //是否有当前页面的操作权限

        string MappingId { get; } //页面映射ID

        string SessionId { get; set; } //会话ID 

        string UniqueId { get; }
    }
}
