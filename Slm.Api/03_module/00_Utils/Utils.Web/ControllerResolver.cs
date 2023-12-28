using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Slm.Utils.Core.DependencyInjection;
using Slm.Utils.Core.Extensions;
using Slm.Utils.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Slm.DynamicApi.Attributes;

namespace Slm.Utils.Web.Mvc;

/// <summary>
/// 控制器解析器
/// </summary>
public class ControllerResolver 
    //: ISingletonDependency
{
    private readonly ApplicationPartManager _partManager;

    private readonly AttributeHelper _attributeHelper;

    /// <summary>
    /// 控制器集合
    /// </summary>
    public List<ControllerDescriptor> Controllers { get; }

    public ControllerResolver(ApplicationPartManager partManager, AttributeHelper attributeHelper)
    {
        _partManager = partManager;
        _attributeHelper = attributeHelper;
        Controllers = new List<ControllerDescriptor>();
        LoadControllers();
    }

    /// <summary>
    /// 获取指定区域和名称的控制器的操作列表
    /// </summary>
    /// <param name="area"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public List<ActionDescriptor> GetActions(string area, string name)
    {
        return Controllers.FirstOrDefault(m => m.Area.EqualsIgnoreCase(area) && m.Name.EqualsIgnoreCase(name))?.Actions!;
    }

    /// <summary>
    /// 加载控制器
    /// </summary>
    private void LoadControllers()
    {
        ControllerFeature controllerFeature = new ControllerFeature();
        _partManager.PopulateFeature(controllerFeature);
        foreach (var item2 in controllerFeature.Controllers.ToList())
        {
            if (item2.IsAbstract)
            {
                continue;
            }
            ControllerDescriptor controllerDescriptor = new ControllerDescriptor
            {
                Name = item2.Name.Replace("Controller", "").Replace("Service", ""),
                Description = _attributeHelper.GetDescription(item2)!,
                Actions = new List<ActionDescriptor>(),
                TypeInfo = item2
            };
            AreaAttribute areaAttribute = (AreaAttribute)Attribute.GetCustomAttribute(item2, typeof(AreaAttribute))!;
            if (areaAttribute != null)
            {
                controllerDescriptor.Area = areaAttribute.RouteValue;
            }
            DynamicApiAttribute areaAttribute2 = (DynamicApiAttribute)Attribute.GetCustomAttribute(item2, typeof(DynamicApiAttribute))!;
            if (areaAttribute2 != null)
            {
                controllerDescriptor.Area = areaAttribute2.Area;
            }

            MethodInfo[] methods = item2.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            foreach (MethodInfo methodInfo in methods)
            {
                if (methodInfo.CustomAttributes.Any(m => m.AttributeType == typeof(HttpGetAttribute) || m.AttributeType == typeof(HttpPostAttribute) || m.AttributeType == typeof(HttpPutAttribute) || m.AttributeType == typeof(HttpOptionsAttribute) || m.AttributeType == typeof(HttpHeadAttribute) || m.AttributeType == typeof(HttpPatchAttribute) || m.AttributeType == typeof(HttpDeleteAttribute)))
                {
                    ActionDescriptor item = new ActionDescriptor
                    {
                        Name = methodInfo.Name.Replace("Async", ""),
                        MethodInfo = methodInfo
                    };
                    controllerDescriptor.Actions.Add(item);
                }
                else
                {
                    ActionDescriptor item = new ActionDescriptor
                    {
                        Name = methodInfo.Name.Replace("Async", ""),
                        MethodInfo = methodInfo
                    };
                    controllerDescriptor.Actions.Add(item);
                }


            }
            Controllers.Add(controllerDescriptor);
        }
    }
}