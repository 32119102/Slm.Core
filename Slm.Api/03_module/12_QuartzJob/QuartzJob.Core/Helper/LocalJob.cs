using Microsoft.Extensions.Logging;
using Quartz;
using Slm.Utils.Core;
using System.Diagnostics;
using System.Text.Json;

namespace Slm.QuartzJob.Core.Helper;

public class LocalJob : IJob
{
    private readonly Stopwatch _stopwatch;
    private readonly ILogger<LocalJob> _logger;
    public LocalJob(ILogger<LocalJob> logger)
    {
        _stopwatch = new Stopwatch();
        this._logger = logger;

    }

    /// <summary>
    /// 执行本地任务
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            _stopwatch.Restart();
            var jobTaskString = context.MergedJobDataMap.GetString(QuartzStartupConfig.JobTaskKey)?.ToString();
            if (string.IsNullOrWhiteSpace(jobTaskString))
            {
                _logger.LogError($"jobTaskString is NULL !");
                return;
            }
            var quartzJobTask = JsonSerializer.Deserialize<QuartzJobTask>(jobTaskString);

            if (quartzJobTask == null)
            {
                _logger.LogError("quartzJobTask is NULL !");
                return;
            }
            var jobTaskInfo = JobLocalCollection._jobTaskInfos.FirstOrDefault(w => w.Key == quartzJobTask.JobPoint);

            if (jobTaskInfo == null)
            {
                _logger.LogError($"jobTaskInfo is NULL !");
                return;
            }
            // 获取类型的代理类
            var jobTaskObjectProxy = App.GetService(jobTaskInfo.ClassType);
            var _type = jobTaskObjectProxy.GetType();
            var constructors = _type.GetConstructors()
             ?.FirstOrDefault()
             ?.GetParameters()
             ?.Select(w => w.ParameterType)
             ?.ToArray()
             ;
            var parametersObjects = new List<object>();

            if (constructors != null && constructors.Length > 0)
            {
                foreach (var item in constructors)
                {
                    parametersObjects.Add(item: App.GetService(item));
                }
            }


            var jobTaskObject = Activator.CreateInstance(_type, parametersObjects.ToArray());

            if (jobTaskObject == null)
            {
                _logger.LogError($"jobTaskObject is not a dependency injection service ! [{_type}]");
                return;
            }
            var time = DateTime.Now;
            var taskId = quartzJobTask?.Id ?? 0;

            var text = $"任务={quartzJobTask.Name}|组={quartzJobTask.GroupName}|{time:yyyy-MM-dd}|Time={time: HH:mm:ss:fff} - ";

            var result = "执行开始";
            try
            {
                var method = jobTaskObject.GetType().GetMethod(jobTaskInfo.MethodInfo.Name);
                var methodResult = method?.Invoke(jobTaskObject, Array.Empty<object>());

                // 异步函数处理
                if (methodResult is Task)
                {
                    if (methodResult.GetType().IsGenericType)
                    {
                        var _value = methodResult.GetType().GetProperty(nameof(Task<object>.Result)).GetValue(methodResult);

                        result = _value?.ToString();
                    }
                    else
                    {
                        ((Task)methodResult).Wait(CancellationToken.None);
                    }
                }
                else
                {
                    result = methodResult?.ToString();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Managers.Quartz.Jobs.LocalJob.Execute Execution failed : {ex.Message}");
                result = ex.Message;
            }


            result = result ?? "执行完成";
            _stopwatch.Stop();

            var endTime = $"{DateTime.Now:HH:mm:ss:fff}";

            //运行结束记录
            text += $"{endTime}|结果={result}|耗时={_stopwatch.ElapsedMilliseconds} 毫秒";
            Console.WriteLine(text);





        }
        catch (Exception ex)
        {
            var message = $@">>>>>>>>>>>>>>> 定时任务 {nameof(LocalJob)} Message={ex.Message}\r\n
                                    StackTrace={ex.StackTrace}\r\n
                                    Source={ex.Source}\r\n";

            _logger.LogError(ex, message, null);
        }
    }
}