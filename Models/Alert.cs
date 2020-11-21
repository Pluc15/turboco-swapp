using System;
using System.ComponentModel.DataAnnotations;

namespace TurboCoConsole.Models
{
    public class Alert
    {
        [Required]
        public string Message { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}