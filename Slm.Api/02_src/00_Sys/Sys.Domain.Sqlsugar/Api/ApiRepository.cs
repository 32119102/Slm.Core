
using Slm.Data.Core.Repository;
using SqlSugar;
using Sys.Domain.Api;
using Sys.Domain.Tenant;

namespace Sys.Domain.Sqlsugar.Api;

/// <summary>
/// 接口API仓储实现
/// </summary>
public class ApiRepository : RepositoryAbstract<ApiEntity>, IApiRepository
{


    public Task<List<ApiEntity>> TreeTable()
    {
        return AsQueryable().OrderBy(a => a.Sort).ToTreeAsync(a => a.Children, a => a.ParentId, 0);
    }


}

