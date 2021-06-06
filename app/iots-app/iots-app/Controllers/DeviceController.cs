using DeviceMicroservice.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net.Http;

namespace DeviceMicroservice.Controllers
{
    [EnableCors]
    [ApiController]
    public class DeviceController : Controller
    {
        private readonly IDataServiceCO _service;
        private readonly ILogger<DeviceController> _logger;
        public DeviceController(IDataServiceCO service, ILogger<DeviceController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [Route("/")]
        [HttpGet]
        [SwaggerOperation("Returns all information about CO sensor","Returns name, description and data send interval.")]
        [SwaggerResponse(200)]
        public IActionResult GetInfo()
        {
            return new OkObjectResult(new
            {
                Name = "Device 1",
                Desciption = "Carbon monoxide (chemical formula CO) is a colorless, odorless, and tasteless dangerous flammable gas that is slightly less dense than air. Carbon monoxide consists of one carbon atom and one oxygen atom. It is the simplest molecule of the Oxocarbon family. In coordination complexes the carbon monoxide ligand is called carbonyl.",
                SendInterval = _service.GetSendInterval()
            });
        }

        [Route("/{sendInterval}")]
        [HttpPost]
        [SwaggerOperation("Sets data send interval.","Interval must be greater then zero.")]
        [SwaggerResponse(200, "Everything is okey.")]
        [SwaggerResponse(400,"Interval is negative number.")]
        public IActionResult ChangeSendInterval(int sendInterval)
        {
            if (sendInterval <= 0)
                return BadRequest();
            _service.SetSendInterval(sendInterval);
            _logger.LogInformation("Send interval value has been changed on : {sendInterval}s !",sendInterval);
            return Ok(sendInterval);
        }


        [Route("/actuator")]
        [HttpPost]
        [SwaggerOperation("Sets command to virtual actuator.", "Logs command to console.")]
        [SwaggerResponse(200)]
        public IActionResult ActuatorCO([FromBody] string command)
        {
            _logger.LogInformation("Command : " + command);
            return Ok(command);
        }
    }
}
