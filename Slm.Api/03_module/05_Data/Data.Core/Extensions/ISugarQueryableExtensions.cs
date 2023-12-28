using Slm.Utils.Core.Pagination;
using SqlSugar;
using System.Net.NetworkInformation;

namespace Slm.Data.Core.Extensions;

public static class ISugarQueryableExtensions
{
    /// <summary>
    /// 分页扩展类
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="sugarQueryable"></param>
    /// <param name="paging"></param>
    /// <returns></returns>
    public static async Task<QueryResultModel<TResult>> ToPaginationAsync<TResult>(this ISugarQueryable<TResult> sugarQueryable, Paging paging)
    {
        RefAsync<int> totalCount = 0;
        var rows = await sugarQueryable.ToPageListAsync(paging.Index, paging.Size, totalCount);
        paging.TotalCount = totalCount;

        return new QueryResultModel<TResult>(rows, paging.TotalCount);
    }

}
