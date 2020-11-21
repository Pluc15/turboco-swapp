using System.ComponentModel.DataAnnotations;

namespace TurboCoConsole.Models
{
    public class Alert
    {
        [Required]
        public string Message { get; set; }
    }
}