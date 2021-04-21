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
        private readonly ILogger<DataService> logger;

        private readonly DataClient dataClient;

        private Task sendTask;

        private Random r;
        public int SendInterval { get; set; }

        private Timer _sendTimer;

        private IList<SensorData> data;
      
       
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
        }

        private void SendData(object state)
        {
            //logger.LogInformation("{time} :: data sent {data}", DateTime.Now, this.SendInterval);
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
     
    }
}
