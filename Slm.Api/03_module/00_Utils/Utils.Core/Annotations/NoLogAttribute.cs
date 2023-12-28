namespace Slm.Utils.Core.Annotations;

/// <summary>
/// 不记录操作日志的属性
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class NoLogAttribute : Attribute
{
}
