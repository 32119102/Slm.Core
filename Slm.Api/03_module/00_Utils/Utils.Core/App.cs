using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Slm.Utils.Core.ConfigurableOptions.Internal;
using Slm.Utils.Core.ConfigurableOptions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace Slm.Utils.Core;

public class App
{
    /// <summary>
    /// 全局配置选项
    /// </summary>
    public static IConfiguration Configuration => CatchOrDefault(() => InternalApp.Configuration!.Reload(), new ConfigurationBuilder().Build())!;

    /// <summary>
    /// 获取Web主机环境，如，是否是开发环境，生产环境等
    /// </summary>
    public static IWebHostEnvironment WebHostEnvironment => InternalApp.WebHostEnvironment!;

    /// <summary>
    /// 获取泛型主机环境，如，是否是开发环境，生产环境等
    /// </summary>
    public static IHostEnvironment HostEnvironment => InternalApp.HostEnvironment!;

    /// <summary>
    /// 存储根服务，可能为空
    /// </summary>
    public static IServiceProvider ServiceProvider => InternalApp.ServiceProvider!;

    /// <summary>
    /// 获取请求上下文
    /// </summary>
    public static HttpContext HttpContext => CatchOrDefault(() => ServiceProvider.GetService<IHttpContextAccessor>()?.HttpContext!)!;

    /// <summary>
    /// 获取请求上下文用户
    /// </summary>
    /// <remarks>只有授权访问的页面或接口才存在值，否则为 null</remarks>
    public static ClaimsPrincipal? User => HttpContext?.User;


    /// <summary>
    /// 解析服务提供器
    /// </summary>
    /// <param name="serviceType"></param>
    /// <returns></returns>
    public static IServiceProvider GetServiceProvider(Type serviceType)
    {
        // 处理控制台应用程序
        if (HostEnvironment == default) return ServiceProvider;

        //使用微软本身的注入可以检测到，AutoFac检测不到
        // 第一选择，判断是否是单例注册且单例服务不为空，如果是直接返回根服务提供器
        if (ServiceProvider != null &&
            InternalApp.Services!
            .Where(u => u.ServiceType == (serviceType.IsGenericType ? serviceType.GetGenericTypeDefinition() : serviceType))
             .Any(u => u.Lifetime == ServiceLifetime.Singleton)) return ServiceProvider;

        //AutoFac可以获取
        // 第二选择是获取 HttpContext 对象的 RequestServices
        var httpContext = HttpContext;
        if (httpContext?.RequestServices != null) return httpContext.RequestServices;
        // 第三选择，创建新的作用域并返回服务提供器
        else if (ServiceProvider != null)
        {
            var scoped = ServiceProvider.CreateScope();

            return scoped.ServiceProvider;
        }
        // 第四选择，构建新的服务对象（性能最差）
        else
        {
            var serviceProvider = InternalApp.Services!.BuildServiceProvider();

            return serviceProvider;
        }
    }


    /// <summary>
    /// 获取请求生存周期的服务
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static TService? GetService<TService>(IServiceProvider? serviceProvider = default)
        where TService : class
    {
        return GetService(typeof(TService), serviceProvider) as TService;
    }


    /// <summary>
    /// 获取请求生存周期的服务
    /// </summary>
    /// <param name="type"></param>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static object GetService(Type type, IServiceProvider? serviceProvider = default)
    {
        return CatchOrDefault(() => (serviceProvider ?? GetServiceProvider(type)).GetService(type), default)!;
    }


    /// <summary>
    /// 获取请求生存周期的服务
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static TService GetRequiredService<TService>(IServiceProvider serviceProvider = default)
        where TService : class
    {
        return (GetRequiredService(typeof(TService), serviceProvider) as TService)!;
    }



    /// <summary>
    /// 获取请求生存周期的服务
    /// </summary>
    /// <param name="type"></param>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static object GetRequiredService(Type type, IServiceProvider serviceProvider = default)
    {
        return (serviceProvider ?? GetServiceProvider(type)).GetRequiredService(type);
    }


    /// <summary>
    /// 封装要操作的字符
    /// </summary>
    /// <param name="sections">节点配置</param>
    /// <returns></returns>
    public static string app(string sections)
    {
        try
        {
            return Configuration[sections];
        }
        catch (Exception) { }

        return "";
    }


    /// <summary>
    /// 处理获取对象异常问题
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="action">获取对象委托</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>T</returns>
    private static T? CatchOrDefault<T>(Func<T> action, T? defaultValue = null)
        where T : class
    {
        try
        {
            return action();
        }
        catch (Exception ex)
        {
            return defaultValue ?? null;
        }
    }





    /// <summary>
    /// 获取配置(项目启动程序中使用这种获取)
    /// </summary>
    /// <typeparam name="TOptions">强类型选项类</typeparam>
    /// <param name="path">配置中对应的Key</param>
    /// <param name="loadPostConfigure"></param>
    /// <returns>TOptions</returns>
    public static TOptions GetConfig<TOptions>(string path, bool loadPostConfigure = false)
    {
        var options = Configuration.GetSection(path).Get<TOptions>();

        // 加载默认选项配置
        if (loadPostConfigure && typeof(IConfigurableOptions).IsAssignableFrom(typeof(TOptions)))
        {
            var postConfigure = typeof(TOptions).GetMethod("PostConfigure");
            if (postConfigure != null)
            {
                options ??= Activator.CreateInstance<TOptions>();
                postConfigure.Invoke(options, new object[] { options!, Configuration });
            }
        }

        return options;
    }

    /// <summary>
    /// 获取选项
    /// </summary>
    /// <typeparam name="TOptions">强类型选项类</typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns>TOptions</returns>
    public static TOptions GetOptions<TOptions>(IServiceProvider serviceProvider = default)
        where TOptions : class, new()
    {
        return Penetrates.GetOptionsOnStarting<TOptions>()
            ?? GetService<IOptions<TOptions>>(serviceProvider ?? ServiceProvider)?.Value!;
    }

    /// <summary>
    /// 获取选项
    /// </summary>
    /// <typeparam name="TOptions">强类型选项类</typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns>TOptions</returns>
    public static TOptions GetOptionsMonitor<TOptions>(IServiceProvider serviceProvider = default)
        where TOptions : class, new()
    {
        return Penetrates.GetOptionsOnStarting<TOptions>()
            ?? GetService<IOptionsMonitor<TOptions>>(serviceProvider ?? ServiceProvider)?.CurrentValue!;
    }

    /// <summary>
    /// 获取选项
    /// </summary>
    /// <typeparam name="TOptions">强类型选项类</typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns>TOptions</returns>
    public static TOptions GetOptionsSnapshot<TOptions>(IServiceProvider serviceProvider = default)
        where TOptions : class, new()
    {
        // 这里不能从根服务解析，因为是 Scoped 作用域
        return Penetrates.GetOptionsOnStarting<TOptions>()
            ?? GetService<IOptionsSnapshot<TOptions>>(serviceProvider)?.Value!;
    }









}

