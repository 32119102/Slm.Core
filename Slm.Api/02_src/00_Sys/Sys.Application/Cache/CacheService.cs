using EasyCaching.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Slm.Cache;
using Slm.DynamicApi;
using Slm.DynamicApi.Attributes;
using Slm.Utils.Core;
using Slm.Utils.Core.DependencyInjection;
using Sys.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Cache;




/// <summary>
/// 缓存服务
/// </summary>
[DynamicApi(Area = SsyAreaConst.Area)]
[AllowAnonymous]
[Order(0)]
public class CacheService : IDynamicApi
{
    /// <summary>
    /// 赖解析
    /// </summary>
    public IAbpLazyServiceProvider AbpLazyServiceProvider { get; set; } = default!;

    /// <summary>
    /// 缓存解析
    /// </summary>
    public IEasyCachingProvider _easyCachingProvider => AbpLazyServiceProvider.LazyGetRequiredService<IEasyCachingProvider>();
    /// <summary>
    /// 缓存解析
    /// </summary>
    public IRedisCachingProvider _redisCachingProvider => AbpLazyServiceProvider.LazyGetRequiredService<IRedisCachingProvider>();

    /// <summary>
    /// 获取缓存键名集合
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<string>> KeyList()
    {
        var _options = App.GetOptionsMonitor<CacheOptions>();
        var keys = await _redisCachingProvider.SearchKeysAsync($"{_options.Redis.DBConfig.KeyPrefix}*");
        return keys.Select(a=>a.Replace(_options.Redis.DBConfig.KeyPrefix,"")).OrderBy(a => a).ToList();

    }



    /// <summary>
    /// 获取key值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    [HttpGet("key")]
    public async Task<object> Value(string key)
    {

        var obj = await _easyCachingProvider.GetAsync(key, typeof(object));
        return obj;
    }

}

