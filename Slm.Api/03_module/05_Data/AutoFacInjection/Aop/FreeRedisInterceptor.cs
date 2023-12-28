

//using Castle.DynamicProxy;
//using FreeRedis;
//using Newtonsoft.Json;
//using System.Reflection;
//using Wj.Data.Core.Login;
//using Wj.Utils.Annotations;
//using Wj.Utils.Extensions;
//using Wj.Utils.Helpers;
//using IInterceptor = Castle.DynamicProxy.IInterceptor;

//namespace Slm.AutoFacInjection.Aop;

//public class FreeRedisInterceptor : IInterceptor
//{
//    private readonly RedisClient _redisClient;

//    public FreeRedisInterceptor(RedisClient redisClient)
//    {
//        this._redisClient = redisClient;
//    }

//    public void Intercept(IInvocation invocation)
//    {
//        var method = invocation.MethodInvocationTarget ?? invocation.Method;
//        var freeReidsAttribute = method.GetCustomAttribute<FreeReidsAttribute>();
//        if (freeReidsAttribute == null)
//        {
//            //调用业务方法
//            invocation.Proceed();
//        }
//        else
//        {
//            try
//            {
//                ConsoleHelper.WriteColorLine("触发aop缓存事件", ConsoleColor.Red);
//                invocation.Proceed();
//                string key = freeReidsAttribute.Key;
//                if (freeReidsAttribute.IsLogin)
//                {
//                    key = $"{UserResolver.UserId}:{key}";
//                }
//                Type returnType;
//                if (typeof(Task).IsAssignableFrom(method.ReturnType))
//                {
//                    returnType = method.ReturnType.GenericTypeArguments.FirstOrDefault();
//                }
//                else
//                {
//                    returnType = method.ReturnType;
//                }
//                if (_redisClient.Exists(key))
//                {
//                    var cacheValue = _redisClient.Get(key);
//                    dynamic _result = JsonConvert.DeserializeObject(cacheValue, returnType);
//                    invocation.ReturnValue = (typeof(Task).IsAssignableFrom(method.ReturnType)) ? Task.FromResult(_result)
//                        : _result;

//                }
//                else
//                {
//                    // 异步获取异常，先执行
//                    if (invocation.Method.IsAsyncMethod())
//                    {
//                        dynamic result = invocation.ReturnValue;
//                        //等待完成
//                        Task.WaitAll(result as Task);
//                        string json = JsonConvert.SerializeObject(result.Result);
//                        if (freeReidsAttribute.timeoutSeconds.HasValue)
//                        {

//                            _redisClient.Set(key, json, timeoutSeconds: freeReidsAttribute.timeoutSeconds.Value);
//                        }
//                        else
//                        {
//                            _redisClient.Set(key, json);
//                        }
//                    }
//                    else
//                    {

//                    }
//                }


//            }
//            catch (Exception ex)
//            {
//                ConsoleHelper.WriteErrorLine("缓存数据设置或者读取失败");

//            }
//        }
//    }




//}
