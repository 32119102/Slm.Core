using Slm.Utils.Core.DependencyInjection;

namespace Slm.Utils.Core.Helpers;

public class DateTimeHelper : ISingletonDependency
{
    /// <summary>
    /// 时间戳起始日期
    /// </summary>
    public static readonly DateTime TimestampStart = new DateTime(1970, 1, 1, 0, 0, 0, 0);

    /// <summary>
    /// 获取时间戳
    /// </summary>
    /// <param name="milliseconds">是否使用毫秒</param>
    /// <returns></returns>
    public string GetTimestamp(bool milliseconds = true)
    {
        var ts = DateTime.UtcNow - TimestampStart;
        return Convert.ToInt64(milliseconds ? ts.TotalMilliseconds : ts.TotalSeconds).ToString();
    }

    /// <summary>
    /// 时间戳转日期
    /// </summary>
    /// <param name="timestamp">时间戳</param>
    /// <param name="milliseconds">是否使用毫秒</param>
    /// <returns></returns>
    public DateTime Timestamp2DateTime(long timestamp, bool milliseconds = true)
    {
        var val = milliseconds ? 10000 : 10000000;
        return TimestampStart.AddTicks(timestamp * val);
    }

    /// <summary>判断当前年份是否是闰年</summary>
    /// <param name="year">年份</param>
    /// <returns></returns>
    private bool IsLeapYear(int year)
    {
        int n = year;
        if ((n % 400 == 0) || (n % 4 == 0 && n % 100 != 0))
        {
            return true;
        }
        return false;
    }

   
}
