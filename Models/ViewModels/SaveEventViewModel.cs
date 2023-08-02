using System.ComponentModel.DataAnnotations;

namespace FullCalenderApp.Models.ViewModels
{
    public class SaveEventViewModel
    {
            public int EventId { get; set; }
            [Required]
            public string Subject { get; set; }
            public string? Description { get; set; }
            public bool IsFullDay { get; set; }
            public string? ThemeColor { get; set; }
            public DateTime Start { get; set; }
            public DateTime? End { get; set; }
        }
}
