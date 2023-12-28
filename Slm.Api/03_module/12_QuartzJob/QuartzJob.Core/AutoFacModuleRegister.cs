using Autofac;
using Quartz.Impl;
using Quartz;
using Quartz.Spi;
using Slm.QuartzJob.Core.Helper;
using Slm.QuartzJob.Core.Abstractions;
using Slm.Utils.Core.Helpers;

namespace Slm.QuartzJob.Core
{
    public class AutoFacModuleRegister : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LocalJob>().InstancePerLifetimeScope();
            builder.RegisterType<WebApiJob>().InstancePerLifetimeScope();
            builder.RegisterType<JobFactory>().As<IJobFactory>().SingleInstance();
            builder.RegisterType<QuartzJobService>().As<IQuartzJobService>().InstancePerLifetimeScope();
            builder.RegisterType<DefaultJobHandler>().As<IJobHandler>().SingleInstance();
            
            //注入任务调度工厂
            builder.RegisterType<StdSchedulerFactory>().As<ISchedulerFactory>().SingleInstance();


            ConsoleHelper.WriteColorLine("======Wj.QuartzJob||注入定时任务======", ConsoleColor.DarkYellow);
        }
    }
}
