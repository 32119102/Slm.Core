using Slm.Utils.Core.Annotations;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Menu2Api;

/// <summary>
/// 租户表
/// </summary>
[SugarTable("sys_menu2api")]
[SysTable]
public partial class Menu2ApiEntity
{

    [SugarColumn(IsPrimaryKey = true)]
    public long MenuId { get; set; }

    [SugarColumn(IsPrimaryKey = true)]
    public long ApiId { get; set; }

}