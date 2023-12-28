using Autofac.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Slm.Modularity.Abstractions;
using Slm.Modularity.Abstractions.ConfigureServices;
using Slm.Modularity.Abstractions.MiddlewareConfigure;
using Slm.Utils.Core;

namespace Slm.Modularity.Core;

public abstract class AppApplicationBase : IAppApplication
{
    public Type StartupModuleType { get; }

    public IReadOnlyList<IAppModuleDescriptor>? Modules { get; }


    public AppApplicationBase(Type startupModuleType) 
    {
        Check.NotNull(startupModuleType, nameof(startupModuleType));
        StartupModuleType = startupModuleType;

        //注入----到时候启动需要
        InternalApp.Services!.AddSingleton<IAppApplication>(this);
        InternalApp.Services!.AddSingleton<IModuleContainer>(this);
        var moduleLoader = new ModuleLoader();
        InternalApp.Services!.TryAddSingleton<IModuleLoader>(moduleLoader);


        Modules = LoadModules();

    }

    public void ConfigureServices()
    {
        //PreConfigureServices
        foreach (var module in Modules.Where(m => m.Instance is IPreConfigureServices))
        {
            try
            {
                ((IPreConfigureServices)module.Instance).PreConfigureServices();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred during {nameof(IPreConfigureServices.PreConfigureServices)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
            }
        }

        //ConfigureServices
        foreach (var module in Modules)
        {
            try
            {
                module.Instance.ConfigureServices();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred during {nameof(IConfigureServices.ConfigureServices)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
            }
        }

        //PostConfigureServices
        foreach (var module in Modules.Where(m => m.Instance is IPostConfigureServices))
        {
            try
            {
                ((IPostConfigureServices)module.Instance).PostConfigureServices();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred during {nameof(IPostConfigureServices.PostConfigureServices)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
            }
        }
    }

    public void Initialize()
    {
        //IOnPreApplicationConfigure
        foreach (var module in Modules.Where(m => m.Instance is IOnPreApplicationConfigure))
        {
            try
            {
                ((IOnPreApplicationConfigure)module.Instance).OnPreApplicationConfigure();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred during {nameof(IPreConfigureServices.PreConfigureServices)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
            }
        }
        //PostConfigureServices
        foreach (var module in Modules.Where(m => m.Instance is IOnApplicationConfigure))
        {
            try
            {
                ((IOnApplicationConfigure)module.Instance).OnApplicationConfigure();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred during {nameof(IPostConfigureServices.PostConfigureServices)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
            }
        }


        foreach (var module in Modules.Where(m => m.Instance is IOnPostApplicationConfigure))
        {
            try
            {
                ((IOnPostApplicationConfigure)module.Instance).OnPostApplicationConfigure();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred during {nameof(IPostConfigureServices.PostConfigureServices)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
            }
        }



    }

    public void Shutdown()
    {
        throw new NotImplementedException();
    }

    protected virtual IReadOnlyList<IAppModuleDescriptor> LoadModules()
    {
        return InternalApp.Services!
            .GetSingletonInstance<IModuleLoader>()
            .LoadModules(StartupModuleType);
    }
}
