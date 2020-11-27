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
        private readonly RepositoryFactory _repositoryFactory;
        private static int _connectedUsers;

        public DashboardHub(RepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public override async Task OnConnectedAsync()
        {
            _connectedUsers++;
            await Clients.Caller.OnConnectionCountChanged(_connectedUsers);

            await foreach (var alert in _repositoryFactory.Alerts.GetAll())
                await Clients.Caller.OnAlert(alert.Message, alert.Timestamp);

            await foreach (var log in _repositoryFactory.Logs.GetAll())
                await Clients.Caller.OnLog(log.Level, log.Message, log.Timestamp);

            await foreach (var robotInfo in _repositoryFactory.RobotInfos.GetAll())
                await Clients.Caller.OnRobotLocationChanged(
                    robotInfo.Label,
                    robotInfo.X,
                    robotInfo.Y,
                    robotInfo.Z,
                    robotInfo.Timestamp
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