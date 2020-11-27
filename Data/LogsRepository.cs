using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Cosmos;
using TurboCoConsole.Models;

namespace TurboCoConsole.Data
{
    public class LogsRepository
    {
        private readonly CosmosContainer _logsContainer;

        public LogsRepository(
            CosmosClient cosmosClient
        )
        {
            _logsContainer = cosmosClient.GetDatabase("turboco").GetContainer("logs");
        }

        public async IAsyncEnumerable<LogMessage> GetAll(int limit = 100)
        {
            var query = new QueryDefinition($"SELECT * FROM a ORDER BY a.Timestamp DESC OFFSET 0 LIMIT {limit}");
            await foreach (var log in _logsContainer.GetItemQueryIterator<LogMessage>(query))
            {
                yield return log;
            }
        }

        public async Task Add(
            LogMessage log
        )
        {
            log.Id = Guid.NewGuid().ToString();
            await _logsContainer.UpsertItemAsync(log);
        }
    }
}