using Slm.Auth.Abstractions;
using Slm.Data.Abstractions;
using Slm.Data.Abstractions.Attributes;
using Slm.Data.Abstractions.Entities;
using Slm.Data.Core.Extensions;
using Slm.Utils.Core.Extensions;
using SqlSugar;
using System.Collections;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace Slm.Data.Core.Repository;

/// <summary>
/// 删除
/// </summary>
public  partial class RepositoryAbstract<TEntity> : IRepositoryDel<TEntity>
{

    ///// <summary>
    ///// 软删除
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //public virtual async Task<bool> FalseDeleteAsync(dynamic id)
    //{
    //    return await Context.Updateable<TEntity>()
    //        .SetColumns("IsDelete", 1)
    //        .Where($"Id={id}").ExecuteCommandAsync() > 0;
    //}

    /// <summary>
    /// 软删除
    /// </summary>
    /// <param name="ids">object[] </param>
    /// <returns></returns>
    public virtual async Task<bool> FalseDeletesAsync(dynamic ids)
    {
        return await Context.Updateable<TEntity>()
            .SetColumns("IsDelete", 1)
            .In(ids).ExecuteCommandAsync() > 0;
    }


    /// <summary>
    /// 软删除
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    public async Task<bool> FalseDeleteAsync(Expression<Func<TEntity, bool>> whereExpression)
    {
        return await Context.Updateable<TEntity>()
            .SetColumns("IsDelete", 1)
            .Where(whereExpression).ExecuteCommandAsync() > 0;
    }





    ///// <summary>
    ///// 删除指定ID的数据
    ///// </summary>
    ///// <param name="id">主键ID</param>
    ///// <returns></returns>
    //public async Task<bool> DeleteAsync(dynamic id)
    //{
    //    return await Db.Deleteable<TEntity>(id).ExecuteCommandHasChangeAsync();
    //}

    ///// <summary>
    ///// 删除指定ID集合的数据
    ///// </summary>
    ///// <param name="id">主键ID</param>
    ///// <returns></returns>
    //public async Task<bool> BulkDeleteAsync(dynamic id)
    //{
    //    return await Db.Deleteable<TEntity>().In(id).ExecuteCommandHasChangeAsync();
    //}

    ///// <summary>
    ///// 根据条实体删除
    ///// </summary>
    ///// <param name="whereExpression"></param>
    ///// <returns></returns>
    //public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> whereExpression)
    //{
    //    return await Db.Deleteable<TEntity>(whereExpression).ExecuteCommandHasChangeAsync();
    //}

    ///// <summary>
    ///// 根据条实体删除
    ///// </summary>
    ///// <returns></returns>
    //public async Task<bool> BulkDeleteAsync(List<TEntity> entities)
    //{
    //    return await Db.Deleteable<TEntity>(entities).ExecuteCommandHasChangeAsync();
    //}

    ///// <summary>
    ///// 软删除
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //public virtual async Task<bool> SoftDeleteAsync(dynamic id)
    //{
    //    // 实体类型
    //    var et = typeof(TEntity);
    //    string keyName = GetPrimaryName(et);
    //    var result = await Db.Updateable<TEntity>()
    //     .SlmDelColumns()
    //     .SetColumns(keyName, id as object)
    //     .WhereColumns(keyName)
    //     .ExecuteCommandHasChangeAsync();
    //    return result;





    //    //// 获取实体表名
    //    //var table = GetTableName(et);
    //    ////字典
    //    //var dt = new Dictionary<string, object>();
    //    //dt.Add("Id", id);
    //    //dt.Add("IsDeleted", 1);
    //    //dt.Add("DeletId", UserResolver.UserId!);
    //    //dt.Add("Deletor", UserResolver.UserName);
    //    //dt.Add("Deleted", DateTime.Now);
    //    //int i = await Db.Updateable(dt).AS(table).WhereColumns("Id").ExecuteCommandAsync();
    //    //return i > 0;
    //}

    ///// <summary>
    ///// 软删除
    ///// </summary>
    ///// <param name="whereExpression"></param>
    ///// <returns></returns>
    //public async Task<bool> SoftDeleteAsync(Expression<Func<TEntity, bool>> whereExpression)
    //{

