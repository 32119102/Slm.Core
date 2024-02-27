using Slm.Utils.Core.Models;
using SqlSugar;
using Sys.Domain.Shared.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Tenant.Dto;


/// <summary>
/// 表格Dto
/// </summary>
public class OutTenantTableDto : TableDto
{
    public long Id { get; set; }

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
    public TenantTypeEnum TenantType { get; set; }

    /// <summary>
    /// 数据库类型
    /// </summary>
    public SqlSugar.DbType DbType { get; set; }

    /// <summary>
    /// 数据库连接
    /// </summary>
    public string? Connection { get; set; }

    /// <summary>
    /// 数据库标识
    /// </summary>
    public string? ConfigId { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnable { get; set; }
}
