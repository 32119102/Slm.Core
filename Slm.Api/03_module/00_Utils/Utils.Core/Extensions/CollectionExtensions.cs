
namespace Slm.Utils.Core.Extensions;

public static class CollectionExtensions
{
    /// <summary>
    /// 添加一个元素若是元素不存在
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    public static bool AddIfNotContains<T>(this ICollection<T> source, T item)
    {
        if (source.Contains(item))
        {
            return false;
        }
        source.Add(item);
        return true;
    }


    public static bool IsNull<T>(this ICollection<T> source)
    {
        return source == null && source!.Count <= 0;
    }

    public static bool NotNull<T>(this ICollection<T> source)
    {
        if (source != null && source.Count > 0)
        {
            var i = source.Where(a => !a.IsNull()).ToList();
            return i.Count() > 0;
        }
        return false;
    }

    public static string ToJoin<T>(this ICollection<T> source, char split = ',')
    {
        return string.Join(",", source);
    }

    public static string ToJoin<T>(this IEnumerable<T> source, char split = ',')
    {
        return string.Join(",", source);
    }



}
