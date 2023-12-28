using System.Reflection;
using Slm.Modularity.Abstractions.ConfigureServices;

namespace Slm.Modularity.Abstractions;

public interface IAppModuleDescriptor
{
    /// <summary>
    /// 
    /// </summary>
    Type Type { get; }

    /// <summary>
    /// 
    /// </summary>
    Assembly Assembly { get; }

    /// <summary>
    /// 
    /// </summary>
    IConfigureServices Instance { get; }

    /// <summary>
    /// 
    /// </summary>
    IReadOnlyList<IAppModuleDescriptor> Dependencies { get; }
}
