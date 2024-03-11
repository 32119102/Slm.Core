using EasyCaching.Redis;
using Microsoft.Extensions.Configuration;
using Slm.Utils.Core.ConfigurableOptions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slm.Cache;

/// <summary>
/// 缓存配置选项
/// </summary>
public sealed class CacheOptions : IConfigurableOptions
{

    /// <summary>
    /// 缓存类型
    /// </summary>
    public string CacheType { get; set; }

    /// <summary>
    /// redis配置
    /// </summary>
    public RedisOptions Redis { get; set; }




}


public sealed class RedisOption : RedisOptions
{
    /// <summary>
    /// 别名
    /// </summary>
    public string Name { get; set; }


}



