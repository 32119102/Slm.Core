using Slm.Data.Abstractions.SqlSugar;
using Sys.Domain.Api;

namespace Sys.Domain.Sqlsugar.Api;

/// <summary>
/// 接口种子数据
/// </summary>
public class ApiSeedData : ISqlSugarEntitySeedData<ApiEntity>
{
    public IEnumerable<ApiEntity> HasData()
    {
        return new List<ApiEntity>() {
        new ApiEntity { Id = 15449102576709, ParentId = 0, Label = "基础管理", Path= "Sys",Sort=0,IsDelete=false,Enabled=true, Created = DateTime.Parse("2023-12-18 09:01:56"),Lastmodified = DateTime.Parse("2023-12-18 09:24:02") },
      };
    }
}
