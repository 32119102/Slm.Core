using SqlSugar;
using System.Linq.Expressions;
using Slm.Data.Abstractions.Entities;
using System.Runtime.CompilerServices;

namespace Slm.Data.Abstractions;

public interface IRepositoryDel<TEntity>
{

    Task<bool> DeleteAsync(TEntity deleteObj);

    Task<bool> DeleteAsync(List<TEntity> deleteObjs);

    Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> whereExpression);

    Task<bool> DeleteByIdAsync(dynamic id);

    Task<bool> DeleteByIdsAsync(dynamic[] ids);


    ///// <summary>
    ///// 软删除
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //Task<bool> FalseDeleteAsync(dynamic id);

    /// <summary>
    /// 软删除
    /// </summary>
    /// <param name="ids">new object[]{}</param>
    /// <returns></returns>
    Task<bool> FalseDeletesAsync(dynamic ids);

    /// <summary>
    /// 软删除
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    Task<bool> FalseDeleteAsync(Expression<Func<TEntity, bool>> whereExpression);














    ///// <summary>
    ///// 删除指定ID的数据
    ///// </summary>
    ///// <param name="id">主键ID</param>
    ///// <returns></returns>
    //Task<bool> DeleteAsync(dynamic id);

    ///// <summary>
    ///// 删除指定ID集合的数据
    ///// </summary>
    ///// <param name="id">主键ID</param>
    ///// <returns></returns>
    //Task<bool> BulkDeleteAsync(dynamic id);


    ///// <summary>
    ///// 根据条实体删除
    ///// </summary>
    ///// <param name="whereExpression"></param>
    ///// <returns></returns>
    //Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> whereExpression);


    ///// <summary>
    ///// 根据条实体删除
    ///// </summary>
    ///// <returns></returns>
    //Task<bool> BulkDeleteAsync(List<TEntity> entities);



}
