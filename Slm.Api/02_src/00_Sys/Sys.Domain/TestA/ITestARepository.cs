using Slm.Data.Abstractions;
using SqlSugar;
using Sys.Domain.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.TestA;

public interface ITestARepository : IBaseRepository<TestAEntity>
{

    Task<ConnectionConfig> Contxt();


}
