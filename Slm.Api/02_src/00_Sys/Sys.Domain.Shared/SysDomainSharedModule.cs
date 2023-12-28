using Slm.Modularity.Abstractions;
using Slm.Utils.Core.Helpers;
using Slm.Utils.Core.Models;
using Slm.Utils.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Shared;

/// <summary>
/// 枚举模块设置
/// </summary>
public class SysDomainSharedModule : AppModule
{
    public override void ConfigureServices()
    {
        //注册枚举
        var enumTypes = Assembly.GetAssembly(typeof(SysDomainSharedModule))!.GetTypes().Where(a => a.IsEnum);

        foreach (var enumType in enumTypes)
        {
            var options = Enum.GetValues(enumType)
                .Cast<Enum>().
                Where(m => !m.ToString().EqualsIgnoreCase("UnKnown"))
                .Select(x => new OptionResultModel
                {
                    Label = x.ToDescription(),
                    Value = x
                }).ToList();

            ModuleEnumDescriptor moduleEnumDescriptor = new ModuleEnumDescriptor();
            moduleEnumDescriptor.ModuleName = "Sys";
            moduleEnumDescriptor.Name = enumType.Name;
            moduleEnumDescriptor.Type = enumType;
            moduleEnumDescriptor.Options = options;
            ModuleEnumDescriptorCollection.Add(moduleEnumDescriptor);


        }

        ConsoleHelper.WriteColorLine("SysDomainSharedModule(ConfigureServices)==========", ConsoleColor.Green);
        ConsoleHelper.WriteColorLine("枚举注册", ConsoleColor.Green);
    }
}