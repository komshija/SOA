using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandMicroservice.Services
{
    public interface IMqttSubscriber
    {
        void COCommandMessageHandler(MqttApplicationMessage msg);
        void NO2CommandMessageHandler(MqttApplicationMessage msg);
    }
}
