using System;
using System.Collections.Generic;
using System.Text;

namespace Slm.Utils.Core.DependencyInjection;

/// <summary>
/// 缓存服务基类
/// </summary>
public interface ICachedServiceProviderBase : IServiceProvider
{
    T GetService<T>(T defaultValue);

    object GetService(Type serviceType, object defaultValue);

    T GetService<T>(Func<IServiceProvider, object> factory);

    object GetService(Type serviceType, Func<IServiceProvider, object> factory);
}
