using System.Reflection;
using System.Collections.Immutable;
using Slm.Modularity.Abstractions.ConfigureServices;
using Slm.Utils.Core;
using Slm.Modularity.Abstractions;

namespace Slm.Modularity.Core;

public class AppModuleDescriptor : IAppModuleDescriptor
{
    public Type Type { get; }

    public Assembly Assembly { get; }

    public IConfigureServices Instance { get; }

    public IReadOnlyList<IAppModuleDescriptor> Dependencies => _dependencies.ToImmutableList();
    private readonly List<IAppModuleDescriptor> _dependencies;

    public AppModuleDescriptor(Type type, IConfigureServices instance)
    {
        Check.NotNull(type, nameof(type));
        Check.NotNull(instance, nameof(instance));

        if (!type.GetTypeInfo().IsAssignableFrom(instance.GetType()))
        {
            throw new ArgumentException($"Modul: Given module instance ({instance.GetType().AssemblyQualifiedName}) is not an instance of given module type: {type.AssemblyQualifiedName}");
        }

        Type = type;
        Assembly = type.Assembly;
        Instance = instance;

        _dependencies = new List<IAppModuleDescriptor>();
    }

    public void AddDependency(IAppModuleDescriptor descriptor)
    {
        _dependencies.AddIfNotContains(descriptor);
    }

    public override string ToString()
    {
        return $"[AbpModuleDescriptor {Type.FullName}]";
    }
}
