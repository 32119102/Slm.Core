using Slm.Auth.Abstractions;
using Slm.Data.Abstractions;
using Slm.Data.Abstractions.Attributes;
using Slm.Utils.Core.Extensions;
using SqlSugar;

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Slm.Data.Core.Repository;

/// <summary>
/// 新增
/// </summary>
public  partial class RepositoryAbstract<TEntity> : IRepositoryAdd<TEntity>
{










    ///// <summary>
    ///// 获取主键信息(是否多主键,主键类型)
    ///// </summary>
    ///// <param name="t"></param>
    ///// <returns></returns>
    //public virtual (bool isMultipleKey, Type? type) GetPrimaryKeys(Type t)
    //{
    //    var entityInfo = Db.EntityMaintenance.GetEntityInfo(t);
    //    List<EntityColumnInfo> primarycolumn = entityInfo.Columns.Where(it => it.IsPrimarykey).ToList();
    //    bool isMultipleKey = false;
    //    int keyCount = primarycolumn.Count;
    //    Type? type = null;
    //    if (keyCount == 1)
    //    {
    //        type = primarycolumn[0].UnderType;
    //    }
    //    else
    //    {
    //        isMultipleKey = true;
    //    }
    //    return (isMultipleKey, type);
    //}

    ///// <summary>
    ///// 新增
    ///// 支持多种主键类型
    ///// </summary>
    ///// <param name="entity">实体</param>
    ///// <returns></returns>
    //public async Task<dynamic?> AddAsync(TEntity entity)
    //{
    //    await SetCreateInfo(entity);
    //    var insert = Db.Insertable(entity);
    //    Type et = typeof(TEntity);
    //    (bool isMultipleKey, Type? type) keyInfo = GetPrimaryKeys(et);
    //    if (keyInfo.isMultipleKey)
    //    {
    //        await insert.ExecuteCommandAsync();
    //        return null;
    //    }
    //    if (keyInfo.type == typeof(long))
    //    {
    //        long id = await insert.ExecuteReturnSnowflakeIdAsync();
    //        return id;
    //    }
    //    else if (keyInfo.type == typeof(int))
    //    {
    //        int id = await insert.ExecuteReturnIdentityAsync();
    //        return id;
    //    }
    //    else
    //    {
    //        await insert.ExecuteCommandAsync();
    //        return null;
    //    }

    //}

    ///// <summary>
    ///// 写入实体数据
    ///// 支持多种主键类型
    ///// </summary>
    ///// <param name="entity">实体类</param>
    ///// <param name="insertColumns">指定只插入列</param>
    ///// <returns>返回自增量列</returns>
    //public async Task<dynamic?> AddAsync(TEntity entity, Expression<Func<TEntity, object>> insertColumns)
    //{
    //    await SetCreateInfo(entity);
    //    var insert = Db.Insertable(entity);
    //    insert = insert.InsertColumns(insertColumns);

    //    Type et = typeof(TEntity);
    //    (bool isMultipleKey, Type? type) keyInfo = GetPrimaryKeys(et);
    //    if (keyInfo.isMultipleKey)
    //    {
    //        await insert.ExecuteCommandAsync();
    //        return null;
    //    }
    //    if (keyInfo.type == typeof(long))
    //    {
    //        long id = await insert.ExecuteReturnSnowflakeIdAsync();
    //        return id;
    //    }
    //    else if (keyInfo.type == typeof(int))
    //    {
    //        int id = await insert.ExecuteReturnIdentityAsync();
    //        return id;
    //    }
    //    else
    //    {
    //        await insert.ExecuteCommandAsync();
    //        return null;
    //    }
    //}

    ///// <summary>
    ///// 批量插入大数据实体(1万条以上)
    ///// </summary>
    ///// <param name="listEntity">实体集合</param>
    ///// <returns>影响行数</returns>
    //public async Task<bool> BulkAddAsync(List<TEntity> listEntity)
    //{
    //    if (listEntity.Count == 0)
    //    {
    //        return false;
    //    }

    //    foreach (var item in listEntity)
    //    {
    //        await SetCreateInfo(item);
    //    }
    //    Type et = typeof(TEntity);
    //    (bool isMultipleKey, Type? type) keyInfo = GetPrimaryKeys(et);
    //    if (keyInfo.isMultipleKey)
    //    {
    //        if (listEntity.Count <= 500)
    //        {
    //            int i = await Db.Insertable(listEntity).UseParameter().ExecuteCommandAsync();
    //            return i == listEntity.Count;
    //        }
    //        else
    //        {
    //            int i = await Db.Fastest<TEntity>().BulkCopyAsync(listEntity);
    //            return i == listEntity.Count;
    //        }
    //    }
    //    if (keyInfo.type == typeof(long))
    //    {
    //        var ids = await Db.Insertable(listEntity).ExecuteReturnSnowflakeIdListAsync();
    //        return ids.Count == listEntity.Count;
    //    }
    //    else if (keyInfo.type == typeof(int))
    //    {
    //        if (listEntity.Count <= 500)
    //        {
    //            int i = await Db.Insertable(listEntity).UseParameter().ExecuteCommandAsync();
    //            return i == listEntity.Count;
    //        }
    //        else
    //        {
    //            int i = await Db.Fastest<TEntity>().BulkCopyAsync(listEntity);
    //            return i == listEntity.Count;
    //        }
    //    }
    //    else
    //    {
    //        if (listEntity.Count <= 500)
    //        {
    //            int i = await Db.Insertable(listEntity).UseParameter().ExecuteCommandAsync();
    //            return i == listEntity.Count;
    //        }
    //        else
    //        {
    //            int i = await Db.Fastest<TEntity>().BulkCopyAsync(listEntity);
    //            return i == listEntity.Count;
    //        }
    //    }



    //}




    ///// <summary>
    ///// 批量插入大数据实体(1万条以上)
    ///// </summary>
    ///// <param name="listEntity">实体集合</param>
    ///// <returns>影响行数</returns>
    //public async Task<bool> BulkSnowFlakeAddAsync(List<TEntity> listEntity)
    //{
    //    if (listEntity.Count == 0)
    //    {
    //        return false;
    //    }

    //    foreach (var item in listEntity)
    //    {
    //        await SetCreateInfo(item);
    //    }

    //    if (listEntity.Count <= 500)
    //    {
    //        int i = await Db.Insertable(listEntity).UseParameter().ExecuteCommandAsync();
    //        return i == listEntity.Count;
    //    }
    //    else
    //    {
    //        int i = await Db.Fastest<TEntity>().BulkCopyAsync(listEntity);
    //        return i == listEntity.Count;
    //    }



    //}


    ///// <summary>
    ///// 设置创建信息
    ///// </summary>
    //public Task SetCreateInfo<T>(T entity)
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
    //                if (s.HandleType == HandleTypeEnum.Add)
    //                {
    //                    if (s.TableType == TableTypeEnum.TenantId)
    //                    {
    //                        p.SetValue(entity, UserResolver.TenantId);
    //                    }
    //                    else if (s.TableType == TableTypeEnum.UserId)
    //                    {
    //                        p.SetValue(entity, UserResolver.UserId);
    //                    }
    //                    else if (s.TableType == TableTypeEnum.UserName)
    //                    {
    //                        p.SetValue(entity, UserResolver.UserName);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    return Task.FromResult(entity);
    //}
}