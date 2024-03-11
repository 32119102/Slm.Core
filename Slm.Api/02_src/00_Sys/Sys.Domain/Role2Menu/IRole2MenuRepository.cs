using Slm.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Role2Menu;


public interface IRole2MenuRepository : IBaseRepository<Role2MenuEntity>
{

    /// <summary>
    /// 授权角色和菜单关系
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="menuIds"></param>
    /// <returns></returns>
    Task GrantRoleMenu(long roleId, List<long> menuIds);

}

