
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Slm.DynamicApi.Attributes;
using Slm.Modularity.Abstractions;
using Slm.Swashbuckle.Filter;
using Slm.Swashbuckle.Options;
using Slm.Utils.Core;
using Slm.Utils.Core.ConfigurableOptions.Extensions;
using Slm.Utils.Core.Helpers;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Reflection;
using System.Text.RegularExpressions;
namespace Slm.Swashbuckle;

public class AppSwashbuckleModule : AppModule
{
    public override void PostConfigureServices()
    {

        InternalApp.Services!.AddConfigurableOptions<SwashbuckleOptions>();


        InternalApp.Services!.AddSwaggerGen(c =>
        {
            //加载xml文档
            AssemblyHelper assemblyHelper = new AssemblyHelper();

            var swaggers = App.GetOptions<SwashbuckleOptions>();

            foreach (var item in swaggers.SwashbuckleConfigs)
            {
                c.SwaggerDoc(item.Code, new OpenApiInfo
                {
                    Title = $"{item.Name}({item.Code})",
                    Version = item.Version,
                    Description = item.Description
                });
            }
            string[] xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
            if (xmlFiles.Length > 0)
            {
                foreach (var xmlFile in xmlFiles)
                {
                    c.IncludeXmlComments(xmlFile, true);
                }
            }

            #region 移除
            //c.CustomOperationIds(apiDesc =>
            //{
            //    var controllerAction = apiDesc.ActionDescriptor as ControllerActionDescriptor;
            //    return controllerAction.ActionName;
            //});
            ////接口全路径
            //c.CustomOperationIds(apiDesc =>
            //{
            //    var controllerAction = apiDesc.ActionDescriptor as ControllerActionDescriptor;
            //    var api = controllerAction!.AttributeRouteInfo!.Template!;
            //    api = Regex.Replace(api, @"[\{\\\/\}]", "-") + "-" + apiDesc.HttpMethod!.ToLower();
            //    return api.Replace("--", "-");
            //});
            //c.ResolveConflictingActions(apiDescription =>
            //{
            //    return apiDescription.First();
            //});


            //string DefaultSchemaIdSelector(Type modelType)
            //{
            //    var modelName = modelType.Name;
            //    if (modelType.IsConstructedGenericType)
            //    {
            //        var prefix = modelType.GetGenericArguments()
            //        .Select(DefaultSchemaIdSelector)
            //        .Aggregate((previous, current) => previous + current);
            //        modelName = modelName.Split('`').First() + prefix;
            //    }
            //    else
            //    {
            //        modelName = modelName.Replace("[]", "Array");
            //    }
            //    return modelName;
            //}
            //c.CustomSchemaIds(modelType =>
            //{
            //    return DefaultSchemaIdSelector(modelType);
            //});

            #endregion


            // 动态webapi需要
            c.DocInclusionPredicate((docName, description) =>
            {
                var nonGroup = false;
                var groupNames = new List<string>();
                var dynamicApiAttribute = description.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x is DynamicApiAttribute);
                if (dynamicApiAttribute != null)
                {
                    var dynamicApi = dynamicApiAttribute as DynamicApiAttribute;
                    if (dynamicApi.Area?.Length > 0)
                    {
                        groupNames.Add(dynamicApi.Area);
                    }
                }
                return docName == description.GroupName || groupNames.Any(a => a == docName) || nonGroup;

            });



            var securityScheme = new OpenApiSecurityScheme
            {
                Description = "JWT认证请求头格式: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            };

            //添加设置Token的按钮
            c.AddSecurityDefinition("Bearer", securityScheme);

            //添加Jwt验证设置,如果说无授权则不加锁
            c.OperationFilter<AddAuthHeaderOperationFilter>();
            //隐藏  httpget 请求的参数,主要是分页参数
            c.OperationFilter<SwaggerJsonIgnoreFilter>();

            //地址和描述,文档暴露给外面的
            var server = new OpenApiServer()
            {
                Url = "www.xxxx.com",
                Description = "无敌文档"
            };
            server.Extensions.Add("extensions", new OpenApiObject
            {
                ["copyright"] = new OpenApiString("201944")
            });
            c.AddServer(server);
            //枚举
            c.SchemaFilter<EnumSchemaFilter>();


            //排序
            c.DocumentFilter<OrderTagsDocumentFilter>();
            //控制器排序
            c.OrderActionsBy(apiDesc =>
            {
                var order = 0;
                var objOrderAttribute = apiDesc.CustomAttributes().FirstOrDefault(x => x is OrderAttribute);
                if (objOrderAttribute != null)
                {
                    var orderAttribute = objOrderAttribute as OrderAttribute;
                    order = orderAttribute.Value;
                }
                return order.ToString();
            });

        });
        //var sw = new Stopwatch();
        //sw.Start();
        //sw.Stop();
        ConsoleHelper.WriteColorLine("AppSwashbuckleModule(PostConfigureServices)==========", ConsoleColor.Green);
        ConsoleHelper.WriteColorLine($"Swagger服务注入", ConsoleColor.Green);
    }

    public override void OnApplicationConfigure()
    {


        InternalApp.ApplicationBuilder!.UseSwagger();
        //InternalApp.ApplicationBuilder!.UseSwaggerUI(c =>
        //{
        //    var swaggers = App.GetOptions<SwashbuckleOptions>();
        //    foreach (var item in swaggers.SwashbuckleConfigs)
        //    {
        //        c.SwaggerEndpoint($"/swagger/{item.Code}/swagger.json", $"{item.Name}({item.Code})");
        //    }
        //    //启用过滤
        //    c.EnableFilter();
        //    //c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        //    //model删除
        //    c.DefaultModelsExpandDepth(-1);
        //    c.RoutePrefix = string.Empty;
        //});


        InternalApp.ApplicationBuilder!.UseKnife4UI(c =>
        {
            var swaggers = App.GetOptions<SwashbuckleOptions>();
            foreach (var item in swaggers.SwashbuckleConfigs)
            {
                c.SwaggerEndpoint($"/swagger/{item.Code}/swagger.json", $"{item.Name}({item.Code})");
            }
            ////启用过滤
            ////c.EnableFilter();
            //c.DocExpansion(DocExpansion.None);
            ////model删除
            //c.DefaultModelsExpandDepth(-1);
            c.RoutePrefix = string.Empty;
        });


        ConsoleHelper.WriteColorLine("AppSwashbuckleModule(OnApplicationConfigure)==========", ConsoleColor.Green);
        ConsoleHelper.WriteColorLine("Swagger中间件设置", ConsoleColor.Green);
    }
}
