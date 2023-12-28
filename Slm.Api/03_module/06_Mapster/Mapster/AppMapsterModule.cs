using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Slm.Modularity.Abstractions;
using Slm.Utils.Core;
using Slm.Utils.Core.Helpers;
using Slm.Utils.Core.Pagination;
using Slm.Utils.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Slm.Mapster;

/// <summary>
/// 需要做处理只注册当前程序集的数据,todo
/// </summary>
public class AppMapsterModule : AppModule
{

    public override void ConfigureServices()
    {
        //程序集
        AssemblyHelper assemblyHelper = new AssemblyHelper();
        var assemblies = assemblyHelper.LoadByNameEndStringArry(".Application");
        InternalApp.Services!.AddSingleton<IMapper>(src => new Mapper());


        TypeAdapterConfig.GlobalSettings.ForType(typeof(QueryResultModel<>), typeof(QueryResultModel<>)).MapToConstructor(true);

        TypeAdapterConfig.GlobalSettings.Scan(assemblies.ToArray()!);


        #region Mapster 映射配置
       // var config = new TypeAdapterConfig();
       // IMapper mapper = new Mapper(config);

       // config.ForType(typeof(QueryResultModel<>),
       //typeof(QueryResultModel<>)).MapToConstructor(true);
       // TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);
       // //InternalApp.Services!.AddSingleton(mapper);
  
       // InternalApp.Services!.AddScoped<IMapper>(src=> mapper);



        #endregion Mapster 映射配置





        // var config = new TypeAdapterConfig();
        // AssemblyHelper assemblyHelper = new AssemblyHelper();
        // var assemblyList = assemblyHelper.LoadByNameEndStringArry(".Application");

        // foreach (var assembly in assemblyList)
        // {
        //     //走配置管理
        //     var mapConfigs = assembly!.GetTypes().Where(t => !t.IsInterface && typeof(IMapsterConfig).IsImplementType(t));
        //     if (mapConfigs != null)
        //     {
        //         foreach (var mapConfig in mapConfigs)
        //         {
        //             ((IMapsterConfig)Activator.CreateInstance(mapConfig)).Bind(config);
        //         }
        //     }
        // }
        // IMapper mapper = new Mapper(config);

        // config.ForType(typeof(QueryResultModel<>),
        //typeof(QueryResultModel<>)).MapToConstructor(true);
        // TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);
        // InternalApp.InternalServices!.AddSingleton(mapper);

        ConsoleHelper.WriteColorLine("AppMapsterModule(ConfigureServices)==========", ConsoleColor.Green);
        ConsoleHelper.WriteColorLine("添加实体和Dto之间的映射", ConsoleColor.Green);
    }
}
