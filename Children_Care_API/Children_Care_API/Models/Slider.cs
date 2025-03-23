using System.ComponentModel.DataAnnotations;

namespace Children_Care_API.Models
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Image { get; set; } = string.Empty;

        [Required]
        public string Backlink { get; set; } = string.Empty;

        public string? Notes { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
