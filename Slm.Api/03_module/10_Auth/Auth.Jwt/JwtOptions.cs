using Slm.Utils.Core.ConfigurableOptions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slm.Auth.Jwt;

public class JwtOptions : IConfigurableOptions
{
    /// <summary>
    /// 加密密钥
    /// </summary>
    public string Key { get; set; } = "twAJ$j5##pVc5*y&";

    /// <summary>
    /// 发行人
    /// </summary>
    public string Issuer { get; set; } = "http://www.baidu.com";

    /// <summary>
    /// 消费者
    /// </summary>
    public string Audience { get; set; } = "http://www.baidu.com";

    /// <summary>
    /// 令牌有效期(分钟，默认120)
    /// </summary>
    public int Expires { get; set; } = 120;

    /// <summary>
    /// 刷新令牌有效期(单位：天，默认7)
    /// </summary>
    public int RefreshTokenExpires { get; set; } = 7;
}
