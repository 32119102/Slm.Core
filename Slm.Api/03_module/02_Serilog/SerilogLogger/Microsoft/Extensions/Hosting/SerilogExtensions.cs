using Serilog;
using Serilog.Filters;
using Slm.SerilogLogger;
using Slm.Utils.Core;
using Slm.Utils.Core.Const;

namespace Microsoft.Extensions.Hosting;

/// <summary>
/// 日志扩展
/// </summary>
public static class SerilogExtensions
{
    /// <summary>
    /// 日志扩展
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="serilogConfig"></param>
    /// <returns></returns>
    public static IHostBuilder UseSlmSerilog(this IHostBuilder builder, SerilogFileOptions serilogConfig)
    {
        LoggerConfiguration logger = new LoggerConfiguration()
            .ReadFrom.Configuration(App.Configuration)      //读取配置文件
            .Enrich.FromLogContext();                             //添加记录信息
                                                                  //.MinimumLevel.Debug(); // 所有Sink的最小记录级别
        foreach (var item in serilogConfig.FileFolders)
        {
            logger = logger.WriteTo.Logger(lg =>
                        lg.Filter.ByIncludingOnly(Matching.WithProperty<string>(SerilogConst.Placeholder, p => p == item))
                        .WriteTo.Async(a =>
                                     a.File(string.Format(serilogConfig.FilePath, item), rollingInterval: RollingInterval.Day, outputTemplate: serilogConfig.OutputTemplate))
            );
        }
        Log.Logger = logger.CreateLogger();
        return builder.UseSerilog(Log.Logger);
    }
}


//static string LogFilePath(string FileName) => $@"ALL_Logs\{FileName}\log.log";
//string SerilogOutputTemplate = "{NewLine}Date：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}LogLevel：{Level}{NewLine}Message：{Message}{NewLine}{Exception}" + new string('-', 100);
//Log.Logger = new LoggerConfiguration()
//            .Enrich.FromLogContext()
//            .MinimumLevel.Debug() // 所有Sink的最小记录级别
//            .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(Matching.WithProperty<string>("position", p => p == "WebLog")).WriteTo.Async(a => a.File(LogFilePath("WebLog"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate)))
//            .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(Matching.WithProperty<string>("position", p => p == "ApiLog")).WriteTo.Async(a => a.File(LogFilePath("ApiLog"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate)))
//            .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(Matching.WithProperty<string>("position", p => p == "ErrorLog")).WriteTo.Async(a => a.File(LogFilePath("ErrorLog"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate)))
//            .CreateLogger();


//var elapsedMs2 = 34;

//var position1 = new { Latitude = 25, Longitude = 134 };
//Log.Information("{position}:111111,{elapsedMs2}", "WebLog", elapsedMs2);
//Log.Information("{position}:222222,{@Position}+>{url}", "ApiLog", position1,"我是字符串");
//Log.Information("{position}:333333", "ErrorLog");
