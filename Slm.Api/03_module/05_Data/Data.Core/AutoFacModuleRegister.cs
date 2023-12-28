using Autofac;
using Slm.Data.Abstractions;
using Slm.Utils.Core.Helpers;

namespace Slm.Data.Core;

public class AutoFacModuleRegister : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        ConsoleHelper.WriteColorLine("======Data.Core||注入事务======", ConsoleColor.DarkYellow);
        builder.RegisterType<Tran>().As<ITran>().InstancePerLifetimeScope();

    }
}