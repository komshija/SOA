using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceMicroservice.Service
{
    public interface IDataServiceT
    {
        void SetSendInterval(int value);
        int GetSendInterval();
    }
}