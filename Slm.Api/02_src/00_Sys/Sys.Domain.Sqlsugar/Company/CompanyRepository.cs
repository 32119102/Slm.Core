using Slm.Data.Abstractions;
using Slm.Data.Core.Repository;
using SqlSugar;
using Sys.Domain.Company;

namespace Sys.Domain.Sqlsugar.Company;

/// <summary>
/// 企业仓储实现
/// </summary>
public class CompanyRepository : RepositoryAbstract<CompanyEntity>, ICompanyRepository
{

}
