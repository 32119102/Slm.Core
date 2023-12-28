using Slm.Data.Abstractions;
using Slm.Data.Core.Extensions;
using Slm.Utils.Core.Pagination;
using SqlSugar;
using System.Linq.Expressions;

namespace Slm.Data.Core.Repository;

/// <summary>
/// 查询
/// </summary>
public  partial class RepositoryAbstract<TEntity> : IRepositoryFind<TEntity>
{
    public Task<TEntity> GetAsync(dynamic id)
    {
        return base.Context.Queryable<TEntity>().InSingleAsync(id);
    }

    public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> whereExpression) => base.GetFirstAsync(whereExpression);

    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="conditionalList"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public Task<QueryResultModel<TEntity>> GetPageAsync(List<IConditionalModel> conditionalList, QueryDto query) =>
        base.AsQueryable()
        .Where(conditionalList)
        .ToPaginationAsync(query.Paging);


    /// <summary>
    /// 条件分页
    /// </summary>
    /// <param name="conditionalList"></param>
    /// <param name="query"></param>
    /// <param name="orderByExpression"></param>
    /// <param name="orderByType"></param>
    /// <returns></returns>
    public Task<QueryResultModel<TEntity>> GetPageListAsync(List<IConditionalModel> conditionalList, QueryDto query, Expression<Func<TEntity, object>> orderByExpression, OrderByType orderByType)
    =>
        base.AsQueryable()
            .OrderByIF(orderByExpression != null, orderByExpression, orderByType)
      .Where(conditionalList)
      .ToPaginationAsync(query.Paging);








    ///// <summary>
    ///// 查询指定Id的数据
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //public virtual async Task<TEntity> GetAsync(dynamic id)
    //{
    //    return await Db.Queryable<TEntity>().In(id).SingleAsync();
    //}

    ///// <summary>
    ///// 查询指定Id的集合数据
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //public virtual async Task<List<TEntity>> GetListAsync(dynamic id)
    //{
    //    return await Db.Queryable<TEntity>().In(id).ToListAsync();
    //}


    ///// <summary>
    ///// 查询数据集合
    ///// </summary>
    ///// <returns></returns>
    //public virtual async Task<List<TEntity>> GetListAsync()
    //{
    //    return await Db.Queryable<TEntity>().ToListAsync();
    //}

    ///// <summary>
    ///// 根据表达式查询List集合
    ///// </summary>
    ///// <param name="expression"></param>
    ///// <returns></returns>
    //public virtual Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression)
    //{
    //    return Db.Queryable<TEntity>().Where(expression).ToListAsync();
    //}

    ///// <summary>
    ///// 根据条件查询列表数据
    ///// </summary>
    ///// <param name="conditions"></param>
    ///// <returns></returns>
    //public virtual Task<List<TEntity>> GetListAsync(List<IConditionalModel> conditions)
    //{
    //    return Db.Queryable<TEntity>().Where(conditions).ToListAsync();
    //}


    ///// <summary>
    ///// 获取分页数据
    ///// </summary>
    ///// <param name="conditionalModels"></param>
    ///// <param name="query"></param>
    ///// <returns></returns>
    //public virtual Task<QueryResultModel<TEntity>> GetPageAsync(List<IConditionalModel> conditionalModels, QueryDto query)
    //{
    //    return this.Find()
    //    .Where(conditionalModels)
    //    .ToPaginationAsync(query.Paging);
    //}





    ///// <summary>
    ///// 是否存在
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //public async Task<bool> ExistsAsync(dynamic id)
    //{
    //    return await Db.Queryable<TEntity>().In(id).AnyAsync();
    //}

    ///// <summary>
    ///// 是否存在
    ///// </summary>
    ///// <param name="expression"></param>
    ///// <returns></returns>
    //public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    //{
    //    return await Db.Queryable<TEntity>().Where(expression).AnyAsync();
    //}

    ///// <summary>
    ///// 是否存在
    ///// </summary>
    ///// <param name="expression"></param>
    ///// <returns></returns>
    //public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
    //{
    //    return await Db.Queryable<TEntity>().Where(expression).AnyAsync();
    //}


    ///// <summary>
    ///// 内存查询
    ///// </summary>
    ///// <returns></returns>
    //public ISugarQueryable<TEntity> Find()
    //{
    //    return Db.Queryable<TEntity>();
    //}

    //public ISugarQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
    //{
    //    return Db.Queryable<TEntity>().Where(expression);
    //}


    ///// <summary>
    ///// 跨库查询，用于导航查询
    ///// </summary>
    ///// <returns></returns>
    //public ISugarQueryable<TEntity> CrossFind() 
    //{

    //    return _dbBase.QueryableWithAttr<TEntity>();
    //}


}