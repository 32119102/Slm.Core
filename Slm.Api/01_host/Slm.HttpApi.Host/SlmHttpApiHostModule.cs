using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Slm.Modularity.Abstractions;
using Slm.Utils.Core;
using Slm.Utils.Core.Converters;
using Slm.Utils.Core.Extensions;
using Slm.Utils.Core.Json.Converters;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Slm.Utils.Core.Json;
using Slm.Auth.Web.Attributes;
using Slm.Validation.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Slm.Auth.Jwt;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Slm.Utils.Core.Helpers;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Slm.Auth.Web.Middleware;
using Slm.DynamicApi;
using Slm.Modularity.Core;
using Slm.Swashbuckle;
using Slm.Data.Core;
using Slm.Local.Event;
using Slm.Mapster;
using Sys.HttpApi;
using Slm.Validation.FluentValidation;
using Slm.Cache;

namespace Slm.HttpApi.Host;

/// <summary>
/// 模块入口启动程序
/// </summary>

[DependsOn(
   typeof(SysHttpApiModule),
   typeof(AppLocalEventModule),
   typeof(AppFluentValidationModule),
   typeof(AppSwashbuckleModule),
   typeof(AppDynamicWebApiModule),
   typeof(AppSqlSugarModule),
   typeof(AppCacheModule),
   typeof(AppMapsterModule)
  )]
public class SlmHttpApiHostModule : AppModule
{
    public override void PreConfigureServices()
    {
        //添加http上下文访问器，用于获取认证信息
        InternalApp.Services!.AddHttpContextAccessor();



        #region 解决Multipart body length limit 134217728 exceeded
        //int siz = App.app("FileUpload:Size").ToInt();
        //InternalApp.Services!.Configure<FormOptions>(options =>
        //{
        //    options.ValueLengthLimit = siz * 1024 * 1024;
        //    options.MultipartBodyLengthLimit = siz * 1024 * 1024;
        //});
        //InternalApp.Services!.Configure<KestrelServerOptions>(options =>
        //{
        //    //28.6M 默认   50*1024**1024
        //    options.Limits.MaxRequestBodySize = siz * 1024 * 1024;
        //    options.Limits.MaxRequestBufferSize = siz * 1024 * 1024;
        //});
        #endregion


        //添加HttpClient相关,服务里面可以发起HTTP请求
        InternalApp.Services!.AddHttpClient();
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


        #region 设置过滤器 返回json格式
        InternalApp.Services!.AddMvc(c =>
        {    //模型验证
            c.Filters.Add<ValidateResultFormatAttribute>();
            //结果处理
            c.Filters.Add<FormatResultAttribute>(20);

            //日志记录
            c.Filters.Add<AuditingLogAttribute>();
            //权限验证
            c.Filters.Add<ValidatePermissionAttribute>();
        })
         .AddJsonOptions(options =>
         {
             //不区分大小写的反序列化
             options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
             //属性名称使用 camel 大小写
             options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
             //最大限度减少字符转义
             options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
             //添加日期转换器
             options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
             //添加Long转换器
             options.JsonSerializerOptions.Converters.Add(new LongConverter());
             //添加多态嵌套序列化
             options.JsonSerializerOptions.AddPolymorphism();
         }).AddControllersAsServices();
        #endregion 设置返回json格式

        #region 认证授权
        var jwtConfig = App.GetOptions<JwtOptions>();
        //添加身份认证服务
        InternalApp.Services!.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                //配置令牌验证参数
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig!.Key)),
                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Audience
                };
                //自定义配置
                //configure?.Invoke(options);
            });

        #endregion 认证授权

        #region 跨域设置
        InternalApp.Services!.AddCors(options =>
        {
            /*浏览器的同源策略，就是出于安全考虑，浏览器会限制从脚本发起的跨域HTTP请求（比如异步请求GET, POST, PUT, DELETE, OPTIONS等等，
                    所以浏览器会向所请求的服务器发起两次请求，第一次是浏览器使用OPTIONS方法发起一个预检请求，第二次才是真正的异步请求，
                    第一次的预检请求获知服务器是否允许该跨域请求：如果允许，才发起第二次真实的请求；如果不允许，则拦截第二次请求。
                    Access-Control-Max-Age用来指定本次预检请求的有效期，单位为秒，，在此期间不用发出另一条预检请求。*/
            var preflightMaxAge = new TimeSpan(0, 30, 0);
            options.AddPolicy("Default",
                builder => builder.SetIsOriginAllowed(_ => true)
                    .SetPreflightMaxAge(preflightMaxAge)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithExposedHeaders("Content-Disposition"));//下载文件时，文件名称会保存在headers的Content-Disposition属性里面
        });
        #endregion 跨域设置
        ConsoleHelper.WriteColorLine("SlmHttpApiHostModule(PreConfigureServices)==========", ConsoleColor.Green);
        ConsoleHelper.WriteColorLine("注册主程序服务", ConsoleColor.Green);
    }


    public override void OnApplicationConfigure()
    {
        InternalApp.ApplicationBuilder!.UseMiddleware<ExceptionHandleMiddleware>();
        InternalApp.ApplicationBuilder!.UseCors("Default");
        InternalApp.ApplicationBuilder!.UseStaticFiles();
        InternalApp.ApplicationBuilder!.UseDirectoryBrowser(new DirectoryBrowserOptions
        {
            FileProvider = new PhysicalFileProvider(
                                   Path.Combine(InternalApp.HostEnvironment!.ContentRootPath, "wwwroot")),
            RequestPath = "/wwwroot"
        });
        #region 设置wwwroot能够访问的格式
        //var provider = new FileExtensionContentTypeProvider(new Dictionary<string, string>
        //{
        //    [".apk"] = "application//wnd.android.package--archive",
        //    [".log"] = "application/octet-stream"
        //});
        //InternalApp.ApplicationBuilder!.UseStaticFiles(new StaticFileOptions
        //{
        //    FileProvider = new PhysicalFileProvider(
        //                    Path.Combine(InternalApp.HostEnvironment!.ContentRootPath, "wwwroot")),
        //    ContentTypeProvider = provider,
        //    RequestPath = "/wwwroot"
        //});
        #endregion

        InternalApp.ApplicationBuilder!.UseRouting();
        //认证
        InternalApp.ApplicationBuilder!.UseAuthentication();
        //授权
        InternalApp.ApplicationBuilder!.UseAuthorization();
        InternalApp.ApplicationBuilder!.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        ConsoleHelper.WriteColorLine("SlmHttpApiHostModule(OnApplicationConfigure)==========", ConsoleColor.Green);
        ConsoleHelper.WriteColorLine("主程序启动成功", ConsoleColor.Green);
    }



}

