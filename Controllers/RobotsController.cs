using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TurboCoConsole.Data;
using TurboCoConsole.Hubs;
using TurboCoConsole.Models;

namespace TurboCoConsole.Controllers
{
    [ApiController]
    public class RobotsController : ControllerBase
    {
        private readonly IHubContext<DashboardHub, IDashboardClient> _dashboardHubContext;
        private readonly RepositoryFactory _repositoryFactory;

        public RobotsController(
            IHubContext<DashboardHub, IDashboardClient> dashboardHubContext,
            RepositoryFactory repositoryFactory
        )
        {
            _dashboardHubContext = dashboardHubContext;
            _repositoryFactory = repositoryFactory;
        }

        [HttpPut("api/robots/{robotLabel}")]
        public async Task<IActionResult> Index(
            [FromRoute] string robotLabel,
            RobotInfo robotInfo
        )
        {
            await _dashboardHubContext.Clients.All.OnRobotLocationChanged(
                robotLabel,
                robotInfo.X,
                robotInfo.Y,
                robotInfo.Z,
                robotInfo.Timestamp
            );

            await _repositoryFactory.RobotInfos.AddOrUpdate(robotInfo);
            
            return Ok();
        }
    }
}