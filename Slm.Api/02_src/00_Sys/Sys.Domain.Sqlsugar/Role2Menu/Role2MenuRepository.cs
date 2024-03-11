using Slm.Data.Core.Repository;
using Slm.Utils.Core.Extensions;
using Sys.Domain.Role2Menu;

namespace Sys.Domain.Sqlsugar.Role2Menu;

/// <summary>
/// 用户和角色关系
/// </summary>
public class Role2MenuRepository : RepositoryAbstract<Role2MenuEntity>, IRole2MenuRepository
{
    /// <summary>
    /// 授权用户和菜单的关系
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="menuIds"></param>
    /// <returns></returns>
    public async Task GrantRoleMenu(long roleId, List<long> menuIds)
    {
        await base.DeleteAsync(a => a.RoleId == roleId);
        if (menuIds.IsNull()) return;
        var role2MenuList = menuIds.Select(a => new Role2MenuEntity
        {
            RoleId = roleId,
            MenuId = a
        }).ToList();
        await base.InsertRangeAsync(role2MenuList);
    }



}