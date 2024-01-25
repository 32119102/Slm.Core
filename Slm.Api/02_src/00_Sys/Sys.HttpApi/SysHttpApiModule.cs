using Slm.Modularity.Abstractions;
using Slm.Modularity.Core;
using Slm.Utils.Core.Helpers;
using Sys.Application;
using Sys.Domain.Sqlsugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.HttpApi;

/// <summary>
/// 集成Controller
/// </summary>
[DependsOn(
    typeof(SysApplicationModule),
    typeof(SysDomainSqlsugarModule)
)]
public class SysHttpApiModule : AppModule
{




}
