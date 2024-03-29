﻿using Mapster;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Slm.Auth.Abstractions;
using Slm.Data.Abstractions;
using Slm.Data.Abstractions.Attributes;
using Slm.Data.Abstractions.Entities;
using Slm.Data.Abstractions.SqlSugar;
using Slm.Data.Core.Repository;
using Slm.Utils.Core;
using Slm.Utils.Core.Annotations;
using Slm.Utils.Core.ConfigurableOptions.Extensions;
using Slm.Utils.Core.Const;
using Slm.Utils.Core.Extensions;
using Slm.Utils.Core.Helpers;
using SqlSugar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace Slm.Data.Core;

public static class SqlSugarSetup
{

    /// <summary>
    /// SqlSugar 上下文初始化
    /// </summary>
    public static void AddSqlSugar()
    {

        var snowConfig = App.GetOptions<SnowIdOptions>();
        // 注册雪花Id
        YitIdHelper.SetIdGenerator(snowConfig);
        // 自定义 SqlSugar 雪花ID算法
        SnowFlakeSingle.WorkId = snowConfig.WorkerId;
        StaticConfig.CustomSnowFlakeFunc = () =>
        {
            return YitIdHelper.NextId();
        };

        var dbOptions = App.GetOptions<DbOptions>();

        dbOptions.ConnectionConfigs.ForEach(SetDbConfig);


        var dddd = dbOptions.ConnectionConfigs.Adapt<List<ConnectionConfig>>();

        SqlSugarScope sqlSugar = new SqlSugarScope(dbOptions.ConnectionConfigs.Adapt<List<ConnectionConfig>>(), db =>
        {
            dbOptions.ConnectionConfigs.ForEach(config =>
            {
                var dbProvider = db.GetConnectionScope(config.ConfigId);
                SetDbAop(dbProvider, dbOptions.EnableConsoleSql);
                SetDbDiffLog(dbProvider, config);
            });
        });


        InternalApp.Services!.AddSingleton<ISqlSugarClient>(sqlSugar);


        // 初始化数据库表结构及种子数据
        dbOptions.ConnectionConfigs.ForEach(config =>
        {
            InitDatabase(sqlSugar, config);
        });


        //    builder.RegisterAssemblyTypes(assembly!)
        //    .Where(m =>
        //              m.Name.EndsWith("Repository") && !m.IsInterface)
        //        .AsImplementedInterfaces()
        //        .InstancePerLifetimeScope()
        //             .PropertiesAutowired();// 属性注入
        //}

        //InternalApp.Services!.AddScoped<ISqlSugarClient>(o =>
        //{
        //    SqlSugarClient db = new SqlSugarClient(dbOptions.ConnectionConfigs.Adapt<List<ConnectionConfig>>(), db => {
        //        dbOptions.ConnectionConfigs.ForEach(config =>
        //        {
        //            var dbProvider = db.GetConnectionScope(config.ConfigId);
        //            SetDbAop(dbProvider, dbOptions.EnableConsoleSql);
        //            SetDbDiffLog(dbProvider, config);
        //        });

        //    });


        //    return db;
        //}); // 单例注册



    }


