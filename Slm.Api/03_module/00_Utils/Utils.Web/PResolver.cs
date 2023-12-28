using Slm.Utils.Core;

namespace Slm.Utils.Web;

public class PResolver
{
    /// <summary>
    /// 获取当前用户IP(包含IPv和IPv6)
    /// </summary>
    public static string IP
    {
        get
        {
            if (App.HttpContext?.Connection.RemoteIpAddress == null)
                return "";

            return App.HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }

    /// <summary>
    /// 获取当前用户IPv4
    /// </summary>
    public static string IPv4
    {
        get
        {
            if (App.HttpContext?.Connection.RemoteIpAddress == null)
                return "";

            return App.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }

    /// <summary>
    /// 获取当前用户IPv6
    /// </summary>
    public static string IPv6
    {
        get
        {
            if (App.HttpContext?.Connection.RemoteIpAddress == null)
                return "";

            return App.HttpContext.Connection.RemoteIpAddress.MapToIPv6().ToString();
        }
    }

    /// <summary>
    /// 获取当前用户请求的User-Agent
    /// </summary>
    public static string UserAgent
    {
        get
        {
            if (App.HttpContext?.Request == null)
                return "";

            return App.HttpContext.Request.Headers["User-Agent"];
        }
    }
}
