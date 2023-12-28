using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Shared.Tenant;

/// <summary>
/// 租户类型枚举
/// </summary>
[Description("租户类型枚举")]
public enum TenantTypeEnum
{

    /// <summary>
    /// 未知
    /// </summary>
    [Description("未知")]
    UnKnown = -1,

    /// <summary>
    /// Id隔离
    /// </summary>
    [Description("Id隔离")]
    Id = 0,

    /// <summary>
    /// 库隔离
    /// </summary>
    [Description("库隔离")]
    Db = 1,




}
