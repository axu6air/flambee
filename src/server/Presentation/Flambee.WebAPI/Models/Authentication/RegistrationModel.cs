using System;
using System.ComponentModel.DataAnnotations;

namespace Flambee.WebAPI.Models.Authentication
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "User Name is required")]
        [RegularExpression("^[-0-9A-Za-z_-]{4,15}$", ErrorMessage = "Invalid Username")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        public string PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}
