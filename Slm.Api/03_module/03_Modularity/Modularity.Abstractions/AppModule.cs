using System.Reflection;
using Slm.Modularity.Abstractions.ConfigureServices;
using Slm.Modularity.Abstractions.MiddlewareConfigure;

namespace Slm.Modularity.Abstractions;

public abstract class AppModule :
      IPreConfigureServices,
      IConfigureServices,
      IPostConfigureServices,
      IOnPreApplicationConfigure,
      IOnApplicationConfigure,
      IOnPostApplicationConfigure
{
    public virtual void PreConfigureServices()
    {

    }

    public virtual void ConfigureServices()
    {

    }

    public virtual void PostConfigureServices()
    {

    }

    public virtual void OnPreApplicationConfigure()
    {

    }

    public virtual void OnApplicationConfigure()
    {

    }

    public virtual void OnPostApplicationConfigure()
    {

    }


    public static bool IsAppModule(Type type)
    {
        var typeInfo = type.GetTypeInfo();

        return
            typeInfo.IsClass &&
            !typeInfo.IsAbstract &&
            !typeInfo.IsGenericType &&
            typeof(IConfigureServices).GetTypeInfo().IsAssignableFrom(type);
    }


    public static void CheckAbpModuleType(Type moduleType)
    {
        if (!IsAppModule(moduleType))
        {
            throw new ArgumentException("Given type is not an  module: " + moduleType.AssemblyQualifiedName);
        }
    }

}
