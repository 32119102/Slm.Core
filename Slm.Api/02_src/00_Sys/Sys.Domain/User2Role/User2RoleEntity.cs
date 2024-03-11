using Slm.Utils.Core.Annotations;
using SqlSugar;

namespace Sys.Domain.User2Role;

/// <summary>
/// 账号表
/// </summary>
[SugarTable("sys_user2role")]
[SysTable]
public class User2RoleEntity
{
    /// <summary>
    /// 用户id
    /// </summary>
    [SugarColumn(ColumnName = "UserId")]
    public long UserId { get; set; }


    /// <summary>
    /// 角色id
    /// </summary>
    [SugarColumn(ColumnName = "RoleId")]
    public long RoleId { get; set; }

}
