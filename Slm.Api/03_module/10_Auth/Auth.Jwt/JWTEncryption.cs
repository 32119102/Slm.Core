using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Slm.Utils.Core;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;



namespace Slm.Auth.Jwt;

/// <summary>
/// 
/// </summary>
public class JWTEncryption
{

    /// <summary>
    /// 刷新 Token 身份标识
    /// </summary>
    private static readonly string[] _refreshTokenClaims = new[] { "f", "e", "s", "l", "k" };

    /// <summary>
    /// 生成token
    /// </summary>
    /// <param name="payload"></param>
    /// <param name="expiredTime"></param>
    /// <returns></returns>
    public static string Encrypt(IDictionary<string, object> payload, long? expiredTime = null)
    {
        var _options = App.GetOptionsMonitor<JwtOptions>();
        var payloadFull = CombinePayload(payload, _options, expiredTime);

        return Encrypt(_options.IssuerSigningKey, payloadFull, _options.Algorithm);
    }


    /// <summary>
    /// 生成 Token
    /// </summary>
    /// <param name="issuerSigningKey"></param>
    /// <param name="payload"></param>
    /// <param name="algorithm"></param>
    /// <returns></returns>
    public static string Encrypt(string issuerSigningKey, IDictionary<string, object> payload, string algorithm = SecurityAlgorithms.HmacSha256)
    {
        // 处理 JwtPayload 序列化不一致问题
        var stringPayload = payload is JwtPayload jwtPayload ? jwtPayload.SerializeToJson() : JsonSerializer.Serialize(payload, new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });
        return Encrypt(issuerSigningKey, stringPayload, algorithm);
    }


    /// <summary>
    /// 生成 Token
    /// </summary>
    /// <param name="issuerSigningKey"></param>
    /// <param name="payload"></param>
    /// <param name="algorithm"></param>
    /// <returns></returns>
    public static string Encrypt(string issuerSigningKey, string payload, string algorithm = SecurityAlgorithms.HmacSha256)
    {
        SigningCredentials credentials = null;

        if (!string.IsNullOrWhiteSpace(issuerSigningKey))
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey));
            credentials = new SigningCredentials(securityKey, algorithm);
        }

        var tokenHandler = new JsonWebTokenHandler();
        return credentials == null ? tokenHandler.CreateToken(payload) : tokenHandler.CreateToken(payload, credentials);
    }

    /// <summary>
    /// 生成刷新 Token
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="expiredTime">刷新 Token 有效期（分钟） 默认30天</param>
    /// <returns></returns>
    public static string GenerateRefreshToken(string accessToken, int expiredTime = 43200)
    {
        // 分割Token
        var tokenParagraphs = accessToken.Split('.', StringSplitOptions.RemoveEmptyEntries);

        var s = RandomNumberGenerator.GetInt32(10, tokenParagraphs[1].Length / 2 + 2);
        var l = RandomNumberGenerator.GetInt32(3, 13);

        var payload = new Dictionary<string, object>
            {
                { "f",tokenParagraphs[0] },
                { "e",tokenParagraphs[2] },
                { "s",s },
                { "l",l },
                { "k",tokenParagraphs[1].Substring(s,l) }
            };

        return Encrypt(payload, expiredTime);
    }



    /// <summary>
    /// 组合 Claims 负荷
    /// </summary>
    /// <param name="payload"></param>
    /// <param name="jwtOptions"></param>
    /// <param name="expiredTime">过期时间,单位分钟</param>
    /// <returns></returns>
    private static IDictionary<string, object> CombinePayload(IDictionary<string, object> payload, JwtOptions jwtOptions, long? expiredTime = null)
    {
        var datetimeOffset = DateTimeOffset.UtcNow;

        if (!payload.ContainsKey(JwtRegisteredClaimNames.Iat))
        {
            payload.Add(JwtRegisteredClaimNames.Iat, datetimeOffset.ToUnixTimeSeconds());
        }

        if (!payload.ContainsKey(JwtRegisteredClaimNames.Nbf))
        {
            payload.Add(JwtRegisteredClaimNames.Nbf, datetimeOffset.ToUnixTimeSeconds());
        }

        if (!payload.ContainsKey(JwtRegisteredClaimNames.Exp))
        {
            var minute = expiredTime ?? jwtOptions?.ExpiredTime ?? 20;
            payload.Add(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMinutes(minute).ToUnixTimeSeconds());
        }

        if (!payload.ContainsKey(JwtRegisteredClaimNames.Iss))
        {
            payload.Add(JwtRegisteredClaimNames.Iss, jwtOptions?.ValidIssuer);
        }

        if (!payload.ContainsKey(JwtRegisteredClaimNames.Aud))
        {
            payload.Add(JwtRegisteredClaimNames.Aud, jwtOptions?.ValidAudience);
        }

        return payload;
    }

}
