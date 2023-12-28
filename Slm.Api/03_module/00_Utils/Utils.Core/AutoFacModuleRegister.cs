using Autofac;
using Slm.Utils.Core.DependencyInjection;
using Slm.Utils.Core.Extensions;
using Slm.Utils.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Slm.Utils.Core;

/// <summary>
/// 自动注入(1.特性自动注入;2.配置自动添加)
/// </summary>
public class AutoFacModuleRegister : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        ConsoleHelper.WriteColorLine("======Utils.Core||自动注入(1.特性自动注入;2.配置自动添加) 开始执行======", ConsoleColor.DarkYellow);


        var assemblies = new AssemblyHelper().Load();
        foreach (var assembly in assemblies)
        {
            try
            {


                //添加注入
                AddServicesFromAssembly(builder, assembly!);
                //添加配置
                //AddConfigure(builder, assembly!);
            }
            catch
            {
                //此处防止第三方库抛出一场导致系统无法启动，所以需要捕获异常来处理一下
            }
        }
    }


    /// <summary>
    /// 从指定程序集中注入服务(ISingletonDependency,ITransientDependency,IScopedDependency   自动注入服务)
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="assembly"></param>
    public static void AddServicesFromAssembly(ContainerBuilder builder, Assembly assembly)
    {
        var singletons = assembly.GetTypes()
                          .Where(m => !m.IsInterface && typeof(ISingletonDependency).IsImplementType(m)).ToList();
        foreach (var item in singletons)
        {
            var list = item.GetCustomAttribute<ExposeServicesAttribute>()?.ServiceTypes;
            var interfaceTypes = item.GetInterfaces()
                .Where(p =>
                !p.FullName.Contains("ISingletonDependency"))
                .ToList();
            if (interfaceTypes.NotNull())
            {
                foreach (var interfaceType in interfaceTypes)
                {
                    if (list.NotNull())
                    {
                        if (list.Any(a => a.Name == interfaceType.Name))
                        {
                            builder.RegisterType(item).As(interfaceType).SingleInstance();
                        }
                    }
                    else
                    {
                        builder.RegisterType(item).As(interfaceType).SingleInstance();
                    }
                }
            }
            else
            {
                builder.RegisterType(item).SingleInstance();
            }

        }
        var transients = assembly.GetTypes()
                         .Where(m => !m.IsInterface && typeof(ITransientDependency).IsImplementType(m)).ToList();
        foreach (var item in transients)
        {
            var list = item.GetCustomAttribute<ExposeServicesAttribute>()?.ServiceTypes;
            var interfaceTypes = item.GetInterfaces().Where(p => !p.FullName.Contains("ITransientDependency")).ToList();
            if (interfaceTypes.NotNull())
            {
                foreach (var interfaceType in interfaceTypes)
                {
                    if (list.NotNull())
                    {
                        if (list.Any(a => a.Name == interfaceType.Name))
                        {
                            builder.RegisterType(item).As(interfaceType).InstancePerDependency();
                        }
                    }
                    else
                    {
                        builder.RegisterType(item).As(interfaceType).InstancePerDependency();
                    }
                }
            }
            else
            {
                builder.RegisterType(item).InstancePerDependency();
            }
        }
        var scopeds = assembly.GetTypes()
                         .Where(m => !m.IsInterface && typeof(IScopedDependency).IsImplementType(m)).ToList();
        foreach (var item in scopeds)
        {
            var list = item.GetCustomAttribute<ExposeServicesAttribute>()?.ServiceTypes;
            var interfaceTypes = item.GetInterfaces().Where(p => !p.FullName.Contains("IScopedDependency")).ToList();
            if (interfaceTypes.NotNull())
            {
                foreach (var interfaceType in interfaceTypes)
                {
                    if (list.NotNull())
                    {
                        if (list.Any(a => a.Name == interfaceType.Name))
                        {
                            builder.RegisterType(item).As(interfaceType).InstancePerLifetimeScope();
                        }
                    }
                    else
                    {
                        builder.RegisterType(item).As(interfaceType).InstancePerLifetimeScope();
                    }
                }
            }
            else
            {
                builder.RegisterType(item).InstancePerLifetimeScope();
            }

        }
    }


    ///// <summary>
    ///// 自动添加配置数据
    ///// </summary>
    ///// <param name="builder"></param>
    ///// <param name="assembly"></param>
    //public static void AddConfigure(ContainerBuilder builder, Assembly assembly)
    //{
    //    var configureDependency = assembly.GetTypes()
    //                       .Where(m => !m.IsInterface && typeof(IConfigureDependency).IsImplementType(m)).ToList();
    //    foreach (var type in configureDependency)
    //    {
    //        var configureAttr = (WjConfigureAttribute)Attribute.GetCustomAttribute(type, typeof(WjConfigureAttribute));
    //        if (configureAttr != null)
    //        {
    //            MethodInfo methodInfo = typeof(ConfigureDependencyHelper).GetMethod("Configure");
    //            //传入泛型
    //            methodInfo = methodInfo.MakeGenericMethod(type);
    //            methodInfo.Invoke(new ConfigureDependencyHelper(), new object[] { builder, configureAttr.wjConfigureEnum, configureAttr.Key });
    //        }
    //    }
    //}

}
