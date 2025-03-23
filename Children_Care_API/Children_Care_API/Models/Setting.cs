using System.ComponentModel.DataAnnotations;

namespace Children_Care_API.Models
{
    public class Setting
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Type { get; set; } = string.Empty;

        [Required, MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Value { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool Status { get; set; } = true;
    }
}
