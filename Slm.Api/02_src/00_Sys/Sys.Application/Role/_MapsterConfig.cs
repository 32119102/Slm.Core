using Mapster;
using Slm.Utils.Core.Extensions;
using Slm.Utils.Core.Models;
using Sys.Application.Role.Dto;
using Sys.Domain.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Role;

/// <summary>
/// 映射
/// </summary>
public class MapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig cfg)
    {
        //分页
        cfg.NewConfig<RoleEntity, OutRoleTableDto>().MapToConstructor(true)
               .Map(m => m.DataScopeName, y => y.DataScope.ToDescription())
          ;
        //详情
        cfg.NewConfig<RoleEntity, OutRoleDto>()
           .Map(m => m.MenuIds, y => y.Role2MenuEntities.Select(a => a.MenuId).ToList())
            ;
        //新增
        cfg.NewConfig<InRoleDto, RoleEntity>();


        //下拉框
        cfg.NewConfig<RoleEntity, OptionResultModel>()
            .Map(m => m.Value, y => y.Id)
                .Map(m => m.Label, y => y.Name)
            ;

        //详情
        cfg.NewConfig<RoleEntity, InGrantDataScopeDto>()
           .Map(m => m.OrgIds, y => y.Role2OrgEntities.Select(a => a.OrgId).ToList())
            ;



    }
}
