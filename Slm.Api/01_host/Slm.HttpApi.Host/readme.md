## ConfigHelper
���治��Ҫʹ�õĿ���ֱ��ConfigHelper.Get,�����ʹ������Ҫע��
```
List<DbOption> dbs = ConfigHelper.Get<List<DbOption>>("dbconfig", InternalApp.WebHostEnvironment!.EnvironmentName);
var dbConfigRoot = ConfigHelper.Load("dbconfig", InternalApp.WebHostEnvironment!.EnvironmentName, true);
InternalApp.Services.Configure<List<DbOption>>(dbConfigRoot);



```
## ��־��д��

```
//д���ַ���
_logger.LogInformation("{Po}".ToLog(), SerilogConst.Info, "������");

//д�����
A a = new A{Age = 1,Name = "���"};
_logger.LogInformation("{@Po}".ToLog(), SerilogConst.Info, a);

 //д�뼯��
A a2 = new A{Age = 1,Name = "���"};
List<A> aList = new List<A>() { a, a2 };
_logger.LogInformation("{@Po}".ToLog(), SerilogConst.Info, aList);
```



## ��������
```

--����һ�����ݣ�ȡ��5������
select * from im_chat  order by id  limit 1, 5;

--�α��ҳ,ÿ����Ҫ��ȡ���ص�������Сid
select * from im_chat where id>15449102576709 order by id  limit 1;


```