using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Shared.User;



/// <summary>
/// 性别枚举
/// </summary>
[Description("性别枚举")]
public enum UserSexEnum
{

    /// <summary>
    /// 未知
    /// </summary>
    [Description("未知")]
    UnKnown = -1,

    /// <summary>
    /// 男
    /// </summary>
    [Description("男")]
    Male = 1,

    /// <summary>
    /// 女
    /// </summary>
    [Description("女")]
    Female = 2,

    /// <summary>
    /// 其他
    /// </summary>
    [Description("其他")]
    Other = 3,
}

/// <summary>
/// 账号类型枚举
/// </summary>
[Description("账号类型枚举")]
public enum UserAccountTypeEnum
{
    /// <summary>
    /// 超级管理员
    /// </summary>
    [Description("超级管理员")]
    SuperAdmin = 9,

    /// <summary>
    /// 系统管理员
    /// </summary>
    [Description("系统管理员")]
    SysAdmin = 2,

    /// <summary>
    /// 普通账号
    /// </summary>
    [Description("普通账号")]
    NormalUser = 1,

}


