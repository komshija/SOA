using CommandMicroservice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandMicroservice.Controllers
{
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
        [Route("/cocommand/{command}")]
        public IActionResult COCommand(string command)
        {
            if (command.CompareTo("") == 0)
                return BadRequest();
            
            _actuatorClient.PostOnCOActuator(command);
            _notificationService.SendNotification("CO command activated : " + command);
            return Ok(command);
        }

        [HttpPost]
        [Route("/no2command/{command}")]
        public IActionResult NO2Command(string command)
        {
            if (command.CompareTo("") == 0)
                return BadRequest();

            _actuatorClient.PostOnNO2Actuator(command);
            _notificationService.SendNotification("NO2 command activated : " + command);
            return Ok(command);
        }
    }
}
