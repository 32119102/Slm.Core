using Autofac;
using Slm.Utils.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slm.Auth.Abstractions;

/// <summary>
/// 自动注入(1.特性自动注入;2.配置自动添加)
/// </summary>
public class AutoFacModuleRegister : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        ConsoleHelper.WriteColorLine("======Auth.Abstractions||模块解析,自定义授权,默认权限 开始执行======", ConsoleColor.DarkYellow);

        ////添加模块权限解析器,可以获取模块下面有哪些接口数据,会有个查询模块下面权限的接口
        //builder.RegisterType<PermissionResolver>().As<IPermissionResolver>().SingleInstance();

        //默认权限实现
        builder.RegisterType<DefaultPermissionValidateHandler>().As<IPermissionValidateHandler>().SingleInstance();
    }

}