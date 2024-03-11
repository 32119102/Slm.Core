
using Slm.Data.Core.Repository;
using Slm.Utils.Core.Extensions;
using Sys.Domain.Role2Api;

namespace Sys.Domain.Sqlsugar.Role2Api;


/// <summary>
/// 用户和角色关系
/// </summary>
public class Role2ApiRepository : RepositoryAbstract<Role2ApiEntity>, IRole2ApiRepository
{
    /// <summary>
    /// 授权用户和菜单的关系
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="apiIds"></param>
    /// <returns></returns>
    public async Task GrantRoleApi(long roleId, List<long> apiIds)
    {
        await base.DeleteAsync(a => a.RoleId == roleId);
        if (apiIds.IsNull()) return;
        var role2MenuList = apiIds.Select(a => new Role2ApiEntity
        {
            RoleId = roleId,
            ApiId = a
        }).ToList();
        await base.InsertRangeAsync(role2MenuList);
    }



}