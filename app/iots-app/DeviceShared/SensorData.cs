using FileHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceShared
{
    [IgnoreFirst]
    [DelimitedRecord(";")]
    [IgnoreEmptyLines]
    public class SensorData
    {
        [FieldConverter(ConverterKind.Date, "d/M/yyyy")]
        public DateTime Date { get; set; }
        public string Time { get; set; }
        [Display (Name = "CO")]
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal CO_GT { get; set; } // kandidat
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal PT08S1_CO { get; set; }
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal NMHC_GT { get; set; }
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal C6H6_GT { get; set; } // kandidat benzene
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal PT08S2_NMHC { get; set; }
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal NOx_GT { get; set; } //kandidat
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal PT08S3_NOx { get; set; }
        [Display(Name = "NO2")]
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal NO2_GT { get; set; } // kandidat
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal PT08S4_NO2 { get; set; }
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal PT08S5_O3 { get; set; }
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
