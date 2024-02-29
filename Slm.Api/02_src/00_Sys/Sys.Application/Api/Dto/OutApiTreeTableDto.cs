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
    public long Id { get; set; }


    public string? Label { get; set; }

    public string? Path { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int? Sort { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnable { get; set; }


    public List<OutApiTreeTableDto> Children { get; set; }

}
