

using Slm.Data.Abstractions;

namespace Sys.Domain.Role2Api;


public interface IRole2ApiRepository : IBaseRepository<Role2ApiEntity>
{

    /// <summary>
    /// 授权角色和接口关系
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="apiIds"></param>
    /// <returns></returns>
    Task GrantRoleApi(long roleId, List<long> apiIds);

}

