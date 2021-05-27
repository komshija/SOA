using CommandMicroservice.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandMicroservice.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationsHub> _hub;

        public NotificationService(IHubContext<NotificationsHub> hub)
        {
            _hub = hub;
        }
        public async Task SendNotification(string message)
        {
            await _hub.Clients.All.SendAsync("Notification", message);
        }
    }
}
