using Slm.Data.Abstractions.Entities;
using Slm.Utils.Core.Annotations;
using SqlSugar;
using Sys.Domain.Shared.Tenant;
using System.ComponentModel.DataAnnotations;

namespace Sys.Domain.Tenant;


/// <summary>
/// 租户表
/// </summary>
[SugarTable("sys_tenant")]
[SysTable]
public partial class TenantEntity : EntityBase<long>
{

    /// <summary>
    /// 名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// 租户类型(0.Id;1.Db;)
    /// </summary>
    [SugarColumn(ColumnName = "TenantType")]
    public TenantTypeEnum TenantType { get; set; }

    /// <summary>
    /// 数据库类型
    /// </summary>
    [SugarColumn(ColumnName = "DbType")]
    public SqlSugar.DbType DbType { get; set; }

    /// <summary>
    /// 数据库连接
    /// </summary>
    [SugarColumn(ColumnName = "Connection")]
    public string? Connection { get; set; }

    /// <summary>
    /// 数据库标识
    /// </summary>
    [SugarColumn(ColumnName = "ConfigId")]
    public string? ConfigId { get; set; }



}