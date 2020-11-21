using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TurboCoConsole.Hubs
{
    public interface IDashboardClient
    {
        public Task OnConnectionCountChanged(int count);
        public Task OnAlert(string message);
    }
    
    
    public class DashboardHub: Hub<IDashboardClient>
    {
        private static int _connectedUsers;
        
        public override async Task OnConnectedAsync()
        {
            _connectedUsers++;
            await Clients.Caller.OnConnectionCountChanged(_connectedUsers);
            await Clients.All.OnConnectionCountChanged(_connectedUsers);
        }

        public override async Task OnDisconnectedAsync(
            Exception exception
        )
        {
            await Clients.All.OnConnectionCountChanged(_connectedUsers--);
        }
    }
}