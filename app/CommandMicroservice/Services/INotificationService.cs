using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandMicroservice.Services
{
    public interface INotificationService
    {
        Task SendNotification(string message);

    }
}
