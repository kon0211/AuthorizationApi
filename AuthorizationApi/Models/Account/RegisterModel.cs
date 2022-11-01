using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
#nullable enable

namespace AuthorizationApi.Models
{
    public class RegisterModel
    {
        #region nesessary fields

        [Required(ErrorMessage = "Login is required")]
        public string Login { get; set; } = string.Empty;


        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;


        [Required(ErrorMessage = "Password confirmation is required")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not mathed")]
        public string PasswordConfirm { get; set; } = string.Empty;


        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(RegExp.Email, ErrorMessage = "Incorrect email")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        #endregion //nesessary fields

        #region not nesessary fields

        public string? Surname { get; set; }

        public string? Phone { get; set; }

        #endregion //not nesessary fields

    }
}
