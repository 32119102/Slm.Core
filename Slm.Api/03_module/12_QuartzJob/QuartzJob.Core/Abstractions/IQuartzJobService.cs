
using Quartz;
using Slm.QuartzJob.Core.Helper;

namespace Slm.QuartzJob.Core.Abstractions;

public interface IQuartzJobService
{

    /// <summary>
    /// 添加一个任务
    /// </summary>
    /// <returns></returns>
    Task<bool> AddJobAsync(QuartzJobTask tasks);

    /// <summary>
    /// 添加一个任务
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="tasks"></param>
    /// <returns></returns>
    Task<bool> AddJobAsync<T>(QuartzJobTask tasks) where T : IJob;








}
