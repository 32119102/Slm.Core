using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slm.Data.Core;



/// <summary>
/// 数据过滤器
/// </summary>
public class SqlSugarFilter
{
    /// <summary>
    /// 删除用户机构缓存
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="dbConfigId"></param>
    public 
        static void DeleteUserOrgCache(long userId, string dbConfigId)
    {

    }

    /// <summary>
    /// 配置用户机构集合过滤器
    /// </summary>
    public static void SetOrgEntityFilter(SqlSugarScopeProvider db)
    {


    }

    /// <summary>
    /// 配置用户仅本人数据过滤器
    /// </summary>
    private static int SetDataScopeFilter(SqlSugarScopeProvider db)
    {
        return 0;
    }


    /// <summary>
    /// 配置自定义过滤器
    /// </summary>
    public static void SetCustomEntityFilter(SqlSugarScopeProvider db)
    {

    }

}



/// <summary>
/// 自定义实体过滤器接口
/// </summary>
public interface IEntityFilter
{
    /// <summary>
    /// 实体过滤器
    /// </summary>
    /// <returns></returns>
    IEnumerable<TableFilterItem<object>> AddEntityFilter();
}