
namespace Slm.Auth.Abstractions;


public class DefaultPermissionValidateHandler : IPermissionValidateHandler
{
    /// <summary>
    /// 权限验证
    /// </summary>
    /// <param name="api"></param>
    /// <param name="httpMethod"></param>
    /// <returns></returns>
    public Task<bool> Validate(string api, string httpMethod)
    {
        return Task.FromResult(true);
    }
}
