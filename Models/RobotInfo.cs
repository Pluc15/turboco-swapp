using System;
using System.Text.Json.Serialization;

namespace TurboCoConsole.Models
{
    public class RobotInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        public string Label { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}