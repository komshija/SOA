using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Subscribing;
using MQTTnet.Client.Receiving;
using MQTTnet.Client.Options;
using Microsoft.Extensions.Logging;
using System.Text;
using CommandMicroservice.Hubs;

namespace CommandMicroservice.Services
{
    public class MqttSubscriber : IMqttSubscriber
    {
        private static string coTopic = "device/co/command";
        private static string no2Topic = "device/no2/command";
        private IMqttClient mqttClient;
        private readonly ActuatorClient _actuatorClient;
        private readonly ILogger<MqttSubscriber> _logger;
        private readonly INotificationService _notificationService;

        public MqttSubscriber(ActuatorClient actuatorClient, ILogger<MqttSubscriber> logger, INotificationService notificationService)
        {
            _logger = logger;
            _actuatorClient = actuatorClient;
            _notificationService = notificationService;
            ConnectToMqtt().GetAwaiter().GetResult();

        }

        private async Task ConnectToMqtt()
        {
            IMqttFactory factory = new MqttFactory();
            mqttClient = factory.CreateMqttClient();
            IMqttClientOptions options = new MqttClientOptionsBuilder().WithTcpServer("mqtt", 1883).Build();
            while (!mqttClient.IsConnected)
            {
                try
                {
                    await mqttClient.ConnectAsync(options);
                }
                catch (Exception e)
                {
                    _logger.LogInformation("Failed to connect to MQTT broker. Waiting 10 sec");
                    await Task.Delay(TimeSpan.FromSeconds(10));
                }
            }
            if (mqttClient.IsConnected)
            {
                _logger.LogInformation("Connected to MQTT broker.");
                await mqttClient.SubscribeAsync(coTopic);
                await mqttClient.SubscribeAsync(no2Topic);

                mqttClient.UseApplicationMessageReceivedHandler(e =>
                {
                    string msgTopic = e.ApplicationMessage.Topic;
                    int topicId = getTopicId(msgTopic);
                    switch(topicId)
                    {
                        case 0:
                            COCommandMessageHandler(e.ApplicationMessage);
                            break;
                        case 1:
                            NO2CommandMessageHandler(e.ApplicationMessage);
                            break;
                        default:
                            break;
                    }
                });

            }
        }

        public void COCommandMessageHandler(MqttApplicationMessage msg)
        {
            _logger.LogInformation("CO command received!");
            _notificationService.SendNotification("CO command activated.");
            _actuatorClient.PostOnCOActuator();
            
        }

        public void NO2CommandMessageHandler(MqttApplicationMessage msg)
        {
            _logger.LogInformation("NO2 command received!");
            _notificationService.SendNotification("NO2 command activated.");
            _actuatorClient.PostOnNO2Actuator();
        }

        private int getTopicId(string topic)
        {
            if (topic.CompareTo(coTopic) == 0)
                return 0;
            if (topic.CompareTo(no2Topic) == 0)
                return 1;
            return -1;
        }
    }
}
