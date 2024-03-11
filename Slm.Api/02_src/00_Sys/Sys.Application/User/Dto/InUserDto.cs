
using SqlSugar;
using Sys.Domain.Shared.User;

namespace Sys.Application.User.Dto;

public class InUserDto
{
    /// <summary>
    /// 账号
    /// </summary>
    public string? Account { get; set; }


    /// <summary>
    /// 昵称
    /// </summary>
    public string? RealName { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    public string? NickName { get; set; }


    /// <summary>
    /// 性别
    /// </summary>
    public UserSexEnum? Sex { get; set; }

    /// <summary>
    /// 年龄
    /// </summary>
    public int? Age { get; set; }


    /// <summary>
    /// 邮件
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string? Phone { get; set; }


    /// <summary>
    /// 组织id
    /// </summary>
    public long? OrgId { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int? Sort { get; set; }

    /// <summary>
    /// 数据库连接
    /// </summary>
    public string? Remark { get; set; }


    /// <summary>
    /// 账号类型
    /// </summary>
    public UserAccountTypeEnum? AccountType { get; set; }

    /// <summary>
    /// 角色id集合
    /// </summary>
    /// 
    public List<long>? RoleIds { get; set; }

    /// <summary>
    /// 附属机构集合
    /// </summary>
    public List<long>? OrgIds { get; set; }
}
