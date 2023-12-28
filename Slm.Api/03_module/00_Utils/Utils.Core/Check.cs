using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slm.Utils.Core;


/// <summary>
/// 检查
/// </summary>
[DebuggerStepThrough]
public static class Check
{
    public static T NotNull<T>(T value, string parameterName)
    {
        if (value == null)
        {
            throw new ArgumentNullException(parameterName);
        }

        return value;
    }

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

    public static List<T> SortByDependencies<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies, IEqualityComparer<T>? comparer = null)
    {
        /* See: http://www.codeproject.com/Articles/869059/Topological-sorting-in-Csharp
         *      http://en.wikipedia.org/wiki/Topological_sorting
         */

        var sorted = new List<T>();
        var visited = new Dictionary<T, bool>(comparer);

        foreach (var item in source)
        {
            SortByDependenciesVisit(item, getDependencies, sorted, visited);
        }

        return sorted;
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T">The type of the members of values.</typeparam>
    /// <param name="item">Item to resolve</param>
    /// <param name="getDependencies">Function to resolve the dependencies</param>
    /// <param name="sorted">List with the sortet items</param>
    /// <param name="visited">Dictionary with the visited items</param>
    private static void SortByDependenciesVisit<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<T> sorted,
        Dictionary<T, bool> visited)
    {
        bool inProcess;
        var alreadyVisited = visited.TryGetValue(item, out inProcess);

        if (alreadyVisited)
        {
            if (inProcess)
            {
                throw new ArgumentException("Cyclic dependency found! Item: " + item);
            }
        }
        else
        {
            visited[item] = true;

            var dependencies = getDependencies(item);
            if (dependencies != null)
            {
                foreach (var dependency in dependencies)
                {
                    SortByDependenciesVisit(dependency, getDependencies, sorted, visited);
                }
            }

            visited[item] = false;
            sorted.Add(item);
        }
    }

    public static void MoveItem<T>(this List<T> source, Predicate<T> selector, int targetIndex)
    {
        if (!targetIndex.IsBetween(0, source.Count - 1))
        {
            throw new IndexOutOfRangeException("targetIndex should be between 0 and " + (source.Count - 1));
        }

        var currentIndex = source.FindIndex(0, selector);
        if (currentIndex == targetIndex)
        {
            return;
        }

        var item = source[currentIndex];
        source.RemoveAt(currentIndex);
        source.Insert(targetIndex, item);
    }

    /// <summary>
    /// Checks a value is between a minimum and maximum value.
    /// </summary>
    /// <param name="value">The value to be checked</param>
    /// <param name="minInclusiveValue">Minimum (inclusive) value</param>
    /// <param name="maxInclusiveValue">Maximum (inclusive) value</param>
    public static bool IsBetween<T>(this T value, T minInclusiveValue, T maxInclusiveValue) where T : IComparable<T>
    {
        return value.CompareTo(minInclusiveValue) >= 0 && value.CompareTo(maxInclusiveValue) <= 0;
    }

    public static T GetSingletonInstanceOrNull<T>(this IServiceCollection services)
    {
        //选择是!在右侧添加 null 宽容运算符：
        return (T)services
            .FirstOrDefault(d => d.ServiceType == typeof(T))
            ?.ImplementationInstance!;
    }

    public static T GetSingletonInstance<T>(this IServiceCollection services)
    {
        var service = services.GetSingletonInstanceOrNull<T>();
        if (service == null)
        {
            throw new InvalidOperationException("Could not find singleton service: " + typeof(T).AssemblyQualifiedName);
        }

        return service;
    }
}
