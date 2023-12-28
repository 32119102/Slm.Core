using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Slm.Modularity.Abstractions;
using Slm.Modularity.Core;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions                    
{

    public static IServiceCollection AddApplication<T>(this IServiceCollection services) where T : AppModule
    {
        services.AddApplication(typeof(T));
        return services;
    }

    private static IServiceCollection AddApplication(this IServiceCollection services, Type type)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));

        IAppApplication runner = new StartupModuleRunner(type);
        runner.ConfigureServices();
        return services;
    }


}
