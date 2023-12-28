using Mapster;
namespace Slm.Mapster;

public interface IMapsterConfig
{
    /// <summary>
    /// 绑定
    /// </summary>
    /// <param name="cfg"></param>
    void Bind(TypeAdapterConfig cfg);
}
