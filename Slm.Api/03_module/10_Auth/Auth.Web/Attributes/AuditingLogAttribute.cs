using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Slm.Utils.Core.Annotations;

namespace Slm.Auth.Web.Attributes;

/// <summary>
/// 审计日志
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class AuditingLogAttribute : AuthorizeAttribute, IAsyncActionFilter
{
    public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        return next();
        //如果禁用审计功能，直接走下一步
        if (CheckDisabled(context))
        {
            return next();
        }
        var handler = context.HttpContext.RequestServices.GetService<IAuditingHandler>();

        return handler.Hand(context, next);
    }



    /// <summary>
    /// 判断是否禁用审计功能
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    private bool CheckDisabled(ActionExecutingContext context)
    {
        return context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(NoLogAttribute));
    }


}
