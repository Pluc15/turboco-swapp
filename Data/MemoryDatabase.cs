using System.Collections.Generic;
using TurboCoConsole.Models;

namespace TurboCoConsole.Data
{
    public static class MemoryDatabase
    {
        public static List<Alert> Alerts = new List<Alert>();
        public static Dictionary<string, RobotInfo> RobotInfos = new Dictionary<string, RobotInfo>();
    }
}