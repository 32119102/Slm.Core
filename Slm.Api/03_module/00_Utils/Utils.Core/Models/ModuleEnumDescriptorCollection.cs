using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slm.Utils.Core.Models;


/// <summary>
/// 数据集合配置
/// </summary>
public class ModuleEnumDescriptorCollection : IReadOnlyCollection<ModuleEnumDescriptor>
{
    public static List<ModuleEnumDescriptor> _optionResultModels;

    static ModuleEnumDescriptorCollection()
    {
        _optionResultModels = new List<ModuleEnumDescriptor>();
    }
    #region  需要完成的接口
    public int Count => _optionResultModels.Count;

    public IEnumerator<ModuleEnumDescriptor> GetEnumerator()
    {
        foreach (var item in _optionResultModels)
        {
            yield return item;
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #endregion


    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="dbOptions"></param>
    /// <returns></returns>
    public static void Add(ModuleEnumDescriptor dbOptions)
    {
        _optionResultModels.Add(dbOptions);
    }


    /// <summary>
    /// 移除
    /// </summary>
    /// <param name="dbOptions"></param>
    /// <returns></returns>
    public static bool Remove(ModuleEnumDescriptor dbOptions)
    {
        return _optionResultModels.Remove(dbOptions);
    }


}
