using Microsoft.Extensions.DependencyInjection;
using Slm.Modularity.Abstractions;
using Slm.Modularity.Abstractions.ConfigureServices;
using Slm.Modularity.Core.Helper;
using Slm.Utils.Core;

namespace Slm.Modularity.Core;

public class ModuleLoader : IModuleLoader
{
    public IAppModuleDescriptor[] LoadModules(Type startupModuleType)
    {
        Check.NotNull(startupModuleType, nameof(startupModuleType));
        var modules = GetDescriptors(startupModuleType);

        modules = SortByDependency(modules, startupModuleType);

        return modules.ToArray();
    }

    private List<IAppModuleDescriptor> GetDescriptors(Type startupModuleType)
    {
        var modules = new List<AppModuleDescriptor>();

        //查找模块
        FillModules(modules, startupModuleType);
        //设置依赖
        SetDependencies(modules);

        return modules.Cast<IAppModuleDescriptor>().ToList();
    }


    protected virtual void SetDependencies(List<AppModuleDescriptor> modules)
    {
        foreach (var module in modules)
        {
            SetDependencies(modules, module);
        }
    }

    protected virtual void SetDependencies(List<AppModuleDescriptor> modules, AppModuleDescriptor module)
    {
        foreach (var dependedModuleType in AppModuleHelper.FindDependedModuleTypes(module.Type))
        {
            var dependedModule = modules.FirstOrDefault(m => m.Type == dependedModuleType);
            if (dependedModule == null)
            {
                throw new Exception("Could not find a depended module " + dependedModuleType.AssemblyQualifiedName + " for " + module.Type.AssemblyQualifiedName);
            }

            module.AddDependency(dependedModule);
        }
    }

    protected virtual void FillModules(List<AppModuleDescriptor> modules,Type startupModuleType)
    {
        //All modules starting from the startup module
        foreach (var moduleType in AppModuleHelper.FindAllModuleTypes(startupModuleType))
        {
            modules.Add(CreateModuleDescriptor(moduleType));
        }
    }

    protected virtual AppModuleDescriptor CreateModuleDescriptor(Type moduleType)
    {
        return new AppModuleDescriptor(moduleType, CreateAndRegisterModule(moduleType));
    }


    protected virtual IConfigureServices CreateAndRegisterModule(Type moduleType)
    {
        var module = (IConfigureServices)Activator.CreateInstance(moduleType);
        InternalApp.Services!.AddSingleton(moduleType, module);
        return module;
    }

    protected virtual List<IAppModuleDescriptor> SortByDependency(List<IAppModuleDescriptor> modules, Type startupModuleType)
    {
        var sortedModules = modules.SortByDependencies(m => m.Dependencies);
        sortedModules.MoveItem(m => m.Type == startupModuleType, modules.Count - 1);
        return sortedModules;
    }
}
