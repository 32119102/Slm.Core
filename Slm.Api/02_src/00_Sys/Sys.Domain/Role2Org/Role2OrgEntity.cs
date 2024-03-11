

using Slm.Utils.Core.Annotations;
using SqlSugar;

namespace Sys.Domain.Role2Org;

/// <summary>
/// 角色接口
/// </summary>
[SugarTable("sys_role2org")]
[SysTable]
public class Role2OrgEntity
{
    /// <summary>
    /// 组织id
    /// </summary>
    [SugarColumn(ColumnName = "OrgId")]
    public long OrgId { get; set; }


    /// <summary>
    /// 角色id
    /// </summary>
    [SugarColumn(ColumnName = "RoleId")]
    public long RoleId { get; set; }

}

