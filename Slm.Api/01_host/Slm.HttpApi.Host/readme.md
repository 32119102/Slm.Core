## ConfigHelper
后面不需要使用的可以直接ConfigHelper.Get,如果有使用则需要注入
```
List<DbOption> dbs = ConfigHelper.Get<List<DbOption>>("dbconfig", InternalApp.WebHostEnvironment!.EnvironmentName);
var dbConfigRoot = ConfigHelper.Load("dbconfig", InternalApp.WebHostEnvironment!.EnvironmentName, true);
InternalApp.Services.Configure<List<DbOption>>(dbConfigRoot);



```
## 日志的写法

```
//写入字符串
_logger.LogInformation("{Po}".ToLog(), SerilogConst.Info, "哈哈哈");

//写入对象
A a = new A{Age = 1,Name = "你好"};
_logger.LogInformation("{@Po}".ToLog(), SerilogConst.Info, a);

 //写入集合
A a2 = new A{Age = 1,Name = "你好"};
List<A> aList = new List<A>() { a, a2 };
_logger.LogInformation("{@Po}".ToLog(), SerilogConst.Info, aList);
```
