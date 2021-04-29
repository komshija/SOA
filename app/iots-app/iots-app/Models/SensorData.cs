using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileHelpers;

namespace DeviceMicroservice.Models
{
    [IgnoreFirst]
    [DelimitedRecord(";")]
    [IgnoreEmptyLines]
    public class SensorData
    {
        [FieldConverter(ConverterKind.Date, "d/M/yyyy")]
        public DateTime Date { get; set; }

        [FieldConverter(typeof(TimeConverter))]
        public DateTime Time { get; set; }

        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal CO_GT { get; set; }
        public int PT08S1_CO { get; set; }
        public int NMHC_GT { get; set; }
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal C6H6_GT { get; set; }
        public int PT08S2_NMHC { get; set; }
        public int NOx_GT { get; set; }
        public int PT08S3_NOx { get; set; }
        public int NO2_GT { get; set; }
        public int PT08S4_NO2 { get; set; }
        public int PT08S5_O3 { get; set; }
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal T { get; set; }
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal RH { get; set; }
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal AH { get; set; }

        public class TimeConverter : ConverterBase
        {
            public override object StringToField(string from)
            {
                DateTime time = new DateTime();
                time.AddHours(Convert.ToDouble(from.Substring(0, 2)));
                time.AddMinutes(Convert.ToDouble(from.Substring(3, 2)));
                time.AddSeconds(Convert.ToDouble(from.Substring(5, 2)));
                return time;
            }

            public override string FieldToString(object fieldValue)
            {
                return ((DateTime)fieldValue).ToString("H.m.s");
            }

        }

    }

  


}
