using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace ToursSoft.Data.Models
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