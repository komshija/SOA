using DeviceMicroservice.Models;
using DeviceMicroservice.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceMicroservice.Controllers
{
    [Route("/api/{controller}")]
    [ApiController]
    public class DeviceControlller : Controller
    {
        private readonly IDataService _service;
        private readonly ILogger<DeviceControlller> _logger;
        public DeviceControlller(IDataService service, ILogger<DeviceControlller> logger)
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
                Desciption = "Description ..."
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


        [Route("/test")]
        [HttpPost]
        public IActionResult TestPost([FromBody]SensorData data)
        {
            _logger.LogInformation("{data}",data);
            return Ok();
        }
    }
}
