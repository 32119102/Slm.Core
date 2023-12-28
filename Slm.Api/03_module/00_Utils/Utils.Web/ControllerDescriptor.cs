using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Slm.Utils.Web.Mvc;

/// <summary>
/// 控制器描述符
/// </summary>
public class ControllerDescriptor
{
    /// <summary>
    /// 区域
    /// </summary>
    public string Area { get; set; } = String.Empty;

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = String.Empty;

    /// <summary>
    /// 说明
    /// </summary>
    public string Description { get; set; } = String.Empty;

    /// <summary>
    /// 操作列表
    /// </summary>
    public List<ActionDescriptor>? Actions { get; set; }

    /// <summary>
    /// 类型信息
    /// </summary>
    [JsonIgnore]
    public TypeInfo? TypeInfo { get; set; }
}