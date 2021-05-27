using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataMicroservice.Services
{
    public interface IMqttPublisher
    {
        public record Data(string date, decimal value, string dataName);
        Task COMqttPublish(Data value);
        Task NO2MqttPublish(Data value);

    }
}
