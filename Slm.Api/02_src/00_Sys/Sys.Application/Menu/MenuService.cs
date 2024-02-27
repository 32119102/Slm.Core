using Microsoft.AspNetCore.Authorization;
using Slm.Data.Core.Service;
using Slm.DynamicApi.Attributes;
using Slm.DynamicApi;
using Sys.Domain.Shared;
using Sys.Domain.Menu;
using Sys.Application.Menu.Dto;
using Microsoft.AspNetCore.Mvc;
using Slm.Data.Abstractions.Attributes;
using Sys.Domain.Shared.Menu;
using Yitter.IdGenerator;
using Sys.Domain.Menu2Api;
using Slm.Utils.Core.Models;
using Sys.Application.Api.Dto;

namespace Sys.Application.Menu;


/// <summary>
/// 菜单服务
/// </summary>
[DynamicApi(Area = SsyAreaConst.Area)]
[Order(3)]
[AllowAnonymous]
public class MenuService : ServiceAbstract<MenuEntity, InMenuDto, OutMenuDto, InMenuSearchDto, OutMenuTableDto, long>, IDynamicApi
{
    /// <summary>
    /// 菜单仓储
    /// </summary>
    public IMenuRepository _menuRepository => AbpLazyServiceProvider.LazyGetRequiredService<IMenuRepository>();

    public IMenu2ApiRepository _menu2ApiRepository => AbpLazyServiceProvider.LazyGetRequiredService<IMenu2ApiRepository>();



    /// <summary>
    /// 获取树形table
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Order(110)]
    public async Task<List<OutMenuTreeTableDto>> TreeTable()
    {
        var trees = await _menuRepository.TreeTable();
        var result = _mapper.Map<List<OutMenuTreeTableDto>>(trees);
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
        var trees = await _menuRepository.TreeTable(a => a.Type != MenuTypeEnum.Button);
        var result = _mapper.Map<List<OutCascaderDto>>(trees);
        return result;
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    //[Order(201)]
    [Transaction]
    public override async Task<dynamic> AddAsync(InMenuDto dto)
    {
        var entity = _mapper.Map<MenuEntity>(dto);
        long id = await _menuRepository.InsertReturnSnowflakeIdAsync(entity);
        if (dto.ButtonProps.Count > 0)
        {
            List<MenuEntity> menuEntities = new List<MenuEntity>();
            List<Menu2ApiEntity> menu2ApiEntities = new List<Menu2ApiEntity>();
            foreach (var item in dto.ButtonProps)
            {
                MenuEntity menuEntity = new MenuEntity();
                menuEntity.Id = YitIdHelper.NextId();
                menuEntity.ParentId = id;
                menuEntity.Type = MenuTypeEnum.Button;
                menuEntity.Title = item.Title;
                menuEntity.ButtonCode = item.ButtonCode;
                menuEntity.Sort = item.Sort;
                menuEntity.IsEnable = true;
                menuEntities.Add(menuEntity);

                if (item.ApiId.HasValue) 
                {
                    Menu2ApiEntity menu2ApiEntity = new Menu2ApiEntity();
                    menu2ApiEntity.MenuId = menuEntity.Id;
                    menu2ApiEntity.ApiId = item.ApiId.Value;
                    menu2ApiEntities.Add(menu2ApiEntity);
                }
                

            }
            await _menuRepository.InsertRangeAsync(menuEntities);
            await _menu2ApiRepository.InsertRangeAsync(menu2ApiEntities);


        }


        return true;
    }


    /// <summary>
    /// 获取左侧菜单
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Order(55)]
    public async Task<List<OutLeftMenuDto>> GetLeftMenu() 
    {
        var trees = await _menuRepository.TreeTable(a => a.Type != MenuTypeEnum.Button);
        var result = _mapper.Map<List<OutLeftMenuDto>>(trees);
        return result;


    }
}