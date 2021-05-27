using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.ComponentModel.DataAnnotations;
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
            PropertyInfo property = data.GetType().GetProperty(value);
            var displayName = ((MemberInfo)property).GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
            await _httpClient.PostAsJsonAsync(_clientSettings.HostName, new Data(data.Date.ToString("dd/MM/yyyy HH:mm:ss"), (decimal)property.GetValue(data),displayName.Name));
        }

        record Data(string date, decimal value,string dataName);
    }
}
