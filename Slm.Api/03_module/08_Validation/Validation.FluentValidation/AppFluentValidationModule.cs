using FluentValidation;
using FluentValidation.AspNetCore;
using Slm.Modularity.Abstractions;
using Slm.Utils.Core;
using Slm.Utils.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slm.Validation.FluentValidation;

public class AppFluentValidationModule : AppModule
{

    public override void PostConfigureServices()
    {
        AssemblyHelper assemblyHelper = new AssemblyHelper();
        var assemblyList = assemblyHelper.LoadByNameEndStringArry(".Application");
        InternalApp.Services.AddValidatorsFromAssemblies(assemblyList);
        InternalApp.Services.AddFluentValidationAutoValidation();

        //当一个验证失败时，后续的验证不再执行
        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
        ConsoleHelper.WriteColorLine("AppFluentValidationModule(PostConfigureServices)==========", ConsoleColor.Green);
        ConsoleHelper.WriteColorLine($"FluentValidation服务注入", ConsoleColor.Green);
    }


}
