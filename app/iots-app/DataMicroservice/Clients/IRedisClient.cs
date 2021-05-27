using System;
using System.Collections.Generic;
using DataMicroservice.Controllers;
using StackExchange.Redis;

namespace DataMicroservice.Clients
{
    public interface IRedisClient
    {
        List<KeyValuePair<string, DataController.Data>> JsonGet(RedisKey key, CommandFlags flags = CommandFlags.None);
        bool JsonSet(RedisKey key, RedisValue hashField, object value, TimeSpan? expiry = null, When when = When.Always, CommandFlags flags = CommandFlags.None);
    }
}
