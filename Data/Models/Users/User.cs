using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace ToursSoft.Data.Models.Users
{
    [Table("User")]
    public class User: Model
    {
        private string _password;

        public User(Guid id, string name, string company, int phoneNumber, string login, string password, string role) 
            : base(id)
        {
            Name = name;
            Company = company;
            PhoneNumber = phoneNumber;
            Login = login;
            Password = password;
            Role = role;
        }

        public User()
        {
        }

        /// <summary>
        /// Name of the User
        /// </summary>
        [Required]
        public string Name { get; set; }
        
        /// <summary>
        /// info about company name
        /// </summary>
        /// 
        public string Company { get; set; }

        /// <summary>
        /// User phone
        /// </summary>
        public long PhoneNumber { get; set; }

        /// <summary>
        /// User login
        /// </summary>
        [Required]
        public string Login { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        [Required]
        public string Password
        {
            get => _password;
            set => _password = Convert.ToBase64String(MD5.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(value)));
        }

        /// <summary>
        /// attribute: manager, admin, user
        /// </summary>
        [Required]
        public string Role { get; set; }
    }
}