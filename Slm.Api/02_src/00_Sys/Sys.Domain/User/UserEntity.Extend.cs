using SqlSugar;
using Sys.Domain.Org;
using Sys.Domain.User2Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.User;

public partial class UserEntity
{
    /// <summary>
    /// 用户角色
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    [Navigate(NavigateType.OneToMany, nameof(User2RoleEntity.UserId))]//一对一
    public List<User2RoleEntity> User2Roles { get; set; }

    /// <summary>
    /// 用户角色
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    [Navigate(NavigateType.OneToOne, nameof(OrgId))]//一对一
    public OrgEntity Org { get; set; }


}
