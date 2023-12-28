using Slm.Modularity.Abstractions;
using Slm.Modularity.Core;
using Sys.Domain;

namespace Sys.Application;     

/// <summary>
/// 服务实现
/// </summary>
[DependsOn(
   typeof(SysDomainModule)
)]
public class SysApplicationModule : AppModule
{

}
