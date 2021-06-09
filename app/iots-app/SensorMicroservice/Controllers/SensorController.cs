using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SensorMicroservice.Service;
using Swashbuckle.AspNetCore.Annotations;
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
        [HttpGet]
        [SwaggerOperation("Returns all information about NO2 sensor.", "Returns name, description, data send interval and data send treshold.")]
        [SwaggerResponse(200)]
        public IActionResult GetInfo()
        {
            return new OkObjectResult(new
            {
                Name = "NO2 Sensor ",
                Desciption = "Nitrogen Dioxide (NO2) is one of a group of highly reactive gases known as oxides of nitrogen or nitrogen oxides (NOx). Other nitrogen oxides include nitrous acid and nitric acid. NO2 is used as the indicator for the larger group of nitrogen oxides. NO2 primarily gets in the air from the burning of fuel.",
                SendInterval = _service.GetSendInterval(),
                Threshold = _service.GetThreshold(),
                LastDataRead = _service.GetLastDataRead()
            });
        }

        [Route("/interval/{sendInterval}")]
        [HttpPost]
        [SwaggerOperation("Sets data send interval.", "Interval must be greater then zero.")]
        [SwaggerResponse(200, "Everything is okey.")]
        [SwaggerResponse(400, "Interval is negative number.")]
        public IActionResult ChangeSendInterval(int sendInterval)
        {
            if (sendInterval <= 0)
                return BadRequest();
            _service.SetSendInterval(sendInterval);
            _logger.LogInformation("Send interval value has been changed on : {sendInterval}s !",sendInterval);
            return Ok(sendInterval);
        }

        [Route("/treshold/{threshold}")]
        [HttpPost]
        [SwaggerOperation("Sets data treshold.", "Interval must be greater then two.")]
        [SwaggerResponse(200, "Everything is okey.")]
        [SwaggerResponse(400, "Interval is less then two number.")]
        public IActionResult ChangeThreshold(double threshold)
        {
            if (threshold < 0 || threshold > 1)
                return BadRequest();
            _service.SetThreshold(threshold);
            _logger.LogInformation("Threshold value has been changed on : {threshold} !",threshold);
            return Ok(threshold);
        }

        [Route("/actuator")]
        [HttpPost]
        [SwaggerOperation("Sets command to virtual actuator.", "Logs command to console.")]
        [SwaggerResponse(200)]
        public IActionResult ActuatorNO2([FromBody] string command)
        {
            _logger.LogInformation("Command : " + command);
            return Ok(command);
        }
    }
}
