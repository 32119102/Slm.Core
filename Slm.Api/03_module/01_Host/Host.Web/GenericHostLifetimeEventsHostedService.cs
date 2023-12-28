using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using HostOptions = Wj.Host.Options.HostOptions;
using Slm.Utils.Core;
using Slm.Utils.Core.Helpers;

namespace Slm.Host;


/// <summary>
/// 项目启动加载
/// </summary>
public class GenericHostLifetimeEventsHostedService: IHostedService
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="host"></param>
    public GenericHostLifetimeEventsHostedService(IHost host)
    {
        //ConsoleHelper.WriteErrorLine("构造函数");
        // 存储根服务  
        if (InternalApp.ServiceProvider == null) {
            InternalApp.ServiceProvider = host.Services;
        }
    
    }

    /// <summary>
    /// 监听主机启动
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        ConsoleHelper.WriteErrorLine("项目启动");
        ////显示启动Banner
        var options = App.GetService<HostOptions>();
        ConsoleHelper.ConsoleBanner(options?.Urls!);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 监听主机停止
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        ConsoleHelper.WriteErrorLine("项目停止");
        return Task.CompletedTask;
    }
}
