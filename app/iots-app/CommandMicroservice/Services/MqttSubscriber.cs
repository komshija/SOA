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
using CommandMicroservice.Configuration;
using Microsoft.Extensions.Options;

namespace CommandMicroservice.Services
{
    public class MqttSubscriber : IMqttSubscriber
    {

        private IMqttClient mqttClient;
        private readonly ActuatorClient _actuatorClient;
        private readonly ILogger<MqttSubscriber> _logger;
        private readonly INotificationService _notificationService;
        private readonly ConfigurationSettings _settings;

        public MqttSubscriber(ActuatorClient actuatorClient, ILogger<MqttSubscriber> logger, INotificationService notificationService, IOptions<ConfigurationSettings> settings)
        {
            _logger = logger;
            _actuatorClient = actuatorClient;
            _notificationService = notificationService;
            _settings = settings.Value;
            ConnectToMqtt().GetAwaiter().GetResult();

        }

        private async Task ConnectToMqtt()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
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
                    _logger.LogError(e.ToString());
                    _logger.LogInformation("Failed to connect to MQTT broker. Waiting 10 sec");
                    await Task.Delay(TimeSpan.FromSeconds(10));
                    _logger.LogInformation("Trying again to connect to MQTT broker");
                }
            }
            if (mqttClient.IsConnected)
            {
                _logger.LogInformation("Connected to MQTT broker.");
                await mqttClient.SubscribeAsync(_settings.CO_TOPIC_COMMAND);
                await mqttClient.SubscribeAsync(_settings.NO2_TOPIC_COMMAND);

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
            _actuatorClient.PostOnCOActuator("Activate CO alarm!");
            
        }

        public void NO2CommandMessageHandler(MqttApplicationMessage msg)
        {
            _logger.LogInformation("NO2 command received!");
            _notificationService.SendNotification("NO2 command activated.");
            _actuatorClient.PostOnNO2Actuator("Activate NO2 alarm!");
        }

        private int getTopicId(string topic)
        {
            if (topic.CompareTo(_settings.CO_TOPIC_COMMAND) == 0)
                return 0;
            if (topic.CompareTo(_settings.NO2_TOPIC_COMMAND) == 0)
                return 1;
            return -1;
        }
    }
}
