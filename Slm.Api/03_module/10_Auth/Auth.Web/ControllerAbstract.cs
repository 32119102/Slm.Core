

using Microsoft.AspNetCore.Mvc;
using Slm.Validation.Abstractions;

namespace Slm.Auth.Web;

/// <summary>
/// 控制器抽象
/// </summary>
[Route("api/[area]/[controller]/[action]")]
[ApiController]
[ValidateResultFormat] //结果验证
public abstract class ControllerAbstract : ControllerBase
{

}