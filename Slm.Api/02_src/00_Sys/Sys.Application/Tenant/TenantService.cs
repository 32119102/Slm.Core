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
    /// <summary>
    /// 菜单仓储
    /// </summary>
    public ITenantRepository _tenantRepository => AbpLazyServiceProvider.LazyGetRequiredService<ITenantRepository>();
  
    /// <summary>
    /// 更新状态
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<bool> IsEnable(InIsEnableDto dto)
    {
        await _tenantRepository.UpdateSetColumnsTrueAsync(a => new TenantEntity
        {
            IsEnable = !a.IsEnable
        }, a => a.Id == dto.Id);
        return true;
    }



}
