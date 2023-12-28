using Microsoft.Extensions.Configuration;
using Slm.Modularity.Abstractions;
using Slm.Modularity.Core;

namespace Sys.Domain.Sqlsugar;

/// <summary>
/// 数据库操作
/// </summary>
[DependsOn(
typeof(SysDomainModule)
 )]
public class SysDomainSqlsugarModule : AppModule
{

}
