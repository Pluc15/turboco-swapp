using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        public IActionResult Index(
            Alert alert
        )
        {
            _dashboardHubContext.Clients.All.OnAlert(alert.Message);
            return Ok();
        }
    }
}