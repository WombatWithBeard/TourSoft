using System.ComponentModel.DataAnnotations;

namespace ToursSoft.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан Login")]
        public string Login { get; set; }
         
        [Required(ErrorMessage = "Не указан password")]
        [DataType(DataType.Password)]
        public string Password  { get; set; }
    }
}