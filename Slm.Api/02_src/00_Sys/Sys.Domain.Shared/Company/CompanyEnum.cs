using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Shared.Company;

/// <summary>
/// 企业状态
/// </summary>
public enum CompanyStatusEnum {

    /// <summary>
    /// 未知
    /// </summary>
    [Description("未知")]
    UnKnown = -1,
    /// <summary>
    /// 禁用
    /// </summary>
    [Description("禁用")]
    Disable = 0,
    /// <summary>
    /// 启用
    /// </summary>
    [Description("启用")]
    Enable = 1,

}

