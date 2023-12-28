using FreeRedis;

namespace Slm.FreeRedis;

/// <summary>
/// redis缓存帮助
/// </summary>
public  class SlmRedisHelper
{

    private static  RedisClient _redisClient;
    public SlmRedisHelper(RedisClient redisClient)
    {
        _redisClient = redisClient;
    }

    ///// <summary>
    /////  获取字典名称
    ///// </summary>
    ///// <param name="key"></param>
    ///// <param name="field"></param>
    ///// <returns></returns>
    //public  static string GetDictName(string key,string field)
    //{
      
    //    return _redisClient.HGet($"{WjConst.DICT}:{key}", field);
    //}
}

