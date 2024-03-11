using Slm.Utils.Core.Annotations;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Role2Api;

/// <summary>
/// 角色接口
/// </summary>
[SugarTable("sys_role2api")]
[SysTable]
public class Role2ApiEntity
{
    /// <summary>
    /// 菜单id
    /// </summary>
    [SugarColumn(ColumnName = "ApiId")]
    public long ApiId { get; set; }


    /// <summary>
    /// 角色id
    /// </summary>
    [SugarColumn(ColumnName = "RoleId")]
    public long RoleId { get; set; }

}

