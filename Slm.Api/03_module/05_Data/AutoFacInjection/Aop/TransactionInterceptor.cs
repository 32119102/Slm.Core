using Castle.DynamicProxy;
using Slm.Data.Abstractions;
using Slm.Data.Abstractions.Attributes;
using Slm.Utils.Core.Extensions;
using Slm.Utils.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Slm.AutoFacInjection.Aop;


public class TransactionInterceptor : IInterceptor
{
    private readonly ITran _tran;

    public TransactionInterceptor(ITran tran)
    {
        _tran = tran;
    }


    public void Intercept(IInvocation invocation)
    {
        var transactionAttribute = invocation.MethodInvocationTarget.GetCustomAttribute<TransactionAttribute>();
        if (transactionAttribute == null)
        {
            //调用业务方法
            invocation.Proceed();
        }
        else
        {
            try
            {
                ConsoleHelper.WriteErrorLine("触发aop事务事件");
                _tran.BeginTran();
                invocation.Proceed();
                // 异步获取异常，先执行
                if (invocation.Method.IsAsyncMethod())
                {
                    dynamic result = invocation.ReturnValue;
                    if (result.Exception != null)
                    {
                        ConsoleHelper.WriteErrorLine("Rollback Transaction");
                        _tran.RollbackTran();
                    }
                    //等待完成
                    Task.WaitAll(result as Task);
                    _tran.CommitTran();
                    ConsoleHelper.WriteErrorLine("aop事务完成");
                    //if (result.Result.Successful)
                    //{
                    //    _tran.CommitTran();
                    //}
                    //else
                    //{
                    //    _tran.RollbackTran();
                    //}
                }
                else
                {
                    _tran.CommitTran();
                }
            }
            catch (Exception)
            {
                ConsoleHelper.WriteErrorLine("Rollback Transaction");
                _tran.RollbackTran();
            }
        }
    }
}
