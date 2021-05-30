using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SensorMicroservice.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorMicroservice.Controllers
{
    [EnableCors]
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
                Desciption = "Nitrogen Dioxide (NO2) is one of a group of highly reactive gases known as oxides of nitrogen or nitrogen oxides (NOx). Other nitrogen oxides include nitrous acid and nitric acid. NO2 is used as the indicator for the larger group of nitrogen oxides. NO2 primarily gets in the air from the burning of fuel.",
                SendInterval = _service.GetSendInterval(),
                Threshold = _service.GetThreshold()
            });
        }

        [Route("/interval/{sendInterval}")]
        [HttpPost]
        public IActionResult ChangeSendInterval(int sendInterval)
        {
            if (sendInterval <= 0)
                return BadRequest();
            _service.SetSendInterval(sendInterval);
            _logger.LogInformation("Send interval value has been changed on : {sendInterval}s !" + sendInterval);
            return Ok(sendInterval);
        }

        [Route("/treshold/{threshold}")]
        [HttpPost]
        public IActionResult ChangeThreshold(double threshold)
        {
            if (threshold <= 2)
                return BadRequest();
            _service.SetThreshold(threshold);
            _logger.LogInformation("Threshold value has been changed on : {threshold} !" + threshold);
            return Ok(threshold);
        }

        [Route("/actuator")]
        [HttpPost]
        public IActionResult ActuatorNO2([FromBody] string command)
        {
            _logger.LogInformation("Command : " + command);
            return Ok(command);
        }
    }
}
