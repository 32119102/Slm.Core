using Microsoft.AspNetCore.Mvc;
using Slm.DynamicApi.Attributes;
using Slm.Utils.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slm.Data.Core.Service;

public partial class ServiceAbstract<TEntity, TEntityInput, TEntityOutput, TEntitySearch, TTableDto, TKey>
{


    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Order(6)]
    public async Task<bool> DelAsync(TKey id)
    {
        return await _repositoriesBase.FalseDeletesAsync(new object[] { id! });
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpDelete]
    [Order(7)]
    public async Task<bool> BatchDelAsync([FromBody] InDelDto dto)
    {
        return await _repositoriesBase.FalseDeletesAsync(dto.Ids);
    }

}

