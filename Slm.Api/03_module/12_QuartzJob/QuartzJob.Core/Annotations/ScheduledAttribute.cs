
namespace Slm.QuartzJob.Core.Annotations;


//
// 摘要:
//     定时任务作业 特性标记
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class ScheduledAttribute : Attribute
{
    //
    // 摘要:
    //     任务名称
    public string Name { get; set; }

    //
    // 摘要:
    //     分组名称
    public string GroupName { get; set; }

    //
    // 摘要:
    //     定时表达式
    public string Cron { get; set; }

    //
    // 摘要:
    //     备注
    public string Remark { get; set; }

    //
    // 摘要:
    //     定时任务作业
    //
    // 参数:
    //   cron:
    //     定时表达式
    public ScheduledAttribute(string cron)
    {
        Cron = cron;
    }

    //
    // 摘要:
    //     定时任务作业
    //
    // 参数:
    //   cron:
    //     定时表达式
    //
    //   remark:
    //     备注
    public ScheduledAttribute(string cron, string remark)
    {
        Cron = cron;
        Remark = remark;
    }
}