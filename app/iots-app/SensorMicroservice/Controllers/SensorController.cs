using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SensorMicroservice.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorMicroservice.Controllers
{
    [Route("/api/{controller}")]
    [ApiController]
    public class SensorController : Controller
    {
        private readonly IDataServiceNO2 _service;
        private readonly ILogger<SensorController> _logger;

        public SensorController(IDataServiceNO2 service, ILogger<SensorController> logger)
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
                Name = "NO2 Sensor ",
                Desciption = "NO2 Sensor for Air Quality",
                SendInterval = _service.GetSendInterval(),
                Threshold = _service.GetThreshold()
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

        [Route("/threshold")]
        [HttpPost]
        public IActionResult ChangeThreshold(double threshold)
        {
            if (threshold <= 2)
                return BadRequest();
            _service.SetThreshold(threshold);
            return Ok(threshold);
        }
    }
}
