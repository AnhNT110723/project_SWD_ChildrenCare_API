using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Children_Care_API.Models
{
    public class MedicalExamination
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ReservationId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public DateTime ExaminationDate { get; set; }

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; }

        [ForeignKey("ReservationId")]
        public Reservation? Reservation { get; set; }

        [ForeignKey("DoctorId")]
        public User? Doctor { get; set; }
    }
}
