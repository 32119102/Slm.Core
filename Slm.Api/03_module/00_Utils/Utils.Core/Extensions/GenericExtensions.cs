namespace Slm.Utils.Core.Extensions;

public static class GenericExtensions
{
    /// <summary>
    /// 判断对象是否为空，为空返回true
    /// </summary>
    /// <typeparam name="T">要验证的对象的类型</typeparam>
    /// <param name="data">要验证的对象</param>
    public static bool IsNull<T>(this T data)
    {
        //如果为null
        if (data == null)
        {
            return true;
        }

        //如果为""
        if (data.GetType() == typeof(String))
        {
            if (string.IsNullOrEmpty(data.ToString().Trim()) || data.ToString() == "")
            {
                return true;
            }
        }

        //如果为DBNull
        if (data.GetType() == typeof(DBNull))
        {
            return true;
        }

        //不为空
        return false;
    }

}