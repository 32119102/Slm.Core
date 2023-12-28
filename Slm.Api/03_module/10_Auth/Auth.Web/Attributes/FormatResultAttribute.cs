using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Slm.DynamicApi.Attributes;
using Slm.Utils.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Slm.Auth.Web.Attributes;

/// <summary>
/// 结果格式化过滤器
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class FormatResultAttribute : AuthorizeAttribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var actionExecutedContext = await next();

        if (actionExecutedContext.Exception != null)
        {
            return;
        }

        if (context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(NonFormatResultAttribute)))
        {
            return;
        }

        IActionResult result = actionExecutedContext.Result;

        var formatResult = result switch
        {
            ViewResult => false,
            PartialViewResult => false,
            ViewComponentResult => false,
            PageResult => false,
            FileResult => false,
            SignInResult => false,
            SignOutResult => false,
            RedirectToPageResult => false,
            RedirectToRouteResult => false,
            RedirectResult => false,
            RedirectToActionResult => false,
            LocalRedirectResult => false,
            ChallengeResult => false,
            ForbidResult => false,
            BadRequestObjectResult => false,
            _ => true,
        };

        if (!formatResult)
        {
            return;
        }

        var data = result switch
        {
            ContentResult contentResult => contentResult.Content,
            ObjectResult objectResult => objectResult.Value,
            JsonResult jsonResult => jsonResult.Value,
            _ => null,
        };

        actionExecutedContext.Result = new JsonResult(new ResultModel<dynamic>().Success(data));
    }
}
