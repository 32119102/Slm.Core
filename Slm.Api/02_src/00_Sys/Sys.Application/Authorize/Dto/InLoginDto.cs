using System.ComponentModel.DataAnnotations;

namespace Sys.Application.Authorize.Dto;

public class InLoginDto
{
    /// <summary>
    /// 租户code
    /// </summary>
    [Required(ErrorMessage = "租户编码不能为空")]
    public string TenantCode { get; set; }

    /// <summary>
    /// 账号
    /// </summary>
    /// <example>admin</example>
    [Required(ErrorMessage = "账号不能为空"), MinLength(2, ErrorMessage = "账号不能少于2个字符")]
    public string Account { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    /// <example>123456</example>
    [Required(ErrorMessage = "密码不能为空"), MinLength(3, ErrorMessage = "密码不能少于3个字符")]
    public string Password { get; set; }

    /// <summary>
    /// 验证码Id
    /// </summary>
    public long VerifyId { get; set; }

    /// <summary>
    /// 验证码
    /// </summary>
    public string VerifyCode { get; set; }

}
