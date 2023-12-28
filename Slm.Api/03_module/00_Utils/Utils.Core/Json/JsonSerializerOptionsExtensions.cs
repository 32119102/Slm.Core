﻿using Slm.Utils.Core.Helpers;
using Slm.Utils.Core.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;



namespace Slm.Utils.Core.Json;

public static class JsonSerializerOptionsExtensions
{
    /// <summary>
    /// 添加多态嵌套序列化解决方案
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public static JsonSerializerOptions? AddPolymorphism(this JsonSerializerOptions options)
    {
        if (options != null)
        {
            var assemblies = new AssemblyHelper().Load();
            foreach (var assembly in assemblies)
            {
                try
                {
                    var typeSignature = "PolymorphismConverterLib";

                    var types = assembly?.GetTypes().Where(m => m.GetCustomAttributes().Any(n => n.GetType() == typeof(JsonPolymorphismAttribute)));
                    foreach (var t in types!)
                    {
                        var assemblyName = new AssemblyName(typeSignature);
                        var dynamicAssembly = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
                        var dynamicModule = dynamicAssembly.DefineDynamicModule(typeSignature);
                        var dynamicType = dynamicModule.DefineType(t.Name + "DynamicJsonConverter",
                            TypeAttributes.Public |
                            TypeAttributes.Class,
                            null);

                        dynamicType.SetParent(typeof(PolymorphismConverter<>).MakeGenericType(t));

                        var generatedType = dynamicType.CreateType();

                        options.Converters.Add((JsonConverter)Activator.CreateInstance(generatedType));
                    }
                }
                catch
                {
                    //此处防止第三方库抛出一场导致系统无法启动，所以需要捕获异常来处理一下
                }
            }
        }

        return options;
    }
}
