using Slm.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.User;

public interface IUserRepository : IBaseRepository<UserEntity>
{
    /// <summary>
    /// 获取登录信息
    /// </summary>
    /// <param name="tenantCode"></param>
    /// <param name="account"></param>
    /// <returns></returns>
    Task<UserEntity> Login(string tenantCode, string account);

    /// <summary>
    /// 获取登录人信息
    /// </summary>
    /// <returns></returns>
    Task<UserEntity> UserInfo();
}