
using SqlSugar;
using Sys.Domain.Api;

namespace Sys.Domain.Menu;

public partial class MenuEntity
{
    /// <summary>
    /// 树行
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public List<MenuEntity> Children { get; set; }
}
