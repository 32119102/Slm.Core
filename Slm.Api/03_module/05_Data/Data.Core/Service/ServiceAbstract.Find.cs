using Microsoft.AspNetCore.Mvc;
using Slm.DynamicApi.Attributes;
using Slm.Utils.Core.Annotations;
using Slm.Utils.Core.Models;
using Slm.Utils.Core.Pagination;
using SqlSugar;
using System.Linq.Expressions;

namespace Slm.Data.Core.Service;

public partial class ServiceAbstract<TEntity, TEntityInput, TEntityOutput, TEntitySearch, TTableDto, TKey>
{


    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpGet]
    [Order(1)]
    public virtual async Task<QueryResultModel<TTableDto>> GetPageAsync(TEntitySearch dto)
    {
        List<IConditionalModel> conditionalModels = this._repositoriesBase.GetConditionals(dto);
        var list = await _repositoriesBase.GetPageAsync(conditionalModels, dto);
        var result = _mapper.Map<QueryResultModel<TTableDto>>(list);
        return result;
    }



    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [Order(2)]
    public virtual async Task<TEntityOutput> GetAsync(TKey id)
    {
        var entity = await _repositoriesBase.GetAsync(id!);
        var result = _mapper.Map<TEntityOutput>(entity);
        return result;
    }




    /// <summary>
    /// 获取下拉框数据
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowWhenAuthenticated]
    [Order(8)]
    public virtual async Task<List<OptionResultModel>> GetSelectAsync()
    {
        var entity = await _repositoriesBase.GetListAsync();
        return _mapper.Map<List<OptionResultModel>>(entity);
    }






}
