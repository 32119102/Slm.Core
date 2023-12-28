using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Slm.Utils.Core.DependencyInjection;


/// <summary>
/// 缓存抽象类
/// </summary>
public abstract class CachedServiceProviderBase : ICachedServiceProviderBase
{
    /// <summary>
    /// 服务
    /// </summary>
    protected IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// 并发字典  Lazy:延迟加载
    /// </summary>
    protected ConcurrentDictionary<Type, Lazy<object?>> CachedServices { get; }

    protected CachedServiceProviderBase(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        CachedServices = new ConcurrentDictionary<Type, Lazy<object?>>();
        CachedServices.TryAdd(typeof(IServiceProvider), new Lazy<object?>(() => ServiceProvider));
    }

    /// <summary>
    /// 根据类型获取服务
    /// </summary>
    /// <param name="serviceType"></param>
    /// <returns></returns>
    public virtual object? GetService(Type serviceType)
    {
        return CachedServices.GetOrAdd(
            serviceType,
            _ => new Lazy<object?>(() => ServiceProvider.GetService(serviceType))
        ).Value;
    }


    public T GetService<T>(T defaultValue)
    {
        return (T)GetService(typeof(T), defaultValue!);
    }

    public object GetService(Type serviceType, object defaultValue)
    {
        return GetService(serviceType) ?? defaultValue;
    }

    public T GetService<T>(Func<IServiceProvider, object> factory)
    {
        return (T)GetService(typeof(T), factory);
    }

    public object GetService(Type serviceType, Func<IServiceProvider, object> factory)
    {
        return CachedServices.GetOrAdd(
            serviceType,
            _ => new Lazy<object?>(() => factory(ServiceProvider))
        ).Value!;
    }


}
