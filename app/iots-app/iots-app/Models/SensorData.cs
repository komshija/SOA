using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceMicroservice.Models
{
    public class SensorData
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int CO { get; set; }
        public int PT { get; set; }
        public int NMHC { get; set; }
        public double C6H6 { get; set; }
        public int PT08S1 { get; set; }
        public int NOx { get; set; }
        public int PT08S2 { get; set; }
        public int NO2 { get; set; }
        public int PT08S3 { get; set; }
        public int PT08S4 { get; set; }
        public double T { get; set; }
        public double RH { get; set; }
        public double AH { get; set; }
    }

}
