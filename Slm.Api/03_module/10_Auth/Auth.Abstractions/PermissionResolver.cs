
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Routing;
using Slm.Auth.Abstractions.Options;
using Slm.Utils.Core.Annotations;
using Slm.Utils.Core.Extensions;
using Slm.Utils.Web.Mvc;
using HttpMethod = Slm.Auth.Abstractions.Options.HttpMethod;

namespace Slm.Auth.Abstractions;



[Obsolete("使用IAsyncSwaggerProvider(swagger) 来替换次方法")]
public class PermissionResolver : IPermissionResolver
{
    private readonly ControllerResolver _controllerResolver;
    private readonly List<PermissionDescriptor> _descriptors = new();

    public PermissionResolver(ControllerResolver controllerResolver)
    {
        _controllerResolver = controllerResolver;

        Load();
    }

    /// <summary>
    /// 加载权限信息
    /// </summary>
    private void Load()
    {
        var controllers = _controllerResolver.Controllers;
        foreach (var controller in controllers)
        {
            if (!controller.Actions!.Any())
                continue;

            PermissionMode permissionMode = PermissionMode.Authorization;

            var allowAnonymousAttribute_Controller = controller.TypeInfo.CustomAttributes.FirstOrDefault(m => typeof(AllowAnonymousAttribute) == m.AttributeType);
            if (allowAnonymousAttribute_Controller != null)
            {
                permissionMode = PermissionMode.Anonymous;
            }
            else
            {
                var allowLoginAttribute = controller.TypeInfo.CustomAttributes.FirstOrDefault(m => typeof(AllowWhenAuthenticatedAttribute) == m.AttributeType);
                if (allowLoginAttribute != null)
                {
                    permissionMode = PermissionMode.Login;
                }
            }
            foreach (var action in controller.Actions!)
            {
                var permission = new PermissionDescriptor
                {
                    Action = action.Name,
                    Controller = controller.Name,
                    ModuleCode = controller.Area,
                    HttpMethod = HttpMethod.Get,
                    Mode = permissionMode
                };

                //请求方式
                var httpMethodAttr = action.MethodInfo.CustomAttributes.FirstOrDefault(m => typeof(HttpMethodAttribute).IsAssignableFrom(m.AttributeType));
                if (httpMethodAttr != null)
                {
                    var httpMethodName = httpMethodAttr.AttributeType.Name.Replace("Http", "").Replace("Attribute", "");
                    permission.HttpMethod = (HttpMethod)Enum.Parse(typeof(HttpMethod), httpMethodName);
                }
                else
                {
                    permission.HttpMethod = HttpMethod.Post;
                }

                #region ==权限模式==

                var allowAnonymousAttribute = action.MethodInfo.CustomAttributes.FirstOrDefault(m => typeof(AllowAnonymousAttribute) == m.AttributeType);
                if (allowAnonymousAttribute != null)
                {
                    permission.Mode = PermissionMode.Anonymous;
                }
                else
                {
                    var allowLoginAttribute = action.MethodInfo.CustomAttributes.FirstOrDefault(m => typeof(AllowWhenAuthenticatedAttribute) == m.AttributeType);
                    if (allowLoginAttribute != null)
                    {
                        permission.Mode = PermissionMode.Login;
                    }
                }

                #endregion

                _descriptors.Add(permission);
            }
        }
    }

    public List<PermissionDescriptor> GetPermissions(string? moduleCode)
    {
        if (moduleCode.IsNull())
        {
            return _descriptors.ToList();
        }
        return _descriptors.Where(m => m.ModuleCode.EqualsIgnoreCase(moduleCode!)).ToList();
    }
}