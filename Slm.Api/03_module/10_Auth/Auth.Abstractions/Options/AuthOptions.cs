using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slm.Auth.Abstractions.Options;

public class AuthOptions
{
    /// <summary>
    /// 启用权限验证
    /// </summary>
    public bool EnablePermissionVerify { get; set; } = true;

    /// <summary>
    /// 启用日志功能
    /// </summary>
    public bool EnableAuditLog { get; set; }

}
