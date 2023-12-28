using Sys.Domain.Shared;
using Sys.Domain.Shared.Company;

namespace Sys.Application.Company.Dto;

/// <summary>
///  返回详情Dto
/// </summary>
public class OutCompanyDto
{
    /// <summary>
    /// 企业ID
    /// </summary>
    public string CmpId { get; set; }
    /// <summary>
    /// 代码
    /// </summary>
    public string Code { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 简称
    /// </summary>
    public string ShortName { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public CompanyStatusEnum Status { get; set; }
    /// <summary>
    /// 企业类型
    /// </summary>
    public string Type { get; set; }
    /// <summary>
    /// 统一信用代码
    /// </summary>
    public string SocialCreditCode { get; set; }
    /// <summary>
    /// 联系人
    /// </summary>
    public string LinkPerson { get; set; }
    /// <summary>
    /// 联系电话
    /// </summary>
    public string LinkPhone { get; set; }
    /// <summary>
    /// 联系邮箱
    /// </summary>
    public string LinkEmail { get; set; }
    /// <summary>
    /// 企业地址
    /// </summary>
    public string Address { get; set; }
    /// <summary>
    /// 企业网址
    /// </summary>
    public string UrlAddress { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

}
