using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Tasks;

namespace DeviceShared
{
    public class DataClient
    {
        private readonly HttpClient _httpClient;
        private readonly DataClientSettings _clientSettings;
        private readonly ILogger<DataClient> _logger;

        public DataClient(HttpClient httpClient, IOptions<DataClientSettings> options, ILogger<DataClient> logger)
        {
            _logger = logger;
            _httpClient = httpClient;
            _clientSettings = options.Value;
        }

        public async Task PostOnDataClientAsync(SensorData data, string value)
        {
             await _httpClient.PostAsJsonAsync<Data>(_clientSettings.HostName, new Data(data.Date, data.Time, (decimal)data.GetType().GetProperty(value).GetValue(data)));
        }

        record Data(DateTime date, string time, decimal value);
    }
}
