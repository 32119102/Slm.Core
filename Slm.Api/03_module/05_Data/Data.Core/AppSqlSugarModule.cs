using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Slm.Data.Abstractions.Entities;
using Slm.Modularity.Abstractions;
using Slm.Utils.Core;
using Slm.Utils.Core.Helpers;
using SqlSugar;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Slm.Utils.Core.Extensions;
using Slm.Utils.Core.Const;
using Microsoft.Extensions.Configuration;
using Slm.Data.Abstractions;
using DbType = SqlSugar.DbType;

namespace Slm.Data.Core
{
    public class AppSqlSugarModule : AppModule
    {
        public override void ConfigureServices()
        {
            //添加sqlsugar 逻辑
            SqlSugarSetup.AddSqlSugar();


            //List<ConnectionConfig> dbList = new List<ConnectionConfig>();

            //var dbConfig = ConfigHelper.Get<DbConfig>("dbconfig", InternalApp.HostEnvironment!.EnvironmentName);
            //if (dbConfig == null)
            //    return;

            //dbConfig.dbs.ToList().ForEach(db =>
            //{
            //    ConnectionConfig connectionConfig = new ConnectionConfig();
            //    connectionConfig.ConfigId = db.ConfigId;
            //    connectionConfig.DbType = (DbType)db.DbType;
            //    connectionConfig.ConnectionString = db.ConnectionString;
            //    connectionConfig.IsAutoCloseConnection = true;
            //    if (connectionConfig.DbType == DbType.SqlServer)
            //    {
            //        connectionConfig.MoreSettings = new ConnMoreSettings()
            //        {
            //            IsWithNoLockQuery = true//查询
            //        };
            //    }
            //    dbList.Add(connectionConfig);
            //});

            //AssemblyHelper assemblyHelper = new AssemblyHelper();
            //// 把多个连接对象注入服务，这里必须采用Scope，因为有事务操作
            //InternalApp.Services!.AddScoped<ISqlSugarClient>(o =>
            //{
            //    var _logger = o.GetService<ILogger<ISqlSugarClient>>();
            //    var db = new SqlSugarClient(dbList,
            //    db =>
            //    {
            //        foreach (var item in dbList)
            //        {
            //            if (App.HostEnvironment.IsDevelopment())
            //            {
            //                db.GetConnection((string)item.ConfigId).Aop.OnLogExecuting = (sql, p) =>
            //                {
            //                    //执行时间超过1秒
            //                    if (db.Ado.SqlExecutionTime.TotalSeconds > 1)
            //                    {
            //                        //代码CS文件名
            //                        var fileName = db.Ado.SqlStackTrace.FirstFileName;
            //                        //代码行数
            //                        var fileLine = db.Ado.SqlStackTrace.FirstLine;
            //                        //方法名
            //                        var FirstMethodName = db.Ado.SqlStackTrace.FirstMethodName;
            //                        ConsoleHelper.WriteErrorLine($"{db.Ado.SqlExecutionTime.TotalSeconds}_{fileName}_{FirstMethodName}_{fileLine}");
            //                        //db.Ado.SqlStackTrace.MyStackTraceList[1].xxx 获取上层方法的信息
            //                        _logger.LogInformation("{info}".ToLog(), SerilogConst.Sql, $"{db.Ado.SqlExecutionTime.TotalSeconds}_{fileName}_{FirstMethodName}_{fileLine}");

            //                    }
            //                    _logger.LogInformation("{sql}\r\n{pars}".ToLog(), SerilogConst.Sql, sql, p == null ? "" : p.Select(it => it.ParameterName + ":" + it.Value).ToJoin());

            //                    ConsoleHelper.WriteSuccessLine(sql + "\r\n" + (p == null ? "" : p.Select(it => it.ParameterName + ":" + it.Value).ToJoin()));
            //                };
            //            }
            //        }
            //        //接口过滤器 (继承接口的类都有效) 请升级 5.1.3.47
            //        //db.QueryFilter.AddTableFilter<ISoftDelete>(it => it.IsDeleted == false);
            //        //if (!UserResolver.IsPadmin && UserResolver.TenantId > 0)
            //        //{
            //        //    db.QueryFilter.AddTableFilter<IWjTenant>(it => it.CompanyId == UserResolver.TenantId);
            //        //}
            //    });
            //    return db;
            //});




            ConsoleHelper.WriteColorLine("AppSqlSugarModule(ConfigureServices)==========", ConsoleColor.Green);
            ConsoleHelper.WriteColorLine("SqlSugar配置成功", ConsoleColor.Green);
        }
    }
}
