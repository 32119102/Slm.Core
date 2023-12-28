using Autofac;
using Autofac.Extensions.DependencyInjection;
using Dm;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Slm.HttpApi.Host;
using Slm.SerilogLogger;
using Slm.Utils.Core;
using Slm.Utils.Core.Extensions;
using Slm.Utils.Core.Helpers;
using System.Diagnostics;

var sw = new Stopwatch();
sw.Start();


var builder = WebApplication.CreateBuilder(args);
//赋值基本配置,加载josn文件
builder.ConfigureApplication();

//使用Serilog日志  
var serilogConfig = App.GetOptions<SerilogFileOptions>();
builder.Host.UseSlmSerilog(serilogConfig);

//使用autoFac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
 .ConfigureContainer<ContainerBuilder>(builderAutoFac =>
 {
     //注册所有的程序集
     AssemblyHelper assemblyHelper = new AssemblyHelper();
     var assemblyList = assemblyHelper.Load();
     builderAutoFac.RegisterAssemblyModules(assemblyList.ToArray()!);


 });





//var appConfig = ConfigHelper.Get<AppConfig>("appconfig", env.EnvironmentName);

////配置Kestrel服务器
//builder.WebHost.ConfigureKestrel((context, options) =>
//{
//    //设置应用服务器Kestrel请求体最大为100MB
//    options.Limits.MaxRequestBodySize = appConfig.MaxRequestBodySize;
//});

////访问地址
//if (appConfig.Urls?.Length > 0)
//{
//    builder.WebHost.UseUrls(appConfig.Urls);
//}

//执行Module启动加载
builder.Services.AddApplication<SlmHttpApiHostModule>();

var app = builder.Build();
//基本赋值
app.ConfigureApplication();
//执行
app.Initialize();


app.Lifetime.ApplicationStarted.Register(() =>
{
    sw.Stop();
    ConsoleHelper.WriteSuccessLine($"项目启动,耗时:{sw.ElapsedMilliseconds} ms");

});

app.Lifetime.ApplicationStopped.Register(() =>
{
    ConsoleHelper.WriteSuccessLine("项目暂停");
});
app.Run();
