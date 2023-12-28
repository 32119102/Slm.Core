
namespace Slm.Auth.Abstractions;


/// <summary>
/// 权限验证处理接口
/// </summary>
public interface IPermissionValidateHandler
{
    /// <summary>
    /// 权限验证
    /// </summary>
    /// <param name="api">接口路径</param>
    /// <param name="httpMethod">http请求方法</param>
    /// <returns></returns>
    Task<bool> Validate(string api, string httpMethod);
}

