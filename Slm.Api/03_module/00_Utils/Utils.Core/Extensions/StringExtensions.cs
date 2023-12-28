using Slm.Utils.Core.Const;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Slm.Utils.Core.Extensions;

/// <summary>
/// 字符串扩展
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// 判断字符串是否为Null、空
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool IsNull(this string? s)
    {
        return string.IsNullOrWhiteSpace(s);
    }

    /// <summary>
    /// 判断字符串是否不为Null、空
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool NotNull(this string s)
    {
        return !string.IsNullOrWhiteSpace(s);
    }

    /// <summary>
    /// 与字符串进行比较，忽略大小写
    /// </summary>
    /// <param name="s"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool EqualsIgnoreCase(this string s, string value)
    {
        if (s == null)
            return false;
        return s.Equals(value, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 匹配字符串结尾，忽略大小写
    /// </summary>
    /// <param name="s"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool EndsWithIgnoreCase(this string s, string value)
    {
        return s.EndsWith(value, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 匹配字符串开头，忽略大小写
    /// </summary>
    /// <param name="s"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool StartsWithIgnoreCase(this string s, string value)
    {
        return s.StartsWith(value, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 首字母转小写
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string FirstCharToLower(this string s)
    {
        if (string.IsNullOrEmpty(s))
            return s;

        string str = s.First().ToString().ToLower() + s.Substring(1);
        return str;
    }

    /// <summary>
    /// 首字母转大写
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string FirstCharToUpper(this string s)
    {
        if (string.IsNullOrEmpty(s))
            return s;

        string str = s.First().ToString().ToUpper() + s.Substring(1);
        return str;
    }

    /// <summary>
    /// 转为Base64，UTF-8格式
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string? ToBase64(this string s)
    {
        return s.ToBase64(Encoding.UTF8);
    }

    /// <summary>
    /// 转为Base64
    /// </summary>
    /// <param name="s"></param>
    /// <param name="encoding">编码</param>
    /// <returns></returns>
    public static string? ToBase64(this string s, Encoding encoding)
    {
        if (s.IsNull())
            return string.Empty;

        var bytes = encoding.GetBytes(s);
        return bytes.ToBase64();
    }

    /// <summary>
    /// 转换为Base64
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string? ToBase64(this byte[] bytes)
    {
        if (bytes == null)
            return null;

        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// Base64解析
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string FromBase64(this string s)
    {
        byte[] data = Convert.FromBase64String(s);
        return Encoding.UTF8.GetString(data);
    }

    /// <summary>
    /// string字符串转集合
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static List<string> String2Arry(this string s)
    {
        return s.Split(',').ToList();
    }




    /// <summary>
    /// 切割骆驼命名式字符串
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string[] SplitCamelCase(this string str)
    {
        if (str == null) return Array.Empty<string>();

        if (string.IsNullOrWhiteSpace(str)) return new string[] { str };
        if (str.Length == 1) return new string[] { str };

        return Regex.Split(str, @"(?=\p{Lu}\p{Ll})|(?<=\p{Ll})(?=\p{Lu})")
            .Where(u => u.Length > 0)
            .ToArray();
    }


    /// <summary>
    /// 首字母小写
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ToLowerCamelCase(this string str)
    {
        if (string.IsNullOrWhiteSpace(str)) return str;

        return string.Concat(str.First().ToString().ToLower(), str.AsSpan(1).ToString());
    }


    /// <summary>
    /// 清除字符串前后缀
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="pos">0：前后缀，1：后缀，-1：前缀</param>
    /// <param name="affixes">前后缀集合</param>
    /// <returns></returns>
    public static string ClearStringAffixes(this string str, int pos = 0, params string[] affixes)
    {
        // 空字符串直接返回
        if (string.IsNullOrWhiteSpace(str)) return str;

        // 空前后缀集合直接返回
        if (affixes == null || affixes.Length == 0) return str;

        var startCleared = false;
        var endCleared = false;

        string? tempStr = null;
        foreach (var affix in affixes)
        {
            if (string.IsNullOrWhiteSpace(affix)) continue;

            if (pos != 1 && !startCleared && str.StartsWith(affix, StringComparison.OrdinalIgnoreCase))
            {
                tempStr = str[affix.Length..];
                startCleared = true;
            }
            if (pos != -1 && !endCleared && str.EndsWith(affix, StringComparison.OrdinalIgnoreCase))
            {
                var _tempStr = !string.IsNullOrWhiteSpace(tempStr) ? tempStr : str;
                tempStr = _tempStr[..^affix.Length];
                endCleared = true;
            }
            if (startCleared && endCleared) break;
        }

        return !string.IsNullOrWhiteSpace(tempStr) ? tempStr : str;
    }

    /// <summary>
    /// 输出日志内容
    /// </summary>
    /// <param name="mes"></param>
    /// <returns></returns>
    public static string ToLog(this string mes)
    {
        return $"{{{SerilogConst.Placeholder}}}:{mes}";
    }

    public static string ToPath(this string s)
    {
        if (s.IsNull())
            return string.Empty;

        return s.Replace(@"\", "/");
    }




    public static string ToRouteUrl(this string routeUrl)
    {
        string regex = @"\{.*?\}";
        if (Regex.IsMatch(routeUrl, regex))
        {
            var strs = routeUrl.Split('/').ToList();
            strs.RemoveAt(strs.Count()-1);
            return string.Join('/', strs);
        }
        return routeUrl;
    }

}