using EasyCaching.Core;
using Microsoft.AspNetCore.Authorization;
using Slm.Data.Abstractions.Attributes;
using Slm.Data.Core.Service;
using Slm.DynamicApi;
using Slm.DynamicApi.Attributes;
using Slm.Utils.Core;
using Slm.Utils.Core.Models;
using Sys.Application.User.Dto;
using Sys.Domain.Shared;
using Sys.Domain.Shared.Const;
using Sys.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.User;

/// <summary>
/// 账号服务
/// </summary>
[DynamicApi(Area = SsyAreaConst.Area)]
[Order(7)]
[AllowAnonymous]
public class UserService : ServiceAbstract<UserEntity, InUserDto, OutUserDto, InUserSearchDto, OutUserTableDto, long>, IDynamicApi
{
    /// <summary>
    /// 账号仓储
    /// </summary>
    public IUserRepository _userRepository => AbpLazyServiceProvider.LazyGetRequiredService<IUserRepository>();
    public IEasyCachingProvider _easyCachingProvider => AbpLazyServiceProvider.LazyGetRequiredService<IEasyCachingProvider>();
    public JsonHelper _jsonHelper => AbpLazyServiceProvider.LazyGetRequiredService<JsonHelper>();


    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [Transaction]
    public override async Task<dynamic> AddAsync(InUserDto dto)
    {
        var user = _mapper.Map<UserEntity>(dto);
        //默认密码
        long id = await _userRepository.InsertReturnSnowflakeIdAsync(user);

        //await _easyCachingProvider.SetAsync(CacheConst.KeyUserButton + id, user, TimeSpan.FromDays(7));
        //var ddd = await _easyCachingProvider.GetAsync<UserEntity>(CacheConst.KeyUserButton + id);
        //await _easyCachingProvider.RemoveAsync(CacheConst.KeyUserButton + id);

        //01.用户
        //02.角色


        //03.附属组织
        return id;
    }



}