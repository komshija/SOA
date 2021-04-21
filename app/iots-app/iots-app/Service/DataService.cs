using DeviceMicroservice.Clients;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FileHelpers;
using System.IO;
using System.Globalization;
using DeviceMicroservice.Models;

namespace DeviceMicroservice.Service
{
    public class DataService : IDataService
    {
        #region Fields

        private readonly ILogger<DataService> logger;

        private readonly DataClient dataClient;

        private Task sendTask;

        private Random r;
        private Timer _sendTimer;
        #endregion
        #region Properties
        public int SendInterval { get; set; }
        private IList<SensorData> data { get; set; }

        #endregion
        #region Methods
        public DataService(DataClient dataClient,ILogger<DataService> logger)
        {
            this.dataClient = dataClient;
            this.logger = logger;
            SendInterval = 5;
            r = new Random();
            var engine = new FileHelperEngine<SensorData>();
            var records = engine.ReadFile("./Data/AirQuality.csv");
            data = records.ToList();
            _sendTimer = new Timer(SendData, null, TimeSpan.Zero, TimeSpan.Zero);
            logger.LogInformation("Data service starting..");
        }

        private void SendData(object state)
        {
            logger.LogDebug("{time} :: data sent {data}", DateTime.Now, this.SendInterval);
            _sendTimer.Change(Timeout.Infinite, Timeout.Infinite);
            sendTask = ExecuteSendAsync();
            
        } 

        private async Task ExecuteSendAsync()
        {
            int index = r.Next(data.Count());
            await dataClient.PostOnDataClientAsync(data.ElementAt(index));

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
