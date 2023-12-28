using Microsoft.AspNetCore.Http;
using Quartz;
using Quartz.Spi;
using Slm.QuartzJob.Core.Abstractions;
using Slm.QuartzJob.Core.Helper;
using Slm.Utils.Core.Helpers;
using System.Text.Json;

namespace Slm.QuartzJob.Core;

public class QuartzJobService : IQuartzJobService
{
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly IJobFactory _jobFactory;

    public QuartzJobService(ISchedulerFactory schedulerFactory, IJobFactory jobFactory)
    {
        _schedulerFactory = schedulerFactory;
        _jobFactory = jobFactory;
    }


    public async Task<bool> AddJobAsync(QuartzJobTask tasks)
    {
        if (tasks.Type == QuartzJobTaskTypeEnum.WebApi)
        {
            return await AddJobAsync<WebApiJob>(tasks);
        }

        if (tasks.Type == QuartzJobTaskTypeEnum.Local)
        {
            return await AddJobAsync<LocalJob>(tasks);
        }

        return false;
    }


    public async Task<bool> AddJobAsync<T>(QuartzJobTask tasks) where T : IJob
    {
        IJobDetail job = JobBuilder.Create<T>()
             .WithIdentity(tasks.Name, tasks.GroupName)
             .UsingJobData(QuartzStartupConfig.JobTaskKey, JsonSerializer.Serialize(tasks))
            .Build();
        ITrigger trigger = TriggerBuilder.Create()
           .WithIdentity(tasks.Name, tasks.GroupName)
           .StartNow()
           .WithDescription(tasks.Remark)
           .WithCronSchedule(tasks.Cron)
           .Build();

        IScheduler scheduler = await _schedulerFactory.GetScheduler();

        scheduler.JobFactory = _jobFactory;
        await scheduler.ScheduleJob(job, trigger);
        if (tasks.State ==  QuartzJobTaskStateEnum.运行中)
        {
            await scheduler.Start();
        }
        else
        {
            await scheduler.PauseJob(job.Key);
            ConsoleHelper.WriteColorLine($"作业:{tasks.Name},分组:{tasks.GroupName},新建时未启动原因,状态为:{tasks.State.ToString()}", ConsoleColor.Blue);
        }
              return await Task.FromResult(true);
    }
}
