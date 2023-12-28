using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Slm.Data.Abstractions
{
    public interface IRepositoryAdd<TEntity>
    {

        #region 单个实体
        Task<bool> InsertAsync(TEntity entity);


        Task<int> InsertReturnIdentityAsync(TEntity entity);


        Task<long> InsertReturnSnowflakeIdAsync(TEntity entity);
        #endregion


        #region 批量
        Task<bool> InsertRangeAsync(List<TEntity> entitys);
        #endregion












        ///// <summary>
        ///// 写入实体数据
        ///// </summary>
        ///// <param name="entity">实体类</param>
        ///// <param name="insertColumns">指定只插入列</param>
        ///// <returns>返回自增量列</returns>
        //Task<dynamic?> AddAsync(TEntity entity, Expression<Func<TEntity, object>> insertColumns);


        ///// <summary>
        ///// 批量插入大数据实体(1万条以上)
        ///// </summary>
        ///// <param name="listEntity">实体集合</param>
        ///// <returns>影响行数</returns>
        //Task<bool> BulkAddAsync(List<TEntity> listEntity);


        ///// <summary>
        ///// 批量插入大数据实体(1万条以上)
        ///// </summary>
        ///// <param name="listEntity">实体集合</param>
        ///// <returns>影响行数</returns>
        //Task<bool> BulkSnowFlakeAddAsync(List<TEntity> listEntity);

        ///// <summary>
        ///// 设置创建信息
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //Task SetCreateInfo<T>(T entity);
    }
}
