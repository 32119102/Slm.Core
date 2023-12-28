using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Slm.Utils.Core.Annotations;
using Slm.Utils.Core;
using Slm.Auth.Abstractions.Options;
using Slm.Auth.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Slm.Auth.Web.Attributes;


/// <summary>
/// 启用权限验证
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class ValidatePermissionAttribute : AuthorizeAttribute, IAuthorizationFilter, IAsyncAuthorizationFilter
{


    private IOptionsMonitor<AuthOptions> _options = App.GetService<IOptionsMonitor<AuthOptions>>()!;


    private async Task PermissionAuthorization(AuthorizationFilterContext context)
    {

        if (!_options.CurrentValue.EnablePermissionVerify)
        {
            return;
        }


        //排除匿名访问
        if (context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(AllowAnonymousAttribute)))
            return;

        if (!App.User!.Identity!.IsAuthenticated)
        {
            context.Result = new ChallengeResult();
            return;
        }

        //排除登录接口
        if (context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(AllowWhenAuthenticatedAttribute)))
            return;




        //权限验证
        var httpMethod = context.HttpContext.Request.Method;
        var api = context.ActionDescriptor.AttributeRouteInfo.Template;


        var permissionHandler = context.HttpContext.RequestServices.GetService<IPermissionValidateHandler>();
        var isValid = await permissionHandler.Validate(api, httpMethod);
        if (!isValid)
        {
            context.Result = new ForbidResult();
        }



    }

    public async void OnAuthorization(AuthorizationFilterContext context)
    {
        await PermissionAuthorization(context);
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        await PermissionAuthorization(context);
    }
}