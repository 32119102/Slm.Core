using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slm.Utils.Core;

/// <summary>
/// 内部App用于赋值
/// </summary>
public static class InternalApp
{
    /// <summary>
    /// 应用服务
    /// </summary>
    public static IServiceCollection? Services;

    /// <summary>
    /// 根服务
    /// </summary>
    public static IServiceProvider? ServiceProvider;

    /// <summary>
    /// 配置对象
    /// </summary>
    public static IConfiguration? Configuration;

    /// <summary>
    /// 获取Web主机环境
    /// </summary>
    public static IWebHostEnvironment? WebHostEnvironment;

    /// <summary>
    /// 获取泛型主机环境
    /// </summary>
    public static IHostEnvironment? HostEnvironment;

    public static IApplicationBuilder? ApplicationBuilder;



    /// <summary>
    /// 
    /// </summary>
    /// <param name="webApplicationBuilder"></param>
    public static void ConfigureApplication(this WebApplicationBuilder webApplicationBuilder)
    {
        WebHostEnvironment = webApplicationBuilder.Environment;
        Services = webApplicationBuilder.Services;
        Configuration = webApplicationBuilder.Configuration;

        ConfigureHostAppConfiguration(webApplicationBuilder.WebHost);


    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    public static void ConfigureApplication(this WebApplication app)
    {
        ServiceProvider = app.Services;
        ApplicationBuilder = app;
    }



    /// <summary>
    /// 自动装载主机配置
    /// </summary>
    /// <param name="builder"></param>
    private static void ConfigureHostAppConfiguration(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((hostContext, configurationBuilder) =>
        {
            // 存储环境对象
            HostEnvironment = hostContext.HostingEnvironment;

            // 加载配置
            AddJsonFiles(configurationBuilder, hostContext.HostingEnvironment);
        });
    }



    /// <summary>
    /// 加载自定义 .json 配置文件
    /// </summary>
    /// <param name="configurationBuilder"></param>
    /// <param name="hostEnvironment"></param>
    internal static void AddJsonFiles(IConfigurationBuilder configurationBuilder, IHostEnvironment hostEnvironment)
    {
        var configuration = configurationBuilder is ConfigurationManager
       ? (configurationBuilder as ConfigurationManager)
       : configurationBuilder.Build();

        // 获取程序执行目录
        var executeDirectory = AppContext.BaseDirectory;

        // 获取自定义配置扫描目录
        var configurationScanDirectories = (configuration.GetSection("ConfigurationScanDirectories")
                .Get<string[]>()
            ?? Array.Empty<string>()).Select(u => Path.Combine(executeDirectory, u));

        // 扫描执行目录及自定义配置目录下的 *.json 文件
        var jsonFiles = new[] { executeDirectory }
                            .Concat(configurationScanDirectories)
                            .SelectMany(u =>
                                Directory.GetFiles(u, "*.json", SearchOption.TopDirectoryOnly));


        // 如果没有配置文件，中止执行
        if (!jsonFiles.Any()) return;

        // 获取环境变量名，如果没找到，则读取 NETCORE_ENVIRONMENT 环境变量信息识别（用于非 Web 环境）
        var envName = hostEnvironment?.EnvironmentName ?? Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT") ?? "Unknown";

        // 读取忽略的配置文件
        var ignoreConfigurationFiles = (configuration.GetSection("IgnoreConfigurationFiles")
                .Get<string[]>() ?? Array.Empty<string>());

        // 处理控制台应用程序
        var _excludeJsonPrefixs = hostEnvironment == default ? excludeJsonPrefixs.Where(u => !u.Equals("appsettings")) : excludeJsonPrefixs;

        // 将所有文件进行分组
        var jsonFilesGroups = SplitConfigFileNameToGroups(jsonFiles)
                                                                .Where(u => !_excludeJsonPrefixs.Contains(u.Key, StringComparer.OrdinalIgnoreCase) && !u.Any(c => runtimeJsonSuffixs.Any(z => c.EndsWith(z, StringComparison.OrdinalIgnoreCase)) || ignoreConfigurationFiles.Contains(Path.GetFileName(c), StringComparer.OrdinalIgnoreCase) || ignoreConfigurationFiles.Any(i => new Matcher().AddInclude(i).Match(Path.GetFileName(c)).HasMatches)));

        // 遍历所有配置分组
        foreach (var group in jsonFilesGroups)
        {
            // 限制查找的 json 文件组
            var limitFileNames = new[] { $"{group.Key}.json", $"{group.Key}.{envName}.json" };

            // 查找默认配置和环境配置
            var files = group.Where(u => limitFileNames.Contains(Path.GetFileName(u), StringComparer.OrdinalIgnoreCase))
                                             .OrderBy(u => Path.GetFileName(u).Length);

            // 循环加载
            foreach (var jsonFile in files)
            {
                configurationBuilder.AddJsonFile(jsonFile, optional: true, reloadOnChange: true);
            }
        }

    }


    /// <summary>
    /// 排除的配置文件前缀
    /// </summary>
    private static readonly string[] excludeJsonPrefixs = new[] { "appsettings", "bundleconfig", "compilerconfig" };

    /// <summary>
    /// 排除运行时 Json 后缀
    /// </summary>
    private static readonly string[] runtimeJsonSuffixs = new[]
    {
            "deps.json",
            "runtimeconfig.dev.json",
            "runtimeconfig.prod.json",
            "runtimeconfig.json",
            "staticwebassets.runtime.json"
        };

    /// <summary>
    /// 对配置文件名进行分组
    /// </summary>
    /// <param name="configFiles"></param>
    /// <returns></returns>
    private static IEnumerable<IGrouping<string, string>> SplitConfigFileNameToGroups(IEnumerable<string> configFiles)
    {
        // 分组
        return configFiles.GroupBy(Function);

        // 本地函数
        static string Function(string file)
        {
            // 根据 . 分隔
            var fileNameParts = Path.GetFileName(file).Split('.', StringSplitOptions.RemoveEmptyEntries);
            if (fileNameParts.Length == 2) return fileNameParts[0];

            return string.Join('.', fileNameParts.Take(fileNameParts.Length - 2));
        }
    }


}
