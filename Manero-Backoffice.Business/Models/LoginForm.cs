using System.ComponentModel.DataAnnotations;

namespace Manero_Backoffice.Business.Models
{
    public class LoginForm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public bool LockoutOnFailure { get; set; }
    }
}
