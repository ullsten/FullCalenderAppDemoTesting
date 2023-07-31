using System.ComponentModel.DataAnnotations;

namespace FullCalenderApp.Models
{
    public class Event
    {
        public int EventId { get; set; }

        [StringLength(100)]
        public string Subject { get; set; }

        [StringLength(300)]
        public string? Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; } = null;

        [StringLength(10)]
        public string? ThemeColor { get; set; }
        public bool IsFullDay { get; set; }
    }
}