    //    int i = await Db.Updateable<TEntity>().SlmDelColumns().Where(whereExpression).ExecuteCommandAsync();
    //    return i > 0;
    //}

    ///// <summary>
    ///// 软删除多条数据
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //public virtual async Task<bool> BulkSoftDeleteAsync(dynamic id)
    //{
    //    // 实体类型
    //    var et = typeof(TEntity);
    //    string keyName = GetPrimaryName(et);

    //    string sql = $"{keyName} in (";
    //    foreach (var item in id)
    //    {
    //        sql += $"{item},";
    //    }
    //    sql = sql.TrimEnd(',');
    //    sql += ")";
    //    var result = await Db.Updateable<TEntity>()
    //     .SlmDelColumns()
    //     .Where(sql)
    //     .ExecuteCommandHasChangeAsync();
    //    return result;


    //    //// 获取实体表名
    //    //var table = GetTableName(et);

    //    //string sql = @$"update  {table} set IsDeleted=@IsDeleted,DeletId=@DeletId,Deletor=@Deletor,Deleted=@Deleted where id in(";
    //    //foreach (var item in id)
    //    //{
    //    //    sql += $"{item},";
    //    //}
    //    //sql = sql.TrimEnd(',');
    //    //sql += ")";
    //    //int i = await Db.Ado.ExecuteCommandAsync(sql,
    //    //      new List<SugarParameter>(){
    //    //              new SugarParameter("@IsDeleted",1),
    //    //              new SugarParameter("@DeletId",UserResolver.UserId),
    //    //              new SugarParameter("@Deletor",UserResolver.UserName),
    //    //              new SugarParameter("@Deleted",DateTime.Now)
    //    //          });

    //    //return i > 0;





    //}

    ///// <summary>
    ///// 获取实体表名
    ///// </summary>
    ///// <param name="et"></param>
    ///// <returns></returns>
    //public static string GetTableName(Type? et = null)
    //{
    //    if (et == null)
    //        et = typeof(TEntity);
    //    var table = et.GetTableName();
    //    if (table.IsNull())
    //        throw new Exception("获取表名失败");
    //    return table;
    //}


    ///// <summary>
    ///// 获取主键信息(是否多主键,主键类型)
    ///// </summary>
    ///// <param name="t"></param>
    ///// <returns></returns>
    //public virtual string GetPrimaryName(Type t)
    //{
    //    var entityInfo = Db.EntityMaintenance.GetEntityInfo(t);
    //    List<EntityColumnInfo> primarycolumn = entityInfo.Columns.Where(it => it.IsPrimarykey).ToList();

    //    int keyCount = primarycolumn.Count;
    //    string nameKey = string.Empty;
    //    if (keyCount == 1)
    //    {
    //        return primarycolumn[0].DbColumnName;
    //    }
    //    else
    //    {
    //        return nameKey;
    //    }
    //}


}



public static class DelExtion
{





    public static IUpdateable<T> SlmDelColumns<T>(this IUpdateable<T> updateable) where T : class, new()
    {
        Type type = typeof(T);
        Type et = typeof(T);
        var propertys = type.GetProperties()
       .Where(it => it.GetCustomAttributes(typeof(TableFieldAttribute), true).Any())
       .ToList();
        if (propertys.NotNull())
        {
            foreach (var p in propertys)
            {
                var s = (TableFieldAttribute)p.GetCustomAttributes(true).First(it => it is TableFieldAttribute);
                if (!s.IsNull())
                {
                    if (s.HandleType == HandleTypeEnum.Del)
                    {
                        if (s.TableType == TableTypeEnum.UserId)
                        {
                            updateable = updateable.SetColumns(et.GetColumnName(p.Name), UserResolver.UserId);
                        }
                        else if (s.TableType == TableTypeEnum.UserName)
                        {
                            updateable = updateable.SetColumns(et.GetColumnName(p.Name), UserResolver.UserName);
                        }
                        else if (s.TableType == TableTypeEnum.Dt)
                        {
                            updateable = updateable.SetColumns(et.GetColumnName(p.Name), DateTime.Now);
                        }
                        else if (s.TableType == TableTypeEnum.IsDelete)
                        {
                            updateable = updateable.SetColumns(et.GetColumnName(p.Name), 1);
                        }
                    }
                }
            }
        }
        return updateable;
    }
}
