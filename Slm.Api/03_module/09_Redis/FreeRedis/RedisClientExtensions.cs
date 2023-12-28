using FreeRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Slm.FreeRedis
{
    public static class RedisClientExtensions
    {
        public static string GetPrefix(this RedisClient client)
        {
            return typeof(RedisClient).GetProperty("Prefix", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.GetValue(client) as string;
        }
    }
}
