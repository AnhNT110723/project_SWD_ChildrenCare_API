using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Children_Care_API.Models.Enums;

namespace Children_Care_API.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;
        [MaxLength(500)]
        public string? BriefInfo { get; set; } //brief-info

        [MaxLength(255)]
        public string? Thumbnail { get; set; } //thumbnail

        [Required, MaxLength(50)]
        public BlogCategory Category { get; set; } = BlogCategory.News;

        public bool IsFeatured { get; set; } = false; //featured

        public bool Status { get; set; } = true;

        [Required]
        public int AuthorId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        [ForeignKey("AuthorId")]
        public User? Author { get; set; }
    }
}
