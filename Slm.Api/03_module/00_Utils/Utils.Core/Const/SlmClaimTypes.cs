using System.ComponentModel;

namespace Slm.Utils.Core.Const;

public class SlmClaimConst
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public const string UserId = "UserId";

    /// <summary>
    /// 账号
    /// </summary>
    public const string Account = "Account";

    /// <summary>
    /// 真实姓名
    /// </summary>
    public const string RealName = "RealName";

    /// <summary>
    /// 昵称
    /// </summary>
    public const string NickName = "NickName";

    /// <summary>
    /// 账号类型
    /// </summary>
    public const string AccountType = "AccountType";

    /// <summary>
    /// 租户Id
    /// </summary>
    public const string TenantId = "TenantId";

    /// <summary>
    /// 组织机构Id
    /// </summary>
    public const string OrgId = "OrgId";

}

/// <summary>
/// 账号类型枚举
/// </summary>
[Description("账号类型枚举")]
public enum AccountTypeEnum
{
    /// <summary>
    /// 超级管理员
    /// </summary>
    [Description("超级管理员")]
    SuperAdmin = 999,

    /// <summary>
    /// 系统管理员
    /// </summary>
    [Description("系统管理员")]
    SysAdmin = 888,

    /// <summary>
    /// 普通账号
    /// </summary>
    [Description("普通账号")]
    NormalUser = 777,

}