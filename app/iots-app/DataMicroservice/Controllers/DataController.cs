using DataMicroservice.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataMicroservice.Controllers
{
    [ApiController]
    public class DataController : Controller
    {
        private readonly IRedisClient _client;
        public record Data(DateTime date, string time, decimal value, string dataName);
        private readonly ILogger<DataController> _logger;

        public DataController(IRedisClient client, ILogger<DataController> logger)
        {
            _client = client;
            _logger = logger;
        }

        [HttpGet]
        [Route("/search")]
        public IActionResult Search()
        {
            return Accepted();
        }

        [HttpPost]
        [Route("/sensordata")]
        public IActionResult SensorData([FromBody] Data sensorData)
        {
            _client.JsonSet(sensorData.dataName, sensorData.date.ToShortDateString(), sensorData);
            _logger.LogInformation(sensorData.dataName +" data received: {data}", sensorData.value);
            return Ok(sensorData);
        }
    }
}
