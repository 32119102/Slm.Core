using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Api;

public partial class ApiEntity
{

    /// <summary>
    /// 树行
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public List<ApiEntity> Children { get; set; }


}
