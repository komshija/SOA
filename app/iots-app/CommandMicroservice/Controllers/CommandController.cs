using CommandMicroservice.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandMicroservice.Controllers
{
    [EnableCors]
    [ApiController]
    public class CommandController : Controller
    {
        private readonly ActuatorClient _actuatorClient;
        private readonly INotificationService _notificationService;

        public CommandController(ActuatorClient actuatorClient, INotificationService notificationService)
        {
            _actuatorClient = actuatorClient;
            _notificationService = notificationService;
        }

        [HttpPost]
        [Route("/command/{sensor}/{command}")]
        public IActionResult Command(string sensor,string command)
        {
            try
            {
                sensor = sensor.ToUpper();
                if (command.CompareTo("") != 0 && (sensor.CompareTo("CO") == 0 || sensor.CompareTo("NO2") == 0))
                {
                    if (sensor.CompareTo("CO") == 0)
                        _actuatorClient.PostOnCOActuator(command);
                    else
                        _actuatorClient.PostOnNO2Actuator(command);
                    _notificationService.SendNotification(sensor + " command activated : " + command);
                    return Ok(command);
                }

                return BadRequest();
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }
    }
}
