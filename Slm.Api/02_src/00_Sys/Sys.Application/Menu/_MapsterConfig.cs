using Mapster;
using Slm.Utils.Core.Extensions;
using Slm.Utils.Core.Models;
using Sys.Application.Api.Dto;
using Sys.Application.Menu.Dto;
using Sys.Domain.Api;
using Sys.Domain.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Menu;


/// <summary>
/// 映射
/// </summary>
public class MapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig cfg)
    {
        //分页
        cfg.NewConfig<MenuEntity, OutMenuTableDto>().MapToConstructor(true)
          ;
        //详情
        cfg.NewConfig<MenuEntity, OutMenuDto>();
        //新增
        cfg.NewConfig<InMenuDto, MenuEntity>();

        //树形列表
        cfg.NewConfig<MenuEntity, OutMenuTreeTableDto>()

            ;


        //树形列表
        cfg.NewConfig<MenuEntity, OutCascaderDto>()
            .Map(m => m.Value, y => y.Id)
                .Map(m => m.Label, y => y.Title)
            ;


        //树形列表
        cfg.NewConfig<MenuEntity, OutLeftMenuDto>()
             .Map(m => m.Name, y => (y.IsFrame&&y.FrameSrc.NotNull()) ? y.FrameSrc : y.Name)
             .Map(m => m.Meta, y => new MetaDto
             {
                 FrameSrc =  y.FrameSrc ,
                 Icon = y.Icon,
                 KeepAlive = y.IsKeepAlive,
                 Rank = y.Sort,
                 Redirect = y.Redirect,
                 Title = y.Title,
                 IsFrame = y.IsFrame,
             })
            ;

    }
}
