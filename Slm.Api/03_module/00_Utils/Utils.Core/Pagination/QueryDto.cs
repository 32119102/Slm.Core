using System.Text.Json.Serialization;

namespace Slm.Utils.Core.Pagination;

public abstract class QueryDto
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public QueryPagingDto Page { get; set; } = new QueryPagingDto();

    /// <summary>
    /// 转换成Paging分页类
    /// </summary>
    [JsonIgnore]
    public Paging Paging
    {
        get
        {
            return new Paging(Page.Index, Page.Size);
        }
    }
}
