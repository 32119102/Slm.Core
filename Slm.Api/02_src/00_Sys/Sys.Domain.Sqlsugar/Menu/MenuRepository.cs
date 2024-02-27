using Slm.Data.Core.Repository;
using Sys.Domain.Api;
using Sys.Domain.Menu;
using Sys.Domain.Shared.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Sqlsugar.Menu;


/// <summary>
/// 菜单仓储实现
/// </summary>
public class MenuRepository : RepositoryAbstract<MenuEntity>, IMenuRepository
{

    /// <summary>
    /// 树形
    /// </summary>
    /// <returns></returns>
    public Task<List<MenuEntity>> TreeTable(Expression<Func<MenuEntity, bool>> whereExpression = null)
    {
        return AsQueryable().WhereIF(whereExpression != null, whereExpression).OrderBy(a => a.Sort).ToTreeAsync(a => a.Children, a => a.ParentId, 0);
    }



    //public Task<List<>> GetLeftMenuAsync() 
    //{

    //}

}

