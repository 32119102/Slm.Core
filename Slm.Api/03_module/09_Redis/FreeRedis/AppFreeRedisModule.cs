
using FreeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Slm.Modularity.Abstractions;
using Slm.Utils.Core;
using Slm.Utils.Core.Extensions;
using Slm.Utils.Core.Helpers;

namespace Slm.FreeRedis;

/// <summary>
/// Redis缓存
/// </summary>
public class AppFreeRedisModule : AppModule
{
    public override void ConfigureServices()
    {
        if (App.app("Redis:IsEnable").ToBool()) 
        {
            string str = App.app("Redis:RedisStr");

            RedisClient cli = new RedisClient(str);

            InternalApp.Services!.AddSingleton(cli);

            InternalApp.Services!.AddSingleton(new SlmRedisHelper(cli));
            ConsoleHelper.WriteColorLine("AppFreeRedisModule(ConfigureServices)==========", ConsoleColor.Green);
            ConsoleHelper.WriteColorLine("Redis缓存注入", ConsoleColor.Green);

        }

      
    }
}

