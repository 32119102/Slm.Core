

using Mapster;
using Slm.Utils.Core.Models;
using Sys.Application.Org.Dto;
using Sys.Domain.Org;

namespace Sys.Application.Org;

/// <summary>
/// 映射
/// </summary>
public class MapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig cfg)
    {
        //分页
        cfg.NewConfig<OrgEntity, OutOrgTableDto>().MapToConstructor(true)
          ;
        //详情
        cfg.NewConfig<OrgEntity, OutOrgDto>();
        //新增
        cfg.NewConfig<InOrgDto, OrgEntity>();

        //树形列表
        cfg.NewConfig<OrgEntity, OutOrgTreeTableDto>()

            ;


        //树形列表
        cfg.NewConfig<OrgEntity, OutCascaderDto>()
            .Map(m => m.Value, y => y.Id)
                .Map(m => m.Label, y => y.Name)
            ;


        

    }
}
