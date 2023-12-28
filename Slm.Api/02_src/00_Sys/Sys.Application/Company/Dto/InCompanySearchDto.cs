using Slm.Utils.Core.Annotations;
using Slm.Utils.Core.Pagination;
using System.Text.Json.Serialization;

namespace Sys.Application.Company.Dto;

/// <summary>
/// 查询Dto
/// </summary>
public class InCompanySearchDto: QueryDto
{
    /// <summary>
    /// 企业代码
    /// </summary>
    [Search(ConditionalType = SlmConditionalType.Equal, FieldName = "Code")]
    public string? Code { get; set; }

    /// <summary>
    /// 企业名称
    /// </summary>
    [Search(ConditionalType = SlmConditionalType.Like, FieldName = "Name")]
    public string? Name { get; set; }
}
