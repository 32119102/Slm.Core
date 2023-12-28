using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slm.Modularity.Abstractions;

/// <summary>
/// 模块加载
/// </summary>
public interface IModuleLoader
{
    IAppModuleDescriptor[] LoadModules(Type startupModuleType);
}
