using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Cosmos;
using TurboCoConsole.Models;

namespace TurboCoConsole.Data
{
    public class RobotInfosRepository
    {
        private readonly CosmosContainer _robotsContainer;

        public RobotInfosRepository(
            CosmosClient cosmosClient
        )
        {
            _robotsContainer = cosmosClient.GetDatabase("turboco").GetContainer("robots");
        }

        public async IAsyncEnumerable<RobotInfo> GetAll()
        {
            await foreach (var robotInfo in _robotsContainer.GetItemQueryIterator<RobotInfo>())
            {
                yield return robotInfo;
            }
        }

        public async Task AddOrUpdate(
            RobotInfo robotInfo
        )
        {
            robotInfo.Id = Guid.NewGuid().ToString();
            await _robotsContainer.UpsertItemAsync(robotInfo);
        }
    }
}