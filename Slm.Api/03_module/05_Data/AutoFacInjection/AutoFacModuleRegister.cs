using Autofac;
using Autofac.Core;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyModel;
using Slm.AutoFacInjection.Aop;
using Slm.Utils.Core.DependencyInjection;
using Slm.Utils.Core.Extensions;
using Slm.Utils.Core.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using static System.Formats.Asn1.AsnWriter;

namespace Slm.AutoFacInjection;

public class AutoFacModuleRegister : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        ConsoleHelper.WriteColorLine("======AutoFacInjection||Service,Repository,AOP ======", ConsoleColor.DarkYellow);



        ////注册AOP
        builder.RegisterType<TransactionInterceptor>();
        //builder.RegisterType<FreeRedisInterceptor>();


        //事务拦截
        var interceptorServiceTypes = new List<Type>();
        interceptorServiceTypes.Add(typeof(TransactionInterceptor));
        //interceptorServiceTypes.Add(typeof(FreeRedisInterceptor));

        //注入Service 服务
        //按照约定，应用服务必须采用Service结尾
        AssemblyHelper assemblyHelper = new AssemblyHelper();
        var assemblyList = assemblyHelper.LoadByNameEndStringArry(".Application");
        foreach (var assembly in assemblyList)
        {

            //有接口实例
            builder.RegisterAssemblyTypes(assembly!)
            .Where(m => m.Name.EndsWith("Service") && !m.IsInterface)
            .AsImplementedInterfaces()
                 .PropertiesAutowired()// 属性注入
            .InstancePerLifetimeScope();
            //.InterceptedBy(interceptorServiceTypes.ToArray())
            //.EnableInterfaceInterceptors();


            //无接口实例---执行方法需要是 virtual
            builder.RegisterAssemblyTypes(assembly!)
            .Where(m => m.Name.EndsWith("Service") && !m.IsInterface)
            .InstancePerLifetimeScope()
             .PropertiesAutowired();// 属性注入
            //.InterceptedBy(interceptorServiceTypes.ToArray())
            //.EnableClassInterceptors();

        }


        assemblyList = assemblyHelper.LoadByNameEndStringArry(".Domain.Sqlsugar");


        foreach (var assembly in assemblyList)
        {

            builder.RegisterAssemblyTypes(assembly!)
            .Where(m =>
                      m.Name.EndsWith("Repository") && !m.IsInterface)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            // .PropertiesAutowired();// 属性注入会影响到切库功能
        }


        ////手动一个个的注入
        //foreach (var assembly in assemblyList)
        //{
        //    var scopeds = assembly.GetTypes()
        //           .Where(m => !m.IsInterface&&m.Name.Contains("Repository")).ToList();
        //    foreach (var item in scopeds)
        //    {
        //      var dd=  item.GetInterfaces().Except(item.BaseType.GetInterfaces()).ToArray();


        //        var interfaceTypes = item.GetInterfaces().Where(a=>a.Name.Contains("IBaseRepository")).ToList();
        //        var interfaceType = dd.FirstOrDefault();
        //        builder.RegisterType(item).As(interfaceType).AsImplementedInterfaces().InstancePerLifetimeScope();
        //    }


        //}

    }
}