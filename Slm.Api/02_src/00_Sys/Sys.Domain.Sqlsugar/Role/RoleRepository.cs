using Slm.Data.Core.Repository;
using Sys.Domain.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Sqlsugar.Role;



/// <summary>
/// 机构仓储实现
/// </summary>
public class RoleRepository : RepositoryAbstract<RoleEntity>, IRoleRepository
{
    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<RoleEntity> GetInfo(long id)
    {
        return AsQueryable().Includes(a => a.Role2MenuEntities).InSingleAsync(id);
    }


    public Task<RoleEntity> GetRoleAndOrg(long id)
    {
        return AsQueryable().Includes(a => a.Role2OrgEntities).InSingleAsync(id);
    }
}
