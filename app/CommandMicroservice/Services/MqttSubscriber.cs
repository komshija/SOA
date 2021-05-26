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

namespace CommandMicroservice.Services
{
    public class MqttSubscriber
    {
        private static string coTopic = "device/co/command";
        private static string no2Topic = "device/no2/command";
        private IMqttClient mqttClient;
        private readonly ActuatorClient _actuatorClient;

        public MqttSubscriber(ActuatorClient actuatorClient)
        {
            _actuatorClient = actuatorClient;
            Task.Run(() => ConnectToMqtt());

        }

        private async Task ConnectToMqtt()
        {
            IMqttFactory factory = new MqttFactory();
            mqttClient = factory.CreateMqttClient();
            IMqttClientOptions options = new MqttClientOptionsBuilder().WithTcpServer("mqtt", 1833).Build();
            await mqttClient.ConnectAsync(options, CancellationToken.None);
            if(mqttClient.IsConnected)
            {
                await mqttClient.SubscribeAsync(coTopic);
                await mqttClient.SubscribeAsync(no2Topic);

                mqttClient.UseApplicationMessageReceivedHandler(e =>
                {
                    string msgTopic = e.ApplicationMessage.Topic;
                    int topicId = getTopicId(msgTopic);
                    switch(topicId)
                    {
                        case 1:
                            COCommandMessageHandler(e.ApplicationMessage);
                            break;
                        case 2:
                            NO2CommandMessageHandler(e.ApplicationMessage);
                            break;
                        default:
                            break;
                    }
                    
                });

            }
        
        
        }


        private void COCommandMessageHandler(MqttApplicationMessage msg)
        {

        }

        private void NO2CommandMessageHandler(MqttApplicationMessage msg)
        {

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
