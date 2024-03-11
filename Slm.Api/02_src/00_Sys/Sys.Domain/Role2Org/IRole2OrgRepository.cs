

using Slm.Data.Abstractions;

namespace Sys.Domain.Role2Org;



public interface IRole2OrgRepository : IBaseRepository<Role2OrgEntity>
{

    /// <summary>
    /// 授权自定义权限
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="orgIds"></param>
    /// <returns></returns>
    Task GrantRoleOrg(long roleId, List<long> orgIds);

}

