using CommandMicroservice.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        private readonly ConfigurationSettings _settings;
        public ActuatorClient(HttpClient httpClient, ILogger<ActuatorClient> logger,IOptions<ConfigurationSettings> settings)
        {
            _httpClient = httpClient;
            _logger = logger;
            _settings = settings.Value;
        }

        public async void PostOnCOActuator(string command)
        {
            try
            {
                await _httpClient.PostAsJsonAsync(_settings.CO_ACTUATOR_URL, command);
                _logger.LogInformation("===== Command sucessfuly activated on CO actuator! =====");
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
                _logger.LogInformation("Command failed to activate on CO actuator!");
            }
        }

        public async void PostOnNO2Actuator(string command)
        {
            try
            {
                await _httpClient.PostAsJsonAsync(_settings.NO2_ACTUATOR_URL, command);
                _logger.LogInformation("===== Command sucessfuly activated on NO2 actuator! =====");
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
                _logger.LogInformation("Command failed to activate on NO2 actuator!");
            }
        }
    }
}
