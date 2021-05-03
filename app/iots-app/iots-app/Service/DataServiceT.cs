using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using DeviceShared;

namespace DeviceMicroservice.Service
{
    public class DataServiceT : DataService, IDataServiceT
    {
        #region Methods
        public DataServiceT(DataClient dataClient,ILogger<DataServiceT> logger, string dataValue = "T") :base(dataClient,logger,dataValue)
        {

        }

        public override async Task ExecuteSendAsync()
        {
            int index = r.Next(data.Count());
            await dataClient.PostOnDataClientAsync(data.ElementAt(index),DataValue);

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
