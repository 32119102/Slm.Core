
using Slm.Utils.Core.Helpers;

namespace Slm.Utils.Core.Extensions;

public static class SLDateTimeExtensions
{
    /// <summary>
    /// 日期格式化
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="format">默认：yyyy-MM-dd HH:mm:ss</param>
    /// <returns></returns>
    public static string Format(this DateTime dateTime, string? format = null)
    {
        if (format.IsNull())
            format = "yyyy-MM-dd HH:mm:ss";

        return dateTime.ToString(format);
    }

    /// <summary>
    /// 转换为时间戳
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="milliseconds">是否使用毫秒</param>
    /// <returns></returns>
    public static long ToTimestamp(this DateTime dateTime, bool milliseconds = true)
    {
        var ts = dateTime.ToUniversalTime() - DateTimeHelper.TimestampStart;
        return (long)(milliseconds ? ts.TotalMilliseconds : ts.TotalSeconds);
    }

    ///// <summary>
    ///// 获取周几
    ///// </summary>
    ///// <param name="datetime"></param>
    ///// <returns></returns>
    //public static WeekEnum GetWeek(this DateTime datetime)
    //{
    //    var dayOfWeek = datetime.DayOfWeek.ToInt();
    //    switch (dayOfWeek)
    //    {
    //        case 0:
    //            return WeekEnum.Sunday;
    //        case 1:
    //            return WeekEnum.Monday;
    //        case 2:
    //            return WeekEnum.Tuesday;
    //        case 3:
    //            return WeekEnum.Wednesday;
    //        case 4:
    //            return WeekEnum.Thursday;
    //        case 5:
    //            return WeekEnum.Friday;
    //        default:
    //            return WeekEnum.Saturday;
    //    }
    //}
}
