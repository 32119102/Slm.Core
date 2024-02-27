using Slm.DynamicApi.Attributes;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;

namespace Slm.Data.Core.Service;

public partial class ServiceAbstract<TEntity, TEntityInput, TEntityOutput, TEntitySearch, TTableDto, TKey>
{

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="dto">dto</param>
    /// <returns></returns>
    [Order(201)]
    public virtual async Task<dynamic> AddAsync(TEntityInput dto)
    {
        var entity = _mapper.Map<TEntity>(dto);

        if (typeof(TKey) == typeof(long))
        {
            return await this._repositoriesBase.InsertReturnSnowflakeIdAsync(entity);
        }
        else if (typeof(TKey) == typeof(int))
        {
            return await this._repositoriesBase.InsertReturnIdentityAsync(entity);
        }
        else
        {

            return await this._repositoriesBase.InsertAsync(entity);
        }
    }


    /// <summary>
    /// 批量新增
    /// </summary>
    /// <param name="dtos">实体集合</param>
    /// <returns>影响行数</returns>
    [Order(202)]
    public virtual async Task<bool> BulkAddAsync(List<TEntityInput> dtos)
    {
        var entitys = _mapper.Map<List<TEntity>>(dtos);
        return await this._repositoriesBase.InsertRangeAsync(entitys);
    }

}
