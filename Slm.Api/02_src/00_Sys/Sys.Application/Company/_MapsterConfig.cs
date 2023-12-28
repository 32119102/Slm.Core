using Mapster;
using Sys.Application.Company.Dto;
using Sys.Domain.Company;


namespace Sys.Application.Company;

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
        cfg.NewConfig<CompanyEntity, OutCompanyTableDto>()
          ;
        //详情
        cfg.NewConfig<CompanyEntity, OutCompanyDto>();
        //新增
        cfg.NewConfig<InCompanyDto, CompanyEntity>();

    }
}