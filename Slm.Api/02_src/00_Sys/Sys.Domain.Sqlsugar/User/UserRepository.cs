using Slm.Auth.Abstractions;
using Slm.Data.Core.Repository;
using Sys.Domain.Org;
using Sys.Domain.Tenant;
using Sys.Domain.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Sqlsugar.User;

public class UserRepository : RepositoryAbstract<UserEntity>, IUserRepository
{

    /// <summary>
    /// 获取登录信息
    /// </summary>
    /// <param name="tenantCode"></param>
    /// <param name="account"></param>
    /// <returns></returns>
    public Task<UserEntity> Login(string tenantCode, string account)
    {
        return AsQueryable().InnerJoin<TenantEntity>((a, b) => a.TenantId == b.Id)
             .Where((a, b) => a.Account.Equals(account) && b.Code.Equals(tenantCode))
             .FirstAsync();

    }

    /// <summary>
    /// 获取当前登录人信息
    /// </summary>
    /// <returns></returns>
    public Task<UserEntity> UserInfo()
    {
        return AsQueryable().Includes(a => a.User2Roles)
             .Includes(a => a.Org).Where(a => a.Id == UserResolver.UserId).FirstAsync();
    }

}
