namespace Slm.Utils.Core.Models;

/// <summary>
/// 返回Dto
/// </summary>
public abstract class TableDto
{
    /// <summary>
    /// 创建人名称
    /// </summary>
    public string? Creator { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? Created { get; set; }


    /// <summary>
    /// 修改人名称
    /// </summary>
    public string? Lastmodifier { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    public DateTime? Lastmodified { get; set; }
}
