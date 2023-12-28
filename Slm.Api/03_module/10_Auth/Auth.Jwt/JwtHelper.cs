using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Slm.Utils.Core.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Slm.Utils.Core.Extensions;
using Slm.Utils.Core.Const;
using Microsoft.Extensions.Options;
using Slm.Utils.Core;

namespace Slm.Auth.Jwt;

public class JwtHelper : IScopedDependency
{


    /// <summary>
    /// 生成Token
    /// </summary>
    /// <param name="claims"></param>
    /// <returns></returns>
    public JwtResultModel Build(List<Claim> claims)
    {
        var _options = App.GetOptionsMonitor<JwtOptions>();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //刷新token过期时间
        var timestamp = DateTime.Now.AddDays(_options.RefreshTokenExpires).ToTimestamp();
        claims.Add(new Claim(SlmClaimTypes.RefreshExpires, timestamp.ToString()));

        var expires = _options.Expires < 0 ? 120 : _options.Expires;
        var expiresStamp = DateTime.Now.AddMinutes(expires).ToTimestamp();

        var jwtSecurityToken = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            DateTime.Now,
            DateTime.Now.AddMinutes(expires),
            signingCredentials);
        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);


        var result = new JwtResultModel
        {
            AccessToken = token,
            ExpiresIn = expiresStamp,
            RefreshToken = token,
            RefreshExpiresIn = timestamp
        };

        return result;
    }

    /// <summary>
    /// 解析token
    /// </summary>
    /// <param name="jwtToken"></param>
    /// <returns></returns>
    public JwtSecurityToken Decode(string jwtToken)
    {
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = jwtSecurityTokenHandler.ReadJwtToken(jwtToken);
        return jwtSecurityToken;
    }




}
