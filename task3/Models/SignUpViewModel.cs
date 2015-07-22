using System.ComponentModel.DataAnnotations;
using task3.Infrastructure;

namespace task3.Models
{
    [PasswordsMatch(ErrorMessage = "Passwords do not match")]
    public class SignUpViewModel
    {
        [UniqueUsername(ErrorMessage = "User name is already exists")]
        [Display(Name = "User name")]
        [Required(ErrorMessage = "Field is required")]
        [MinLength(2, ErrorMessage = "Name is too short.")]
        [MaxLength(15, ErrorMessage = "Name is too long. 20 characters are allowed")]
        [RegularExpression(@"^[a-zA-Z0-9_-]*$", ErrorMessage = "Only latin letters, digits, \"_\" and \"-\" are allowed")]
        public string Username { get; set; }

        [UniqueEmail(ErrorMessage = "Email is already exists")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Field is required")]
        public string Email { get; set; }


        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Field is required")]
        public string Password { get; set; }


        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Field is required")]
        public string ConfirmPassword { get; set; }
    }
}