namespace Slm.Utils.Core.Annotations;

/// <summary>
/// 条件查询特性
/// 传入有值则加入查询条件
/// </summary>
public class SearchAttribute : Attribute
{
    /// <summary>
    /// 字段名
    /// 如果不设置默认取查询的字段名
    /// </summary>
    public string FieldName { get; set; }=String.Empty;

    /// <summary>
    /// 条件类型
    /// </summary>
    public SlmConditionalType ConditionalType { get; set; }
}

public enum SlmConditionalType
{
    Equal,
    Like,
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual,
    In,
    NotIn,
    LikeLeft,
    LikeRight,
    NoEqual,
    IsNullOrEmpty,
    IsNot,
    NoLike,
    EqualNull,
    InLike
}
