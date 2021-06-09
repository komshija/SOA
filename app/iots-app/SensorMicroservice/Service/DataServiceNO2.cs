using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceShared;
using Microsoft.Extensions.Logging;

namespace SensorMicroservice.Service
{
    public class DataServiceNO2 : DataService, IDataServiceNO2
    {
        #region Properties
        public double Threshold { get; set; }
        public decimal LastDataRead { get; set; }
        #endregion
        #region Methods
        public DataServiceNO2(DataClient dataClient, ILogger<DataServiceNO2> logger, string dataValue = "NO2_GT", double th=0.2, decimal last=90) : base(dataClient, logger, dataValue)
        {
            Threshold = th;
            LastDataRead = last;
        }

        public override async Task ExecuteSendAsync()
        {
            int index = r.Next(data.Count());
            SensorData d = data.ElementAt(index);

            if (d.NO2_GT > LastDataRead * ((decimal)Threshold + 1))
            {
                d.Date = DateTime.Now;
                await dataClient.PostOnDataClientAsync(d, DataValue);
                logger.LogInformation("NO2 salje podatke - {0}", data.ElementAt(index).NO2_GT);
            }

            LastDataRead = d.NO2_GT;
            await Task.Delay(TimeSpan.FromSeconds(SendInterval));
            _sendTimer.Change(TimeSpan.Zero, TimeSpan.Zero);
        }

        public int GetSendInterval()
        {
            return SendInterval;
        }

        public double GetThreshold()
        {
            return Threshold;
        }

        public void SetSendInterval(int value)
        {
            SendInterval = value;
        }

        public void SetThreshold(double value)
        {
            Threshold = value;
        }

        public decimal GetLastDataRead()
        {
            return LastDataRead;
        }
        #endregion
    }
}
