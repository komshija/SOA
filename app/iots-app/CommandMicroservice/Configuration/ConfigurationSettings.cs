using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandMicroservice.Configuration
{
    public class ConfigurationSettings
    {
        public string CO_ACTUATOR_URL { get; set; }
        public string NO2_ACTUATOR_URL { get; set; }
        public string CO_TOPIC_COMMAND { get; set; }
        public string NO2_TOPIC_COMMAND { get; set; }
    }
}
