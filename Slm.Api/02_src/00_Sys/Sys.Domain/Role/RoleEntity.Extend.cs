using SqlSugar;
using Sys.Domain.Role2Menu;
using Sys.Domain.Role2Org;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Role;


public partial class RoleEntity
{

    /// <summary>
    /// 菜单集合
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    [Navigate(NavigateType.OneToMany, nameof(Role2MenuEntity.RoleId))]
    public List<Role2MenuEntity> Role2MenuEntities { get; set; }

    /// <summary>
    /// 自定义权限
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    [Navigate(NavigateType.OneToMany, nameof(Role2OrgEntity.RoleId))]
    public List<Role2OrgEntity> Role2OrgEntities { get; set; }

}
