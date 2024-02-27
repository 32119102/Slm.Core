using Slm.Utils.Core.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Org.Dto;

public class InOrgSearchTreeTableDto
{
    /// <summary>
    /// 父级id
    /// </summary>
    public long Pid { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string? Name { get; set; }
}
