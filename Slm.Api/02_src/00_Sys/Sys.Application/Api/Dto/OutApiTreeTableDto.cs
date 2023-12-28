using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Api.Dto;

public class OutApiTreeTableDto
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Key { get; set; }


    public string? Label { get; set; }

    public string? Path { get; set; }


    public List<OutApiTreeTableDto> Children { get; set; }

}
