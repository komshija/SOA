﻿using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataMicroservice.Clients
{
    public class RedisClient : IRedisClient
    {

        private readonly ConfigurationOptions configuration = null;
        private Lazy<IConnectionMultiplexer> _Connection = null;
        public IConnectionMultiplexer Connection { get { return _Connection.Value; } }
        public IDatabase Database => Connection.GetDatabase();
        private readonly ILogger<RedisClient> _logger;

        public RedisClient(ILogger<RedisClient> logger, string host = "localhost", int port = 6379, bool allowAdmin = false)
        {
            _logger = logger;
            configuration = new ConfigurationOptions()
            {
                EndPoints = { { host, port }, },
                AllowAdmin = allowAdmin,
                //Password = "", //to the security for the production
                ClientName = "Data Client",
                ReconnectRetryPolicy = new LinearRetry(5000),
                AbortOnConnectFail = false,
            };
            _Connection = new Lazy<IConnectionMultiplexer>(() =>
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(configuration);
                //redis.ErrorMessage += _Connection_ErrorMessage;
                //redis.InternalError += _Connection_InternalError;
                //redis.ConnectionFailed += _Connection_ConnectionFailed;
                //redis.ConnectionRestored += _Connection_ConnectionRestored;
                return redis;
            });
            logger.LogInformation("Redis connection established!");
        }

        public T JsonGet<T>(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None)
        {
            RedisValue value = Database.HashGet(key, hashField);
            if (!value.HasValue)
                return default;
            return JsonConvert.DeserializeObject<T>(value);
        }

        public bool JsonSet(RedisKey key, RedisValue hashField, object value, TimeSpan? expiry = null, When when = When.Always, CommandFlags flags = CommandFlags.None)
        {
            if (value == null)
                return false;
            return Database.HashSet(key, hashField, JsonConvert.SerializeObject(value), when, flags);
        }
    }
}