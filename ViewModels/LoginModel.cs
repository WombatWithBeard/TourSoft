using System.ComponentModel.DataAnnotations;

namespace ToursSoft.ViewModels
{
    /// <summary>
    /// LoginModel class: using only in authentication
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// User login
        /// </summary>
        [Required(ErrorMessage = "Login is not entered")]
        public string Login { get; set; }
         
        /// <summary>
        /// User password
        /// </summary>
        [Required(ErrorMessage = "Password is not entered")]
        [DataType(DataType.Password)]
        public string Password  { get; set; }
    }
}