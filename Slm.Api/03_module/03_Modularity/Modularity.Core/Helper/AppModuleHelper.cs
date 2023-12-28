using Slm.Modularity.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Slm.Utils.Core.Extensions;

namespace Slm.Modularity.Core.Helper;

/// <summary>
/// 模块帮助类
/// </summary>
public class AppModuleHelper
{
    public static List<Type> FindAllModuleTypes(Type startupModuleType)
    {
        var moduleTypes = new List<Type>();

        AddModuleAndDependenciesRecursively(moduleTypes, startupModuleType);
        return moduleTypes;
    }

    public static List<Type> FindDependedModuleTypes(Type moduleType)
    {
        AppModule.CheckAbpModuleType(moduleType);

        var dependencies = new List<Type>();

        var dependencyDescriptors = moduleType
            .GetCustomAttributes()
            .OfType<IDependedTypesProvider>();

        foreach (var descriptor in dependencyDescriptors)
        {
            foreach (var dependedModuleType in descriptor.GetDependedTypes())
            {
                dependencies.AddIfNotContains(dependedModuleType);
            }
        }

        return dependencies;
    }

    private static void AddModuleAndDependenciesRecursively(
        List<Type> moduleTypes,
        Type moduleType,
        int depth = 0)
    {
        AppModule.CheckAbpModuleType(moduleType);

        if (moduleTypes.Contains(moduleType))
        {
            return;
        }

        moduleTypes.Add(moduleType);

        foreach (var dependedModuleType in FindDependedModuleTypes(moduleType))
        {
            AddModuleAndDependenciesRecursively(moduleTypes, dependedModuleType, depth + 1);
        }
    }
}

