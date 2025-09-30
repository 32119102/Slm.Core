
using SqlSugar;
using Sys.Domain.Role;

namespace Sys.Domain.User2Role;



public partial class User2RoleEntity
{
    /// <summary>
    /// 用户角色
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    [Navigate(NavigateType.OneToOne, nameof(RoleId), nameof(RoleEntity.Id))]
    public RoleEntity Role { get; set; }




}
