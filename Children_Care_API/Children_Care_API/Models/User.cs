using Children_Care_API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Children_Care_API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public Role Role { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public ICollection<Reservation>? Reservations { get; set; }
        public ICollection<Feedback>? Feedbacks { get; set; }
        public ICollection<Prescription>? PrescriptionsAsDoctor { get; set; } // Đơn thuốc do bác sĩ kê
        public ICollection<Prescription>? PrescriptionsAsCustomer { get; set; } // Đơn thuốc của khách hàng
        public ICollection<Blog>? Blogs { get; set; } // Blog do user viết
    }
}
