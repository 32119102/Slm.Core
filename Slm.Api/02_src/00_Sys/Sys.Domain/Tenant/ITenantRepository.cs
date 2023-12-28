using Slm.Data.Abstractions;
using Sys.Domain.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Tenant;


/// <summary>
/// 租户仓储
/// </summary>
public interface ITenantRepository : IBaseRepository<TenantEntity>
{



}
