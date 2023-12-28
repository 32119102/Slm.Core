using Microsoft.Extensions.Logging;
using Slm.Data.Abstractions;
using Slm.Utils.Core.Const;
using Slm.Utils.Core.Extensions;
using SqlSugar;

namespace Slm.Data.Core;

public class Tran : ITran
{
    private readonly ISqlSugarClient _sqlSugarClient;
    private readonly ILogger<Tran> _logger;

    public Tran(ISqlSugarClient sqlSugarClient, ILogger<Tran> logger)
    {
        _sqlSugarClient = sqlSugarClient;
        _logger = logger;
    }


    public void BeginTran()
    {
        _sqlSugarClient.AsTenant().BeginTran();
    }

    public async Task BeginTranAsync()
    {
        await _sqlSugarClient.AsTenant().BeginTranAsync();
    }

    public void CommitTran()
    {
        try
        {
            _sqlSugarClient.AsTenant().CommitTran();
        }
        catch (Exception ex)
        {
            _sqlSugarClient.AsTenant().RollbackTran();
            _logger.LogError("{str}".ToLog(), SerilogConst.Error, $"{ex.Message}\r\n{ex.InnerException}");
            //_logger.LogError($"{ex.Message}\r\n{ex.InnerException}");
        }
    }

    public async Task CommitTranAsync()
    {
        try
        {
            await _sqlSugarClient.AsTenant().CommitTranAsync();
        }
        catch (Exception ex)
        {
            await _sqlSugarClient.AsTenant().RollbackTranAsync();
            _logger.LogError("{str}".ToLog(), SerilogConst.Error, $"{ex.Message}\r\n{ex.InnerException}");
            //_logger.LogError($"{ex.Message}\r\n{ex.InnerException}");
        }

    }

    public void RollbackTran()
    {
        _sqlSugarClient.AsTenant().RollbackTran();
    }

    public async Task RollbackTranAsync()
    {
        await _sqlSugarClient.AsTenant().RollbackTranAsync();
    }
}


