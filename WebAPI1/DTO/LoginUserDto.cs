using System.ComponentModel.DataAnnotations;

namespace WebAPI1.DTO
{
    public class LoginUserDto
    {
        [Required]
        public string ?Name { get; set; }
        [Required]
        public string ?Password { get; set; }   
    }
}
