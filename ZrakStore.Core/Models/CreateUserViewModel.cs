using System.ComponentModel.DataAnnotations;

namespace ZrakStore.Core.Models
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [MinLength(4, ErrorMessage = "{0} has to be at least {1} characters long")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "{0} has to be at least {1} characters long")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        [Display(Name = "Confirm Password")]
        public string PasswordConfirm { get; set; }
    }
}
