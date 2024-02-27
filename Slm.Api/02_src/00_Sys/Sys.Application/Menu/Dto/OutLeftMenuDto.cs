using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sys.Application.Menu.Dto;

public class OutLeftMenuDto
{
    /// <summary>
    /// 主键里面声明的名称,如果是外链则给这个赋值
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// path随便写，但前面必须有个 `/`
    /// </summary>
    public string? Path { get; set; }

    /// <summary>
    /// 可不要
    ///  component对应的值前不需要加 / 值对应的是实际业务 `.vue` 或 `.tsx` 代码路径
    /// </summary>
    public string? Component { get; set; }

    public MetaDto Meta { get; set; }



    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<OutLeftMenuDto>? Children { get; set; }

}

public class MetaDto
{
    /// <summary>
    /// 图标
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string? Title { get; set; }


    /// <summary>
    /// 内嵌
    /// </summary>
    public string? FrameSrc { get; set; }

    /// <summary>
    /// 菜单排序
    /// </summary>
    public int? Rank { get; set; }

    /// <summary>
    /// 是否缓存
    /// </summary>
    public bool KeepAlive { get; set; }

    /// <summary>
    /// 是否显示父级菜单
    /// </summary>
    public bool ShowParent { get; set; } = true;

    /// <summary>
    /// 重定向(选填，如果不写则默认去第一个菜单)
    /// </summary>
    public string? Redirect { get; set; }

    public bool IsFrame { get; set; }
}
