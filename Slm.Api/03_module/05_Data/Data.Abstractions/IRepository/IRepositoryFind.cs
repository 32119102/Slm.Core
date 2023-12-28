using SqlSugar;
using System.Linq.Expressions;
using Slm.Utils.Core.Pagination;
using Slm.Data.Abstractions.Entities;

namespace Slm.Data.Abstractions;

public interface IRepositoryFind<TEntity>
{
    /// <summary>
    /// 查询一条数据(id)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity> GetAsync(dynamic id);

    /// <summary>
    /// 查询一条数据(自定义条件)
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> whereExpression);

    /// <summary>
    /// 查询集合
    /// </summary>
    /// <returns></returns>

    Task<List<TEntity>> GetListAsync();

    /// <summary>
    /// 查询集合
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> whereExpression);

    /// <summary>
    /// 获取分页数据
    /// </summary>
    /// <param name="conditionalList"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    Task<QueryResultModel<TEntity>> GetPageAsync(List<IConditionalModel> conditionalList, QueryDto query);

    /// <summary>
    /// 分页,排序
    /// </summary>
    /// <param name="conditionalList"></param>
    /// <param name="query"></param>
    /// <param name="orderByExpression"></param>
    /// <param name="orderByType"></param>
    /// <returns></returns>
    Task<QueryResultModel<TEntity>> GetPageListAsync(List<IConditionalModel> conditionalList, QueryDto query, Expression<Func<TEntity, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);


    Task<bool> IsAnyAsync(Expression<Func<TEntity, bool>> whereExpression);

    Task<int> CountAsync(Expression<Func<TEntity, bool>> whereExpression);




    ///// <summary>
    ///// 根据主键集合查询
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //Task<List<TEntity>> GetListAsync(dynamic id);

    ///// <summary>
    ///// 根据表达式查询List集合
    ///// </summary>
    ///// <param name="expression"></param>
    ///// <returns></returns>
    //Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression);

    ///// <summary>
    ///// 根据条件查询列表数据
    ///// </summary>
    ///// <param name="conditions"></param>
    ///// <returns></returns>
    //Task<List<TEntity>> GetListAsync(List<IConditionalModel> conditions);

    ///// <summary>
    ///// 查询数据集合
    ///// </summary>
    ///// <returns></returns>
    //Task<List<TEntity>> GetListAsync();

    ///// <summary>
    ///// 根据主键判断是否存在
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //Task<bool> ExistsAsync(dynamic id);


    ///// <summary>
    ///// 根据主键判断是否存在
    ///// </summary>
    ///// <param name="expression"></param>
    ///// <returns></returns>
    //Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);

    ///// <summary>
    ///// 是否存在
    ///// </summary>
    ///// <param name="expression"></param>
    ///// <returns></returns>
    //Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);

    ///// <summary>
    ///// 查询
    ///// </summary>
    ///// <returns></returns>
    //ISugarQueryable<TEntity> Find();

    ///// <summary>
    ///// 查询
    ///// </summary>
    ///// <param name="expression">过滤条件</param>
    ///// <returns></returns>
    //ISugarQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression);


    //ISugarQueryable<TEntity> CrossFind();
}
