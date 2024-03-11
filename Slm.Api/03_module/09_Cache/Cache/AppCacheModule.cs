using EasyCaching.Core.Configurations;
using EasyCaching.Core.Serialization;
using EasyCaching.Serialization.SystemTextJson;
using EasyCaching.Serialization.SystemTextJson.Configurations;
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
namespace Slm.Cache;

public class AppCacheModule : AppModule
{
    public override void ConfigureServices()
    {

        InternalApp.Services!.AddConfigurableOptions<CacheOptions>();

        //到时候走配置
        InternalApp.Services!.AddSingleton<IEasyCachingSerializer, DefaultJsonSerializer>(x =>
        {
            var _options = new JsonSerializerOptions();
            //不区分大小写的反序列化
            _options.PropertyNameCaseInsensitive = true;
            //属性名称使用 camel 大小写
            _options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            //最大限度减少字符转义
            _options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            //添加日期转换器
            _options.Converters.Add(new DateTimeConverter());

            //添加多态嵌套序列化
            _options.AddPolymorphism();
            return new DefaultJsonSerializer("json", _options);
        });

      



        var cacheConfig = App.GetOptions<CacheOptions>();

        InternalApp.Services!.AddEasyCaching(option =>
        {
            option.UseRedis(config =>
            {
                config.EnableLogging = true;
                config.DBConfig = cacheConfig.Redis.DBConfig;
                config.SerializerName = "json";
            }).WithSystemTextJson("json");

        });
        ConsoleHelper.WriteColorLine("AppCacheModule(ConfigureServices)==========", ConsoleColor.Green);
        ConsoleHelper.WriteColorLine("缓存EasyCaching配置成功", ConsoleColor.Green);
    }
}
