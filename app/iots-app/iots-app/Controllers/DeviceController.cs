﻿using DeviceMicroservice.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DeviceMicroservice.Controllers
{
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
        public IActionResult ChangeSendInterval(int sendInterval)
        {
            if (sendInterval <= 0)
                return BadRequest();
            _service.SetSendInterval(sendInterval);
            return Ok(sendInterval);
        }


        [Route("/actuator")]
        [HttpPost]
        public IActionResult ActuatorCO()
        {
            _logger.LogInformation("Ukljucen alarm!");
            return Ok("Ukljucen alarm!");
        }
    }
}
