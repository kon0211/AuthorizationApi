using AuthorizationApi.Domain;
using System.ComponentModel.DataAnnotations;

namespace AuthorizationApi.Models
{
    /// <summary>
    /// Data required to change the profile
    /// </summary>
    public class UpdateUserModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }


        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(RegExp.Phone, ErrorMessage = "Incorrect phone")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(RegExp.Email, ErrorMessage = "Incorrect email")]
        public string Email { get; set; }


        #region Methods

        /// <summary>
        /// Updates user's properties
        /// </summary>
        /// <param name="user">user for update</param>
        public void UpdateUser(User user)
        {
            user.Name = Name;
            user.Surname = Surname;
            user.Phone = Phone;
            user.Email = Email;
        }
        #endregion //methods
    }
}
