using Slm.Data.Abstractions.Entities;
using Slm.Utils.Core.Annotations;
using SqlSugar;
using Sys.Domain.Shared.Company;

namespace Sys.Domain.Company;

/// <summary>
/// 企业信息
/// </summary>
[SugarTable("sys_company")]
[SysTable]
public partial class CompanyEntity : EntityBase<long>
{
    /// <summary>
    /// 企业代码
    /// </summary>			
    [SugarColumn(ColumnName = "Code")]
    public string? Code { get; set; }

    /// <summary>
    /// 企业名称
    /// </summary>			
    [SugarColumn(ColumnName = "Name")]
    public string? Name { get; set; }

    /// <summary>
    /// 简称
    /// </summary>			
    [SugarColumn(ColumnName = "ShortName")]
    public string? ShortName { get; set; }

    /// <summary>
    /// 企业状态
    /// </summary>			
    [SugarColumn(ColumnName = "Status")]
    public CompanyStatusEnum? Status { get; set; }

    /// <summary>
    /// 企业类型，来自代码表
    /// </summary>			
    [SugarColumn(ColumnName = "Type")]
    public string? Type { get; set; }

    /// <summary>
    /// 统一社会信用代码
    /// </summary>			
    [SugarColumn(ColumnName = "SocialCreditCode")]
    public string? SocialCreditCode { get; set; }

    /// <summary>
    /// 联系人
    /// </summary>			
    [SugarColumn(ColumnName = "LinkPerson")]
    public string? LinkPerson { get; set; }

    /// <summary>
    /// 联系电话，多个逗号分隔
    /// </summary>			
    [SugarColumn(ColumnName = "LinkPhone")]
    public string? LinkPhone { get; set; }

    /// <summary>
    /// 联系邮箱
    /// </summary>			
    [SugarColumn(ColumnName = "LinkEmail")]
    public string? LinkEmail { get; set; }

    /// <summary>
    /// 企业网址
    /// </summary>			
    [SugarColumn(ColumnName = "UrlAddress")]
    public string? UrlAddress { get; set; }

    /// <summary>
    /// 企业地址
    /// </summary>			
    [SugarColumn(ColumnName = "Address")]
    public string? Address { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
	[SugarColumn(ColumnName = "Remark")]
    public string? Remark { get; set; }
}
