using Slm.Data.Abstractions.Entities;
using Slm.Utils.Core.Annotations;
using SqlSugar;
using Sys.Domain.Shared.User;

namespace Sys.Domain.User;



/// <summary>
/// 账号表
/// </summary>
[SugarTable("sys_user")]
[SysTable]
public partial class UserEntity : EntityBase<long>
{
    /// <summary>
    /// 账号
    /// </summary>
    [SugarColumn(ColumnName = "Account")]
    public string? Account { get; set; }
     
    /// <summary>
    /// 密码
    /// </summary>
    [SugarColumn(ColumnName = "Password")]
    public string? Password { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    [SugarColumn(ColumnName = "RealName")]
    public string? RealName { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    [SugarColumn(ColumnName = "NickName")]
    public string? NickName { get; set; }


    /// <summary>
    /// 头像
    /// </summary>
    [SugarColumn(ColumnName = "Avatar")]
    public string? Avatar { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    [SugarColumn(ColumnName = "Sex")]
    public UserSexEnum? Sex { get; set; }

    /// <summary>
    /// 年龄
    /// </summary>
    [SugarColumn(ColumnName = "Age")]
    public int? Age { get; set; }


    /// <summary>
    /// 邮件
    /// </summary>
    [SugarColumn(ColumnName = "Email")]
    public string? Email { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    [SugarColumn(ColumnName = "Phone")]
    public string? Phone { get; set; }


    /// <summary>
    /// 组织id
    /// </summary>
    [SugarColumn(ColumnName = "OrgId")]
    public long? OrgId { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [SugarColumn(ColumnName = "Sort")]
    public int? Sort { get; set; }

    /// <summary>
    /// 数据库连接
    /// </summary>
    [SugarColumn(ColumnName = "Remark")]
    public string? Remark { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    [SugarColumn(ColumnName = "IsEnable")]
    public bool IsEnable { get; set; }

    /// <summary>
    /// 账号类型
    /// </summary>
    [SugarColumn(ColumnName = "AccountType")]
    public UserAccountTypeEnum? AccountType { get; set; }
}
