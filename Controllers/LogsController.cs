using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TurboCoConsole.Data;
using TurboCoConsole.Hubs;
using TurboCoConsole.Models;

namespace TurboCoConsole.Controllers
{
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly IHubContext<DashboardHub, IDashboardClient> _dashboardHubContext;
        private readonly RepositoryFactory _repositoryFactory;

        public LogsController(
            IHubContext<DashboardHub, IDashboardClient> dashboardHubContext,
            RepositoryFactory repositoryFactory
        )
        {
            _dashboardHubContext = dashboardHubContext;
            _repositoryFactory = repositoryFactory;
        }

        [HttpPost("api/logs")]
        public async Task<IActionResult> Index(
            LogMessage log
        )
        {
            await _dashboardHubContext.Clients.All.OnLog(log.Level, log.Message, log.Timestamp);

            await _repositoryFactory.Logs.Add(log);

            return Ok();
        }
    }
}