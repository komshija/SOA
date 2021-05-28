using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ActuatorClient> _logger;
        public ActuatorClient(HttpClient httpClient, ILogger<ActuatorClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async void PostOnCOActuator()
        {
            try
            {
                await _httpClient.PostAsync("http://co-sensor-microservice:80/actuator", new StringContent("Activate CO alarm"));
                _logger.LogInformation("===== Command sucessfuly activated CO alarm! =====");
            }
            catch(Exception e)
            {
                _logger.LogInformation("Command failed to Activate CO alarm!");
            }
        }

        public async void PostOnNO2Actuator()
        {
            try
            {
                await _httpClient.PostAsync("http://no2-sensor-microservice:80/actuator", new StringContent("Activate NO2 alarm"));
                _logger.LogInformation("===== Command sucessfuly activated NO2 alarm! =====");
            }
            catch(Exception e)
            {
                _logger.LogInformation("Command failed to Activate NO2 alarm!");
            }
        }
    }
}
