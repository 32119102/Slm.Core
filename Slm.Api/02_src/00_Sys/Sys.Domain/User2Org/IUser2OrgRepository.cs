using Slm.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.User2Org;



public interface IUser2OrgRepository : IBaseRepository<User2OrgEntity>
{

    /// <summary>
    /// 授权用户和组织关系
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="orgIds"></param>
    /// <returns></returns>
    Task GrantUserOrg(long userId, List<long> orgIds);

}

