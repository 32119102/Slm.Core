using Slm.Utils.Core.Annotations;
using Slm.Utils.Core.Pagination;


namespace Sys.Application.Tenant.Dto;


/// <summary>
/// 查询
/// </summary>
public class InTenantSearchDto : QueryDto
{

    ///// <summary>
    ///// 企业代码
    ///// </summary>
    //[Search(ConditionalType = SlmConditionalType.Equal, FieldName = "Code")]
    //public string? Code { get; set; }

    /// <summary>
    /// 企业名称
    /// </summary>
    [Search(ConditionalType = SlmConditionalType.Like)]
    public string? Name { get; set; }
}
