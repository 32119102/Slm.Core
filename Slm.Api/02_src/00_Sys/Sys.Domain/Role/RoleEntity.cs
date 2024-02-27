using Slm.Data.Abstractions.Entities;
using Slm.Utils.Core.Annotations;
using SqlSugar;
using Sys.Domain.Shared.Role;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Role;

/// <summary>
/// 租户表
/// </summary>
[SugarTable("sys_role")]
[SysTable]
public partial class RoleEntity : EntityTenant<long>
{


    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "Name")]
    public string? Name { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [SugarColumn(ColumnName = "Code")]
    public string? Code { get; set; }


    /// <summary>
    /// 排序
    /// </summary>
    [SugarColumn(ColumnName = "Sort")]
    public int? Sort { get; set; }

    /// <summary>
    /// 数据库连接
    /// </summary>
    [SugarColumn(ColumnName = "Remark")]
    public string? Remark { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    [SugarColumn(ColumnName = "IsEnable")]
    public bool IsEnable { get; set; }


    /// <summary>
    /// 数据范围（1全部数据 2本部门及以下数据 3本部门数据 4仅本人数据 5自定义数据）
    /// </summary>
    [SugarColumn(ColumnName = "DataScope")]
    public DataScopeEnum DataScope { get; set; } = DataScopeEnum.Self;

}

