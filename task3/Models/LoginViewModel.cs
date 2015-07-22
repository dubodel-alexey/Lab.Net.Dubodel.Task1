using System.ComponentModel.DataAnnotations;
using task3.Infrastructure;

namespace task3.Models
{
    [LoginValidation(ErrorMessage = "Incorrect username and/or password")]
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Field is required")]
        [Display(Name = "User name or Email")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}