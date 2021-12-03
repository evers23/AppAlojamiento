using System.ComponentModel.DataAnnotations;

namespace AppAloj.WebAPI.Models.Usuario
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
