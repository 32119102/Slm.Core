using Slm.Data.Abstractions.Entities;
using Slm.Utils.Core.Annotations;
using SqlSugar;
using Sys.Domain.Shared.Menu;

namespace Sys.Domain.Menu;


/// <summary>
/// 租户表
/// </summary>
[SugarTable("sys_menu")]
[SysTable]
public partial class MenuEntity : EntityBase<long>
{
    /// <summary>
    /// 父级id
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 路径
    /// </summary>
    public string? Path { get; set; }


    /// <summary>
    /// 编码(唯一值)
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 组件
    /// </summary>
    public string? Component { get; set; }

    /// <summary>
    /// 类型(0.目录;1.菜单;2.按钮;)
    /// </summary>
    public MenuTypeEnum? Type { get; set; }

    /// <summary>
    /// 回调地址(用于前端面包屑)
    /// </summary>
    public string? Redirect { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int? Sort { get; set; }

    /// <summary>
    /// 隐藏菜单
    /// </summary>
    public bool IsHide { get; set; }

    /// <summary>
    /// 是否外链
    /// </summary>
    public bool IsFrame { get; set; }
    

    /// <summary>
    /// 链接地址
    /// </summary>
    public string? FrameSrc { get; set; }


    /// <summary>
    /// 是否页面缓存
    /// </summary>
    public bool IsKeepAlive { get; set; }

    /// <summary>
    /// 是否固定
    /// </summary>
    public bool IsAffix { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnable { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 按钮编码
    /// </summary>
    public string? ButtonCode { get; set; }

}
