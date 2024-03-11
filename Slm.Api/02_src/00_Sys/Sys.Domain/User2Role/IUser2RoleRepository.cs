using Slm.Data.Abstractions;

namespace Sys.Domain.User2Role;


public interface IUser2RoleRepository : IBaseRepository<User2RoleEntity>
{

    /// <summary>
    /// 授权用户和角色关系
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="roleIds"></param>
    /// <returns></returns>
    Task GrantUserRole(long userId, List<long> roleIds);

}
