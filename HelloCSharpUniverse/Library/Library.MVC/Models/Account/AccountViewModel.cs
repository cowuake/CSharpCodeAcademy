using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.MVC.Models.Account
{
    public class AccountViewModel
    {
        [DisplayName("Username"), Required]
        public string Username { get; set; }
        
        [DisplayName("Password"), Required]
        public string Password { get; set; }
    }
}