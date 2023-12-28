using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Slm.Utils.Core;
using Slm.Utils.Core.Models;
using Slm.Utils.Core.Extensions;
using Slm.Utils.Core.Const;

namespace Slm.Auth.Web.Middleware;

public class ExceptionHandleMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _env;
    private readonly ILogger<ExceptionHandleMiddleware> _logger;
    private readonly JsonHelper _jsonHelper;

    public ExceptionHandleMiddleware(RequestDelegate next, IHostEnvironment env, ILogger<ExceptionHandleMiddleware> logger, JsonHelper jsonHelper)
    {
        _next = next;
        _env = env;
        _logger = logger;
        _jsonHelper = jsonHelper;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var appException = exception as AppException;
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = appException == null ? (int)HttpStatusCode.InternalServerError : (int)HttpStatusCode.OK;

        //开发环境返回详细异常信息
        var error = _env.IsDevelopment() ? exception.ToString() : exception.Message;

        _logger.LogError(error.ToLog(), SerilogConst.Error);

        await context.Response.WriteAsync(_jsonHelper.Serialize(ResultModel.Failed(error)));
    }
}
