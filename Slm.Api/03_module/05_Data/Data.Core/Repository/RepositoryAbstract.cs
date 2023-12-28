using MapsterMapper;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Data.SqlClient;
using Slm.Data.Abstractions;
using Slm.Data.Abstractions.Entities;
using Slm.Data.Core.Extensions;
using Slm.Utils.Core;
using Slm.Utils.Core.Annotations;
using Slm.Utils.Core.Extensions;
using SqlSugar;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace Slm.Data.Core.Repository;



/// <summary>
/// 仓储基类
/// </summary>
/// <typeparam name="TEntity">实体</typeparam>
public   partial class RepositoryAbstract<TEntity> : SimpleClient<TEntity>, IRepository
    where TEntity : class, new()
{
    //用来处理事务
    protected ITenant iTenant = null;

    public RepositoryAbstract()
    {
        iTenant = App.GetRequiredService<ISqlSugarClient>().AsTenant();

        base.Context = iTenant.GetConnectionScope(SqlSugarConst.MainConfigId);
        // 若实体贴有多库特性，则返回指定库连接
        if (typeof(TEntity).IsDefined(typeof(TenantAttribute), false))
        {
            base.Context = iTenant.GetConnectionScopeWithAttr<TEntity>();
            return;
        }

        //// 若实体贴有日志表特性，则返回日志库连接
        //if (typeof(T).IsDefined(typeof(LogTableAttribute), false))
        //{
        //    base.Context = iTenant.IsAnyConnection(SqlSugarConst.LogConfigId)
        //        ? iTenant.GetConnectionScope(SqlSugarConst.LogConfigId)
        //        : iTenant.GetConnectionScope(SqlSugarConst.MainConfigId);
        //    return;
        //}

        // 若实体贴有系统表特性，则返回默认库连接
        if (typeof(TEntity).IsDefined(typeof(SysTableAttribute), false))
        {
            base.Context = iTenant.GetConnectionScope(SqlSugarConst.MainConfigId);
            return;
        }

        //// 若未贴任何表特性或当前未登录或是默认租户Id，则返回默认库连接
        //var tenantId = App.User?.FindFirst(ClaimConst.TenantId)?.Value;
        //if (string.IsNullOrWhiteSpace(tenantId) || tenantId == SqlSugarConst.MainConfigId) return;

        //// 根据租户Id切换库连接, 为空则返回默认库连接
        //var sqlSugarScopeProvider = App.GetRequiredService<SysTenantService>().GetTenantDbConnectionScope(long.Parse(tenantId));
        //if (sqlSugarScopeProvider == null) return;

        //base.Context = sqlSugarScopeProvider;
    }



    /// <summary>
    /// 根据查询模型获取条件
    /// </summary>
    /// <param name="search"></param>
    /// <param name="prefix"></param>
    /// <returns></returns>
    public virtual List<IConditionalModel> GetConditionals<TEntitySearch>(TEntitySearch search, string prefix = "")
    {
        List<IConditionalModel> conModels = new List<IConditionalModel>();
        if (search.IsNull())
        {
            return conModels;
        }
        Type type = search!.GetType();
        var propertys = type.GetProperties()
            .Where(it => it.GetCustomAttributes(typeof(SearchAttribute), true).Any())
            .ToList();
        Type et = typeof(TEntity);
        if (propertys != null)
        {
            foreach (var p in propertys)
            {
                var v = p.GetValue(search, null)?.ToString();
                if (v.IsNull()) continue;

                var s = (SearchAttribute)p.GetCustomAttributes(true).First(it => it is SearchAttribute);
                if (!s.IsNull())
                {
                    conModels.Add(new ConditionalModel
                    {
                        FieldName = (prefix.IsNull() ? "" : prefix + ".") + (s.FieldName.IsNull() ?
                        et.GetColumnName(p.Name) : s.FieldName),
                        ConditionalType = (ConditionalType)s.ConditionalType,
                        FieldValue = v
                    });
                }
            }
        }
        return conModels;
    }










    ///// <summary>
    ///// 事务
    ///// </summary>
    //private readonly ITran _tran;
    ///// <summary>
    ///// 事务数据操作
    ///// </summary>
    //private SqlSugarClient _dbBase;
    ///// <summary>
    ///// 单表操作，切库使用
    ///// </summary>
    //private ISqlSugarClient? _db
    //{
    //    get; set;
    //}

    ///// <summary>
    ///// 用于基类,实体仓储层中使用
    ///// </summary>
    //public ISqlSugarClient Db
    //{
    //    get { return _db!; }
    //}


    ///// <summary>
    ///// 构造函数
    ///// </summary>
    ///// <param name="tran"></param>
    //public RepositoryAbstract(ITran tran)
    //{
    //    _tran = tran;
    //    _dbBase = tran.GetDbClient();
    //    var configId = typeof(TEntity).GetCustomAttribute<TenantAttribute>()?.configId ?? 0;
    //    //单表操作
    //    _db = _dbBase.GetConnection(configId);
    //}






    ///// <summary>
    ///// 获取数据库连接
    ///// </summary>
    ///// <param name="configId"></param>
    ///// <returns></returns>
    //public ISqlSugarClient GetConnection(string configId)
    //{
    //    var data = _dbBase.GetConnection(configId);
    //    return _dbBase.GetConnection(configId);
    //}

    //public async Task<string> UseStoredProcedure(string actionName, string para, string values)
    //{
    //    try
    //    {        
    //        SugarParameter[]? pars = new SugarParameter[] {
    //                new SugarParameter("O_VALUE", "", true),
    //                new SugarParameter("I_EPT_ID", UserResolver.TenantId),
    //                new SugarParameter("I_FILE", actionName),
    //                new SugarParameter("I_PARM", para),
    //                new SugarParameter("I_VALUE", values)
    //        };
    //        //无法识别存储过程中的异常
    //        this.Db.Ado.UseStoredProcedure().ExecuteCommand(actionName, pars);
    //        return pars[0].Value.ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        return ex.Message;
    //    }
    //}


}

