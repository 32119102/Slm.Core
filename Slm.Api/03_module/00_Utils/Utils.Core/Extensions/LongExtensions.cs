namespace Slm.Utils.Core.Extensions;

public static class LongExtensions
{
    /// <summary>
    /// long集合转string集合
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static List<string> ToListString(this List<long> list) 
    {
        return list.Select(a => a.ToString()).ToList();
    }



}
