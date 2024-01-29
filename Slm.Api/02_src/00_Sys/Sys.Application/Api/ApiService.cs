using Microsoft.AspNetCore.Authorization;
using Slm.Data.Core.Service;
using Slm.DynamicApi.Attributes;
using Slm.DynamicApi;
using Sys.Domain.Shared;
using Sys.Domain.Api;
using Sys.Application.Api.Dto;
using Swashbuckle.AspNetCore.Swagger;
using Slm.Swashbuckle.Options;
using Microsoft.OpenApi.Models;
using Slm.Utils.Core.Helpers;
using Slm.Utils.Core;
using Microsoft.AspNetCore.Mvc;
using Yitter.IdGenerator;

using Slm.Utils.Core.Extensions;
using Slm.Data.Abstractions.Attributes;
using Sys.Domain.TestA;
using Slm.Utils.Core.Models;

namespace Sys.Application.Api;


/// <summary>
/// 接口API服务
/// </summary>
[DynamicApi(Area = SsyAreaConst.Area)]
[Order(2)]
[AllowAnonymous]
public class ApiService : ServiceAbstract<ApiEntity, InApiDto, OutApiDto, InApiSearchDto, OutApiTableDto, long>, IDynamicApi
{
    /// <summary>
    /// 接口API仓储
    /// </summary>
    public IApiRepository _apiRepository => AbpLazyServiceProvider.LazyGetRequiredService<IApiRepository>();

    /// <summary>
    /// swagger获取
    /// </summary>
    public IAsyncSwaggerProvider _swaggerGenerator => AbpLazyServiceProvider.LazyGetRequiredService<IAsyncSwaggerProvider>();

    public ITestARepository _testARepository => AbpLazyServiceProvider.LazyGetRequiredService<ITestARepository>();


    /// <summary>
    /// 多库事务
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Transaction]
    public  async Task<bool> Test()
    {
        var dd = await _testARepository.GetListAsync();
        await _testARepository.InsertAsync(new TestAEntity
        {
            Name = "测试"
        });

        await _apiRepository.UpdateSetColumnsTrueAsync(a => new ApiEntity
        {
            Description = "hahah"
        }, a => a.Id == 15541511206597);



        int a = int.Parse("OK");




        return true;
    }

    /// <summary>
    /// 同步
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Order(0)]
    public async Task<bool> SyncAsync()
    {
        //得到一级ID
        List<ApiEntity>? allApi = await _apiRepository.GetListAsync();
        List<ApiEntity> apis = new List<ApiEntity>();


        //新增
        List<ApiEntity> adds = new List<ApiEntity>();
        //修改
        List<ApiEntity> edits = new List<ApiEntity>();
        //删除
        List<ApiEntity> dels = new List<ApiEntity>();

        var swaggers = App.GetOptions<SwashbuckleOptions>();
        int apiOrder = 0;
        foreach (var swagger in swaggers.SwashbuckleConfigs)
        {
            var api = allApi.Where(a => a.Path == swagger.Code && a.ParentId == 0).FirstOrDefault();
            if (api == null)
            {
                api = new ApiEntity();
                api.Id = YitIdHelper.NextId();
                api.Label = swagger.Description;
                api.Path = swagger.Code;
                api.Sort = apiOrder;
                adds.Add(api);
            }
            else
            {
                if (api.Label != swagger.Description || api.Sort != apiOrder)
                {
                    api.Label = swagger.Description;
                    api.Sort = apiOrder;
                    edits.Add(api);
                }
            }
            apiOrder++;


            var list = await _swaggerGenerator.GetSwaggerAsync(swagger.Code);
            int tagOrder = 0;
            foreach (var tag in list.Tags)
            {
                var parentApi = allApi.Where(a => a.Path == tag.Name && a.ParentId == api.Id).FirstOrDefault();
                if (parentApi == null)
                {
                    parentApi = new ApiEntity();
                    parentApi.ParentId = api.Id;
                    parentApi.Id = YitIdHelper.NextId();
                    parentApi.Label = tag.Description;
                    parentApi.Path = tag.Name;
                    parentApi.Sort = tagOrder;
                    adds.Add(parentApi);
                }
                else
                {
                    if (parentApi.Label != tag.Description || parentApi.Sort != tagOrder)
                    {
                        parentApi.Label = tag.Description;
                        parentApi.Sort = tagOrder;
                        edits.Add(parentApi);
                    }
                }
                tagOrder++;
                int childOrder = 0;
                foreach (var item in list.Paths)
                {
                    foreach (KeyValuePair<OperationType, OpenApiOperation> operation in item.Value.Operations)
                    {
                        var openApi = operation.Value;
                        if (openApi.Tags.Any(a => a.Name == tag.Name))
                        {
                            string path = item.Key.ToRouteUrl().ToLower();
                            var childApi = allApi.Where(a => a.Path == path && a.ParentId == parentApi.Id).FirstOrDefault();
                            if (childApi == null)
                            {
                                childApi = new ApiEntity();
                                childApi.ParentId = parentApi.Id;
                                childApi.Id = YitIdHelper.NextId();
                                childApi.Label = openApi.Summary;
                                childApi.Path = path;
                                childApi.Sort = childOrder;
                                childApi.HttpMethods = operation.Key.ToString();
                                adds.Add(childApi);
                            }
                            else
                            {
                                if (childApi.Label != openApi.Summary || childApi.HttpMethods != operation.Key.ToString() || childApi.Sort != childOrder)
                                {
                                    childApi.Label = openApi.Summary;
                                    childApi.HttpMethods = operation.Key.ToString();
                                    childApi.Sort = childOrder;
                                    edits.Add(childApi);
                                }

                            }

                            childOrder++;
                        }
                    }
                }
            }
            foreach (var item in list.Paths)
            {
                foreach (KeyValuePair<OperationType, OpenApiOperation> operation in item.Value.Operations)
                {



                    ConsoleHelper.WriteSuccessLine(operation.Key.ToString());
                }
            }
        }

        if (adds.Count > 0)
        {
            await _apiRepository.InsertRangeAsync(adds);
        }
        if (edits.Count > 0)
        {
            await _apiRepository.UpdateRangeAsync(edits);
        }




        return true;
    }
    /// <summary>
    /// 获取树形table
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Order(1)]
    public async Task<List<OutApiTreeTableDto>> TreeTable()
    {
        var trees = await _apiRepository.TreeTable();
        var result = _mapper.Map<List<OutApiTreeTableDto>>(trees);
        return result;
    }

    /// <summary>
    /// 获取级联select
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Order(2)]
    public async Task<List<OutCascaderDto>> CascaderSelect()
    {
        var trees = await _apiRepository.TreeTable();
        var result = _mapper.Map<List<OutCascaderDto>>(trees);
        return result;
    }


}
