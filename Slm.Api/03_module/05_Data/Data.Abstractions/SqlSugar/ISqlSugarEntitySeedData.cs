using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slm.Data.Abstractions.SqlSugar;


/// <summary>
/// 实体种子数据接口
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface ISqlSugarEntitySeedData<TEntity>
    where TEntity : class, new()
{
    /// <summary>
    /// 种子数据
    /// </summary>
    /// <returns></returns>
    IEnumerable<TEntity> HasData();
}
