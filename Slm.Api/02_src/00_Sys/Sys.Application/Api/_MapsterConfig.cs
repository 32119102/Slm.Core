using Mapster;
using Slm.Utils.Core.Models;
using Sys.Application.Api.Dto;
using Sys.Domain.Api;

namespace Sys.Application.Api;

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
        cfg.NewConfig<ApiEntity, OutApiTableDto>().MapToConstructor(true)
          ;
        //详情
        cfg.NewConfig<ApiEntity, OutApiDto>();
        //新增
        cfg.NewConfig<InApiDto, ApiEntity>();


        //树形列表
        cfg.NewConfig<ApiEntity, OutApiTreeTableDto>()
            ;


        //树形列表
        cfg.NewConfig<ApiEntity, OutCascaderDto>()
            .Map(m => m.Value, y => y.Id)
            ;
    }
}