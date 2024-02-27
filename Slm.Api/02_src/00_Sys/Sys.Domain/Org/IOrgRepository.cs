using Slm.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Org;

public interface IOrgRepository : IBaseRepository<OrgEntity>
{
    /// <summary>
    /// 树形table
    /// </summary>
    /// <returns></returns>
    Task<List<OrgEntity>> TreeTable(long pid,Expression<Func<OrgEntity, bool>> whereExpression = null);


}
