using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Slm.Utils.Core.Models;


public class ModuleEnumDescriptor
{
    /// <summary>
    /// 模块名称
    /// </summary>
    public string ModuleName { get; set; } = String.Empty;

    /// <summary>
    /// 枚举名称
    /// </summary>
    public string Name { get; set; } = String.Empty;

    /// <summary>
    /// 枚举类型
    /// </summary>
    [JsonIgnore]
    public Type? Type { get; set; }

    /// <summary>
    /// 枚举项下拉列表
    /// </summary>
    public IList<OptionResultModel>? Options { get; set; }
}