    /// <summary>
    /// 配置连接属性
    /// </summary>
    /// <param name="config"></param>
    public static void SetDbConfig(DbConnectionConfig config)
    {
        //var configureExternalServices = new ConfigureExternalServices
        //{
        //    EntityNameService = (type, entity) => // 处理表
        //    {
        //        entity.IsDisabledDelete = true; // 禁止删除非 sqlsugar 创建的列
        //        // 只处理贴了特性[SugarTable]表
        //        if (!type.GetCustomAttributes<SugarTable>().Any())
        //            return;
        //        if (config.DbSettings.EnableUnderLine && !entity.DbTableName.Contains('_'))
        //            entity.DbTableName = UtilMethods.ToUnderLine(entity.DbTableName); // 驼峰转下划线
        //    },
        //    EntityService = (type, column) => // 处理列
        //    {
        //        // 只处理贴了特性[SugarColumn]列
        //        if (!type.GetCustomAttributes<SugarColumn>().Any())
        //            return;
        //        if (new NullabilityInfoContext().Create(type).WriteState is NullabilityState.Nullable)
        //            column.IsNullable = true;
        //        if (config.DbSettings.EnableUnderLine && !column.IsIgnore && !column.DbColumnName.Contains('_'))
        //            column.DbColumnName = UtilMethods.ToUnderLine(column.DbColumnName); // 驼峰转下划线
        //    },
        //    DataInfoCacheService = new SqlSugarCache(),
        //};
        //config.ConfigureExternalServices = configureExternalServices;

        config.InitKeyType = InitKeyType.Attribute;
        config.IsAutoCloseConnection = true;
        config.MoreSettings = new ConnMoreSettings
        {
            IsAutoRemoveDataCache = true,
            IsAutoDeleteQueryFilter = true, // 启用删除查询过滤器
            IsAutoUpdateQueryFilter = true, // 启用更新查询过滤器
            SqlServerCodeFirstNvarchar = true // 采用Nvarchar
        };
    }

