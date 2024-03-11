using Mapster;
using Slm.Utils.Core.Extensions;
using Sys.Application.User.Dto;
using Sys.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.User;

/// <summary>
/// 映射
/// </summary>
public class MapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig cfg)
    {
        //分页
        cfg.NewConfig<UserEntity, OutUserTableDto>().MapToConstructor(true)
         .Map(dest => dest.AccountTypeName, src => src.AccountType.ToDescription());
        ;
        //详情
        cfg.NewConfig<UserEntity, OutUserDto>();
        //新增
        cfg.NewConfig<InUserDto, UserEntity>();


 


    }
}
