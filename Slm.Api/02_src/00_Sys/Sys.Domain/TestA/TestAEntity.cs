using Slm.Data.Abstractions.Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.TestA;


[SugarTable("testa")]
[Tenant("1300000000002")]
public class TestAEntity: Entity
{
    /// <summary>
    /// 名称
    /// </summary>
    public string? Name { get; set; }
}
