using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Slm.Data.Abstractions.Attributes;
using Slm.Data.Core.Service;
using Slm.DynamicApi;
using Slm.DynamicApi.Attributes;
using Slm.Utils.Core.Extensions;
using Slm.Utils.Core.Models;
using StackExchange.Redis;
using Sys.Application.Role.Dto;
using Sys.Domain.Role;
using Sys.Domain.Role2Api;
using Sys.Domain.Role2Menu;
using Sys.Domain.Role2Org;
using Sys.Domain.Shared;
using Sys.Domain.Shared.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Role;


/// <summary>
/// 角色服务
/// </summary>
[DynamicApi(Area = SsyAreaConst.Area)]
[Order(6)]
[AllowAnonymous]
public class RoleService : ServiceAbstract<RoleEntity, InRoleDto, OutRoleDto, InRoleSearchDto, OutRoleTableDto, long>, IDynamicApi
{
    /// <summary>
    /// 角色仓储
    /// </summary>
    public IRoleRepository _roleRepository => AbpLazyServiceProvider.LazyGetRequiredService<IRoleRepository>();


    /// <summary>
    /// 角色菜单仓储
    /// </summary>
    public IRole2MenuRepository _role2MenuRepository => AbpLazyServiceProvider.LazyGetRequiredService<IRole2MenuRepository>();

    /// <summary>
    /// 角色接口
    /// </summary>
    public IRole2ApiRepository _role2ApiRepository => AbpLazyServiceProvider.LazyGetRequiredService<IRole2ApiRepository>();

    /// <summary>
    /// 自定义角色权限
    /// </summary>
    public IRole2OrgRepository _role2OrgRepository => AbpLazyServiceProvider.LazyGetRequiredService<IRole2OrgRepository>();

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [Transaction]
    public override async Task<dynamic> AddAsync(InRoleDto dto)
    {
        var entity = _mapper.Map<RoleEntity>(dto);
        var id = await _roleRepository.InsertReturnSnowflakeIdAsync(entity);
        await _role2MenuRepository.GrantRoleMenu(id, dto.MenuIds);
        return id;
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [Transaction]
    public override async Task<bool> UpdateAsync(long id, InRoleDto dto)
    {
        var entity = await _roleRepository.GetAsync(id!);
        _mapper.Map(dto, entity);
        bool result = await _roleRepository.UpdateAsync(entity);
        await _role2MenuRepository.GrantRoleMenu(id, dto.MenuIds);
        return result;
    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public override async Task<OutRoleDto> GetAsync(long id)
    {
        var entity = await _roleRepository.GetInfo(id!);
        var result = _mapper.Map<OutRoleDto>(entity);
        return result;
    }

    /// <summary>
    /// 角色接口授权
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("{id}")]
    [Transaction]
    public async Task<bool> GrantRoleApi(long id, InGrantRole2ApiDto dto)
    {
        await _role2ApiRepository.GrantRoleApi(id, dto.ApiIds);
        return true;
    }

    /// <summary>
    /// 获取角色接口数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<List<long>> GetGrantRoleApi(long id)
    {
        var apis = await _role2ApiRepository.GetListAsync(a => a.RoleId == id);
        return apis.Select(a => a.ApiId).ToList();
    }


    /// <summary>
    /// 数据授权
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("{id}")]
    [Transaction]
    public async Task<bool> GrantDataScope(long id, InGrantDataScopeDto dto)
    {
        // 删除与该角色相关的用户机构缓存
        //var userIdList = await _sysUserRoleService.GetUserIdList(input.Id);
        //foreach (var userId in userIdList)
        //{
        //    SqlSugarFilter.DeleteUserOrgCache(userId, _sysRoleRep.Context.CurrentConnectionConfig.ConfigId.ToString());
        //}
        bool SuperAdmin = true;
        if (!SuperAdmin)
        {
            // 非超级管理员没有全部数据范围权限
            if (dto.DataScope == DataScopeEnum.All)
                throw ResultModel.Exception("没有权限");

            // 若数据范围自定义，则判断授权数据范围是否有权限
            if (dto.DataScope == DataScopeEnum.Define)
            {
                if (dto.OrgIds.NotNull())
                {
                    //todo 获取当前登录人的组织， 用户表主组织，附属组织，角色权限
                    var orgIdList = new List<long>();
                    if (orgIdList.IsNull())
                        throw ResultModel.Exception("没有权限");
                    if (!dto.OrgIds.All(a => orgIdList.Any(b => a == b)))
                        throw ResultModel.Exception("没有权限");

                }
            }
        }

        await _roleRepository.UpdateSetColumnsTrueAsync(a => new RoleEntity { DataScope = dto.DataScope }, a => a.Id == id);
        await _role2OrgRepository.GrantRoleOrg(id, dto.OrgIds);
        return true;
    }

    /// <summary>
    /// 数据授权
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<InGrantDataScopeDto> GetGrantDataScope(long id)
    {
        var role = await _roleRepository.GetRoleAndOrg(id);
        return _mapper.Map<InGrantDataScopeDto>(role);
    }


}