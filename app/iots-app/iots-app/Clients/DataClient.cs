using DeviceMicroservice.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using DeviceMicroservice.Models;

namespace DeviceMicroservice.Clients
{
    public class DataClient
    {
        private readonly HttpClient _httpClient;
        private readonly DataClientSettings _clientSettings;
        private readonly ILogger<DataClient> _logger;

        public DataClient(HttpClient httpClient, IOptions<DataClientSettings> options,ILogger<DataClient> logger)
        {
            _httpClient = httpClient;
            _clientSettings = options.Value;
            _logger = logger;
        }

        public async Task PostOnDataClientAsync(SensorData data)
        {
            await _httpClient.PostAsJsonAsync<SensorData>(_clientSettings.HostName,data);
        }

    }
}
