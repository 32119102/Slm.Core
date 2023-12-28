using Slm.Utils.Core.ConfigurableOptions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slm.SerilogLogger;

/// <summary>
/// 日志配置
/// </summary>
public sealed class SerilogFileOptions : IConfigurableOptions
{
    /// <summary>
    /// 文件夹区分
    /// </summary>
    public string[] FileFolders { get; set; }

    /// <summary>
    /// 输入模版
    /// </summary>
    public string OutputTemplate { get; set; }

    /// <summary>
    /// 输出文件路径
    /// </summary>
    public string FilePath { get; set; }
}