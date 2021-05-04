using DeviceMicroservice.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DeviceMicroservice.Controllers
{
    [Route("/api/{controller}")]
    [ApiController]
    public class DeviceController : Controller
    {
        private readonly IDataServiceT _service;
        private readonly ILogger<DeviceController> _logger;
        public DeviceController(IDataServiceT service, ILogger<DeviceController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [Route("/")]
        [HttpGet]
        public IActionResult GetInfo()
        {
            return new OkObjectResult(new
            {
                Name = "Device 1",
                Desciption = "Description ...",
                SendInterval = _service.GetSendInterval()
            });
        }

        [Route("/{sendInterval}")]
        [HttpPost]
        public IActionResult ChangeSendInterval(int sendInterval)
        {
            if (sendInterval <= 0)
                return BadRequest();
            _service.SetSendInterval(sendInterval);
            return Ok(sendInterval);
        }

        [HttpPost]
        [Route("/test")]
        public IActionResult SensorDevice([FromBody] Data sensorData)
        {
            //_client.JsonSet("Temp", sensorData.date.ToShortDateString(), sensorData);
            _logger.LogInformation("Temperature data received: {data}", sensorData.value);
            return Ok(sensorData);
        }

        public record Data(DateTime date, string time, decimal value);
    }
}
