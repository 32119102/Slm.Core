using Slm.Utils.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Org.Dto;

public class OutOrgTreeTableDto : TableDto
{
    public long Id { get; set; }


    /// <summary>
    /// 机构名称
    /// </summary>
    public string? Name { get; set; }


    /// <summary>
    /// 机构编码
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// 级别
    /// </summary>
    public int? Level { get; set; }



    /// <summary>
    /// 排序
    /// </summary>
    public int? Sort { get; set; }


    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnable { get; set; }


    public List<OutOrgTreeTableDto> Children { get; set; }
}
