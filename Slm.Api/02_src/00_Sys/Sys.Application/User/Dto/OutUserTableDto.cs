using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.User.Dto;

public class OutUserTableDto
{
    /// <summary>
    /// 头像
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// 账号
    /// </summary>
    public string? Account { get; set; }


    /// <summary>
    /// 昵称
    /// </summary>
    public string? RealName { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    public string? NickName { get; set; }


    /// <summary>
    /// 性别
    /// </summary>
    public string? SexName { get; set; }

    /// <summary>
    /// 年龄
    /// </summary>
    public int? Age { get; set; }


    /// <summary>
    /// 邮件
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string? Phone { get; set; }



    /// <summary>
    /// 排序
    /// </summary>
    public int? Sort { get; set; }

    /// <summary>
    /// 数据库连接
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnable { get; set; }

    /// <summary>
    /// 账号类型
    /// </summary>
    public string AccountTypeName { get; set; }

}
