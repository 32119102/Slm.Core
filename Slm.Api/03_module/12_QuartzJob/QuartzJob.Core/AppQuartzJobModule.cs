using Slm.Modularity.Abstractions;
using Slm.QuartzJob.Core.Abstractions;
using Slm.QuartzJob.Core.Annotations;
using Slm.QuartzJob.Core.Helper;
using Slm.Utils.Core;
using Slm.Utils.Core.Helpers;
using System.Reflection;


namespace Slm.QuartzJob.Core;

public class AppQuartzJobModule : AppModule
{
    public override void ConfigureServices()
    {
        //扫描程序集,得到本地事件类
        var assemblies = new AssemblyHelper().Load();
        foreach (var assembly in assemblies)
        {
            try
            {

                var _type = assembly!.GetTypes();
                foreach (var type in _type)
                {
                    MethodInfo[] methods = type.GetMethods();
                    List<MethodInfo> list = methods.Where((MethodInfo w) => w.GetCustomAttribute<ScheduledAttribute>() != null).ToList();
                    if (list == null || list.Count == 0)
                    {
                        continue;
                    }

                    foreach (MethodInfo item2 in list)
                    {
                        string key = item2.ReflectedType!.FullName + ">" + item2.Name;
                        JobTaskInfo jobTaskInfo = new JobTaskInfo();
                        jobTaskInfo.Key = key;
                        jobTaskInfo.MethodInfo = item2;
                        jobTaskInfo.ScheduledAttribute = item2.GetCustomAttribute<ScheduledAttribute>();
                        jobTaskInfo.ClassType = type;
                        JobLocalCollection.Add(jobTaskInfo);
                    }
                }
            }
            catch
            {
                //此处防止第三方库抛出一场导致系统无法启动，所以需要捕获异常来处理一下
            }
        }
        ConsoleHelper.WriteColorLine("AppQuartzJobModule(ConfigureServices)==========", ConsoleColor.Green);
        ConsoleHelper.WriteColorLine("Quartz定时任务注入", ConsoleColor.Green);
    }


    public override async void OnPostApplicationConfigure()
    {
        //恢复任务
        var jobHandler = App.GetService<IJobHandler>();
        if (jobHandler != null) {
           await  jobHandler.RecoveryTaskAsync();
        }
        ConsoleHelper.WriteColorLine("AppQuartzJobModule(OnApplicationConfigure)==========", ConsoleColor.Green);
        ConsoleHelper.WriteColorLine("Quartz定时任务恢复", ConsoleColor.Green);
    }
}