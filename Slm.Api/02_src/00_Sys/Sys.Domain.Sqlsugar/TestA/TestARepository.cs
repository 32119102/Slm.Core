using Slm.Data.Core.Repository;
using SqlSugar;
using Sys.Domain.Api;
using Sys.Domain.TestA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Sqlsugar.TestA;

public class TestARepository : RepositoryAbstract<TestAEntity>, ITestARepository
{

    public async Task<ConnectionConfig> Contxt()
    {
        var dd =await Context.Queryable<TestAEntity>().ToListAsync();

        return base.Context.CurrentConnectionConfig;
    }
}
