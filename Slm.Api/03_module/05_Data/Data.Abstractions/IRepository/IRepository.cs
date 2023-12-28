using Slm.Utils.Core.Pagination;
using SqlSugar;
using System.Linq.Expressions;

namespace Slm.Data.Abstractions;

public interface IRepository
{


    /// <summary>
    /// 获取动态查询条件
    /// </summary>
    /// <typeparam name="TEntitySearch"></typeparam>
    /// <param name="search"></param>
    /// <param name="prefix"></param>
    /// <returns></returns>
    List<IConditionalModel> GetConditionals<TEntitySearch>(TEntitySearch search, string prefix = "");

}


/// <summary>
/// 接口基类
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IBaseRepository<TEntity> :
    IRepository,
   
    ISimpleClient<TEntity> where TEntity : class, new()
{

    #region 查询
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
    #endregion





    #region 软删除
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
    #endregion

}


//IRepositoryAdd<TEntity>,
//    IRepositoryUpdate<TEntity>,
//    IRepositoryDel<TEntity>,
//    IRepositoryFind<TEntity>,