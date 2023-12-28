
namespace Slm.QuartzJob.Core.Helper;

public class QuartzJobTask
{
    /// <summary>
    /// 任务名称
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 任务名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 分组名称
    /// </summary>
    public string GroupName { get; set; }

    /// <summary>
    /// 间隔表达式
    /// </summary>
    public string Cron { get; set; }

    /// <summary>
    /// 执行时间
    /// </summary>
    public DateTime? ExecuteTime { get; set; }

    /// <summary>
    /// 运行状态
    /// </summary>
    public QuartzJobTaskStateEnum State { get; set; } = QuartzJobTaskStateEnum.未运行;

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 任务类型
    /// </summary>
    public QuartzJobTaskTypeEnum Type { get; set; }

    /// <summary>
    /// 作业点
    /// </summary>
    public string JobPoint { get; set; }

    /// <summary>
    /// 请求方式
    /// </summary>
    public QuartzJobTaskRequsetModeEnum? RequsetMode { get; set; }



    public string AuthKey { get; set; }
    public string AuthValue { get; set; }



}


/// <summary>
/// 请求方式
/// </summary>
public enum QuartzJobTaskRequsetModeEnum
{
    Post,
    Get,
    Delete
}

/// <summary>
/// 状态情况
/// </summary>
public enum QuartzJobTaskStateEnum
{
    未运行,
    运行中
}

/// <summary>
/// 类型
/// </summary>
public enum QuartzJobTaskTypeEnum
{
    /// <summary>
    /// webapi 远程程序
    /// </summary>
    WebApi = 1,
    /// <summary>
    /// 本地程序
    /// </summary>
    Local
}



/// <summary>
/// Quartz 启动配置
/// </summary>
public static class QuartzStartupConfig
{
    /// <summary>
    /// 作业任务 Key 名称
    /// </summary>
    public static readonly string JobTaskKey = nameof(QuartzJobTask);

}