using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TurboCoConsole.Models
{
    public class Alert
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        
        [Required]
        public string Message { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}