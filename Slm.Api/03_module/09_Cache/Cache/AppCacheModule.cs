using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Slm.Modularity.Abstractions;
using Slm.Utils.Core;
using Slm.Utils.Core.Helpers;
using Slm.Utils.Core.Json.Converters;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Slm.Utils.Core.Json;
using Slm.Utils.Core.ConfigurableOptions.Extensions;
using FreeRedis;
namespace Slm.Cache;

public class AppCacheModule : AppModule
{
    public override void ConfigureServices()
    {
        JsonHelper jsonHelper = new JsonHelper();

        InternalApp.Services!.AddConfigurableOptions<CacheOptions>();
        var cacheConfig = App.GetOptions<CacheOptions>();
        RedisClient cli = new RedisClient(cacheConfig.ConnectionString);
        //cli.Serialize = obj => jsonHelper.Serialize(obj);
        //cli.Deserialize = (json, type) => jsonHelper.Deserialize(json,type);
        if (InternalApp.HostEnvironment!.EnvironmentName == "Development") {
            cli.Notice += (s, e) => Console.WriteLine(e.Log); //打印命令日志
        }
  

        InternalApp.Services!.AddSingleton(cli);


        ConsoleHelper.WriteColorLine("AppCacheModule(ConfigureServices)==========", ConsoleColor.Green);
        ConsoleHelper.WriteColorLine("缓存EasyCaching配置成功", ConsoleColor.Green);
    }
}
