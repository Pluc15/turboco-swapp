using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TurboCoConsole.Data;
using TurboCoConsole.Hubs;
using TurboCoConsole.Models;

namespace TurboCoConsole.Controllers
{
    [ApiController]
    public class AlertsController : ControllerBase
    {
        private readonly IHubContext<DashboardHub, IDashboardClient> _dashboardHubContext;

        public AlertsController(
            IHubContext<DashboardHub, IDashboardClient> dashboardHubContext
        )
        {
            _dashboardHubContext = dashboardHubContext;
        }

        [HttpPost("api/alerts")]
        public async Task<IActionResult> Index(
            Alert alert
        )
        {
            await _dashboardHubContext.Clients.All.OnAlert(alert.Message);

            MemoryDatabase.Alerts.Add(alert);

            return Ok();
        }
    }
}