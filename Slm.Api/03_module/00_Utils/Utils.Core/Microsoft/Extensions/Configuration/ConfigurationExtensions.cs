using Microsoft.Extensions.Configuration;
using Slm.Utils.Core;
using Slm.Utils.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Configuration;


/// <summary>
/// 配置扩展
/// </summary>
public static class ConfigurationExtensions
{

    /// <summary>
    /// 刷新配置对象
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IConfiguration Reload(this IConfiguration configuration)
    {
        if (App.ServiceProvider == null) return configuration;

        var newConfiguration = App.GetService<IConfiguration>(App.ServiceProvider);
        InternalApp.Configuration = newConfiguration;

        return newConfiguration;
    }



    /// <summary>
    /// 获取指定类型的实例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="cfg"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static T Get<T>(this IConfiguration cfg, string key) where T : class, new()
    {
        if (cfg == null || key.IsNull())
            return new T();

        //Microsoft.Extensions.Configuration.Binder Get<T> 扩展方法
        var t = cfg.GetSection(key).Get<T>();
        if (t == null)
            return new T();

        return t;
    }
}
