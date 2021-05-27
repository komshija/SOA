using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CommandMicroservice.Services
{
    public class ActuatorClient
    {
        private readonly HttpClient _httpClient;

        public ActuatorClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async void PostOnCOActuator()
        {
            await _httpClient.PostAsync("http://co-sensor-microservice:80/actuator", new StringContent("Activate CO alarm"));
        }

        public async void PostOnNO2Actuator()
        {
            await _httpClient.PostAsync("http://no2-sensor-microservice:80/actuator", new StringContent("Activate NO2 alarm"));
        }
    }
}
