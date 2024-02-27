using MapsterMapper;
using Microsoft.Extensions.Logging;
using Slm.Data.Abstractions;
using Slm.Utils.Core.DependencyInjection;
using Slm.Utils.Core.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slm.Data.Core.Service;


/// <summary>
/// 查询:101(110),新增:201(210);更新:301(310);删除:401(410)
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TEntityInput"></typeparam>
/// <typeparam name="TEntityOutput"></typeparam>
/// <typeparam name="TEntitySearch"></typeparam>
/// <typeparam name="TTableDto"></typeparam>
/// <typeparam name="TKey"></typeparam>
public partial class ServiceAbstract<TEntity, TEntityInput, TEntityOutput, TEntitySearch, TTableDto, TKey>
     where TEntity : class, new()
     where TEntityInput : class, new()
     where TEntityOutput : class, new()
     where TEntitySearch : QueryDto
{
    /// <summary>
    /// 赖解析
    /// </summary>
    public IAbpLazyServiceProvider AbpLazyServiceProvider { get; set; } = default!;

    /// <summary>
    /// 映射
    /// </summary>
    public IMapper _mapper => AbpLazyServiceProvider.LazyGetRequiredService<IMapper>();

    /// <summary>
    /// 基础仓储
    /// </summary>
    private IBaseRepository<TEntity> _repositoriesBase => AbpLazyServiceProvider.LazyGetRequiredService<IBaseRepository<TEntity>>();






}