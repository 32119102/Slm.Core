using Slm.QuartzJob.Core.Annotations;
using System.Reflection;

namespace Slm.QuartzJob.Core.Helper;

/// <summary>
/// 作业任务信息
/// </summary>
public class JobTaskInfo
{
    /// <summary>
    /// 唯一表示 key    
    /// </summary>
    public string? Key { get; set; }

    /// <summary>
    /// 函数信息
    /// </summary>
    public MethodInfo? MethodInfo { get; set; }

    /// <summary>
    /// 作业任务特性信息
    /// </summary>
    public ScheduledAttribute? ScheduledAttribute { get; set; }

    /// <summary>
    /// 对象类型
    /// </summary>
    public Type? ClassType { get; set; }
}