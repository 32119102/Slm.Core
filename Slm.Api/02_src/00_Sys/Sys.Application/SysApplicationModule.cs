using Microsoft.Extensions.DependencyInjection;
using Slm.Modularity.Abstractions;
using Slm.Modularity.Core;
using Slm.Utils.Core;
using Sys.Domain;

namespace Sys.Application;     

/// <summary>
/// 服务实现
/// </summary>
[DependsOn(
   typeof(SysDomainModule)
)]
public class SysApplicationModule : AppModule
{

    public override void ConfigureServices()
    {
        // 验证码
        InternalApp.Services!.AddCaptcha();
    }
}
