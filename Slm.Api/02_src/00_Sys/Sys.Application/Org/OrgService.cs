using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slm.Data.Core.Service;
using Slm.DynamicApi;
using Slm.DynamicApi.Attributes;
using Slm.Utils.Core.Extensions;
using Slm.Utils.Core.Models;
using SqlSugar;
using Sys.Application.Org.Dto;
using Sys.Domain.Org;
using Sys.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Org;


/// <summary>
/// 机构(部门)服务
/// </summary>
[DynamicApi(Area = SsyAreaConst.Area)]
[Order(5)]
[AllowAnonymous]
public class OrgService : ServiceAbstract<OrgEntity, InOrgDto, OutOrgDto, InOrgSearchDto, OutOrgTableDto, long>, IDynamicApi
{
    /// <summary>
    /// 菜单仓储
    /// </summary>
    public IOrgRepository _orgRepository => AbpLazyServiceProvider.LazyGetRequiredService<IOrgRepository>();



    /// <summary>
    /// 获取树形table
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Order(110)]
    public async Task<List<OutOrgTreeTableDto>> TreeTable(InOrgSearchTreeTableDto dto)
    {
        Expression<Func<OrgEntity, bool>> whereExpression = a => 1 == 1;
        whereExpression.And(a => SqlFunc.Like(a.Name, dto.Name), dto.Name.NotNull());
        var trees = await _orgRepository.TreeTable(dto.Pid, whereExpression);

        if (dto.Pid != 0)
        {
            var org = await _orgRepository.GetByIdAsync(dto.Pid);
            org.Children = trees;
            trees = new List<OrgEntity> { org };
        }
        var result = _mapper.Map<List<OutOrgTreeTableDto>>(trees);
        return result;
    }


    /// <summary>
    /// 获取级联select
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Order(111)]
    public async Task<List<OutCascaderDto>> CascaderSelect()
    {
        var trees = await _orgRepository.TreeTable(0);
        var result = _mapper.Map<List<OutCascaderDto>>(trees);
        return result;
    }



}