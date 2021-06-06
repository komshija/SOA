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
    /// <summary>
    /// Data client je base klasa koja salje podatke na data microservice 
    /// </summary>
    public class DataClient
    {
        #region Members
        private readonly HttpClient _httpClient;
        private readonly DataClientSettings _clientSettings;
        private readonly ILogger<DataClient> _logger;
        #endregion

        public DataClient(HttpClient httpClient, IOptions<DataClientSettings> options, ILogger<DataClient> logger)
        {
            _logger = logger;
            _httpClient = httpClient;
            _clientSettings = options.Value;
        }

        #region Methods
        public async Task PostOnDataClientAsync(SensorData data, string value)
        {
            try
            {
                PropertyInfo property = data.GetType().GetProperty(value);
                var displayName = ((MemberInfo)property).GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
                await _httpClient.PostAsJsonAsync(_clientSettings.HostName, new Data(data.Date.ToString("dd/MM/yyyy HH:mm:ss"), (decimal)property.GetValue(data),displayName.Name));
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
                _logger.LogInformation("Failed to post to Data microservice");
                
            }
        }
        #endregion

        record Data(string date, decimal value,string dataName);
    }
}
