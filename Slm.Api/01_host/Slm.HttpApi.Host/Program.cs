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
//��ֵ��������,����josn�ļ�
builder.ConfigureApplication();

//ʹ��Serilog��־  
var serilogConfig = App.GetOptions<SerilogFileOptions>();
builder.Host.UseSlmSerilog(serilogConfig);

//ʹ��autoFac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
 .ConfigureContainer<ContainerBuilder>(builderAutoFac =>
 {
     //ע�����еĳ���
     AssemblyHelper assemblyHelper = new AssemblyHelper();
     var assemblyList = assemblyHelper.Load();
     builderAutoFac.RegisterAssemblyModules(assemblyList.ToArray()!);


 });





//var appConfig = ConfigHelper.Get<AppConfig>("appconfig", env.EnvironmentName);

////����Kestrel������
//builder.WebHost.ConfigureKestrel((context, options) =>
//{
//    //����Ӧ�÷�����Kestrel���������Ϊ100MB
//    options.Limits.MaxRequestBodySize = appConfig.MaxRequestBodySize;
//});

////���ʵ�ַ
//if (appConfig.Urls?.Length > 0)
//{
//    builder.WebHost.UseUrls(appConfig.Urls);
//}

//ִ��Module��������
builder.Services.AddApplication<SlmHttpApiHostModule>();

var app = builder.Build();
//������ֵ
app.ConfigureApplication();
//ִ��
app.Initialize();


app.Lifetime.ApplicationStarted.Register(() =>
{
    sw.Stop();
    ConsoleHelper.WriteSuccessLine($"��Ŀ����,��ʱ:{sw.ElapsedMilliseconds} ms");

});

app.Lifetime.ApplicationStopped.Register(() =>
{
    ConsoleHelper.WriteSuccessLine("��Ŀ��ͣ");
});
app.Run();
