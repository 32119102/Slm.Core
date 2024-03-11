
using Slm.Data.Core.Repository;
using Slm.Utils.Core.Extensions;
using Sys.Domain.Role2Org;

namespace Sys.Domain.Sqlsugar.Role2Org;


/// <summary>
/// 用户和角色关系
/// </summary>
public class Role2OrgRepository : RepositoryAbstract<Role2OrgEntity>, IRole2OrgRepository
{


    /// <summary>
    /// 授权用户和菜单的关系
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="orgIds"></param>
    /// <returns></returns>
    public async Task GrantRoleOrg(long roleId, List<long> orgIds)
    {
     
        await base.DeleteAsync(a => a.RoleId == roleId);
        if (orgIds.IsNull()) return;
        var role2MenuList = orgIds.Select(a => new Role2OrgEntity
        {
            RoleId = roleId,
            OrgId = a
        }).ToList();
        await base.InsertRangeAsync(role2MenuList);
    }
}