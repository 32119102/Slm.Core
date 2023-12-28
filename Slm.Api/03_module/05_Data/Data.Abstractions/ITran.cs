using SqlSugar;

namespace Slm.Data.Abstractions;

/// <summary>
/// 事务
/// </summary>
public interface ITran
{
    void BeginTran();

    void CommitTran();
    void RollbackTran();


    Task BeginTranAsync();

    Task CommitTranAsync();
    Task RollbackTranAsync();
}
