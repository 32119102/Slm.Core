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
using Slm.Utils.Core.Models;
using Sys.Domain.User;
using Slm.Auth.Jwt;


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
    /// 用户
    /// </summary>
    public IUserRepository _userRepository => AbpLazyServiceProvider.LazyGetRequiredService<IUserRepository>();


    /// <summary>
    /// 登录
    /// </summary>
    /// <returns></returns>
    public async Task<OutLoginDto> Login(InLoginDto dto)
    {
      
        // 判断验证码
        if (!_captcha.Validate(dto.VerifyId.ToString(), dto.VerifyCode))
            throw ResultModel.Exception("验证码错误");

        var user = await _userRepository.Login(dto.TenantCode, dto.Account);
        if (user == null)
            throw ResultModel.Exception("租户和账号不存在");

        //判断是否禁用状态 todo

        //判断密码   加密判断
        user.Password = dto.Password;


        var accessToken = JWTEncryption.Encrypt(new Dictionary<string, object>
        {
            { SlmClaimConst.UserId, user.Id },
            { SlmClaimConst.TenantId, user.TenantId },
            { SlmClaimConst.Account, user.Account },
            { SlmClaimConst.RealName, user.RealName },
            { SlmClaimConst.AccountType, user.AccountType },
            { SlmClaimConst.OrgId, user.OrgId },
        }, 120);

        // 生成刷新Token令牌
        var refreshToken = JWTEncryption.GenerateRefreshToken(accessToken);







        return new OutLoginDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
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
        return new OutCaptchaDto { Id = codeId, Img = "data:image/png;base64," + captcha.Base64 };
    }




}
