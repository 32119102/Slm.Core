using Mapster;
using Sys.Application.Tenant.Dto;
using Sys.Domain.Tenant;

namespace Sys.Application.Tenant;

/// <summary>
/// 映射
/// </summary>
public class MapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig cfg)
    {
        //config
        //.NewConfig<TenantListOutput, TenantListOutput>()
        //.Map(dest => dest.PkgNames, src => src.Pkgs.Select(a => a.Name));

        //分页
        cfg.NewConfig<TenantEntity, OutTenantTableDto>()
          ;
        //详情
        cfg.NewConfig<TenantEntity, OutTenantDto>();
        //新增
        cfg.NewConfig<InTenantDto, TenantEntity>();

    }
}