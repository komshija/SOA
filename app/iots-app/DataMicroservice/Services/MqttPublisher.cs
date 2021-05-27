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

namespace DataMicroservice.Services
{
    public class MqttPublisher : IMqttPublisher
    {
        private static string coTopic = "device/co/messages";
        private static string no2Topic = "device/no2/messages";
        

        private readonly ILogger<MqttPublisher> _logger;
        private IMqttClient mqttClient;

        public MqttPublisher(ILogger<MqttPublisher> logger)
        {
            _logger = logger;
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
                .WithTopic(coTopic)
                .WithPayload(JsonConvert.SerializeObject(value))
                .Build());
        }

        public async Task NO2MqttPublish(Data value)
        {
            await mqttClient.PublishAsync(new MqttApplicationMessageBuilder()
                 .WithTopic(coTopic)
                 .WithPayload(JsonConvert.SerializeObject(value))
                 .Build());
        }



    }
}
