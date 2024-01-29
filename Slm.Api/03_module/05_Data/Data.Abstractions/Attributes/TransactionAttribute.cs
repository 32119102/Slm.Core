using Rougamo;
using Rougamo.Context;
using Slm.Utils.Core;
using Slm.Utils.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slm.Data.Abstractions.Attributes;

/// <summary>
/// 特性事务,使用的项目需要安装nuget Rougamo.Fody 包才会生效
/// </summary>
public class TransactionAttribute : MoAttribute
{
    //public override AccessFlags Flags => AccessFlags.Public | AccessFlags.Method;


    public ITran _tran { get; set; }

    public override void OnEntry(MethodContext context)
    {
        // 从context对象中能取到包括入参、类实例、方法描述等信息
        ConsoleHelper.WriteErrorLine("触发aop事务事件");
        _tran = App.GetService<ITran>()!;
        _tran.BeginTran();


    }

    //public override void OnException(MethodContext context)
    //{
    //    _tran.RollbackTran();

    //}

    //public override void OnSuccess(MethodContext context)
    //{
    //    _tran.CommitTran();
    //}

    public override void OnExit(MethodContext context)
    {

        if (typeof(Task).IsAssignableFrom(context.RealReturnType))
            ((Task)context.ReturnValue!).ContinueWith(t => _OnExit());
        else _OnExit();

        void _OnExit()
        {
            if (context.Exception == null) _tran.CommitTran();
            else _tran.RollbackTran();
        }
        //ConsoleHelper.WriteErrorLine("方法退出时，不论方法执行成功还是异常，都会执行");
    }
}

