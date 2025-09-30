using Microsoft.Extensions.DependencyInjection;
using Slm.Auth.Jwt;
using Slm.Modularity.Abstractions;
using Slm.Modularity.Core;
using Slm.Utils.Core;
using Sys.Domain;
using Slm.Utils.Core.ConfigurableOptions.Extensions;


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
        ////如果使用redis分布式缓存
        //InternalApp.Services!.AddRedisCacheCaptcha(options =>
        //{
        //    options.Configuration = builder.Configuration.GetConnectionString("RedisCache");
        //    options.InstanceName = "captcha:";
        //});

        //如果使用redis分布式缓存
        InternalApp.Services!.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = App.app("ConnectionStrings:RedisCache");
            options.InstanceName = App.app("ConnectionStrings:Prefix");
        });

        InternalApp.Services!.AddConfigurableOptions<JwtOptions>();
    }
}
