using Slm.Utils.Core.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Api.Dto;

public class InApiTreeTableSearchDto
{ 
    /// <summary>
    /// 企业名称
    /// </summary>
    [Search(ConditionalType = SlmConditionalType.Like)]
    public string? Label { get; set; }
}
