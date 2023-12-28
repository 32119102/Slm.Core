using Autofac;
using MediatR;
using Slm.Local.Event.PipelineBehaviours;
using Slm.Validation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slm.Validation.FluentValidation;
public class AutoFacModuleRegister : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        //注入验证结果格式化器
        builder.RegisterType<ValidateResultFormatHandler>().As<IValidateResultFormatHandler>().SingleInstance();

        //注入 MediatR 模型验证通道
        builder.RegisterGeneric(typeof(ValidationBehaviour<,>)).As(typeof(IPipelineBehavior<,>)).InstancePerDependency();


    }


}