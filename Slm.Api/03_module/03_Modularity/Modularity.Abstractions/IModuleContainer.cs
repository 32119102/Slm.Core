namespace Slm.Modularity.Abstractions;

public interface IModuleContainer
{
    /// <summary>
    /// 获取模块
    /// </summary>
    IReadOnlyList<IAppModuleDescriptor> Modules { get; }
}
