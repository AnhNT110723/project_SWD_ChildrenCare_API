using System.ComponentModel.DataAnnotations;

namespace Children_Care_API.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Reservation>? Reservations { get; set; }
        public ICollection<Feedback>? Feedbacks { get; set; }
    }
}
