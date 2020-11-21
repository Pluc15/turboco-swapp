using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TurboCoConsole.Data;

namespace TurboCoConsole.Hubs
{
    public interface IDashboardClient
    {
        public Task OnConnectionCountChanged(
            int count
        );

        public Task OnAlert(
            string message
        );

        public Task OnRobotLocationChanged(
            string label,
            double x,
            double y,
            double z
        );
    }


    public class DashboardHub : Hub<IDashboardClient>
    {
        private static int _connectedUsers;

        public override async Task OnConnectedAsync()
        {
            _connectedUsers++;
            await Clients.Caller.OnConnectionCountChanged(_connectedUsers);

            foreach (var alert in MemoryDatabase.Alerts)
                await Clients.Caller.OnAlert(alert.Message);

            foreach (var robotInfo in MemoryDatabase.RobotInfos)
                await Clients.Caller.OnRobotLocationChanged(
                    robotInfo.Key,
                    robotInfo.Value.X,
                    robotInfo.Value.Y,
                    robotInfo.Value.Z
                );

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