using Microsoft.Extensions.DependencyInjection;
using Slm.Modularity.Abstractions;
using Slm.Modularity.Core;
using Slm.Utils.Core;
using Slm.Utils.Core.Helpers;
using SqlSugar;
using Sys.Domain.Shared;

namespace Sys.Domain;

/// <summary>
/// 领域接口实体层
/// </summary>
[DependsOn(
    typeof(SysDomainSharedModule)
)]
public class SysDomainModule : AppModule
{

    public override void PostConfigureServices()
    {
        //注入工作单元
        InternalApp.Services!.AddScoped<ISugarUnitOfWork<SysDbContext>>(o =>
        {
            var sqlSugar = o.GetService<ISqlSugarClient>();
            var context = new SugarUnitOfWork<SysDbContext>(sqlSugar);
            return context;
        });

        ConsoleHelper.WriteColorLine("SysDomainModule(PostConfigureServices)==========", ConsoleColor.Green);
        ConsoleHelper.WriteColorLine("领域层设置工作单元信息", ConsoleColor.Green);

    }

}
