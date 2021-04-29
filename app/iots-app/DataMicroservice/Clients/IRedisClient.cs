using System;
using StackExchange.Redis;

namespace DataMicroservice.Clients
{
    public interface IRedisClient
    {
        T JsonGet<T>(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None);
        bool JsonSet(RedisKey key, RedisValue hashField, object value, TimeSpan? expiry = null, When when = When.Always, CommandFlags flags = CommandFlags.None);
    }
}
