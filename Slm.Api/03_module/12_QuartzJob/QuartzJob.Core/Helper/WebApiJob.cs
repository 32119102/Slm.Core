using Autofac.Core.Resolving.Middleware;
using Microsoft.Extensions.Logging;
using Quartz;
using Slm.Utils.Core.Helpers;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace Slm.QuartzJob.Core.Helper;

public class WebApiJob : IJob
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly Stopwatch _stopwatch;
    private readonly ILogger<WebApiJob> _logger;
    public WebApiJob(IHttpClientFactory httpClientFactory, ILogger<WebApiJob> logger)
    {
        this._httpClientFactory = httpClientFactory;
        _stopwatch = new Stopwatch();
        this._logger = logger;
    }
    public async Task Execute(IJobExecutionContext context)
    {
        string httpMessage = "";
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
            var time = DateTime.Now;
            var taskId = quartzJobTask?.Id ?? 0;

            var text = $"任务={quartzJobTask.Name}|组={quartzJobTask.GroupName}|{time:yyyy-MM-dd}|Time={time: HH:mm:ss:fff} - ";

            var result = "执行开始";
            Dictionary<string, string> header = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(quartzJobTask.AuthKey)
                && !string.IsNullOrEmpty(quartzJobTask.AuthValue))
            {
                header.Add(quartzJobTask.AuthKey.Trim(), quartzJobTask.AuthValue.Trim());
            }
            HttpMethod metthod = HttpMethod.Post;
            switch (quartzJobTask.RequsetMode)
            {
                case QuartzJobTaskRequsetModeEnum.Get:
                    metthod = HttpMethod.Get;
                    break;
                case QuartzJobTaskRequsetModeEnum.Delete:
                    metthod = HttpMethod.Delete;
                    break;
                default:

                    break;
            }

            httpMessage = await _httpClientFactory.HttpSendAsync(
                metthod,
                quartzJobTask.JobPoint,
                header);
            var endTime = $"{DateTime.Now:HH:mm:ss:fff}";

            _stopwatch.Stop();

            //运行结束记录
            text += $"{endTime}|结果={httpMessage}|耗时={_stopwatch.ElapsedMilliseconds} 毫秒";
            Console.WriteLine(text);
        }
        catch (Exception ex)
        {                                                  
            var message = $@">>>>>>>>>>>>>>> 定时任务 {nameof(WebApiJob)} Message={ex.Message}\r\n
                                    StackTrace={ex.StackTrace}\r\n
                                    Source={ex.Source}\r\n";

            _logger.LogError(ex, message, null);


        }

     
    }
}
