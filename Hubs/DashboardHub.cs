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
            string message,
            DateTime timestamp
        );

        public Task OnLog(
            int logLevel,
            string message,
            DateTime timestamp
        );

        public Task OnRobotLocationChanged(
            string label,
            double x,
            double y,
            double z,
            DateTime timestamp
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
                await Clients.Caller.OnAlert(alert.Message, alert.Timestamp);

            foreach (var log in MemoryDatabase.Logs)
                await Clients.Caller.OnLog(log.Level, log.Message, log.Timestamp);

            foreach (var robotInfo in MemoryDatabase.RobotInfos)
                await Clients.Caller.OnRobotLocationChanged(
                    robotInfo.Key,
                    robotInfo.Value.X,
                    robotInfo.Value.Y,
                    robotInfo.Value.Z,
                    robotInfo.Value.Timestamp
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