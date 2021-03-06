using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using DeviceShared;

namespace DeviceMicroservice.Service
{
    public class DataServiceCO : DataService, IDataServiceCO
    {
        #region Methods
        public DataServiceCO(DataClient dataClient,ILogger<DataServiceCO> logger, string dataValue = "CO_GT") :base(dataClient,logger,dataValue)
        {

        }

        public override async Task ExecuteSendAsync()
        {
            int index = r.Next(data.Count());
            data.ElementAt(index).Date = DateTime.Now;
            await dataClient.PostOnDataClientAsync(data.ElementAt(index),DataValue);

            logger.LogInformation("CO2 salje podatke {0}", data.ElementAt(index).CO_GT);

            await Task.Delay(TimeSpan.FromSeconds(SendInterval));
            _sendTimer.Change(TimeSpan.Zero, TimeSpan.Zero);
        }

        public void SetSendInterval(int value)
        {
            this.SendInterval = value;
        }

        public int GetSendInterval()
        {
            return this.SendInterval;
        }

        #endregion
    }
}
