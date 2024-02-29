using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Api.Dto;

public class InApiDto
{

    /// <summary>
    /// 父级id
    /// </summary>
    public long? ParentId { get; set; } = 0;



    /// <summary>
    /// 标签
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    public string? Path { get; set; }


    /// <summary>
    /// http类型
    /// </summary>
    public string? HttpMethods { get; set; }



    /// <summary>
    /// 排序
    /// </summary>
    public int? Sort { get; set; }


    /// <summary>
    /// 启用
    /// </summary>
    public bool Enabled { get; set; } = true;

}
