using System.ComponentModel.DataAnnotations;

namespace Flambee.WebAPI.Models.Authentication
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username/email/phone number is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
