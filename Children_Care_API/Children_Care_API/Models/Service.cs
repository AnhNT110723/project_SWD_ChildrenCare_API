using Children_Care_API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Children_Care_API.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string? BriefInfo { get; set; } //brief-info

        [MaxLength(255)]
        public string? Thumbnail { get; set; } //thumbnail

        public string? Description { get; set; }
        [Required, MaxLength(50)]
        public ServiceCategory Category { get; set; } = ServiceCategory.General;

        [Required]
        public decimal Price { get; set; }
        public decimal? SalePrice { get; set; }

        public bool IsFeatured { get; set; } = false; //featured

        public bool Status { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Reservation>? Reservations { get; set; }
        public ICollection<Feedback>? Feedbacks { get; set; }
    }
}
