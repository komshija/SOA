using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceMicroservice.Service
{
    public interface IDataServiceCO
    {
        void SetSendInterval(int value);
        int GetSendInterval();
    }
}