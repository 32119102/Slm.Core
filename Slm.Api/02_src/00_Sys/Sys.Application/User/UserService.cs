using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slm.Auth.Abstractions;
using Slm.Data.Abstractions.Attributes;
using Slm.Data.Core.Service;
using Slm.DynamicApi;
using Slm.DynamicApi.Attributes;
using Slm.Utils.Core;
using Slm.Utils.Core.Annotations;
using Slm.Utils.Core.Models;
using Sys.Application.User.Dto;
using Sys.Domain.Shared;
using Sys.Domain.Shared.Const;
using Sys.Domain.User;
using Sys.Domain.User2Org;
using Sys.Domain.User2Role;
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
public class UserService : ServiceAbstract<UserEntity, InUserDto, OutUserDto, InUserSearchDto, OutUserTableDto, long>, IDynamicApi
{
    /// <summary>
    /// 账号仓储
    /// </summary>
    public IUserRepository _userRepository => AbpLazyServiceProvider.LazyGetRequiredService<IUserRepository>();


    /// <summary>
    /// 用户和角色关系
    /// </summary>
    public IUser2RoleRepository _user2RoleRepository => AbpLazyServiceProvider.LazyGetRequiredService<IUser2RoleRepository>();


    /// <summary>
    /// 用户和组织的关系
    /// </summary>
    public IUser2OrgRepository _user2OrgRepository => AbpLazyServiceProvider.LazyGetRequiredService<IUser2OrgRepository>();

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

        //01.用户
        long id = await _userRepository.InsertReturnSnowflakeIdAsync(user);


        //02.角色
        await _user2RoleRepository.GrantUserRole(id, dto.RoleIds);


        //03.附属组织
        await _user2OrgRepository.GrantUserOrg(id, dto.OrgIds);
        return id;
    }


    /// <summary>
    /// 获取登录信息
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowWhenAuthenticated]
    public async Task<UserLoginOutPut> GetUserInfo()
    {
        var user = await _userRepository.UserInfo();



        return new UserLoginOutPut
        {
            Id = user.Id,
            Account = user.Account,
            AccountType = user.AccountType.Value,
            RealName = user.RealName,
            Avatar = user.Avatar,
            OrgId = user.OrgId.Value,
            OrgName = user.Org.Name,
            RoleIds = user.User2Roles.Select(a => a.RoleId).ToList()

        };
    }
}