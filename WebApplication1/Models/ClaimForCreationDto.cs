using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ClaimForCreationDto
    {
        [Required]
        [MaxLength(10)]
        public string Type { get; set; }

        [Required]
        [MaxLength(200)]
        public string Value { get; set; }

    }
}
