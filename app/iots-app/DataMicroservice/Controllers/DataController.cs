using DataMicroservice.Clients;
using DataMicroservice.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataMicroservice.Controllers
{
    [EnableCors]
    [ApiController]
    public class DataController : Controller
    {
        private readonly IRedisClient _client;
        public record Data(string date, decimal value, string dataName);
        private readonly ILogger<DataController> _logger;
        private readonly IMqttPublisher _mqttPublisher;

        public DataController(IRedisClient client, ILogger<DataController> logger, IMqttPublisher mqttPublisher)
        {
            _client = client;
            _logger = logger;
            _mqttPublisher = mqttPublisher;
        }

        [HttpPost]
        [Route("/sensordata")]
        [SwaggerOperation(Summary = "Receive data from sensors", Description = "Data can be type of CO or NO2")]
        [SwaggerResponse(200, "Everything is ok.")]
        [SwaggerResponse(204, "Something went wrong.")]
        public IActionResult SensorData([FromBody] Data sensorData)
        {
            try
            {
                _client.JsonSet(sensorData.dataName, sensorData.date, sensorData);
                var mqttValue = new IMqttPublisher.Data(sensorData.date, sensorData.value, sensorData.dataName);
                if (sensorData.dataName.CompareTo("CO") == 0)
                    _mqttPublisher.COMqttPublish(mqttValue);
                else
                    _mqttPublisher.NO2MqttPublish(mqttValue);

                _logger.LogInformation(sensorData.dataName + " data received: {data} that was generated : {date}", sensorData.value, sensorData.date);
                return Ok(sensorData);
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
                return new NoContentResult();
            }
        }

        [HttpGet]
        [Route("/get/{sensor}")]
        [SwaggerOperation(Summary = "Returns all data from sensors", Description = "Sensor data can be from CO or NO2 sensor.")]
        [SwaggerResponse(200, "Everything is ok.")]
        [SwaggerResponse(204, "Something went wrong.")]
        public IActionResult Get(string sensor)
        {
            try
            {
                sensor = sensor.ToUpper();
                if (sensor.CompareTo("CO") == 0 || sensor.CompareTo("NO2") == 0)
                {
                    var data = _client.JsonGet(sensor).Select(item => item.Value).OrderByDescending(x => DateTime.ParseExact(x.date, "dd/MM/yyyy HH:mm:ss", null)).ToList();
                    return new OkObjectResult(data);
                }
                return new NoContentResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return new NoContentResult();
            }
        }

        [HttpGet]
        [Route("/greater/{sensor}/{value}")]
        [SwaggerOperation(Summary = "Returns all data grater then value from sensors", Description = "Sensor data can be from CO or NO2 sensor.")]
        [SwaggerResponse(200, "Everything is ok.")]
        [SwaggerResponse(204, "Something went wrong.")]
        public IActionResult GetGreater(string sensor, int value)
        {
            try
            {
                sensor = sensor.ToUpper();
                if (sensor.CompareTo("CO") == 0 || sensor.CompareTo("NO2") == 0)
                {
                    var data = _client.JsonGet(sensor).Select(item => item.Value).Where(item => item.value >= value).OrderByDescending(x => DateTime.ParseExact(x.date, "dd/MM/yyyy HH:mm:ss", null)).ToList(); ;
                    return new OkObjectResult(data);
                }
                return new NoContentResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return new NoContentResult();
            }
        }

        [HttpGet]
        [Route("/less/{sensor}/{value}")]
        [SwaggerOperation(Summary = "Returns all data less then value from sensors", Description = "Sensor data can be from CO or NO2 sensor.")]
        [SwaggerResponse(200, "Everything is ok.")]
        [SwaggerResponse(204, "Something went wrong.")]
        public IActionResult GetLess(string sensor, int value)
        {
            try
            {
                sensor = sensor.ToUpper();
                if (sensor.CompareTo("CO") == 0 || sensor.CompareTo("NO2") == 0)
                {
                    var data = _client.JsonGet(sensor).Select(item => item.Value).Where(item => item.value < value).OrderByDescending(x => DateTime.ParseExact(x.date, "dd/MM/yyyy HH:mm:ss", null)).ToList(); ;
                    return new OkObjectResult(data);
                }
                return new NoContentResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return new NoContentResult();
            }
        }

        [HttpGet]
        [Route("/getlast/{sensor}")]
        [SwaggerOperation(Summary = "Returns last 10 values from sensor.", Description = "Sensor data can be from CO or NO2 sensor.")]
        [SwaggerResponse(200, "Everything is ok.")]
        [SwaggerResponse(204, "Something went wrong.")]
        public IActionResult GetLast(string sensor)
        {
            try
            {
                sensor = sensor.ToUpper();
                if (sensor.CompareTo("CO") == 0 || sensor.CompareTo("NO2") == 0)
                {
                    var data = _client.JsonGet(sensor).Select(item => item.Value).OrderByDescending(x => DateTime.ParseExact(x.date, "dd/MM/yyyy HH:mm:ss", null)).Take(10).ToList(); ;
                    return new OkObjectResult(data);
                }
                return new NoContentResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return new NoContentResult();
            }
        }
    }
}
