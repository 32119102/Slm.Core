using Slm.Data.Abstractions.Entities;
using Slm.Utils.Core.Annotations;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Org;


/// <summary>
/// 租户表
/// </summary>
[SugarTable("sys_org")]
[SysTable]
public partial class OrgEntity : EntityTenant<long>
{

    /// <summary>
    /// 父级id
    /// </summary>
    [SugarColumn(ColumnName = "Pid")]
    public long? Pid { get; set; }

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
    /// 等级
    /// </summary>
    [SugarColumn(ColumnName = "Level")]
    public int? Level { get; set; }

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

}