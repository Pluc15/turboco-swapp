using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Cosmos;
using TurboCoConsole.Models;

namespace TurboCoConsole.Data
{
    public class AlertsRepository
    {
        private readonly CosmosContainer _alertsContainer;

        public AlertsRepository(
            CosmosClient cosmosClient
        )
        {
            _alertsContainer = cosmosClient.GetDatabase("turboco").GetContainer("alerts");
        }

        public async IAsyncEnumerable<Alert> GetAll(int limit = 10)
        {
            var query = new QueryDefinition($"SELECT * FROM a ORDER BY a.Timestamp DESC OFFSET 0 LIMIT {limit}");
            await foreach (var alert in _alertsContainer.GetItemQueryIterator<Alert>(query))
            {
                yield return alert;
            }
        }

        public async Task Add(
            Alert alert
        )
        {
            alert.Id = Guid.NewGuid().ToString();
            await _alertsContainer.UpsertItemAsync(alert);
        }
    }
}