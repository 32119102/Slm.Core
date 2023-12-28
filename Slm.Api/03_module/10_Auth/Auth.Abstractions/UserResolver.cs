
using Slm.Utils.Core;
using Slm.Utils.Core.Const;
using Slm.Utils.Core.Extensions;
using Slm.Utils.Core.Helpers;

namespace Slm.Auth.Abstractions;

public class UserResolver
{
    /// <summary>
    /// 租户
    /// </summary>
    public static int? TenantId
    {
        get
        {
            if (App.User == null)
                return null;
            var tenantId = App.User!.FindFirst(SlmClaimTypes.TenantId);

            if (tenantId != null && tenantId.Value.NotNull())
            {
                return tenantId.Value.To<int>();
            }
            return null;
        }
    }

    /// <summary>
    /// 账户编号
    /// </summary>
    public static long? UserId
    {
        get
        {
            if (App.User == null)
                return null;
            var accountId = App.User!.FindFirst(SlmClaimTypes.UserId);

            if (accountId != null && accountId.Value.NotNull())
            {
                return accountId.Value.To<long>();
            }

            return null;
        }
    }

    /// <summary>
    /// 登录人（UserCode）
    /// </summary>
    public static string UserName
    {
        get
        {
            if (App.User == null)
                return "";
            var accountName = App.User!.FindFirst(SlmClaimTypes.UserName);

            if (accountName == null || accountName.Value.IsNull())
            {
                return "";
            }

            return accountName.Value;
        }
    }


    public static AccountTypeEnum? AccountType
    {
        get
        {
            if (App.User == null)
                return null;
            var accountType = App.User!.FindFirst(SlmClaimTypes.AccountType);
            if (accountType == null || accountType.Value.IsNull())
            {
                return null;
            }
            return accountType.Value.To<AccountTypeEnum>();


        }

    }


    /// <summary>
    /// 是否超级管理员
    /// </summary>
    public static bool IsPadmin
    {
        get
        {
            if (App.User == null)
                return false;
            var accountName = App.User!.FindFirst(SlmClaimTypes.Padmin);
            if (accountName == null || accountName.Value.IsNull())
            {
                return false;
            }
            return accountName.Value.ToBool();


        }
    }




    /// <summary>
    /// 角色集合
    /// </summary>
    public static List<long> Roles
    {
        get
        {
            var accountName = App.User!.FindFirst(SlmClaimTypes.Roles);

            if (accountName == null || accountName.Value.IsNull())
            {
                return new List<long>();
            }
            return accountName.Value.Split(',').Select(a => a.To<long>()).ToList();
        }
    }



    /// <summary>
    /// 获取当前用户IP(包含IPv和IPv6)
    /// </summary>
    public static string IP
    {
        get
        {
            return IPHelper.GetIP(App.HttpContext.Request);
        }
    }
}
