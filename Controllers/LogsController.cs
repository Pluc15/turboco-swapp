using System.Linq;
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

        public LogsController(
            IHubContext<DashboardHub, IDashboardClient> dashboardHubContext
        )
        {
            _dashboardHubContext = dashboardHubContext;
        }

        [HttpPost("api/logs")]
        public async Task<IActionResult> Index(
            LogMessage log
        )
        {
            await _dashboardHubContext.Clients.All.OnLog(log.Level, log.Message, log.Timestamp);

            MemoryDatabase.Logs.Add(log);

            while (MemoryDatabase.Alerts.Count > 100)
            {
                MemoryDatabase.Alerts.Remove(MemoryDatabase.Alerts.OrderBy(a => a.Timestamp).First());
            }

            return Ok();
        }
    }
}