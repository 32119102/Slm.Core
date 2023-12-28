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
    public long? ParentId { get; set; } = 0;


    /// <summary>
    /// 名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 标签
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    public string? Path { get; set; }


    /// <summary>
    /// http类型
    /// </summary>
    public string? HttpMethods { get; set; }


    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }


    /// <summary>
    /// 排序
    /// </summary>
    public int? Sort { get; set; }


    /// <summary>
    /// 启用
    /// </summary>
    public bool Enabled { get; set; } = true;
}