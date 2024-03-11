using Sys.Domain.Shared.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Role.Dto;

public class InGrantDataScopeDto
{
    /// <summary>
    /// 数据范围（1全部数据 2本部门及以下数据 3本部门数据 4仅本人数据 5自定义数据）
    /// </summary>
    public DataScopeEnum DataScope { get; set; }

    public List<long> OrgIds { get; set; }

}
