using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Children_Care_API.Models
{
    public class MedicalPrescription
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PatientName { get; set; }

        [Required]
        public string Medicine { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Doctor { get; set; }

        public DateTime DateIssued { get; set; } = DateTime.UtcNow;
    }
}