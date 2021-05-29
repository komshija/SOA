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

        public async void PostOnCOActuator(string command)
        {
            try
            {
                await _httpClient.PostAsJsonAsync("http://co-sensor-microservice:80/actuator", command);
                _logger.LogInformation("===== Command sucessfuly activated on CO actuator! =====");
            }
            catch(Exception e)
            {
                _logger.LogInformation("Command failed to activate on CO actuator!");
            }
        }

        public async void PostOnNO2Actuator(string command)
        {
            try
            {
                await _httpClient.PostAsJsonAsync("http://no2-sensor-microservice:80/actuator", command);
                _logger.LogInformation("===== Command sucessfuly activated on NO2 actuator! =====");
            }
            catch(Exception e)
            {
                _logger.LogInformation("Command failed to activate on NO2 actuator!");
            }
        }
    }
}
