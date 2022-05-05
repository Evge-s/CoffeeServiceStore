using System.ComponentModel.DataAnnotations;

namespace CoffeeService.Shared.User
{
    public class UserRegister
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Compare("Password", ErrorMessage = "The password do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
