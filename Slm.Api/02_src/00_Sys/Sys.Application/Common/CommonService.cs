using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slm.DynamicApi;
using Slm.DynamicApi.Attributes;
using Slm.Utils.Core.Extensions;
using Slm.Utils.Core.Models;
using Sys.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Common;

/// <summary>
/// 认证服务
/// </summary>
[DynamicApi(Area = SsyAreaConst.Area)]
[AllowAnonymous]
[Order(0)]
public class CommonService : IDynamicApi
{
    /// <summary>
    /// 获取枚举中选项列表
    /// </summary>
    /// <param name="moduleCode"></param>
    /// <param name="enumName"></param>
    /// <returns></returns>
    [HttpGet]
    public List<OptionResultModel> EnumOptions(string moduleCode, string enumName)
    {
        var module = ModuleEnumDescriptorCollection._optionResultModels.Where(a => a.ModuleName.EqualsIgnoreCase(moduleCode) && a.Name.EqualsIgnoreCase(enumName))
            .Select(a => a.Options).FirstOrDefault();
        if (module == null)
        {
            return new List<OptionResultModel>();
        }
        return module.ToList();
    }


}