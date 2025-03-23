using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Children_Care_API.Models.Enums;

namespace Children_Care_API.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        public DateTime ReservationDate { get; set; }

        [Required, MaxLength(20)]
        public ReservationStatus Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("CustomerId")]
        public User? Customer { get; set; }

        [ForeignKey("ServiceId")]
        public Service? Service { get; set; }
        public ICollection<Prescription>? Prescriptions { get; set; }
        public ICollection<Payment>? Payments { get; set; }
        public ICollection<MedicalExamination>? MedicalExaminations { get; set; }
    }
}
