using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Org;

public partial class OrgEntity
{
    /// <summary>
    /// 树行
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public List<OrgEntity> Children { get; set; }
}
