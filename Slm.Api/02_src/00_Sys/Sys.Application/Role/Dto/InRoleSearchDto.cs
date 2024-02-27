using Microsoft.AspNetCore.Mvc.RazorPages;
using Slm.Utils.Core.Annotations;
using Slm.Utils.Core.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Role.Dto;

public class InRoleSearchDto: QueryDto
{
    /// <summary>
    /// 企业名称
    /// </summary>
    [Search(ConditionalType = SlmConditionalType.Like)]
    public string? Name { get; set; }

    /// <summary>
    /// 企业名称
    /// </summary>
    [Search(ConditionalType = SlmConditionalType.Like)]
    public string? Code { get; set; }
}
