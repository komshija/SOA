using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static DataMicroservice.Services.IMqttPublisher;
using Newtonsoft.Json;
using DataMicroservice.Configuration;
using Microsoft.Extensions.Options;

namespace DataMicroservice.Services
{
    public class MqttPublisher : IMqttPublisher
    {
        private readonly ILogger<MqttPublisher> _logger;
        private readonly Settings _settings;
        private IMqttClient mqttClient;

        public MqttPublisher(ILogger<MqttPublisher> logger, IOptions<Settings> settings)
        {
            _logger = logger;
            _settings = settings.Value;
            ConnectToMqtt().GetAwaiter().GetResult();
        }

      
        private async Task ConnectToMqtt()
        {
            IMqttFactory factory = new MqttFactory();
            mqttClient = factory.CreateMqttClient();
            IMqttClientOptions options = new MqttClientOptionsBuilder().WithTcpServer("mqtt", 1883).Build();
            while(!mqttClient.IsConnected)
            {
                try
                {
                    await mqttClient.ConnectAsync(options);
                }
                catch(Exception e)
                {
                    _logger.LogError(e.ToString());
                    await Task.Delay(TimeSpan.FromSeconds(10));
                }
            }
            if (mqttClient.IsConnected)
            {
                _logger.LogInformation("Connected to MQTT broker.");
            }
        }

        public async Task COMqttPublish(Data value)
        {
            await mqttClient.PublishAsync(new MqttApplicationMessageBuilder()
                .WithTopic(_settings.CO_TOPIC_MESSAGES)
                .WithPayload(JsonConvert.SerializeObject(value))
                .Build());
        }

        public async Task NO2MqttPublish(Data value)
        {
            await mqttClient.PublishAsync(new MqttApplicationMessageBuilder()
                 .WithTopic(_settings.NO2_TOPIC_MESSAGES)
                 .WithPayload(JsonConvert.SerializeObject(value))
                 .Build());
        }



    }
}
