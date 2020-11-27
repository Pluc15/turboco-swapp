using Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace TurboCoConsole.Data
{
    public class RepositoryFactory
    {
        private readonly CosmosClient _cosmosClient;

        public RepositoryFactory(
            IConfiguration configuration
        )
        {
            _cosmosClient = new CosmosClient(configuration.GetValue<string>("CosmosConnectionString"));
        }

        public AlertsRepository Alerts => new AlertsRepository(_cosmosClient);

        public RobotInfosRepository RobotInfos => new RobotInfosRepository(_cosmosClient);

        public LogsRepository Logs => new LogsRepository(_cosmosClient);
    }
}