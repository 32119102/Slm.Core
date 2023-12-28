namespace Slm.QuartzJob.Core.Abstractions;

public interface IJobHandler
{
    /// <summary>
    ///恢复任务
    /// </summary>
    /// <returns></returns>
    Task<bool> RecoveryTaskAsync();
}
