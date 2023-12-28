using Slm.Utils.Core.Abstracts;

namespace Slm.Utils.Core.Models;

/// <summary>
/// 选项集合模型
/// </summary>
public class OptionCollectionResultModel<T> : CollectionAbstract<OptionResultModel<T>>
{
}

/// <summary>
/// 选项集合模型
/// </summary>
public class OptionCollectionResultModel : OptionCollectionResultModel<object>
{

}

