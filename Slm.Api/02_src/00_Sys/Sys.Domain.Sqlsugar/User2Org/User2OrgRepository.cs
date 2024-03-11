using Slm.Data.Core.Repository;
using Slm.Utils.Core.Extensions;
using Sys.Domain.User2Org;

namespace Sys.Domain.Sqlsugar.User2Org;


/// <summary>
/// 用户和角色关系
/// </summary>
public class User2OrgRepository : RepositoryAbstract<User2OrgEntity>, IUser2OrgRepository
{


    /// <summary>
    /// 授权用户和附属组织关系
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="orgIds"></param>
    /// <returns></returns>
    public async Task GrantUserOrg(long userId, List<long> orgIds)
    {
        await base.DeleteAsync(a => a.UserId == userId);
        if (orgIds.IsNull()) return;
        var user2OrgList = orgIds.Select(a => new User2OrgEntity
        {
            OrgId = a,
            UserId = userId
        }).ToList();
        await base.InsertRangeAsync(user2OrgList);
    }


}