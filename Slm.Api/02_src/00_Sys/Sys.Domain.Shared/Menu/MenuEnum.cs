using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Shared.Menu;

/// <summary>
/// 类型(0.目录;1.菜单;2.按钮;)枚举
/// </summary>
[Description("菜单类型枚举")]
public enum MenuTypeEnum
{

    /// <summary>
    /// 未知
    /// </summary>
    [Description("未知")]
    UnKnown = -1,

    /// <summary>
    /// 目录
    /// </summary>
    [Description("目录")]
    Catalogue = 0,

    /// <summary>
    /// 菜单
    /// </summary>
    [Description("菜单")]
    Menu = 1,

    /// <summary>
    /// 按钮
    /// </summary>
    [Description("按钮")]
    Button = 2,




}