    /// <summary>
    /// 配置Aop
    /// </summary>
    /// <param name="db"></param>
    /// <param name="enableConsoleSql"></param>
    public static void SetDbAop(SqlSugarScopeProvider db, bool enableConsoleSql)
    {
        var config = db.CurrentConnectionConfig;

        // 设置超时时间
        db.Ado.CommandTimeOut = 30;

        // 打印SQL语句
        if (enableConsoleSql)
        {
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                var originColor = Console.ForegroundColor;
                if (sql.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
                    Console.ForegroundColor = ConsoleColor.Green;
                if (sql.StartsWith("UPDATE", StringComparison.OrdinalIgnoreCase) || sql.StartsWith("INSERT", StringComparison.OrdinalIgnoreCase))
                    Console.ForegroundColor = ConsoleColor.Yellow;
                if (sql.StartsWith("DELETE", StringComparison.OrdinalIgnoreCase))
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("【" + DateTime.Now + "——执行SQL】\r\n" + UtilMethods.GetSqlString(config.DbType, sql, pars) + "\r\n");
                Console.ForegroundColor = originColor;

            };
            db.Aop.OnError = ex =>
            {
                if (ex.Parametres == null) return;
                //var pars = db.Utilities.SerializeObject(((SugarParameter[])ex.Parametres).ToDictionary(it => it.ParameterName, it => it.Value));
                ConsoleHelper.WriteColorLine("【" + DateTime.Now + "——错误SQL】\r\n" + UtilMethods.GetSqlString(config.DbType, ex.Sql, (SugarParameter[])ex.Parametres) + "\r\n", ConsoleColor.DarkRed);


            };
            db.Aop.OnLogExecuted = (sql, pars) =>
            {
                // 执行时间超过5秒
                if (db.Ado.SqlExecutionTime.TotalSeconds > 5)
                {
                    var fileName = db.Ado.SqlStackTrace.FirstFileName; // 文件名
                    var fileLine = db.Ado.SqlStackTrace.FirstLine; // 行号
                    var firstMethodName = db.Ado.SqlStackTrace.FirstMethodName; // 方法名
                    var log = $"【所在文件名】：{fileName}\r\n【代码行数】：{fileLine}\r\n【方法名】：{firstMethodName}\r\n" + $"【sql语句】：{UtilMethods.GetSqlString(config.DbType, sql, pars)}";
                    ConsoleHelper.WriteColorLine(log, ConsoleColor.DarkYellow);
                }
            };
        }
        // 数据审计
        db.Aop.DataExecuting = (oldValue, entityInfo) =>
        {
            //ConsoleHelper.WriteErrorLine("行事件99999");
            /*** 行级别事件 ：更新一条记录只进一次 ***/
            //if (entityInfo.EntityColumnInfo.IsPrimarykey)
            //{

            //    ConsoleHelper.WriteErrorLine("行事件111");
            //}


            if (entityInfo.OperationType == DataFilterType.InsertByObject)
            {
                //ConsoleHelper.WriteErrorLine("列事件222");

                // 主键(long类型)且没有值的---赋值雪花Id
                if (entityInfo.EntityColumnInfo.IsPrimarykey && entityInfo.EntityColumnInfo.PropertyInfo.PropertyType == typeof(long))
                {
                    var id = entityInfo.EntityColumnInfo.PropertyInfo.GetValue(entityInfo.EntityValue);
                    if (id == null || (long)id == 0)
                        entityInfo.SetValue(YitIdHelper.NextId());
                }
                if (App.User != null)
                {
                    var s = entityInfo.GetAttribute<TableFieldAttribute>();
                    if (s != null)
                    {

                        switch (s.TableType)
                        {
                            case TableTypeEnum.UserId:
                                entityInfo.SetValue(UserResolver.UserId);
                                break;
                            case TableTypeEnum.UserName:
                                entityInfo.SetValue(UserResolver.UserName);
                                break;
                            case TableTypeEnum.TenantId:
                                entityInfo.SetValue(UserResolver.TenantId);
                                break;
                            case TableTypeEnum.CreatOrgId:
                                entityInfo.SetValue(UserResolver.UserId);
                                break;
                            default:
                                break;
                        }

                    }




                    //if (entityInfo.PropertyName == SqlSugarConst.TenantId)
                    //{
                    //    var tenantId = ((dynamic)entityInfo.EntityValue).TenantId;
                    //    if (tenantId == null || tenantId == 0)
                    //        entityInfo.SetValue(UserResolver.TenantId);
                    //}
                    //else if (entityInfo.PropertyName == SqlSugarConst.CreatId)
                    //{
                    //    var creatId = ((dynamic)entityInfo.EntityValue).CreatId;
                    //    if (creatId == 0 || creatId == null)
                    //        entityInfo.SetValue(UserResolver.UserId);
                    //}
                    //else if (entityInfo.PropertyName == SqlSugarConst.Creator)
                    //{
                    //    var creator = ((dynamic)entityInfo.EntityValue).Creator;
                    //    if (string.IsNullOrEmpty(creator))
                    //        entityInfo.SetValue(UserResolver.UserName);
                    //}
                    //else if (entityInfo.PropertyName == SqlSugarConst.CreatOrgId)
                    //{
                    //    var creatOrgId = ((dynamic)entityInfo.EntityValue).CreatOrgId;
                    //    if (creatOrgId == 0 || creatOrgId == null)
                    //        entityInfo.SetValue(UserResolver.UserId);
                    //}
                }
            }
            if (entityInfo.OperationType == DataFilterType.UpdateByObject)
            {

                var s = entityInfo.GetAttribute<TableFieldAttribute>();
                if (s != null)
                {
                    if (App.User != null)
                    {

                        switch (s.TableType)
                        {
                            case TableTypeEnum.UserId:
                                entityInfo.SetValue(UserResolver.UserId);
                                break;
                            case TableTypeEnum.UserName:
                                entityInfo.SetValue(UserResolver.UserName);
                                break;
                            case TableTypeEnum.Dt:
                                entityInfo.SetValue(DateTime.Now);
                                break;
                        }

                    }
                    else
                    {
                        switch (s.TableType)
                        {
                            case TableTypeEnum.Dt:
                                entityInfo.SetValue(DateTime.Now);
                                break;
                        }
                    }
                }



                //if (entityInfo.PropertyName == SqlSugarConst.Lastmodified)
                //    entityInfo.SetValue(DateTime.Now);
                //else if (entityInfo.PropertyName == SqlSugarConst.LastmodifiId)
                //    entityInfo.SetValue(UserResolver.UserId);
                //else if (entityInfo.PropertyName == SqlSugarConst.Lastmodifier)
                //    entityInfo.SetValue(UserResolver.UserName);
            }
        };
        // 配置假删除过滤器
        db.QueryFilter.AddTableFilter<IDeletedFilter>(u => u.IsDelete == false);


        // 超管排除其他过滤器
        if (UserResolver.AccountType == AccountTypeEnum.SuperAdmin)
            return;


        // 配置租户过滤器
        if (UserResolver.TenantId > 0)
        {
            db.QueryFilter.AddTableFilter<ITenantIdFilter>(u => u.TenantId == UserResolver.TenantId);

        }


        // 配置用户机构（数据范围）过滤器
        SqlSugarFilter.SetOrgEntityFilter(db);

        // 配置自定义过滤器
        SqlSugarFilter.SetCustomEntityFilter(db);
    }

