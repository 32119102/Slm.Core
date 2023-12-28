using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Slm.Utils.Core;
using Slm.Utils.Core.Const;
using Slm.Utils.Core.Extensions;
using Slm.Utils.Core.Models;
using System.Net;


namespace Slm.Host.Middleware;

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
        context.Response.StatusCode = appException==null?(int)HttpStatusCode.InternalServerError: (int)HttpStatusCode.OK;

        //开发环境返回详细异常信息
        var error = _env.IsDevelopment() ? exception.ToString() : exception.Message;
        //var path = context.Request.Path.Value;

        //var queryDic = context.Request.Query.Select(a => new { a.Key, a.Value });
        //var body = context.Request.Body;
        ////var bodyStr = "";
        ////using (StreamReader reader
        ////            = new StreamReader(body, Encoding.UTF8, true, 1024, true))
        ////{
        ////    bodyStr = await reader.ReadToEndAsync();
        ////}
        //var httpJson = new
        //{
        //    path = path,
        //    query = queryDic,
        //    body = ""
        //};
        //string requstInfo = _jsonHelper.Serialize(httpJson);
        ////无法写入json数据？
        //_logger._ErrorLogInformation(requstInfo);


        _logger.LogError("{str}".ToLog(), SerilogConst.Error, error);

        await context.Response.WriteAsync(_jsonHelper.Serialize(ResultModel.Failed(error)));
    }
}
