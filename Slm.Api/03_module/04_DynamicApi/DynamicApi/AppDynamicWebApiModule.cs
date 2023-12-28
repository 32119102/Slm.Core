using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using Slm.Modularity.Abstractions;
using Slm.Utils.Core;
using Slm.Utils.Core.Helpers;
using System.Reflection;

namespace Slm.DynamicApi;

public class AppDynamicWebApiModule : AppModule
{

    public override void ConfigureServices()
    {


        //新增动态weiApi
        InternalApp.Services!.AddDynamicApi(options =>
        {
            // 指定全局默认的 api 前缀
            //options.DefaultApiPrefix = "api";

            /**
             * 自定义 ActionName 处理函数;
             */     
            options.GetRestFulActionName = (actionName) => actionName.Replace("Aaync", "");
            options.RemoveControllerPostfixes = new List<string>() { "Service" };
        });

        ConsoleHelper.WriteColorLine("AppDynamicWebApiModule(ConfigureServices)==========", ConsoleColor.Green);
        ConsoleHelper.WriteColorLine("动态api设置", ConsoleColor.Green);
    }

}