using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Children_Care_API.Models
{
    public class Prescription
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int ReservationId { get; set; }

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("DoctorId")]
        public User? Doctor { get; set; }

        [ForeignKey("CustomerId")]
        public User? Customer { get; set; }

        [ForeignKey("ReservationId")]
        public Reservation? Reservation { get; set; }
    }
}
