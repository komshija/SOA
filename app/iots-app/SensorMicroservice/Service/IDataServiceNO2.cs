using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorMicroservice.Service
{
    public interface IDataServiceNO2
    {
        void SetThreshold(double value);
        double GetThreshold();
        void SetSendInterval(int value);
        int GetSendInterval();
    }
}