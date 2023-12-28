using Microsoft.AspNetCore.Authorization;
using Slm.Data.Core.Service;
using Slm.DynamicApi.Attributes;
using Slm.DynamicApi;
using Sys.Domain.Shared;
using Sys.Domain.Tenant;
using Sys.Application.Tenant.Dto;

namespace Sys.Application.Tenant;



/// <summary>
/// 租户服务
/// </summary>
[DynamicApi(Area = SsyAreaConst.Area)]
[Order(1)]
[AllowAnonymous]
public class TenantService : ServiceAbstract<TenantEntity, InTenantDto, OutTenantDto, InTenantSearchDto, OutTenantTableDto, long>, IDynamicApi
{






}
