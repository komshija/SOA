using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CommandMicroservice.Services
{
    public class ActuatorClient
    {
        private readonly HttpClient _httpClient;

        public ActuatorClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void PostOnActuator()
        {

        }
    }
}
