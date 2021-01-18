using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class UserForCreationDto
    {

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(500)]
        public string Password { get; set; }
    }
}
