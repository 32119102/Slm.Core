using Slm.Data.Abstractions;
using Slm.Data.Abstractions.Attributes;
using Slm.Data.Core.Extensions;
using Slm.Auth.Abstractions;
using Slm.Utils.Core.Extensions;
using SqlSugar;

using System.Linq.Expressions;
using System.Reflection;


namespace Slm.Data.Core.Repository;

/// <summary>
/// 修改
/// </summary>
public partial class RepositoryAbstract<TEntity> : IRepositoryUpdate<TEntity>
{








    ///// <summary>
    ///// 更新
    ///// </summary>
    ///// <param name="entity">实体</param>
    ///// <returns></returns>
    //public async Task<bool> UpdateAsync(TEntity entity)
    //{
    //    await SetUpdateInfo(entity);
    //    var update = Db.Updateable(entity);
    //    return await update.ExecuteCommandHasChangeAsync();
    //}

    ///// <summary>
    ///// 更新指定的列
    ///// </summary>
    ///// <param name="columns"></param>
    ///// <param name="where"></param>
    ///// <returns></returns>
    //public async Task<bool> UpdateColumnsAsync(Expression<Func<TEntity, TEntity>> columns, Expression<Func<TEntity, bool>> where)
    //{
    //    var result = await Db.Updateable<TEntity>()
    //        .SetColumns(columns)//类只能在表达示里面不能提取
    //        .WjSetColumns()
    //        .Where(where)
    //        .ExecuteCommandHasChangeAsync();
    //    return result;
    //}

    ///// <summary>
    ///// 更新指定的列
    ///// </summary>
    ///// <param name="entity"></param>
    ///// <param name="lstColumns"></param>
    ///// <param name="lstIgnoreColumns"></param>
    ///// <param name="strWhere"></param>
    ///// <returns></returns>
    //public async Task<bool> UpdateAsync(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "")
    //{
    //    IUpdateable<TEntity> up = Db.Updateable(entity);
    //    if (lstIgnoreColumns != null && lstIgnoreColumns.Count > 0)
    //    {
    //        up = up.IgnoreColumns(lstIgnoreColumns.ToArray());
    //    }


    //    if (lstColumns != null && lstColumns.Count > 0)
    //    {
    //        up = up.UpdateColumns(lstColumns.ToArray()).WjSetColumns();
    //    }
    //    if (!string.IsNullOrEmpty(strWhere))
    //    {
    //        up = up.Where(strWhere);
    //    }

    //    return await up.ExecuteCommandHasChangeAsync();
    //}

    ///// <summary>
    ///// 批量更新大数据实体(1万条以上)
    ///// </summary>
    ///// <param name="listEntity"></param>
    ///// <param name="whereColumns"></param>
    ///// <param name="updateColumns"></param>
    ///// <returns></returns>
    //public async Task<bool> BulkUpdateAsync(List<TEntity> listEntity, List<string> whereColumns = null, List<string> updateColumns = null)
    //{
    //    foreach (var item in listEntity)
    //    {
    //        await SetUpdateInfo(item);
    //    }

    //    if (listEntity.Count < 500)
    //    {
    //        var update = Db.Updateable(listEntity);
    //        if (whereColumns != null && whereColumns.Count > 0)
    //        {
    //            update = update.WhereColumns(whereColumns.ToArray());
    //        }
    //        if (updateColumns != null && updateColumns.Count > 0)
    //        {
    //            update = update.UpdateColumns(updateColumns.ToArray());
    //        }
    //        return await update.ExecuteCommandHasChangeAsync();
    //    }
    //    else
    //    {
    //        int i = 0;
    //        if (whereColumns != null && whereColumns.Count > 0)
    //        {
    //            if (updateColumns != null && updateColumns.Count > 0)
    //            {
    //                i = await Db.Fastest<TEntity>().BulkUpdateAsync(listEntity, whereColumns.ToArray(), updateColumns.ToArray());
    //            }
    //            else
    //            {
    //                i = await Db.Fastest<TEntity>().BulkUpdateAsync(listEntity, whereColumns.ToArray());
    //            }
    //        }
    //        else
    //        {
    //            i = await Db.Fastest<TEntity>().BulkUpdateAsync(listEntity);
    //        }
    //        return i == listEntity.Count();
    //    }
    //}



    ///// <summary>
    ///// 设置更新信息
    ///// </summary>
    //public Task SetUpdateInfo<T>(T entity)
    //{
    //    Type type = entity.GetType();
    //    var propertys = type.GetProperties()
    //   .Where(it => it.GetCustomAttributes(typeof(TableFieldAttribute), true).Any())
    //   .ToList();
    //    if (propertys.NotNull())
    //    {

    //        foreach (var p in propertys)
    //        {
    //            var s = (TableFieldAttribute)p.GetCustomAttributes(true).First(it => it is TableFieldAttribute);
    //            if (!s.IsNull())
    //            {
    //                if (s.HandleType == HandleTypeEnum.Edit)
    //                {
    //                    if (s.TableType == TableTypeEnum.UserId)
    //                    {
    //                        p.SetValue(entity, UserResolver.UserId);
    //                    }
    //                    else if (s.TableType == TableTypeEnum.UserName)
    //                    {
    //                        p.SetValue(entity, UserResolver.UserName);
    //                    }
    //                    else if (s.TableType == TableTypeEnum.Dt)
    //                    {
    //                        p.SetValue(entity, DateTime.Now);
    //                    }

    //                }
    //            }
    //        }
    //    }
    //    return Task.FromResult(entity);
    //}
}





public static class UpdateExtion
{

    public static IUpdateable<T> WjSetColumns<T>(this IUpdateable<T> updateable) where T : class, new()
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
                    if (s.HandleType == HandleTypeEnum.Edit)
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
                    }
                }
            }
        }
        return updateable;
    }
}
