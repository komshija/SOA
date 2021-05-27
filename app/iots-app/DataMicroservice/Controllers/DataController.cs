﻿using DataMicroservice.Clients;
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
        public record Data(string date, decimal value, string dataName);
        private readonly ILogger<DataController> _logger;

        public DataController(IRedisClient client, ILogger<DataController> logger)
        {
            _client = client;
            _logger = logger;
        }

        [HttpPost]
        [Route("/sensordata")]
        public IActionResult SensorData([FromBody] Data sensorData)
        {
            _client.JsonSet(sensorData.dataName, sensorData.date, sensorData);
            _logger.LogInformation(sensorData.dataName + " data received: {data} that was generated : {date}", sensorData.value, sensorData.date);
            return Ok(sensorData);
        }

        [HttpGet]
        [Route("/getco")]
        public IActionResult GetCO()
        {
            try
            {
                var data = _client.JsonGet("CO");
                return new OkObjectResult(data);
            }
            catch (Exception e)
            {
                return new NoContentResult();
            }
        }

        [HttpGet]
        [Route("/getno2")]
        public IActionResult GetNO2()
        {
            try
            {
                var data = _client.JsonGet("NO2");
                return new OkObjectResult(data);
            }
            catch (Exception e)
            {
                return new NoContentResult();
            }
        }

        [HttpGet]
        [Route("/greater/{sensor}/{value}")]
        public IActionResult GetGreater(string sensor, int value)
        {
            try
            {
                sensor = sensor.ToUpper();
                if (sensor.CompareTo("CO") != 0 || sensor.CompareTo("NO2") != 0)
                    return new NoContentResult();
                var data = _client.JsonGet(sensor);
                List<Data> lista = data.Select(item => item.Value).Where(item => item.value >= value).ToList();
                return new OkObjectResult(lista);
            }
            catch (Exception e)
            {
                return new NoContentResult();
            }
        }

        [HttpGet]
        [Route("/less/{sensor}/{value}")]
        public IActionResult GetLess(string sensor, int value)
        {
            try
            {
                sensor = sensor.ToUpper();
                if (sensor.CompareTo("CO") != 0 || sensor.CompareTo("NO2") != 0)
                    return new NoContentResult();
                var data = _client.JsonGet(sensor);
                List<Data> lista = data.Select(item => item.Value).Where(item => item.value < value).ToList();
                return new OkObjectResult(lista);
            }
            catch (Exception e)
            {
                return new NoContentResult();
            }
        }
    }
}
