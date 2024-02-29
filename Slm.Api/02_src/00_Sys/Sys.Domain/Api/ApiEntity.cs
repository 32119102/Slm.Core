using Slm.Data.Abstractions.Entities;
using Slm.Utils.Core.Annotations;
using SqlSugar;
using Sys.Domain.Shared.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Api;


/// <summary>
/// 租户表
/// </summary>
[SugarTable("sys_api")]
[SysTable]
public partial class ApiEntity : EntityBase<long>
{

    /// <summary>
    /// 父级id
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public long? ParentId { get; set; } = 0;


    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(IsNullable = true,Length =256)]
    public string? Name { get; set; }

    /// <summary>
    /// 标签
    /// </summary>
    [SugarColumn(IsNullable = true, Length = 256)]
    public string? Label { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    [SugarColumn(IsNullable = true, Length = 256)]
    public string? Path { get; set; }


    /// <summary>
    /// http类型
    /// </summary>
    [SugarColumn(IsNullable = true, Length = 256)]
    public string? HttpMethods { get; set; }


    /// <summary>
    /// 描述
    /// </summary>
    [SugarColumn(IsNullable = true, Length = 256)]
    public string? Description { get; set; }


    /// <summary>
    /// 排序
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public int? Sort { get; set; }


    /// <summary>
    /// 启用
    /// </summary>
    [SugarColumn(IsNullable = true,ColumnDataType = "bit")]
    public bool Enabled { get; set; } = true;
}