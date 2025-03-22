using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Children_Care_API.Models.Enums;

namespace Children_Care_API.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ReservationId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required, MaxLength(50)]
        public PaymentMethod PaymentMethod { get; set; }

        [Required, MaxLength(50)]
        public PaymentStatus Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("ReservationId")]
        public Reservation? Reservation { get; set; }
    }
}
