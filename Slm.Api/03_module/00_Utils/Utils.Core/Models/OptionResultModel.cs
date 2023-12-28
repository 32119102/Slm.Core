using System.Globalization;

namespace Slm.Utils.Core.Models;

/// <summary>
/// 可选项返回模型
/// </summary>
public class OptionResultModel<T>
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Label { get; set; }=String.Empty;

    /// <summary>
    /// 值
    /// </summary>
    public object Value { get; set; } = String.Empty;
        
    /// <summary>
    /// 禁用
    /// </summary>
    public bool Disabled { get; set; }

    /// <summary>
    /// 扩展数据
    /// </summary>
    public T? Data { get; set; }
}

/// <summary>
/// 可选项返回模型
/// </summary>
public class OptionResultModel : OptionResultModel<object>
{
}




/// <summary>
/// 可选项返回模型
/// </summary>
public class AppOptionResultModel<T>
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Text { get; set; } = String.Empty;

    /// <summary>
    /// 值
    /// </summary>
    public object Value { get; set; } = String.Empty;

    /// <summary>
    /// 禁用
    /// </summary>
    public bool Disabled { get; set; }

    /// <summary>
    /// 扩展数据
    /// </summary>
    public T? Data { get; set; }
}

/// <summary>
/// 可选项返回模型
/// </summary>
public class AppOptionResultModel : AppOptionResultModel<object>
{
}
