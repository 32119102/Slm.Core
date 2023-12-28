namespace Slm.Auth.Jwt;

public class JwtResultModel
{
    /// <summary>
    /// 访问令牌
    /// </summary>
    public string AccessToken { get; set; } = String.Empty;

    /// <summary>
    /// 有效期(时间戳)
    /// </summary>
    public long ExpiresIn { get; set; }

    /// <summary>
    /// 刷新token
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;

    /// <summary>
    /// 过期时间(时间戳)
    /// </summary>
    public long RefreshExpiresIn { get; set; } = 0;

}
