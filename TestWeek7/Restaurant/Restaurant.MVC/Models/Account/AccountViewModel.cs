using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Models.Account
{
    public class AccountViewModel
    {
        [DisplayName("Email"), Required]
        public string Email { get; set; }
        
        [DisplayName("Password"), Required]
        public string Password { get; set; }
    }
}