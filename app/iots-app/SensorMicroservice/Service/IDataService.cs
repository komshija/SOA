using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceMicroservice.Service
{
    public interface IDataService
    {
        void SetThreshold(float value);
        float GetThreshold();
        void SetSendInterval(int value);
        int GetSendInterval();
    }
}