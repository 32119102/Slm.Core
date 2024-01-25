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
    /// 登录
    /// </summary>
    /// <returns></returns>
    public async Task<string> Login()
    {
      

        return "OK";
    }



}
