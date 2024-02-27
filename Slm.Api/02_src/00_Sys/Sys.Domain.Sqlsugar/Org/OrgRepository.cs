using Slm.Data.Core.Repository;
using Sys.Domain.Org;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Sqlsugar.Org;

/// <summary>
/// 机构仓储实现
/// </summary>
public class OrgRepository : RepositoryAbstract<OrgEntity>, IOrgRepository
{

    /// <summary>
    /// 树形
    /// </summary>
    /// <returns></returns>
    public Task<List<OrgEntity>> TreeTable(long pid,Expression<Func<OrgEntity, bool>> whereExpression = null)
    {
        return AsQueryable().WhereIF(whereExpression != null, whereExpression).OrderBy(a => a.Sort).ToTreeAsync(a => a.Children, a => a.Pid, pid);
    }


}
