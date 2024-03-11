using EasyCaching.Core;
using Slm.Data.Core.Repository;
using Slm.Utils.Core.Extensions;
using Sys.Domain.Shared.Const;
using Sys.Domain.User2Role;

namespace Sys.Domain.Sqlsugar.User2Role;

/// <summary>
/// 用户和角色关系
/// </summary>
public class User2RoleRepository : RepositoryAbstract<User2RoleEntity>, IUser2RoleRepository
{
    private readonly IEasyCachingProvider _easyCachingProvider;

    public User2RoleRepository(IEasyCachingProvider easyCachingProvider)
    {
        _easyCachingProvider = easyCachingProvider;
    }

    /// <summary>
    /// 授权用户和角色关系
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="roleIds"></param>
    /// <returns></returns>
    public async Task GrantUserRole(long userId, List<long> roleIds)
    {
        await base.DeleteAsync(a => a.UserId == userId);
        if (roleIds.IsNull()) return;
        var user2RoleList = roleIds.Select(a => new User2RoleEntity
        {
            RoleId = a,
            UserId = userId
        }).ToList();
        await base.InsertRangeAsync(user2RoleList);
        _easyCachingProvider.Remove(CacheConst.KeyUserButton + userId);
    }


}