    /// <summary>
    /// 开启库表差异化日志
    /// </summary>
    /// <param name="db"></param>
    /// <param name="config"></param>
    private static void SetDbDiffLog(SqlSugarScopeProvider db, DbConnectionConfig config)
    {
        if (!config.DbSettings.EnableDiffLog) return;

        db.Aop.OnDiffLogEvent = async u =>
        {

            //todo 事件,插入表里面

            //var logDiff = new SysLogDiff
            //{
            //    // 操作后记录（字段描述、列名、值、表名、表描述）
            //    AfterData = JSON.Serialize(u.AfterData),
            //    // 操作前记录（字段描述、列名、值、表名、表描述）
            //    BeforeData = JSON.Serialize(u.BeforeData),
            //    // 传进来的对象
            //    BusinessData = JSON.Serialize(u.BusinessData),
            //    // 枚举（insert、update、delete）
            //    DiffType = u.DiffType.ToString(),
            //    Sql = UtilMethods.GetSqlString(config.DbType, u.Sql, u.Parameters),
            //    Parameters = JSON.Serialize(u.Parameters),
            //    Elapsed = u.Time == null ? 0 : (long)u.Time.Value.TotalMilliseconds
            //};
            //await db.Insertable(logDiff).ExecuteCommandAsync();


            ConsoleHelper.WriteErrorLine(DateTime.Now + $"\r\n*****差异日志开始*****\r\n{Environment.NewLine}{db.Utilities.SerializeObject(u)}{Environment.NewLine}*****差异日志结束*****\r\n");
        };
    }






