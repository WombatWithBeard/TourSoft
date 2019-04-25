using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace ToursSoft.Data.Models.Users
{
    /// <summary>
    /// User class: info about soft user
    /// </summary>
    [Table("User")]
    public class User: Model
    {
        private string _password;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="company"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        public User(Guid id, string name, string company, int phoneNumber, string login, string password) 
            : base(id)
        {
            Name = name;
            Company = company;
            PhoneNumber = phoneNumber;
            Login = login;
            Password = password;
        }

        /// <inheritdoc />
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
        /// link to role table
        /// </summary>
        public IList<UserRole> UserRoles { get; set; }
    }
}