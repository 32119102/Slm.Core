using Slm.Data.Abstractions;
using Sys.Domain.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Api;



/// <summary>
/// 接口api仓储
/// </summary>
public interface IApiRepository : IBaseRepository<ApiEntity>
{
    /// <summary>
    /// 树形table
    /// </summary>
    /// <returns></returns>
    Task<List<ApiEntity>> TreeTable();


}
