using Quartz.Spi;
using Quartz;
using Slm.Utils.Core;

namespace Slm.QuartzJob.Core.Helper;

/// <summary>
/// IJob 对象无法构造注入 需要此类实现 返回 注入后得 Job 实例
/// </summary>
public class JobFactory : IJobFactory
{
    public JobFactory()
    {

    }
    
    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {

        Type jobType = bundle.JobDetail.JobType;
        return (App.GetService(jobType) as IJob)!;
    }

    public void ReturnJob(IJob job)
    {
        (job as IDisposable)?.Dispose();
    }
}