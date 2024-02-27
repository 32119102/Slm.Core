using Microsoft.AspNetCore.Mvc;
using Slm.DynamicApi.Attributes;

namespace Slm.Data.Core.Service;

public partial  class ServiceAbstract<TEntity, TEntityInput, TEntityOutput, TEntitySearch, TTableDto, TKey>
{

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [Order(301)]
    public virtual async Task<bool> UpdateAsync(TKey id, TEntityInput dto)
    {
        var entity = await _repositoriesBase.GetAsync(id!);
        _mapper.Map(dto, entity);
        bool result = await _repositoriesBase.UpdateAsync(entity);
        return result;
    }

}