    /// <summary>
    /// 初始化数据库
    /// </summary>
    /// <param name="db"></param>
    /// <param name="config"></param>
    private static void InitDatabase(SqlSugarScope db, DbConnectionConfig config)
    {
        SqlSugarScopeProvider dbProvider = db.GetConnectionScope(config.ConfigId);

        // 初始化/创建数据库
        if (config.DbSettings.EnableInitDb)
        {
            if (config.DbType != SqlSugar.DbType.Oracle)
                dbProvider.DbMaintenance.CreateDatabase();
        }

        // 初始化表结构
        if (config.TableSettings.EnableInitTable)
        {
            AssemblyHelper assemblyHelper = new AssemblyHelper();
            var types = assemblyHelper.LoadByNameEndStringArry(".Domain").SelectMany(a=>a.GetTypes()).ToList();
         

            var entityTypes = types.Where(u => !u.IsInterface && !u.IsAbstract && u.IsClass && u.IsDefined(typeof(SugarTable), false))
            .ToList();

            if (config.ConfigId.ToString() == SqlSugarConst.MainConfigId) // 默认库（有系统表特性、没有日志表和租户表特性）
                entityTypes = entityTypes.Where(u => u.GetCustomAttributes<SysTableAttribute>().Any() || (u.GetCustomAttributes<TenantAttribute>().Any())).ToList();          
            else
                entityTypes = entityTypes.Where(u => u.GetCustomAttribute<TenantAttribute>()?.configId.ToString() == config.ConfigId.ToString()).ToList(); // 自定义的库

            foreach (var entityType in entityTypes)
            {
                if (entityType.GetCustomAttribute<SplitTableAttribute>() == null)
                    dbProvider.CodeFirst.InitTables(entityType);
                else
                    dbProvider.CodeFirst.SplitTables().InitTables(entityType);
            }
        }

        // 初始化种子数据
        if (config.SeedSettings.EnableInitSeed)
        {
            AssemblyHelper assemblyHelper = new AssemblyHelper();
            var types = assemblyHelper.LoadByNameEndStringArry(".Sqlsugar").SelectMany(a => a.GetTypes()).ToList();

            var seedDataTypes = types.Where(u => !u.IsInterface && !u.IsAbstract && u.IsClass && u.GetInterfaces().Any(i => i.HasImplementedRawGeneric(typeof(ISqlSugarEntitySeedData<>))))
               .ToList();

            foreach (var seedType in seedDataTypes)
            {
                var entityType = seedType.GetInterfaces().First().GetGenericArguments().First();
                if (config.ConfigId.ToString() == SqlSugarConst.MainConfigId) // 默认库（有系统表特性、没有日志表和租户表特性）
                {
                    if (entityType.GetCustomAttribute<SysTableAttribute>() == null && (entityType.GetCustomAttribute<TenantAttribute>() != null))
                        continue;
                }
           
                else
                {
                    var att = entityType.GetCustomAttribute<TenantAttribute>(); // 自定义的库
                    if (att == null || att.configId.ToString() != config.ConfigId.ToString()) continue;
                }

                var instance = Activator.CreateInstance(seedType);
                var hasDataMethod = seedType.GetMethod("HasData");
                var seedData = ((IEnumerable)hasDataMethod?.Invoke(instance, null))?.Cast<object>();
                if (seedData == null) continue;

                var entityInfo = dbProvider.EntityMaintenance.GetEntityInfo(entityType);
                if (entityInfo.Columns.Any(u => u.IsPrimarykey))
                {
                    // 按主键进行批量增加和更新
                    var storage = dbProvider.StorageableByObject(seedData.ToList()).ToStorage();
                    storage.AsInsertable.ExecuteCommand();
                    storage.AsUpdateable.ExecuteCommand();
                }
                else
                {
                    // 无主键则只进行插入
                    if (!dbProvider.Queryable(entityInfo.DbTableName, entityInfo.DbTableName).Any())
                        dbProvider.InsertableByObject(seedData.ToList()).ExecuteCommand();
                }
            }
        }
    }



    ///// <summary>
    ///// 初始化租户业务数据库 (新增租户使用)
    ///// </summary>
    ///// <param name="iTenant"></param>
    ///// <param name="config"></param>
    //public static void InitTenantDatabase(ITenant iTenant, DbConnectionConfig config)
    //{
    //    SetDbConfig(config);

    //    if (!iTenant.IsAnyConnection(config.ConfigId.ToString()))
    //        iTenant.AddConnection(config);
    //    var db = iTenant.GetConnectionScope(config.ConfigId.ToString());
    //    db.DbMaintenance.CreateDatabase();

    //    // 获取所有业务表-初始化租户库表结构（排除系统表、日志表、特定库表）
    //    var entityTypes = App.EffectiveTypes.Where(u => !u.IsInterface && !u.IsAbstract && u.IsClass && u.IsDefined(typeof(SugarTable), false) &&
    //        !u.IsDefined(typeof(SysTableAttribute), false) && !u.IsDefined(typeof(LogTableAttribute), false) && !u.IsDefined(typeof(TenantAttribute), false)).ToList();
    //    if (!entityTypes.Any()) return;

    //    foreach (var entityType in entityTypes)
    //    {
    //        var splitTable = entityType.GetCustomAttribute<SplitTableAttribute>();
    //        if (splitTable == null)
    //            db.CodeFirst.InitTables(entityType);
    //        else
    //            db.CodeFirst.SplitTables().InitTables(entityType);
    //    }
    //}
}
