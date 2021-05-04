using FileHelpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeviceShared
{
    public class DataService : IDisposable
    {
        #region Fields
        protected readonly ILogger<DataService> logger;
        protected readonly DataClient dataClient;
        protected Task sendTask;
        protected Random r;
        protected Timer _sendTimer;
        #endregion
        #region Properties
        protected int SendInterval { get; set; }
        protected IList<SensorData> data { get; set; }
        protected string DataValue { get; set; }

        #endregion
        #region Methods
        public DataService(DataClient dataClient, ILogger<DataService> logger, string dataValue)
        {
            this.dataClient = dataClient;
            this.logger = logger;
            SendInterval = 5;
            DataValue = dataValue;
            r = new Random();
            var engine = new FileHelperEngine<SensorData>();
            var records = engine.ReadFile("../DeviceShared/Data/AirQuality.csv");
            data = records.ToList();
            _sendTimer = new Timer(SendData, null, TimeSpan.Zero, TimeSpan.Zero);
            logger.LogInformation("Data service starting..");
        }

        private void SendData(object state)
        {
            logger.LogInformation("{time} :: data sent {data}", DateTime.Now, this.SendInterval);
            _sendTimer.Change(Timeout.Infinite, Timeout.Infinite);
            sendTask = ExecuteSendAsync();
        }

        public virtual async Task ExecuteSendAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(SendInterval));
            _sendTimer.Change(TimeSpan.Zero, TimeSpan.Zero);
        }

        public void Dispose()
        {
            _sendTimer?.Dispose();
        }
        #endregion

    }
}
