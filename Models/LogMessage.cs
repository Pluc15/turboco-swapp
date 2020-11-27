using System;
using System.Text.Json.Serialization;

namespace TurboCoConsole.Models
{
    public class LogMessage
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        public int Level { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}