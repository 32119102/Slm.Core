using Microsoft.AspNetCore.Authorization;
using Slm.Data.Core.Service;
using Slm.DynamicApi;
using Slm.DynamicApi.Attributes;
using Sys.Application.Role.Dto;
using Sys.Domain.Role;
using Sys.Domain.Shared;
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






}