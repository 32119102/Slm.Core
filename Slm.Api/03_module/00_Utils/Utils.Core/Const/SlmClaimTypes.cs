using System.ComponentModel;

namespace Slm.Utils.Core.Const;

public class SlmClaimTypes
{
    /// <summary>
    /// 租户编号
    /// </summary>
    public const string TenantId = "td";

    /// <summary>
    /// 账户编号
    /// </summary>
    public const string UserId = "id";

    /// <summary>
    /// 账户名称
    /// </summary>
    public const string UserName = "an";

    /// <summary>
    /// 刷新有效期
    /// </summary>
    public const string RefreshExpires = "re";

    /// <summary>
    /// 登录时间
    /// </summary>
    public const string LoginTime = "lt";

    /// <summary>
    /// 客户IP
    /// </summary>
    public const string Ip = "ip";

    /// <summary>
    /// 角色集合
    /// </summary>
    public const string Roles = "ro";

    /// <summary>
    /// 超级管理员
    /// </summary>
    public const string Padmin = "padmin";


    public const string AccountType = "accountType";
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