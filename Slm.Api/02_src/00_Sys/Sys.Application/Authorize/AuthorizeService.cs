using Microsoft.AspNetCore.Authorization;
using Slm.DynamicApi.Attributes;
using Slm.DynamicApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Domain.Shared;
using Slm.Utils.Core.DependencyInjection;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using Slm.Utils.Core.Extensions;
using Slm.Utils.Core.Const;
using Slm.Auth.Abstractions;
#if NET8_0
using Swashbuckle.AspNetCore.SwaggerGen;
using Slm.Swashbuckle;
using Swashbuckle.AspNetCore.Swagger;
using Slm.Swashbuckle.Options;
#endif

using Slm.Data.Core.Extensions;
using Slm.Utils.Core.Helpers;

using Slm.Utils.Core;
using Sys.Application.Authorize.Dto;
using Microsoft.AspNetCore.Mvc;
using Lazy.Captcha.Core;
using Yitter.IdGenerator;


namespace Sys.Application.Authorize;



/// <summary>
/// 认证服务
/// </summary>
[DynamicApi(Area = SsyAreaConst.Area)]
[AllowAnonymous]
[Order(0)]
public class AuthorizeService : IDynamicApi
{

    /// <summary>
    /// 赖解析
    /// </summary>
    public IAbpLazyServiceProvider AbpLazyServiceProvider { get; set; } = default!;

    /// <summary>
    /// 验证码
    /// </summary>
    public ICaptcha _captcha => AbpLazyServiceProvider.LazyGetRequiredService<ICaptcha>();


    /// <summary>
    /// 登录
    /// </summary>
    /// <returns></returns>
    public async Task<string> Login(InLoginDto dto)
    {
      

        return "OK";
    }


    /// <summary>
    /// 获取验证码
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<OutCaptchaDto> GetCaptcha() 
    {
        var codeId = YitIdHelper.NextId().ToString();
        var captcha = _captcha.Generate(codeId);
        return new OutCaptchaDto { Id = codeId, Img = "data:image/png;base64,"+captcha.Base64 };
    }




}
