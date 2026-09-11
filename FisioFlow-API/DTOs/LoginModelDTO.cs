using System.ComponentModel.DataAnnotations;

namespace FisioFlow_API.DTOs
{
    public class LoginModelDTO
    {
        [Required(ErrorMessage = "Username is required.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
    }
}
