using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Slm.Validation.Abstractions;

/// <summary>
/// 模型验证结果格式化过滤器
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class ValidateResultFormatAttribute : AuthorizeAttribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            try
            {
                var formatHandler = context.HttpContext.RequestServices.GetRequiredService<IValidateResultFormatHandler>();
                formatHandler.Format(context);
            }
            catch
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
           
            }
            return;
        }
        await next();
    }




    //public override void OnResultExecuting(ResultExecutingContext context)
    //{
    //    if (!context.ModelState.IsValid)
    //    {
    //        try
    //        {
    //            var formatHandler = context.HttpContext.RequestServices.GetRequiredService<IValidateResultFormatHandler>();
    //            formatHandler.Format(context);
    //        }
    //        catch
    //        {
    //            context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
    //            return;
    //        }
           
    //    }
    //}
}
