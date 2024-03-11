using Slm.Utils.Core.Annotations;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.User2Org;


/// <summary>
/// 账号组织表
/// </summary>
[SugarTable("sys_user2org")]
[SysTable]
public class User2OrgEntity
{
    /// <summary>
    /// 用户id
    /// </summary>
    [SugarColumn(ColumnName = "UserId")]
    public long UserId { get; set; }


    /// <summary>
    /// 组织id
    /// </summary>
    [SugarColumn(ColumnName = "OrgId")]
    public long OrgId { get; set; }

}

