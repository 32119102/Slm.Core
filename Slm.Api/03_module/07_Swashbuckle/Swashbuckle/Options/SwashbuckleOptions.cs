using Slm.Utils.Core.ConfigurableOptions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slm.Swashbuckle.Options;

public class SwashbuckleOptions:IConfigurableOptions
{
    /// <summary>
    /// 配置集合
    /// </summary>
    public List<SwashbuckleConfig> SwashbuckleConfigs { get; set; }


}


public class SwashbuckleConfig
{
    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; } = String.Empty;

    /// <summary>
    /// 名称,标题
    /// </summary>
    public string Name { get; set; } = String.Empty;

    /// <summary>
    /// 版本
    /// </summary>
    public string Version { get; set; } = String.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; } = String.Empty;

    ///// <summary>
    ///// 程序集集合，XML文件
    ///// </summary>
    //public List<string> AssemblyName { get; set; } = new List<string>();
}