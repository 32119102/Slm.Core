namespace Slm.Modularity.Abstractions;  

public interface IAppApplication : IModuleContainer
{
   /// <summary>
   /// 启动模块类型
   /// </summary>
    Type StartupModuleType { get; }


    /// <summary>
    /// 配置所需服务
    /// </summary>
    void ConfigureServices();

    /// <summary>
    /// 解析中间件
    /// </summary>
    void Initialize();


    /// <summary>
    /// 程序关闭触发事件
    /// </summary>
    void Shutdown();



}
