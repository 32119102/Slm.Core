using Slm.Utils.Core.Annotations;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Role2Menu;

/// <summary>
/// 角色菜单
/// </summary>
[SugarTable("sys_role2menu")]
[SysTable]
public class Role2MenuEntity
{
    /// <summary>
    /// 菜单id
    /// </summary>
    [SugarColumn(ColumnName = "MenuId")]
    public long MenuId { get; set; }


    /// <summary>
    /// 角色id
    /// </summary>
    [SugarColumn(ColumnName = "RoleId")]
    public long RoleId { get; set; }

}


