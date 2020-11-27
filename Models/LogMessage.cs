using System;

namespace TurboCoConsole.Models
{
    public class LogMessage
    {
        public int Level { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}