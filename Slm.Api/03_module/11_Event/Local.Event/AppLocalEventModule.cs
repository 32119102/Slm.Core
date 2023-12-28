using MediatR;
using Slm.Modularity.Abstractions;
using Slm.Utils.Core;
using Slm.Utils.Core.Helpers;

namespace Slm.Local.Event;

/// <summary>
/// 本地事件
/// </summary>
public class AppLocalEventModule : AppModule
{
    public override void ConfigureServices()
    {
        var assemblies = new AssemblyHelper().Load(m => m.Name.EndsWith(".Application")||m.Name.EndsWith(".Domain"));
        InternalApp.Services!.AddMediatR(assemblies.ToArray()!);

        ConsoleHelper.WriteColorLine("AppLocalEventModule(ConfigureServices)==========", ConsoleColor.Green);
        ConsoleHelper.WriteColorLine("本地事件注入", ConsoleColor.Green);
    }
}
