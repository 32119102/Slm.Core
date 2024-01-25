using Slm.Data.Abstractions;
using Sys.Domain.Api;
using System.Linq.Expressions;

namespace Sys.Domain.Menu;



/// <summary>
/// 菜单仓储
/// </summary>
public interface IMenuRepository : IBaseRepository<MenuEntity>
{
    /// <summary>
    /// 树形table
    /// </summary>
    /// <returns></returns>
    Task<List<MenuEntity>> TreeTable(Expression<Func<MenuEntity, bool>> whereExpression = null);


}
