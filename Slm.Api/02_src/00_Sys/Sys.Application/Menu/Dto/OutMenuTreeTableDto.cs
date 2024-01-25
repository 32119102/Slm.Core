using Sys.Domain.Shared.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Menu.Dto;

public class OutMenuTreeTableDto
{
    public long Id { get; set; }


    /// <summary>
    /// 标题
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 组件
    /// </summary>
    public string? Component { get; set; }

    /// <summary>
    /// 类型(0.目录;1.菜单;2.按钮;)
    /// </summary>
    public MenuTypeEnum? Type { get; set; }


    /// <summary>
    /// 图标
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int? Sort { get; set; }


    /// <summary>
    /// 按钮编码
    /// </summary>
    public string? ButtonCode { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnable { get; set; }


    public List<OutMenuTreeTableDto> Children { get; set; }
}
