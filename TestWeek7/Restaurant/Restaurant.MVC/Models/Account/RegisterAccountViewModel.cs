using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Models.Account
{
    public class RegisterAccountViewModel
    {
        [DisplayName("Email"), Required(ErrorMessage = "Username is mandatory")]
        [EmailAddress(ErrorMessage = "Specified an invalid email")]
        public string Email { get; set; }

        [DisplayName("Password"), Required(ErrorMessage = "Password is mandatory")]
        [MinLength(5, ErrorMessage = "The password should be at least 5 characters in length")]
        public string Password { get; set; }

        [DisplayName("ConfirmPassword"), Required(ErrorMessage = "Password is mandatory")]
        [MinLength(5, ErrorMessage = "The password should be at least 5 characters in length")]
        public string ConfirmPassword { get; set; }
    }
}