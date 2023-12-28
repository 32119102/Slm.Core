using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slm.Data.Abstractions.Entities;


/// <summary>
/// 假删除接口过滤器
/// </summary>
public interface IDeletedFilter
{
    /// <summary>
    /// 软删除
    /// </summary>
    bool IsDelete { get; set; }
}

/// <summary>
/// 租户Id接口过滤器
/// </summary>
public interface ITenantIdFilter
{
    /// <summary>
    /// 租户Id
    /// </summary>
    long? TenantId { get; set; }
}

/// <summary>
/// 机构Id接口过滤器
/// </summary>
public interface IOrgIdFilter
{
    /// <summary>
    /// 创建者部门Id
    /// </summary>
    long? CreatOrgId { get; set; }
}